using System.Windows.Forms;
using Progetto_ing_sw.Model;
using Progetto_ing_sw.View;
using System.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using Progetto_ing_sw.Utils;

namespace Progetto_ing_sw.Presenter
{
    internal class CreaSpiaggiaAreaPresenter
    {
        private Form _form;
        private DataGridView _gridPezzi;
        private DataGridView _gridArea;
        private ComboBox _areaComboBox;
        private Button _addBtn;
        private DataTable dtPezzi;
        private DataTable dtAree;
        private TextBox _prezzoTxt;
        private IList<IArea> _aree;
        private Button _addMobileBtn;
        private ComboBox _tipoMobileComboBox;
        private TextBox _prezzoMobileTxt;
        private DataTable dtMobili;
        private DataGridView _gridMobili;
        private Button _saveArea;
        private DateTimePicker _dataFine;
        private DateTimePicker _dataInizio;

        public CreaSpiaggiaAreaPresenter()
        {
            this._form = new AreaForm();
            try
            {
                _gridPezzi = _form.Controls.Find("dataGridView1", true)[0] as DataGridView;
                _gridArea = _form.Controls.Find("dataGridView2", true)[0] as DataGridView;
                _areaComboBox = _form.Controls.Find("comboBox1", true)[0] as ComboBox;
                _addBtn = _form.Controls.Find("button1", true)[0] as Button;
                _prezzoTxt = _form.Controls.Find("textBox1", true)[0] as TextBox;
                _prezzoMobileTxt = _form.Controls.Find("textBox2", true)[0] as TextBox;
                _tipoMobileComboBox = _form.Controls.Find("comboBox2", true)[0] as ComboBox;
                _addMobileBtn = _form.Controls.Find("button2", true)[0] as Button;
                _gridMobili = _form.Controls.Find("dataGridView3",true)[0] as DataGridView;
                _saveArea = _form.Controls.Find("button3", true)[0] as Button;
                _dataInizio = _form.Controls.Find("dateTimePicker1", true)[0] as DateTimePicker;
                _dataFine = _form.Controls.Find("dateTimePicker2", true)[0] as DateTimePicker;
            }catch(ArgumentException e) { throw new ArgumentException("Error creating form \n Control " + e.ParamName + " not found."); }

            _gridArea.UserDeletingRow += new DataGridViewRowCancelEventHandler(RowDeleted);
            _gridMobili.UserDeletingRow += new DataGridViewRowCancelEventHandler(RowMobiliDeleted);
            _areaComboBox.SelectedIndexChanged += new EventHandler(SelectedAreaChanged);
            _addBtn.Click += new EventHandler(AddPezzi);
            _addMobileBtn.Click += new EventHandler(AddPrezzoMobile);
            _saveArea.Click += new EventHandler(SaveArea);

            initControls();
            _form.Show();

            RefreshDtArea();
            RefreshDtPezzi();
            RefreshDtPezziMobili();

        }

        private void SaveArea(object sender, EventArgs e)
        {
            IArea area = _areaComboBox.SelectedItem as IArea;
            if (area == null)
                return;
            area.Periodo = new DateRange(_dataInizio.Value.Date, _dataFine.Value.Date);
            Spiaggia.GetInstance().Aree = _aree;
            Spiaggia.GetInstance().OnAreaChanged();
        }

