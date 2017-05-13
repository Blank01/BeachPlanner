using System;
using System.Collections.Generic;
using Progetto_ing_sw.Utils;
using Progetto_ing_sw.Exceptions;
using System.Text;

namespace Progetto_ing_sw.Model
{
    public abstract class Area : IArea
    {
        protected string _nome;
        protected IDictionary<string, double> _prezzi;
        protected DateRange _periodo;
        protected string _id;
        private IDictionary<string, double> _pezziMobili;

        public Area(string nome, DateRange periodo)
        {
            this._nome = nome;
            this._periodo = periodo;
            this._prezzi = new Dictionary<string, double>();
            this._id = Util.GenerateShortID();
            this._pezziMobili = new Dictionary<string, double>();
        }
        public Area(string nome, DateRange periodo, string id):this(nome,periodo)
        {
            this._id = id;
        }

        void IArea.AggiungiPezzoMobile(string tipo, double prezzo)
        {
            if (!_pezziMobili.Keys.Contains(tipo))
            {
                _pezziMobili.Add(tipo, prezzo);
            }else
            {
                _pezziMobili[tipo] = prezzo;
            }
                
        }

        void IArea.AggiungiPezzi(IDictionary<string, double> pezziContenuti)
        {
            foreach (KeyValuePair<string, double> p in pezziContenuti)
                this._prezzi.Add(p.Key, p.Value);
        }
        void IArea.RimuoviPezzo(string id)
        {
            _prezzi.Remove(id);
        }
        public String ID { get { return _id; } }
        
        public String Nome
        {
            get
            {
                return this._nome;
            }
        }

        public DateRange Periodo
        {
            get { return this._periodo; }
            set { this._periodo = value; }
        }

        public IDictionary<string, double> PrezziPezziMobili
        {
            get { return this._pezziMobili; }
            set { this._pezziMobili = value; }
        }

        public IDictionary<String, double> Prezzi
        {
            get { return this._prezzi; }
            set { this._prezzi = value; }
        }
        

        public abstract double CalcolaPrezzo(String id, DateRange periodo);

        public double CalcolaPrezzo(IList<IPezzo> pezzi, DateRange periodo)
        {
            double tot = 0;

            foreach (IPezzo p in pezzi)
                tot+=CalcolaPrezzo(p.ID, periodo);

            return tot;

        }

        public void RimuoviPezzoMobile(string tipo)
        {
            _pezziMobili.Remove(tipo);
        }
    }
}