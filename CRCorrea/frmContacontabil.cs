using CRCorreaBLL;
using CRCorreaInfo;
using CRCorreaFuncoes;
using System;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmContacontabil : Form
    {
        String conexao;
        Int32 id;

        clsContacontabilBLL clsContacontabilBLL;
        clsContacontabilInfo clsContacontabilInfoOld;
        DataGridViewRowCollection rowsContacontabil;

        

        public frmContacontabil()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridViewRowCollection _rowsContacontabil)
        {
            conexao = clsInfo.conexaosqldados;
            id = _id;
            rowsContacontabil = _rowsContacontabil;

            

            clsContacontabilBLL = new clsContacontabilBLL();
        }

        private void frmContacontabil_Load(object sender, EventArgs e)
        {
            CarregaContacontabil(id);
        }

        private void CarregaContacontabil(Int32 _id)
        {
            clsContacontabilInfoOld = new clsContacontabilInfo();
            if (_id > 0)
            {
                clsContacontabilInfo clsContacontabilInfo;
                clsContacontabilBLL clsContacontabilBLL = new clsContacontabilBLL();

                clsContacontabilInfo = clsContacontabilBLL.Carregar(_id, conexao);
                PreencheCamposContacontabil(clsContacontabilInfo);
                PreencheInfoContacontabil(clsContacontabilInfoOld);
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

        private void PreencheCamposContacontabil(clsContacontabilInfo _info)
        {
            tbxCodigo.Text = _info.codigo.ToString();
            tbxNome.Text = _info.nome.ToString();
            tbxReduzido.Text = _info.reduzido.ToString();
            if (_info.tipo == "T")
            {
                rbnTotalizadora.Checked = true;
            }
            else
            {
                rbnAnalitica.Checked = true;
            }
            if (_info.ativo == "N")
            {
                rbnAtivoN.Checked = true;
            }
            else
            {
                rbnAtivoS.Checked = true;
            }
        }

        private void PreencheInfoContacontabil(clsContacontabilInfo _info)
        {
            _info.id = id;
            _info.codigo =tbxCodigo.Text;
            _info.nome = tbxNome.Text;
            _info.reduzido = clsParser.Int32Parse(tbxReduzido.Text);
            if (rbnAnalitica.Checked == true)
            {
                _info.tipo = "A";  // conta analitica
                _info.verfabrica = "S";
            }
            else
            {
                _info.tipo = "T";  // cta totalizadora
                _info.verfabrica = "N";
            }
            if (rbnAtivoN.Checked == true)
            {
                _info.ativo ="N";  // divesos
            }
            else
            {
                _info.ativo = "S";  // unico
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

        private void SalvarContacontabil()
        {
            clsContacontabilInfo clsContacontabilInfo = new clsContacontabilInfo();
            PreencheInfoContacontabil(clsContacontabilInfo);

            if (id == 0)
            {
                clsContacontabilBLL.Incluir(clsContacontabilInfo, conexao);
            }
            else
            {
                clsContacontabilBLL.Alterar(clsContacontabilInfo, conexao);
            }
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
                    SalvarContacontabil();
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
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxCodigo.Select();
                tbxCodigo.SelectAll();
            }

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                clsContacontabilInfo clsContacontabilInfo = new clsContacontabilInfo();
                PreencheInfoContacontabil(clsContacontabilInfo);

                if (clsContacontabilBLL.Equals(clsContacontabilInfo, clsContacontabilInfoOld) == false)
                {
                    DialogResult resultado;
                    resultado = MessageBox.Show("Deseja Salvar e Retornar ?",
                                                    "Aplisoft",
                                                    MessageBoxButtons.YesNoCancel,
                                                    MessageBoxIcon.Question,
                                                    MessageBoxDefaultButton.Button1);
                    if (resultado == DialogResult.Yes)
                    {
                        SalvarContacontabil();
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
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxCodigo.Select();
                tbxCodigo.SelectAll();
            }
        }

        public Boolean MovimentaContacontabil()
        {
            clsContacontabilInfo clsContacontabilInfo = new clsContacontabilInfo();
            PreencheInfoContacontabil(clsContacontabilInfo);
            if (clsContacontabilBLL.Equals(clsContacontabilInfoOld, clsContacontabilInfo) == false)
            {
                DialogResult drt;
                drt = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    SalvarContacontabil();
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
                if (MovimentaContacontabil())
                {
                    id = Int32.Parse(rowsContacontabil[0].Cells[0].Value.ToString());
                    CarregaContacontabil(id);
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
                if (MovimentaContacontabil())
                {
                    foreach (DataGridViewRow row in rowsContacontabil)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index != 0)
                            {
                                id = Int32.Parse(rowsContacontabil[rowsContacontabil.GetPreviousRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaContacontabil(id);
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
                if (MovimentaContacontabil())
                {
                    foreach (DataGridViewRow row in rowsContacontabil)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index < rowsContacontabil.Count - 1)
                            {
                                id = Int32.Parse(rowsContacontabil[rowsContacontabil.GetNextRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaContacontabil(id);
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
                if (MovimentaContacontabil())
                {
                    id = Int32.Parse(rowsContacontabil[rowsContacontabil.Count - 1].Cells[0].Value.ToString());
                    CarregaContacontabil(id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void frmContacontabil_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsInfo.zlastid = id;
        }

        private void tbxDizer_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmContacontabil_Shown(object sender, EventArgs e)
        {
            
        }


    }
}
