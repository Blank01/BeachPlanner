using Progetto_ing_sw.Model;
using Progetto_ing_sw.Utils;
using Progetto_ing_sw.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_ing_sw.Presenter
{
    partial class CreaSpiaggiaPresenter
    {
        private ISelezione _selezione;
        private SpiaggiaPanelPresenter _panelPresenter;
        private Form _form;
        public CreaSpiaggiaPresenter(Form form, Panel panel,  MenuStrip menu, DataGridView dataGridView1, TreeView tree)
        {
            _selezione = new Selezione();
            _form = form;
            _panelPresenter = new SpiaggiaPanelPresenter(panel, _selezione, true);
            new CreaSpiaggiaGridPresenter(dataGridView1, _selezione);
            new CreaSpiaggiaMenuPresenter(menu, _selezione);
            new CreaSpiaggiaTreeViewPresenter(tree, _selezione);
            Spiaggia.GetInstance().SpiaggiaLoaded += new EventHandler(CambiaTitolo);
        }

        private void CambiaTitolo(object sender, EventArgs e)
        {
            _form.Text = "Crea Spiaggia - " + Spiaggia.GetInstance().Filename;
        }
    }
}
