using CRCorreaBLL;
using CRCorreaInfo;
using CRCorreaFuncoes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsRequisicaoBLL : SQLFactory<clsRequisicaoInfo>
    {
        public override int Incluir(clsRequisicaoInfo info, string cnn)
        {
            info.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT TOP 1 NUMERO + 1 FROM REQUISICAO WHERE YEAR(DATA) = YEAR(GETDATE()) ORDER BY NUMERO DESC"));

            if (info.numero == 0)
            {
                info.numero = 1;
            }

            if (info.data == null || info.data == DateTime.MinValue)
            {
                info.data = DateTime.Now;
            }

            if (info.ano <= 0)
            {
                info.ano = info.data.Year;
            }

            return base.Incluir(info, cnn);
        }

        public void FontGrid(DataGridView _grid)
        {
            _grid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            _grid.ForeColor = System.Drawing.Color.Black;
        }

        public DataTable GridDados(Int32 filial, Int32 ano, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;
            query = "SELECT " +
                        "REQUISICAO.ID, " +
                        "REQUISICAO.FILIAL, " +
                        "REQUISICAO.NUMERO, " +
                        "REQUISICAO.DATA, " +
                        "REQUISICAO.EMITENTE " +
                    "FROM " +
                        "REQUISICAO ";

            if (filial > 0)
            {
                query += "WHERE " +
                            " REQUISICAO.FILIAL = @FILIAL ";
            }

            if (ano > 0)
            {
                if (filial > 0)
                {
                    query += " AND " +
                             "YEAR(REQUISICAO.DATA) = @ANO ";
                }
                else
                {
                     query += " WHERE " +
                             "YEAR(REQUISICAO.DATA) = @ANO ";
                }
            }
            query += " ORDER BY REQUISICAO.ID DESC, REQUISICAO.DATA DESC ";



            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);

            if (filial > 0)
            {
                sda.SelectCommand.Parameters.Add("@FILIAL", SqlDbType.Int).Value = filial;
            }
            if (ano > 0)
            {
                sda.SelectCommand.Parameters.Add("@ANO", SqlDbType.Int).Value = ano;
            }
            
            sda.Fill(dt);

            return dt;
        }

        public GridColuna[] dtDadosColunas = new GridColuna[]
        {
            new GridColuna("iD", "ID", 1, false, DataGridViewContentAlignment.MiddleCenter), 
            new GridColuna("F.", "FILIAL", 25, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("NºReq", "NUMERO", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Emissão", "DATA", 70, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Emitente", "EMITENTE", 120, true, DataGridViewContentAlignment.MiddleLeft)
        };

        public void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            dgv.DataSource = dt;

            
            clsGridHelper.MontaGrid2(dgv, dtDadosColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";


        }

        public DataTable GridDadosEpi(Int32 idfuncionario, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;
            query = "SELECT " +
                        "requisicao.id as id, " +
                        "requisicao.numero as numero, " +
                        "requisicao.ano as ano, " +
                        "requisicao.data as data, " +
                        "requisicao.emitente as emitente, " +
                        "requisicao1.tipo as tipo, " +
                        "requisicao1.motivo as motivo, " +
                        "requisicao1.qtde as qtde, " +
                        "requisicao1.qtdedev as qtdedev, " +
                        "requisicao1.qtdesaldo as qtdesaldo, " +
                        "requisicao1.valor as valor, " +
                        "requisicao1.dataentrega as dataentrega, " +
                        "requisicao1.dataretorno as dataretorno, " +
                        "pecas.codigo as codigo, " +
                        "pecas.nome as nomepeca " +
                    "FROM REQUISICAO " +
                        "inner join requisicao1 on requisicao.id = requisicao1.numero " +
                        "inner join  pecas on pecas.id = requisicao1.idcodigo " +
                        "inner join pecasclassifica2 on pecas.idclassifica2 = pecasclassifica2.id " +
                    "where " +
                        "requisicao1.idfuncionario = @IDFUNCIONARIO and " +
                        "pecasclassifica2.codigo='EPI' ";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDFUNCIONARIO", SqlDbType.Int).Value = idfuncionario;
            sda.Fill(dt);

            return dt;
        }

        public GridColuna[] dtDadosEpiColunas = new GridColuna[]
        {
            new GridColuna("Nº", "NUMERO", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Ano", "ANO", 50, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Data", "DATA", 50, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Emitente", "EMITENTE", 50, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Tipo", "TIPO", 50, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Motivo", "MOTIVO", 50, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qtde", "QTDE", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Dev.", "QTDEDEV", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Saldo", "QTDESALDO", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Valor", "VALOR", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Entrega", "DATAENTREGA", 50, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Retorno", "DATARETORNO", 50, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Código", "CODIGO", 50, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Descrição", "NOMEPECA", 50, true, DataGridViewContentAlignment.MiddleLeft)
        };

        public void GridMontaEpi(DataGridView dgv, DataTable dt)
        {
            dgv.DataSource = dt;

            
            clsGridHelper.MontaGrid2(dgv, dtDadosColunas, true);
        }
    }
}
