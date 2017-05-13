using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Utils
{
    public class DateRange
    {
        private DateTime _inizio;
        private DateTime _fine;
        public DateRange(DateTime giorno)
        {
            this._inizio = giorno;
            this._fine = giorno;
        }
        public DateRange(DateTime inizio, DateTime fine)
        {
            inizio = inizio.Date;
            fine = fine.Date;
            if (inizio > fine)
            {
                throw new DateRangeException("Data inizio maggiore di data fine");
            }
            this._inizio = inizio;
            this._fine = fine;
        }
        public DateTime Inizio
        {
            get
            {
                return this._inizio;
            }
            set
            {
                this._inizio = value;
            }
        }
        public DateTime Fine
        {
            get
            {
                return this._fine;
            }
            set
            {
                this._inizio = value;
            }
        }

        public IList<DateTime> GetDates()
        {
            IList<DateTime> date = new List<DateTime>();
            DateTime data = this._inizio;
            while (data <= this._fine)
            {
                date.Add(data);
                data = data.AddDays(1);
            }
            return date;
        }

        public bool IsInRange(DateTime data)
        {
            if (data >= this._inizio && data <= this._fine)
                return true;
            else
                return false;
        }
        public int NumeroGiorni()
        {
            return (this._fine - this._inizio).Days + 1;
        }

        public bool Intersects(DateRange periodo)
        {
            if (this.Contains(periodo))
                return false;
            if (this.Fine >= periodo.Inizio
                && this.Fine <= periodo.Fine
                && this.Inizio <= periodo.Inizio)
                return true;
            else if (this.Inizio >= periodo.Inizio
                && this.Inizio <= periodo.Fine
                && this.Fine >= periodo.Fine)
                return true;
            else if (periodo.Inizio >= _inizio && periodo.Fine <= _fine)
                return true;
            else
                return false;
        }

        /*
        public bool Intersects(DateRange periodo)
        {
            if (this.Fine >= periodo.Inizio
                && this.Fine <= periodo.Fine)
                return true;
            else if (this.Inizio >= periodo.Inizio
                && this.Inizio <= periodo.Fine)
                return true;
            else if (periodo.Inizio >= this.Inizio
                && periodo.Inizio <= this.Fine)
                return true;
            else if (periodo.Fine >= this.Inizio
                && periodo.Fine <= this.Fine)
                return true;
            else
                return false;
        }*/
        public bool Contains(DateRange periodo)
        {
           
            return periodo.Inizio <= _inizio && periodo.Fine >= _fine;
        }
        public bool Contains(DateTime giorno)
        {
            return giorno >= _inizio  && giorno <= _fine;
        }

        internal int Intersection(DateRange periodo)
        {
            int count = 0;
            foreach(DateTime giorno in GetDates())
            {
                if (periodo.Contains(giorno))
                    count++;
            }
            return count;
        }
    }
}
