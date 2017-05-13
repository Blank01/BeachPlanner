using Progetto_ing_sw.Model;
using Progetto_ing_sw.Model.Eventi;
using Progetto_ing_sw.Utils;
using Progetto_ing_sw.View;
using Progetto_ing_sw.View.Pezzi;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_ing_sw.Presenter
{
    class SpiaggiaPanelPresenter
    {
        private readonly SpiaggiaPanelPresenter _self;
        private readonly Panel _panel;
        private readonly ISelezione _selezione;
        private Rectangle rectSelection;
        private Point _mouseDownLocation;
        private bool _mousePressed;
        private ISpiaggia spiaggia;
        private IList<IPezzoView> pezzi;
        private IList<IPezzoView> _pezziSelezionati;
        private Brush rectBrush = new SolidBrush(Color.FromArgb(100, 120, 150, 220));
        private Size _size;
        private bool _moving;

        public SpiaggiaPanelPresenter(Panel panel, ISelezione selezione, bool editable)
        {
            _self = this;
            spiaggia = Spiaggia.GetInstance();
            _panel = panel;
            _selezione = selezione;
            _pezziSelezionati = new List<IPezzoView>();
            _moving = false;
            rectSelection = new Rectangle(0, 0, 0, 0);
            pezzi = new List<IPezzoView>();

            foreach (IPezzo p in spiaggia.Pezzi.Values)
            {
                IPezzoView p1 = new PezzoView(p);
                pezzi.Add(p1);
            }

            if (editable)
            {
                
                _panel.MouseMove += new MouseEventHandler(this.panel_MouseMove);
                _panel.PreviewKeyDown += new PreviewKeyDownEventHandler(panel_PreviewKeyDown);
                (_panel as Control).KeyDown += new KeyEventHandler(PanelKeyDown);
                spiaggia.PezziMoved += new EventHandler(this.pezziMoved);
            }
            spiaggia.PezziChanged += new EventHandler(this.pezzi_Changed);
            _panel.MouseDown += new MouseEventHandler(this.panel_MouseDown);
            _panel.MouseUp += new MouseEventHandler(this.panel_MouseUp);
            _panel.Paint += new PaintEventHandler(this.panel_Paint);
            _selezione.Changed += new EventHandler(RefreshSelezione);

            //Elimina flicker dello schermo
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
            | BindingFlags.Instance | BindingFlags.NonPublic, null,
            _panel, new object[] { true });
        }

        private void pezziMoved(object sender, EventArgs e)
        {
            
            _panel.Invalidate();
        }

        private void panel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || 
                e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
                e.IsInputKey = true;
            
        }

        private void PanelKeyDown(object sender, KeyEventArgs e)
        {
            int modifier = Commands.IsShiftDown() ? 5 : 1;
            
            switch (e.KeyCode)
            {
                case (Keys.Down):
                    UpdateSelected(0, -1 * modifier);
                    
                    break;
                case (Keys.Up):
                    UpdateSelected(0, 1 * modifier);
                    break;
                case (Keys.Left):
                    UpdateSelected(1 * modifier, 0);
                    break;
                case (Keys.Right):
                    UpdateSelected(-1 * modifier, 0);
                    break;
                case (Keys.Delete):
                    _selezione.RemoveSelected();
                    break;
            }
            
            _panel.Focus();
            

        }

        public void RefreshSelezione(object sender, EventArgs e)
        {
            RefreshSelezione();
            this._panel.Invalidate();
        }
        public void RefreshSelezione()
        {
            foreach (IPezzoView p in pezzi)
            {
                p.Selected = false;
            }
            _pezziSelezionati = new List<IPezzoView>();
            foreach (IPezzoView p in pezzi)
            {
                if (_selezione.GetSelezione().Contains(p.Pezzo))
                {
                    p.Selected = true;
                    _pezziSelezionati.Add(p);

                }
            }
        }

        private void selezione_Changed(object sender, EventArgs e)
        {
            this._panel.Invalidate();
        }

        private void pezzi_Changed(object sender, EventArgs e)
        {
            _panel.Focus();
            pezzi = new List<IPezzoView>();
            foreach(IPezzo p in spiaggia.Pezzi.Values)
            {
                IPezzoView p1 = new PezzoView(p);
                pezzi.Add(p1);
            }
            this._panel.Invalidate();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            _panel.Focus();
        }
        

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(rectBrush, rectSelection);
            foreach (IPezzoView p in pezzi)
                p.PaintMe(e.Graphics);
        }

        protected void panel_MouseDown(object sender, MouseEventArgs e)
        {
            _panel.Focus();
            if (e.Button == MouseButtons.Left)
            {
                _mouseDownLocation = e.Location;
                _mousePressed = true;
                IPezzo pezzo = CheckAreaPezzi(e.Location);
                if (pezzo != null)
                {
                    _selezione.Select(pezzo);
                    RefreshSelezione();
                }
                else if(!Commands.IsControlDown())
                    _selezione.Reset();
                if (!_selezione.GetSelezione().Any())
                {
                    rectSelection.Location = _mouseDownLocation;
                }

            }
            
        }
        protected void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _mousePressed)
            {
                int deltaX = _mouseDownLocation.X - e.X;
                int deltaY = _mouseDownLocation.Y - e.Y;

                bool isEmpty = !_selezione.GetSelezione().Any();
                if (isEmpty)
                {
                    ResizeRectSelection(_mouseDownLocation, e.Location);
                }
                else
                {
                    _mouseDownLocation = e.Location;
                    UpdateSelected(deltaX, deltaY);
                    _moving = true;

                }
            }

        }

        private IList<IPezzoView> GetSelezionati()
        {
            return _pezziSelezionati;
        }

        protected void panel_MouseUp(object sender, MouseEventArgs e)
        {
            _panel.Focus();
            _mousePressed = false;
            
            if (rectSelection.Width != 0 && rectSelection.Height != 0)
            {
                List<IPezzo> pezziSelezionati = CheckAreaPezzi(rectSelection);
                if (pezziSelezionati.Any())
                {
                    _selezione.Select(pezziSelezionati);
                }
                this.rectSelection = new Rectangle(0, 0, 0, 0);
                _panel.Invalidate();
            }else if (_moving)
            {
                _moving = false;
                spiaggia.OnPezziMoved(_self, _selezione.GetSelezione());
                _size = CalcolaDimensioni();
            }
        }

        private List<IPezzo> CheckAreaPezzi(Rectangle rectSelection)
        {
            
            List<IPezzo> pezziSelezionati = new List<IPezzo>();
            foreach (IPezzoView pV in pezzi)
            {
                if (rectSelection.IntersectsWith(pV.Rect))
                {
                    pezziSelezionati.Add(pV.Pezzo);
                }
            }
            
            return pezziSelezionati;
        }

        private IPezzo CheckAreaPezzi(Point pt)
        {
            IPezzo p;
            IList<IPezzo> pezzi = new List<IPezzo>();
            foreach (IPezzo p1 in spiaggia.Pezzi.Values)
                pezzi.Add(p1);
            for (int i = spiaggia.Pezzi.Count - 1; i >= 0; i--)
            {
                p = pezzi[i];
                if (p.Rect.Contains(pt))
                {
                    return p;
                }

            }
            return null;
        }

        public void UpdateSelected(int deltaX, int deltaY)
        {
            
            if (!pezzi.Any())
                return;
            foreach (IPezzoView pezzo in _pezziSelezionati)
            {
                pezzo.Pezzo.X -= deltaX;
                pezzo.Pezzo.Y -= deltaY;
                pezzo.Selected = true;
               
            }
            
        }

        private void ResizeRectSelection(Point startPoint, Point endPoint)
        {
            this.rectSelection = new Rectangle(
                        Math.Min(startPoint.X, endPoint.X),
                        Math.Min(startPoint.Y, endPoint.Y),
                        Math.Abs(endPoint.X - startPoint.X),
                        Math.Abs(endPoint.Y - startPoint.Y));
            _panel.Invalidate();
        }

        public Size Size {
            get
            {
                if (!spiaggia.Pezzi.Any() &&
                    _size.Height!=0 &&
                    _size.Width!=0)
                    return _size;
                _size = CalcolaDimensioni();
                return _size;
            }
        }

        private Size CalcolaDimensioni()
        {
            Size s = new Size(0, 0);
            
            foreach (IPezzo p in spiaggia.Pezzi.Values)
            {
                int x = p.Rect.X + p.Rect.Width;
                int y = p.Rect.Y + p.Rect.Height;
                if (x > s.Width)
                    s.Width = x;
                if (y > s.Height)
                    s.Height = y;
            }
            return s;
        }
    }
}
