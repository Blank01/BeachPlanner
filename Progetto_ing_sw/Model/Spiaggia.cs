using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Progetto_ing_sw.Utils;
using Progetto_ing_sw.Model.Eventi;
using System.IO;
using Progetto_ing_sw.Persistence;
using System.Windows.Forms;
using System.Reflection;
using Progetto_ing_sw.Exceptions;
using Progetto_ing_sw.Model.Ordini;

namespace Progetto_ing_sw.Model
{
    class Spiaggia : ISpiaggia
    {
        private String filename;
        private static Spiaggia instance;
        private IDictionary<string, IPezzo> _pezzi;
        private IList<IArea> _aree;
        private IList<IOrdine> _ordini;
        private IDictionary<string, IAffitto> _affitti;
        private IList<IPrezzo> _sconti;
        private DateRange _periodo;
        private string ordiniFilename;
        private IList<ICliente> _clienti;

        public event EventHandler PezziChanged;
        public event EventHandler PezziMoved;
        public event EventHandler AreaChanged;
        public event EventHandler SpiaggiaLoaded;
        public event EventHandler PeriodoChanged;
        public event EventHandler OrdiniChanged;
        public event EventHandler OrdiniSalvati;
        protected Spiaggia()
        {
            _pezzi = new Dictionary<string, IPezzo>();
            _aree = new List<IArea>();
            _ordini = new List<IOrdine>();
            _affitti = new Dictionary<string, IAffitto>();
            _sconti = new List<IPrezzo>();
            _clienti = new List<ICliente>();
            filename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +"\\" + DateTime.UtcNow.ToShortDateString() + ".xml";
            PeriodoChanged += new EventHandler(PeriodoUpdate);

            //TODO Rimuovi questa parte
            _sconti.Add(new PrezzoPezzoFisso("Prezzo pieno", 0, false));
            _sconti.Add(new PrezzoPezzoFisso("Sconto 10%", 20, false));
            _sconti.Add(new PrezzoPezzoFisso("Sconto 10% Ricorsivo", 20, true));
            _sconti.Add(new PrezzoPezzoFisso("Sconto 20%", 20, false));
            _sconti.Add(new PrezzoPezzoFisso("Sconto 20% Ricorsivo", 20, true));
            _sconti.Add(new PrezzoPezzoFisso("Sconto 40%", 40, false));
            _sconti.Add(new PrezzoPezzoFisso("Sconto 40% Ricorsivo", 40, true));
            _sconti.Add(new PrezzoPezzoFisso("Buono hotel", 100, false));
            _sconti.Add(new CustomPrezzo("Prezzo Personalizzato", 0));
            //


        }

        public void UpdateOccupati()
        {
            foreach(IPezzo p in _pezzi.Values)
            {
                p.Lettini = 0;
                p.Sdraio = 0;
                p.Sedie = 0;
                p.Occupato = false;
                p.OccupatoParzialmente = false;
            }
            
            foreach (IPezzo p in PezziOccupati(_periodo))
            {
                _pezzi[p.ID] = p;
            }
            foreach (IPezzo p in PezziParzialmenteOccupati(_periodo))
            {
                _pezzi[p.ID] = p;
                _pezzi[p.ID].Occupato = false;
            }

            OnPezziChanged();
        }
        

        private void PeriodoUpdate(object sender, EventArgs e)
        {
            UpdateOccupati();
        }

        public String Filename { get { return this.filename; } set { this.filename = value; } }

        public String FilenameOrdini { get { return this.ordiniFilename; } set { this.ordiniFilename = value; } }

        event EventHandler ISpiaggia.AreaChanged
        {
            add { this.AreaChanged += value; }
            remove { this.AreaChanged -= value; }
        }
        event EventHandler ISpiaggia.SpiaggiaLoaded
        {
            add { this.SpiaggiaLoaded += value; }
            remove { this.SpiaggiaLoaded -= value; }
        }

        event EventHandler ISpiaggia.PezziChanged
        {
            add { this.PezziChanged += value; }
            remove { this.PezziChanged -= value; }
            
        }

        event EventHandler ISpiaggia.PezziMoved
        {
            add { this.PezziMoved += value; }

            remove { this.PezziMoved += value; }
        }

        public IDictionary<string, IPezzo> Pezzi
        {
            get { return _pezzi; }
            set
            {
                _pezzi = value;
            }
        }

        public IList<ICliente> Clienti
        {
            get
            {
                
                return _clienti;
            }
            set { _clienti = value; }
        }

        public IDictionary<string, IAffitto> Affitti
        {
            get { return this._affitti; }
            set { this._affitti = value; }
        }

        public IList<IOrdine> Ordini
        {
            get { return this._ordini; }
            set { this._ordini = value; }
        }
        
        public IList<IArea> Aree
        {
            get { return this._aree; }
            set { this._aree = value; }
        }

