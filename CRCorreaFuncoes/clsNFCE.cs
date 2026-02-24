using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Transactions;
using CRCorreaInfo;

namespace CRCorreaFuncoes
{
    public class DanfeInfo
    {
        public Decimal valorliquido;
        public Decimal multas;
        public Decimal juros;
        public Decimal descontos;
        public Decimal credito;
        public Decimal saldo;
    }
    public class clsNFCE
    {
        SqlConnection scn;
        SqlCommand scd;
        public static DanfeInfo EmitirNFCE(clsPedidoInfo Info) 
        {
            DanfeInfo danfeinfo = new DanfeInfo();
            String ok = "N";

            SqlConnection scn;
            SqlCommand scd;
            return danfeinfo;
        }
    }
}