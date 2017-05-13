using Progetto_ing_sw.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Model
{
    public interface IArea
    {
        string Nome { get; }
        IDictionary<string, double> PrezziPezziMobili { get; set;}
        IDictionary<String, double> Prezzi { get; set; }
        DateRange Periodo { get; set; }
        string ID { get;}

        double CalcolaPrezzo(String id, DateRange periodo);
        double CalcolaPrezzo(IList<IPezzo> pezzi, DateRange periodo);
        void AggiungiPezzi(IDictionary<String, double> pezziContenuti);
        void RimuoviPezzo(string id);
        void RimuoviPezzoMobile(string tipo);
        void AggiungiPezzoMobile(string tipoMobile, double prezzo);
    }
}
