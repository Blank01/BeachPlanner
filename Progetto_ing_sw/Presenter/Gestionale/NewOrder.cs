using Progetto_ing_sw.Exceptions;
using Progetto_ing_sw.Model;
using Progetto_ing_sw.Model.Ordini;
using Progetto_ing_sw.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_ing_sw.Presenter.Gestionale
{
    public class NewOrder
    {
        private readonly Control _contenitore;
        private readonly DateTimePicker _dataFine;
        private readonly DateTimePicker _dataInizio;
        private readonly TextBox _hotelTxt;
        private readonly NumericUpDown _lettiniNum;
        private readonly ListBox _listBox;
        private readonly TextBox _mailTxt;
        private readonly TextBox _nomeTxt;
        private readonly NumericUpDown _sdraioNum;
        private readonly NumericUpDown _sedieNum;
        private readonly ISpiaggia spiaggia;
        private IList<IPezzo> postiLiberi;
        private readonly Button _addBtn;
        private readonly ISelezione _selezione;
        private readonly CheckBox _postoCheckBox;
        private readonly ComboBox _scontoComboBox;
        private readonly TextBox _prezzoTxt;
        private TextBox _areaTxt;

        public NewOrder(Control contenitore, ISelezione selezione)
        {
            _contenitore = contenitore;
            _selezione = selezione;
            spiaggia = Spiaggia.GetInstance();
            postiLiberi = new List<IPezzo>();
            try
            {
                _dataInizio = _contenitore.Controls.Find("dateTimePicker1", true)[0] as DateTimePicker;
                _dataFine = _contenitore.Controls.Find("dateTimePicker2", true)[0] as DateTimePicker;
                _listBox = _contenitore.Controls.Find("listBox1", true)[0] as ListBox;
                _nomeTxt = _contenitore.Controls.Find("textBox1", true)[0] as TextBox;
                _hotelTxt = _contenitore.Controls.Find("textBox2", true)[0] as TextBox;
                _mailTxt = _contenitore.Controls.Find("textBox3", true)[0] as TextBox;
                _lettiniNum = _contenitore.Controls.Find("numericUpDown1", true)[0] as NumericUpDown;
                _sdraioNum = _contenitore.Controls.Find("numericUpDown2", true)[0] as NumericUpDown;
                _sedieNum = _contenitore.Controls.Find("numericUpDown3", true)[0] as NumericUpDown;
                _addBtn = _contenitore.Controls.Find("button1", true)[0] as Button;
                _postoCheckBox = _contenitore.Controls.Find("checkBox1", true)[0] as CheckBox;
                _scontoComboBox = _contenitore.Controls.Find("comboBox1", true)[0] as ComboBox;
                _prezzoTxt = _contenitore.Controls.Find("textBox4", true)[0] as TextBox;
                _areaTxt = _contenitore.Controls.Find("textBox5", true)[0] as TextBox;

            }
            catch (ArgumentException e) { throw new ArgumentException("Error creating form \n Control " + e.ParamName + " not found."); }

            _areaTxt.Text = "N/A";

            _listBox.DataSource = postiLiberi;
            _listBox.DisplayMember = "Nome";
            _listBox.ValueMember = "ID";
            _listBox.SelectedValueChanged += new EventHandler(UpdateSelection);
            _selezione.Changed += new EventHandler(UpdateSelectionListBox);
            UpdateListBox();

            _dataInizio.ValueChanged += new EventHandler(DataInizioChanged);
            _dataFine.ValueChanged += new EventHandler(DataFineChanged);
            spiaggia.PeriodoChanged += new EventHandler(UpdateListBox);
            spiaggia.PezziChanged += new EventHandler(UpdateListBox);
            _addBtn.Click += new EventHandler(AddAffitto);
            spiaggia.Periodo = new DateRange(_dataInizio.Value, _dataFine.Value);
            spiaggia.UpdateOccupati();
            _postoCheckBox.CheckedChanged += new EventHandler(CheckBoxChanged);
            _prezzoTxt.TextChanged += new EventHandler(prezzoChanged);
            


            _dataInizio.ValueChanged += new EventHandler(RicalcolaPrezzo);
            _dataFine.ValueChanged += new EventHandler(RicalcolaPrezzo);
            _listBox.SelectedIndexChanged += new EventHandler(RicalcolaPrezzo);
            _lettiniNum.ValueChanged += new EventHandler(RicalcolaPrezzo);
            _sedieNum.ValueChanged += new EventHandler(RicalcolaPrezzo);
            _sdraioNum.ValueChanged += new EventHandler(RicalcolaPrezzo);
            _scontoComboBox.SelectedValueChanged += new EventHandler(RicalcolaPrezzo);
            _scontoComboBox.DataSource= spiaggia.Sconti;
            _scontoComboBox.DisplayMember = "Nome";
            _prezzoTxt.Text = "0";
        }

        private void prezzoChanged(object sender, EventArgs e)
        {
            if((_scontoComboBox.SelectedItem is CustomPrezzo))
            {
                _scontoComboBox.SelectedIndex = spiaggia.Sconti.Count - 1;
                IPrezzo p = _scontoComboBox.SelectedItem as IPrezzo;
                try
                {
                    p.Prezzo = Double.Parse(_prezzoTxt.Text);
                }
                catch
                {
                    MessageBox.Show("Prezzo non valido", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }

        }

        private void RicalcolaPrezzo(object sender, EventArgs e)
        {
            PezzoFisso p = null;

            if (_postoCheckBox.Checked)
            {
                p = _listBox.SelectedItem as PezzoFisso;
                string nomi = "";
                IList<IArea> aree = spiaggia.GetAree(p);
                foreach (IArea a in aree)
                {
                    nomi += a.Nome + " ";
                }
                if (nomi == "")
                    _areaTxt.Text = "N/A";
                else
                    _areaTxt.Text = nomi;
            }

            IPrezzo calcolatore = _scontoComboBox.SelectedItem as IPrezzo;
            if (calcolatore is CustomPrezzo)
                _prezzoTxt.Enabled = true;
            else
                _prezzoTxt.Enabled = false;
            DateRange periodo = new DateRange(_dataInizio.Value, _dataFine.Value);
            int lettini = (int)_lettiniNum.Value;
            int sdraio = (int)_sdraioNum.Value;
            int sedie = (int)_sedieNum.Value;
            IAffitto affitto;
            if (lettini + sdraio + sedie == 0)
            {
                _prezzoTxt.Text = "0";
                return;
            }

            if (_postoCheckBox.Checked)
            {
                affitto = new AffittoImpl(p, lettini, sdraio, sedie, periodo);
            } else
                affitto = new AffittoImpl(lettini, sdraio, sedie, periodo);
            
            _prezzoTxt.Text = calcolatore.CalcolaPrezzo(affitto).ToString();
            
                
        }

        private void CheckBoxChanged(object sender, EventArgs e)
        {
            if (_postoCheckBox.Checked)
                _listBox.Enabled = true;
            else
                _listBox.Enabled = false;
        }

        private void UpdateSelectionListBox(object sender, EventArgs e)
        {
            
            if (!_selezione.GetSelezione().Any())
            {
                _postoCheckBox.Checked = false;
                _listBox.Enabled = false;
                return;
            }
                
            int count = _listBox.Items.Count;
            foreach (IPezzo p in _selezione.GetSelezione())
            {
                if (!p.Occupato && !p.OccupatoParzialmente)
                {
                    for (int i = 0; i < count; i++)
                    {
                        IPezzo p1 = _listBox.Items[i] as IPezzo;
                        if (p1.ID == p.ID)
                        {
                            _listBox.SelectedIndex = i;
                            _postoCheckBox.Checked = true;
                            _listBox.Enabled = true;
                            return;
                        }

                    }
                }
                
            }
            _listBox.SelectedIndex = -1;
            _postoCheckBox.Checked = false;
            _listBox.Enabled = false;
        }

        private void UpdateSelection(object sender, EventArgs e)
        {
            IPezzo p = _listBox.SelectedItem as IPezzo;
            if (!_selezione.GetSelezione().Contains(p))
            {
                _selezione.Select(p);
            }
        }

        private void AddAffitto(object sender, EventArgs e)
        {
            ICliente cliente;
            string nome = _nomeTxt.Text;
            string hotel = _hotelTxt.Text;
            string mail = _mailTxt.Text;
            cliente = new ClientImpl(nome, hotel, mail);//TODO controlla se esiste gia'
            PezzoFisso pezzo = _listBox.SelectedItem as PezzoFisso;//TODO piu' item selezionati
            int lettini = Int32.Parse(_lettiniNum.Value.ToString());
            int sdraio = Int32.Parse(_sdraioNum.Value.ToString());
            int sedie = Int32.Parse(_sedieNum.Value.ToString());
            DateRange periodo = new DateRange(_dataInizio.Value.Date, _dataFine.Value.Date);
            IAffitto affitto;
            try
            {
                if(_postoCheckBox.Checked)
                    affitto = new AffittoImpl(pezzo, lettini, sdraio, sedie, periodo);
                else
                    affitto = new AffittoImpl(lettini, sdraio, sedie, periodo);
            }
            catch (AffittoException e1)
            {
                MessageBox.Show(e1.Message, "Errore",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            IOrdine ordine = new OrdineImpl(cliente, new List<string> { affitto.ID });
            spiaggia.AggiungiAffitto(affitto);
            spiaggia.AggiungiOrdine(ordine);
            spiaggia.UpdateOccupati();
            spiaggia.SalvaConNomeOrdini(spiaggia.FilenameOrdini);

        }

        private void UpdateListBox()
        {
            postiLiberi = new List<IPezzo>();
            postiLiberi = spiaggia.PezziLiberi();

            _listBox.DataSource = postiLiberi;
        }

        private void UpdateListBox(object sender, EventArgs e)
        {
            UpdateListBox();
        }

        private void DataInizioChanged(object sender, EventArgs e)
        {

            DateTime inizio = _dataInizio.Value.Date;
            DateTime fine = _dataFine.Value.Date;
            this._dataFine.Value = inizio;
            
            spiaggia.Periodo = new DateRange(_dataInizio.Value.Date, _dataFine.Value.Date);
            spiaggia.OnPeriodoChanged();
        }
        private void DataFineChanged(object sender, EventArgs e)
        {

            DateTime inizio = _dataInizio.Value.Date;
            DateTime fine = _dataFine.Value.Date;
            if (fine < inizio)
            {
                this._dataInizio.Value = fine;
            }


            spiaggia.Periodo = new DateRange(_dataInizio.Value.Date, _dataFine.Value.Date);
            spiaggia.OnPeriodoChanged();
        }
    }
}
