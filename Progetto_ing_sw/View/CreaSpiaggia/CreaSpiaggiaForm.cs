using Progetto_ing_sw.Model;
using Progetto_ing_sw.Presenter;
using Progetto_ing_sw.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_ing_sw
{
    public partial class CreaSpiaggia : Form
    {
        private ISelezione _selezione;
        public CreaSpiaggia()
        {
            InitializeComponent();
            

            //Selezione
            _selezione = new Selezione();
            //Init presenter
            new CreaSpiaggiaPresenter(this, panel1, menuStrip1, dataGridView1, treeView1);

        }

        
    }
}
