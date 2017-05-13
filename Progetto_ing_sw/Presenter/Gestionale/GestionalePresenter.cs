using Progetto_ing_sw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_ing_sw.Presenter.Gestionale
{
    class GestionalePresenter
    {
        private Form _form;
        private SpiaggiaPanelPresenter _panelPresenter;
        private ISelezione _selezione;
        private SplitContainer _splitContainer;

        public GestionalePresenter(Form form, Panel panel, DataGridView grid1, DataGridView grid2, SplitContainer splitContainer, GroupBox addBox, StatusStrip strip, MenuStrip menu, ISelezione selezione)
        {
            _form = form;
            form.Text = "Gestionale - " + Spiaggia.GetInstance().FilenameOrdini;
            _selezione = selezione;
            _panelPresenter = new SpiaggiaPanelPresenter(panel, _selezione, false);

            splitContainer.SplitterDistance = _panelPresenter.Size.Width;
            splitContainer.IsSplitterFixed = true;
            splitContainer.SplitterMoved += new SplitterEventHandler(SplitterMoved);
            _splitContainer = splitContainer;
            new NewOrder(addBox, _selezione);
            new GestionaleMenuPresenter(menu);
            new GestionaleGridOrdiniPresenter(grid1, _selezione);
            new GestionaleStatusStrip(strip);
            new GestionaleGridClientiPresenter(grid2);
        }

        private void SplitterMoved(object sender, SplitterEventArgs e)
        {
            _splitContainer.SplitterDistance = _panelPresenter.Size.Width + 30;
        }
    }
}
