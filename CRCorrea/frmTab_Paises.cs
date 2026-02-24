using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmTab_Paises : Form
    {
        private String conexao;
        private Int32 id;

        
        
        

        private clsTab_PaisesInfo clsTab_PaisesInfoOld;
        private DataGridViewRowCollection dgvrcnPaises;

        public frmTab_Paises()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         Int32 _id,
                         DataGridViewRowCollection _rowsPaises)
        {
            conexao = _conexao;
            id = _id;
            dgvrcnPaises = _rowsPaises;

            
            
            
        }

        private void frmPaises_Load(object sender, EventArgs e)
        {
            CarregaPaises(id);
        }

        private void CarregaPaises(Int32 _id)
        {
            clsTab_PaisesInfoOld = new clsTab_PaisesInfo();
            if (_id > 0) // Alterando / Visualizando
            {
                clsTab_PaisesBLL clsTab_PaisesBLL = new clsTab_PaisesBLL();
                clsTab_PaisesInfo clsTab_PaisesInfo = new clsTab_PaisesInfo();

                //Carrega os Dados
                clsTab_PaisesInfo = clsTab_PaisesBLL.Carregar(id, conexao);
                PreencheCamposPaises(clsTab_PaisesInfo);

                PreencheInfoPaises(clsTab_PaisesInfoOld);
            }
            else
            {
                tspPrimeiro.Enabled = false;
                tspAnterior.Enabled = false;
                tspProximo.Enabled = false;
                tspUltimo.Enabled = false;
            }

            tbxNome.Select();
            tbxNome.SelectAll();
        }

        private void PreencheCamposPaises(clsTab_PaisesInfo _info)
        {
            id = _info.id;
            tbxUF.Text = _info.uf;
            tbxNome.Text = _info.nome;
            tbxDDD.Text = _info.ddd;
            tbxDDDDireto.Text = _info.ddddireto;
            tbxISO3166A.Text = _info.iso3166a;
            tbxISO3166B.Text = _info.iso3166b;
            tbxISO3166C.Text = _info.iso3166c;
            tbxNomeInt.Text = _info.nomeint;
            tbxBacen.Text = _info.bacen.ToString();
            tbxArea.Text = _info.area.ToString();
            tbxPerimetro.Text = _info.perimetro.ToString();
            tbxAnoFundacao.Text = _info.anofundacao.ToString();
            tbxContinente.Text = _info.continente;
            tbxComex.Text = _info.comex;
        }

        private void PreencheInfoPaises(clsTab_PaisesInfo _info)
        {
            _info.id = id;
            _info.uf = tbxUF.Text;
            _info.nome = tbxNome.Text;
            _info.ddd = tbxDDD.Text;
            _info.ddddireto = tbxDDDDireto.Text;
            _info.iso3166a = tbxISO3166A.Text;
            _info.iso3166b = tbxISO3166B.Text;
            _info.iso3166c = tbxISO3166C.Text;
            _info.nomeint = tbxNomeInt.Text;
            _info.bacen = clsParser.Int32Parse(tbxBacen.Text);
            _info.area = clsParser.Int32Parse(tbxArea.Text);
            _info.perimetro = clsParser.Int32Parse(tbxPerimetro.Text);
            _info.anofundacao = clsParser.Int32Parse(tbxAnoFundacao.Text);
            _info.continente = tbxContinente.Text;
            _info.comex = tbxComex.Text;
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

        private void ControlKeyDownNumero(object sender, KeyEventArgs e)
        {

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
                    SalvarPaises();
                }
                else if (resultado == DialogResult.Cancel)
                {
                    tbxNome.Select();
                    tbxNome.SelectAll();
                    return;
                }
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tbxNome.Select();
                tbxNome.SelectAll();
            }
        }

        private void SalvarPaises()
        {
            clsTab_PaisesInfo clsTab_PaisesInfo = new clsTab_PaisesInfo();
            clsTab_PaisesBLL clsTab_PaisesBLL = new clsTab_PaisesBLL();

            PreencheInfoPaises(clsTab_PaisesInfo);
            if (id == 0)    // Incluindo
            {
                id = clsTab_PaisesBLL.Incluir(clsTab_PaisesInfo, conexao);
            }
            else
            {
                // Verifica se algo foi alterado, se foi, Salva
                if (clsTab_PaisesBLL.Equals(clsTab_PaisesInfoOld, clsTab_PaisesInfo) == false)
                {
                    clsTab_PaisesBLL.Alterar(clsTab_PaisesInfo, conexao);
                }
            }
        }

        public Boolean MovimentaPaises()
        {
            clsTab_PaisesBLL clsTab_PaisesBLL = new clsTab_PaisesBLL();
            clsTab_PaisesInfo clsTab_PaisesInfo = new clsTab_PaisesInfo();
            PreencheInfoPaises(clsTab_PaisesInfo);
            if (clsTab_PaisesBLL.Equals(clsTab_PaisesInfoOld, clsTab_PaisesInfo) == false)
            {
                DialogResult drt;
                drt = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    SalvarPaises();
                }
                else if (drt == DialogResult.Cancel)
                {
                    tbxNome.Select();
                    tbxNome.SelectAll();
                    return false;
                }
            }
            return true;
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (MovimentaPaises())
                {
                    id = Int32.Parse(dgvrcnPaises[0].Cells[0].Value.ToString());
                    CarregaPaises(id);
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
                if (MovimentaPaises())
                {
                    foreach (DataGridViewRow dgvr in dgvrcnPaises)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index != 0)
                            {
                                id = Int32.Parse(dgvrcnPaises[dgvrcnPaises.GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaPaises(id);
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
                if (MovimentaPaises())
                {
                    foreach (DataGridViewRow dgvr in dgvrcnPaises)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index < dgvrcnPaises.Count - 1)
                            {
                                id = Int32.Parse(dgvrcnPaises[dgvrcnPaises.GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaPaises(id);
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
                if (MovimentaPaises())
                {
                    id = Int32.Parse(dgvrcnPaises[dgvrcnPaises.Count - 1].Cells[0].Value.ToString());
                    CarregaPaises(id);
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
                clsTab_PaisesBLL clsTab_PaisesBLL = new clsTab_PaisesBLL();
                clsTab_PaisesInfo clsTab_PaisesInfo = new clsTab_PaisesInfo();

                PreencheInfoPaises(clsTab_PaisesInfo);

                if (clsTab_PaisesBLL.Equals(clsTab_PaisesInfo, clsTab_PaisesInfoOld) == false)
                {
                    DialogResult resultado;
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (resultado == DialogResult.Yes)
                    {
                        SalvarPaises();
                    }
                    else if (resultado == DialogResult.Cancel)
                    {
                        tbxUF.Select();
                        tbxUF.SelectAll();
                        return;
                    }
                }
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tbxNome.Select();
                tbxNome.SelectAll();
            }
        }

        private void frmPaises_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsInfo.zrow = null;
        }
    }
}
