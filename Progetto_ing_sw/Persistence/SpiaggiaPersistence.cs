using Progetto_ing_sw.Model;
using Progetto_ing_sw.Model.Ordini;
using Progetto_ing_sw.Model.Pezzi;
using Progetto_ing_sw.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace Progetto_ing_sw.Persistence
{
    public static class SpiaggiaPersistence
    {
        public static bool SaveSpiaggia(String filename, IDictionary<string, IPezzo> pezzi, IList<IArea> aree)
        {
            try
            {
                XmlTextWriter xmltextWriter = new XmlTextWriter(filename, UTF8Encoding.Unicode) { Formatting = Formatting.Indented };

                xmltextWriter.WriteStartDocument();
                xmltextWriter.WriteStartElement("Spiaggia");
                xmltextWriter.WriteStartElement("Pezzi");

                foreach (IPezzo pezzo in pezzi.Values)
                {
                    xmltextWriter.WriteStartElement("Pezzo");
                    xmltextWriter.WriteAttributeString("ID", pezzo.ID);
                    xmltextWriter.WriteAttributeString("Tipo", pezzo.Tipo);
                    xmltextWriter.WriteAttributeString("X", pezzo.X.ToString());
                    xmltextWriter.WriteAttributeString("Y", pezzo.Y.ToString());
                    xmltextWriter.WriteAttributeString("Width", pezzo.Width.ToString());
                    xmltextWriter.WriteAttributeString("Height", pezzo.Height.ToString());
                    xmltextWriter.WriteAttributeString("Numero", pezzo.Numero.ToString());
                    xmltextWriter.WriteEndElement();
                }

                xmltextWriter.WriteEndElement();
                xmltextWriter.WriteStartElement("Aree");
                foreach (IArea area in aree)
                {
                    xmltextWriter.WriteStartElement("Area");
                    xmltextWriter.WriteAttributeString("ID", area.ID);
                    xmltextWriter.WriteAttributeString("Nome", area.Nome);
                    xmltextWriter.WriteAttributeString("Inizio", area.Periodo.Inizio.ToString("o"));
                    xmltextWriter.WriteAttributeString("Fine", area.Periodo.Fine.ToString("o"));
                    xmltextWriter.WriteStartElement("PezziContenuti");
                    xmltextWriter.WriteStartElement("PezziFissi");

                    foreach (KeyValuePair<String, double> entry in area.Prezzi)
                    {
                        xmltextWriter.WriteStartElement("Pezzo");
                        xmltextWriter.WriteAttributeString("ID", entry.Key);
                        xmltextWriter.WriteAttributeString("Prezzo", entry.Value.ToString());
                        xmltextWriter.WriteEndElement();

                    }
                    xmltextWriter.WriteEndElement();
                    xmltextWriter.WriteStartElement("PezziMobili");

                    foreach (KeyValuePair<String, double> entry in area.PrezziPezziMobili)
                    {
                        xmltextWriter.WriteStartElement("PezzoMobile");
                        xmltextWriter.WriteAttributeString("Tipo", entry.Key);
                        xmltextWriter.WriteAttributeString("Prezzo", entry.Value.ToString());
                        xmltextWriter.WriteEndElement();

                    }
                    xmltextWriter.WriteEndElement();

                    xmltextWriter.WriteEndElement();
                    xmltextWriter.WriteEndElement();


                }
                xmltextWriter.WriteEndElement();

                xmltextWriter.WriteEndElement();
                xmltextWriter.Flush();
                xmltextWriter.Close();
            }catch (Exception e)
            {
                MessageBox.Show("Errore durante salvataggio file \n" + e.Message + "\n" + e.StackTrace, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        public static bool LoadSpiaggia(String filename)
        {   
            try
            {
                Spiaggia.GetInstance().Aree = new List<IArea>();
                Spiaggia.GetInstance().Pezzi = new Dictionary<string, IPezzo>();
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);

                XmlNode node = doc.SelectSingleNode("Spiaggia");

                XmlNodeList nodesPezzi = node.SelectSingleNode("Pezzi").SelectNodes("Pezzo");
                IList<IPezzo> pezzi = new List<IPezzo>();
                foreach (XmlNode item in nodesPezzi)
                {
                    String id = item.Attributes["ID"].Value;
                    int n = Int32.Parse(item.Attributes["Numero"].Value);
                    int x = Int32.Parse(item.Attributes["X"].Value);
                    int y = Int32.Parse(item.Attributes["Y"].Value);
                    int width = Int32.Parse(item.Attributes["Width"].Value);
                    String tipo = item.Attributes["Tipo"].Value;
                    int height = Int32.Parse(item.Attributes["Height"].Value);
                    IPezzo p;
                    if(tipo == "Ombrellone")
                    {
                        p = new Ombrellone(new Rectangle(x, y, width, height), n, id);
                    }else if(tipo == "Tenda")
                    {
                        p = new Tenda(new Rectangle(x, y, width, height), n, id);

                    }
                    else
                    {
                        p = new Ombrellone(new Rectangle(x, y, width, height), n);
                    }

                    pezzi.Add(p);
                }
                XmlNodeList nodesAree = node.SelectSingleNode("Aree").SelectNodes("Area");
                IList<IArea> aree = new List<IArea>();
                foreach (XmlNode item in nodesAree)
                {
                    
                    String nome = item.Attributes["Nome"].Value;
                    String ID = item.Attributes["ID"].Value;
                    DateTime inizio = DateTime.Parse(item.Attributes["Inizio"].Value);
                    DateTime fine = DateTime.Parse(item.Attributes["Fine"].Value);
                    DateRange periodo = new DateRange(inizio, fine);
                    XmlNodeList pezziContenutiNode = item.SelectSingleNode("PezziContenuti").SelectSingleNode("PezziFissi").SelectNodes("Pezzo");
                    IDictionary<string, double> pezziContenuti = new Dictionary<string,double>();
                    foreach(XmlNode pNode in pezziContenutiNode)
                    {
                        String id = pNode.Attributes["ID"].Value;
                        double prezzo = Double.Parse(pNode.Attributes["Prezzo"].Value);
                        pezziContenuti.Add(id, prezzo);
                    }

                    XmlNodeList pezziMobiliNode = item.SelectSingleNode("PezziContenuti").SelectSingleNode("PezziMobili").SelectNodes("PezzoMobile");
                    IDictionary<string, double> pezziMobili = new Dictionary<string, double>();
                    foreach (XmlNode pNode in pezziMobiliNode)
                    {
                        String tipo = pNode.Attributes["Tipo"].Value;
                        double prezzo = Double.Parse(pNode.Attributes["Prezzo"].Value);
                        pezziMobili.Add(tipo, prezzo);
                    }
                    IArea area = new AreaPrezzoPieno(nome, periodo, ID);
                    area.Prezzi = pezziContenuti;
                    area.PrezziPezziMobili = pezziMobili;
                    aree.Add(area);
                }

                Spiaggia.GetInstance().AggiungiPezzi(pezzi);
                Spiaggia.GetInstance().AggiungiAree(aree);

            }
            catch(Exception e)
            {
                MessageBox.Show("Errore lettura file \n" + e.Message + "\n" + e.StackTrace, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public static bool SaveOrdini(string filename)
        {

            ISpiaggia spiaggia = Spiaggia.GetInstance();
            IList<IOrdine> ordini = spiaggia.Ordini;
            ISet<ICliente> clienti = new HashSet<ICliente>();

            try
            {
                XmlTextWriter xmltextWriter = new XmlTextWriter(filename, UTF8Encoding.Unicode) { Formatting = Formatting.Indented };


                xmltextWriter.WriteStartDocument();
                xmltextWriter.WriteStartElement("Spiaggia");
                xmltextWriter.WriteAttributeString("Path", spiaggia.Filename);
                xmltextWriter.WriteStartElement("Ordini");

                foreach (IOrdine o in ordini)
                {
                    xmltextWriter.WriteStartElement("Ordine");
                    xmltextWriter.WriteAttributeString("ID", o.ID);
                    xmltextWriter.WriteAttributeString("ClienteID", o.Cliente.ID);
                    xmltextWriter.WriteAttributeString("DataCreazione", o.Giorno.ToString("o"));

                    xmltextWriter.WriteStartElement("Affitti");
                    foreach (string id in o.Affitti)
                    {
                        xmltextWriter.WriteStartElement("Affitto");
                        IAffitto affitto = spiaggia.Affitti[id];
                        xmltextWriter.WriteAttributeString("ID", affitto.ID);
                        if (affitto.Posto != null)
                            xmltextWriter.WriteAttributeString("PostoID", affitto.Posto.ID);
                        xmltextWriter.WriteAttributeString("Lettini", affitto.Lettini.ToString());
                        xmltextWriter.WriteAttributeString("Sdraio", affitto.Sdraio.ToString());
                        xmltextWriter.WriteAttributeString("Sedie", affitto.Sedie.ToString());
                        xmltextWriter.WriteAttributeString("DataInizio", affitto.Periodo.Inizio.ToString("o"));
                        xmltextWriter.WriteAttributeString("DataFine", affitto.Periodo.Fine.ToString("o"));

                        xmltextWriter.WriteEndElement();
                    }
                    xmltextWriter.WriteEndElement();
                    xmltextWriter.WriteEndElement();
                    clienti.Add(o.Cliente);
                }
                
                xmltextWriter.WriteEndElement();
                xmltextWriter.WriteStartElement("Clienti");
                foreach (ICliente c in clienti)
                {
                    xmltextWriter.WriteStartElement("Cliente");
                    xmltextWriter.WriteAttributeString("ID", c.ID);
                    xmltextWriter.WriteAttributeString("Hotel", c.Hotel);
                    xmltextWriter.WriteAttributeString("Nome", c.Nome);
                    xmltextWriter.WriteAttributeString("Mail", c.Mail);
                    xmltextWriter.WriteEndElement();
                    
                }
                xmltextWriter.WriteEndElement();
                xmltextWriter.WriteEndElement();
                xmltextWriter.Flush();
                xmltextWriter.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Errore durante salvataggio file \n" + e.Message + "\n" + e.StackTrace, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public static bool LoadOrdini(string filename)
        {
            ISpiaggia spiaggia = Spiaggia.GetInstance();
            try
            {
                spiaggia.Affitti = new Dictionary<string, IAffitto>();
                spiaggia.Ordini = new List<IOrdine>();
                IDictionary<string, ICliente> clienti = new Dictionary<string, ICliente>();
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                XmlNode node = doc.SelectSingleNode("Spiaggia");
                XmlNode node1 = node.SelectSingleNode("Clienti");
                XmlNodeList nodesClienti = node1.SelectNodes("Cliente");
                IList<IPezzo> pezzi = new List<IPezzo>();
                foreach (XmlNode item in nodesClienti)
                {
                    String id = item.Attributes["ID"].Value;
                    if (clienti.Keys.Contains(id))
                        continue;
                    String nome = item.Attributes["Nome"].Value;
                    String hotel = item.Attributes["Hotel"].Value;
                    String mail = item.Attributes["Mail"].Value;
                    clienti.Add(id, new ClientImpl(nome, hotel, mail, id));
                }

                XmlNode node2 = node.SelectSingleNode("Ordini");
                XmlNodeList nodesOrdini = node2.SelectNodes("Ordine");
                foreach(XmlNode item in nodesOrdini)
                {
                    XmlNodeList nodesAffitti = item.SelectSingleNode("Affitti").SelectNodes("Affitto");
                    IList<string> affittiOrdine = new List<string>();
                    foreach (XmlNode itemAffitto in nodesAffitti)
                    {
                        string idAffitto = itemAffitto.Attributes["ID"].Value;
                        string postoId = itemAffitto.Attributes["PostoID"].Value;
                        PezzoFisso posto = spiaggia.GetPezzo(postoId) as PezzoFisso;
                        int lettini = Int32.Parse(itemAffitto.Attributes["Lettini"].Value);
                        int sdraio = Int32.Parse(itemAffitto.Attributes["Sdraio"].Value);
                        int sedie = Int32.Parse(itemAffitto.Attributes["Sedie"].Value);
                        DateTime inizio = DateTime.Parse(itemAffitto.Attributes["DataInizio"].Value);
                        DateTime fine = DateTime.Parse(itemAffitto.Attributes["DataFine"].Value);
                        DateRange periodo = new DateRange(inizio, fine);
                        IAffitto affitto;
                        if (posto == null)
                            affitto = new AffittoImpl(lettini, sdraio, sedie, periodo, idAffitto);
                        else
                            affitto = new AffittoImpl(posto, lettini, sdraio, sedie, periodo, idAffitto);
                        spiaggia.AggiungiAffitto(affitto);
                        affittiOrdine.Add(affitto.ID);
                    }
                    
                    string id = item.Attributes["ID"].Value;

                    ICliente c = clienti[item.Attributes["ClienteID"].Value];
                    DateTime creazione = DateTime.Parse(item.Attributes["DataCreazione"].Value);
                    IOrdine ordine = new OrdineImpl(c, affittiOrdine, creazione, id);
                    spiaggia.AggiungiOrdine(ordine);

                }

            }
            catch(Exception e)
            {
                MessageBox.Show("Errore durante salvataggio file \n" + e.Message + "\n" + e.StackTrace, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            return true;


        }


    }

}
