using Progetto_ing_sw.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Model
{
    class Selezione : ISelezione
    {
        public List<IPezzo> _selezionati;
        public Selezione()
        {
            _selezionati = new List<IPezzo>();
        }
        public IList<IPezzo> GetSelezione()
        { 
            return _selezionati;
         
        }

        void ISelezione.OnChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        void ISelezione.Reset()
        {
            this._selezionati = new List<IPezzo>();
            Changed(this, EventArgs.Empty);
        }

        public void Select(IList<IPezzo> pezziSelezionati)
        {
            
            if (!Commands.IsControlDown())
            {
                _selezionati = new List<IPezzo>();
            }
            foreach(IPezzo p in pezziSelezionati){
                if (!_selezionati.Contains(p))
                {
                    _selezionati.Add(p);
                }
            }

            Changed(this, EventArgs.Empty);
        }

        public void RemoveSelected()
        {
            Spiaggia.GetInstance().RimuoviPezzi(_selezionati);
            _selezionati = new List<IPezzo>();
            Changed(this, EventArgs.Empty);

        }

        public void Select(IPezzo pezzo)
        {
            if (pezzo == null)
                return;
            if (!_selezionati.Contains(pezzo)) {
                if (!Commands.IsControlDown())
                {
                    _selezionati = new List<IPezzo>();
                }

                _selezionati.Add(pezzo);
                
            }else
            {
                if(Commands.IsControlDown())
                    _selezionati.Remove(pezzo);
            }
            
            Changed(this, EventArgs.Empty);
        }

        public event EventHandler Changed;
    }
}
