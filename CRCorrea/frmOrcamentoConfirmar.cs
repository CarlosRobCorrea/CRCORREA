using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CrystalDecisions.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmOrcamentoConfirmar : Form
    {
        Boolean disposing = false;

        clsOrcamentoBLL OrcamentoBLL;
        clsOrcamentoInfo OrcamentoInfo;
        clsOrcamentoInfo OrcamentoInfoOld;

        clsOrcamento1BLL clsOrcamento1BLL;
        clsOrcamento1Info clsOrcamento1Info;
        
        Int32 id;
        DataGridViewRowCollection rows;
        Int32 idCliente;
        Int32 idCondPagto;
        Int32 idVendedor;

        DataTable dtOrcamento;
        DataTable dtOrcamento1;

        Int32 orcamento1_id;
        Int32 orcamento1_posicao;
        Int32 idcodigo;
        Int32 idordemservico;
        Int32 idordemservicoitem;
        Int32 idpedido;
        Int32 idpedidoitem;

        // Pedido
        Int32 idPedido = 0;
        clsPedidoInfo clsPedidoInfo = new clsPedidoInfo();
        clsPedidoBLL clsPedidoBLL = new clsPedidoBLL();
        clsPedido1Info clsPedido1Info = new clsPedido1Info();
        clsPedido1BLL clsPedido1BLL = new clsPedido1BLL();

        public frmOrcamentoConfirmar()
        {
            InitializeComponent();
        }
        public void Init(Int32 _id, DataGridViewRowCollection _rows)
        {
            id = _id;
            rows = _rows;

            OrcamentoBLL = new clsOrcamentoBLL();
            clsOrcamento1BLL = new clsOrcamento1BLL();

        }

        private void frmOrcamentoConfirmar_Load(object sender, EventArgs e)
        {
            OrcamentoCarregar();
        }

        private void frmOrcamentoConfirmar_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }
        private void TrataCampos(Control ctl)
        {
            if (disposing == false)
            {

                if (clsInfo.zrow != null &&
                clsInfo.zrow.Index != -1 &&
                clsInfo.znomegrid != null &&
                clsInfo.znomegrid != "")
                {
                    // ###############################
                    // Verifica os botões de pesquisa
                    // ###############################

                    if (clsInfo.znomegrid == btnIdCliente.Name)  // FORNECEDOR
                    {
                        idCliente = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        if (idCliente == clsInfo.zempresaclienteid)
                        {   // Cliente avulso liberar para digitar
                            tbxCognome.Enabled = true;
                            tbxDDD.Enabled = true;
                            tbxTelefone.Enabled = true;
                            tbxAtencao.Enabled = true;
                            tbxEmail.Enabled = true;
                        }
                        else
                        {
                            ClienteProspeccao_Carrega();
                            tbxCognome.Enabled = false;
                            tbxDDD.Enabled = false;
                            tbxTelefone.Enabled = false;
                            tbxAtencao.Enabled = false;
                            tbxEmail.Enabled = false;

                        }
                        tbxCognome.Select();
                        tbxCognome.SelectAll();
                    }
                    else if (clsInfo.znomegrid == btnCodigo.Name)    // PRODUTO
                    {
                        idcodigo = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecas where id=" + idcodigo);
                        tbxDescricao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where id=" + idcodigo);
                        if (clsParser.DecimalParse(tbxPreco.Text) == 0)
                        {
                            tbxPreco.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                            "select valoraprazo from pecaspreco where idcodigo = " + idcodigo + " order by data desc ", "0").ToString()).ToString("N6");
                        }
                        tbxCodigo.Select();
                        tbxCodigo.SelectAll();
                    }
                }
                else
                {
                    // ###############################
                    // Verifica os campos de pesquisa
                    // ###############################
                    if (ctl.Name == tbxCodigo.Name)    // FORNECEDOR
                    {
                        idcodigo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecas where codigo='" + tbxCodigo.Text + "' ", "0"));
                        tbxDescricao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where id=" + idcodigo);
                        if (clsParser.DecimalParse(tbxPreco.Text) == 0)
                        {
                            tbxPreco.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                            "select valoraprazo from pecaspreco where idcodigo = " + idcodigo + " order by data desc ", "0").ToString()).ToString("N6");
                        }
                    }
                }
                tbxQtde.Text = clsParser.DecimalParse(tbxQtde.Text).ToString("N2");
                tbxPreco.Text = clsParser.DecimalParse(tbxPreco.Text).ToString("N2");
                tbxDesconto.Text = clsParser.DecimalParse(tbxDesconto.Text).ToString("N4");
                if (clsParser.DecimalParse(tbxDesconto.Text) > 0)
                {
                    tbxValorDesconto.Text = (clsParser.DecimalParse(tbxPreco.Text) * (clsParser.DecimalParse(tbxDesconto.Text) / 100)).ToString("N2");
                }
                tbxValorDesconto.Text = clsParser.DecimalParse(tbxValorDesconto.Text).ToString("N2");
                tbxPrecoLiquido.Text = (clsParser.DecimalParse(tbxPreco.Text) - clsParser.DecimalParse(tbxValorDesconto.Text)).ToString("N2");
                tbxTotalMercadoria.Text = (clsParser.DecimalParse(tbxPrecoLiquido.Text) * clsParser.DecimalParse(tbxQtde.Text)).ToString("N2");
                tbxPeso.Text = clsParser.DecimalParse(tbxPeso.Text).ToString("N4");
                tbxTotalPeso.Text = (clsParser.DecimalParse(tbxPeso.Text) * clsParser.DecimalParse(tbxQtde.Text)).ToString("N3");

                tbxQtdeConfirmadaItem.Text = clsParser.DecimalParse(tbxQtdeConfirmadaItem.Text).ToString("N2");
                tbxTotalConfirmado.Text = clsParser.DecimalParse(tbxTotalConfirmado.Text).ToString("N2");
                tbxTotalConfirmadoItem.Text = (clsParser.DecimalParse(tbxTotalConfirmado.Text) * clsParser.DecimalParse(tbxQtdeConfirmada.Text)).ToString("N2"); ;

                Orcamento_Somar();

                if (clsParser.DecimalParse(tbxTotalConfirmado.Text)> 0)
                {
                    tspSalvar.Enabled = true;
                }
                else
                {
                    tspSalvar.Enabled = false;
                }
                clsInfo.znomegrid = "";
                clsInfo.zrow = null;
            }
        }
        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            if (disposing == false)
            {
                clsVisual.ControlLeave(sender);
                TrataCampos((Control)sender);
            }
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void OrcamentoCarregar()
        {
            OrcamentoInfoOld = new clsOrcamentoInfo();


            OrcamentoInfo = OrcamentoBLL.Carregar(id, clsInfo.conexaosqldados);

            OrcamentoCampos(OrcamentoInfo);
            OrcamentoFillInfo(OrcamentoInfoOld);


            // carregando os itens do Orçamento

            dtOrcamento1 = clsOrcamento1BLL.GridDados(OrcamentoInfo.id, clsInfo.conexaosqldados);
            DataColumn dcPosicao = new DataColumn("orcamento1_posicao", Type.GetType("System.Int32"));
            dtOrcamento1.Columns.Add(dcPosicao);
            for (Int32 i = 1; i <= dtOrcamento1.Rows.Count; i++)
            {
                dtOrcamento1.Rows[i - 1]["orcamento1_posicao"] = i;
            }

            dtOrcamento1.AcceptChanges();
            clsOrcamento1BLL.GridMonta(dgvOrcamento1, dtOrcamento1, orcamento1_posicao);

            dgvOrcamento1.Columns["QTDE"].DefaultCellStyle.Format = "N2";
            dgvOrcamento1.Columns["PRECOLIQUIDO"].DefaultCellStyle.Format = "N2";
            dgvOrcamento1.Columns["TOTALMERCADORIA"].DefaultCellStyle.Format = "N2";
            dgvOrcamento1.Columns["QTDECONFIRMADAITEM"].DefaultCellStyle.Format = "N2";
            dgvOrcamento1.Columns["TOTALCONFIRMADOITEM"].DefaultCellStyle.Format = "N2";

            // Colocando as Cores nos Itens
            for (Int32 X = 0; X < dgvOrcamento1.Rows.Count; X++)
            {
                if (clsParser.DecimalParse(dgvOrcamento1.Rows[X].Cells["QTDECONFIRMADAITEM"].Value.ToString()) > 0)
                {
                    dgvOrcamento1.Rows[X].DefaultCellStyle.BackColor = Color.PaleGoldenrod;
                }
            }


        }
        private void OrcamentoCampos(clsOrcamentoInfo info)
        {
            id = info.id;
            tbxAtencao.Text = info.atencao;
            //tbxCognome.Text = info.cognome;
            tbxData.Text = info.data.ToString("dd/MM/yyyy HH:mm");
            tbxDDD.Text = info.ddd;
            tbxEmail.Text = info.email;
            //info.emitente;
            tbxFilial.Text = info.filial.ToString().PadLeft(2, '0');
            idCliente = info.idcliente;
            ClienteProspeccao_Carrega();
            idCondPagto = info.idcondpag;
            //info.idresponsavel;
            idVendedor = info.idvendedor;
            //info.motivo;
            tbxNumero.Text = info.numero.ToString();
            //info.observa;
            //info.observa1;
            //info.observa2;
            //info.observa3;
            //info.observa4;
            //info.observaa;
            //            tbxPorDesc.Text = info.pordesc.ToString("N2");
            tbxQtdeConfirmada.Text = info.qtdeconfirmada.ToString("N0");
            tbxQtdeOrcada.Text = info.qtdeorcada.ToString("N0");
            //info.qtdeorcadanao;
            //info.referencia;
            //info.sedex;
            if (info.setor == "N")
            {
                rbnSetorNao.Checked = true;
            }
            else
            {
                rbnSetorSim.Checked = true;
            }
            tbxSituacao.Text = info.situacao;
            tbxTelefone.Text = info.telefone;
            tbxTotalConfirmado.Text = info.totalconfirmado.ToString("N2");
            //            tbxTotalOrcamentoDesconto.Text = info.totaldesconto.ToString("N2");
            //info.totalfrete;
            tbxTotalOrcamentoBruto.Text = info.totalorcamentobruto.ToString("N2");
            //            tbxTotalOrcamentoLiquido.Text = info.totalorcamentoliquido.ToString("N2");
            //info.totalpeso;
            //info.validade;
        }
        void OrcamentoFillInfo(clsOrcamentoInfo info)
        {
            info.id = id;
            info.atencao = tbxAtencao.Text;
            //info.cognome = tbxCognome.Text;
            info.data = clsParser.DateTimeParse(tbxData.Text);
            info.ddd = tbxDDD.Text;
            info.email = tbxEmail.Text;
            //info.emitente =
            info.filial = clsParser.Int32Parse(tbxFilial.Text);
            info.idcliente = idCliente;
            info.idcondpag = idCondPagto;
            //info.idresponsavel =
            info.idvendedor = idVendedor;
            //info.motivo =
            info.numero = clsParser.Int32Parse(tbxNumero.Text);
            //info.observa =
            //info.observa1 =
            //info.observa2 =
            //info.observa3=
            //info.observa4 =
            //info.observaa =
            //            info.pordesc = clsParser.DecimalParse(tbxPorDesc.Text);
            info.qtdeconfirmada = clsParser.DecimalParse(tbxQtdeConfirmada.Text);
            info.qtdeorcada = clsParser.DecimalParse(tbxQtdeOrcada.Text);
            //info.qtdeorcadanao;
            //info.referencia =
            //info.sedex =
            if (rbnSetorNao.Checked == true)
            {
                info.setor = "N";
            }
            else
            {
                info.setor = "S";
            }
            info.situacao = tbxSituacao.Text;
            info.telefone = tbxTelefone.Text;
            info.totalconfirmado = clsParser.DecimalParse(tbxTotalConfirmado.Text);
            //            info.totaldesconto = clsParser.DecimalParse(tbxTotalOrcamentoDesconto.Text);
            //info.totalfrete;
            info.totalorcamentobruto = clsParser.DecimalParse(tbxTotalOrcamentoBruto.Text);
            //            info.totalorcamentoliquido = clsParser.DecimalParse(tbxTotalOrcamentoLiquido.Text);
            //info.totalpeso;
            //info.validade;
        }
        private void ClienteProspeccao_Carrega()
        {
            tbxCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id=" + idCliente);
            tbxAtencao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from cliente where id = " + idCliente);
            tbxDDD.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ddd from ClienteContato where idcliente = " + idCliente);
            tbxTelefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select telefone from ClienteContato where idcliente = " + idCliente);
            tbxEmail.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select email from ClienteContato where idcliente = " + idCliente);
        }
        private void Orcamento_Somar()
        {
            tbxQtdeOrcada.Text = "0";
            tbxQtdeConfirmada.Text = "0";
            tbxTotalOrcamentoBruto.Text = "0";
            tbxTotalConfirmado.Text = "0";
            //tbxTotalOrcamentoDesconto.Text = "0";
            //tbxPorDesc.Text = "0";
            //tbxTotalOrcamentoLiquido.Text = "0";

            foreach (DataRow row in dtOrcamento1.Rows)
            {
                if (row.RowState == DataRowState.Deleted ||
                    row.RowState == DataRowState.Detached)
                {
                    continue;   // não somar se apagou
                }
                else
                {
                    // Somar os campos do 
                    tbxQtdeOrcada.Text = (clsParser.DecimalParse(tbxQtdeOrcada.Text) + clsParser.DecimalParse(row["QTDE"].ToString())).ToString("N2");
                    tbxQtdeConfirmada.Text = (clsParser.DecimalParse(tbxQtdeConfirmada.Text) + clsParser.DecimalParse(row["QTDECONFIRMADAITEM"].ToString())).ToString("N2");
                    tbxTotalOrcamentoBruto.Text = (clsParser.DecimalParse(tbxTotalOrcamentoBruto.Text) + clsParser.DecimalParse(row["TOTALMERCADORIA"].ToString())).ToString("N2");
                    tbxTotalConfirmado.Text = (clsParser.DecimalParse(tbxTotalConfirmado.Text) + clsParser.DecimalParse(row["TOTALCONFIRMADOITEM"].ToString())).ToString("N2");

                }
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvOrcamento1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvOrcamento1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            orcamento1_posicao = Int32.Parse(dgvOrcamento1.CurrentRow.Cells["orcamento1_posicao"].Value.ToString());

            DataRow row;
            DataRow[] rows = dtOrcamento1.Select("orcamento1_posicao=" + orcamento1_posicao);

            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtOrcamento1.NewRow();
            }

            if (clsParser.DecimalParse(row["qtdeconfirmadaitem"].ToString()) > 0)
            {

                row["precoconfirmado"] = "0";
                row["qtdeconfirmadaitem"] = "0";
                row["situacao"] = "A";
                row["totalconfirmadoitem"] = "0";
            }
            else
            {
                row["precoconfirmado"] = row["preco"];
                row["qtdeconfirmadaitem"] = row["qtde"];
                row["situacao"] = "F";
                row["totalconfirmadoitem"] = row["totalmercadoria"]; 
            }
            Orcamento_Somar();
            if (clsParser.DecimalParse(tbxQtdeConfirmada.Text) > 0)
            {
                tbxSituacao.Text = "F";
            }
            else
            {
                tbxSituacao.Text = "A";
            }
            if (clsParser.DecimalParse(tbxTotalConfirmado.Text) > 0)
            {
                tspSalvar.Enabled = true;
            }
            else
            {
                tspSalvar.Enabled = false;
            }


        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Confirmar os Itens ?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (resultado == DialogResult.Yes)
                {
                    OrcamentoSalvar();
                }
                else if (resultado == DialogResult.Cancel)
                {
                    tbxNumero.Select();
                    tbxNumero.SelectAll();
                    return;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void OrcamentoSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {

                // ###############################
                // Tabela: ORCAMENTO
                OrcamentoInfo = new clsOrcamentoInfo();
                OrcamentoFillInfo(OrcamentoInfo);

                if (id == 0)
                {
                    OrcamentoInfo.id = OrcamentoBLL.Incluir(OrcamentoInfo, clsInfo.conexaosqldados);

                }
                else
                {
                    OrcamentoBLL.Alterar(OrcamentoInfo, clsInfo.conexaosqldados);
                }

                //// Marca os registros filho
                //// Orçamento1
                foreach (DataRow rowOrcamento1 in dtOrcamento1.Rows)
                {
                    if (rowOrcamento1.RowState != DataRowState.Deleted &&
                        rowOrcamento1.RowState != DataRowState.Detached &&
                        rowOrcamento1.RowState != DataRowState.Unchanged)
                    {
                        rowOrcamento1["idorcamento"] = OrcamentoInfo.id;
                    }
                }

                //// ###############################
                //// Tabela: ORCAMENTO1
                ArrayList orcamento1_excluir = new ArrayList();
                foreach (DataRow row in dtOrcamento1.Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                    {
                        orcamento1_excluir.Add(row["id", DataRowVersion.Original]);
                        continue;
                    }
                    else if (row.RowState == DataRowState.Detached ||
                             row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }

                    clsOrcamento1Info = new clsOrcamento1Info();
                    Orcamento1FillGridToInfo(clsOrcamento1Info, clsParser.Int32Parse(row["orcamento1_posicao"].ToString()));

                    if (clsOrcamento1Info.id == 0)
                    {
                        clsOrcamento1Info.id = clsOrcamento1BLL.Incluir(clsOrcamento1Info, clsInfo.conexaosqldados);
                    }
                    else
                    {
                        clsOrcamento1BLL.Alterar(clsOrcamento1Info, clsInfo.conexaosqldados);
                    }

                }

                foreach (Object obj in orcamento1_excluir)
                {
                    if (obj is Int32)
                    {
                        clsOrcamento1BLL.Excluir(Int32.Parse(obj.ToString()), clsInfo.conexaosqldados);
                    }
                }

                // ###############################
                tse.Complete();
            }
            if (clsParser.DecimalParse(tbxTotalConfirmado.Text)> 0)
            {
                // Incluir Pedido dos itens confirmados
                IncluirPedido();
            }
        }
        private void Orcamento1FillGridToInfo(clsOrcamento1Info info, Int32 posicao)
        {
            DataRow row = dtOrcamento1.Select("orcamento1_posicao=" + posicao)[0];

            info.id = clsParser.Int32Parse(row["id"].ToString());
            info.desconto = clsParser.DecimalParse(row["desconto"].ToString());
            info.descricao1 = row["descricao1"].ToString();
            info.descricao2 = row["descricao2"].ToString();
            info.descricao3 = row["descricao3"].ToString();
            info.descricao4 = row["descricao4"].ToString();
            info.idcodigo = clsParser.Int32Parse(row["idcodigo"].ToString());
            info.idorcamento = clsParser.Int32Parse(row["idorcamento"].ToString());
            info.idordemservico = clsParser.Int32Parse(row["idordemservico"].ToString());
            info.idordemservicoitem = clsParser.Int32Parse(row["idordemservicoitem"].ToString());
            info.idpedido = clsParser.Int32Parse(row["idpedido"].ToString());
            info.idpedidoitem = clsParser.Int32Parse(row["idpedidoitem"].ToString());
            info.item = clsParser.Int32Parse(row["item"].ToString());
            info.motivo = row["motivo"].ToString();
            info.peso = clsParser.DecimalParse(row["peso"].ToString());
            info.preco = clsParser.DecimalParse(row["preco"].ToString());
            info.precoconfirmado = clsParser.DecimalParse(row["precoconfirmado"].ToString());
            info.precoliquido = clsParser.DecimalParse(row["precoliquido"].ToString());
            info.qtde = clsParser.DecimalParse(row["qtde"].ToString());
            info.qtdeconfirmadaitem = clsParser.DecimalParse(row["qtdeconfirmadaitem"].ToString());
            info.referencia1 = row["referencia1"].ToString();
            info.referencia2 = row["referencia2"].ToString();
            info.situacao = row["situacao"].ToString();
            info.totalconfirmadoitem = clsParser.DecimalParse(row["totalconfirmadoitem"].ToString());
            info.totalmercadoria = clsParser.DecimalParse(row["totalmercadoria"].ToString());
            info.totalpeso = clsParser.DecimalParse(row["totalpeso"].ToString());
            info.unid = row["unid"].ToString();
            info.valordesconto = clsParser.DecimalParse(row["valordesconto"].ToString());
        }
        private void IncluirPedido()
        {
            clsPedidoInfo = new clsPedidoInfo();
            clsPedidoInfo.ano = DateTime.Now.Year;
            clsPedidoInfo.data = DateTime.Now;
            clsPedidoInfo.emitente = clsInfo.zusuario;
            clsPedidoInfo.filial = clsInfo.zfilial;
            clsPedidoInfo.idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where id= " + idCliente + ""));
            clsPedidoInfo.idcondpagto = clsInfo.zcondpagto;
            clsPedidoInfo.idformapagto = clsInfo.zformapagto;
            clsPedidoInfo.idtransportadora = idCliente;
            clsPedidoInfo.idredespacho = idCliente;
            clsPedidoInfo.idvendedor = clsInfo.zempresaclienteid;
            clsPedidoInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT TOP 1 NUMERO FROM PEDIDO WHERE YEAR(DATA) = " + DateTime.Now.Year + " ORDER BY NUMERO DESC ")) + 1;
            clsPedidoInfo.observa = "";
            clsPedidoInfo.pago_ctareceber = "N";
            clsPedidoInfo.id = clsPedidoBLL.Incluir(clsPedidoInfo, clsInfo.conexaosqldados);
            idPedido = clsPedidoInfo.id;
            // Itens do Pedido1
            // Fazer um Looping dos Itens Orçados
            SqlConnection scn;
            SqlCommand scd;

            foreach (DataRow row in dtOrcamento1.Rows)
            {
                if (clsParser.DecimalParse(row["qtdeconfirmadaitem"].ToString()) > 0)
                {
                    clsPedido1Info = new clsPedido1Info();

                    clsPedido1Info.baseicm = 0;
                    clsPedido1Info.baseicmsubst = 0;
                    clsPedido1Info.basemp = 0;
                    clsPedido1Info.calculoautomatico = "S";
                    clsPedido1Info.codigoemp01 = row["descricao1"].ToString();
                    clsPedido1Info.codigoemp02 = row["descricao2"].ToString();
                    clsPedido1Info.codigoemp03 = row["descricao3"].ToString();
                    clsPedido1Info.codigoemp04 = row["descricao4"].ToString();
                    clsPedido1Info.cofins1 = 0;
                    clsPedido1Info.complemento = "";
                    clsPedido1Info.consumo = "";
                    clsPedido1Info.custoicm = 0;
                    clsPedido1Info.custoipi = 0;
                    clsPedido1Info.icm = 0;
                    clsPedido1Info.icmsubst = 0;

                    clsPedido1Info.idtiponota = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from tiponota where codigo= '5124-000' "));

                    clsPedido1Info.idcfop = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcfopok from tiponota where id= " + clsPedido1Info.idtiponota + ""));
                    clsPedido1Info.idcodigo = clsParser.Int32Parse(row["idcodigo"].ToString());
                    clsPedido1Info.idipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idipi from pecas where id= " + clsPedido1Info.idcodigo + ""));
                    clsPedido1Info.idorcamento = clsParser.Int32Parse(row["idorcamento"].ToString());
                    clsPedido1Info.idorcamentoitem = clsParser.Int32Parse(row["id"].ToString());
                    clsPedido1Info.idordemservico = clsParser.Int32Parse(row["idordemservico"].ToString());
                    clsPedido1Info.idpedido = idPedido;
                    clsPedido1Info.idsittriba = clsInfo.zsituacaotriba;
                    clsPedido1Info.idsittribb = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCSTICMS from tiponota where id= " + clsPedido1Info.idtiponota + ""));
                    clsPedido1Info.idsittribcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCSTCOFINS from tiponota where id= " + clsPedido1Info.idtiponota + ""));
                    clsPedido1Info.idsittribipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDCSTIPI from tiponota where id= " + clsPedido1Info.idtiponota + ""));
                    clsPedido1Info.idsittribpispasep = 1; // clsParser.Int32Parse((clsInfo.zleipispasep).ToString());

                    clsPedido1Info.idcentrocusto = clsInfo.zcentrocustos;
                    clsPedido1Info.idcontacontabil = clsInfo.zcontacontabil;

                    
                    clsPedido1Info.idunidade = clsInfo.zunidade;
                    clsPedido1Info.ipi = 0;
                    clsPedido1Info.nitem = 0;
                    clsPedido1Info.peso = clsParser.DecimalParse(row["peso"].ToString());
                    clsPedido1Info.pispasep = 0;
                    clsPedido1Info.preco = clsParser.DecimalParse(row["precoconfirmado"].ToString());
                    clsPedido1Info.precodesconto = clsParser.DecimalParse(row["desconto"].ToString());
                    clsPedido1Info.precotabela = clsParser.DecimalParse(row["precoconfirmado"].ToString());
                    clsPedido1Info.qtde = clsParser.DecimalParse(row["qtdeconfirmadaitem"].ToString());
                    clsPedido1Info.qtdebaixada = 0;
                    clsPedido1Info.qtdedefeito = 0;
                    clsPedido1Info.qtdeentregue = 0;
                    clsPedido1Info.qtdeosaux = 0;
                    clsPedido1Info.qtdesaldo = 0;
                    clsPedido1Info.qtdesucata = 0;
                    clsPedido1Info.totalmercado = clsParser.DecimalParse(row["totalconfirmadoitem"].ToString());
                    clsPedido1Info.totalnota = clsParser.DecimalParse(row["totalconfirmadoitem"].ToString());
                    clsPedido1Info.totalpeso = clsParser.DecimalParse(row["totalpeso"].ToString());
                    clsPedido1Info.totalprevisto = clsParser.DecimalParse(row["totalconfirmadoitem"].ToString());
                    clsPedido1Info.valorfrete = clsParser.DecimalParse("0".ToString());
                    //            clsPedido1Info.valorfreteicms = row["valorfreteicms"].ToString();
                    //            clsPedido1Info.valoroutras = clsParser.DecimalParse(row["valoroutras"].ToString());
                    //            clsPedido1Info.valoroutrasicms = row["valoroutrasicms"].ToString();
                    clsPedido1Info.valorseguro = clsParser.DecimalParse("0".ToString());
                    //info.valorseguroicms = row["valorseguroicms"].ToString();
                    // Incluir item no Pedido1 item
                    clsPedido1Info.id = clsPedido1BLL.Incluir(clsPedido1Info, clsInfo.conexaosqldados);
                    // gravar o numero do pedido no orçamento
                    // Gravar no pagar
                    scn = new SqlConnection(clsInfo.conexaosqldados);
                    scd = new SqlCommand("UPDATE ORCAMENTO1 SET " +
                                            "IDPEDIDO=@IDPEDIDO, IDPEDIDOITEM=@IDPEDIDO1 " +
                                            "WHERE ID = @ID", scn);
                    scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = clsParser.Int32Parse(row["ID"].ToString());
                    scd.Parameters.AddWithValue("@IDPEDIDO", SqlDbType.Decimal).Value = idPedido;
                    scd.Parameters.AddWithValue("@IDPEDIDO1", SqlDbType.Decimal).Value = clsPedido1Info.id;
                    scn.Open();
                    scd.ExecuteNonQuery();
                    scn.Close();
                }
            }
            // Marcar que foi fechado no orcamento
            scn = new SqlConnection(clsInfo.conexaosqldados);
            scd = new SqlCommand("UPDATE ORCAMENTO SET " +
                                    "SITUACAO=@SITUACAO " +
                                    "WHERE ID = @ID", scn);
            scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = id;
            scd.Parameters.AddWithValue("@SITUACAO", SqlDbType.Char).Value = "S";
            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();



            frmPedido frmPedido = new frmPedido();
            frmPedido.Init(idPedido, dgvOrcamento1.Rows);
            clsFormHelper.AbrirForm(this.MdiParent, frmPedido, clsInfo.conexaosqldados);

        }

    }
}
