using Progetto_ing_sw.Presenter;
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
    public partial class LoaderForm : Form
    {
        public LoaderForm()
        {

            InitializeComponent();
            new LoaderPresenter(this, button3, button2, button1, textBox1, textBox2, button4);
        }

        
    }
}
