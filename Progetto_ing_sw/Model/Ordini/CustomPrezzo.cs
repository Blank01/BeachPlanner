using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Model.Ordini
{
    public class CustomPrezzo : IPrezzo
    {
        private string _nome;
        private double _prezzo;

        public CustomPrezzo(string nome, double prezzo)
        {
            _nome = nome;
            _prezzo = prezzo;
        }
        public string Nome
        {
            get
            {
                return _nome;
            }

            set
            {
                Nome = value;
            }
        }

        public int ScontoPercentuale
        {
            get
            {
                return 0;
            }

            set
            {
                
            }
        }

        public bool ScontoProgressivo
        {
            get
            {
                return false;
            }

            set
            {
                
            }
        }

        public double Prezzo { get { return _prezzo; } set { _prezzo = value; } }

        public double CalcolaPrezzo(IAffitto affitto)
        {
            return _prezzo;
        }
    }
}
