using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorreaBLL
{ 
    public class clsMaqctBLL : SQLFactory<clsMaqctInfo>
    {
        public override int Incluir(clsMaqctInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsMaqctInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Alterar(info, cnn);
        }


        public void VerificaInfo(clsMaqctInfo info)
        {
            if (info.ativo == null || 
                info.ativo == "" ||
                (info.ativo != "S" &&
                 info.ativo != "N"))
            {
                throw new Exception("Valor para o campo 'Ativo' é inválido.");
            }

            if (info.codigo == null || info.codigo == "")
            {
                throw new Exception("Código inválido.");
            }

            if (info.nome == null || info.nome == "")
            {
                throw new Exception("Nome inválido.");
            }
        }

        public DataTable CarregaGrid(String cnn, String ativo)
        {
            ativo = ativo.ToUpper();

            DataTable dt;
            String query;
            SqlDataAdapter sda;

            dt = new DataTable();

            query = "SELECT " +
				            " * " +
			            "FROM " +
				            "MAQCT ";

            if (ativo != "T" && (ativo == "S" || ativo == "N"))
            {
                query += " where ATIVO = @ATIVO ";
            }

            sda = new SqlDataAdapter(query, cnn);

            if (ativo != "T" && (ativo == "S" || ativo == "N"))
            {
                sda.SelectCommand.Parameters.Add("@ATIVO", SqlDbType.NVarChar).Value = ativo;
            }

            sda.Fill(dt);

            return dt;
        }

     
        private static GridColuna[] dtGridColunasMaqCt = new GridColuna[]
        {
                new GridColuna("Código", "CODIGO", 120, true,DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Nome", "NOME", 180, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("A.", "ATIVO", 35, true, DataGridViewContentAlignment.MiddleCenter),         
        };
        
        public static void GridMontaMaqCt(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasMaqCt, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NOME");
            dgv.AllowUserToResizeColumns = true;
            
        }
    }
}
