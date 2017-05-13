using Progetto_ing_sw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_ing_sw.Presenter.Gestionale
{
    public class GestionaleStatusStrip
    {
        private ToolStripItem _lastSaveTxt;
        private StatusStrip _strip;
        private ISpiaggia spiaggia;

        public GestionaleStatusStrip(StatusStrip strip)
        {
            _strip = strip;
            _lastSaveTxt = _strip.Items["toolStripStatusLabel2"];
            _lastSaveTxt.Text = "Mai";
            spiaggia = Spiaggia.GetInstance();
            spiaggia.OrdiniSalvati += new EventHandler(AggiornaLastSaveTxt);
        }

        private void AggiornaLastSaveTxt(object sender, EventArgs e)
        {
            _lastSaveTxt.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
