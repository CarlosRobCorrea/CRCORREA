using System;
using System.Drawing;
using System.Collections.Generic;
//using static System.Net.Mime.MediaTypeNames;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace CRCorreaInfo
{
    public class clsEmpresaInfo
    {
        public String andar { get; set; }
        public Int32 codigo { get; set; }
        public String cognome { get; set; }
        public String pessoa { get; set; }
        public String nome { get; set; }
        public String cgc { get; set; }
        public String ibge { get; set; }
        public Int32 id { get; set; }
        public String ie { get; set; }
        public String imunicipal { get; set; }
        public String cadastrado { get; set; }
        public String endereco { get; set; }
        public Image logotipo { get; set; }
        public String tiponumero { get; set; }
        public String numeroend { get; set; }
        
        public String tipocomple { get; set; }
        public String comple { get; set; }
        public String bairro { get; set; }
        public String cidade { get; set; }
        public Int32 idcidade { get; set; }
        public Int32 idestado { get; set; }
        public Int32 idramo { get; set; }
        public String cep { get; set; }
        public String ddd { get; set; }
        public String telefone { get; set; }
        public String contato { get; set; }

    }
}
