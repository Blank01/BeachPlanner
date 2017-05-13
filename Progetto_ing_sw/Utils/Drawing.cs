using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Utils
{
    public static class Drawing
    {
        private static Pen outlinePen;
        private static Brush freeBrush;
        private static Brush busyBrush;
        private static Brush selectedBrush;
        private static FontFamily fontFamily;
        private static Font font;
        private static StringFormat sf;
        private static Pen busyPen;
        private static Brush partlyBusyBrush;

        public static void init()
        {
            outlinePen = new Pen(Brushes.Black);
            outlinePen.Width = 1;
            busyPen = new Pen(Brushes.Red);
            busyPen.Width = 1;
            freeBrush = Brushes.LawnGreen;
            busyBrush = Brushes.LightSalmon;
            partlyBusyBrush = Brushes.Yellow;
            selectedBrush = Brushes.LightYellow;
            fontFamily = new FontFamily("Arial");
            font = font = new Font(fontFamily, 8,
                FontStyle.Regular, GraphicsUnit.Pixel);
            sf = new StringFormat(StringFormatFlags.NoClip);
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
        }

        public static Font Font { get { return font; } }
        public static Pen OutlinePen { get { return outlinePen; } }
        public static Pen OutlineBusyPen { get { return busyPen; } }
        public static Brush FreeBrush { get { return freeBrush; } }
        public static Brush BusyBrush { get { return busyBrush; } }
        public static Brush PartlyBusyBrush { get { return partlyBusyBrush; } }
        public static Brush SelectedBrush { get { return selectedBrush; } }
        public static FontFamily FF { get { return fontFamily; } }
        public static StringFormat SF { get { return sf; } }
    }
}
