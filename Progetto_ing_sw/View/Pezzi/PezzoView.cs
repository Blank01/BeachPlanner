using Progetto_ing_sw.Model;
using Progetto_ing_sw.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.View.Pezzi
{
    class PezzoView:IPezzoView
    {
        
        private bool _selected;
        private IPezzo _pezzo;
        public PezzoView(IPezzo pezzo)
        {
            this._pezzo = pezzo;
        }

        public void PaintMe(Graphics g)
        {
            string pezziMobili = "\n";
            if (_pezzo.Occupato)
            {
                if (_pezzo.Lettini != 0)
                    pezziMobili += _pezzo.Lettini + " L ";
                if (_pezzo.Sdraio != 0)
                    pezziMobili += _pezzo.Sdraio + " S ";
                if (_pezzo.Sedie != 0)
                    pezziMobili += _pezzo.Sedie + " ■";

            }

            if (_pezzo.Tipo == "Ombrellone") PaintOmbrellone(g, pezziMobili);
            else if (_pezzo.Tipo == "Tenda") PaintTenda(g, pezziMobili);
        }

        private void PaintOmbrellone(Graphics g,string pezziMobili)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (this._selected)
                g.FillEllipse(Drawing.SelectedBrush, _pezzo.Rect);
            else if (_pezzo.Occupato)
                g.FillEllipse(Drawing.BusyBrush, _pezzo.Rect);
            else if (_pezzo.OccupatoParzialmente)
                g.FillEllipse(Drawing.PartlyBusyBrush, _pezzo.Rect);
            else
                g.FillEllipse(Drawing.FreeBrush, _pezzo.Rect);

            if(_pezzo.Occupato || _pezzo.OccupatoParzialmente)
                g.DrawEllipse(Drawing.OutlineBusyPen, _pezzo.Rect);
            else
                g.DrawEllipse(Drawing.OutlinePen, _pezzo.Rect);
            g.DrawString(pezziMobili + "\n" + _pezzo.Numero, Drawing.Font, Brushes.Black, _pezzo.Rect, Drawing.SF);
        }

        public void PaintTenda(Graphics g, string pezziMobili)
        {
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            if (this._selected)
                g.FillRectangle(Drawing.SelectedBrush, _pezzo.Rect);
            else if (_pezzo.Occupato)
                g.FillRectangle(Drawing.BusyBrush, _pezzo.Rect);
            else if (_pezzo.OccupatoParzialmente)
                g.FillRectangle(Drawing.PartlyBusyBrush, _pezzo.Rect);
            else
                g.FillRectangle(Drawing.FreeBrush, _pezzo.Rect);

            if (_pezzo.Occupato || _pezzo.OccupatoParzialmente)
                g.DrawRectangle(Drawing.OutlineBusyPen, _pezzo.Rect);
            else
                g.DrawRectangle(Drawing.OutlinePen, _pezzo.Rect);
            g.DrawString(pezziMobili + "\n" + _pezzo.Numero, Drawing.Font, Brushes.Black, _pezzo.Rect, Drawing.SF);


        }

        public bool Selected
        {
            get { return this._selected; }
            set { this._selected = value; }
        }
        public int X
        {
            get { return this._pezzo.X; }
            set { this._pezzo.X = value; }
        }
        public int Y
        {
            get { return this.Y; }
            set { this.Y = value; }
        }
        public String Numero
        {
            get { return _pezzo.Numero.ToString(); }
        }
        public String Tipo
        {
            get { return "Ombrellone"; }
        }
        public Rectangle Rect
        {
            get { return this._pezzo.Rect; }
            set { this._pezzo.Rect = value; }
        }
        String IPezzoView.Numero
        {
            get
            {
                return _pezzo.Numero.ToString();
            }
        }
        String IPezzoView.Tipo
        {
            get
            {
                return _pezzo.Tipo;
            }
        }
        IPezzo IPezzoView.Pezzo
        {
            get
            {
                return this._pezzo;
            }
        }
        public bool InArea(Point point)
        {
            return _pezzo.Rect.Contains(point) ? true : false;
        }
    }
}
