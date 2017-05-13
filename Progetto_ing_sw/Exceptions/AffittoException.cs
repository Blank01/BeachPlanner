using System;

namespace Progetto_ing_sw.Exceptions
{
    class AffittoException:Exception
    {
        public AffittoException()
        {
        }

        public AffittoException(string message)
            : base(message)
        {
        }

        public AffittoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
