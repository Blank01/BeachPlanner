using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Model.Pezzi
{
    class Tenda : PezzoFisso
    {
        public Tenda(Rectangle rect, int numero) : base(rect, numero)
        {
            this._tipo = "Tenda";
        }
        public Tenda(Rectangle rect, int numero, string id) : this(rect, numero)
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