        public DateRange Periodo
        {
            get { return this._periodo; }
            set
            {
                _periodo = value;
                this.OnPeriodoChanged();
            }
        }

        public IList<IPrezzo> Sconti
        {
            get { return _sconti; }
            set { this._sconti = value; }
        }

        public void OnOrdiniChanged()
        {
            if (OrdiniChanged != null)
                OrdiniChanged(this, EventArgs.Empty);
        }

        public void OnPeriodoChanged()
        {
            if (PeriodoChanged != null)
                PeriodoChanged(this, EventArgs.Empty);
        }
        public void OnPezziChanged()
        {
            if (PezziChanged != null)
                PezziChanged(this, EventArgs.Empty);
        }

        public void OnAreaChanged()
        {
            if (AreaChanged != null)
                AreaChanged(this, EventArgs.Empty);
        }

        public void OnOrdiniSalvati()
        {
            if (OrdiniSalvati != null)
                OrdiniSalvati(this, EventArgs.Empty);
        }

        public void OnSpiaggiaLoaded()
        {
            this.OnPezziChanged();
            this.OnAreaChanged();
            if (SpiaggiaLoaded != null)
                SpiaggiaLoaded(this, EventArgs.Empty);
        }

        public static Spiaggia GetInstance()
        {
            if(instance == null)
            {
                instance = new Spiaggia();
            }
            return instance;
        }

        public IOrdine GetOrdine(string ID)
        {
            foreach (IOrdine o in _ordini)
                if (o.ID == ID)
                    return o;
            return null;
        }

        public IArea GetArea(string ID)
        {
            foreach (IArea a in this._aree)
            {
                if (a.ID == ID)
                    return a;
            }
            return null;
        }

        public IPezzo GetPezzo(string ID)
        {
            try
            {
                return _pezzi[ID];
            }catch { return null; }
        }

        public void AggiungiPezzo(IPezzo pezzo)
        {
            _pezzi.Add(pezzo.ID, pezzo);
            this.OnPezziChanged();

        }

        public void RimuoviPezzo(IPezzo pezzo)
        {
            _pezzi.Remove(pezzo.ID);
            this.OnPezziChanged();

        }

        void ISpiaggia.OnPezziMoved(object sender, IList<IPezzo> pezzi)
        {
            if (PezziChanged != null)
                PezziMoved(sender, new EventArgsPezziMoved(pezzi));
        }
        
        public void AggiungiPezzi(IList<IPezzo> pezzi)
        {
            foreach(IPezzo p in pezzi)
            {
                if (!_pezzi.Keys.Contains(p.ID))
                    _pezzi.Add(p.ID, p);
                else
                    _pezzi[p.ID] = p;
                
            }
            this.OnPezziChanged();
        }

        public void RimuoviPezzi(IList<IPezzo> pezzi)
        {
            foreach(IPezzo p in pezzi)
            {
                _pezzi.Remove(p.ID);
            }
            this.OnPezziChanged();
        }

        public void AggiungiAree(IList<IArea> aree)
        {
            foreach (IArea a in aree)
            {
                if (!_aree.Contains(a))
                    _aree.Add(a);
            }
            this.OnAreaChanged();
        }

        public void AggiungiAffitto(IAffitto affitto)
        {
            _affitti.Add(affitto.ID, affitto);
        }

        public void RimuoviAffitto(string id)
        {
            _affitti.Remove(id);
            foreach(IOrdine o in _ordini)
            {
                if (o.Affitti.Contains(id))
                {
                    o.Affitti.Remove(id);
                    if (!o.Affitti.Any())
                        _ordini.Remove(o);
                    return;
                }
                    
                
            }
            OnOrdiniChanged();
                
        }

        public bool SalvaSpiaggia()
        {
            return SpiaggiaPersistence.SaveSpiaggia(filename, _pezzi, _aree);
        }

        public IList<string> GetPezziMobili()
        {
            return new List<string> { "Lettino", "Sdraio", "Sedia" };
        }

        public void SalvaConNomeSpiaggia(String filename)
        {
            this.filename = filename;
            SpiaggiaPersistence.SaveSpiaggia(filename, _pezzi, _aree);
        }

        public void SalvaConNomeOrdini(string filename)
        {
            this.ordiniFilename = filename;
            SpiaggiaPersistence.SaveOrdini(filename);
            OnOrdiniSalvati();
        }

        public bool CaricaSpiaggia(string filename)
        {
            
            bool loaded = SpiaggiaPersistence.LoadSpiaggia(filename);
            if (!loaded)
                return false;

            this.filename = filename;
            this.OnSpiaggiaLoaded();
            return true;
        }

        public bool CaricaOrdini(string filename)
        {
            bool loaded = SpiaggiaPersistence.LoadOrdini(filename);
            if (!loaded)
                return false;

            this.ordiniFilename = filename;
            
            //this.OnOrdiniChanged();
            return true;
        }

