using Progetto_ing_sw.Utils;
using System;

namespace Progetto_ing_sw.Model.Ordini
{
    public class ClientImpl : ICliente
    {
        private string _hotel;
        private string _id;
        private string _mail;
        private string _nome;

        public ClientImpl()
        {
            this._hotel = "N/A";
            this._id = Util.GenerateID();
            this._mail = "N/A";
            this._nome = "N/A";
        }
        
        public ClientImpl(string nome, string hotel, string mail) : this()
        {
            if (nome != null || nome.Trim() != "")
                this._nome = nome.Trim();
            if(hotel!=null || hotel.Trim() != "")
                this._hotel = hotel.Trim();
            if (mail != null || mail.Trim() != "")
                this._mail = mail.Trim();
        }
        public ClientImpl(string nome, string hotel, string mail, string id) : this(nome, hotel, mail)
        {
            this._id = ID;
        }


        public string Hotel
        {
            get
            {
                return _hotel;
            }
        }

        public string ID
        {
            get
            {
                return _id;
            }
        }

        public string Mail
        {
            get
            {
                return _mail;
            }
        }

        public string Nome
        {
            get
            {
                return _nome;
            }
        }
    }
}