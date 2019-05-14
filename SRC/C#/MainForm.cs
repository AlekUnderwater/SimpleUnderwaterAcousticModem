using NAudio.Wave;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace SimpleUnderwaterAcousticModem
{
    public partial class MainForm : Form
    {
        #region Properties

        WaveIn waveIn;
        bool isTransmitting = false;

        SUAModem modem;

        int threshold
        {
            get { return thresholdTkb.Value; }
            set
            {
                int val = value;
                if (val > thresholdTkb.Maximum) val = thresholdTkb.Maximum;
                if (val < thresholdTkb.Minimum) val = thresholdTkb.Minimum;
                thresholdTkb.Value = val;
                thresholdTkb_Scroll(null, null);
            }
        }

        int sampleRateHz
        {
            get { return (int)sampleRateHzCbx.SelectedItem; }
            set
            {
                int idx = sampleRateHzCbx.Items.IndexOf(value);
                if (idx >= 0)
                    sampleRateHzCbx.SelectedIndex = idx;
            }
        }

        int windowSize
        {
            get { return (int)windowSizeCbx.SelectedItem; }
            set
            {
                int idx = windowSizeCbx.Items.IndexOf(value);
                if (idx >= 0)
                    windowSizeCbx.SelectedIndex = idx;
            }
        }

        int b1Multiplier
        {
            get { return (int)b1DurationCbx.SelectedItem; }
            set
            {
                int idx = b1DurationCbx.Items.IndexOf(value);
                if (idx >= 0)
                    b1DurationCbx.SelectedIndex = idx;
            }
        }

        int defIntDuration
        {
            get { return (int)defIntDurationCbx.SelectedItem; }
            set
            {
                int idx = defIntDurationCbx.Items.IndexOf(value);
                if (idx >= 0)
                    defIntDurationCbx.SelectedIndex = idx;
            }
        }

        #endregion

        public MainForm()
        {
            InitializeComponent();

            sampleRateHzCbx.Items.AddRange(new object[] { 44100, 48000, 96000, 192000 });
            windowSizeCbx.Items.AddRange(new object[] { 32, 64, 128, 256, 512 });
            b1DurationCbx.Items.AddRange(new object[] { 2, 4, 8, 16, 32, 64 });
            defIntDurationCbx.Items.AddRange(new object[] { 2, 4, 8, 16, 32, 64, 128, 256, 512 });

            sampleRateHz = 96000;
            windowSize = 128;
            b1Multiplier = 8;
            defIntDuration = 32;
            threshold = 500;
  
        }

        private void startStopBtn_Click(object sender, EventArgs e)
        {
            //modem = new SUAModem(sampleRateHz, windowSize, b0Multiplier, b0Multiplier * 2, defIntDuration, threshold, 0.2, 0.8);
            //modem.DataReceivedEventHandler += new EventHandler<ByteReceivedEventArgs>(modem_DataReceived);
            //modem.ReInit();
            //WaveFileReader wfr = new WaveFileReader("C:\\Temp\\0-0-0.wav");
            //byte[] buffer = new byte[512];

            //while (wfr.Position != wfr.Length)
            //{
            //    int readBytes = wfr.Read(buffer, 0, buffer.Length);

            //    int samplesLength = readBytes / 2;
            //    short[] samples = new short[samplesLength];
            //    for (int i = 0; i < samplesLength; i++)
            //        samples[i] = BitConverter.ToInt16(buffer, i * 2);
            //    modem.ProcessInputSignal(samples);
            //}
            //wfr.Close();                     

            if ((modem != null) && (modem.IsRunning))
            {
                modem.Stop();

                waveIn.StopRecording();
                startStopBtn.Text = "START";
                startStopBtn.Checked = false;                            
            }
            else
            {
                modem = new SUAModem(sampleRateHz, windowSize, b1Multiplier, b1Multiplier * 2, defIntDuration, threshold);
                modem.DataReceivedEventHandler += new EventHandler<ByteReceivedEventArgs>(modem_DataReceived);   

                modem.Start();

                waveIn = new WaveIn();
                waveIn.WaveFormat = new WaveFormat(sampleRateHz, 1);
                waveIn.BufferMilliseconds = 100;
                waveIn.NumberOfBuffers = 3;

                waveIn.DeviceNumber = 0;
                waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailable);
                waveIn.RecordingStopped += new EventHandler<StoppedEventArgs>(waveIn_RecordingStopped);

                waveIn.StartRecording();

                startStopBtn.Text = "STOP";
                startStopBtn.Checked = true;
            }

            terminalGroup.Enabled = modem.IsRunning;
            settingsGroup.Enabled = !modem.IsRunning;
        }

        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (!isTransmitting)
            {
                int samplesLength = e.BytesRecorded / 2;
                short[] samples = new short[samplesLength];
                for (int i = 0; i < samplesLength; i++)
                    samples[i] = BitConverter.ToInt16(e.Buffer, i * 2);
                modem.ProcessInputSignalAsync(samples);
            }
        }

        private void waveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            //          
        }

        private void modem_DataReceived(object sender, ByteReceivedEventArgs e)
        {            
            InvokeAppendLine(string.Format(">> {0}\r\n", Encoding.ASCII.GetString(new byte[] { e.Data })));
        }

        private void AppendLine(string line)
        {
            terminalTxb.AppendText(line);
            if (!line.EndsWith("\r\n"))
                terminalTxb.AppendText("\r\n");
        }

        private void InvokeAppendLine(string line)
        {
            if (terminalTxb.InvokeRequired)
                terminalTxb.Invoke((MethodInvoker)delegate { AppendLine(line); });
            else
                AppendLine(line);
        }

        private void InvokeSetStatusLabelText(StatusStrip strip, ToolStripStatusLabel lbl, string text)
        {
            if (strip.InvokeRequired)
                strip.Invoke((MethodInvoker)delegate { lbl.Text = text; });
            else
                lbl.Text = text;
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            isTransmitting = true;
            this.Enabled = false;

            var txTime = modem.TransmitData(Encoding.ASCII.GetBytes(textToSendTxb.Text));
            AppendLine(string.Format("<< {0} (Tx time: {1:F03} sec, Tx speed: {2:F01} baud)\r\n", textToSendTxb.Text, txTime, 8.0 * textToSendTxb.Text.Length / txTime));

            this.Enabled = true;
            isTransmitting = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            sendBtn.Enabled = !string.IsNullOrEmpty(textToSendTxb.Text);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            terminalTxb.ScrollToCaret();
        }

        private void thresholdTkb_Scroll(object sender, EventArgs e)
        {
            if (modem != null)
                modem.Threshold = threshold;

            thresholdGroup.Text = string.Format("Threshold = {0}", threshold);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (modem != null)
            {
                modem.Stop();
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Process.Start(toolStripLabel1.Text);
        }
    }
}
