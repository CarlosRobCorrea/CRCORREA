using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace CRCorreaBLL
{
    public class clsRequisicao1BLL : SQLFactory<clsRequisicao1Info>
    {
        Int32 idtipodocumento;
        clsMovPecasInfo clsMovPecasInfo = new clsMovPecasInfo();
        clsMovPecasBLL clsMovPecasBLL = new clsMovPecasBLL();

        public clsRequisicao1BLL()
        {
            clsMovPecasBLL = new clsMovPecasBLL();

            idtipodocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select top 1 id from docfiscal where cognome = 'RM'"));
        }

        public override int Incluir(clsRequisicao1Info info, string cnn)
        {
            VerificaInfo(info);

            Int32 result = base.Incluir(info, cnn);

            String data = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select data from requisicao where id = " + clsParser.Int32Parse(info.numero.ToString()) + " ");

            clsMovPecasBLL.MovimentaItem(info.idcodigo,
                                      info.motivo,
                                      info.qtde,
                                      clsParser.DateTimeParse(data),
                                      idtipodocumento,
                                      info.numero,
                                      result,
                                      0,
                                      info.valor,
                                      clsInfo.zusuario,
                                      cnn);
            return result;
        }

        public override int Alterar(clsRequisicao1Info info, string cnn)
        {
            VerificaInfo(info);

            Int32 result;

            result = base.Alterar(info, cnn);

            String data = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select data from requisicao where id = " + clsParser.Int32Parse(info.numero.ToString()) + " ");

            clsMovPecasBLL.MovimentaItem(info.qtde,
                                     info.tipo,
                                     idtipodocumento,
                                     info.numero,
                                     info.id,
                                     0,
                                     info.valor,
                                     info.idcodigo,
                                     clsParser.DateTimeParse(data),
                                     cnn);

            return result;
        }

        public void VerificaInfo(clsRequisicao1Info _info)
        {
            /*
            if (_info.idmaquina == 0)
            {
                throw new Exception("Informe o número da máquina");
            }
            if (_info.idfuncionario == 0)
            {
                throw new Exception("Chapa não encontrada");
            } */           
            if (_info.idcodigo == 0)
            {
                throw new Exception("Informe o código do item");
            }
            if (_info.qtde == 0)
            {
                throw new Exception("Informe uma quantidade");
            }

            if (_info.idunidade == 0)
            {
                throw new Exception("Informe a unidade.");
            }
        }

        public DataTable GridDados(Int32 idrequisicao, String cnn)
        {
            String query;
            SqlDataAdapter sda;
            DataTable dt = new DataTable();
            query = " SELECT REQUISICAO1.ID,REQUISICAO1.NUMERO,REQUISICAO1.TIPO,REQUISICAO1.MOTIVO,REQUISICAO1.IDCODIGO, " +
                    "REQUISICAO1.IDMAQUINA,REQUISICAO1.QTDE,REQUISICAO1.VALOR,REQUISICAO1.OBSERVAR,REQUISICAO1.IDFUNCIONARIO,  " + 
                    "REQUISICAO1.FUNCIONARIO, REQUISICAO1.IDORDEMSERVICO,  " +
                    "REQUISICAO1.IDORDEMSERVICOOP,REQUISICAO1.QTDEDEV,REQUISICAO1.QTDEENTREGA,REQUISICAO1.QTDESALDO, " +
                    "REQUISICAO1.DATARETORNO, REQUISICAO1.DATAENTREGA,REQUISICAO1.IDUNIDADE, " +
                    "PECAS.CODIGO, PECAS.NOME, MAQUINAS.NUMERO AS [MAQNRO], UNIDADE.CODIGO AS [UNID],  " +
                    "ORDEMSERVICO.NUMERO AS [OS], REQUISICAO1.VALOR " +
                    "FROM REQUISICAO1  " +
                    "LEFT JOIN PECAS ON PECAS.ID = REQUISICAO1.IDCODIGO  " +
                    "LEFT JOIN MAQUINAS ON MAQUINAS.ID = REQUISICAO1.IDMAQUINA  " +
                    "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID = REQUISICAO1.IDORDEMSERVICO  " +
                    "LEFT JOIN UNIDADE ON UNIDADE.ID = REQUISICAO1.IDUNIDADE  " +
                    "WHERE " +
                        "REQUISICAO1.NUMERO = @IDREQUISICAO";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@IDREQUISICAO", SqlDbType.Int).Value = idrequisicao;
            sda.Fill(dt);
            return dt;
        }

        public GridColuna[] dtDadosColunas = new GridColuna[]
        {
            new GridColuna("iD", "ID", 1, false, DataGridViewContentAlignment.MiddleCenter), 
            new GridColuna("Tp", "TIPO", 30, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("E/S", "MOTIVO", 30, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qt Ret", "QTDE", 60, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Qt Dev", "QTDEDEV", 60, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Qt Entr", "QTDEENTREGA", 60, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Qt Saldo", "QTDESALDO", 60, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Codigo", "CODIGO", 80, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Nome", "NOME", 320, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Unid", "UNID", 40, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Valor", "VALOR", 90, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Maquina", "MAQNRO", 40, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Funcionario", "FUNCIONARIO", 80, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Observa", "OBSERVAR", 200, true, DataGridViewContentAlignment.MiddleLeft),
             
        };

        public void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            dgv.DataSource = dt;

            
            clsGridHelper.MontaGrid2(dgv, dtDadosColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "CODIGO");
            dgv.Columns["QTDE"].DefaultCellStyle.Format = "N3";
            dgv.Columns["QTDEDEV"].DefaultCellStyle.Format = "N3";
            dgv.Columns["QTDEENTREGA"].DefaultCellStyle.Format = "N3";
            dgv.Columns["QTDESALDO"].DefaultCellStyle.Format = "N3";
            dgv.Columns["VALOR"].DefaultCellStyle.Format = "N4";
        }
        public DataTable GridDadosNFCompraTemp(Int32 idcodigo, String cnn)
        {
            String query;
            SqlDataAdapter sda;
            DataTable dt = new DataTable();

            query = "SELECT * FROM NFCOMPRATEMP1 WHERE IDCODIGO = @IDCODIGO";

            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);

            sda.SelectCommand.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = idcodigo;

            sda.Fill(dt);

            return dt;
        }

        public DataTable GridDadosDespacho(Int32 idos, String cnn)
        {
            String query;
            SqlDataAdapter sda;
            DataTable dt = new DataTable();

            query = "select " +
                        "requisicao.numero, " +
                        "requisicao.data, " +
                        "requisicao.emitente, " +
                        "requisicao1.qtde, " +
                        "requisicao1.valor, " +
                        "pecas.codigo, " +
                        "pecas.nome, " +
                        "unidade.codigo as unidade_codigo " +
                    "from " +
                        "requisicao1 " +
                            "inner join requisicao on requisicao.id = requisicao1.numero " +
                            "inner join ordemservico on ordemservico.id = requisicao1.idordemservico " +
                            "inner join pecas on pecas.id = requisicao1.idcodigo " +
                            "inner join unidade on unidade.id = requisicao1.idunidade " +
                    "where " +
                        "ordemservico.id = @idordemservico ";

            query += "order by " +
                        "requisicao.data desc";

            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);

            sda.SelectCommand.Parameters.Add("@idordemservico", SqlDbType.Int).Value = idos;

            sda.Fill(dt);

            return dt;
        }

        public GridColuna[] dtRequisicao1DespachoColunas = new GridColuna[]
        {
                new GridColuna("Nº", "NUMERO", 60, true, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("Data", "DATA", 120, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Código", "CODIGO", 120, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Descrição", "NOME", 320, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Qtde", "QTDE", 90, true, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("Un.", "UNIDADE_CODIGO", 45, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Valor", "VALOR", 90, true, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("Emitente", "EMITENTE", 120, true, DataGridViewContentAlignment.MiddleLeft)
        };

        public void GridMontaDespacho(DataGridView dgv, DataTable dt)
        {
            dgv.DataSource = dt;

            
            clsGridHelper.MontaGrid2(dgv, dtRequisicao1DespachoColunas, true);

            dgv.Columns["data"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }
    }
}
