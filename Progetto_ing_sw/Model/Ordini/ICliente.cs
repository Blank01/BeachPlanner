using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Model
{
    interface ICliente
    {
        string Nome { get; }
        string ID { get; }
        string Mail { get; }
        string Hotel { get; } 
    }
}
