using System;

namespace LegacyApp
{
    interface ITimeProvider
    {
        DateTime Now { get; }
    }
}