        private void AddPrezzoMobile(object sender, EventArgs e)
        {
            double prezzo = 0;
            try
            {
                prezzo = Double.Parse(_prezzoMobileTxt.Text);
            }
            catch
            {
                MessageBox.Show("Prezzo invalido", "Errore",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string tipoMobile = _tipoMobileComboBox.SelectedItem.ToString() ;
            IArea area = _areaComboBox.SelectedItem as IArea;
            area.AggiungiPezzoMobile(tipoMobile, prezzo);
            RefreshDtPezziMobili();
            /*
            Spiaggia.GetInstance().Aree = _aree;
            Spiaggia.GetInstance().OnAreaChanged();
            _form.Focus();
            */
        }

        private void SelectedAreaChanged(object sender, EventArgs e)
        {
            _aree = Spiaggia.GetInstance().Aree;
            IArea area = _areaComboBox.SelectedItem as IArea;
            _dataInizio.Value = area.Periodo.Inizio;
            _dataFine.Value = area.Periodo.Fine;
            RefreshDtPezzi();
            RefreshDtPezziMobili();
        }

        private void RowMobiliDeleted(object sender, DataGridViewRowCancelEventArgs e)
        {
            IArea a = _areaComboBox.SelectedItem as IArea;
            string tipo = e.Row.Cells["tipo"].Value.ToString();
            a.RimuoviPezzoMobile(tipo);
            

        }

        private void RowDeleted(object sender, DataGridViewRowCancelEventArgs e)
        {
            IArea a = _areaComboBox.SelectedItem as IArea;
            string id = e.Row.Cells["ID"].Value.ToString();
            a.RimuoviPezzo(id);
            
            
        }

        private void AddPezzi(object sender, EventArgs e)
        {
            IList<IPezzo> pezziSelezionati = new List<IPezzo>();
            foreach(DataGridViewRow r in _gridPezzi.SelectedRows)
            {
                IPezzo p = Spiaggia.GetInstance().GetPezzo(r.Cells["ID"].Value.ToString());
                pezziSelezionati.Add(p);
            }
            IArea currentArea = _areaComboBox.SelectedItem as IArea;
            if (currentArea == null)
                return;
            IDictionary<String, double> pezziArea = currentArea.Prezzi;
            IDictionary<String, double> addPezzi = new Dictionary<String,double>();
            double prezzo = 0;
            try
            {
                prezzo = Double.Parse(_prezzoTxt.Text);
            }
            catch
            {
                MessageBox.Show("Prezzo invalido", "Errore",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (IPezzo p in pezziSelezionati)
            {
                if (!pezziArea.Keys.Contains(p.ID))
                    addPezzi.Add(p.ID, prezzo);
            }
            

            currentArea.AggiungiPezzi(addPezzi);
            RefreshDtArea();
            /*
            Spiaggia.GetInstance().Aree = _aree;
            Spiaggia.GetInstance().OnAreaChanged();
            _form.Focus();
            */
        }

        private void RefreshDtArea()
        {
            IArea area = _areaComboBox.SelectedItem as IArea;
            if (area == null)
                return;
            dtAree.Clear();
            foreach(String id in area.Prezzi.Keys)
            {
                IPezzo p = Spiaggia.GetInstance().GetPezzo(id);
                dtAree.Rows.Add(p.Tipo, p.Numero, area.Prezzi[id], p.ID);
            }
        }

        private void initControls()
        {
            

            dtMobili = new DataTable();
            dtAree = new DataTable();
            dtPezzi = new DataTable();

            dtAree.Columns.Add("Tipo");
            dtAree.Columns.Add("Numero");
            dtAree.Columns.Add("Prezzo");
            dtAree.Columns.Add("ID");

            dtPezzi.Columns.Add("Tipo");
            dtPezzi.Columns.Add("Numero");
            dtPezzi.Columns.Add("ID");
            
            dtMobili.Columns.Add("Tipo");
            dtMobili.Columns.Add("Prezzo giornaliero");


            _gridPezzi.DataSource = dtPezzi;
            _gridArea.DataSource = dtAree;
            _gridMobili.DataSource = dtMobili;
            _tipoMobileComboBox.DataSource = Spiaggia.GetInstance().GetPezziMobili();

            _gridPezzi.ReadOnly = true;
            _gridArea.ReadOnly = true;
            _gridMobili.ReadOnly = true;

            _aree = Spiaggia.GetInstance().Aree;
            _areaComboBox.DataSource = _aree;
            _areaComboBox.DisplayMember = "Nome";
            _areaComboBox.ValueMember = "ID";

        }

        private void RefreshDtPezziMobili()
        {
            IArea area = _areaComboBox.SelectedItem as IArea;

            dtMobili.Clear();
            try
            {
                foreach (string tipo in area.PrezziPezziMobili.Keys)
                {
                    dtMobili.Rows.Add(tipo, area.PrezziPezziMobili[tipo]);
                }
            }catch(Exception e){ Console.WriteLine(e.Message + "\n" + e.StackTrace); }
            
        }

        private void RefreshDtPezzi()
        {
            var pezzi = Spiaggia.GetInstance().Pezzi.Values;

            IList<IPezzo> sortedPezzi = pezzi
                .OrderBy(p => p.Numero)
                .GroupBy(p => p.Tipo)
                .SelectMany(grp => grp)
                .ToList();

            dtPezzi.Clear();

            foreach (IPezzo p in sortedPezzi)
            {
                dtPezzi.Rows.Add(p.Tipo, p.Numero, p.ID);
            }
        }

        public Form Form { get { return this._form; } }
    }
}