using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

using CRCorreaInfo;
using CRCorreaFuncoes;

namespace CRCorreaBLL
{
    public class clsIpiBLL : SQLFactory<clsIpiInfo>
    {
/*
        public override int Incluir(clsIpiInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsIpiInfo info, string cnn)
        {
            VerificaInfo(info);
            return base.Alterar(info, cnn);
        }
*/
        public void VerificaInfo(clsIpiInfo info)
        {

        }

        public DataTable GridDados(String ativo, String tipo, String cnn)
        {
            String query;
            String filtro="";

            DataTable dte = new DataTable();
            query = "SELECT " +
                        "id, " +
                        "CODIGO, " +
                        "NOME, " +
                        "ALIQUOTA, " +
                        "TIPO, " +
                        "ATIVO, " +
                        "PISPASEP, " +
                        "COFINS " +
                    "FROM " +
                        "IPI "
                        ;

            if (ativo == "N")
            {
                filtro = " WHERE ativo = 'N' ";
            }
            else if (ativo == "S")
            {
                filtro = " WHERE ativo = 'S' ";
            }
            if (tipo == "S" || tipo == "T")
            {
                if (filtro.Length > 2)
                {
                    filtro += " AND ";
                }
                else
                {
                    filtro = " WHERE ";
                }
                if (tipo == "S")
                {
                    filtro += " TIPO = 'S'";
                }
                else
                {
                    filtro += " TIPO = 'T'";
                }
            }
            if (filtro.Length > 2)
            {
                query += filtro;
            }
            query += " Order by codigo ";
            SqlDataAdapter sda = new SqlDataAdapter(query, cnn);
            //sda.SelectCommand.Parameters.Add("@ativo ", SqlDbType.NVarChar).Value = ativo;
            //sda.SelectCommand.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = tipo;
            sda.Fill(dte);

            return dte;
        }
        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Cod.NBM", "CODIGO", 160, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "NOME", 550, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna(" % ", "ALIQUOTA", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("T", "TIPO", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("A", "ATIVO", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Pis/Pasep", "PISPASEP", 70, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Cofins", "COFINS", 70, true, DataGridViewContentAlignment.MiddleCenter)
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 8, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "CODIGO");
            dgv.Columns["ALIQUOTA"].DefaultCellStyle.Format = "N2";
        }



    }
}
