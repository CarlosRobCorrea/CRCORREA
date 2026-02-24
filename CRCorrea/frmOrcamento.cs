using CRCorreaBLL;
using CRCorreaInfo;
using CRCorreaFuncoes;
using CrystalDecisions.Shared;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;
using System.IO;


namespace CRCorrea
{
    public partial class frmOrcamento : Form
    {
        Boolean disposing = false;

        clsOrcamentoBLL OrcamentoBLL;
        clsOrcamentoInfo OrcamentoInfo;
        clsOrcamentoInfo OrcamentoInfoOld;

        clsOrcamento1BLL clsOrcamento1BLL;
        clsOrcamento1Info clsOrcamento1Info;
        clsOrcamento1Info clsOrcamento1InfoOld;

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
        Int32 idunidade;

        clsPecasInfo clsPecasInfo = new clsPecasInfo();
        clsPecasBLL clsPecasBLL = new clsPecasBLL();

        public frmOrcamento()
        {
            InitializeComponent();
        }
        public void Init(Int32 _id, DataGridViewRowCollection _rows)
        {
            id = _id;
            rows = _rows;

            OrcamentoBLL = new clsOrcamentoBLL();
            clsOrcamento1BLL = new clsOrcamento1BLL();
            clsPecasBLL = new clsPecasBLL();

        }

