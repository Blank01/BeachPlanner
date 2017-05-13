using Progetto_ing_sw.Model;
using Progetto_ing_sw.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using Progetto_ing_sw.Properties;

namespace Progetto_ing_sw.Presenter
{
    class LoaderPresenter
    {
        private Button _apri;
        private Button _apriOrdini;
        private Button _crea;
        private TextBox _filepath;
        private Form _form;
        private TextBox _ordiniPath;
        private Button _selezione;

        public LoaderPresenter(Form form, Button apri, Button crea, Button selezione, TextBox spiaggiaTxt, TextBox ordiniTxt, Button apriOrdini)
        {
            this._form = form;
            this._apri = apri;
            this._crea = crea;
            this._selezione = selezione;
            this._filepath = spiaggiaTxt;
            this._ordiniPath = ordiniTxt;
            this._apriOrdini = apriOrdini;
            
            
            _filepath.Text = Settings.Default["spiaggia"].ToString();
            _ordiniPath.Text = Settings.Default["ordini"].ToString();
            _filepath.TextChanged += new EventHandler(AggiornaConfig);
            _ordiniPath.TextChanged += new EventHandler(AggiornaConfig);
            _selezione.Click += new EventHandler(SelezionaFile);
            _crea.Click += new EventHandler(OpenCreaSpiaggia);
            _apri.Click += new EventHandler(OpenGestionale);
            _apriOrdini.Click += new EventHandler(SelezionaFileOrdini);
        }

        private void AggiornaConfig(object sender, EventArgs e)
        {
            Settings.Default["spiaggia"] = _filepath.Text;
            Settings.Default["ordini"] = _ordiniPath.Text;
            Settings.Default.Save();

        }

        private void OpenCreaSpiaggia(object sender, EventArgs e)
        {
            Program.CurrentForm = new CreaSpiaggia();
            this._form.Close();

        }

        private void OpenGestionale(object sender, EventArgs e)
        {
            string path = _filepath.Text;
            if (path != null)
                if (!Spiaggia.GetInstance().CaricaSpiaggia(path))
                    return;
            if (path == null || path == "")
                return;
            string ordiniPath = _ordiniPath.Text;
            if (ordiniPath != null && ordiniPath != "")
                if (!Spiaggia.GetInstance().CaricaOrdini(ordiniPath))
                    return;
            Program.CurrentForm = new GestionaleForm();
            this._form.Close();

        }

        private void SelezionaFile(object sender, EventArgs e)
        {
            using (FileDialog fd = new OpenFileDialog())
            {
                fd.Filter = "XML File|*.xml";
                fd.Title = "Selezione Spiaggia";
                if (DialogResult.OK == fd.ShowDialog())
                {
                    _filepath.Text = fd.FileName;
                }
            }
        }

        private void SelezionaFileOrdini(object sender, EventArgs e)
        {
            using (FileDialog fd = new OpenFileDialog())
            {
                fd.Filter = "XML File|*.xml";
                fd.Title = "Selezione Ordini";
                if (DialogResult.OK == fd.ShowDialog())
                {
                    _ordiniPath.Text = fd.FileName;
                }
            }
        }
    }
}
