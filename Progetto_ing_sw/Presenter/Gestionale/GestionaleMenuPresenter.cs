using Progetto_ing_sw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_ing_sw.Presenter.Gestionale
{
    public class GestionaleMenuPresenter
    {
        private ToolStripMenuItem _file;
        private MenuStrip _menu;
        private ToolStripMenuItem _salvaConNomeBtn;

        public GestionaleMenuPresenter(MenuStrip menu)
        {
            _menu = menu;
            _file = _menu.Items["fileToolStripMenuItem"] as ToolStripMenuItem;
            _salvaConNomeBtn = _file.DropDownItems.Find("salvaConNomeToolStripMenuItem", true)[0] as ToolStripMenuItem;

            _salvaConNomeBtn.Click += new EventHandler(SalvaConNome);

        }

        private void SalvaConNome(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML file|*.xml";
            saveFileDialog1.Title = "Salva Ordini";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                Spiaggia.GetInstance().SalvaConNomeOrdini(saveFileDialog1.FileName);
            }
        }
    }
}
