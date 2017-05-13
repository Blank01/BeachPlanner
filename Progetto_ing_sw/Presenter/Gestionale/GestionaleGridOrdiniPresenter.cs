using Progetto_ing_sw.Model;
using Progetto_ing_sw.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_ing_sw.Presenter.Gestionale
{
    class GestionaleGridOrdiniPresenter
    {
        private readonly DataGridView _grid;
        private readonly ISelezione _selezione;
        private ISpiaggia spiaggia;
        private DataTable dtOrdini;

        public GestionaleGridOrdiniPresenter(DataGridView grid, ISelezione selezione)
        {
            _grid = grid;
            _selezione = selezione;
            spiaggia = Spiaggia.GetInstance();
            initGrid();
            spiaggia.OrdiniChanged += new EventHandler(RefreshDtOrdini);
            _grid.UserDeletingRow += new DataGridViewRowCancelEventHandler(DeleteAffitto);
            _grid.CellValueChanged += new DataGridViewCellEventHandler(CellUpdated);
        }

        private void CellUpdated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow r = _grid.Rows[e.RowIndex];
            string affittoID = r.Cells[8].Value as string;
            IAffitto affitto = spiaggia.Affitti[affittoID];
            int index = e.ColumnIndex;
            switch (index)
            {
                case 0:
                    DateTime inizio = DateTime.Parse(r.Cells[0].Value.ToString()).Date;
                    try
                    {
                        affitto.Periodo = new DateRange(inizio, affitto.Periodo.Fine);
                        spiaggia.UpdateOccupati();

                    }
                    catch {
                        r.Cells[index].Value = affitto.Periodo.Inizio.ToShortDateString();

                    }

                    break;
                case 1:
                    DateTime fine = DateTime.Parse(r.Cells[1].Value.ToString()).Date;
                    try
                    {
                        affitto.Periodo = new DateRange(affitto.Periodo.Inizio, fine);
                        spiaggia.UpdateOccupati();
                    }
                    catch
                    {
                        r.Cells[index].Value = affitto.Periodo.Fine.ToShortDateString();

                    }
                    break;

                case 3:
                    try
                    {
                        affitto.Lettini = Int32.Parse(r.Cells[3].Value.ToString());
                        spiaggia.UpdateOccupati();
                    }
                    catch
                    {
                        r.Cells[index].Value = affitto.Lettini;
                    }
                    break;
                case 4:
                    try
                    {
                        affitto.Sdraio = Int32.Parse(r.Cells[4].Value.ToString());
                        spiaggia.UpdateOccupati();

                    }
                    catch
                    {
                        r.Cells[index].Value = affitto.Sdraio;
                    }
                    break;
                case 5:
                    try
                    {
                        affitto.Sedie = Int32.Parse(r.Cells[5].Value.ToString());
                        spiaggia.UpdateOccupati();
                    }
                    catch
                    {
                        r.Cells[index].Value = affitto.Sedie;

                    }
                    break;
                default:
                    _grid.CancelEdit();
                    _grid.RefreshEdit();
                    break;

            }
        }

        private void DeleteAffitto(object sender, DataGridViewRowCancelEventArgs e)
        {
            IOrdine ordine = spiaggia.GetOrdine(e.Row.Cells[7].Value.ToString());
            spiaggia.RimuoviAffitto(e.Row.Cells[8].Value.ToString());
        }

        private void initGrid()
        {
            dtOrdini = new DataTable();
            dtOrdini.Columns.Add("Data Inizio");
            dtOrdini.Columns.Add("Data Fine");
            dtOrdini.Columns.Add("Posto");
            dtOrdini.Columns.Add("Lettini");
            dtOrdini.Columns.Add("Sdraio");
            dtOrdini.Columns.Add("Sedie");
            dtOrdini.Columns.Add("Cliente");
            dtOrdini.Columns.Add("Ordine ID");
            dtOrdini.Columns.Add("Affitto ID");
            _grid.DataSource = dtOrdini;
            _grid.Columns[2].ReadOnly = true;
            _grid.Columns[6].ReadOnly = true;
            _grid.Columns[7].ReadOnly = true;
            _grid.Columns[8].ReadOnly = true;
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //_grid.Columns[_grid.ColumnCount-1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            RefreshDtOrdini();
        }

        private void RefreshDtOrdini(object sender, EventArgs e)
        {
            RefreshDtOrdini();
        }

        private void RefreshDtOrdini()
        {
            IList<IOrdine> ordini = spiaggia.Ordini;
            dtOrdini.Clear();
            foreach (IOrdine o in ordini)
            {
                foreach(string idAffitto in o.Affitti)
                {
                    IAffitto affitto = spiaggia.Affitti[idAffitto];
                    DateTime inizio = affitto.Periodo.Inizio;
                    DateTime fine = affitto.Periodo.Fine;
                    string posto = "-";
                    if (affitto.Posto != null)
                    {
                        IPezzo pezzo = affitto.Posto as IPezzo;
                        posto = pezzo.Nome;
                    }
                    int lettini = affitto.Lettini;
                    int sdraio = affitto.Sdraio;
                    int sedie = affitto.Sedie;
                    ICliente cliente = o.Cliente;
                    DateTime dataCreazione = o.Giorno;
                    dtOrdini.Rows.Add(inizio.ToShortDateString(), fine.ToShortDateString(), posto, lettini, sdraio, sedie, cliente.Nome, o.ID, affitto.ID);
                }
            }
        }
    }
}
