using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmPecasExportar : Form
    {
        Int32 id;
        public frmPecasExportar()
        {
            InitializeComponent();
        }
        public void Init(Int32 _idcodigo)
        {
            id = _idcodigo;
            DataTable dtPecas;

        }

        private void frmPecasExportar_Load(object sender, EventArgs e)
        {

        }

        private void tspPecasExportaSalvar_Click(object sender, EventArgs e)
        {
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            DataTable dtPecas;
            query = "select PECAS.CODIGO AS COD, PECAS.NOME AS NOM, ROUND((PECAS.QTDESALDO*.30),0) AS QTD, PECAS.PRECOVENDA AS PREV, IPI.CODIGO AS NCM " +
                    "from pecas " +
                    "LEFT JOIN IPI ON IPI.ID = PECAS.IDIPI " +
                    "WHERE PECAS.ATIVO ='S' AND PECAS.QTDESALDO>0 " +
                    "order by PECAS.NOME";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            dtPecas = new DataTable();
            sda.Fill(dtPecas);

            StreamWriter arq = new StreamWriter("c:\\Clientes\\Arquivos\\PecasNew.txt");

            foreach (DataRow row in dtPecas.Rows)
            {
                arq.WriteLine(row["COD"].ToString() + ";" + row["NOM"].ToString() + ";" +
                              row["QTD"].ToString() + ";" + row["PREV"].ToString() + ";" +
                              row["NCM"].ToString());

            }
            arq.Close();
/*
            string path = @"c:\Clientes\Arquivos\PecasNew.txt";
            if (!File.Exists(path))
            {
                using (var sw = File.CreateText(path))
                {
                    sw.WriteLine(value: "Hello");
                }
            }
*/
        }

        private void tspPecasExportaRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
