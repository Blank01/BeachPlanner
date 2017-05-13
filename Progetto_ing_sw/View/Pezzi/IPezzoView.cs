using Progetto_ing_sw.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_ing_sw.View
{
    interface IPezzoView
    {
        void PaintMe(Graphics g);
        bool Selected { get; set; }
        
        Rectangle Rect { get; set; }
        String Numero { get; }
        String Tipo { get; }
        IPezzo Pezzo { get; }
        

        bool InArea(Point point);
    }
}
