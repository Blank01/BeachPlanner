using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Model.Ordini
{
    class PrezzoPezzoFisso : IPrezzo
    {
        private string _nome;
        private int _scontoPercentuale;
        private bool _scontoProgressivo;
        private double _prezzo;

        public PrezzoPezzoFisso(string nome, int scontoPercentuale, bool scontoProgressivo)
        {
            _nome = nome;
            _scontoPercentuale = scontoPercentuale;
            _scontoProgressivo = scontoProgressivo;
            _prezzo = -1;
        }
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public bool ScontoProgressivo
        {
            get { return _scontoProgressivo; }
            set { _scontoProgressivo = value; }
        }

        public int ScontoPercentuale
        {
            get { return _scontoPercentuale; }
            set { _scontoPercentuale = value; }
        }

        public double Prezzo
        {
            get { return _prezzo; }
            set { _prezzo = value; }
        }

        public double CalcolaPrezzo(IAffitto affitto)
        {
            double tot = 0;
            IDictionary<IArea, int> giorniAree = Spiaggia.GetInstance().GetAree(affitto.Posto, affitto.Periodo);
            if (ScontoProgressivo)
                tot += CalcolaScontoProgressivo(giorniAree, affitto);
            else
                tot+= CalcolaScontoTotale(giorniAree, affitto);
            _prezzo = Math.Round(tot, 2);
            return _prezzo;
        }

        private double CalcolaScontoTotale(IDictionary<IArea, int> giorniAree, IAffitto affitto)
        {
            double tot = 0;
            foreach(IArea area in giorniAree.Keys)
            {
                double prezzoGiornaliero = 0;
                prezzoGiornaliero += area.Prezzi[affitto.Posto.ID];
                prezzoGiornaliero += area.PrezziPezziMobili["Lettino"] * affitto.Lettini;
                prezzoGiornaliero += area.PrezziPezziMobili["Sedia"] * affitto.Sedie;
                prezzoGiornaliero += area.PrezziPezziMobili["Sdraio"] * affitto.Sdraio;
                double prezzoArea = prezzoGiornaliero * giorniAree[area];
                tot += prezzoArea;
            }
            tot = tot - (tot * ScontoPercentuale / 100.0);
            return tot;
        }

        private double CalcolaScontoProgressivo(IDictionary<IArea, int> giorniAree, IAffitto affitto)
        {
            double tot = 0;

            foreach (IArea area in giorniAree.Keys)
            {
                double prezzoGiornaliero = 0;
                prezzoGiornaliero += area.Prezzi[affitto.Posto.ID];
                prezzoGiornaliero += area.PrezziPezziMobili["Lettino"] * affitto.Lettini;
                prezzoGiornaliero += area.PrezziPezziMobili["Sedia"] * affitto.Sedie;
                prezzoGiornaliero += area.PrezziPezziMobili["Sdraio"] * affitto.Sdraio;
                for(int i=1;i<=giorniAree[area]; i++)
                {
                    double sconto = (tot + prezzoGiornaliero) * (ScontoPercentuale / 100.0);
                    tot = tot+prezzoGiornaliero - sconto;
                }
                
            }
            return tot;
        }
    }
}
