using Progetto_ing_sw.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Model.Ordini
{
    class OrdineImpl : IOrdine
    {
        private IList<string> _affitti;
        private ICliente _cliente;
        private DateTime _giorno;
        private string _id;

        public OrdineImpl(ICliente cliente, IList<string> affittiId)
        {
            this._id = Util.GenerateID();
            this._cliente = cliente;
            this._giorno = DateTime.Today;
            this._affitti = affittiId;
        }

        public OrdineImpl(ICliente cliente, IList<string> affittiId, DateTime giorno) : this(cliente, affittiId)
        {
            this._giorno = giorno;
        }
        public OrdineImpl(ICliente cliente, IList<string> affittiId, DateTime giorno, string id) : this(cliente, affittiId, giorno)
        {
            this._id = id;
        }
        public OrdineImpl(IList<string> affittiId):this(cliente:new ClientImpl(), affittiId:affittiId)
        {

        }
        public IList<string> Affitti
        {
            get
            {
                return _affitti;
            }
        }

        public ICliente Cliente
        {
            get
            {
                return _cliente;
            }
        }

        public DateTime Giorno
        {
            get
            {
                return _giorno;
            }
        }
        public string ID
        {
            get { return _id;}
        }
    }
}
