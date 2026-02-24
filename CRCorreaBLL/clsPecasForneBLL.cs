using CRCorreaFuncoes;
using CRCorreaInfo;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsPecasForneBLL : SQLFactory<clsPecasForneInfo>
    {
        public override Int32 Incluir(clsPecasForneInfo info, String cnn)
        {
            VerificaInfo(info);  

            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsPecasForneInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Alterar(info, cnn);
        }

        public void VerificaInfo(clsPecasForneInfo info)
        {
            if (info.descricaofornecedor.ToString().Length > 50)
            {
                throw new Exception("Descrição do produto do fornecedor não pode ser maior que 50 caracteres!");
            }

            if (info.codigofornecedor.ToString().Length > 50)
            {
                throw new Exception("Codigo do produto do fornecedor não pode ser maior que 50 caracteres!");
            }
        }

        //
        public static DataTable GridDados(Int32 IdCodigo, String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT PECASFORNE.ID, PECASFORNE.IDCODIGO, PECASFORNE.IDFORNECEDOR, " +
                    "CLIENTE.COGNOME as [FORNECEDOR], PECASFORNE.DATABASE1, PECASFORNE.PRECOUNI, " +
                    "PECASFORNE.IDUNIDADE, UNIDADE.CODIGO AS [UNID], PECASFORNE.FATORCONV, " +
                    "PECASFORNE.CODIGOFORNECEDOR, PECASFORNE.DESCRICAOFORNECEDOR,  " +
                    "PECASFORNE.DIAS, PECASFORNE.ICMS " +
                    "FROM PECASFORNE " +
                    "INNER JOIN CLIENTE ON CLIENTE.ID=PECASFORNE.IDFORNECEDOR " +
                    "LEFT JOIN UNIDADE ON UNIDADE.ID=PECASFORNE.IDUNIDADE ";
            filtro = " WHERE PECASFORNE.IDCODIGO = @IDCODIGO  ORDER BY CLIENTE.COGNOME ";
            query = query + " " + filtro;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = IdCodigo;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Fornecedor", "Fornecedor", 110, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Dt Base", "DATABASE1", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Preço Unit.", "PRECOUNI", 75, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Unid", "UNID", 25, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Fator", "FATORCONV", 65, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Cod. Fornec", "CODIGOFORNECEDOR", 65, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Descrição Fornec", "DESCRICAOFORNECEDOR", 150, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qt Dias", "DIAS", 65, true, DataGridViewContentAlignment.MiddleRight),
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "duplicata");
            dgv.Columns["DATABASE1"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["PRECOUNI"].DefaultCellStyle.Format = "N6";
            dgv.Columns["FATORCONV"].DefaultCellStyle.Format = "N6";
        }


    }
}
