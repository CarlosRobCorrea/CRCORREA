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
    public class clsPecasCadastroBLL : SQLFactory<clsPecasCadastroInfo>
    {
        public override Int32 Incluir(clsPecasCadastroInfo info, String cnn) {
            VerificaInfo(info);
            return base.Incluir(info, cnn);
        }

        public override Int32 Alterar(clsPecasCadastroInfo info, String cnn)
        {
            VerificaInfo(info);
            return base.Alterar(info, cnn);
        }

        public void VerificaInfo(clsPecasCadastroInfo info){
            if (String.IsNullOrEmpty(info.codigo)) 
            {
                throw new Exception("código não pode ficar em branco");
            }
            else if (info.codigo.Length > 30 || info.codigo.Length < 1) {
                throw new Exception("Código não pode ficar em branco.");
            }
        }
        public static DataTable GridDados(String ativo, String tipo, String cnn)
        {
            DataTable dt;
            String query = "";
            String filtro = "";
            SqlDataAdapter sda;

            query = "SELECT " +
            "PECAS.ID,PECAS.FOTO,PECAS.CODIGO, PECAS.CODIGOBARRA," +
            "CASE WHEN PECAS.ATIVO = 'S' THEN 'S' WHEN PECAS.ATIVO = 'N' THEN 'N' ELSE 'N' END AS ATIVO, " +
            "PECAS.NOME, " + //PECAS.QTDESALDO," +
            "((PECAS.qtdeinicio + PECAS.qtdeentra) - PECAS.qtdesaida) AS [QTDESALDO], " +
            "UNIDADE.CODIGO AS [UNID],PECAS.PRECOVENDA, PECASCLASSIFICA.CODIGO AS [GRUPO], PECAS.CADASTRADO, PECASTIPO.CODIGO AS [MARCA] " + //PECAS.DATAPRECO " +
            //"(select top 1 saldo from movpecas where idcodigo = pecas.id order by data desc, id desc)as qtdesaldo " + 
            "FROM PECAS " +
            "LEFT JOIN UNIDADE ON UNIDADE.ID = PECAS.IDUNIDADE " +
            "LEFT JOIN PECASCLASSIFICA ON PECASCLASSIFICA.ID = PECAS.IDCLASSIFICA " +
            "LEFT JOIN PECASTIPO ON PECASTIPO.ID = PECAS.IDMARCA "; 

            if (ativo == "S")
            {
                filtro = "PECAS.ATIVO = 'S'";
            }
            else if (ativo == "N")
            {
                filtro = "PECAS.ATIVO = 'N'";
            }
            else if (ativo == "R")
            {
                filtro = "PECAS.ATIVO = 'R'";
            }
            if (filtro.Length > 2)
            {
                filtro += "AND ";
            }
            filtro += " PECAS.CODIGO > '0' ";

            if (filtro.Length > 5)
            {
                filtro = "WHERE " + filtro;
            }
            query = query + " " + filtro + " ORDER BY PECAS.NOME ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            dt = new DataTable();
            sda.Fill(dt);
            return dt;


        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("id", "ID", 0, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Codigo", "CODIGO", 130, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Cod.Barra", "CODIGOBARRA", 110, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("A", "ATIVO", 21, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Descrição", "NOME", 450, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qt.Saldo", "QTDESALDO", 80, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Unid", "UNID", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Preco Venda", "PRECOVENDA", 95, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Grupo", "GRUPO", 55, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Dt. Valid", "DATAPRECO", 70, true, DataGridViewContentAlignment.MiddleCenter),
           new GridColuna("Dt.Cadast", "CADASTRADO", 70, true, DataGridViewContentAlignment.MiddleCenter),
           
        };

        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 8, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "CODIGO");
            dgv.Columns["QTDESALDO"].DefaultCellStyle.Format = "N3";
            dgv.Columns["PRECOVENDA"].DefaultCellStyle.Format = "N4";
            //dgv.Columns["BASEMP"].DefaultCellStyle.Format = "N0";
            dgv.Columns["DATAPRECO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns["CADASTRADO"].DefaultCellStyle.Format = "dd/MM/yyyy";
            
        }

        public static void ConfigurarGrade(DataGridView dgv, DataTable dt, Int32 id)
        {
            dgv.DataSource = dt;
            dgv.DefaultCellStyle.Font = new Font("Tahoma", 8);
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

            dgv.Columns["ID"].HeaderText = "ID";
            dgv.Columns["ID"].Width = 0;
            dgv.Columns["ID"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["ID"].Visible = false;

            dgv.Columns["FOTO"].HeaderText = "Foto";
            dgv.Columns["FOTO"].Width = 60;
            dgv.Columns["FOTO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["FOTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dgv.Columns["CODIGO"].HeaderText = "Codigo";
            dgv.Columns["CODIGO"].Width = 100;
            dgv.Columns["CODIGO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns["CODIGOBARRA"].HeaderText = "Codigo Barra";
            dgv.Columns["CODIGOBARRA"].Width = 100;
            dgv.Columns["CODIGOBARRA"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["CODIGOBARRA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns["ATIVO"].HeaderText = "A";
            dgv.Columns["ATIVO"].Width = 21;
            dgv.Columns["ATIVO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["ATIVO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["NOME"].HeaderText = "Nome Material";
            dgv.Columns["NOME"].Width = 320;
            dgv.Columns["NOME"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["NOME"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns["QTDESALDO"].HeaderText = "Qt.Saldo";
            dgv.Columns["QTDESALDO"].Width = 80;
            dgv.Columns["QTDESALDO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["QTDESALDO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["QTDESALDO"].DefaultCellStyle.Format = "N3";

            dgv.Columns["UNID"].HeaderText = "Un";
            dgv.Columns["UNID"].Width = 30;
            dgv.Columns["UNID"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["UNID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["PRECOVENDA"].HeaderText = "Preco Venda";
            dgv.Columns["PRECOVENDA"].Width = 95;
            dgv.Columns["PRECOVENDA"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["PRECOVENDA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["PRECOVENDA"].DefaultCellStyle.Format = "N3";

            dgv.Columns["GRUPO"].HeaderText = "Grupo";
            dgv.Columns["GRUPO"].Width = 85;
            dgv.Columns["GRUPO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["GRUPO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns["MARCA"].HeaderText = "Marca";
            dgv.Columns["MARCA"].Width = 70;
            dgv.Columns["MARCA"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["MARCA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

        }



    }
}
