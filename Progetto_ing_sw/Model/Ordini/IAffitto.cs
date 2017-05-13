using Progetto_ing_sw.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Model
{
    public interface IAffitto
    {
        string ID { get; }
        DateRange Periodo { get; set; }
        PezzoFisso Posto { get; }
        int Lettini { get; set; }
        int Sdraio { get; set; }
        int Sedie { get; set; }
    }
}
