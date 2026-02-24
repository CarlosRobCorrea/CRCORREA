using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmSituacaocobrancacod : Form
    {
        String conexao;
        String conexao_banco;

        Int32 idsituacaocobrancacod;
        Int32 idhistoricopagar;
        Int32 idcentrocustopagar;
        Int32 idhistoricoreceber;
        Int32 idcentrocustoreceber;

        String[] valoresantigos = new String[] { };
        String[] valoresatuais = new String[] { };

        
        
        

        clsSelectInsertUpdateBLL clsSelectInsertUpdateBLL = new clsSelectInsertUpdateBLL();
        delegate void AddnewDelegate(TextBox textbox, TextBox colecao);

        DataTable dtSituacaocobrancacod1;
        clsSituacaocobrancacod1BLL clsSituacaocobrancacod1BLL;
        Int32 ixSituacaocobrancacod1;

        clsSituacaocobrancacodInfo clsSituacaocobrancacodInfoOld;
        DataGridViewRowCollection dgvrcnSituacaocobrancacod;

        public frmSituacaocobrancacod()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         String _conexao_banco,
                         Int32 _id,
                         DataGridViewRowCollection _rowsSituacaocobrancacod)
        {
            conexao = _conexao;
            conexao_banco = _conexao_banco;

            idsituacaocobrancacod = _id;
            dgvrcnSituacaocobrancacod = _rowsSituacaocobrancacod;

            clsSituacaocobrancacod1BLL = new clsSituacaocobrancacod1BLL();
        }

        private void frmSituacaocobrancacod_Load(object sender, EventArgs e)
        {
            // Carrega o(s) AutoComplete
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "", tbxHistoricopagar);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "", tbxHistoricoreceber);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "", tbxCentrocustopagar);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "", tbxCentrocustoreceber);

            CarregaSituacaocobrancacod(idsituacaocobrancacod);
        }

        private void CarregaSituacaocobrancacod(Int32 _idsituacaocobrancacod)
        {
            tspTool.Cursor = Cursors.Hand;
            PreencheCamposSituacaocobrancacod();
            valoresantigos = PreencheValoresSituacaocobrancacod();
            if (idsituacaocobrancacod == 0)
            {
                tspPrimeiro.Enabled = false;
                tspAnterior.Enabled = false;
                tspProximo.Enabled = false;
                tspUltimo.Enabled = false;
            }
            tbxCodigo.Select();
            tbxCodigo.SelectAll();

            /*
            clsSituacaocobrancacodInfoOld = new clsSituacaocobrancacodInfo();
            if (_id > 0) // Alterando / Visualizando
            {
                clsSituacaocobrancacodBLL clsSituacaocobrancacodBLL = new clsSituacaocobrancacodBLL();
                clsSituacaocobrancacodInfo clsSituacaocobrancacodInfo = new clsSituacaocobrancacodInfo();

                //Carrega os Dados
                clsSituacaocobrancacodInfo = clsSituacaocobrancacodBLL.Carregar(conexao, id);
                PreencheCamposSituacaocobrancacod(clsSituacaocobrancacodInfo);

                PreencheInfoSituacaocobrancacod(clsSituacaocobrancacodInfoOld);
            }
            else
            {
                tspPrimeiro.Enabled = false;
                tspAnterior.Enabled = false;
                tspProximo.Enabled = false;
                tspUltimo.Enabled = false;
            }

            tbxCodigo.Select();
            tbxCodigo.SelectAll();
             */



        }

        private void PreencheCamposSituacaocobrancacod()
        {
            if (idsituacaocobrancacod > 0)
            {
                DataRow drSituacaocobrancacod;
                drSituacaocobrancacod = clsSelectInsertUpdateBLL.SelectCollectionFields(clsInfo.conexaosqldados, "*", "SITUACAOCOBRANCACOD", "ID = " + clsParser.Int32Parse(idsituacaocobrancacod.ToString()), "ID");

                if (drSituacaocobrancacod != null)
                {
                    idcentrocustopagar = clsParser.Int32Parse(drSituacaocobrancacod["IDCENTROCUSTOPAGAR"].ToString());
                    idcentrocustoreceber = clsParser.Int32Parse(drSituacaocobrancacod["IDCENTROCUSTORECEBER"].ToString());
                    idhistoricopagar = clsParser.Int32Parse(drSituacaocobrancacod["IDHISTORICOPAGAR"].ToString());
                    idhistoricoreceber = clsParser.Int32Parse(drSituacaocobrancacod["IDHISTORICORECEBER"].ToString());

                    tbxCodigo.Text = drSituacaocobrancacod["CODIGO"].ToString();
                    tbxNome.Text = drSituacaocobrancacod["NOME"].ToString();
                }
            }
            if (idhistoricopagar == 0)
                idhistoricopagar = clsInfo.zhistoricos;

            tbxHistoricopagar.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from HISTORICOS where ID = " + idhistoricopagar, "");
            //tbxHistoricopagar.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", idhistoricopagar);
            
            if (idhistoricoreceber == 0)
                idhistoricoreceber = clsInfo.zhistoricos;

            tbxHistoricoreceber.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from HISTORICOS where ID = " + idhistoricoreceber, "");
            //tbxHistoricoreceber.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "CODIGO", "ID", idhistoricoreceber);

            if (idcentrocustopagar == 0)
                idcentrocustopagar = clsInfo.zcentrocustos;

            tbxCentrocustopagar.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select CODIGO from CENTROCUSTOS where ID = " + idcentrocustopagar, "");
            //tbxCentrocustopagar.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", idcentrocustopagar);

            if (idcentrocustoreceber == 0)
                idcentrocustoreceber = clsInfo.zcentrocustos;

            tbxCentrocustoreceber.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select codigo from CENTROCUSTOS where ID = " + idcentrocustoreceber, "");
            //tbxCentrocustoreceber.Text = Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "ID", idcentrocustoreceber);
        }

        private String[] PreencheValoresSituacaocobrancacod()
        {
            String[] _valores = { 
                                    tbxCodigo.Text,
                                    tbxNome.Text,           
                                    idcentrocustopagar.ToString(),
                                    idcentrocustoreceber.ToString(),
                                    idhistoricopagar.ToString(),
                                    idhistoricoreceber.ToString(),
                                };
            return _valores;
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            if (((Control)sender).Name == "tbxHistoricopagar")
            {
                idhistoricopagar = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from HISTORICOS where CODIGO='" + tbxHistoricopagar.Text + "'", "0"));
                //idhistoricopagar = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "ID", "CODIGO", tbxHistoricopagar.Text));

                if (idhistoricopagar == 0)
                    tbxHistoricopagar.Text = "";
            }
            else if (((Control)sender).Name == "tbxHistoricoreceber")
            {
                idhistoricoreceber = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "SELECT id from HISTORICOS where CODIGO='" +  tbxHistoricoreceber.Text + "'" , "0"));
                //idhistoricoreceber = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "HISTORICOS", "ID", "CODIGO", tbxHistoricoreceber.Text));

                if (idhistoricoreceber == 0)
                    tbxHistoricoreceber.Text = "";
            }
            else if (((Control)sender).Name == "tbxCentrocustopagar")
            {
                idcentrocustopagar = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from CENTROCUSTOS where codigo ='" + tbxCentrocustopagar.Text + "'" ,"0"));
                //idcentrocustopagar = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "ID", "CODIGO", tbxCentrocustopagar.Text));

                if (idcentrocustopagar == 0)
                    tbxCentrocustopagar.Text = "";
            }
            else if (((Control)sender).Name == "tbxCentrocustoreceber")
            {
                idcentrocustoreceber = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from CENTROCUSTOS where CODIGO='" + tbxCentrocustoreceber.Text + "'", "0"));
                // idcentrocustoreceber = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "ID", "CODIGO", tbxCentrocustoreceber.Text));
                if (idcentrocustoreceber == 0)
                    tbxCentrocustoreceber.Text = "";
            }

            clsVisual.ControlLeave(sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void btnIdHistoricoPagar_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvHistoricoPagar";
            frmHistoricosPes frmHistoricosPes = new frmHistoricosPes();
            frmHistoricosPes.Init(conexao_banco, idhistoricopagar);

             clsFormHelper.AbrirForm(this.MdiParent, frmHistoricosPes,conexao_banco);
            tbxHistoricopagar.Select();
            tbxHistoricopagar.SelectAll();
        }

        private void btnIdHistoricoReceber_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvHistoricoReceber";
            frmHistoricosPes frmHistoricosPes = new frmHistoricosPes();
            frmHistoricosPes.Init(conexao_banco, idhistoricoreceber);

             clsFormHelper.AbrirForm(this.MdiParent, frmHistoricosPes, conexao_banco);
            tbxHistoricoreceber.Select();
            tbxHistoricoreceber.SelectAll();
        }

        private void frmSituacaocobrancacod_Activated(object sender, EventArgs e)
        {
            if (clsInfo.zrow != null)
            {
                if (clsInfo.znomegrid == "dgvHistoricoPagar")
                {
                    idhistoricopagar = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxHistoricopagar.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                }
                else if (clsInfo.znomegrid == "dgvHistoricoReceber")
                {
                    idhistoricoreceber = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxHistoricoreceber.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                }
                else if (clsInfo.znomegrid == "dgvCentroCustoPagar")
                {
                    idcentrocustopagar = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCentrocustopagar.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                }
                else if (clsInfo.znomegrid == "dgvCentroCustoReceber")
                {
                    idcentrocustoreceber = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxCentrocustoreceber.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                }
                clsInfo.zrow = null;
            }
            clsInfo.znomegrid = "";
        }

        private void btnIdCentroCustoPagar_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCentroCustoPagar";
            frmCentroCustosPes frmCentroCustosPes = new frmCentroCustosPes();
            frmCentroCustosPes.Init(conexao_banco, idcentrocustopagar);

             clsFormHelper.AbrirForm(this.MdiParent, frmCentroCustosPes,conexao_banco);
            tbxCentrocustopagar.Select();
            tbxCentrocustopagar.SelectAll();
        }

        private void btnIdCentroCustoReceber_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCentroCustoReceber";
            frmCentroCustosPes frmCentroCustosPes = new frmCentroCustosPes();
            frmCentroCustosPes.Init(conexao_banco, idcentrocustoreceber);

             clsFormHelper.AbrirForm(this.MdiParent, frmCentroCustosPes,conexao_banco);
            tbxCentrocustoreceber.Select();
            tbxCentrocustoreceber.SelectAll();
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);

                if (resultado == DialogResult.Yes)
                {
                    SalvarSituacaocobrancacod();
                }
                else if (resultado == DialogResult.Cancel)
                {
                    tbxCodigo.Select();
                    tbxCodigo.SelectAll();
                    return;
                }
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tbxCodigo.Select();
                tbxCodigo.SelectAll();
            }
        }

        private void SalvarSituacaocobrancacod()
        {
            if (idsituacaocobrancacod == 0)    // Incluindo os valores do Cabeçalho -- valores locais
            {
                idsituacaocobrancacod = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD",
                                                                        "CODIGO," +
                                                                        "NOME," +
                                                                        "IDHISTORICOPAGAR, " +
                                                                        "IDCENTROCUSTOPAGAR, " +
                                                                        "IDHISTORICORECEBER, " +
                                                                        "IDCENTROCUSTORECEBER " ,
                                                         clsParser.SqlStringFormat(tbxCodigo.Text, false, "CODIGO") +
                                                         clsParser.SqlStringFormat(tbxNome.Text, false, "NOME") +
                                                         clsParser.SqlInt32Format(idhistoricopagar.ToString(), false, "IDHISTORICOPAGAR") +
                                                         clsParser.SqlInt32Format(idcentrocustopagar.ToString(), false, "IDCENTROCUSTOPAGAR") +
                                                         clsParser.SqlInt32Format(idhistoricoreceber.ToString(), false, "IDHISTORICORECEBER") +
                                                         clsParser.SqlInt32Format(idcentrocustoreceber.ToString(), true, "IDCENTROCUSTORECEBER"));
            }
            else
            {
                // Alterando os valores do Cabeçalho -- valores locais
                clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "SITUACAOCOBRANCACOD",
                                                        "CODIGO = " + clsParser.SqlStringFormat(tbxCodigo.Text, false, "CODIGO") +
                                                        "NOME = " + clsParser.SqlStringFormat(tbxNome.Text, false, "NOME") +
                                                        "IDHISTORICOPAGAR = " + clsParser.SqlInt32Format(idhistoricopagar.ToString(), false, "IDHISTORICOPAGAR") +
                                                        "IDCENTROCUSTOPAGAR = " + clsParser.SqlInt32Format(idcentrocustopagar.ToString(), false, "IDCENTROCUSTOPAGAR") +
                                                        "IDHISTORICORECEBER = " + clsParser.SqlInt32Format(idhistoricoreceber.ToString(), false, "IDHISTORICORECEBER") +
                                                        "IDCENTROCUSTORECEBER = " + clsParser.SqlInt32Format(idcentrocustoreceber.ToString(), true, "IDCENTROCUSTORECEBER"),
                                                        "ID = " + idsituacaocobrancacod.ToString());
            }


            /*
            clsSituacaocobrancacodInfo clsSituacaocobrancacodInfo = new clsSituacaocobrancacodInfo();
            clsSituacaocobrancacodBLL clsSituacaocobrancacodBLL = new clsSituacaocobrancacodBLL();

            PreencheInfoSituacaocobrancacod(clsSituacaocobrancacodInfo);
            if (id == 0)    // Incluindo
            {
                id = clsSituacaocobrancacodBLL.Incluir(conexao, clsSituacaocobrancacodInfo);


            }
            else
            {
                // Verifica se algo foi alterado, se foi, Salva
                if (clsSituacaocobrancacodBLL.ComparaInfo(clsSituacaocobrancacodInfoOld, clsSituacaocobrancacodInfo) == false)
                {
                    clsSituacaocobrancacodBLL.Alterar(conexao, clsSituacaocobrancacodInfo);
                }
            }
             */
        }

        public Boolean MovimentaSituacaocobrancacod()
        {
            /*
            clsSituacaocobrancacodBLL clsSituacaocobrancacodBLL = new clsSituacaocobrancacodBLL();
            clsSituacaocobrancacodInfo clsSituacaocobrancacodInfo = new clsSituacaocobrancacodInfo();
            PreencheInfoSituacaocobrancacod(clsSituacaocobrancacodInfo);
            if (clsSituacaocobrancacodBLL.ComparaInfo(clsSituacaocobrancacodInfoOld, clsSituacaocobrancacodInfo) == false)
            {
                DialogResult drt;
                drt = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    SalvarSituacaocobrancacod();
                }
                else if (drt == DialogResult.Cancel)
                {
                    tbxCodigo.Select();
                    tbxCodigo.SelectAll();
                    return false;
                }
            }
            return true;
             * */
            return true;
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (MovimentaSituacaocobrancacod())
                {
                    idsituacaocobrancacod = Int32.Parse(dgvrcnSituacaocobrancacod[0].Cells[0].Value.ToString());
                    CarregaSituacaocobrancacod(idsituacaocobrancacod);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Primeiro Registro:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tspAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (MovimentaSituacaocobrancacod())
                {
                    foreach (DataGridViewRow dgvr in dgvrcnSituacaocobrancacod)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == idsituacaocobrancacod)
                        {
                            if (dgvr.Index != 0)
                            {
                                idsituacaocobrancacod = Int32.Parse(dgvrcnSituacaocobrancacod[dgvrcnSituacaocobrancacod.GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaSituacaocobrancacod(idsituacaocobrancacod);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Registro Anterior:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tspProximo_Click(object sender, EventArgs e)
        {
            try
            {
                if (MovimentaSituacaocobrancacod())
                {
                    foreach (DataGridViewRow dgvr in dgvrcnSituacaocobrancacod)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == idsituacaocobrancacod)
                        {
                            if (dgvr.Index < dgvrcnSituacaocobrancacod.Count - 1)
                            {
                                idsituacaocobrancacod = Int32.Parse(dgvrcnSituacaocobrancacod[dgvrcnSituacaocobrancacod.GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaSituacaocobrancacod(idsituacaocobrancacod);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Próximo Registro:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tspUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (MovimentaSituacaocobrancacod())
                {
                    idsituacaocobrancacod = Int32.Parse(dgvrcnSituacaocobrancacod[dgvrcnSituacaocobrancacod.Count - 1].Cells[0].Value.ToString());
                    CarregaSituacaocobrancacod(idsituacaocobrancacod);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
/*                clsSituacaocobrancacodBLL clsSituacaocobrancacodBLL = new clsSituacaocobrancacodBLL();
                clsSituacaocobrancacodInfo clsSituacaocobrancacodInfo = new clsSituacaocobrancacodInfo();
                PreencheInfoSituacaocobrancacod(clsSituacaocobrancacodInfo);

                if (clsSituacaocobrancacodBLL.ComparaInfo(clsSituacaocobrancacodInfo, clsSituacaocobrancacodInfoOld) == false)
                {
                    DialogResult resultado;
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (resultado == DialogResult.Yes)
                    {
                        SalvarSituacaocobrancacod();
                    }
                    else if (resultado == DialogResult.Cancel)
                    {
                        tbxCodigo.Select();
                        tbxCodigo.SelectAll();
                        return;
                    }
                }
 */
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tbxCodigo.Select();
                tbxCodigo.SelectAll();
            }
        }

        private void frmSituacaocobrancacod_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsInfo.zrow = null;
        }

        private void bwrSituacaocobrancacod1_DoWork(object sender, DoWorkEventArgs e)
        {
            dtSituacaocobrancacod1 = CarregaGridSituacaocobrancacod1();
        }

        private DataTable CarregaGridSituacaocobrancacod1()
        {
            try
            {
                return clsSituacaocobrancacod1BLL.GridDados(idsituacaocobrancacod, conexao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrSituacaocobrancacod1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvSituacaocobrancacod1.DataSource = dtSituacaocobrancacod1;
            clsGridHelper.MontaGrid(dgvSituacaocobrancacod1,
                                new String[] { "Id.", "Principal", "Cód.", "Nome" },
                                new String[] { "ID", "PRINCIPAL", "CODIGO", "NOME" },
                                new int[] { 10, 140, 140, 360 },
                                new DataGridViewContentAlignment[] {
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleLeft,
                                    DataGridViewContentAlignment.MiddleLeft},
                                new bool[] { false, false, true, true },
                                true, 1, ListSortDirection.Ascending);
            clsGridHelper.SelecionaLinha(ixSituacaocobrancacod1, dgvSituacaocobrancacod1, 2);
        }

        private void tspSetorSetorIncluir_Click(object sender, EventArgs e)
        {
            clsSituacaocobrancacodBLL clsSituacaocobrancacodBLL = new clsSituacaocobrancacodBLL();
            clsSituacaocobrancacodInfo clsSituacaocobrancacodInfo = new clsSituacaocobrancacodInfo();
            //PreencheInfoSituacaocobrancacod(clsSituacaocobrancacodInfo);
            if (clsSituacaocobrancacodBLL.Equals(clsSituacaocobrancacodInfoOld, clsSituacaocobrancacodInfo) == false)
            {
                DialogResult drt;
                drt = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    SalvarSituacaocobrancacod();
                    clsSituacaocobrancacodInfoOld = clsSituacaocobrancacodInfo;
                }
                else
                {
                    tbxCodigo.Select();
                    tbxCodigo.SelectAll();
                    return;
                }
            }

            if (dgvSituacaocobrancacod1.CurrentRow != null)
                ixSituacaocobrancacod1 = (Int32)dgvSituacaocobrancacod1.CurrentRow.Cells[0].Value;

            frmSituacaocobrancacod1 frmSituacaocobrancacod1 = new frmSituacaocobrancacod1();
            frmSituacaocobrancacod1.Init(conexao, conexao_banco, idsituacaocobrancacod, true, dgvSituacaocobrancacod1.Rows);

             clsFormHelper.AbrirForm(this.MdiParent, frmSituacaocobrancacod1,conexao);
        }

        private void tspSetorSetorAlterar_Click(object sender, EventArgs e)
        {
            if (dgvSituacaocobrancacod1.CurrentRow != null)
            {
                clsSituacaocobrancacodBLL clsSituacaocobrancacodBLL = new clsSituacaocobrancacodBLL();
                clsSituacaocobrancacodInfo clsSituacaocobrancacodInfo = new clsSituacaocobrancacodInfo();
                //PreencheInfoSituacaocobrancacod(clsSituacaocobrancacodInfo);
                if (clsSituacaocobrancacodBLL.Equals(clsSituacaocobrancacodInfoOld, clsSituacaocobrancacodInfo) == false)
                {
                    DialogResult drt;
                    drt = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (drt == DialogResult.Yes)
                    {
                        SalvarSituacaocobrancacod();
                        clsSituacaocobrancacodInfoOld = clsSituacaocobrancacodInfo;
                    }
                    else
                    {
                        tbxCodigo.Select();
                        tbxCodigo.SelectAll();
                        return;
                    }
                }

                ixSituacaocobrancacod1 = (Int32)dgvSituacaocobrancacod1.CurrentRow.Cells[0].Value;
                frmSituacaocobrancacod1 frmSituacaocobrancacod1 = new frmSituacaocobrancacod1();
                frmSituacaocobrancacod1.Init(conexao, conexao_banco, (Int32)dgvSituacaocobrancacod1.CurrentRow.Cells[0].Value, false, dgvSituacaocobrancacod1.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmSituacaocobrancacod1,conexao);
            }
        }

        private void tspSetorSetorExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSituacaocobrancacod1.CurrentRow != null)
                {
                    if (MessageBox.Show("Deseja Realmente 'Excluir' o Registro: " +
                            dgvSituacaocobrancacod1.Columns[2].HeaderText + " " + dgvSituacaocobrancacod1.CurrentRow.Cells[2].Value, "Aplisoft",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsSituacaocobrancacodBLL clsSituacaocobrancacodBLL = new clsSituacaocobrancacodBLL();
                        clsSituacaocobrancacodBLL.Excluir((Int32)dgvSituacaocobrancacod1.CurrentRow.Cells[0].Value, conexao);
                        dgvSituacaocobrancacod1.Rows.Remove(dgvSituacaocobrancacod1.CurrentRow);

                        try
                        {
                            ixSituacaocobrancacod1 = (Int32)dgvSituacaocobrancacod1.Rows[dgvSituacaocobrancacod1.CurrentRow.Index - 1].Cells["ID"].Value;
                        }
                        catch
                        {
                            try
                            {
                                ixSituacaocobrancacod1 = (Int32)dgvSituacaocobrancacod1.Rows[dgvSituacaocobrancacod1.CurrentRow.Index + 1].Cells["ID"].Value;
                            }
                            catch
                            {
                                ixSituacaocobrancacod1 = 0;
                            }
                        }

                        if (bwrSituacaocobrancacod1.IsBusy == false)
                            bwrSituacaocobrancacod1.RunWorkerAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvSituacaocobrancacod1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //tspSetorSetorAlterar.PerformClick();
        }

        private void frmSituacaocobrancacod_Shown(object sender, EventArgs e)
        {
            

        }
    }
}
