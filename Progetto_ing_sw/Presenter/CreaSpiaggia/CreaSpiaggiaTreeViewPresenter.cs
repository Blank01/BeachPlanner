using Progetto_ing_sw.Model;
using Progetto_ing_sw.Model.Pezzi;
using Progetto_ing_sw.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Progetto_ing_sw.Presenter
{
    class CreaSpiaggiaTreeViewPresenter
    {
        private ISelezione _selezione;
        private TreeView _tree;
        private ISpiaggia spiaggia;
        public CreaSpiaggiaTreeViewPresenter(TreeView tree, ISelezione selezione)
        {
            if (tree == null)
                throw new ArgumentNullException("tree");
            if (selezione == null)
                throw new ArgumentNullException("selezione");
            _selezione = selezione;
            _tree = tree;
            spiaggia = Spiaggia.GetInstance();
            initTreeView();
            spiaggia.AreaChanged += new EventHandler(AreaChanged);
            _tree.AfterSelect += new TreeViewEventHandler(NodeClick);
            _tree.LostFocus += new EventHandler(LostFocus);
            _tree.KeyDown += new KeyEventHandler(KeyPress);
        }

        private void KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                string idArea;
                try
                {
                    idArea = _tree.SelectedNode.FirstNode.FirstNode.Text;
                }
                catch { return; }
                IArea area = spiaggia.GetArea(idArea);
                spiaggia.RimuoviArea(area);
                _selezione.Reset();
            }
                
        }

        private void LostFocus(object sender, EventArgs e)
        {
            _tree.SelectedNode = null;
        }

        private void NodeClick(object sender, TreeViewEventArgs e)
        {
            string idArea;
            try
            {
                idArea = e.Node.FirstNode.FirstNode.Text;
            }
            catch { return; }
            
            IArea area = spiaggia.GetArea(idArea);
            if (area == null)
                return;
            IList<IPezzo> pezziSelezionati = new List<IPezzo>();
            foreach(string id in area.Prezzi.Keys)
            {
                IPezzo p = spiaggia.GetPezzo(id);
                pezziSelezionati.Add(p);
            }
            _selezione.Select(pezziSelezionati);
        }
        
        

        private void AreaChanged(object sender, EventArgs e)
        {
            UpdateTreeView();
        }

        private void initTreeView()
        {
            UpdateTreeView();
            
        }
        private void UpdateTreeView()
        {
            
            DisplayAllAree();
            _tree.LabelEdit = false;
           
        }
        private void DisplayAllAree()
        {
            _tree.Nodes.Clear();
            foreach (IArea area in spiaggia.Aree)
            {
                IList<TreeNode> pezziFissi = new List<TreeNode>();
                foreach (string ID in area.Prezzi.Keys)
                {
                    IPezzo p = spiaggia.GetPezzo(ID);
                    pezziFissi.Add(new TreeNode(p.Tipo + " " + p.Numero + ": " + area.Prezzi[ID] + " Euro"));
                }
                TreeNode pezzi = new TreeNode("Pezzi",pezziFissi.ToArray());
                TreeNode id =new TreeNode("ID", new TreeNode[] { new TreeNode(area.ID) });
                TreeNode inizio = new TreeNode(area.Periodo.Inizio.ToShortDateString());
                TreeNode fine = new TreeNode(area.Periodo.Fine.ToShortDateString());
                TreeNode[] periodo = new TreeNode[] { inizio, fine };
                TreeNode periodi = new TreeNode("Periodo", periodo);
                IList<TreeNode> pezziMobili = new List<TreeNode>();
                foreach (string p1 in area.PrezziPezziMobili.Keys)
                    pezziMobili.Add(new TreeNode(p1 + ": " + area.PrezziPezziMobili[p1] + " Euro"));
                TreeNode mobili = new TreeNode("Pezzi Mobili:", pezziMobili.ToArray());
                TreeNode node = new TreeNode(area.Nome,new TreeNode[] { id, periodi, pezzi, mobili});
                
                _tree.Nodes.Add(node);
                
            }
            
            
        }
    }
}
