using System;

namespace Model
{
    public class TimeClass: BaseFinType
    {
        public virtual TimeSpan Time { get; }
        public bool IsGreater(TimeClass other) =>  Time > other.Time;

    }
}