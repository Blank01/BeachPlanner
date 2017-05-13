using Progetto_ing_sw.Model;
using Progetto_ing_sw.Model.Pezzi;
using Progetto_ing_sw.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_ing_sw.Presenter
{
    class CreaSpiaggiaAggiungiPresenter
    {
        private Button _addBtn;
        private Form _aggiungiForm;
        private TextBox _heightTxt;
        private TextBox _numTxt;
        private TextBox _offsetTxt;
        private TextBox _offsetYTxt;
        private TextBox _qtTxt;
        private TextBox _righeTxt;
        private ComboBox _tipoComboBox;
        private TextBox _widthTxt;
        private ISpiaggia spiaggia;
        private ISelezione _selezione;

        public Form Form { get { return this._aggiungiForm; }  }

        public CreaSpiaggiaAggiungiPresenter(ISelezione selezione)
        {
            spiaggia = Spiaggia.GetInstance();
            this._aggiungiForm = new AggiungiForm();
            _aggiungiForm.Show();
            _aggiungiForm.FormClosed+=new FormClosedEventHandler(Closed);
            _selezione = selezione;
            initAddForm();
            
        }

        private void Closed(object sender, FormClosedEventArgs e)
        {
            this._aggiungiForm = null;
        }

        public void initAddForm()
        {
            
            try
            {
                _numTxt = _aggiungiForm.Controls.Find("numTxt", true)[0] as TextBox;
                _qtTxt = _aggiungiForm.Controls.Find("qtTxt", true)[0] as TextBox;
                _widthTxt = _aggiungiForm.Controls.Find("widthTxt", true)[0] as TextBox;
                _heightTxt = _aggiungiForm.Controls.Find("heightTxt", true)[0] as TextBox;
                _offsetTxt = _aggiungiForm.Controls.Find("offsetTxt", true)[0] as TextBox;
                _righeTxt = _aggiungiForm.Controls.Find("righeTxt", true)[0] as TextBox;
                _tipoComboBox = _aggiungiForm.Controls.Find("tipoComboBox", true)[0] as ComboBox;
                _addBtn = _aggiungiForm.Controls.Find("addBtn", true)[0] as Button;
                _offsetYTxt = _aggiungiForm.Controls.Find("offsetYTxt", true)[0] as TextBox;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentNullException("Errore aggiungi form ", e.ToString());
            }
            List<String> tipiDisponibili = new List<String> { "Ombrelloni", "Tende" };

            this._tipoComboBox.DataSource = tipiDisponibili;
            this._qtTxt.Text = "1";
            this._numTxt.Text = "1";
            this._widthTxt.Text = "30";
            this._heightTxt.Text = "30";
            this._offsetTxt.Text = "0";
            this._offsetYTxt.Text = "0";
            this._righeTxt.Text = "1";
            this._addBtn.Click += new EventHandler(AddRequest);
        }

        private void AddRequest(object sender, EventArgs e)
        {

            int numeroIniziale = 0;
            int qt = 0;
            int offsetX = 0;
            int offsetY = 0;
            int width = 0;
            int height = 0;
            int righe = 0;
            String tipo;
            try
            {
                numeroIniziale = Int32.Parse(this._numTxt.Text);
            }
            catch
            {
                MessageBox.Show("Errore numero iniziale", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                qt = Int32.Parse(this._qtTxt.Text);
            }
            catch
            {
                MessageBox.Show("Errore quantità", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                width = Int32.Parse(this._widthTxt.Text);
            }
            catch
            {
                MessageBox.Show("Errore larghezza", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                height = Int32.Parse(this._heightTxt.Text);
            }
            catch
            {
                MessageBox.Show("Errore altezza", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                righe = Int32.Parse(this._righeTxt.Text);
            }
            catch
            {
                MessageBox.Show("Errore righe", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                offsetX = Int32.Parse(this._offsetTxt.Text);
            }
            catch
            {
                MessageBox.Show("Errore offset", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                offsetY = Int32.Parse(this._offsetYTxt.Text);
            }
            catch
            {
                MessageBox.Show("Errore offset", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tipo = this._tipoComboBox.Text;
            
            IPezzo pezzo;
            Point origine = new Point(30, 30);
            int offset = 0;
            int n = 0;
            List<IPezzo> pezzi = new List<IPezzo>();
            for (int k = 0; k < righe; k++)
            {
                for (int i = 0; i < qt; i++)
                {
                    Rectangle rect = new Rectangle(origine, new Size(width, height));
                    origine.X = origine.X + width + offset;
                    int numero = (numeroIniziale + n);
                    if (tipo.Equals("Ombrelloni"))
                        pezzo = new Model.Pezzi.Ombrellone(rect, numero);
                    else if (tipo.Equals("Tende"))
                        pezzo = new Model.Pezzi.Tenda(rect, numero);
                    else
                        pezzo = new Model.Pezzi.Ombrellone(rect, numero);
                    pezzi.Add(pezzo);
                    offset = offsetX;
                    n++;
                }

                origine.Y = origine.Y + height + offsetY;
                origine.X = 30;
                offset = 0;
            }
            spiaggia.AggiungiPezzi(pezzi);
            _selezione.Select(pezzi);
            
            this._numTxt.Text = (numeroIniziale + n).ToString();
        }
    }
}
