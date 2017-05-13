using System;

namespace Progetto_ing_sw.Exceptions
{
    class PrezzoException : Exception
    {
        public PrezzoException()
        {
        }

        public PrezzoException(string message)
            : base(message)
        {
        }

        public PrezzoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
