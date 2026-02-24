using CRCorreaFuncoes;
using CRCorreaInfo;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsCadClienteBLL : SQLFactory<clsCadClienteInfo>
    {
        public clsCadClienteInfo Carregar(String cgc, String cnn)
        {
            List<clsSqlFactoryValue> parametros = new List<clsSqlFactoryValue>();
            clsSqlFactoryValue parametroCGC = new clsSqlFactoryValue();
            parametroCGC.Nome = "CGC";
            parametroCGC.Valor = cgc;

            parametros.Add(parametroCGC);

            return Carregar(parametros, cnn);
        }

        public clsCadClienteInfo CarregarNumero(Int32 numero, String cnn)
        {
            List<clsSqlFactoryValue> parametros = new List<clsSqlFactoryValue>();
            clsSqlFactoryValue parametroNumero = new clsSqlFactoryValue();
            parametroNumero.Nome = "NUMERO";
            parametroNumero.Valor = numero;

            parametros.Add(parametroNumero);

            return Carregar(parametros, cnn);
        }

        public override Int32 Incluir(clsCadClienteInfo info, String cnn)
        {
            VerificaInfo(info);

            info.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "SELECT top 1 MAX(NUMERO) + 1 FROM CLIENTE group by NUMERO ORDER BY NUMERO DESC"));

            if (info.numero == 0)
            {
                info.numero = 1;
            }

            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsCadClienteInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Alterar(info, cnn);
        }

        public override Boolean Excluir(Int32 id, String cnn)
        {
            Boolean result= false;

            using (TransactionScope tse = new TransactionScope())
            {
                result = base.Excluir(id, cnn);

                if (result == true)
                {
                    tse.Complete();
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public void VerificaInfo(clsCadClienteInfo info)
        {
            if (info.cognome == "")
            {
                throw new Exception("Falta Cognome.");
            }

            if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where id!=" + info.id + " and UPPER(cognome)='" + info.cognome.ToUpper() + "'")) > 0)
            {
                throw new Exception("Já existe um Cliente cadastrado com esse Cognome.");
            }

            info.cgc = info.cgc.Replace(".", "").Replace("/", "").Replace("-", "").Replace(" ", "");
            String cgc = info.cgc;
            if (cgc != "")
            {
                if (info.pessoa == "J")
                {
                    if (clsVisual.ValidaCnpj(cgc) == false)
                    {
                        throw new Exception("CNPJ inválido.");
                    }

                    if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where id!=" + info.id + " and cgc='" + cgc + "'")) > 0)
                    {
                        throw new Exception("Já existe um Cliente cadastrado com esse CNPJ/CPF.");
                    }
                }
                else
                {
                    if (clsVisual.ValidaCPF(cgc) == false)
                    {
                        throw new Exception("CPF inválido.");
                    }
                }

                info.ie = info.ie.Replace(".", "").Replace("/", "").Replace("-", "");
                String ie = info.ie.Replace(".", "").Replace("/", "").Replace("-", "");

                if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where id!=" + info.id.ToString() + " and ie='" + ie + "'")) > 0 && ie != "" && ie != "ISENTO")
                {
                    throw new Exception("Inscrição Estadual já cadastrada.");
                }

                if (info.nome == "")
                {
                    throw new Exception("Falta Razão Social/Nome.");
                }

                //if (clsParser.Int32Parse(Procedures.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where id!=" + info.id + " and UPPER(nome)='" + info.nome + "'")) > 0)
                //{
                //    throw new Exception("Já existe um Cliente cadastrado com esse Nome/razão social.");
                //}

                if (info.ibge == null || info.ibge == "" || info.ibge.Length != 7)
                {
                    throw new Exception("Código IBGE do município inválido.");
                }

                if (info.idestado == 0)
                {
                    throw new Exception("Falta o UF.");
                }

                if (info.cep == "")
                {
                    throw new Exception("Falta o CEP.");
                }

                if (info.idregimeapuracao == 0)
                {
                    throw new Exception("Falta Regime de Apuração.");
                }

                if (info.idformapagto == 0)
                {
                    throw new Exception("Falta Forma de Pagamento.");
                }

                if (info.idcondpagto == 0)
                {
                    throw new Exception("Falta Condição de Pagamento.");
                }

                if (info.idtransportadora == 0)
                {
                    info.idtransportadora = clsInfo.zempresaclienteid;
                }

                if (info.idrepresentante == 0)
                {
                    info.idrepresentante = clsInfo.zempresaclienteid;
                }

                if (info.idcoordenador == 0)
                {
                    info.idcoordenador = clsInfo.zempresaclienteid;
                }

                if (info.idsupervisor == 0)
                {
                    info.idsupervisor = clsInfo.zempresaclienteid;
                }

                //if (info.idramo == 0)
                //{
                //    throw new Exception("Falta Ramo de Atividade.");
                //}

                if (info.idzona == 0)
                {
                    throw new Exception("Falta o Código da Zona.");
                }

                if (info.temfaturamento != null)
                {
                    if (info.temfaturamento == "P" || info.temfaturamento == "F")
                    {
                        if (info.valorminimo <= 0)
                        {
                            throw new Exception("Faturamento minímo deve ser maior que 0(zero).");
                        }
                    }
                }

                //if (info.contato.Trim() == "" || info.telefone.Trim() == "")
                //{
                //    throw new Exception("É necessário ter pelo menos um contato.");
                //}
            }
            else
            {
                throw new Exception("É necessário preencher o CPF/CNPJ.");
            }

            if (info.cognome.Length > 20)
            {
                throw new Exception("Cliente - Cognome ultrapassou o limite permitido (20 caracteres).");
            }

            if (info.nome.Length > 80)
            {
                throw new Exception("Cliente - Nome ultrapassou o limite permitido (80 caracteres).");
            }

            if (info.pessoa.Length > 1)
            {
                throw new Exception("Cliente - Pessoa ultrapassou o limite permitido (1 caracteres).");
            }

            if (info.ativo.Length > 1)
            {
                throw new Exception("Cliente - Ativo ultrapassou o limite permitido (1 caracteres).");
            }

            if (info.tipo.Length > 1)
            {
                throw new Exception("Cliente - Tipo ultrapassou o limite permitido (1 caracteres).");
            }

            if (info.ie.Length > 16)
            {
                throw new Exception("Cliente - IE ultrapassou o limite permitido (16 caracteres).");
            }

            if (info.cgc.Length > 18)
            {
                throw new Exception("Cliente - CGC ultrapassou o limite permitido (18 caracteres).");
            }

            if (info.imunicipal.Length > 16)
            {
                throw new Exception("Cliente - IMUNICIPAL ultrapassou o limite permitido (16 caracteres).");
            }

            if (info.suframa.Length > 9)
            {
                throw new Exception("Cliente - Suframa ultrapassou o limite permitido (9 caracteres).");
            }

            //if (info.endtipo.Length > 50)
            //{
            //    throw new Exception("Cliente - Endtipo ultrapassou o limite permitido (50 caracteres).");
            //}

            if (info.endereco.Length > 80)
            {
                throw new Exception("Cliente - Endereco ultrapassou o limite permitido (80 caracteres).");
            }

            if (info.tiponumero.Length > 3)
            {
                throw new Exception("Cliente - Tiponumero ultrapassou o limite permitido (3 caracteres).");
            }

            if (info.numeroend.Length > 6)
            {
                throw new Exception("Cliente - Numeroend ultrapassou o limite permitido (6 caracteres).");
            }

            if (info.andar.Length > 3)
            {
                throw new Exception("Cliente - Andar ultrapassou o limite permitido (3 caracteres).");
            }

            if (info.tipocomple.Length > 2)
            {
                throw new Exception("Cliente - Tipocomple ultrapassou o limite permitido (2 caracteres).");
            }

            if (info.comple.Length > 10)
            {
                throw new Exception("Cliente - Complemento  ultrapassou o limite permitido (10 caracteres).");
            }

            if (info.bairro.Length > 40)
            {
                throw new Exception("Cliente - Bairro ultrapassou o limite permitido (40 caracteres).");
            }

            //if (info.idcidade == 0)
            //{
            //    throw new Exception("Cliente - Cidade não foi incluida");
            //}

            if (info.ibge.Length > 7)
            {
                throw new Exception("Cliente - IBGE ultrapassou o limite permitido (7 caracteres).");
            }

            if (info.pais.Length > 3)
            {
                throw new Exception("Cliente - Pais ultrapassou o limite permitido (3 caracteres).");
            }

            if (info.cep.Length > 9)
            {
                throw new Exception("Cliente - CEP ultrapassou o limite permitido (9 caracteres).");
            }

            if (info.regiao.Length > 2)
            {
                   throw new Exception("Cliente - Regiao ultrapassou o limite permitido (2 caracteres).");
            }

            if (info.homepage.Length > 50)
            {
                throw new Exception("Cliente - Homepage ultrapassou o limite permitido (50 caracteres).");
            }

            if (info.email.Length > 50)
            {
                throw new Exception("Cliente - Email ultrapassou o limite permitido (50 caracteres).");
            }

            if (info.ftp.Length > 50)
            {
                throw new Exception("Cliente - FTP ultrapassou o limite permitido (50 caracteres).");
            }

            if (info.codigocliente.Length > 20)
            {
                throw new Exception("Cliente - Codigocliente ultrapassou o limite permitido (20 caracteres).");
            }

            if (info.freteincluso.Length > 1)
            {
                throw new Exception("Cliente - freteincluso ultrapassou o limite permitido (1 caracteres).");
            }

            if (info.freteporconta.Length > 1)
            {
                throw new Exception("Cliente - freteporconta ultrapassou o limite permitido (1 caracteres).");
            }

            if (info.meiodetransporte.Length > 1)
            {
                throw new Exception("Cliente - Meiodetransporte ultrapassou o limite permitido (1 caracteres).");
            }

            if (info.credito.Length > 1)
            {
                throw new Exception("Cliente - Credito ultrapassou o limite permitido (1 caracteres).");
            }

            if (info.temfaturamento.Length > 1)
            {
                throw new Exception("Cliente - Temfaturamento ultrapassou o limite permitido (1 caracteres).");
            }

            if (info.agencia.Length > 10)
            {
                throw new Exception("Cliente - Agencia ultrapassou o limite permitido (10 caracteres).");
            }

            if (info.titularcta.Length > 40)
            {
                throw new Exception("Cliente - Titularcta ultrapassou o limite permitido (40 caracteres).");
            }

            if (info.ctacorrente.Length > 15)
            {
                throw new Exception("Cliente - Ctacorrente ultrapassou o limite permitido (15 caracteres).");
            }

            if (info.clienteaprovado.Length > 1)
            {
                throw new Exception("Cliente - Clienteaprovado ultrapassou o limite permitido (1 caracteres).");
            }

            if (info.ddd.Length > 4)
            {
                throw new Exception("Cliente - DDD ultrapassou o limite permitido (4 caracteres).");
            }

            if (info.telefone.Length > 10)
            {
                throw new Exception("Cliente - Telefone ultrapassou o limite permitido (10 caracteres).");
            }

            if (info.contato.Length > 50)
            {
                throw new Exception("Cliente - Contato ultrapassou o limite permitido (50 caracteres).");
            }

            if (info.comissaopor.Length > 1)
            {
                throw new Exception("Cliente - Comissaopor ultrapassou o limite permitido (1 caracteres).");
            }

            if (info.comissaopagto.Length > 1)
            {
                throw new Exception("Cliente - Comissaopagto ultrapassou o limite permitido (1 caracteres).");
            }

            if (info.comissaocomo.Length > 1)
            {
                throw new Exception("Cliente - Comissaocomo ultrapassou o limite permitido (1 caracteres).");
            }

            if (info.descontapispasepent.Length > 1)
            {
                throw new Exception("Cliente - Descontapispasepent ultrapassou o limite permitido (1 caracteres).");
            }


            if (info.descontapispasepsai.Length > 1)
            {
                throw new Exception("Cliente - Descontapispasepsai ultrapassou o limite permitido (1 caracteres).");
            }


            if (info.descontacofinsent.Length > 1)
            {
                throw new Exception("Cliente - Descontacofinsent ultrapassou o limite permitido (1 caracteres).");
            }


            if (info.descontacofinssai.Length > 1)
            {
                throw new Exception("Cliente - Descontacofinssai ultrapassou o limite permitido (1 caracteres).");
            }


            if (info.isentoipi.Length > 1)
            {
                throw new Exception("Cliente - isentoipi ultrapassou o limite permitido (1 caracteres).");
            }


            if (info.revendedor.Length > 1)
            {
                throw new Exception("Cliente - Revendedor ultrapassou o limite permitido (1 caracteres).");
            }


            if (info.contribuinte.Length > 1)
            {
                throw new Exception("Cliente - Contribuinte ultrapassou o limite permitido (1 caracteres).");
            }


            if (info.alc.Length > 1)
            {
                throw new Exception("Cliente - ALC ultrapassou o limite permitido (1 caracteres).");
            }


            if (info.consumo.Length > 1)
            {
                throw new Exception("Cliente - Consumo ultrapassou o limite permitido (1 caracteres).");
            }
        }

        public DataTable GridDados(String ativo, String tipo, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "CLIENTE.ID, " +
                        "CLIENTE.NUMERO, " +
                        "CLIENTE.COGNOME, " +
                        "CLIENTE.NOME, " +
                        "ESTADOS.ESTADO AS [UF], " +
                        "CLIENTE.DDD, " +
                        "CLIENTE.TELEFONE, " +
                        "CLIENTE.CONTATO, " +
                        "RAMO.CODIGO AS RAMO, " +
                        "CLIENTE.CGC, " +
                        "case CLIENTE.ATIVO " +
                            "when 'S' then 'Ativo' " +
                            "when 'N' then 'Inativo' " +
                        "end	as ATIVO, " +
                        "case CLIENTE.TIPO " +
                            "when 'F' then 'Fornecedor' " +
                            "when 'C' then 'Cliente' " +
                            "when 'E' then 'Empresa' " +
                            "when 'V' then 'Vendedor' " +
                        "end	as TIPO, " +
                        "CLIENTE.ULTDATNF, " +
                        "CLIENTE.ULTVALNF " +
                    "FROM CLIENTE " +
                         //"INNER JOIN RAMO ON CLIENTE.IDRAMO = RAMO.ID " +
                         "INNER JOIN ESTADOS ON CLIENTE.IDESTADO = ESTADOS.ID ";

            if (ativo != null && ativo != "")
            {
                query += " where ativo=@ativo ";
            }

            if (tipo != null && tipo != "")
            {
                if (query.IndexOf("where") == -1)
                {
                    query += " where ";
                }
                else
                {
                    query += " and ";
                }

                query += " tipo = @tipo ";
            }

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            if (ativo != null && ativo != "")
            {
                sda.SelectCommand.Parameters.Add("@ativo", SqlDbType.NVarChar).Value = ativo;
            }

            if (tipo != null && tipo != "")
            {
                sda.SelectCommand.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = tipo;
            }
            sda.Fill(dt);

            return dt;
        }

        public DataTable GridDadosPes(String pesquisa, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "CLIENTE.ID, " +
                        "CLIENTE.NUMERO, " +
                        "CLIENTE.COGNOME, " +
                        "CLIENTE.NOME, " +
                        "ESTADOS.ESTADO AS [UF], " +
                        "CLIENTE.DDD, " +
                        "CLIENTE.TELEFONE, " +
                        "CLIENTE.CONTATO, " +
                        "RAMO.CODIGO AS RAMO, " +
                        "CLIENTE.CGC,  " +
                        "case CLIENTE.ATIVO " +
                            "when 'S' then 'Ativo' " +
                            "when 'N' then 'Inativo' " +
                        "end	as ATIVO, " +
                        "case CLIENTE.TIPO " +
                            "when 'F' then 'Fornecedor' " +
                            "when 'C' then 'Cliente' " +
                            "when 'E' then 'Empresa' " +
                            "when 'V' then 'Vendedor' " +
                        "end	as TIPO, " +
                        "CLIENTE.ULTDATNF, " +
                        "CLIENTE.ULTVALNF " +
                    "FROM CLIENTE " +
                         //"INNER JOIN RAMO ON CLIENTE.IDRAMO = RAMO.ID " +
                         "INNER JOIN ESTADOS ON CLIENTE.IDESTADO = ESTADOS.ID " +
                    "WHERE " +
                        "CONVERT(NVARCHAR(20),CLIENTE.NUMERO) = @PESQUISA OR " +
                        "CLIENTE.COGNOME like @PESQUISA + '%' " +
                        "OR CLIENTE.NOME LIKE @PESQUISA + '%' " +
                        "OR ESTADOS.ESTADO LIKE @PESQUISA + '%' " +
                        "OR CLIENTE.DDD LIKE @PESQUISA + '%' " +
                        "OR CLIENTE.TELEFONE like @PESQUISA + '%' " +
                        "OR CLIENTE.CONTATO LIKE @PESQUISA + '%' " +
                        "OR CLIENTE.CGC LIKE @PESQUISA + '%' " +
                        "or CONVERT(NVARCHAR(100),CLIENTE.ULTDATNF,103) like CONVERT(NVARCHAR(100),@PESQUISA,103) + '%'";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@pesquisa", SqlDbType.NVarChar).Value = pesquisa;
            sda.Fill(dt);

            return dt;
        }

        public DataTable GridDados1(Int32 idcliente, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "nfvenda.id AS [ID], " +
                        "nfvenda.numero AS [NUMERO], " +
                        "nfvenda.serie AS [SERIE], " +
                        "'NFV' as [DOCUMENTO], " +
                        "data as [DT], " +
                        "convert(varchar(12), nfvenda.data, 103) AS [DATA], " +
                        "nfvenda.situacao AS [SITUACAO], " +
                        "nfvenda.tipoentrada AS [TIPOENTRADA], " +
                        "nfvenda.totalnotafiscal AS [TOTALNOTAFISCAL] " +
                    "from NFVENDA " +
                    "where NFVENDA.IDCLIENTE=@IDCLIENTE " +
                    "union all " +
                        "select nfcompra.id  AS [ID], " +
                            "nfcompra.numero  AS [NUMERO], " +
                            "nfcompra.serie  AS [SERIE], " +
                            "'NFE' as [DOCUMENTO], " +
                            "data as [DT], " +
                            "convert(varchar(12), nfcompra.data, 103) AS [DATA], " +
                            "nfcompra.situacao  AS [SITUACAO], " +
                            "nfcompra.tipoentrada  AS [TIPOENTRADA], " +
                            "nfcompra.totalnotafiscal  AS [TOTALNOTAFISCAL] " +
                        "from nfcompra " +
                        "where NFCOMPRA.IDFORNECEDOR=@IDCLIENTE " +
                        "order by DT";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@idcliente", SqlDbType.NVarChar).Value = idcliente;
            sda.Fill(dt);

            return dt;
        }

        public DataTable GriDados2(Int32 idcliente, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "NFVENDA.id AS [ID], " +
                        "NFVENDA.numero AS [NUMERO], " +
                        "NFVENDA.serie AS [SERIE], " +
                        "'NFV' as [DOCUMENTO], " +
                        "NFVENDA.DATA as [DT], " +
                        "convert(varchar(12), NFVENDA.data, 103) AS [DATA], " +
                        "NFVENDA.situacao AS [SITUACAO], " +
                        "NFVENDA1.TIPOITEM AS [TIPOENTRADA], " +
                        "NFVENDA1.QTDE AS [QTDE], " +
                        "NFVENDA1.UNID AS [UNID], " +
                        "PECAS.CODIGO AS [CODIGO], " +
                        "PECAS.NOME AS [NOME], " +
                        "NFVENDA1.PRECO AS [PRECO], " +
                        "NFVENDA1.IPI AS [IPI], " +
                        "NFVENDA1.TOTALNOTA AS [TOTALNOTA] " +
                    "from NFVENDA " +
                        "INNER JOIN NFVENDA1 ON NFVENDA.ID = NFVENDA1.NUMERO " +
                        "INNER JOIN PECAS ON PECAS.ID = NFVENDA1.IDCODIGO " +
                    "where NFVENDA.IDCLIENTE=@IDCLIENTE " +
                        "union all " +
                        "select NFCOMPRA.id  AS [ID], " +
                            "NFCOMPRA.numero  AS [NUMERO], " +
                            "NFCOMPRA.serie  AS [SERIE], " +
                            "'NFE' as [DOCUMENTO], " +
                            "NFCOMPRA.DATA as [DT], " +
                            "convert(varchar(12), NFCOMPRA.data, 103) AS [DATA], " +
                            "NFCOMPRA.situacao  AS [SITUACAO], " +
                            "NFCOMPRA1.TIPOENTRADA AS [TIPOENTRADA], " +
                            "NFCOMPRA1.QTDE AS [QTDE], " +
                            "NFCOMPRA1.UNID AS [UNID], " +
                            "PECAS.CODIGO AS [CODIGO], " +
                            "PECAS.NOME AS [NOME], " +
                            "NFCOMPRA1.PRECO AS [PRECO], " +
                            "NFCOMPRA1.IPI AS [IPI], " +
                            "NFCOMPRA1.TOTALNOTA  AS [TOTALNOTA] " +
                        "from NFCOMPRA " +
                            "INNER JOIN NFCOMPRA1 ON NFCOMPRA.ID = NFCOMPRA1.NUMERO " +
                            "INNER JOIN PECAS ON PECAS.ID = NFCOMPRA1.IDCODIGO " +
                        "where NFCOMPRA.IDFORNECEDOR=@IDCLIENTE " +
                        "order by DT";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@idcliente", SqlDbType.NVarChar).Value = idcliente;
            sda.Fill(dt);

            return dt;
        }

        public DataTable GridDadosFinac(Int32 idcliente, DateTime _datade, DateTime _dataate, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "PAGAR.ID, " +
                        "PAGAR.DUPLICATA, " +
                        "PAGAR.POSICAO, " +
                        "PAGAR.POSICAOFIM, " +
                        "PAGAR.EMISSAO, " +
                        "DOCFISCAL.COGNOME AS [DOCUMENTO], " +
                        "CLIENTE.COGNOME, " +
                        "PAGAR.VENCIMENTO, " +
                        "((PAGAR.VALOR+PAGAR.VALORMULTA) -  ((PAGAR.VALORPAGO)+(PAGAR.VALORDEVOLVIDO)+(PAGAR.VALORDESCONTO))) AS [VAL], " +
                        "(SITUACAOTIPOTITULO.CODIGO + ' = ' + SITUACAOTIPOTITULO.NOME) AS [TITULO], PAGAR.OBSERVA " +
                    "FROM PAGAR " +
                        "INNER JOIN CLIENTE ON CLIENTE.ID = PAGAR.IDFORNECEDOR " +
                        "INNER JOIN DOCFISCAL ON DOCFISCAL.ID = PAGAR.IDDOCUMENTO " +
                        "INNER JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = PAGAR.IDFORMAPAGTO " +
                    "WHERE " +
                        "CLIENTE.ID = @IDCLIENTE AND " +
                        "CONVERT(DATETIME, PAGAR.VENCIMENTO, 102) >= CONVERT(DATETIME, @DATADE,102) AND " +
                        "CONVERT(DATETIME, PAGAR.VENCIMENTO, 102) <= CONVERT(DATETIME, @DATAATE,102) " +
                        "UNION ALL " +
                            "SELECT " +
                                "RECEBER.ID, " +
                                "RECEBER.DUPLICATA, " +
                                "RECEBER.POSICAO, " +
                                "RECEBER.POSICAOFIM, " +
                                "RECEBER.EMISSAO, " +
                                "DOCFISCAL.COGNOME AS [DOCUMENTO], " +
                                "CLIENTE.COGNOME, " +
                                "RECEBER.VENCIMENTO, " +
                                "((RECEBER.VALOR+RECEBER.VALORMULTA) -  ((RECEBER.VALORPAGO)+(RECEBER.VALORDEVOLVIDO)+(RECEBER.VALORDESCONTO))) AS [VAL], " +
                                "(SITUACAOTIPOTITULO.CODIGO + ' = ' + SITUACAOTIPOTITULO.NOME) AS [TITULO], " +
                                "RECEBER.OBSERVA " +
                            "FROM RECEBER " +
                                "INNER JOIN CLIENTE ON CLIENTE.ID = RECEBER.IDCLIENTE " +
                                "INNER JOIN DOCFISCAL ON DOCFISCAL.ID = RECEBER.IDDOCUMENTO	" +
                                "INNER JOIN SITUACAOTIPOTITULO ON SITUACAOTIPOTITULO.ID = RECEBER.IDFORMAPAGTO " +
                            "WHERE " +
                                "CLIENTE.ID = @IDCLIENTE AND CONVERT(DATETIME, RECEBER.VENCIMENTO, 102) >= CONVERT(DATETIME, @DATADE,102) AND " +
                                "CONVERT(DATETIME, RECEBER.VENCIMENTO, 102) <= CONVERT(DATETIME, @DATAATE,102) ";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@idcliente", SqlDbType.NVarChar).Value = idcliente;
            sda.Fill(dt);

            return dt;
        }

        //public Int32 SicronizarCliente(Int32 id, String cnn)
        //{
        //    era para incluir o cliente no cadastro de prospecção - desativado em 19 / 03 / 2025 - Carlos

        //}

        public static DataTable GridDadosCliente(String ativo, String tipo)
        {
            string filtro = string.Empty;

            if (ativo == "S")
            {
                filtro = " CLIENTE.ATIVO = 'S' ";
            }
            else if (ativo == "N")
            {
                filtro = " CLIENTE.ATIVO = 'N' ";
            }

            if (tipo == "C" ||
                tipo == "F" ||
                tipo == "V" ||
                tipo == "T" ||
                tipo == "U")
            {
                if (!string.IsNullOrEmpty(filtro))
                {
                    filtro += " and ";
                }

                filtro += string.Format("(cliente.tipo = '{0}' or cliente.tipo = 'E')", tipo);
            }

            var query = "select " +
                    "cliente.id, " +
                    "cliente.numero, " +
                    "cliente.cognome, " +
                    "cliente.nome, " +
                    "estados.estado as [UF], " +
                    "cliente.ddd, " +
                    "cliente.telefone, " +
                    "cliente.contato, " +
                    "ramo.codigo as ramo, " +
                    "cliente.cgc, " +
                    "cliente.ativo, " +
                    "case cliente.tipo  " +
                        "when 'T' then 'Transportadora' " +
                        "when 'F' then 'Fornecedor' " +
                        "when 'U' then 'Funcionario' " +
                        "when 'C' then 'Cliente' " +
                        "when 'E' then 'Empresa' " +
                        "when 'V' then 'Vendedor' " +
                    "end as cliente_tipo, " +
                    // "(select top 1 data from nfvenda where idcliente = cliente.id and (select count(*) from nfvenda1 where fatura='S' and nfvenda1.numero = nfvenda.id) > 0 order by nfvenda.data desc) as ultdatnf, " +
                    // "(select top 1 (select sum(totalnota) from nfvenda1 where fatura='S' and nfvenda1.numero = nfvenda.id) from nfvenda where idcliente=cliente.id and (select count(*) from nfvenda1 where fatura='S' and nfvenda1.numero = nfvenda.id) > 0 order by nfvenda.data desc) as ultvalnf, " +
                    "cliente.comissaoaliquota, " +
                    "CLIENTE1.COGNOME AS [REPRESENTANTE], " +
                    "CLIENTE1.COMISSAOALIQUOTA AS [REPRESENTANTECOMISSAO], " +
                    "CLIENTE2.COGNOME AS [COORDENADOR], " +
                    "CLIENTE2.COMISSAOALIQUOTA AS [COORDENADORCOMISSAO], " +
                    "CLIENTE3.COGNOME AS [SUPERVISOR], " +
                    "CLIENTE3.COMISSAOALIQUOTA AS [SUPERVISORCOMISSAO], " +
                    "cliente.emailnfe, " +
                    "ZONAS.CODIGO AS ZONA " +
                "from cliente " +
                    "LEFT JOIN ESTADOS ON ESTADOS.ID=CLIENTE.IDESTADO " +
                    "LEFT JOIN RAMO ON RAMO.ID=CLIENTE.IDRAMO " +
                    "LEFT JOIN CLIENTE CLIENTE1 ON CLIENTE1.ID=CLIENTE.IDVENDEDOR " +
                    "LEFT JOIN CLIENTE CLIENTE2 ON CLIENTE2.ID=CLIENTE.IDCOORDENADOR " +
                    "LEFT JOIN CLIENTE CLIENTE3 ON CLIENTE3.ID=CLIENTE.IDSUPERVISOR " +
                    "LEFT JOIN ZONAS ON ZONAS.ID=CLIENTE.IDZONA ";

            if (!string.IsNullOrEmpty(filtro))
            {
                query += " where " + filtro;
            }

            query += " order by cliente.nome";

            var dt = new DataTable();

            var sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);

            sda.Fill(dt);

            return dt;
        }

        private static GridColuna[] dtGridColunasCliente = new GridColuna[]
        {
                new GridColuna("Id.", "id", 10, false, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("Nº", "numero", 40, true, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("Cognome", "cognome", 90, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Nome", "nome", 200, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("UF", "uf", 25, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("DDD", "ddd", 30, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Telefone", "telefone", 70, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Contato", "contato", 90, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Ramo", "ramo", 80, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("CNPJ", "cgc", 80, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("A.", "ativo", 20, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Tipo", "cliente_tipo", 60, true, DataGridViewContentAlignment.MiddleLeft),
                // new GridColuna("Dt. Ult. NFVenda", "ultdatnf", 65, true, DataGridViewContentAlignment.MiddleCenter),
                // new GridColuna("R$ Ult. NFVenda", "ultvalnf", 75, true, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("% C.", "COMISSAOALIQUOTA", 65, false, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("Representante", "REPRESENTANTE", 85, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("%", "REPRESENTANTECOMISSAO", 50, false, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("Coordenador", "COORDENADOR", 80, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("%", "COORDENADORCOMISSAO", 60, false, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("Supervisor", "SUPERVISOR", 80, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("%", "SUPERVISORCOMISSAO", 50, false, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("E-mail NF-e", "emailnfe", 100, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Zona", "ZONA", 60, true, DataGridViewContentAlignment.MiddleLeft),           
        };

        public static void GridMontaCliente(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasCliente, true);
            // dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
            dgv.AllowUserToResizeColumns = true;
            // dgv.Columns["ultdatnf"].DefaultCellStyle.Format = "dd/MM/yyyy";
            // dgv.Columns["ultvalnf"].DefaultCellStyle.Format = "N2";
        }

        ////////////////////////////////////////////////////////////////////////
         public static DataTable GridDadosClientePes(String ativo, String tipo)
        {
            DataTable dt;
            String query = "";
            SqlDataAdapter sda;
            query = "select cliente.id, " +
            "cliente.numero,  " +
            "cliente.cognome,  " +
            "cliente.nome, " +
            "estados.estado as [UF],  " +
            "cliente.ddd,  " +
            "cliente.telefone,  " +
            "cliente.contato, " +
            "ramo.codigo as ramo,  " +
            "cliente.cgc, case cliente.ativo  when 'S' then 'Ativo'  when 'N' then 'Inativo' end as ativo, " +
            "case cliente.tipo  when 'F' then 'Fornecedor'  when 'C' then 'Cliente'  when 'E' then 'Empresa'  when 'V' then 'Vendedor' end as cliente_tipo, cliente.ultdatnf, cliente.ultvalnf, cliente.comissaoaliquota, " +
            "CLIENTE1.COGNOME AS [REPRESENTANTE], " +
            "CLIENTE1.COMISSAOALIQUOTA AS [REPRESENTANTECOMISSAO], " +
            "CLIENTE2.COGNOME AS [COORDENADOR],  " +
            "CLIENTE2.COMISSAOALIQUOTA AS [COORDENADORCOMISSAO], " +
            "CLIENTE3.COGNOME AS [SUPERVISOR],  " +
            "CLIENTE3.COMISSAOALIQUOTA AS [SUPERVISORCOMISSAO], " +
            "cliente.emailnfe " +
            "from cliente " +
            "LEFT JOIN ESTADOS ON ESTADOS.ID=CLIENTE.IDESTADO " +
            "LEFT JOIN RAMO ON RAMO.ID=CLIENTE.IDRAMO " +
            "LEFT JOIN CLIENTE CLIENTE1 ON CLIENTE1.ID=CLIENTE.IDVENDEDOR " +
            "LEFT JOIN CLIENTE CLIENTE2 ON CLIENTE2.ID=CLIENTE.IDCOORDENADOR " +
            "LEFT JOIN CLIENTE CLIENTE3 ON CLIENTE3.ID=CLIENTE.IDSUPERVISOR";
            
             if (ativo == "S")
            {
                query = query + " where CLIENTE.ativo = 'S' ";
            }
            else if (ativo == "N")
            {
                query = query + " where CLIENTE.ativo = 'N' ";
            }

            if (ativo != "S" && ativo != "N")
            {
                query += " where ";
            }
            else
            {
                query += " and ";
            }


            query += "(";

            if (tipo == "C") //Clientes
            {
                query += "cliente.tipo = 'C'";
            }
            else if (tipo == "F") //Fornecedores
            {
                query += "cliente.tipo = 'F'";
            }
            else if (tipo == "V") //Vendedores
            {
                query += "cliente.tipo = 'V'";
            }
            else if (tipo == "T") //Transportadoras
            {
                query += "cliente.tipo = 'T'";
            }            

            if (tipo == "") //todos
            {
                query += "cliente.id > 0)";
            }
            else
            {
                query += " or cliente.tipo = 'E')"; //Empresa
            }
             
            query += " order by cliente.cognome ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);

            dt = new DataTable();
            sda.Fill(dt);
            return dt;
         }

         private static GridColuna[] dtGridColunasClientePes = new GridColuna[]
        {
                new GridColuna("Id.", "id", 10, false, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("Nº", "numero", 40, true, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("Cognome", "cognome", 110, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Nome", "nome", 400, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("UF", "uf", 25, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("DDD", "ddd", 30, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Telefone", "telefone", 70, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Contato", "contato", 90, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Ramo", "ramo", 80, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("CNPJ", "cgc", 80, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("A.", "ativo", 40, true, DataGridViewContentAlignment.MiddleCenter),
                //new GridColuna("Tipo", "cliente_tipo", 60, true, DataGridViewContentAlignment.MiddleLeft),
                //new GridColuna("Dt. NF", "ultdatnf", 65, true, DataGridViewContentAlignment.MiddleCenter),
                //new GridColuna("R$ NF", "ultvalnf", 75, true, DataGridViewContentAlignment.MiddleRight),
                //new GridColuna("% C.", "COMISSAOALIQUOTA", 65, false, DataGridViewContentAlignment.MiddleRight),
                //new GridColuna("Representante", "REPRESENTANTE", 85, true, DataGridViewContentAlignment.MiddleLeft),
                //new GridColuna("%", "REPRESENTANTECOMISSAO", 50, false, DataGridViewContentAlignment.MiddleRight),
                //new GridColuna("Coordenador", "COORDENADOR", 80, false, DataGridViewContentAlignment.MiddleLeft),
                //new GridColuna("%", "COORDENADORCOMISSAO", 60, false, DataGridViewContentAlignment.MiddleRight),
                //new GridColuna("Supervisor", "SUPERVISOR", 80, false, DataGridViewContentAlignment.MiddleLeft),
                //new GridColuna("%", "SUPERVISORCOMISSAO", 50, false, DataGridViewContentAlignment.MiddleRight),
                //new GridColuna("E-mail NF-e", "emailnfe", 100, false, DataGridViewContentAlignment.MiddleLeft),
         };

         public static void GridMontaClientePes(DataGridView dgv, DataTable dt, Int32 id)
         {
             
             dgv.DataSource = dt;
             clsGridHelper.MontaGrid2(dgv, dtGridColunasClientePes, true);
             dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
             clsGridHelper.SelecionaLinha(id, dgv, "NUMERO");
             dgv.AllowUserToResizeColumns = true;
             dgv.Columns["ultdatnf"].DefaultCellStyle.Format = "dd/MM/yyyy";
             dgv.Columns["ultvalnf"].DefaultCellStyle.Format = "N2";
         }
    }
}
