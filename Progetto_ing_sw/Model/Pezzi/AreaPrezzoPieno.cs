using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Progetto_ing_sw.Utils;

namespace Progetto_ing_sw.Model.Pezzi
{
    class AreaPrezzoPieno : Area
    {

        public AreaPrezzoPieno(string nome, DateRange periodo) : base(nome, periodo)
        {
        }

        public AreaPrezzoPieno(string nome, DateRange periodo, string id) : base(nome, periodo, id)
        {
        }

        public override double CalcolaPrezzo(String id, DateRange periodo)
        {
            double tot = 0;
            int giorni = periodo.GetDates().Count;
            tot += (_prezzi[id] * giorni);

            return tot;
        }
    }
}
