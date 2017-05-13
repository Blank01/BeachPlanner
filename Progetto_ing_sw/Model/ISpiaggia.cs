using Progetto_ing_sw.Model.Ordini;
using Progetto_ing_sw.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_ing_sw.Model
{
    interface ISpiaggia
    {
        IDictionary<string,IPezzo> Pezzi { get; }
        IList<IArea> Aree { get; set; }
        IList<IOrdine> Ordini { get; set; }
        IDictionary<string, IAffitto> Affitti { get; set; }
        IList<IPrezzo> Sconti { get; set; }
        IList<ICliente> Clienti { get; set; }

        string Filename { get; set; }
        DateRange Periodo { get; set; }
        string FilenameOrdini { get; set; }

        //Crea spiaggia
        event EventHandler PezziChanged;
        event EventHandler PezziMoved;
        event EventHandler AreaChanged;
        event EventHandler SpiaggiaLoaded;
        event EventHandler PeriodoChanged;
        event EventHandler OrdiniChanged;
        event EventHandler OrdiniSalvati;



        void OnPezziChanged();
        void OnPezziMoved(object sender, IList<IPezzo> pezzi);
        void OnAreaChanged();
        void OnPeriodoChanged();
        void OnOrdiniSalvati();

        IPezzo GetPezzo(string id);
        IOrdine GetOrdine(string id);
        void AggiungiPezzo(IPezzo pezzo);
        void RimuoviPezzo(IPezzo pezzo);
        void AggiungiPezzi(IList<IPezzo> pezzi);
        void RimuoviPezzi(IList<IPezzo> pezzi);

        void AggiungiArea(IArea area);
        void RimuoviArea(IArea area);
        IArea GetArea(string id);
        IDictionary<IArea, int> GetAree(IPezzo p, DateRange periodo);
        IList<IArea> GetAree(IPezzo p);

        bool isAvailable(IPezzo pezzo);

        void AggiungiOrdine(IOrdine ordine);
        void RimuoviOrdine(IOrdine ordine);
        double CalcolaPrezzo(IAffitto affitto);
        
        IList<IAffitto> AffittiInPeriodo(DateRange periodo);
        
        IList<IAffitto> AffittiCorrenti();
        void UpdateOccupati();
        IList<IPezzo> PezziOccupati();
        IList<IPezzo> PezziLiberi();
        IList<IPezzo> PezziOccupati(DateRange periodo);

        void SalvaConNomeSpiaggia(String filename);
        bool SalvaSpiaggia();
        bool CaricaSpiaggia(String filename);
        void AggiungiAffitto(IAffitto affitto);
        void RimuoviAffitto(string id);
        void SalvaConNomeOrdini(string filenameOrdini);
    }
}
