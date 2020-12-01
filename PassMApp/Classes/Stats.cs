using System;

namespace djane
{
    internal class Stats
    {
        public string attempts { get; internal set; }
        public string progress { get; internal set; }
        public string account { get; internal set; }

        internal static void Add(Stats stats)
        {
            throw new NotImplementedException();
        }
    }
}