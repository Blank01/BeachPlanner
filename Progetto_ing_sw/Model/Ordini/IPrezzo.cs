using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Model.Ordini
{
    public interface IPrezzo
    {
        string Nome { get; set; }
        int ScontoPercentuale { get; set; }
        bool ScontoProgressivo { get; set; }
        double Prezzo { get; set; }
        double CalcolaPrezzo(IAffitto affitto);


    }
}
