using System;

namespace Progetto_ing_sw
{
    public class DateRangeException : Exception
    {
        public DateRangeException()
        {
        }

        public DateRangeException(string message)
            : base(message)
        {
        }

        public DateRangeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
