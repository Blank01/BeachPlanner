using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Model
{
    interface IOrdine
    {
        ICliente Cliente { get; }
        IList<string> Affitti { get; }
        DateTime Giorno { get; }
        string ID { get; }
    }
}
