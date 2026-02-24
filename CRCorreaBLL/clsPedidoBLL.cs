using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CRCorreaBLL
{
    public class clsPedidoBLL : SQLFactory<clsPedidoInfo>
    {
        clsPedido1BLL clsPedido1BLL;
        clsPedidoInfo clsPedidoInfo;
        clsPedido1Info clsPedido1Info;
        public override Int32 Incluir(clsPedidoInfo info, String cnn)
        {
            VerificaInfo(info);

            SqlConnection scn = new SqlConnection(cnn);
            SqlCommand scd = new SqlCommand("SELECT TOP 1 NUMERO FROM PEDIDO WHERE YEAR(DATA) = YEAR(GETDATE()) ORDER BY NUMERO DESC", scn);
            SqlDataReader sdr;

            //scd.Parameters.Add("@DATA", SqlDbType.DateTime).Value = info.data;

            scn.Open();
            sdr = scd.ExecuteReader();
            if (sdr.Read() == true)
            {
                info.numero = clsParser.Int32Parse(sdr[0].ToString()) + 1;
            }
            else
            {
                info.numero = 1;
            }
            scn.Close();

            //return base.Incluir(info, cnn);

            info.id = base.Incluir(info, cnn);

            return info.id;
        }

        public override int Alterar(clsPedidoInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Alterar(info, cnn);
        }
        public void VerificaInfo(clsPedidoInfo info)
        {
            if (info.idcliente <= 0 && info.numero != 0)
            {
                throw new Exception("Cliente deve ser escolhido.");
            }
            else if (info.idvendedor <= 0 && info.numero != 0)
            {
                throw new Exception("Representante não pode ficar em branco.");
            }
            else if (info.idtransportadora <= 0 && info.numero != 0)
            {
                throw new Exception("Transportadora não pode ficar em branco.");
            }
            else if (info.idformapagto <= 0 && info.numero != 0)
            {
                throw new Exception("Forma de Pagamento não pode ficar em branco.");
            }
            else if (info.idcondpagto <= 0 && info.numero != 0)
            {
                throw new Exception("Condição de Pagamento não pode ficar em branco.");
            }
        }

        public void AtualizaPedido(Int32 idpedido, String cnn)
        {
            DataTable dtPedido1 = new DataTable();
            DataTable dtPedido2 = new DataTable();

            clsPedidoInfo = new clsPedidoInfo();
            clsPedido1Info = new clsPedido1Info();
            clsPedido1BLL = new clsPedido1BLL();

            Decimal qtdebaixada = 0;
            Decimal qtdedefeito = 0;
            Decimal qtdeentregue = 0;
            Decimal qtdeosaux = 0;
            Decimal qtdepedido = 0;
            Decimal qtdesaldo = 0;
            Decimal qtdesucata = 0;
            Decimal totalpedidoliquido = 0;
            Decimal totalbaseicm = 0;
            Decimal totalicm = 0;
            Decimal totalipi = 0;
            Decimal totalpedido = 0;
            Decimal totalprevisto = 0;

            // Carregar o Pedido de Venda
            clsPedidoInfo = Carregar(idpedido, clsInfo.conexaosqldados);

            SqlDataAdapter sda = new SqlDataAdapter("select * from pedido1 where idpedido= @idpedido", cnn);
            sda.SelectCommand.Parameters.Add("@idpedido", SqlDbType.Int).Value = idpedido;
            sda.Fill(dtPedido1);
            foreach (DataRow row in dtPedido1.Rows)
            {
                clsPedido1Info = clsPedido1BLL.Carregar(clsParser.Int32Parse(row["ID"].ToString()), clsInfo.conexaosqldados);

                totalpedidoliquido = totalpedidoliquido + clsParser.DecimalParse(row["TOTALMERCADO"].ToString());
                totalbaseicm = totalbaseicm + clsParser.DecimalParse(row["BASEICM"].ToString());
                totalicm = totalicm + clsParser.DecimalParse(row["CUSTOICM"].ToString());
                totalipi = totalipi + clsParser.DecimalParse(row["CUSTOIPI"].ToString());
                totalpedido = totalpedido + clsParser.DecimalParse(row["TOTALNOTA"].ToString());

                qtdepedido = qtdepedido + clsPedido1Info.qtde;
                qtdebaixada = qtdebaixada + clsPedido1Info.qtdebaixada;
                qtdedefeito = qtdedefeito + clsPedido1Info.qtdedefeito;
                qtdeentregue = qtdeentregue + clsPedido1Info.qtdeentregue;
                qtdeosaux = qtdeosaux + clsPedido1Info.qtdeosaux;
                qtdesaldo = qtdesaldo + clsPedido1Info.qtdesaldo;
                qtdesucata = qtdesucata + clsPedido1Info.qtdesucata;
                totalprevisto = totalprevisto + (clsPedido1Info.preco * clsPedido1Info.qtdesaldo);
            }
            clsPedidoInfo.qtdepedido = qtdepedido;
            clsPedidoInfo.qtdebaixada = qtdebaixada;
            clsPedidoInfo.qtdedefeito = qtdedefeito;
            clsPedidoInfo.qtdeentregue = qtdedefeito;
            clsPedidoInfo.qtdeosaux = qtdeosaux;
            clsPedidoInfo.qtdesaldo = qtdesaldo;
            clsPedidoInfo.qtdesucata = qtdesucata;
//            clsPedidoInfo.totalpedidoliquido = totalpedidoliquido;
            clsPedidoInfo.totalbaseicm = totalbaseicm;
            clsPedidoInfo.totalicm = totalicm;
            clsPedidoInfo.totalipi = totalipi;
            clsPedidoInfo.totalpedido = totalpedido;
            clsPedidoInfo.totalprevisto = totalprevisto;

            Alterar(clsPedidoInfo, clsInfo.conexaosqldados);
        }
        public override bool Excluir(int id, string cnn)
        {
            DataTable dtReceber = new DataTable();
            DataTable dtPedidoReceber = new DataTable();
            DataTable dtPedido1 = new DataTable();

            String query;
            Int32 idtipodocumento;
            SqlDataAdapter sda;

            //idtipodocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select iddocumento from nfcompra where id=" + id));

            query = "select id from receber where idnotafiscal=" + id;  //+ " and iddocumento=" + idtipodocumento;
            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dtReceber);

            query = "select id, pagou from pedidoreceber where idnota=" + id;
            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dtPedidoReceber);

            query = "select id, idordemservico from pedido1 where idpedido=" + id;
            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dtPedido1);

            // Verifica se já houve alguma parcela paga
            foreach (DataRow row in dtPedidoReceber.Rows)
            {
                if (row["pagou"].ToString() == "S")
                {
                    throw new Exception("Já houve parcelas pagas. Não é possível excluir NF.");
                }
            }

            // Verifica se algum item está ligado a uma Ordem de Serviço
            Int32 idos;
            foreach (DataRow row in dtPedido1.Rows)
            {
                idos = clsParser.Int32Parse(row["idordemservico"].ToString());
                if (idos != 0 && idos != clsInfo.zordemservico)
                {
                    throw new Exception("Existe item(ns) da NF ligados a Ordem(ns) de Serviço.");
                }
            }

            clsReceberBLL ReceberBLL = new clsReceberBLL();

            foreach (DataRow row in dtReceber.Rows)
            {
                ReceberBLL.Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
            }

            clsPedidoReceberBLL pedidoReceberBLL = new clsPedidoReceberBLL();

            foreach (DataRow row in dtPedidoReceber.Rows)
            {
                pedidoReceberBLL.Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
            }

            clsPedido1BLL pedido1BLL = new clsPedido1BLL();

            foreach (DataRow row in dtPedido1.Rows)
            {
                pedido1BLL.Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
            }

            return base.Excluir(id, cnn);
        }

        /*
        public DataTable CarregaGrid(String _conexao, Int32 _ano_ini, Int32 _ano_fim)
        {
            clsPedidoDAL clsPedidoDAL = new clsPedidoDAL();
            return clsPedidoDAL.CarregaGrid(_conexao, _ano_ini, _ano_fim);
        }

        public DataTable CarregaGrid2(String _conexao, Int32 _ano_ini, Int32 _ano_fim)
        {
            clsPedidoDAL clsPedidoDAL = new clsPedidoDAL();
            return clsPedidoDAL.CarregaGrid2(_conexao, _ano_ini, _ano_fim);
        }

        public DataTable CarregaGrid3(String _conexao, Int32 _ano_ini, Int32 _ano_fim)
        {
            clsPedidoDAL clsPedidoDAL = new clsPedidoDAL();
            return clsPedidoDAL.CarregaGrid3(_conexao, _ano_ini, _ano_fim);
        }
         */
    }
}
