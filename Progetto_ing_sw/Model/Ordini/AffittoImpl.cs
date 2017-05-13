using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Progetto_ing_sw.Utils;
using Progetto_ing_sw.Exceptions;

namespace Progetto_ing_sw.Model
{
    public class AffittoImpl : IAffitto
    {
        private DateRange _periodo;
        private string _id;
        private PezzoFisso _posto;
        private int _sdraio;
        private int _sedie;
        private int _lettini;

        public AffittoImpl(int lettini, int sdraio, int sedie, DateRange periodo)
        {
            if (lettini ==0 && sdraio ==0 && sedie ==0 || lettini + sdraio + sedie >= 6)
            {
                throw new AffittoException("Lista di pezzi mobili selezionati non valida");
            }
            this._periodo = periodo;
            _lettini = lettini;
            _sdraio = sdraio;
            _sedie = sedie;
            this._id = Util.GenerateID();
        }
        public AffittoImpl(int lettini, int sdraio, int sedie, DateRange periodo, string id) : this(lettini, sdraio, sedie, periodo)
        {
            this._id = id;
        }
        public AffittoImpl(PezzoFisso posto, int lettini, int sdraio, int sedie, DateRange periodo, string id) : this(lettini, sdraio, sedie, periodo, id)
        {
            this._posto = posto;
        }
        public AffittoImpl(PezzoFisso posto, int lettini, int sdraio, int sedie, DateRange periodo) : this(lettini, sdraio, sedie, periodo)
        {
            this._posto = posto;
        }
    
        string IAffitto.ID { get { return this._id; } }

        DateRange IAffitto.Periodo
        {
            get
            {
                return this._periodo;
            }
            set { this._periodo = value; }
        }

        PezzoFisso IAffitto.Posto
        {
            get { return this._posto; }
        }

        public int Lettini
        {
            get
            {
                return _lettini;
            }
            set { _lettini = value; }
        }

        public int Sdraio
        {
            get
            {
                return _sdraio;
            }
            set { _sdraio = value; }
        }

        public int Sedie
        {
            get
            {
                return _sedie;
            }
            set { _sedie = value; }
        }
    }
}
