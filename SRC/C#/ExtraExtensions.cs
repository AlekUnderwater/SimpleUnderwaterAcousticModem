using System;

namespace SimpleUnderwaterAcousticModem
{
    public static class ExtraExtensions
    {
        public static void Rise(this EventHandler handler, object sender, EventArgs e)
        {
            if (handler != null)
                handler(sender, e);
        }

        public static void Rise<TEventArgs>(this EventHandler<TEventArgs> handler,
            object sender, TEventArgs e) where TEventArgs : EventArgs
        {
            if (handler != null)
                handler(sender, e);
        }

        public static void RiseInvoke<TEventArgs>(this EventHandler<TEventArgs> handler,
            object sender, TEventArgs e) where TEventArgs : EventArgs
        {
            if (handler != null)
                handler.BeginInvoke(sender, e, null, null);
        }

        public static bool IsInRange(this double value, double lowerBoundInclusive, double upperBoundInclusive)
        {
            return (value >= lowerBoundInclusive) && (value <= upperBoundInclusive);
        }

        public static bool IsInRange(this int value, int lowerBoundInclusive, int upperBoundInclusive)
        {
            return (value >= lowerBoundInclusive) && (value <= upperBoundInclusive);
        }

    }
}
