using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmAcerto : Form
    {
        public frmAcerto()
        {
            InitializeComponent();
        }
        public void Init()
        {
        }
        private void btnRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAcerto_Click(object sender, EventArgs e)
        {
            String query;
            SqlConnection scn;
            SqlCommand scd;

            query = "SELECT PECAS.ID, PECAS.CODIGO, PECAS.NOME, PECAS.TIPOPRODUTO, PECAS.PRECOCOMPRA, PEDIDO1.ID AS PEDIDO1ID, PEDIDO1.* " +
                    "FROM  PEDIDO1 " +
                    "left join pedido on pedido.id = pedido1.IDPEDIDO " +
                    "left join pecas on pecas.id = pedido1.IDCODIGO " +
                    "ORDER BY PECAS.CODIGO ";

            // Preparar a Tabela ja Filtrando os dados solicitados
            DataTable dtaux = new DataTable();
            dtaux = Procedure.RetornaDataTable(clsInfo.conexaosqldados, query);
            Decimal precocusto = 0;
            Decimal totalcusto = 0;

            foreach (DataRow row in dtaux.Rows)
            {
                if (row["CODIGO"].ToString() == "" || row["CODIGO"].ToString() == "0" || row["CODIGO"].ToString() == "00")
                {
                    continue;
                }
                else
                {
                    if (row["TIPOPRODUTO"].ToString() == "00")
                    {
                        if (clsParser.DecimalParse(row["PRECOCUSTO"].ToString()) == 0)
                        {
                            // Calcular o preço de custo e colocar na tabela de pedido1
                            precocusto = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select PRECOCOMPRA from PECAS where ID =" + clsParser.Int32Parse(row["ID"].ToString()) + " "));
                            if (precocusto == 0)
                            {
                                precocusto = ((clsParser.DecimalParse(row["PRECOCOMPRA"].ToString()) * 70) / 100);

                                scn = new SqlConnection(clsInfo.conexaosqldados);
                                scn.Open();
                                scd = new SqlCommand("update pecas set " +
                                                     "precocusto= @precocusto " +
                                                     "where id=@id ", scn);
                                scd.Parameters.Add("@id", SqlDbType.Int).Value = clsParser.Int32Parse(row["ID"].ToString());
                                scd.Parameters.Add("@precocusto", SqlDbType.Int).Value = precocusto;

                                scd.ExecuteNonQuery();
                                scn.Close();


                            }
                            totalcusto = precocusto * clsParser.DecimalParse(row["QTDE"].ToString());
                        }
                    }
                    if (totalcusto > 0)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        scn.Open();
                        scd = new SqlCommand("update pedido1 set " +
                                             "precocusto= @precocusto, totalcustoitem=@totalcustoitem " +
                                             "where id=@pedido1id ", scn);
                        scd.Parameters.Add("@pedido1id", SqlDbType.Int).Value = clsParser.Int32Parse(row["PEDIDO1ID"].ToString());
                        scd.Parameters.Add("@precocusto", SqlDbType.Int).Value = precocusto;
                        scd.Parameters.Add("@totalcustoitem", SqlDbType.Int).Value = totalcusto;

                        scd.ExecuteNonQuery();
                        scn.Close();
                        precocusto = 0;
                        totalcusto = 0;
                    }
                }
            }
            MessageBox.Show("Termino Acerto");
        }

    }
}