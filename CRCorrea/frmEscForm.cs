using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CRCorreaFuncoes;
using CRCorreaBLL;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmEscForm : Form
    {
        String conexao;
        Int32 id;
        String tabela;
        String nometabela;
        Int32 maxlengthcodigo;
        Int32 maxlengthnome;
        clsEscFormBLL clsEscFormBLL;
        clsEscFormInfo clsEscFormInfoOld;

        public frmEscForm()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         Int32 _id,
                         String _tabela,
                         Int32 _maxlengthcodigo,
                         Int32 _maxlengthnome,
                         String _nometabela)
        {
            conexao = _conexao;
            id = _id;
            tabela = _tabela;
            nometabela = _nometabela;

            maxlengthcodigo = _maxlengthcodigo;
            maxlengthnome = _maxlengthnome;

            if (maxlengthcodigo == 0)
            {
                maxlengthcodigo = 1;
            }
            if (maxlengthnome == 0)
            {
                maxlengthnome = 1;
            }

            
            clsEscFormBLL = new clsEscFormBLL(tabela);
        }

        private void frmEscForm_Load(object sender, EventArgs e)
        {
            tbxCodigo.MaxLength = maxlengthcodigo;
            tbxNome.MaxLength = maxlengthnome;
            lblNomeTabela.Text = nometabela;
            CarregaEscForm(id);
        }

        private void CarregaEscForm(Int32 _id)
        {
            try
            {
                clsEscFormInfoOld = new clsEscFormInfo();
                if (_id > 0)    // Alterando / Visualizando
                {
                    clsEscFormBLL clsEscFormBLL = new clsEscFormBLL(tabela);
                    clsEscFormInfo clsEscFormInfo;

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

        private void PreencheCamposEscForm(clsEscFormInfo _info)
        {
            id = _info.id;
            tbxCodigo.Text = _info.codigo;
            tbxNome.Text = _info.nome;
        }

        private void PreencheInfoEscForm(clsEscFormInfo _info)
        {
            _info.id = id;
            _info.codigo = tbxCodigo.Text;
            _info.nome = tbxNome.Text;
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                clsEscFormInfo clsEscFormInfo = new clsEscFormInfo();
                PreencheInfoEscForm(clsEscFormInfo);

                if (clsEscFormBLL.Equals(clsEscFormInfoOld, clsEscFormInfo) == false)
                {
                    DialogResult resultado;
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (resultado == DialogResult.Yes)
                    {
                        SalvarEscForm();
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

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (resultado == DialogResult.Yes)
                {
                    SalvarEscForm();
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

        private void SalvarEscForm()
        {
            clsEscFormInfo clsEscFormInfo = new clsEscFormInfo();
            PreencheInfoEscForm(clsEscFormInfo);

            if (id == 0)
                clsEscFormBLL.Incluir(clsEscFormInfo, conexao);
            else
                clsEscFormBLL.Alterar(clsEscFormInfo, conexao);
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

        private void frmEscForm_Shown(object sender, EventArgs e)
        {
            
        }
    }
}
