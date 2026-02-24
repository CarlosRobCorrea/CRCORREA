using System;
using System.Data;

namespace CRCorreaFuncoes
{ 
    public class clsProgramacaoEntrega
    {
        public enum Periodo
        {
            Semana,
            Mes,
            Trimestre,
            Semestre,
            Quinzena,
            Anual
        }

        public DataTable GerarProgramacao(Decimal qtdeprogramacao,
                                            Decimal valortotal,
                                            Int32 qtdeentregas,
                                            Periodo p,
                                            DateTime primeiraEntrega,
                                            Int32 idunidade,
                                            Boolean somente_dias_uteis)
        {
            String unidade = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where id=" + idunidade);

            if (unidade == null || unidade.Trim() == "")
            {
                throw new Exception("Unidade da Programação de Entrega inválida.");
            }

            unidade = unidade.Trim().ToUpper();

            if (qtdeprogramacao <= 0)
            {
                throw new Exception("Qtde Programada da Programação de Entrega inválida.");
            }

            if (qtdeentregas <= 0)
            {
                throw new Exception("Qtde Entregas da Programação de Entrega inválida.");
            }

            if (primeiraEntrega == null)
            {
                throw new Exception("Data indicada para a Primeira Entrega é inválida.");
            }
                  

            Int32 iAux;
            DateTime dataAux;
            DateTime dataEntrega = primeiraEntrega;
            Decimal qtdePorEntrega = 0;
            Decimal precoPorEntrega = 0;
            Decimal totalgeral = 0;
            Decimal qtdetotalgeral = 0;

            DataTable dtProgramacao = new DataTable();

            DataColumn dcEntrega = new DataColumn("entrega", Type.GetType("System.DateTime"));
            DataColumn dcQtde = new DataColumn("qtde", Type.GetType("System.Decimal"));
            DataColumn dcValor = new DataColumn("preco", Type.GetType("System.Decimal"));
            DataColumn dcTotal = new DataColumn("total", Type.GetType("System.Decimal"));

            dtProgramacao.Columns.Add(dcEntrega);
            dtProgramacao.Columns.Add(dcQtde);
            dtProgramacao.Columns.Add(dcValor);
            dtProgramacao.Columns.Add(dcTotal);

            if (unidade == "PC" ||
                unidade == "UN" ||
                unidade == "TB" ||
                unidade == "CX" ||
                unidade == "GL" ||
                unidade == "BL" ||
                unidade == "MQ" ||
                unidade == "JG" ||
                unidade == "SC")
            {
                qtdePorEntrega = Decimal.Round(Decimal.Divide(qtdeprogramacao, qtdeentregas), 0);
            }
            else
            {
                qtdePorEntrega = Decimal.Parse(Decimal.Divide(qtdeprogramacao, qtdeentregas).ToString("N3"));
            }

            precoPorEntrega = Decimal.Parse(Decimal.Divide(valortotal, qtdeprogramacao).ToString("N3"));

            for (Int32 i = 0; i < qtdeentregas; i++)
            {
                iAux = 0;
                dataAux = dataEntrega;

                if (i == 0)
                {
                    if (somente_dias_uteis == true)
                    {
                        while (clsVisual.IsHoliday(dataEntrega) == true ||
                                dataEntrega.DayOfWeek == DayOfWeek.Saturday ||
                                dataEntrega.DayOfWeek == DayOfWeek.Sunday)
                        {
                            dataEntrega = dataEntrega.AddDays(1);
                        }

                        dataAux = dataEntrega;
                    }
                }
                else
                {
                    if (p == Periodo.Anual)
                    {
                        dataEntrega = dataEntrega.AddYears(i);
                    }
                    else if (p == Periodo.Semestre)
                    {
                        dataEntrega = dataEntrega.AddMonths(6);
                    }
                    else if (p == Periodo.Trimestre)
                    {
                        dataEntrega = dataEntrega.AddMonths(3);
                    }
                    else if (p == Periodo.Mes)
                    {
                        dataEntrega = dataEntrega.AddMonths(1);
                    }
                    else if (p == Periodo.Quinzena)
                    {
                        dataEntrega = dataEntrega.AddDays(15);
                    }
                    else if (p == Periodo.Semana)
                    {
                        dataEntrega = dataEntrega.AddDays(7);
                    }
                }

                if (somente_dias_uteis == true)
                {
                    do
                    {
                        iAux = clsVisual.GetDiffDays(dataAux, dataEntrega);

                        dataAux = dataEntrega;
                        dataEntrega = dataEntrega.AddDays(iAux);

                        iAux = clsVisual.GetDiffDays(dataAux, dataEntrega);
                    }
                    while (iAux > 0);
                }

                DataRow row = dtProgramacao.NewRow();

                row["entrega"] = dataEntrega;             
                row["qtde"] = qtdePorEntrega;
                row["preco"] = precoPorEntrega;
                row["total"] = clsVisual.Truncar(qtdePorEntrega * precoPorEntrega, 2);
               
                totalgeral += clsParser.DecimalParse(row["total"].ToString());
                qtdetotalgeral += clsParser.DecimalParse(row["qtde"].ToString());

                dtProgramacao.Rows.Add(row);
            }

            if (valortotal != totalgeral)
            {
                dtProgramacao.Rows[0]["total"] = clsParser.DecimalParse(dtProgramacao.Rows[0]["total"].ToString()) + (valortotal - totalgeral);
            }

            if (qtdetotalgeral != qtdeprogramacao)
            {
                dtProgramacao.Rows[0]["qtde"] = clsParser.DecimalParse(dtProgramacao.Rows[0]["qtde"].ToString()) + (qtdeprogramacao - qtdetotalgeral);
            }

            return dtProgramacao;
        }
    }
}
