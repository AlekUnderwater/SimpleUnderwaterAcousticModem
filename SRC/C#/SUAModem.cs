using NAudio.Wave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SimpleUnderwaterAcousticModem
{
    public class ByteReceivedEventArgs : EventArgs
    {
        public byte Data { get; private set; }

        public ByteReceivedEventArgs(byte data)
        {
            Data = data;
        }
    }

    public class SUAModem
    {
        #region Properties

        public static readonly double MinEnergyThreshold = 10;
        public static readonly double MaxEnergyThreshold = 100000;

        public static readonly double MinBitDurationMs = 1;
        public static readonly double MaxBitDurationMs = 1000;

        public double SampleRateHz { get; private set; }
        
        int windowSize;
        public int WindowSize
        {
            get { return windowSize; }
        }
        
        int oneDurationSmp;
        int zeroDurationSmp;
        int defenseIntervalSmp;

        double energyThreshold;
        double currentEnergy;
        double prevEnergy;

        public double Threshold
        {
            get { return energyThreshold; }
            set
            {
               if (value.IsInRange(MinEnergyThreshold, MaxEnergyThreshold))
                    energyThreshold = value;
               else
                    throw new ArgumentOutOfRangeException("value");
            }
        }

        public double CurrentEnergy
        {
            get { return currentEnergy; }
        }

        double oneDurationMs;
        public double OneDurationMs
        {
            get { return oneDurationMs; }           
        }

        double zeroDurationMs;
        public double ZeroDurationMs
        {
            get { return zeroDurationMs; }           
        }       

        double defenseIntervalMs;
        public double DefenseIntervalMs
        {
            get { return defenseIntervalMs; }
        }

        double delta;
        double alimit;

        short[] ring;
        int rWPos = 0;
        int rRPos = 0;
        int rCnt = 0;
        int rSize = 0;

        int[] dRing1;
        int[] dRing2;
        double s1, s2;
        int rHead, rTail;
        int cycle;

        bool isRise = false;
        int riseSmp = 0;

        int bPos;
        int samplesSinceLastBit = 0;
        byte data;

        int skip = 0;

        int rLock = 0;


        bool stopped = true;

        public bool IsRunning { get { return !stopped; } }

        #endregion

        #region Constructor

        public SUAModem(double sRateHz, int wSize, int oneMultiplier, int zeroMultiplier, int defIntMultiplier, double eThreshold)
        {
            if (!sRateHz.IsInRange(44100, 192000))
                throw new ArgumentOutOfRangeException("sRateHz");
            else
                SampleRateHz = sRateHz;

            if (!wSize.IsInRange(16, 512))
                throw new ArgumentOutOfRangeException("wSize");
            else
                windowSize = wSize;

            if (!oneMultiplier.IsInRange(4, 128))
                throw new ArgumentOutOfRangeException("oneMultiplier");
            else
            {
                oneDurationSmp = windowSize * oneMultiplier;
                oneDurationMs = SMP2MS(oneDurationSmp, SampleRateHz);                
            }

            if (!zeroMultiplier.IsInRange(4, 128))
                throw new ArgumentOutOfRangeException("zeroMultiplier");
            else
            {
                zeroDurationSmp = oneDurationSmp * 2;
                zeroDurationMs = SMP2MS(zeroDurationSmp, SampleRateHz);
            }

            if (!defIntMultiplier.IsInRange(4, 256))
                throw new ArgumentOutOfRangeException("defIntMultiplier");
            {
                defenseIntervalSmp = windowSize * defIntMultiplier;
                defenseIntervalMs = SMP2MS(defenseIntervalSmp, SampleRateHz);
            }          

            Threshold = eThreshold;          
 
            delta = Math.PI / 2; // Math.PI * 2 * (SampleRateHz / 4) / SampleRateHz;
            alimit = Math.PI * 2;

            rSize = 65536;
            ring = new short[rSize];

            dRing1 = new int[windowSize];
            dRing2 = new int[windowSize];
            rHead = windowSize - 1;
            rTail = 0;
            cycle = 0;
            s1 = 0;
            s2 = 0;  
        }

        #endregion

        #region Methods

        public static int MS2SMP(double durationInMs, double sampleRateHz)
        {
            return Convert.ToInt32(sampleRateHz * durationInMs / 1000.0);
        }

        public static double SMP2MS(int durationInSmp, double sampleRateHz)
        {
            return (durationInSmp / sampleRateHz) * 1000.0;
        }


        private void AddBit(bool bit)
        {                     
            if (bit)
                data |= (byte)(1 << bPos);

            if (++bPos >= 8)
            {
                bPos = 0;                
                DataReceivedEventHandler.RiseInvoke(this, new ByteReceivedEventArgs(data));
                data = 0;
            }
        }

        private void DiscardBits()
        {
            DataReceivedEventHandler.Rise(this, new ByteReceivedEventArgs(data));
            bPos = 0;
            data = 0;
        }

        public void ReInit()
        {
            Array.Clear(ring, 0, ring.Length);            
            Array.Clear(dRing1, 0, dRing1.Length);
            Array.Clear(dRing2, 0, dRing2.Length);

            s1 = 0;
            s2 = 0;
            cycle = 0;
            isRise = false;
            data = 0;
            bPos = 0;
            riseSmp = 0;
            samplesSinceLastBit = 0;
        }

        public void Start()
        {
            Thread tr = new Thread(new ThreadStart(Receiver));
            stopped = false;
            ReInit();
            tr.Start();
        }

        public void Stop()
        {
            stopped = true;
        }

        private void Receive()
        {
            while (Interlocked.CompareExchange(ref rLock, 1, 0) != 0)
                Thread.SpinWait(1);


            int a;
            while (rCnt >= 4)
            {
                a = ring[rRPos];
                rRPos = (rRPos + 1) % rSize;
                rCnt--;
                dRing1[rHead] = a;
                dRing2[rHead] = a;
                s1 += a - dRing1[rTail];
                s2 += a - dRing2[rTail];
                rHead = (rHead + 1) % windowSize;
                rTail = (rTail + 1) % windowSize;

                a = ring[rRPos];
                rRPos = (rRPos + 1) % rSize;
                rCnt--;
                dRing1[rHead] = a;
                dRing2[rHead] = -a;
                s1 += a - dRing1[rTail];
                s2 += -a - dRing2[rTail];
                rHead = (rHead + 1) % windowSize;
                rTail = (rTail + 1) % windowSize;

                a = ring[rRPos];
                rRPos = (rRPos + 1) % rSize;
                rCnt--;
                dRing1[rHead] = -a;
                dRing2[rHead] = -a;
                s1 += -a - dRing1[rTail];
                s2 += -a - dRing2[rTail];
                rHead = (rHead + 1) % windowSize;
                rTail = (rTail + 1) % windowSize;

                a = ring[rRPos];
                rRPos = (rRPos + 1) % rSize;
                rCnt--;
                dRing1[rHead] = -a;
                dRing2[rHead] = a;
                s1 += -a - dRing1[rTail];
                s2 += a - dRing2[rTail];
                rHead = (rHead + 1) % windowSize;
                rTail = (rTail + 1) % windowSize;

                if (++cycle >= windowSize)
                {
                    cycle = 0;
                    currentEnergy = Math.Sqrt(s1 * s1 + s2 * s2) / windowSize;
                    double de = currentEnergy - prevEnergy;                                     

                    #region analysis

                    if (skip > 0)
                        skip -= windowSize * 4;
                    else
                    {
                        if (isRise)
                        {
                            if (de > -Threshold)
                            {
                                riseSmp += windowSize * 4;
                            }
                            else
                            {
                                // analyse symbol
                                isRise = false;

                                double oneDiff = Math.Abs(oneDurationSmp - riseSmp);
                                double zeroDiff = Math.Abs(zeroDurationSmp - riseSmp);

                                if (oneDiff > zeroDiff)
                                {
                                    // Mostly likely "0"
                                    AddBit(false);
                                }
                                else
                                {
                                    // Mostly likely "1"
                                    AddBit(true);
                                }

                                samplesSinceLastBit = 0;
                                skip = defenseIntervalSmp / 2;
                            }
                        }
                        else
                        {
                            if (de > Threshold)
                            {
                                isRise = true;
                                riseSmp = windowSize * 4;
                            }
                        }
                    }

                    #endregion

                    prevEnergy = currentEnergy;

                    if (bPos > 0)
                    {
                        if (samplesSinceLastBit >= defenseIntervalSmp*2 + zeroDurationSmp + oneDurationMs)
                            DiscardBits();                           

                        samplesSinceLastBit += 4 * windowSize;
                    }
                }                
            }

            Interlocked.Decrement(ref rLock);
        }        

        private void Receiver()
        {
            while (!stopped)
            {
                Receive();
                Thread.SpinWait(1);
            }
        }
        
        public void ProcessInputSignalAsync(short[] data)
        {           
            #region write samples to main ring

            while (Interlocked.CompareExchange(ref rLock, 1, 0) != 0)
                Thread.SpinWait(1);

            for (int i = 0; i < data.Length; i++)
            {
                ring[rWPos] = data[i];
                rWPos = (rWPos + 1) % rSize;
                rCnt++;
            }

            Interlocked.Decrement(ref rLock);

            #endregion                                   
        }

        public void ProcessInputSignal(short[] data)
        {
            #region write samples to main ring

            for (int i = 0; i < data.Length; i++)
            {
                ring[rWPos] = data[i];
                rWPos = (rWPos + 1) % rSize;
                rCnt++;
            }

            #endregion      

            Receive();
        }

        public short[] ModulateData(byte[] data)
        {
            double alpha = 0;
            double phase = 0;           

            List<short> samples = new List<short>();            
            BitArray bits = new BitArray(data);         

            for (int i = 0; i < bits.Length; i++)
            {
                int sLim = (bits[i]) ? oneDurationSmp : zeroDurationSmp;

                alpha = 0;
                phase = 0;
                for (int sIdx = 0; sIdx <= sLim; sIdx++)
                {
                    alpha = Math.Sin(phase);
                    phase += delta;
                    if (phase >= alimit)
                        phase -= alimit;
                    samples.Add(Convert.ToInt16(alpha * short.MaxValue));
                }

                samples.AddRange(new short[defenseIntervalSmp]);                
            }

            return samples.ToArray();
        }

        public double TransmitData(byte[] data)
        {
            var samples = ModulateData(data);
            double txTime = ((double)samples.Length) / SampleRateHz;

            var rawBytes = new byte[samples.Length * 2];
            for (int i = 0; i < samples.Length; i++)
            {
                var bts = BitConverter.GetBytes(samples[i]);
                rawBytes[i * 2] = bts[0];
                rawBytes[i * 2 + 1] = bts[1];
            }

            using (var ms = new MemoryStream(rawBytes))
            {
                using (var rs = new RawSourceWaveStream(ms, new WaveFormat(Convert.ToInt32(SampleRateHz), 16, 1)))
                {
                    using (var wo = new WaveOutEvent())
                    {
                        wo.Init(rs);
                        wo.Play();
                        while (wo.PlaybackState == PlaybackState.Playing)
                            Thread.SpinWait(1);
                    }

                    rs.Close();
                }
                ms.Close();
            }
            
            return txTime;
        }

        #endregion

        #region Events

        public EventHandler<ByteReceivedEventArgs> DataReceivedEventHandler;

        #endregion
    }
}
