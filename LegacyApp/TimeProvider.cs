using System;

namespace LegacyApp
{
    class TimeProvider : ITimeProvider
    {
        public DateTime Now
        {
            get => DateTime.Now;
        }
    }
}
