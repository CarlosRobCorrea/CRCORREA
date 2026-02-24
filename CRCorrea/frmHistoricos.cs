using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmHistoricos : Form
    {
        String conexao;

        clsHistoricosBLL clsHistoricosBLL;

        Int32 id;
        Int32 idcontabil;
        Int32 idcobranca;

        DataGridViewRowCollection rows;
        clsHistoricosInfo clsHistoricosInfoOld;

        
        

        public frmHistoricos()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         Int32 _id,
                         DataGridViewRowCollection _dgvrcnHistoricos)
        {
            clsHistoricosBLL = new clsHistoricosBLL();

            conexao = _conexao;
            id = _id;
            rows = _dgvrcnHistoricos;

            
            

            cbxAtivo.SelectedIndex = 0;
            cbxTipo.SelectedIndex = 0;
        }

        private void frmHistoricos_Load(object sender, EventArgs e)
        {
            CarregaHistoricos(id);
        }

        private void CarregaHistoricos(Int32 _id)
        {
            clsInfo.zidincluir = id;

            clsHistoricosInfoOld = new clsHistoricosInfo();
            if (_id > 0)    // Alterando / Visualizando
            {
                clsHistoricosInfo clsHistoricosInfo = new clsHistoricosInfo();

                // Carrega os Dados
                clsHistoricosInfo = clsHistoricosBLL.Carregar(_id, conexao);
                PreencheCamposHistoricos(clsHistoricosInfo);

                PreencheInfoHistoricos(clsHistoricosInfoOld);
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
        }

        private void PreencheCamposHistoricos(clsHistoricosInfo _info)
        {
            id = _info.id;
            idcontabil = _info.idcontabil;
            idcobranca = _info.idcobranca;

            tbxCodigo.Text = _info.codigo.ToString();
            tbxNome.Text = _info.nome;
            cbxAtivo.SelectedIndex = clsVisual.SelecionarIndex(_info.ativo, 1, cbxAtivo);
            if (_info.tipo != null)
            {
                cbxTipo.SelectedIndex = clsVisual.SelecionarIndex(_info.tipo, 1, cbxTipo);
            }
            tbxContabil.Text = _info.contabil.ToString();
            tbxCobranca.Text = _info.cobranca.ToString();

            //if (idcontabil > 0)
            //{
            //    //tbxContabil.Text = Procedure.PesquisaValor(clsInfo.conexaosqldadosbanco, "Historicos", "CODIGO", "ID", idcontabil);
            //    //lblNomeContaContabil.Text = Procedure.PesquisaValor(clsInfo.conexaosqldadosbanco, "Historicos", "COGNOME", "ID", idbanco);
            //}
            //else
            //{
            //    tbxContabil.Text = "0";
            //}

            tbxNivel.Text = _info.nivel;

            //if (idcobranca > 0)
            //{
            //    //tbxCobranca.Text = Procedure.PesquisaValor(Properties.Settings.Default.conexaosql2, "Historicos", "CODIGO", "ID", idbanco);
            //    //lblNomeCobranca.Text = Procedure.PesquisaValor(Properties.Settings.Default.conexaosql2, "Historicos", "COGNOME", "ID", idbanco);
            //}
            //else
            //{
            //    tbxCobranca.Text = "0";
            //}
        }

        private void PreencheInfoHistoricos(clsHistoricosInfo _info)
        {
            _info.id = id;
            _info.idcontabil = idcontabil;
            _info.idcobranca = idcobranca;
            _info.codigo =tbxCodigo.Text;
            _info.nome = tbxNome.Text;
            _info.ativo = cbxAtivo.Text.Substring(0, 1);
            _info.tipo = cbxTipo.Text.Substring(0, 1);
            _info.contabil = tbxContabil.Text;
            _info.nivel = tbxNivel.Text;
            _info.cobranca = tbxCobranca.Text;
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void frmHistoricos_Activated(object sender, EventArgs e)
        {

        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar ?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (resultado == DialogResult.Yes)
                {
                    SalvarHistoricos();
                }
                else if (resultado == DialogResult.Cancel)
                {
                    tbxCodigo.Select();
                    tbxCodigo.SelectAll();
                    return;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tbxCodigo.Select();
                tbxCodigo.SelectAll();
            }
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (MovimentaHistoricos())
                {
                    id = Int32.Parse(rows[0].Cells[0].Value.ToString());
                    CarregaHistoricos(id);
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
                if (MovimentaHistoricos())
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
                    CarregaHistoricos(id);
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
                if (MovimentaHistoricos())
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
                    CarregaHistoricos(id);
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
                if (MovimentaHistoricos())
                {
                    id = Int32.Parse(rows[rows.Count - 1].Cells[0].Value.ToString());
                    CarregaHistoricos(id);
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
                clsHistoricosInfo clsHistoricosInfo = new clsHistoricosInfo();

                PreencheInfoHistoricos(clsHistoricosInfo);


                if (clsHistoricosBLL.Equals(clsHistoricosInfoOld, clsHistoricosInfo) == false)
                {
                    DialogResult resultado;
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (resultado == DialogResult.Yes)
                    {
                        SalvarHistoricos();
                    }
                    else if (resultado == DialogResult.Cancel)
                    {
                        tbxCodigo.Select();
                        tbxCodigo.SelectAll();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tbxCodigo.Select();
                tbxCodigo.SelectAll();
            }

            this.Close();
        }
        public Boolean MovimentaHistoricos()
        {
            clsHistoricosInfo clsHistoricosInfo = new clsHistoricosInfo();
            PreencheInfoHistoricos(clsHistoricosInfo);

            if (clsHistoricosBLL.Equals(clsHistoricosInfoOld, clsHistoricosInfo) == false)
            {
                DialogResult drt;
                drt = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    SalvarHistoricos();
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

        private void SalvarHistoricos()
        {
            clsHistoricosInfo clsHistoricosInfo = new clsHistoricosInfo();
            clsHistoricosBLL clsHistoricosBLL = new clsHistoricosBLL();

            if (id == 0)    // Incluindo
            {
                PreencheInfoHistoricos(clsHistoricosInfo);
                id = clsHistoricosBLL.Incluir(clsHistoricosInfo, conexao);
                clsInfo.zidincluir = id;
            }
            else
            {
                PreencheInfoHistoricos(clsHistoricosInfo);
                // Verifica se algo foi alterado, se foi, Salva
                if (clsHistoricosBLL.Equals(clsHistoricosInfoOld, clsHistoricosInfo) == false)
                {
                    clsHistoricosBLL.Alterar(clsHistoricosInfo, conexao);
                }
            }
        }
    }
}
