using Progetto_ing_sw.Model;
using Progetto_ing_sw.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace Progetto_ing_sw.Presenter
{
    class CreaSpiaggiaMenuPresenter
    {
        private ToolStripMenuItem _addItem;
        private MenuStrip _menu;
        private ToolStripMenuItem _new;
        private ToolStripMenuItem _open;
        private ToolStripMenuItem _save;
        private ISelezione _selezione;
        private ToolStripMenuItem _edit;
        private ToolStripMenuItem _file;
        private Form _aggiungiForm;
        private ToolStripMenuItem _save2;
        private ToolStripMenuItem _addArea;
        private Form _aggiungiAreaForm;
        private ToolStripMenuItem _selezioneAreaBtn;
        private Form _selezioneAreaForm;

        public CreaSpiaggiaMenuPresenter(MenuStrip menu, ISelezione selezione)
        {
            if (selezione == null)
                throw new ArgumentNullException("selezione");
            _selezione = selezione;
            if (menu == null)
                throw new ArgumentNullException("menu");
            _menu = menu;
            
            _file = _menu.Items["fileToolStripMenuItem"] as ToolStripMenuItem;
            _edit = _menu.Items["editToolStripMenuItem"] as ToolStripMenuItem;
            try
            {
                _save = _file.DropDownItems.Find("saveToolStripMenuItem", false)[0] as ToolStripMenuItem;
                _save2 = _file.DropDownItems.Find("save2ToolStripMenuItem", false)[0] as ToolStripMenuItem;
                _new = _file.DropDownItems.Find("newToolStripMenuItem", false)[0] as ToolStripMenuItem;
                _open = _file.DropDownItems.Find("openToolStripMenuItem", false)[0] as ToolStripMenuItem;
                _addItem = _edit.DropDownItems.Find("addToolStripMenuItem", false)[0] as ToolStripMenuItem;
                _addArea = _edit.DropDownItems.Find("aggiungiAreaToolStripMenuItem1", false)[0] as ToolStripMenuItem;
                _selezioneAreaBtn = _edit.DropDownItems.Find("areaToolStripMenuItem", false)[0] as ToolStripMenuItem;
            }
            catch
            {
                throw new ArgumentException("menu errato");
            }
            _addArea.Click += new EventHandler(OpenAggiungiAreaForm);
            _addItem.Click += new EventHandler(OpenAggiungiPezzoForm);
            _save.Click += new EventHandler(SalvaSpiaggia);
            _save2.Click += new EventHandler(SalvaConNomeSpiaggia);
            _open.Click += new EventHandler(OpenSpiaggia);
            _selezioneAreaBtn.Click += new EventHandler(OpenSelezioneAreaForm);

        }

        

        private void OpenSpiaggia(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML File|*.xml";
            openFileDialog1.Title = "Selezione spiaggia";

            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Spiaggia.GetInstance().CaricaSpiaggia(openFileDialog1.FileName);
            }

        }
        private void SalvaConNomeSpiaggia(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML file|*.xml";
            saveFileDialog1.Title = "Salva spiaggia";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                Spiaggia.GetInstance().SalvaConNomeSpiaggia(saveFileDialog1.FileName);
                
            }
        
        }

        
        private void SalvaSpiaggia(object sender, EventArgs e)
        {
            Spiaggia.GetInstance().SalvaSpiaggia();
        }

        private void OpenSelezioneAreaForm(object sender, EventArgs e)
        {
            if (_selezioneAreaForm == null)
            {
                _selezioneAreaForm = new CreaSpiaggiaAreaPresenter().Form;
                _selezioneAreaForm.FormClosed += new FormClosedEventHandler(ClosedSelezioneAreaForm);
            }
            else
            {
                _selezioneAreaForm.Focus();
            }
        }

        private void OpenAggiungiPezzoForm(object sender, EventArgs e)
        {
            if (_aggiungiForm == null)
            {
                _aggiungiForm = new CreaSpiaggiaAggiungiPresenter(_selezione).Form;
                _aggiungiForm.FormClosed+=new FormClosedEventHandler(ClosedAggiungiForm);
            }else
            {
                _aggiungiForm.Focus();
            }
        }

        private void OpenAggiungiAreaForm(object sender, EventArgs e)
        {
            if (_aggiungiAreaForm == null)
            {
                _aggiungiAreaForm = new CreaSpiaggiaAggiungiAreaPresenter(_selezione).Form;
                _aggiungiAreaForm.FormClosed += new FormClosedEventHandler(ClosedAggiungiAreaForm);
            }
            else
            {
                _aggiungiAreaForm.Focus();
            }
        }

        private void ClosedSelezioneAreaForm(object sender, FormClosedEventArgs e)
        {
            _selezioneAreaForm = null;
        }

        private void ClosedAggiungiAreaForm(object sender, FormClosedEventArgs e)
        {
            _aggiungiAreaForm = null;
        }

        private void ClosedAggiungiForm(object sender, FormClosedEventArgs e)
        {
            _aggiungiForm = null;
        }
    }
}
