using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Model
{
    public interface ISelezione
    {
        IList<IPezzo> GetSelezione();
        event EventHandler Changed;
        void OnChanged();
        void Reset();
        void Select(IPezzo pezzo);
        void Select(IList<IPezzo> pezziSelezionati);
        void RemoveSelected();
    }
}