        void ISpiaggia.AggiungiArea(IArea area)
        {
            _aree.Add(area);
            OnAreaChanged();
        }

        
        void ISpiaggia.RimuoviArea(IArea area)
        {
            _aree.Remove(area);
            OnAreaChanged();
        }

        double ISpiaggia.CalcolaPrezzo(IAffitto affitto)
        {
            throw new NotImplementedException();
        }

        IList<IPezzo> ISpiaggia.PezziOccupati()
        {
            IList<IPezzo> pezzi = new List<IPezzo>();
            foreach(IPezzo p in _pezzi.Values)
            {
                if (p.Occupato)
                    pezzi.Add(p);
            }
            return pezzi;
        }

        public IDictionary<IArea, int> GetAree(IPezzo p, DateRange periodo)
        {
            IDictionary<IArea, int> aree = new Dictionary<IArea, int>();
            if (p == null)
            {
                foreach(IArea a in _aree)
                {
                    if (a.Periodo.Intersection(periodo) > 0 && !a.Prezzi.Any())
                        aree.Add(a, a.Periodo.Intersection(periodo));
                }
                return aree;
            }
                
            foreach(IArea a in _aree)
            {
                bool intersecaPeriodo = a.Periodo.Intersects(periodo);
                bool contienePeriodo = a.Periodo.Contains(periodo);
                bool contienePezzo = a.Prezzi.Keys.Contains(p.ID);
                if (contienePeriodo && contienePezzo)
                {
                    aree.Add(a, a.Periodo.GetDates().Count());
                    return aree;
                }

                if (intersecaPeriodo && contienePezzo)
                    aree.Add(a, a.Periodo.Intersection(periodo));

            }
            return aree;
        }

        public bool isAvailable(IPezzo pezzo)
        {
            IPezzo p = _pezzi[pezzo.ID];
            return (p.Occupato || p.OccupatoParzialmente);
            
        }

        public void AggiungiOrdine(IOrdine ordine)
        {
            _ordini.Add(ordine);
             if (!_clienti.Contains(ordine.Cliente))
            {
                _clienti.Add(ordine.Cliente);
            }
            
            this.OnOrdiniChanged();
        }

        public void RimuoviOrdine(IOrdine ordine)
        {
            foreach (string id in ordine.Affitti)
                _affitti.Remove(id);
            _ordini.Remove(ordine);
            this.OnOrdiniChanged();
        }

        public IList<IOrdine> OrdiniPerGiorno(DateTime giorno)
        {
            IList<IOrdine> ordini = new List<IOrdine>();
            foreach (IOrdine o in _ordini)
            {
                if (o.Giorno.Date == giorno.Date)
                    ordini.Add(o);
            }
            return ordini;
        }

        public IList<IAffitto> AffittiInPeriodo(DateRange periodo)
        {
            IList<IAffitto> affitti = new List<IAffitto>();
            foreach (IAffitto a in _affitti.Values)
            {
                if (periodo.Contains(a.Periodo))
                    affitti.Add(a);
            }
            return affitti;
        }
        public IList<IAffitto> AffittiParzialmenteInPeriodo(DateRange periodo)
        {
            IList<IAffitto> affitti = new List<IAffitto>();
            foreach (IAffitto a in _affitti.Values)
            {
                if (periodo.Intersects(a.Periodo))
                    affitti.Add(a);
            }
            return affitti;
        }

        
        IList<IAffitto> ISpiaggia.AffittiCorrenti()
        {
            return AffittiInPeriodo(_periodo);
        }
        
        public IList<IArea> GetAree(IPezzo p)
        {
            return GetAree(p, _periodo).Keys.ToList<IArea>();
        }

        public IList<IPezzo> PezziParzialmenteOccupati(DateRange periodo)
        {
            IList<IPezzo> pezzi = new List<IPezzo>();
            foreach (IAffitto a in AffittiParzialmenteInPeriodo(periodo))
            {
                if (!pezzi.Contains(a.Posto))
                {
                    IPezzo p = a.Posto;
                    p.OccupatoParzialmente = true;
                    pezzi.Add(p);
                }
            }
            return pezzi;
        }

        public IList<IPezzo> PezziOccupati(DateRange periodo)
        {
            IList<IPezzo> pezzi = new List<IPezzo>();
            foreach(IAffitto a in AffittiInPeriodo(periodo))
            {
                if (!pezzi.Contains(a.Posto))
                {
                    IPezzo p = a.Posto;
                    p.Lettini = a.Lettini;
                    p.Sdraio = a.Sdraio;
                    p.Sedie = a.Sedie;
                    p.Occupato = true;
                    pezzi.Add(p);
                }
            }
            return pezzi;
        }

        public IList<IPezzo> PezziLiberi()
        {
            IList<IPezzo> pezzi = new List<IPezzo>();
            foreach(IPezzo p in _pezzi.Values)
            {
                if (!p.Occupato)
                    pezzi.Add(p);
            }
            return pezzi;
        }

        
    }
}
