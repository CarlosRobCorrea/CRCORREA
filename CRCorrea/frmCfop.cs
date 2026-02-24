using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmCfop : Form
    {
        String conexao;
        Int32 id;
        Int32 idContaDeb;
        Int32 idContaCre;


        clsCfopBLL clsCfopBLL;
        clsCfopInfo clsCfopInfoOld;
        DataGridViewRowCollection rowsCfop;

        public frmCfop()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id, DataGridViewRowCollection _rowsCfop)
        {
            conexao = clsInfo.conexaosqldados;
            id = _id;
            rowsCfop = _rowsCfop;

            clsCfopBLL = new clsCfopBLL();
        }

        private void frmCfop_Load(object sender, EventArgs e)
        {
            CarregaCfop(id);
        }

        private void CarregaCfop(Int32 _id)
        {
            clsCfopInfoOld = new clsCfopInfo();
            if (_id > 0)
            {
                clsCfopInfo clsCfopInfo;
                clsCfopBLL clsCfopBLL = new clsCfopBLL();

                clsCfopInfo = clsCfopBLL.Carregar(_id, conexao);
                PreencheCamposCfop(clsCfopInfo);
                PreencheInfoCfop(clsCfopInfoOld);
            }
            else
            {
                tspPrimeiro.Enabled = false;
                tspAnterior.Enabled = false;
                tspProximo.Enabled = false;
                tspUltimo.Enabled = false;
            }
            tbxCfop.Select();
            tbxCfop.SelectAll();
        }

        private void PreencheCamposCfop(clsCfopInfo _info)
        {
            tbxCfop.Text = _info.cfop.ToString();
            tbxNomeNota.Text = _info.nomenota.ToString();
            tbxDizer.Text = _info.dizer.ToString();
            if (_info.tipo == "S")
            {
                rbnSaida.Checked = true;
            }
            else
            {
                rbnEntrada.Checked = true;
            }
            if (tbxCfop.Text.Length > 5)
            {
                rbnVariosCfop.Checked = true;
            }
            else
            {
                rbn1Cfop.Checked = true;
            }
            if (_info.ativo == "N")
            {
                rbnAtivoN.Checked = true;
            }
            else
            {
                rbnAtivoS.Checked = true;
            }
            idContaDeb = _info.idcontadeb;
            if (idContaDeb > 0)
            {
                tbxContaDeb.Text = Procedure.PesquisaoPrimeiro(conexao, "select codigo from CONTACONTABIL where ID=" + idContaDeb, "");
                //tbxContaDeb.Text = Procedure.PesquisaValor(conexao, "CONTACONTABIL", "CODIGO", "ID", idContaDeb);
                tbxContaDebRed.Text = Procedure.PesquisaoPrimeiro(conexao, "select REDUZIDO from CONTACONTABIL where ID=" + idContaDeb, "");
                //tbxContaDebRed.Text = Procedure.PesquisaValor(conexao, "CONTACONTABIL", "REDUZIDO", "ID", idContaDeb);
            }
            else
            {
                tbxContaDeb.Text = "0";
            }
            idContaCre = _info.idcontacre;
            if (idContaCre > 0)
            {
                tbxContaCre.Text = Procedure.PesquisaoPrimeiro(conexao, "select codigo from CONTACONTABIL where ID=" + idContaCre, "");
                //tbxContaCre.Text = Procedure.PesquisaValor(conexao, "CONTACONTABIL", "CODIGO", "ID", idContaCre);

                tbxContaCreRed.Text = Procedure.PesquisaoPrimeiro(conexao, "select REDUZIDO from CONTACONTABIL where ID=" + idContaCre, "");
                //tbxContaCreRed.Text = Procedure.PesquisaValor(conexao, "CONTACONTABIL", "REDUZIDO", "ID", idContaCre);
            }
            else
            {
                tbxContaDeb.Text = "0";
            }

            if (_info.combustivel == "S")
            {
                ckxCombustivel.Checked = true;
            }
            else
            {
                ckxCombustivel.Checked = false;
            }
        }

        private void PreencheInfoCfop(clsCfopInfo _info)
        {
            _info.id = id;
            _info.cfop = tbxCfop.Text;
            _info.nomenota = clsVisual.RemoveAcentos(tbxNomeNota.Text);
            _info.dizer = clsVisual.RemoveAcentos(tbxDizer.Text);
            if (rbnSaida.Checked == true)
            {
                _info.tipo = "S";  // saida
            }
            else
            {
                _info.tipo = "E";  // entrada
            }
            if (tbxCfop.Text.Length > 5)
            {
                _info.cfopunico = "D";  // divesos
            }
            else
            {
                _info.cfopunico = "U";  // unico
            }
            if (rbnAtivoN.Checked == true)
            {
                _info.ativo = "N";  // divesos
            }
            else
            {
                _info.ativo = "S";  // unico
            }
            _info.idcontadeb = clsParser.Int32Parse(idContaDeb.ToString());
            _info.idcontacre = clsParser.Int32Parse(idContaCre.ToString());
            
            if (ckxCombustivel.Checked)
            {
                _info.combustivel = "S";
            }
            else
            {
                _info.combustivel = "N";
            }
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

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void SalvarCfop()
        {
            clsCfopInfo clsCfopInfo = new clsCfopInfo();
            PreencheInfoCfop(clsCfopInfo);

            if (id == 0)
                clsCfopBLL.Incluir(clsCfopInfo, conexao);
            else
                clsCfopBLL.Alterar(clsCfopInfo, conexao);
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar ?",
                                                "Aplisoft",
                                                MessageBoxButtons.YesNoCancel,
                                                MessageBoxIcon.Question,
                                                MessageBoxDefaultButton.Button1);
                if (resultado == DialogResult.Yes)
                {
                    SalvarCfop();
                }
                else if (resultado == DialogResult.Cancel)
                {
                    tbxCfop.Select();
                    tbxCfop.SelectAll();
                    return;
                }
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxNomeNota.Select();
                tbxNomeNota.SelectAll();
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                clsCfopInfo clsCfopInfo = new clsCfopInfo();
                PreencheInfoCfop(clsCfopInfo);

                if (clsCfopBLL.Equals(clsCfopInfo, clsCfopInfoOld) == false)
                {
                    DialogResult resultado;
                    resultado = MessageBox.Show("Deseja Salvar e Retornar ?",
                                                    "Aplisoft",
                                                    MessageBoxButtons.YesNoCancel,
                                                    MessageBoxIcon.Question,
                                                    MessageBoxDefaultButton.Button1);
                    if (resultado == DialogResult.Yes)
                    {
                        SalvarCfop();
                    }
                    else if (resultado == DialogResult.Cancel)
                    {
                        tbxCfop.Select();
                        tbxCfop.SelectAll();
                        return;
                    }
                }
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxCfop.Select();
                tbxCfop.SelectAll();
            }
        }

        public Boolean MovimentaCfop()
        {
            clsCfopInfo clsCfopInfo = new clsCfopInfo();
            PreencheInfoCfop(clsCfopInfo);
            if (clsCfopBLL.Equals(clsCfopInfoOld, clsCfopInfo) == false)
            {
                DialogResult drt;
                drt = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    SalvarCfop();
                }
                else if (drt == DialogResult.Cancel)
                {
                    tbxCfop.Select();
                    tbxCfop.SelectAll();
                    return false;
                }
            }
            return true;
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (MovimentaCfop())
                {
                    id = Int32.Parse(rowsCfop[0].Cells[0].Value.ToString());
                    CarregaCfop(id);
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
                if (MovimentaCfop())
                {
                    foreach (DataGridViewRow row in rowsCfop)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index != 0)
                            {
                                id = Int32.Parse(rowsCfop[rowsCfop.GetPreviousRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaCfop(id);
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
                if (MovimentaCfop())
                {
                    foreach (DataGridViewRow row in rowsCfop)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index < rowsCfop.Count - 1)
                            {
                                id = Int32.Parse(rowsCfop[rowsCfop.GetNextRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaCfop(id);
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
                if (MovimentaCfop())
                {
                    id = Int32.Parse(rowsCfop[rowsCfop.Count - 1].Cells[0].Value.ToString());
                    CarregaCfop(id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void frmCfop_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsInfo.zlastid = id;
        }

        private void tbxDizer_TextChanged(object sender, EventArgs e)
        {

        }

        private void rbnSaida_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnContaDeb_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvContaDeb";
            frmContacontabilPes frmContacontabilPes = new frmContacontabilPes();
            frmContacontabilPes.Init(idContaDeb);

            clsFormHelper.AbrirForm(this.MdiParent, frmContacontabilPes, clsInfo.conexaosqldados);

        }

        private void btnContaCre_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvContaCre";
            frmContacontabilPes frmContacontabilPes = new frmContacontabilPes();
            frmContacontabilPes.Init(idContaCre);

            clsFormHelper.AbrirForm(this.MdiParent, frmContacontabilPes, clsInfo.conexaosqldados);
        }

        private void tbxContaCreRed_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmCfop_Activated(object sender, EventArgs e)
        {
            Lupa();
        }
        public void Lupa()
        {
            if (clsInfo.zrow != null && clsInfo.zrow.Index != -1)
            {
                if (clsInfo.znomegrid == "dgvContaDeb")
                {
                    idContaDeb = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxContaDeb.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxContaDebRed.Text = Procedure.PesquisaoPrimeiro(conexao, "select REDUZIDO from CONTACONTABIL where ID=" + idContaDeb, "");
                    //tbxContaDebRed.Text = Procedure.PesquisaValor(conexao, "CONTACONTABIL", "REDUZIDO", "ID", idContaDeb);
                }
                if (clsInfo.znomegrid == "dgvContaCre")
                {
                    idContaCre = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxContaCre.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxContaCreRed.Text = Procedure.PesquisaoPrimeiro(conexao, "select REDUZIDO from CONTACONTABIL where ID=" + idContaCre, "");
                    //tbxContaCreRed.Text = Procedure.PesquisaValor(conexao, "CONTACONTABIL", "REDUZIDO", "ID", idContaCre);
                }
            }
        }

        private void frmCfop_Shown(object sender, EventArgs e)
        {

        }
    }
}