        private void frmOrcamento_Load(object sender, EventArgs e)
        {
            OrcamentoCarregar();
        }
        private void frmOrcamento_Activated(object sender, EventArgs e)
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
                            Cliente_Carrega();
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
                        if (idcodigo > 0)
                        {
                            // Carregar por causa da fotografia (Não consegui converter)
                            clsPecasInfo = clsPecasBLL.Carregar(idcodigo, clsInfo.conexaosqldados);
                            tbxCodigo.Text = clsPecasInfo.codigo; //Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecas where id=" + idcodigo);
                            idunidade = clsPecasInfo.idunidade;//  clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idunidade from pecas where id=" + idcodigo, "0"));
                            tbxUnid.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where id=" + idunidade);
                            tbxDescricao.Text = clsPecasInfo.nome; // Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where id=" + idcodigo);
                            pbxFoto.Image = clsPecasInfo.foto;
                            //tbxFotoCodigo.Text = "c:\\Clientes\\Eletrica\\Imagens\\" + tbxCodigo.Text + ".png";
                        }
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
                        if (idcodigo > 0)
                        {
                            // Carregar por causa da fotografia (Não consegui converter)
                            clsPecasInfo = clsPecasBLL.Carregar(idcodigo, clsInfo.conexaosqldados);
                            tbxCodigo.Text = clsPecasInfo.codigo; //Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecas where id=" + idcodigo);
                            idunidade = clsPecasInfo.idunidade;//  clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idunidade from pecas where id=" + idcodigo, "0"));
                            tbxUnid.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where id=" + idunidade);
                            tbxDescricao.Text = clsPecasInfo.nome; // Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where id=" + idcodigo);
                            pbxFoto.Image = clsPecasInfo.foto;
                        }
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
                // Fechar o Orçamento
                if (clsParser.DecimalParse(tbxTotalConfirmado.Text) > 0)
                {
                    tbxSituacao.Text = "S";
                }
                else
                {
                    tbxSituacao.Text = "A";
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

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
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
                disposing = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (OrcamentoMovimenta() == true)
                {
                    id = Int32.Parse(rows[0].Cells[0].Value.ToString());
                    OrcamentoCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Primeiro Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (OrcamentoMovimenta() == true)
                {
                    foreach (DataGridViewRow dgvr in rows)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index != 0)
                            {
                                id = Int32.Parse(rows[rows.GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    OrcamentoCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Registro Anterior:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspProximo_Click(object sender, EventArgs e)
        {
            try
            {
                if (OrcamentoMovimenta() == true)
                {
                    foreach (DataGridViewRow dgvr in rows)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index < rows.Count - 1)
                            {
                                id = Int32.Parse(rows[rows.GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    OrcamentoCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Próximo Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tspUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (OrcamentoMovimenta() == true)
                {
                    id = Int32.Parse(rows[rows.Count - 1].Cells[0].Value.ToString());
                    OrcamentoCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspOrcamento_Imprimir1_Click(object sender, EventArgs e)
        {
            OrcamentoSalvar();

            // Imprimir Orçamento
            
            frmCrystalReport frmCrystalReport;
            frmCrystalReport = new frmCrystalReport();
            ParameterFields parameters = new ParameterFields();
            // Cabeçalho
            ParameterDiscreteValue valor = new ParameterDiscreteValue();
            ParameterField field = new ParameterField();
            valor.Value = id;
            field.Name = "id";
            field.CurrentValues.Add(valor);
            parameters.Add(field);

            DialogResult resultado1;
            resultado1 = MessageBox.Show("Deseja Imprimir Foto no Orçamento ?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (resultado1 == DialogResult.Yes)
            {
                frmCrystalReport.Init(clsInfo.caminhorelatorios, "ORCAMENTO_FOTO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }
            else if (resultado1 == DialogResult.Cancel)
            {

            }
            else
            {
                frmCrystalReport.Init(clsInfo.caminhorelatorios, "ORCAMENTO_FOTO.RPT", "", parameters, "", "", "", clsInfo.conexaosqldados, clsInfo.conexaosqldados);
                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }


        }


        private void tspRetornar_Click(object sender, EventArgs e)
        {
            disposing = true;
            this.Close();
        }
        private void OrcamentoCarregar()
        {
            OrcamentoInfoOld = new clsOrcamentoInfo();


            if (id == 0)
            {
                OrcamentoInfo = new clsOrcamentoInfo();
                //OrcamentoInfo.cognome = "";
                OrcamentoInfo.data = DateTime.Now;
                OrcamentoInfo.email = "";
                OrcamentoInfo.emitente = "";
                OrcamentoInfo.filial = clsInfo.zfilial;
                OrcamentoInfo.id = 0;
                OrcamentoInfo.idcliente = clsInfo.zempresaclienteid;
                OrcamentoInfo.idcondpag = clsInfo.zcondpagto;
                OrcamentoInfo.idresponsavel = clsInfo.zusuarioid;
                OrcamentoInfo.idvendedor = clsInfo.zempresaclienteid;
                OrcamentoInfo.motivo = "";
                OrcamentoInfo.numero = 0; // clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select top 1 numero from orcamento order by numero desc")) + 1;
                OrcamentoInfo.observa = "";
                OrcamentoInfo.observa1 = "";
                OrcamentoInfo.observa2 = "";
                OrcamentoInfo.observa3 = "";
                OrcamentoInfo.observa4 = "";
                OrcamentoInfo.observaa = "";
                OrcamentoInfo.pordesc = 0;
                OrcamentoInfo.qtdeconfirmada = 0;
                OrcamentoInfo.qtdeorcada = 0;
                OrcamentoInfo.qtdeorcadanao = 0;
                OrcamentoInfo.referencia = "";
                OrcamentoInfo.sedex = 0;
                OrcamentoInfo.setor = "N";
                OrcamentoInfo.situacao = "A";
                OrcamentoInfo.totalconfirmado = 0;
                OrcamentoInfo.totaldesconto = 0;
                OrcamentoInfo.totalfrete = 0;
                OrcamentoInfo.totalorcamentobruto = 0;
                OrcamentoInfo.totalorcamentoliquido = 0;
                OrcamentoInfo.totalpeso = 0;
                OrcamentoInfo.validade = "";
            }
            else
            {
                OrcamentoInfo = OrcamentoBLL.Carregar(id, clsInfo.conexaosqldados);
            }

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
                //switch (dgvOrcamento1.Rows[X].Cells["QTDECONFIRMADAITEM"].Value.ToString())
                //{
                //    case "00":
                //        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.White;
                //        break;
                //    case "09":
                //        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.PaleGoldenrod;
                //        break;
                //    case "10":
                //        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.PaleGoldenrod;
                //        break;
                //    case "20":
                //        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.PaleTurquoise;
                //        break;
                //    case "30":
                //        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.Thistle;
                //        break;
                //    case "40":
                //        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.PowderBlue;
                //        break;
                //    case "44":
                //        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                //        break;
                //    case "45":
                //        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.DeepSkyBlue;
                //        break;
                //    case "90":
                //        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.LightSalmon;
                //        break;
                //    default:    // > 11 e < 22
                //        dgvNFCompra1.Rows[X].DefaultCellStyle.BackColor = Color.Aquamarine;
                //        break;
                //}
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
            Cliente_Carrega();
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
        }
        private void Orcamento1FillInfoToGrid(clsOrcamento1Info info)
        {
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

            row["id"] = info.id;
            row["desconto"] = info.desconto;
            row["descricao1"] = info.descricao1;
            row["descricao2"] = info.descricao2;
            row["descricao3"] = info.descricao3;
            row["descricao4"] = info.descricao4;
            row["foto"] = clsParser.DecodificarFotoDatatablerowcell(info.foto);
            row["idcodigo"] = info.idcodigo;
            row["idorcamento"] = info.idorcamento;
            row["idordemservico"] = info.idordemservico;
            row["idpedido"] = info.idpedido;
            row["idpedidoitem"] = info.idpedidoitem;
            row["item"] = info.item;
            row["motivo"] = info.motivo;
            row["peso"] = info.peso;
            row["preco"] = info.preco;
            row["precoconfirmado"] = info.precoconfirmado;
            row["precoliquido"] = info.precoliquido;
            row["qtde"] = info.qtde;
            row["qtdeconfirmadaitem"] = info.qtdeconfirmadaitem;
            row["referencia1"] = info.referencia1;
            row["referencia2"] = info.referencia2;
            row["situacao"] = info.situacao;
            row["totalmercadoria"] = info.totalmercadoria;
            row["totalpeso"] = info.totalpeso;
            row["totalconfirmadoitem"] = info.totalconfirmadoitem;
            row["unid"] = info.unid;
            row["valordesconto"] = info.valordesconto;

            row["codigo"] = tbxCodigo.Text;
            row["descritab"] = tbxDescricao.Text; 

            if (rows.Length == 0)
            {
                row["orcamento1_posicao"] = orcamento1_posicao;
                dtOrcamento1.Rows.Add(row);
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
            info.foto = clsParser.DecodificarFotoDatatablerowcell(row["FOTO"]);
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

        private void btnIdCliente_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid =btnIdCliente.Name;
            frmClienteVis frmClienteVis = new frmClienteVis();
            frmClienteVis.Init("", idCliente);
            clsFormHelper.AbrirForm(this.MdiParent, frmClienteVis, clsInfo.conexaosqldados);

        }
        private void Cliente_Carrega()
        {
            tbxCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id=" + idCliente);
            tbxAtencao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from cliente where id = " + idCliente);
            tbxDDD.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ddd from ClienteContato where idcliente = " + idCliente);
            tbxTelefone.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select telefone from ClienteContato where idcliente = " + idCliente);
            tbxEmail.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select email from ClienteContato where idcliente = " + idCliente);
        }
        public Boolean OrcamentoMovimenta()
        {
            OrcamentoInfo = new clsOrcamentoInfo();
            OrcamentoFillInfo(OrcamentoInfo);

            if (OrcamentoBLL.Equals(OrcamentoInfoOld, OrcamentoInfo) == false)
            {
                DialogResult drt;
                drt = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    OrcamentoSalvar();
                }
                else if (drt == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private void tsbOrcamento1Vis_Incluir_Click(object sender, EventArgs e)
        {
            try
            {
                orcamento1_posicao = 0;
                Orcamento1Carregar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tbxDescricao4_TextChanged(object sender, EventArgs e)
        {

        }

        private void tspSalvar1_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult drt;
                drt = MessageBox.Show("Deseja salvar / alterar este item ?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    clsOrcamento1Info = new clsOrcamento1Info();
                    Orcamento1FillInfo(clsOrcamento1Info);
                    Orcamento1FillInfoToGrid(clsOrcamento1Info);
                } 
                else if (drt == DialogResult.Cancel)
                {
                    return;
                }
                gbxItemOrcamento.Visible = false;
                gbxCabecalho.Visible = true;
                tclOrcamento.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspPrimeiro1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desabilitado");
        }

        private void tspAnterior1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desabilitado");
        }

        private void tspProximo1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desabilitado");
        }

        private void tspUltimo1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desabilitado");
        }

        private void tspRetornar1_Click(object sender, EventArgs e)
        {
            gbxItemOrcamento.Visible = false;
            gbxCabecalho.Visible = true;
            tclOrcamento.SelectedIndex = 0;
        }
        private void Orcamento1Carregar()
        {
            // CARREGA CABEÇALHO
            tclOrcamento.SelectedIndex = 1;
            gbxCabecalho.Visible = false;
            gbxItemOrcamento.Visible = true;
            
            tbxNroOrcamento.Text = tbxNumero.Text;
            tbxDataEmissaoItem.Text = tbxData.Text;
            tbxCogCliProsItem.Text = tbxCognome.Text;
            tbxNomeCliProsItem.Text = tbxAtencao.Text;

            clsOrcamento1Info = new clsOrcamento1Info();
            clsOrcamento1InfoOld = new clsOrcamento1Info();

            if (orcamento1_posicao == 0)
            {
                clsOrcamento1Info.desconto = 0;
                clsOrcamento1Info.descricao1 = "";
                clsOrcamento1Info.descricao2 = "";
                clsOrcamento1Info.descricao3 = "";
                clsOrcamento1Info.descricao4 = "";
                clsOrcamento1Info.foto = null;
                //clsOrcamento1Info.fotocodigo = "";
                clsOrcamento1Info.id = 0;
                clsOrcamento1Info.idcodigo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecas where codigo='0'"));
                clsOrcamento1Info.idorcamento = id;
                clsOrcamento1Info.idordemservico = clsInfo.zordemservico;
                clsOrcamento1Info.idordemservicoitem = 0;
                clsOrcamento1Info.idpedido = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pedido where numero = 0"));
                clsOrcamento1Info.idpedidoitem = 0;
                clsOrcamento1Info.item = dtOrcamento1.Rows.Count + 1;
                clsOrcamento1Info.motivo = "";
                clsOrcamento1Info.peso = 0;
                clsOrcamento1Info.preco = 0;
                clsOrcamento1Info.precoconfirmado = 0;
                clsOrcamento1Info.precoliquido = 0;
                clsOrcamento1Info.qtde = 1;
                clsOrcamento1Info.qtdeconfirmadaitem = 0;
                clsOrcamento1Info.referencia1 = "";
                clsOrcamento1Info.referencia2 = "";
                clsOrcamento1Info.situacao = "A";
                clsOrcamento1Info.totalconfirmadoitem = 0;
                clsOrcamento1Info.totalmercadoria = 0;
                clsOrcamento1Info.totalpeso = 0;
                clsOrcamento1Info.unid = "";
                clsOrcamento1Info.valordesconto = 0;
                orcamento1_posicao = dtOrcamento1.Rows.Count + 1;
            }
            else
            {
                Orcamento1FillGridToInfo(clsOrcamento1Info, orcamento1_posicao);
            }

            Orcamento1Campos(clsOrcamento1Info);
            Orcamento1FillInfo(clsOrcamento1InfoOld);
        }
        private void Orcamento1Campos(clsOrcamento1Info info)
        {
            orcamento1_id = info.id;
            tbxDesconto.Text = info.desconto.ToString("N4");
            tbxDescricao1.Text = info.descricao1;
            tbxDescricao2.Text = info.descricao2;
            tbxDescricao3.Text = info.descricao3;
            tbxDescricao4.Text = info.descricao4;
            idcodigo =  info.idcodigo;
            clsPecasInfo = clsPecasBLL.Carregar(idcodigo, clsInfo.conexaosqldados);
            tbxCodigo.Text = clsPecasInfo.codigo;
            tbxDescricao.Text = clsPecasInfo.nome;
            pbxFoto.Image = clsPecasInfo.foto;
            id = info.idorcamento;
            idordemservico = info.idordemservico;
            idordemservicoitem = info.idordemservicoitem;
            idpedido = info.idpedido;
            idpedidoitem = info.idpedidoitem;
            tbxItem.Text = info.item.ToString("N0");
            //  info.motivo;
            tbxPeso.Text = info.peso.ToString("N3");
            tbxPreco.Text = info.preco.ToString("N2"); 
            tbxPrecoConfirmado.Text = info.precoconfirmado.ToString("N2"); ;
            tbxPrecoLiquido.Text = info.precoliquido.ToString("N2"); 
            tbxQtde.Text = info.qtde.ToString("N3"); ;
            tbxQtdeConfirmadaItem.Text = info.qtdeconfirmadaitem.ToString("N3"); ;
            tbxReferencia1.Text = info.referencia1;
            tbxReferencia2.Text = info.referencia2;
            //tbxSituacao.Text = info.situacao;
            tbxTotalConfirmadoItem.Text = info.totalconfirmadoitem.ToString("N2");
            tbxTotalMercadoria.Text = info.totalmercadoria.ToString("N2");
            tbxTotalPeso.Text = info.totalpeso.ToString("N3");
            tbxUnid.Text = info.unid;
            tbxValorDesconto.Text = info.valordesconto.ToString("N2");

        }
        private void Orcamento1FillInfo(clsOrcamento1Info info)
        {
            info.id = orcamento1_id;

            info.desconto = clsParser.DecimalParse(tbxDesconto.Text);
            info.descricao1 = tbxDescricao1.Text;
            info.descricao2 = tbxDescricao2.Text;
            info.descricao3 = tbxDescricao3.Text;
            info.descricao4 = tbxDescricao4.Text;
            info.foto = pbxFoto.Image;
            info.idcodigo = idcodigo;
            info.idorcamento = id;
            info.idordemservico = idordemservico;
            info.idordemservicoitem = idordemservicoitem;
            info.idpedido = idpedido;
            info.idpedidoitem = idpedidoitem;
            info.item = clsParser.Int32Parse(tbxItem.Text);
            //info.motivo = 
            info.peso = clsParser.DecimalParse(tbxPeso.Text);
            info.preco = clsParser.DecimalParse(tbxPreco.Text);
            info.precoconfirmado = clsParser.DecimalParse(tbxPrecoConfirmado.Text);
            info.precoliquido = clsParser.DecimalParse(tbxPrecoLiquido.Text);
            info.qtde = clsParser.DecimalParse(tbxQtde.Text);
            info.qtdeconfirmadaitem = clsParser.DecimalParse(tbxQtdeConfirmadaItem.Text);
            info.referencia1 = tbxReferencia1.Text;
            info.referencia2 = tbxReferencia2.Text;
            //info.situacao = tbx
            info.totalconfirmadoitem = clsParser.DecimalParse(tbxTotalConfirmadoItem.Text);
            info.totalmercadoria = clsParser.DecimalParse(tbxTotalMercadoria.Text);
            info.totalpeso = clsParser.DecimalParse(tbxTotalPeso.Text);
            info.unid = tbxUnid.Text;
            info.valordesconto = clsParser.DecimalParse(tbxValorDesconto.Text);
        }

        private void btnCodigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCodigo.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(idcodigo);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }

        private void tbxDescricao1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tsbOrcamento1Vis_Alterar_Click(object sender, EventArgs e)
        {
            if (dgvOrcamento1.CurrentRow != null)
            {
                orcamento1_posicao = Int32.Parse(dgvOrcamento1.CurrentRow.Cells["orcamento1_posicao"].Value.ToString());
                Orcamento1Carregar();
            }

        }

        private void tsbOrcamento1Vis_Excluir_Click(object sender, EventArgs e)
        {
            if (dgvOrcamento1.CurrentRow != null)
            {
                DialogResult drt;

                drt = MessageBox.Show("Deseja o excluir item selecionado?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    dtOrcamento1.Select("orcamento1_posicao = " + dgvOrcamento1.CurrentRow.Cells["orcamento1_posicao"].Value.ToString())[0].Delete();
                    Orcamento_Somar();
                }
            }

        }
        private void Orcamento_Somar()
        {
            tbxQtdeOrcada.Text = "0";
            tbxQtdeConfirmada.Text= "0";
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

        private void dgvOrcamento1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tsbOrcamento1Vis_Alterar.PerformClick();
        }

        private void dgvOrcamento1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsbOrcamento1Vis_Alterar.PerformClick();
        }

        private void gbxItemOrcamento_Enter(object sender, EventArgs e)
        {

        }

        private void pbxFoto_Click(object sender, EventArgs e)
        {

        }
    }
}
