using Progetto_ing_sw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_ing_sw.Presenter.Gestionale
{
    public class GestionaleGridClientiPresenter
    {
        private ISpiaggia spiaggia;
        private readonly DataGridView _grid;

        public GestionaleGridClientiPresenter(DataGridView grid)
        {
            _grid = grid;
            spiaggia = Spiaggia.GetInstance();
            initGrid();
        }

        private void initGrid()
        {
            _grid.DataSource = spiaggia.Clienti;
            
        }
    }
}
