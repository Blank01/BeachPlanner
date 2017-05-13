using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Model.Pezzi
{
    class Ombrellone : PezzoFisso
    {
        public Ombrellone(Rectangle rect, int numero) : base(rect, numero)
        {
            this._tipo = "Ombrellone";
        }
        public Ombrellone(Rectangle rect, int numero, string id):this(rect,numero)
        {
            this._id = id;
        }

        public override string Tipo
        {
            get
            {
                return this._tipo;
            }
        }
    }
}
