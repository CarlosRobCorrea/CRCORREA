using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmNFCompraResumida : Form
    {
        
        
        
        
        clsFinanceiro clsFinanceiro = new clsFinanceiro();
        clsBasicReport clsBr;

        SqlConnection scn;
        SqlCommand scd;

        BackgroundWorker bwrNFCompraResumida;

        Int32 id;
        Int32 idbancoint;
        Int32 idcentrocusto;
        Int32 idcodigo;
        Int32 idcodigoctabil;
        Int32 iddocumento;
        Int32 idfluxo;
        Int32 idfluxo01;
        Int32 idfornecedor;
        Int32 idhistorico;

        String filtro_status;

        static DataTable dtNFCompraResumida;

        clsNFCompraResumidaBLL clsNFCompraResumidaBLL;
        clsNFCompraResumidaInfo clsNFCompraResumidaInfo;
        clsNFCompraResumidaInfo clsNFCompraResumidaInfoOld;

        Boolean carregandoNFCompraResumida;

        public frmNFCompraResumida()
        {
            InitializeComponent();
        }

        public void Init()
        {
            clsBr = new clsBasicReport(this, dgvNFCompraResumida, toolTip1, gbxNFCompraResumida);
            carregandoNFCompraResumida = false;
            clsNFCompraResumidaBLL = new clsNFCompraResumidaBLL();
            clsInfo.zrowid = 0;

            nMes.Text = DateTime.Now.Month.ToString().PadLeft(2, '0');
            nAno.Text = DateTime.Now.Year.ToString();


            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxFornecedor_Cognome);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", tbxPecas_Codigo);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from historicos order by codigo", tbxHistorico);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select codigo from centrocustos order by codigo", tbxCentroCusto);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from contacontabil order by codigo", tbxContaContabil);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "select conta from bancos order by conta", tbxBanco_Conta);
        }

        private void frmNFCompraResumida_Load(object sender, EventArgs e)
        {
            rbnDoMes.Checked = true;
        }

        private void frmNFCompraResumida_Activated(object sender, EventArgs e)
        {
            bwrNFCompraResumida_Run();
            TrataCampos((Control)sender);
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            id = 0;
            gbxNFCompraResumida.Enabled = false;
            tclNfCompraResumida.SelectedIndex = 1;
            gbxNFCompraItem.Visible = true;
            NFCompraResumidaCarregar();
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvNFCompraResumida.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvNFCompraResumida.CurrentRow.Cells["ID"].Value.ToString());
                gbxNFCompraResumida.Enabled = false;
                tclNfCompraResumida.SelectedIndex = 1;
                gbxNFCompraItem.Visible = true;
                NFCompraResumidaCarregar();
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            TrataCampos((Control)sender);
        }

        private void ControlKeyDownHora(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownHora((TextBox)sender, e);
        }
        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void bwrNFCompraResumida_Run()
        {
            if (carregandoNFCompraResumida == false)
            {
                carregandoNFCompraResumida = true;
                pbxNFCompraResumida.Visible = true;
                bwrNFCompraResumida = new BackgroundWorker();
                bwrNFCompraResumida.DoWork += new DoWorkEventHandler(bwrNFCompraResumida_DoWork);
                bwrNFCompraResumida.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrNFCompraResumida_RunWorkerCompleted);
                bwrNFCompraResumida.RunWorkerAsync();
            }
        }


        private void bwrNFCompraResumida_DoWork(object sender, DoWorkEventArgs e)
        {
            if (rbnTodos.Checked == true)
            {
                filtro_status = "T";
            }
            else 
            {
                filtro_status = "A";
            }

            dtNFCompraResumida = clsNFCompraResumidaBLL.GridDados(Int32.Parse(nMes.Text) , Int32.Parse(nAno.Text), clsInfo.zfilial, filtro_status);
        }

        private void bwrNFCompraResumida_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                pbxNFCompraResumida.Visible = false;
                clsNFCompraResumidaBLL.GridMonta(dgvNFCompraResumida, dtNFCompraResumida, clsInfo.zrowid);
                clsGridHelper.SelecionaLinha(id, dgvNFCompraResumida, 1);
                for (int x = 0; x < dgvNFCompraResumida.Rows.Count; x++)
                {
                    if (clsParser.Int32Parse(dgvNFCompraResumida.Rows[x].Cells["IDFLUXO"].Value.ToString()) > 0)
                    {
                        dgvNFCompraResumida.Rows[x].DefaultCellStyle.BackColor = System.Drawing.Color.SpringGreen;
                    }
                }

                carregandoNFCompraResumida = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoNFCompraResumida = false;
            }
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            if (clsParser.Int32Parse(tbxNumero.Text) > 0)
            {
                if (clsParser.DecimalParse(tbxTotalNota.Text) > 0)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                    gbxNFCompraResumida.Enabled = true;
                    tclNfCompraResumida.SelectedIndex = 0;
                    gbxNFCompraItem.Visible = false;

                    bwrNFCompraResumida_Run();

                }
                else
                {
                    MessageBox.Show("Acerte o Valor do documento !!");
                }
            }
            else
            {
                MessageBox.Show("Não pode incluir um documento sem numero !!!!");
            }
        }

        private void tspRetornarItem_Click(object sender, EventArgs e)
        {
            gbxNFCompraResumida.Enabled = true;
            tclNfCompraResumida.SelectedIndex = 0;
            gbxNFCompraItem.Visible = false;
        }

        void TrataCampos(Control ctl)
        {
            nMes.Text = clsParser.Int32Parse(nMes.Text).ToString();
            nAno.Text = clsParser.Int32Parse(nAno.Text).ToString();
            if (ctl.Name == nMes.Name || ctl.Name == nAno.Name)
            {
                bwrNFCompraResumida_Run();
            }
            tbxNumero.Text = clsParser.Int32Parse(tbxNumero.Text).ToString();
            tbxTotalNota.Text = clsParser.DecimalParse(tbxTotalNota.Text).ToString("N2");

            if (ctl.Name == tbxFornecedor_Cognome.Name)
            {
                idfornecedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CLIENTE where COGNOME='" + tbxFornecedor_Cognome.Text + "'"));
                if (idfornecedor == 0)
                {
                    idfornecedor = clsInfo.zempresaclienteid;
                }
                tbxFornecedor_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select COGNOME from CLIENTE where ID=" + idfornecedor + " ");
                tbxFornecedor_cnpj.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cgc from CLIENTE where ID=" + idfornecedor + " ");
            }
            if (ctl.Name == tbxPecas_Codigo.Name)
            {
                idcodigo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + tbxPecas_Codigo.Text + "'"));
                if (idcodigo == 0)
                {
                    idcodigo = clsInfo.zpecas;
                }
                tbxPecas_Codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS where ID=" + idcodigo + " ");
                tbxPecas_Nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS where ID=" + idcodigo + " ");
                // Puxar 
                idhistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDHISTORICOBCO from PECAS where ID=" + idcodigo + ""));
                tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID=" + idhistorico + " ");
                idcentrocusto = clsInfo.zcentrocustos;
                tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID=" + idcentrocusto + " ");
                idcodigoctabil = clsInfo.zcontacontabil;
                tbxContaContabil.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from CONTACONTABIL where ID=" + idcodigoctabil + " ");
            }
            if (ctl.Name == tbxHistorico.Name)
            {
                idhistorico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from HISTORICOS where CODIGO='" + tbxHistorico.Text + "'"));
                if (idhistorico == 0)
                {
                    idhistorico = clsInfo.zhistoricos;
                }
                tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from HISTORICOS where ID=" + idhistorico + " ");
            }
            if (ctl.Name == tbxCentroCusto.Name)
            {
                idcentrocusto = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from CENTROCUSTOS where CODIGO='" + tbxCentroCusto.Text + "'"));
                if (idcentrocusto == 0)
                {
                    idcentrocusto = clsInfo.zcentrocustos;
                }
                tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID=" + idcentrocusto + " ");
            }
            if (ctl.Name == tbxContaContabil.Name)
            {
                idcodigoctabil = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from CONTACONTABIL where CODIGO='" + tbxContaContabil.Text + "'"));
                if (idcodigoctabil == 0)
                {
                    idcodigoctabil = clsInfo.zcontacontabil;
                }
                tbxContaContabil.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from CONTACONTABIL where ID=" + idcodigoctabil + " ");
            }
            if (ctl.Name == tbxBanco_Conta.Name)
            {
                idbancoint = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select ID from BANCOS where conta=" + clsParser.Int32Parse(tbxBanco_Conta.Text) + " "));
                if (idbancoint == 0)
                {
                    idbancoint = clsInfo.zbancoint;
                }
                tbxBanco_Conta.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idbancoint + " ");
                tbxBanco_Nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from BANCOS where ID=" + idbancoint + " ");
            }
            if (clsInfo.znomegrid == btnIdDocumento.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        iddocumento = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxDocumento.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    }
                }
                tbxDocumento.Select();
            }
            if (clsInfo.znomegrid == btnIdCliente.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idfornecedor = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                       tbxFornecedor_Cognome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                    }
                }
                tbxFornecedor_Cognome.Select();
            }
            if (clsInfo.znomegrid == btnIdDespesa.Name)
            {
                if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                {
                    idcodigo = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxPecas_Codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxPecas_Nome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                }
                tbxPecas_Codigo.Select();
            }
            if (clsInfo.znomegrid == btnIdHistorico.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idhistorico = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxHistorico.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    }
                }
                tbxHistorico.Select();
            }
            if (clsInfo.znomegrid == btnIdCentroCusto.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idcentrocusto = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxCentroCusto.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    }
                }
                tbxCentroCusto.Select();
            }
            if (clsInfo.znomegrid == btnIdCodigoCtaBil.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idcodigoctabil = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        tbxContaContabil.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    }
                }
                tbxContaContabil.Select();
            }
            if (clsInfo.znomegrid == btnIdBancoInt.Name)
            {
                if (clsInfo.zrow != null)
                {
                    idbancoint = 0;
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        idbancoint = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    }
                    if (idbancoint == 0)
                    {
                        idbancoint = clsInfo.zbancoint;
                    }
                    tbxBanco_Conta.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CONTA from BANCOS where ID=" + idbancoint + " ");
                    tbxBanco_Nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from BANCOS where ID=" + idbancoint + " ");

                }
                tbxContaContabil.Select();
            }

            if (ctl.Name == tbxNumero.Name || ctl.Name == tbxFornecedor_Cognome.Name)
            {
                if (clsParser.Int32Parse(tbxNumero.Text) > 0)
                {
                    //VERIFICAR SE JA EXISTE ALGUM DOCUMENTO LANÇADO COM ESTE NUMERO
                    Int32 idChecar = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from NFCOMPRA where NUMERO=" + clsParser.Int32Parse(tbxNumero.Text) + " and IDFORNECEDOR =" + idfornecedor + " "));
                    if (idChecar == 0)
                    {
                        idChecar = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from NFCOMPRARESUMIDA where NUMERO=" + clsParser.Int32Parse(tbxNumero.Text) + " and IDFORNECEDOR =" + idfornecedor + " and ID <>" + id + " "));
                    }
                    if (idChecar > 0)
                    {
                        MessageBox.Show("Este documento já existe com este fornecedor !!!");
                        tbxNumero.Text = "0";
                        tbxNumero.Select();
                        tbxNumero.SelectAll();
                    }
                }
            }
            ctl.Name = "";
            clsInfo.znomegrid = "";
            clsInfo.zrow = null;


        }
        private void NFCompraResumidaCarregar()
        {
            clsNFCompraResumidaInfoOld = new clsNFCompraResumidaInfo();

            if (id == 0)
            {
                clsNFCompraResumidaInfo = new clsNFCompraResumidaInfo();
                clsNFCompraResumidaInfo.complemento = "";
                clsNFCompraResumidaInfo.data = DateTime.Now;
                clsNFCompraResumidaInfo.datalanca = DateTime.Now;
                clsNFCompraResumidaInfo.emitente = clsInfo.zusuario;
                clsNFCompraResumidaInfo.filial = clsInfo.zfilial;
                clsNFCompraResumidaInfo.id = 0;
                clsNFCompraResumidaInfo.idbancoint = clsInfo.zbancoint;
                clsNFCompraResumidaInfo.idcentrocusto = clsInfo.zcentrocustos;
                clsNFCompraResumidaInfo.idcodigo = clsInfo.zpecas;
                clsNFCompraResumidaInfo.idcodigoctabil = clsInfo.zcontacontabil;
                clsNFCompraResumidaInfo.iddocumento = 0;
                clsNFCompraResumidaInfo.idfluxo = 0;
                clsNFCompraResumidaInfo.idfluxo01 = 0;
                clsNFCompraResumidaInfo.idfornecedor = clsInfo.zempresaclienteid;
                clsNFCompraResumidaInfo.idhistorico = clsInfo.zhistoricos;
                clsNFCompraResumidaInfo.numero = 0;
                clsNFCompraResumidaInfo.totalnota = 0;
            }
            else
            {
                clsNFCompraResumidaInfo = clsNFCompraResumidaBLL.Carregar(id, clsInfo.conexaosqldados);
            }
            NFCompraResumidaCampos(clsNFCompraResumidaInfo);
            NFCompraResumidaFillInfo(clsNFCompraResumidaInfoOld);

            tbxNumero.Select();
            tbxNumero.SelectAll();
        }

        private void NFCompraResumidaCampos(clsNFCompraResumidaInfo info)
        {
            id = info.id;
            idbancoint =  info.idbancoint;
                tbxBanco_Conta.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CONTA FROM BANCOS WHERE ID=" + idbancoint);
                tbxBanco_Nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT NOME FROM BANCOS WHERE ID=" + idbancoint);
            idcentrocusto = info.idcentrocusto;
                tbxCentroCusto.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM CENTROCUSTOS WHERE ID=" + idcentrocusto);
            idcodigo = info.idcodigo;
                tbxPecas_Codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM PECAS WHERE ID=" + idcodigo);
                tbxPecas_Nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT NOME FROM PECAS WHERE ID=" + idcodigo);
            idcodigoctabil = info.idcodigoctabil;
                tbxContaContabil.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CODIGO FROM CONTACONTABIL WHERE ID=" + idcodigoctabil);
            iddocumento = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT ID FROM DOCFISCAL WHERE COGNOME='" + "NFEZ" + "'"));
            if (iddocumento == 0)
            {
                MessageBox.Show("Cadastre um Documento Fiscal de [NFEZ] (Nota Fiscal Entrada Especial)");
                this.Close();
            }
            tbxDocumento.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM DOCFISCAL WHERE ID=" + iddocumento);
            idfluxo = info.idfluxo;
            tbxidFluxo.Text = idfluxo.ToString();
            idfluxo01 = info.idfluxo01;
            tbxidFluxo01.Text = idfluxo01.ToString();
            idfornecedor = info.idfornecedor;
                tbxFornecedor_Cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT COGNOME FROM CLIENTE WHERE ID=" + idfornecedor);
                tbxFornecedor_cnpj.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT CGC FROM CLIENTE WHERE ID=" + idfornecedor);
            idhistorico = info.idhistorico;
                tbxHistorico.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT CODIGO FROM HISTORICOS WHERE ID=" + idhistorico);

            tbxNumero.Text = info.numero.ToString();

            tbxComplemento.Text = info.complemento;
            tbxData.Text = info.data.ToString("dd/MM/yyyy");
            tbxDataLanca.Text = info.datalanca.ToString("dd/MM/yyyy");
            tbxEmitente.Text = info.emitente;
            tbxFilial.Text = info.filial.ToString();
            tbxTotalNota.Text = info.totalnota.ToString("N2");

            if (idfluxo > 0)
            {
                MessageBox.Show("Registro já foi Transferido - Não pode ser alterado");

                tspSalvar.Enabled = false;
                btnTransfere_Banco.Enabled = false;
            }
            else
            {
                tspSalvar.Enabled = true;
                btnTransfere_Banco.Enabled = true;
            }
        }

        private void NFCompraResumidaFillInfo(clsNFCompraResumidaInfo info)
        {
            info.id = id;
            info.idbancoint = idbancoint;
            info.idcentrocusto = idcentrocusto;
            info.idcodigo = idcodigo;
            info.idcodigoctabil = idcodigoctabil;
            info.iddocumento = iddocumento;
            info.idfluxo = idfluxo;
            info.idfluxo01 = idfluxo01;
            info.idfornecedor = idfornecedor;
            info.idhistorico = idhistorico;
            

            info.complemento = tbxComplemento.Text;
            info.data = DateTime.Parse(tbxData.Text);
            info.datalanca = DateTime.Parse(tbxDataLanca.Text);
            info.emitente = tbxEmitente.Text;
            info.filial = clsParser.Int32Parse(tbxFilial.Text);
            info.numero = clsParser.Int32Parse(tbxNumero.Text);
            info.totalnota = clsParser.DecimalParse(tbxTotalNota.Text);
        }

        private void dgvNFCompraResumida_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void btnIdDocumento_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdDocumento.Name;
            frmDocFiscalVis frmDocFiscalVis = new frmDocFiscalVis();
            frmDocFiscalVis.Init(0);

            clsFormHelper.AbrirForm(this.MdiParent, frmDocFiscalVis, clsInfo.conexaosqldados);
        }

        private void btnIdCliente_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCliente.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", idfornecedor);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnIdDespesa_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdDespesa.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(0);

            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }

        private void btnIdHistorico_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdHistorico.Name;
            frmHistoricosPes frmHistoricosPes = new frmHistoricosPes();
            frmHistoricosPes.Init(clsInfo.conexaosqlbanco, idhistorico);

            clsFormHelper.AbrirForm(this.MdiParent, frmHistoricosPes, clsInfo.conexaosqlbanco);
        }

        private void btnIdCentroCusto_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCentroCusto.Name;
            frmCentroCustosPes frmCentroCustosPes = new frmCentroCustosPes();
            frmCentroCustosPes.Init(clsInfo.conexaosqlbanco, idcentrocusto);

            clsFormHelper.AbrirForm(this.MdiParent, frmCentroCustosPes, clsInfo.conexaosqlbanco);
        }

        private void btnIdCodigoCtaBil_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdCodigoCtaBil.Name;
            frmContacontabilPes frmContacontabilPes = new frmContacontabilPes();
            frmContacontabilPes.Init(idcodigoctabil);

            clsFormHelper.AbrirForm(this.MdiParent, frmContacontabilPes, clsInfo.conexaosqldados);
        }

        private void btnIdBancoInt_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdBancoInt.Name;
            frmBancosPes frmBancosPes = new frmBancosPes();
            frmBancosPes.Init(idbancoint, clsInfo.conexaosqlbanco);

            clsFormHelper.AbrirForm(this.MdiParent, frmBancosPes, clsInfo.conexaosqlbanco);
        }

        private DialogResult Salvar()
        {
            DialogResult drt;
            
            drt = MessageBox.Show("Deseja Salvar este Registro e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            
            if (drt == DialogResult.Yes)
            {
                NFCompraResumidaSalvar();
            }

            return drt;
        }

        private void NFCompraResumidaSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: NFCOMPRARESUMIDA
                clsNFCompraResumidaInfo = new clsNFCompraResumidaInfo();
                NFCompraResumidaFillInfo(clsNFCompraResumidaInfo);

                if (id == 0)
                {
                    id = clsNFCompraResumidaBLL.Incluir(clsNFCompraResumidaInfo, clsInfo.conexaosqldados);
                }
                else
                {
                    clsNFCompraResumidaBLL.Alterar(clsNFCompraResumidaInfo, clsInfo.conexaosqldados);
                }
                //
                tse.Complete();
            }
        }

        private void btnTransfere_Banco_Click(object sender, EventArgs e)
        {
            // Salvar o registro
            if (clsParser.Int32Parse(tbxNumero.Text) > 0)
            {
                NFCompraResumidaSalvar();

                DialogResult drt;
                drt = MessageBox.Show("Deseja Salvar e Transferir este Registro para o Aplibank ?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
                if (drt == DialogResult.Yes)
                {
                    // Transferir para o APlibank
                    // Verificar se gravou os Id necessarios
                    if (clsParser.Int32Parse(tbxBanco_Conta.Text) > 0)
                    {
                        if (idhistorico > 0)
                        {
                            if (idcentrocusto > 0)
                            {
                                if (idcodigoctabil > 0)
                                {
                                    if (idbancoint > 0)
                                    {

                                        // transferindo para o Aplibank  ==> 1a Fase 
                                        String NroLote = "";
                                        String Comobaixou = "Codigo = " + tbxPecas_Codigo.Text.Trim() + " " + tbxComplemento.Text.Trim();
                                        if (Comobaixou.Length > 100)
                                        {
                                            Comobaixou = Comobaixou.Substring(0, 99);
                                        }
                                        DateTime databaixa = DateTime.Parse(tbxData.Text);
                                        DateTime datacredito = DateTime.Parse(tbxData.Text);
                                        TimeSpan Compensar;
                                        NroLote = "PAG" + DateTime.Now.ToString("yyyyMMddHHmmss") + clsParser.Int32Parse(tbxNumero.Text).ToString("0000000000") + clsInfo.zusuarioid.ToString("0000000");
                                        Compensar = datacredito.Subtract(databaixa);
                                        String nrocheque = "";
                                        Int32 conferido = 0;

                                        // Colocar o historico no complemento do lançamento
                                        String complemento = "";
                                        if (tbxHistorico.Text != "NC")
                                        {
                                            complemento = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select NOME from HISTORICOS where ID=" + idhistorico + " ");
                                        }
                                        if (tbxComplemento.Text.Length > 0)
                                        {
                                            if (complemento.Length > 0)
                                            {
                                                complemento = complemento + "-" + tbxComplemento.Text;
                                            }
                                            else
                                            {
                                                complemento = tbxComplemento.Text;
                                            }

                                        }
                                        if (tbxDocumento.Text.Length > 0)
                                        {
                                            if (complemento.Length > 0)
                                            {
                                                complemento = complemento + "-" + tbxDocumento.Text;
                                            }
                                            else
                                            {
                                                complemento = tbxDocumento.Text;
                                            }
                                        }
                                        if (tbxNumero.Text.Length > 0)
                                        {
                                            if (complemento.Length > 0)
                                            {
                                                complemento = complemento + "=" + tbxNumero.Text;
                                            }
                                            else
                                            {
                                                complemento = tbxNumero.Text;
                                            }
                                        }

                                        clsFinanceiro.BaixarBanco("PAGARNFEZ", id, NroLote, "0", idbancoint, idhistorico, idcentrocusto,
                                                                  idcodigoctabil, DateTime.Parse(tbxData.Text), DateTime.Parse(tbxData.Text),
                                                                  clsParser.DecimalParse(tbxTotalNota.Text), "00", "D", complemento,
                                                                  tbxFornecedor_Cognome.Text, "NFEZ",
                                                                  "NFEZ", tbxNumero.Text, 1, 1, Comobaixou, clsInfo.zusuario, nrocheque, conferido, "UNICA");

                                    }
                                    else
                                    {
                                        MessageBox.Show("Não adicionou a Conta do Banco Interno (Aplisoft)!!");
                                        //tbxBaixaBcoInt.Select();
                                        //tbxBaixaBcoInt.SelectAll();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Não adicionou a Conta Contabil na Baixa da Duplicata !!");
                                    //tbxBaixaContacontabil.Select();
                                    //tbxBaixaContacontabil.SelectAll();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Não adicionou o Centro de Custo na Baixa da Duplicata !!");
                                //tbxBaixaCentroCusto.Select();
                                //tbxBaixaCentroCusto.SelectAll();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Não adicionou o Historico na Baixa da Duplicata !!");
                            //tbxBaixaHistorico.Select();
                            //tbxBaixaHistorico.SelectAll();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Indique a Conta do Aplibank que vai efetuar o Pagamento !!");
                        //tbxBaixaBcoInt.Select();
                        //tbxBaixaBcoInt.SelectAll();
                    }
                    // Gravar como já transferido
                    // Retornar 
                    gbxNFCompraResumida.Enabled = true;
                    tclNfCompraResumida.SelectedIndex = 0;
                    gbxNFCompraItem.Visible = false;

                    bwrNFCompraResumida_Run();


                }
            }
            else
            {
                MessageBox.Show("Não pode incluir um documento sem numero !!!!");

            }
            
        }

        private void tspExtornar_Click(object sender, EventArgs e)
        {  // Extornar o Pagto
            
           // Vai no Banco e exclui do Fluxo e do Fluxo01
            scn = new SqlConnection(clsInfo.conexaosqlbanco);
            scd = new SqlCommand("DELETE FLUXO01 WHERE FLUXO=" + idfluxo , scn);
            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();
            scd = new SqlCommand("DELETE FLUXO WHERE ID=" + idfluxo , scn);
            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();
            // Acertar o Saldo do Aplibank
            clsFinanceiro.SaldoBanco(idbancoint, clsParser.DateTimeParse(tbxData.Text));
           // Excluir o Lançamento
            scn = new SqlConnection(clsInfo.conexaosqldados);
            scd = new SqlCommand("DELETE NFCOMPRARESUMIDA WHERE ID=" + id, scn);
            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();
            MessageBox.Show("Excluido do Aplibank e das Notas Fiscais Entrada Resumida");
            gbxNFCompraResumida.Enabled = true;
            tclNfCompraResumida.SelectedIndex = 0;
            gbxNFCompraItem.Visible = false;

            bwrNFCompraResumida_Run();

        }

        private void rbnTodos_Click(object sender, EventArgs e)
        {
            bwrNFCompraResumida_Run();
        }

        private void rbnDoMes_Click(object sender, EventArgs e)
        {
            bwrNFCompraResumida_Run();
        }
    }
}
