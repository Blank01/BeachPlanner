using Progetto_ing_sw.Model;
using Progetto_ing_sw.Model.Eventi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_ing_sw.Presenter
{
    class CreaSpiaggiaGridPresenter
    {
        private readonly CreaSpiaggiaGridPresenter _self;
        private DataTable dt;
        private ISpiaggia spiaggia;
        private DataGridView _grid;
        private ISelezione _selezione;

        public CreaSpiaggiaGridPresenter(DataGridView grid, ISelezione selezione)
        {
            _self = this;
            spiaggia = Spiaggia.GetInstance();
            if (grid == null)
                throw new ArgumentNullException("grid");
            if (selezione == null)
                throw new ArgumentNullException("selezione");
            _grid = grid;
            _selezione = selezione;

            initGrid();
            spiaggia.PezziChanged += new EventHandler(UpdateGrid);
            spiaggia.PezziMoved += new EventHandler(PezziMoved);
            _selezione.Changed += new EventHandler(UpdateGrid);
            _grid.UserDeletingRow += new DataGridViewRowCancelEventHandler(RimuoviRow);
            //_selezione.Changed += new EventHandler(RefreshSelezione);
            _grid.AllowUserToResizeRows = false;
            _grid.AllowUserToAddRows = false;
            _grid.CellValueChanged += new DataGridViewCellEventHandler(CellEdit);
        }

        private void CellEdit(object sender, DataGridViewCellEventArgs e)
        {
            string id = _grid[4, e.RowIndex].Value.ToString();
            IPezzo p = spiaggia.GetPezzo(id);
            if (p == null)
                return;
            string val = _grid[e.ColumnIndex, e.RowIndex].Value.ToString();
            try
            {
                if (e.ColumnIndex == 2)
                {
                    p.X = Int32.Parse(val);
                }else if(e.ColumnIndex == 3)
                {
                    p.Y = Int32.Parse(val);
                }
                spiaggia.OnPezziMoved(_self, new List<IPezzo> { p });
            }
            catch
            {
                MessageBox.Show("Numero non valido", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt.RejectChanges();
            }
            
                
                 
        }

        private bool IsGridSelectUpdated()
        {
            foreach(DataGridViewRow r in _grid.Rows)
            {
                foreach(IPezzo p in _selezione.GetSelezione())
                {
                    if (r.Cells["ID"].Value.ToString() != p.ID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void RimuoviRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            IList<IPezzo> pezzi = new List<IPezzo>();
            foreach (IPezzo p in spiaggia.Pezzi.Values)
            {
                foreach (DataGridViewRow r in _grid.SelectedRows)
                {
                    if (r.Cells["ID"].Value.ToString() == p.ID)
                    {
                        pezzi.Add(p);
                        break;
                    }
                }
            }
            
            spiaggia.RimuoviPezzi(pezzi);
            dt.Clear();
            AddRows(spiaggia.Pezzi.Values);
            e.Cancel = true;
            return;
        }

        private void PezziMoved(object sender, EventArgs e)
        {
            if (sender.GetType() == this._self.GetType())
                return;
            IList<IPezzo> pezzi = ((EventArgsPezziMoved)e).Moved;
            foreach(IPezzo p in pezzi)
            {
                DataGridViewRow row = _grid.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells["ID"].Value.ToString().Equals(p.ID))
                        .First();
                row.Cells["X"].Value = p.X;
                row.Cells["Y"].Value = p.Y;
            }
            
        }

        private IPezzo GetPezzoFromRow(DataGridViewRow r)
        {
            foreach(IPezzo p in spiaggia.Pezzi.Values)
            {
                if (p.ID == r.Cells["ID"].Value.ToString())
                    return p;
            }
            return null;
        }


        public void RefreshSelezione(object sender, EventArgs e)
        {
            
            _grid.ClearSelection();
            foreach (IPezzo p in _selezione.GetSelezione())
            {
                if (p != null)
                {
                    DataGridViewRow row = _grid.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells["ID"].Value.ToString().Equals(p.ID))
                        .First();

                    _grid.Rows[row.Index].Selected = true;
                }
            }
            
        }
        

        private void initGrid()
        {
            dt = new DataTable("Pezzi");
            dt.Columns.Add("Tipo");
            dt.Columns.Add("Numero");
            dt.Columns.Add("X");
            dt.Columns.Add("Y");
            dt.Columns.Add("ID");
            

            _grid.DataSource = dt;
            _grid.ReadOnly = false;
            _grid.Columns[0].ReadOnly = true;
            _grid.Columns[1].ReadOnly = true;
            _grid.Columns[4].ReadOnly = true;

            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            AddRows(_selezione.GetSelezione());
        }

        public void AddRows(IEnumerable<IPezzo> pezzi)
        {
            foreach (IPezzo p in pezzi)
            {
                AddRow(p);
            }
        }

        private void AddRow(IPezzo p)
        {
            dt.Rows.Add( p.Tipo, p.Numero, p.X, p.Y, p.ID);
        }

        private void UpdateGrid(object sender, EventArgs e)
        {
            dt.Clear();
            AddRows(_selezione.GetSelezione());
        }
    }
}
