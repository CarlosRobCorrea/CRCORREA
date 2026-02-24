using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmSituacaocobrancacod1 : Form
    {
        private String conexao;
        private String conexao_banco;

        private Int32 id;
        private Int32 idcobrancacod;
        private Int32 idhistoricopagar;
        private Int32 idcentrocustopagar;
        private Int32 idhistoricoreceber;
        private Int32 idcentrocustoreceber;

        
        
        
        

        private clsSituacaocobrancacod1Info clsSituacaocobrancacod1InfoOld;
        private DataGridViewRowCollection dgvrcnSituacaocobrancacod1;

        private Boolean incluindo;

        public frmSituacaocobrancacod1()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         String _conexao_banco,
                         Int32 _id,
                         Boolean _incluindo,
                         DataGridViewRowCollection _rows)
        {
            conexao = _conexao;
            conexao_banco = _conexao_banco;

            if (_incluindo)
            {
                idcobrancacod = _id;
                id = 0;
            }
            else
                id = _id;

            incluindo = _incluindo;
            dgvrcnSituacaocobrancacod1 = _rows;

            
            
            
            
        }

        private void frmSituacaocobrancacod1_Load(object sender, EventArgs e)
        {
            // Carrega o(s) AutoComplete
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "BANCOS", "NOME", "", tbxHistoricopagar);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "BANCOS", "NOME", "", tbxHistoricoreceber);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "", tbxCentrocustopagar);
            clsVisual.AutoCompletar(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "CODIGO", "", tbxCentrocustoreceber);

            CarregaSituacaocobrancacod1(id);
        }

        private void CarregaSituacaocobrancacod1(Int32 _id)
        {
            clsSituacaocobrancacod1InfoOld = new clsSituacaocobrancacod1Info();
            if (_id > 0) // Alterando / Visualizando
            {
                clsSituacaocobrancacod1BLL clsSituacaocobrancacod1BLL = new clsSituacaocobrancacod1BLL();
                clsSituacaocobrancacod1Info clsSituacaocobrancacod1Info = new clsSituacaocobrancacod1Info();

                //Carrega os Dados
                clsSituacaocobrancacod1Info = clsSituacaocobrancacod1BLL.Carregar(id, conexao);
                PreencheCamposSituacaocobrancacod1(clsSituacaocobrancacod1Info);

                PreencheInfoSituacaocobrancacod1(clsSituacaocobrancacod1InfoOld);
            }
            else
            {
                tspPrimeiro.Enabled = false;
                tspAnterior.Enabled = false;
                tspProximo.Enabled = false;
                tspUltimo.Enabled = false;
            }

            tbxCobranca_codigo.Text = Procedure.PesquisaoPrimeiro(conexao, "select codigo from SITUACAOCOBRANCACOD where ID = " + idcobrancacod, "");
            //tbxCobranca_codigo.Text = Procedure.PesquisaValor(conexao, "SITUACAOCOBRANCACOD", "CODIGO", "ID", idcobrancacod);
            tbxCobranca_nome.Text = Procedure.PesquisaoPrimeiro(conexao, "select nome from SITUACAOCOBRANCACOD where ID=" + idcobrancacod, "");
            //tbxCobranca_nome.Text = Procedure.PesquisaValor(conexao, "SITUACAOCOBRANCACOD", "NOME", "ID", idcobrancacod);
            
            tbxCodigo.Select();
            tbxCodigo.SelectAll();
        }

        private void PreencheCamposSituacaocobrancacod1(clsSituacaocobrancacod1Info _info)
        {
            id = _info.id;
            idcentrocustopagar = _info.idcentrocustopagar;
            idcentrocustoreceber = _info.idcentrocustoreceber;
            idhistoricopagar = _info.idhistoricopagar;
            idhistoricoreceber = _info.idhistoricoreceber;

            tbxCodigo.Text = _info.codigo;
            tbxNome.Text = _info.nome;
            tbxHistoricopagar.Text = _info.historicopagar;
            tbxCentrocustopagar.Text = _info.centrocustopagar;
            tbxHistoricoreceber.Text = _info.historicoreceber;
            tbxCentrocustoreceber.Text = _info.centrocustoreceber;
        }

        private void PreencheInfoSituacaocobrancacod1(clsSituacaocobrancacod1Info _info)
        {
            _info.id = id;
            _info.idcentrocustopagar = idcentrocustopagar;
            _info.idcentrocustoreceber = idcentrocustoreceber;
            _info.idhistoricopagar = idhistoricopagar;
            _info.idhistoricoreceber = idhistoricoreceber;

            _info.codigo = tbxCodigo.Text;
            _info.nome = tbxNome.Text;
            _info.historicopagar = tbxHistoricopagar.Text;
            _info.centrocustopagar = tbxCentrocustopagar.Text;
            _info.historicoreceber = tbxHistoricoreceber.Text;
            _info.centrocustoreceber = tbxCentrocustoreceber.Text;
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            if (((Control)sender).Name == "tbxHistoricopagar")
            {
                idhistoricopagar = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from BANCOS where NOME='" + tbxHistoricopagar.Text + "'", ""));
                //idhistoricopagar = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "BANCOS", "ID", "NOME", tbxHistoricopagar.Text));

                if (idhistoricopagar == 0)
                    tbxHistoricopagar.Text = "";
            }
            else if (((Control)sender).Name == "tbxHistoricoreceber")
            {
                //idhistoricoreceber = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "BANCOS", "ID", "NOME", tbxHistoricoreceber.Text));
                idhistoricoreceber = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from BANCOS where NOME='" + tbxHistoricoreceber.Text + "'", "0"));

                if (idhistoricoreceber == 0)
                    tbxHistoricoreceber.Text = "";
            }
            else if (((Control)sender).Name == "tbxCentrocustopagar")
            {
                idcentrocustopagar = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from CENTROCUSTOS where CODIGO='" + tbxCentrocustopagar.Text + "'", "0"));
                // idcentrocustopagar = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "ID", "CODIGO", tbxCentrocustopagar.Text));
                if (idcentrocustopagar == 0)
                    tbxCentrocustopagar.Text = "";
            }
            else if (((Control)sender).Name == "tbxCentrocustoreceber")
            {
                idcentrocustoreceber = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlbanco, "select id from CENTROCUSTOS where CODIGO='" + tbxCentrocustoreceber.Text + "'", "0"));
                //idcentrocustoreceber = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqlbanco, "CENTROCUSTOS", "ID", "CODIGO", tbxCentrocustoreceber.Text));

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
            clsInfo.znomegrid = "frmBancosVis";
            frmBancosVis frmBancosVis = new frmBancosVis();
            frmBancosVis.Init(conexao_banco, conexao);

            clsFormHelper.AbrirForm(this.MdiParent, frmBancosVis,conexao_banco);
        }

        private void btnIdHistoricoReceber_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "frmBancosVis2";
            frmBancosVis frmBancosVis = new frmBancosVis();
            frmBancosVis.Init(conexao_banco, conexao);

            clsFormHelper.AbrirForm(this.MdiParent, frmBancosVis,conexao_banco);
        }

        private void frmSituacaocobrancacod1_Activated(object sender, EventArgs e)
        {
            if (clsInfo.zrow != null)
            {
                if (clsInfo.znomegrid == "frmBancosVis")
                {
                    idhistoricopagar = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxHistoricopagar.Text = clsInfo.zrow.Cells[5].Value.ToString();
                }
                else if (clsInfo.znomegrid == "frmBancosVis2")
                {
                    idhistoricoreceber = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxHistoricoreceber.Text = clsInfo.zrow.Cells[5].Value.ToString();
                }
                else if (clsInfo.znomegrid == "frmCentroCustosVis")
                {
                    idcentrocustopagar = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxCentrocustopagar.Text = clsInfo.zrow.Cells[1].Value.ToString();
                }
                else if (clsInfo.znomegrid == "frmCentroCustosVis2")
                {
                    idcentrocustoreceber = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxCentrocustoreceber.Text = clsInfo.zrow.Cells[1].Value.ToString();
                }
                clsInfo.zrow = null;
            }
            clsInfo.znomegrid = "";
        }

        private void btnIdCentroCustoPagar_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "frmCentroCustosVis";
            frmCentroCustosVis frmCentroCustosVis = new frmCentroCustosVis();
            frmCentroCustosVis.Init(conexao_banco);

            clsFormHelper.AbrirForm(this.MdiParent, frmCentroCustosVis,conexao_banco);
        }

        private void btnIdCentroCustoReceber_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "frmCentroCustosVis2";
            frmCentroCustosVis frmCentroCustosVis = new frmCentroCustosVis();
            frmCentroCustosVis.Init(conexao_banco);

            clsFormHelper.AbrirForm(this.MdiParent, frmCentroCustosVis,conexao_banco);
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
                    SalvarSituacaocobrancacod1();
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

        private void SalvarSituacaocobrancacod1()
        {
            clsSituacaocobrancacod1Info clsSituacaocobrancacod1Info = new clsSituacaocobrancacod1Info();
            clsSituacaocobrancacod1BLL clsSituacaocobrancacod1BLL = new clsSituacaocobrancacod1BLL();

            PreencheInfoSituacaocobrancacod1(clsSituacaocobrancacod1Info);
            if (id == 0)    // Incluindo
            {
                id = clsSituacaocobrancacod1BLL.Incluir(clsSituacaocobrancacod1Info, conexao);
            }
            else
            {
                // Verifica se algo foi alterado, se foi, Salva
                if (clsSituacaocobrancacod1BLL.Equals(clsSituacaocobrancacod1InfoOld, clsSituacaocobrancacod1Info) == false)
                {
                    clsSituacaocobrancacod1BLL.Alterar(clsSituacaocobrancacod1Info, conexao);
                }
            }
        }

        public Boolean MovimentaSituacaocobrancacod1()
        {
            clsSituacaocobrancacod1BLL clsSituacaocobrancacod1BLL = new clsSituacaocobrancacod1BLL();
            clsSituacaocobrancacod1Info clsSituacaocobrancacod1Info = new clsSituacaocobrancacod1Info();
            PreencheInfoSituacaocobrancacod1(clsSituacaocobrancacod1Info);
            if (clsSituacaocobrancacod1BLL.Equals(clsSituacaocobrancacod1InfoOld, clsSituacaocobrancacod1Info) == false)
            {
                DialogResult drt;
                drt = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    SalvarSituacaocobrancacod1();
                }
                else if (drt == DialogResult.Cancel)
                {
                    tbxCodigo.Select();
                    tbxCodigo.SelectAll();
                    return false;
                }
            }
            return true;
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (MovimentaSituacaocobrancacod1())
                {
                    id = Int32.Parse(dgvrcnSituacaocobrancacod1[0].Cells[0].Value.ToString());
                    CarregaSituacaocobrancacod1(id);
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
                if (MovimentaSituacaocobrancacod1())
                {
                    foreach (DataGridViewRow dgvr in dgvrcnSituacaocobrancacod1)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index != 0)
                            {
                                id = Int32.Parse(dgvrcnSituacaocobrancacod1[dgvrcnSituacaocobrancacod1.GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaSituacaocobrancacod1(id);
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
                if (MovimentaSituacaocobrancacod1())
                {
                    foreach (DataGridViewRow dgvr in dgvrcnSituacaocobrancacod1)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index < dgvrcnSituacaocobrancacod1.Count - 1)
                            {
                                id = Int32.Parse(dgvrcnSituacaocobrancacod1[dgvrcnSituacaocobrancacod1.GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaSituacaocobrancacod1(id);
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
                if (MovimentaSituacaocobrancacod1())
                {
                    id = Int32.Parse(dgvrcnSituacaocobrancacod1[dgvrcnSituacaocobrancacod1.Count - 1].Cells[0].Value.ToString());
                    CarregaSituacaocobrancacod1(id);
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
                clsSituacaocobrancacod1BLL clsSituacaocobrancacod1BLL = new clsSituacaocobrancacod1BLL();
                clsSituacaocobrancacod1Info clsSituacaocobrancacod1Info = new clsSituacaocobrancacod1Info();

                PreencheInfoSituacaocobrancacod1(clsSituacaocobrancacod1Info);

                if (clsSituacaocobrancacod1BLL.Equals(clsSituacaocobrancacod1Info, clsSituacaocobrancacod1InfoOld) == false)
                {
                    DialogResult resultado;
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (resultado == DialogResult.Yes)
                    {
                        SalvarSituacaocobrancacod1();
                    }
                    else if (resultado == DialogResult.Cancel)
                    {
                        tbxCodigo.Select();
                        tbxCodigo.SelectAll();
                        return;
                    }
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

        private void frmSituacaocobrancacod1_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsInfo.zrow = null;
        }
    }
}
