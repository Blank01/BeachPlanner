using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Model.Eventi
{
    class EventArgsPezziMoved : EventArgs
    {
        private IList<IPezzo> _moved;
        public EventArgsPezziMoved(IList<IPezzo> pezzi)
        {
            _moved = pezzi;
        }
        

        public IList<IPezzo> Moved
        {
            get { return this._moved; }
        }
    }
}
