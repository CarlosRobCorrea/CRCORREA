using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaInfo
{
    public class clsFormsObjetoInfo
    {
        private Int32 _id;
        private Int32 _idforms;
        private Int32 _idformsobjeto;
        private Nullable<DateTime> _datac;
        private String _nome;
        private String _nomeobjeto;
        private String _tipo;
        private String _ativo;
        private String _visivel;
        private String _lista;
        private Int32 _posleft;
        private Int32 _postop;
        private Int32 _posdif;

        public Int32 id { get; set; }
        public Int32 idforms { get; set; }
        public Int32 idformsobjeto { get; set; }
        public Nullable<DateTime> datac { get; set; }
        public String nome { get; set; }
        public String nomeobjeto { get; set; }
        public String tipo { get; set; }
        public String ativo { get; set; }
        public String visivel { get; set; }
        public String lista { get; set; }
        public Int32 posleft { get; set; }
        public Int32 postop { get; set; }
        public Int32 posdif { get; set; }
    }
}
