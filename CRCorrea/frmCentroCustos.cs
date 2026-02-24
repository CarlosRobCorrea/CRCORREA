using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CRCorreaFuncoes;
using CRCorreaBLL;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmCentroCustos : Form
    {
        const Int32 zFormNumero = 0;
        String conexao;
        Int32 id;

        DataGridViewRowCollection dgvrcnCentroCustos;
        clsCentrocustosInfo clsCentrocustosInfoOld = new clsCentrocustosInfo();

        
        
        

        public frmCentroCustos()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         Int32 _id,
                         DataGridViewRowCollection _dgvrcnCentroCustos)
        {
            conexao = _conexao;
            id = _id;
            dgvrcnCentroCustos = _dgvrcnCentroCustos;

            
            
            
        }

        private void frmCentroCustos_Load(object sender, EventArgs e)
        {
            CarregaCentroCustos(id);
        }

        private void CarregaCentroCustos(Int32 _id)
        {
            clsInfo.zidincluir = id;

            clsCentrocustosInfoOld = new clsCentrocustosInfo();
            if (_id > 0)    // Alterando / Visualizando
            {
                clsCentrocustosBLL clsCentrocustosBLL = new clsCentrocustosBLL();
                clsCentrocustosInfo clsCentrocustosInfo = new clsCentrocustosInfo();

                // Carrega os Dados
                clsCentrocustosInfo = clsCentrocustosBLL.Carregar(_id, conexao);
                PreencheCamposCentroCustos(clsCentrocustosInfo);

                PreencheInfoCentroCustos(clsCentrocustosInfoOld);
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

        private void PreencheCamposCentroCustos(clsCentrocustosInfo _info)
        {
            id = _info.id;

            tbxCodigo.Text = _info.codigo.ToString();
            tbxNome.Text = _info.nome;
            if (_info.ativo == "S")
            {
                rbnAtivo.Checked = true;
            }
            else
            {
                rbnInativo.Checked = true;
            }
            tbxData.Text = _info.data;
        }

        private void PreencheInfoCentroCustos(clsCentrocustosInfo _info)
        {
            

            _info.id = id;
            _info.codigo = tbxCodigo.Text;
            _info.nome = tbxNome.Text;
            if (rbnAtivo.Checked == true)
            {
                _info.ativo = "S";
            }
            else
            {
                if (_info.ativo != null)
                {
                    _info.ativo = "N";
                }
                else
                {
                    _info.ativo = "";
                }
            }
            _info.data = tbxData.Text;
        }
        private void CamposCentroCustos(object _ctrl)
        {
            if (_ctrl is TextBox)
            {
            }
        }

        private void ControlEnter(object sender, EventArgs e)
        {

            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            CamposCentroCustos(sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void frmCentroCustos_Activated(object sender, EventArgs e)
        {

        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar ?", "Aplisoft",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);

                if (resultado == DialogResult.Yes)
                {
                    SalvarCentroCustos();
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

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (MovimentaCentroCustos())
                {
                    id = Int32.Parse(dgvrcnCentroCustos[0].Cells[0].Value.ToString());
                    CarregaCentroCustos(id);
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
                if (MovimentaCentroCustos())
                {
                    foreach (DataGridViewRow dgvr in dgvrcnCentroCustos)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index != 0)
                            {
                                id = Int32.Parse(dgvrcnCentroCustos[dgvrcnCentroCustos.GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaCentroCustos(id);
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
                if (MovimentaCentroCustos())
                {
                    foreach (DataGridViewRow dgvr in dgvrcnCentroCustos)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index < dgvrcnCentroCustos.Count - 1)
                            {
                                id = Int32.Parse(dgvrcnCentroCustos[dgvrcnCentroCustos.GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaCentroCustos(id);
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
                if (MovimentaCentroCustos())
                {
                    id = Int32.Parse(dgvrcnCentroCustos[dgvrcnCentroCustos.Count - 1].Cells[0].Value.ToString());
                    CarregaCentroCustos(id);
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
                clsCentrocustosBLL clsCentrocustosBLL = new clsCentrocustosBLL();
                clsCentrocustosInfo clsCentrocustosInfo = new clsCentrocustosInfo();

                PreencheInfoCentroCustos(clsCentrocustosInfo);


                if (clsCentrocustosBLL.Equals(clsCentrocustosInfoOld, clsCentrocustosInfo) == false)
                {
                    DialogResult resultado;
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (resultado == DialogResult.Yes)
                    {
                        SalvarCentroCustos();
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
            this.Close();
            this.Dispose();

        }
        public Boolean MovimentaCentroCustos()
        {
            clsCentrocustosBLL clsCentrocustosBLL = new clsCentrocustosBLL();
            clsCentrocustosInfo clsCentrocustosInfo = new clsCentrocustosInfo();
            PreencheInfoCentroCustos(clsCentrocustosInfo);
            //if (clsCentrocustosBLL.ComparaInfo(clsCentrocustosInfoOld, clsCentrocustosInfo) == false || bnAlunoCurso == true)
            if (clsCentrocustosBLL.Equals(clsCentrocustosInfoOld, clsCentrocustosInfo) == false)
            {
                DialogResult drt;
                drt = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    SalvarCentroCustos();
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
        private void SalvarCentroCustos()
        {
            clsCentrocustosInfo clsCentrocustosInfo = new clsCentrocustosInfo();
            clsCentrocustosBLL clsCentrocustosBLL = new clsCentrocustosBLL();

            if (id == 0)    // Incluindo
            {
                PreencheInfoCentroCustos(clsCentrocustosInfo);
                id = clsCentrocustosBLL.Incluir(clsCentrocustosInfo, conexao);
                clsInfo.zidincluir = id;
            }
            else
            {
                PreencheInfoCentroCustos(clsCentrocustosInfo);
                // Verifica se algo foi alterado, se foi, Salva
                if (clsCentrocustosBLL.Equals(clsCentrocustosInfoOld, clsCentrocustosInfo) == false)
                {
                    clsCentrocustosBLL.Alterar(clsCentrocustosInfo, conexao);
                }
            }
        }

        private void frmCentroCustos_Shown(object sender, EventArgs e)
        {
            
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
