using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmFormId : Form
    {
        private String conexao;
        private Int32 id;
        private String tabela;

        private clsFormIdInfo clsEscFormInfoOld;

        
        

        public frmFormId()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         Int32 _id,
                         String _tabela)
        {
            conexao = _conexao;
            id = _id;
            tabela = _tabela;

            
            
        }

        private void frmEscForm_Load(object sender, EventArgs e)
        {
            /* habilita quando tiver a propriedade TAG
            if (FormHelper.ConfigForm(clsParser.Int32Parse(this.Tag.ToString()),
                                    this,
                                    this.Text,
                                    clsInfo.conexaosqldados,
                                    toolTip1) == false)
            {
                this.Close();
                this.Dispose();
            }
            */

            CarregaEscForm(id);

            /* habilita quando tiver a propriedade TAG
            if (FormHelper.AcessaForm(clsParser.Int32Parse(this.Tag.ToString()),
                                    this,
                                    this.Text,
                                    clsInfo.conexaosqldados,
                                    toolTip1) == false)
            {
                this.Close();
                this.Dispose();
            }

            
            */
        }

        private void CarregaEscForm(Int32 _id)
        {
            try
            {
                clsEscFormInfoOld = new clsFormIdInfo();
                if (_id > 0)    // Alterando / Visualizando
                {
                    clsFormIdBLL clsEscFormBLL = new clsFormIdBLL(tabela);
                    clsFormIdInfo clsEscFormInfo;

                    // Carrega os Dados
                    clsEscFormInfo = clsEscFormBLL.Carregar(_id, conexao);
                    PreencheCamposEscForm(clsEscFormInfo);

                    PreencheInfoEscForm(clsEscFormInfoOld);
                }
                else
                {
                    tspPrimeiro.Enabled = false;
                    tspAnterior.Enabled = false;
                    tspProximo.Enabled = false;
                    tspUltimo.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                tbxCodigo.Select();
                tbxCodigo.SelectAll();
            }
        }

        private void PreencheCamposEscForm(clsFormIdInfo _info)
        {
            id = _info.id;
            tbxCodigo.Text = _info.codigo;
            tbxNome.Text = _info.nome;
        }

        private void PreencheInfoEscForm(clsFormIdInfo _info)
        {
            

            _info.id = id;
            _info.codigo = tbxCodigo.Text;
            _info.nome = tbxNome.Text;
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                clsFormIdInfo clsEscFormInfo = new clsFormIdInfo();
                PreencheInfoEscForm(clsEscFormInfo);

                clsFormIdBLL clsEscFormBLL = new clsFormIdBLL(tabela);
                if (clsEscFormBLL.Equals(clsEscFormInfoOld, clsEscFormInfo) == false)
                {
                    DialogResult resultado;
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (resultado == DialogResult.Yes)
                    {
                        if (id <= 0)
                        {
                            clsEscFormBLL.Incluir(clsEscFormInfo, conexao);
                        }
                        else
                        {
                            clsEscFormBLL.Alterar(clsEscFormInfo, conexao);
                        }
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
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Close();
            this.Dispose();
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (id == 0)    // Incluindo
                {
                    if (resultado == DialogResult.Yes)
                    {
                        clsFormIdInfo clsEscFormInfo = new clsFormIdInfo();
                        PreencheInfoEscForm(clsEscFormInfo);

                        clsFormIdBLL clsEscFormBLL = new clsFormIdBLL(tabela);
                        clsEscFormBLL.Incluir(clsEscFormInfo, conexao);
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
                else
                {
                    if (resultado == DialogResult.Yes)
                    {
                        clsFormIdInfo clsEscFormInfo = new clsFormIdInfo();
                        PreencheInfoEscForm(clsEscFormInfo);

                        // Verifica se algo foi alterado, se foi, pede pra Salvar
                        clsFormIdBLL clsEscFormBLL = new clsFormIdBLL(tabela);
                        if (clsEscFormBLL.Equals(clsEscFormInfoOld, clsEscFormInfo) == false)
                        {
                            clsEscFormBLL.Alterar(clsEscFormInfo, conexao);
                        }
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxCodigo.Select();
                tbxCodigo.SelectAll();
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
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{tab}");
            }
            else if (e.KeyCode == Keys.Up)
            {
                SendKeys.Send("+{tab}");
            }
        }
    }
}
