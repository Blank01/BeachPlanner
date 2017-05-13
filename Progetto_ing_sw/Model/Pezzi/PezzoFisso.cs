using Progetto_ing_sw.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Model
{
    public abstract class PezzoFisso: IPezzo
    {   
        protected String _tipo;
        protected Rectangle _rect; 
        protected int _numero;
        protected String _id;
        protected IList<IAffitto> _affitti;
        protected bool _occupato;
        private int _sdraio;
        private int _sedie;
        private int _lettini;
        private bool _occupatoParzialmente;

        protected PezzoFisso(Rectangle rect, int numero)
        {
            this._rect = rect;
            this._occupato = false;
            this._numero = numero;
            this._id = Util.GenerateID();
            this._affitti = new List<IAffitto>();
        }

        public abstract string Tipo{ get; }
        
        public bool Occupato
        {
            get { return this._occupato; }
            set { this._occupato = value; }
        }

        public int Numero
        {
            get
            {
                return this._numero;
            }
        }

        public string ID
        {
            get
            {
                return this._id;
            }
        }

        public IList<IAffitto> Affitti
        {
            get { return this._affitti; }
            set { this._affitti = value; }
        }


        public Rectangle Rect
        {
            get
            {
                return this._rect;
            }
            set
            {
                this._rect = value;
            }
        }

        public Point Location
        {
            get
            {
                return this._rect.Location;
            }

            set
            {
                this._rect.Location = value;
            }
        }

        public int X
        {
            get
            {
                return this._rect.X;
            }

            set
            {
                this._rect.X = value;
            }
        }

        public int Y
        {
            get
            {
                return this._rect.Y;
            }

            set
            {
                this._rect.Y = value;
            }
        }

        public int Width
        {
            get
            {
                return this._rect.Width;
            }

            set
            {
                this._rect.Width = value;
            }
        }

        public int Height
        {
            get
            {
                return this._rect.Height;
            }

            set
            {
                this._rect.Height = value;
            }
        }

        public int Lettini
        {
            get
            {
                return _lettini;
            }

            set
            {
                _lettini = value;
            }
        }

        public int Sedie
        {
            get
            {
                return _sedie;
            }

            set
            {
                _sedie = value;
            }
        }

        public int Sdraio
        {
            get
            {
                return _sdraio;
            }

            set
            {
                _sdraio = value;
            }
        }

        String IPezzo.Nome
        {
            get { return Tipo + " " + Numero; }
        }

        public bool OccupatoParzialmente
        {
            get
            {
                return _occupatoParzialmente;
            }

            set
            {
                _occupatoParzialmente = value;
            }
        }

        public double CalcolaPrezzo(DateRange periodo)
        {
            throw new NotImplementedException();
        }
    }
}
