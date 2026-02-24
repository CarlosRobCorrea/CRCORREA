using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CRCorreaFuncoes
{
    public class clsUsuarioFormsInfo
    {

        public Int32 id { get; set; }
        public Int32 idusuario { get; set; }
        public Int32 idforms { get; set; }
        public String permitido { get; set; }

    }
}
