using Progetto_ing_sw.Model;
using Progetto_ing_sw.Presenter.Gestionale;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_ing_sw.View
{
    public partial class GestionaleForm : Form
    {
        public GestionaleForm()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = Program.Culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = Program.Culture;
            InitializeComponent();
            ISelezione selezione = new Selezione();
            new GestionalePresenter(this, panel1, dataGridView1, dataGridView2, splitContainer1, groupBox2, statusStrip1, menuStrip1, selezione);
           
        }

        
    }
}
