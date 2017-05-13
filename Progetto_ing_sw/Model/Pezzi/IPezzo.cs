using Progetto_ing_sw.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Progetto_ing_sw.Model
{

    public interface IPezzo
    {

        
        String Tipo { get; }
        int Numero { get; }
        string ID { get; }
        IList<IAffitto> Affitti { get; set; }
        Rectangle Rect { get; set; }
        Point Location { get; set; }
        int Lettini { get; set; }
        int Sedie { get; set; }
        int Sdraio { get; set; }
        int X { get; set; }
        int Y { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        bool Occupato { get; set; }
        bool OccupatoParzialmente { get; set; }

        String Nome { get; }

        double CalcolaPrezzo(DateRange periodo);


    }
}
