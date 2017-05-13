using Progetto_ing_sw.Model;
using Progetto_ing_sw.View;
using System.Windows.Forms;
using System;
using Progetto_ing_sw.Utils;
using Progetto_ing_sw.Model.Pezzi;

namespace Progetto_ing_sw.Presenter
{
    internal class CreaSpiaggiaAggiungiAreaPresenter
    {
        private Form _form;
        private ISelezione _selezione;
        private ISpiaggia spiaggia;
        private TextBox _nomeTxt;
        private DateTimePicker _dataInizio;
        private DateTimePicker _dataFine;
        private Button _addBtn;

        public CreaSpiaggiaAggiungiAreaPresenter(ISelezione selezione)
        {
            this._selezione = selezione;
            this._form = new AggiungiAreaForm();
            spiaggia = Spiaggia.GetInstance();
            _form.Show();
            initAddForm();
        }

        private void initAddForm()
        {
            try
            {
                _nomeTxt = _form.Controls.Find("textBox1", true)[0] as TextBox;
                _dataInizio = _form.Controls.Find("dateTimePicker1", true)[0] as DateTimePicker;
                _dataFine = _form.Controls.Find("dateTimePicker2", true)[0] as DateTimePicker;
                _addBtn = _form.Controls.Find("button1", true)[0] as Button;
            }catch(Exception e)
            {
                throw new ArgumentNullException("Errore aggiungi form ", e.ToString());
            }
            _addBtn.Click += new EventHandler(AddArea);
        }

        private void AddArea(object sender, EventArgs e)
        {
            DateRange periodo;
            try
            {
                
                periodo = new DateRange(_dataInizio.Value.Date, _dataFine.Value.Date);
            }catch(DateRangeException e1)
            {
                MessageBox.Show(e1.Message, "Errore",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return;
            }
            if (_nomeTxt.Text.Trim() == "")
            {
                MessageBox.Show("Nome vuoto", "Errore",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            spiaggia.AggiungiArea(new AreaPrezzoPieno(_nomeTxt.Text, periodo));
            
        }

        public Form Form { get { return this._form; } }
    }
}