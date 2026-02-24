using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CRCorreaBLL
{
    public class clsComprasEntregaValoresBLL : SQLFactory<clsComprasEntregaValoresInfo>
    {
        public override int Incluir(clsComprasEntregaValoresInfo info, string cnn)
        {
            Int32 result = base.Incluir(info, cnn);

            SicronizarPagar(info.idcompras, cnn);

            return result;
        }

        public override int Alterar(clsComprasEntregaValoresInfo info, string cnn)
        {
            Int32 result = base.Alterar(info, cnn);

            SicronizarPagar(info.idcompras, cnn);

            return result;
        }

        public override bool Excluir(int id, string cnn)
        {
            Int32 idcompras;
            

            idcompras = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select idcompras from comprasentregavalores where id=" + id));

            Boolean result = base.Excluir(id, cnn);

            SicronizarPagar(idcompras, cnn);

            return result;
        }

        public void SicronizarPagar(Int32 idcompras, string cnn)
        {
            Int32 iddocumento;

            SqlDataAdapter sda;
            DataTable dtEntregas;
            DataTable dtPagar;
            DateTime dataAtual;
            Dictionary<DateTime, Decimal> dtyDatas;

            List<Int32> pagarComEntrega = new List<int>();

            
            
            iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select id from docfiscal where cognome='PCO'"));

            if (iddocumento == 0 ||
                iddocumento == clsInfo.zdocumento)
            {
                throw new Exception("Favor Incluir documento 'PCO - Pedido de Compra' na tabela de documentos." + Environment.NewLine + "Não foi transferido para o Contas a Receber.");
            }

            clsComprasBLL CompraBLL = new clsComprasBLL();
            clsComprasInfo CompraInfo = CompraBLL.Carregar(idcompras, cnn);

            dtEntregas = new DataTable();

            sda = new SqlDataAdapter("select datavencimento, sum(valorvencimento) as valorvencimento from comprasentregavalores where idcompras=@idcompras group by datavencimento", cnn);
            sda.SelectCommand.Parameters.Add("@idcompras", SqlDbType.Int).Value = idcompras;
            sda.Fill(dtEntregas);

            dtyDatas = new Dictionary<DateTime, decimal>();

            foreach (DataRow row in dtEntregas.Rows)
            {
                dataAtual = clsParser.DateTimeParse((clsParser.DateTimeParse(row["datavencimento"].ToString()).Day) + "/" +
                            (clsParser.DateTimeParse(row["datavencimento"].ToString()).Month) + "/" +
                            (clsParser.DateTimeParse(row["datavencimento"].ToString()).Year) + " 00:00:00");

                // alterei em 02/11/2011 -crc
                /*dataAtual = clsParser.DateTimeParse(row["datavencimento"].ToString());
                dataAtual.AddHours(-dataAtual.Hour);
                dataAtual.AddMinutes(-dataAtual.Minute);
                dataAtual.AddSeconds(-dataAtual.Second);
                dataAtual.AddMilliseconds(-dataAtual.Millisecond); */

                if (dtyDatas.ContainsKey(dataAtual) == true)
                {
                    dtyDatas[dataAtual] = dtyDatas[dataAtual] + clsParser.DecimalParse(row["valorvencimento"].ToString());
                }
                else
                {
                    dtyDatas.Add(dataAtual, clsParser.DecimalParse(row["valorvencimento"].ToString()));
                }
            }

            List<clsSqlFactoryValue> lsPagarFind;
            clsSqlFactoryValue sfvPagarVencimento;
            clsSqlFactoryValue sfvPagarDocfiscal;
            clsSqlFactoryValue sfvPagarDocumento;

            clsPagarBLL PagarBLL = new clsPagarBLL();
            clsPagarInfo PagarInfo = new clsPagarInfo();

            for (Int32 i = 0; i < dtyDatas.Count; i++)
            {
                if (dtyDatas.ElementAt(i).Value > 0)
                {
                    lsPagarFind = new List<clsSqlFactoryValue>();

                    sfvPagarVencimento = new clsSqlFactoryValue();
                    sfvPagarVencimento.Nome = "VENCIMENTO";
                    sfvPagarVencimento.Valor = dtyDatas.ElementAt(i).Key;
                    lsPagarFind.Add(sfvPagarVencimento);

                    sfvPagarDocfiscal = new clsSqlFactoryValue();
                    sfvPagarDocfiscal.Nome = "IDDOCUMENTO";
                    sfvPagarDocfiscal.Valor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select id from docfiscal where cognome='PCO'"));
                    lsPagarFind.Add(sfvPagarDocfiscal);

                    sfvPagarDocumento = new clsSqlFactoryValue();
                    sfvPagarDocumento.Nome = "IDNOTAFISCAL";
                    sfvPagarDocumento.Valor = CompraInfo.id;
                    lsPagarFind.Add(sfvPagarDocumento);

                    PagarInfo = PagarBLL.Carregar(lsPagarFind, cnn);

                    if (PagarInfo == null ||
                        PagarInfo.id == 0)
                    {
                        PagarInfo = new clsPagarInfo();

                        PagarInfo.atevencimento = "S";
                        PagarInfo.baixa = "N";
                        PagarInfo.boleto = "";
                        PagarInfo.boletonro = 0;
                        PagarInfo.chegou = "N";
                        PagarInfo.datalanca = CompraInfo.data;
                        PagarInfo.despesapublica = "N";
                        PagarInfo.duplicata = CompraInfo.numero;
                        PagarInfo.dv = "";
                        PagarInfo.emissao = CompraInfo.data;
                        PagarInfo.emitente = Procedure.PesquisaoPrimeiro(cnn, "select usuario from usuario where id=" + CompraInfo.idemitente);

                        if (PagarInfo.emitente == null) PagarInfo.emitente = "";

                        PagarInfo.filial = CompraInfo.filial;
                        PagarInfo.id = 0;
                        PagarInfo.idbanco = clsInfo.zbanco;
                        PagarInfo.idbancoint = clsInfo.zbancoint;
                        PagarInfo.idcentrocusto = clsInfo.zcentrocustos;
                        PagarInfo.idcodigoctabil = clsInfo.zcontacontabil;
                        PagarInfo.iddocumento = iddocumento;
                        PagarInfo.idformapagto = CompraInfo.idformapagto;
                        PagarInfo.idfornecedor = CompraInfo.idfornecedor;
                        PagarInfo.idhistorico = clsInfo.zhistoricos;
                        PagarInfo.idnotafiscal = CompraInfo.id;
                        PagarInfo.idpagarnfe = clsInfo.znfcomprapagar;
                        PagarInfo.idsitbanco = clsInfo.zsituacaotitulo;
                        PagarInfo.imprimir = "N";
                        PagarInfo.observa = "PEDIDO DE COMPRA";
                        PagarInfo.posicao = 1;
                        PagarInfo.posicaofim = 1;
                        PagarInfo.setor = "N";
                        PagarInfo.valorbaixando = 0;
                        PagarInfo.valordesconto = 0;
                        PagarInfo.valordevolvido = 0;
                        PagarInfo.valorjuros = 0;
                        PagarInfo.valormulta = 0;
                        PagarInfo.valorpago = 0;
                    }

                    PagarInfo.valor = dtyDatas.ElementAt(i).Value;
                    PagarInfo.valorliquido = dtyDatas.ElementAt(i).Value;
                    PagarInfo.vencimento = dtyDatas.ElementAt(i).Key;
                    //alterei em 02/11/2011 crc
                    if (PagarInfo.vencimento < DateTime.Now)
                    {
                        PagarInfo.vencimento = (PagarInfo.vencimento.AddDays(DateTime.Now.Subtract(PagarInfo.vencimento).Days)).AddDays(1);
                    }
                    PagarInfo.vencimentoprev = PagarInfo.vencimento;
                    //PagarInfo.vencimentoprev = dtyDatas.ElementAt(i).Key;

                    if (PagarInfo == null ||
                        PagarInfo.id == 0)
                    {
                        PagarInfo.id = PagarBLL.Incluir(PagarInfo, cnn);
                    }
                    else
                    {
                        PagarBLL.Alterar(PagarInfo, cnn);
                    }

                    pagarComEntrega.Add(PagarInfo.id);
                }
            }

            // APAGA OS LANÇAMENTOS NÃO UTILIZADOS
            dtPagar = new DataTable();

            sda = new SqlDataAdapter("select id from pagar where idnotafiscal=@idnotafiscal and iddocumento=@iddocumento order by VENCIMENTO", cnn);
            sda.SelectCommand.Parameters.Add("@idnotafiscal", SqlDbType.Int).Value = CompraInfo.id;
            sda.SelectCommand.Parameters.Add("@iddocumento", SqlDbType.Int).Value = iddocumento;
            sda.Fill(dtPagar);

            Int32 pagar_posicao = 1;
            Int32 pagar_posicaofim = 0;

            Int32 lastIndex = -1;

            foreach (DataRow row in dtPagar.Rows)
            {
                lastIndex = pagarComEntrega.IndexOf(clsParser.Int32Parse(row["id"].ToString()));

                if (lastIndex != -1)
                {
                    pagarComEntrega.RemoveAt(lastIndex);

                    pagar_posicaofim++;
                }
                else
                {
                    PagarBLL.Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
                }
            }

            dtPagar = new DataTable();
            sda = new SqlDataAdapter("select id from pagar where idnotafiscal=@idnotafiscal and iddocumento=@iddocumento order by VENCIMENTO", cnn);
            sda.SelectCommand.Parameters.Add("@idnotafiscal", SqlDbType.Int).Value = CompraInfo.id;
            sda.SelectCommand.Parameters.Add("@iddocumento", SqlDbType.Int).Value = iddocumento;
            sda.Fill(dtPagar);

            foreach (DataRow row in dtPagar.Rows)
            {
                PagarInfo = PagarBLL.Carregar(clsParser.Int32Parse(row["id"].ToString()), cnn);

                PagarInfo.posicao = pagar_posicao;
                PagarInfo.posicaofim = pagar_posicaofim;

                PagarBLL.Alterar(PagarInfo, cnn);

                pagar_posicao++;
            }
        }

        public void ExcluirComprasEntrega(Int32 idcomprasentrega, String cnn)
        {
            //SqlConnection scn;
            //SqlCommand scd;
            //SqlDataReader sdr;

            //scn = new SqlConnection(cnn);
            //scd = new SqlCommand("select id from comprasentregavalores where idcomprasentrega=@idcomprasentrega", scn);
            //scd.Parameters.Add("@idcomprasentrega", SqlDbType.Int).Value = idcomprasentrega;

            //scn.Open();

            //sdr = scd.ExecuteReader();
            //while (sdr.Read() == true)
            //{
            //    Excluir(clsParser.Int32Parse(sdr["id"].ToString()), cnn);
            //}
            //scn.Close();
            DataTable comprasEntregaValores = Procedure.RetornaDataTable(clsInfo.conexaosqldados, "select id from comprasentregavalores where idcomprasentrega=" + idcomprasentrega);

            foreach (DataRow item in comprasEntregaValores.Rows)
            {
                Excluir(clsParser.Int32Parse(item["id"].ToString()), cnn);
            }

        }
    }
}
