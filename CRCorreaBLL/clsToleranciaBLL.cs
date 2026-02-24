using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Windows.Forms;
using System.Drawing;

using CRCorreaInfo;

using CRCorreaFuncoes;


namespace CRCorreaBLL
{ 
    public class clsToleranciaBLL : SQLFactory<clsToleranciaInfo>
    {

        public void VerificaInfo(clsToleranciaInfo info)
        {
            if (info.codigo.ToString().Length == 0)
            {
                throw new Exception("Material Não pode ser Castrado Sem Código");
            }
        }

        public override int Incluir(clsToleranciaInfo info, string cnn)
        {
            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsToleranciaInfo info, String cnn)
        {
            VerificaInfo(info);

            return base.Alterar(info, cnn);
        }

        public DataTable GridDados(String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;
            query = "SELECT " +
                        "TOLERANCIA.ID, " +
                        "TOLERANCIA.CODIGO, " +
                        "TOLERANCIA.TIPO, " +
                        "TOLERANCIA.NOME, " +
                        "TOLERANCIA.NORMA " +
                    "FROM " +
                        "TOLERANCIA WHERE TOLERANCIA.CODIGO <> '' ORDER BY TOLERANCIA.CODIGO ";
            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            //            sda.SelectCommand.Parameters.Add("@FILIAL", SqlDbType.Int).Value = filial;
            //            sda.SelectCommand.Parameters.Add("@ANO", SqlDbType.Int).Value = ano;
            sda.Fill(dt);
            return dt;
        }

        public GridColuna[] dtDadosColunas = new GridColuna[]
        {
            new GridColuna("iD", "ID", 1, false, DataGridViewContentAlignment.MiddleCenter), 
            new GridColuna("Codigo", "CODIGO", 175, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Tipo", "TIPO", 155, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "NOME", 470, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Norma", "NORMA", 150, true, DataGridViewContentAlignment.MiddleLeft)
        };

        public void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            dgv.DataSource = dt;
            
            clsGridHelper.MontaGrid2(dgv, dtDadosColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "CODIGO");
        }

    }
}
