using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmFormIdVis : Form
    {
        String conexao;
        String tabela;
        DataTable dtFormId;
        
        Int32 indexTabFormIdValor;
        Int32 idCodigo;
        clsTab_BancosInfo clsTab_BancosInfo;

        clsFormIdInfo clsFormIdInfoOld = new clsFormIdInfo();

        clsBasicReport clsBR;
        
        public frmFormIdVis()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         String _tabela,
                         Int32 _idcodigo,
                         clsTab_BancosInfo _clsTab_BancosInfo)
        {
            conexao = _conexao;
            tabela = _tabela;
            idCodigo = _idcodigo;
            clsTab_BancosInfo = _clsTab_BancosInfo;
            clsBR = new clsBasicReport(this, dgvFormId, toolTip1);
        }

        private void frmFormIdVis_Load(object sender, EventArgs e)
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

            bwrFormId.RunWorkerAsync();
            PreencheCamposTab_Bancos(clsTab_BancosInfo);

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

        private void bwrFormId_DoWork(object sender, DoWorkEventArgs e)
        {
            dtFormId = CarregaGridFormId();
        }

        private DataTable CarregaGridFormId()
        {
            try
            {
                clsFormIdBLL clsFormIdBLL = new clsFormIdBLL(tabela);
                return clsFormIdBLL.GridDados(idCodigo, conexao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrFormId_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvFormId.DataSource = dtFormId;
            if (dtFormId.Rows.Count > 0)
            {
                clsGridHelper.MontaGrid(dgvFormId,
                        new String[] { "Id.", "Código", "Nome" },
                        new String[] { "ID", "CODIGO", "NOME" },
                        new int[] { 10, 80, 650 },
                        new DataGridViewContentAlignment[] {
                                        DataGridViewContentAlignment.MiddleCenter,
                                        DataGridViewContentAlignment.MiddleRight,
                                        DataGridViewContentAlignment.MiddleLeft},
                        new bool[] { false, true, true },
                        true, 1, ListSortDirection.Ascending);

                if (indexTabFormIdValor > 0)
                {
                    foreach (DataGridViewRow linha in dgvFormId.Rows)
                    {
                        foreach (DataGridViewCell celula in dgvFormId.Rows[linha.Index].Cells)
                        {
                            if (dgvFormId.Columns[celula.ColumnIndex].Name == "ID")
                            {
                                if (dgvFormId.Rows[linha.Index].Cells["ID"].Value.ToString().ToLower() == indexTabFormIdValor.ToString().ToLower())
                                {
                                    try
                                    {
                                        dgvFormId.FirstDisplayedCell = dgvFormId.Rows[celula.RowIndex].Cells[2];
                                        dgvFormId[2, celula.RowIndex].Selected = true;
                                        dgvFormId.Select();
                                        return;
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            bwrFormId.Dispose();
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            frmFormId frmFormId = new frmFormId();
            frmFormId.Init(conexao, 0, tabela);

            clsFormHelper.AbrirForm(this.MdiParent, frmFormId, conexao);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (bwrFormId.IsBusy == false)
            {
                if ((Int32)dgvFormId.CurrentRow.Cells[0].Value > 0)
                {
                    frmFormId frmFormId = new frmFormId();
                    frmFormId.Init(conexao, (Int32)dgvFormId.CurrentRow.Cells[0].Value, tabela);

                    clsFormHelper.AbrirForm(this.MdiParent, frmFormId, conexao);
                }
                else
                {
                    MessageBox.Show("Primeiro selecione um registro.", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvFormId.Select();
                }
            }
        }

        private void tspProcurar_Click(object sender, EventArgs e)
        {
            //frmProcurar frmProcurar = new frmProcurar();
            //frmProcurar.Init(new DataGridView[] { this.dgvFormId }, true);

            //frmProcurar.ShowDialog();
        }



        private void frmFormIdVis_Activated(object sender, EventArgs e)
        {
            try
            {
                if (dtFormId != null && bwrFormId.IsBusy == false)
                {
                    bwrFormId.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvEscForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tspAlterar.PerformClick();
            }
        }

        private void dgvEscForm_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvFormId.CurrentRow;
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            this.Close();
            this.Dispose();
        }

        private void tspRetornar_Click_(object sender, EventArgs e)
        {
            clsInfo.zrow = null;
            this.Close();
            this.Dispose();
        }



        private void PreencheCamposTab_Bancos(clsTab_BancosInfo _info)
        {
            tbxCodigo.Text = _info.codigo.ToString();
            tbxCognome.Text = _info.cognome;
            tbxNome.Text = _info.nome;
        }

        private void tspIncluir_Click_1(object sender, EventArgs e)
        {
            tbxCodigo1.Text = "";
            tbxNome1.Text = "";

            indexTabFormIdValor = 0;
            gbxRegistrar.Visible = true;
            gbxCodigos.Enabled = false;
            tbxCodigo1.Select();
        }

        private void tspAlterar_Click_1(object sender, EventArgs e)
        {
            if (dgvFormId.CurrentRow != null)
            {
                //clsInfo.zidincluir = 0;
                indexTabFormIdValor = (Int32)dgvFormId.CurrentRow.Cells[0].Value;

                CarregaTabFormId(indexTabFormIdValor);

                gbxRegistrar.Visible = true;
                gbxCodigos.Enabled = false;
                //                frmTab_Bancos frmTab_Bancos = new frmTab_Bancos(conexao, (Int32)dgvTab_Bancos.CurrentRow.Cells[0].Value, dgvTab_Bancos.Rows);
                //                
                //                 clsFormHelper.AbrirForm(this.MdiParent, frmTab_Bancos);
            }
        }

        

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBR.Imprimir(clsInfo.caminhorelatorios, "Tipo Documento", clsInfo.conexaosqldados);
        }
        
        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void CarregaTabFormId(Int32 _id)
        {
            try
            {
                clsFormIdInfoOld = new clsFormIdInfo();
                if (indexTabFormIdValor > 0)    // Alterando / Visualizando
                {
                    clsFormIdBLL clsFormIdBLL = new clsFormIdBLL(tabela);
                    clsFormIdInfo clsFormIdInfo;

                    // Carrega os Dados
                    clsFormIdInfo = clsFormIdBLL.Carregar(indexTabFormIdValor, conexao);
                    PreencheCamposFormId(clsFormIdInfo);

                    PreencheInfoFormId(clsFormIdInfoOld);
                }
                else
                {
//                    tspPrimeiro.Enabled = false;
//                    tspAnterior.Enabled = false;
//                    tspProximo.Enabled = false;
//                    tspUltimo.Enabled = false;
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
        private void PreencheCamposFormId(clsFormIdInfo _info)
        {
//            id = _info.id;
            idCodigo = _info.idcodigo;
            tbxCodigo1.Text = _info.codigo.ToString();
            tbxNome1.Text = _info.nome;
        }

        private void PreencheInfoFormId(clsFormIdInfo _info)
        {
            

            _info.id = indexTabFormIdValor;
            _info.idcodigo = idCodigo;
            _info.codigo = tbxCodigo1.Text;
            _info.nome = tbxNome1.Text;

        }

        private void tspRetornar1_Click(object sender, EventArgs e)
        {
            gbxRegistrar.Visible = false;
            gbxCodigos.Enabled = true;
            bwrFormId.RunWorkerAsync();
        }


        private void SalvaFormId()
        {
            clsFormIdInfo _info = new clsFormIdInfo();
            PreencheInfoFormId(_info);

            if (indexTabFormIdValor == 0)
            {
                clsFormIdBLL clsFormIdBLL = new clsFormIdBLL(tabela);
                clsFormIdBLL.Incluir(_info, conexao);
            }
            else
            {
                clsFormIdBLL clsFormIdBLL = new clsFormIdBLL(tabela);
                clsFormIdBLL.Alterar(_info, conexao);
            }
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            SalvaFormId();

            gbxRegistrar.Visible = false;
            gbxCodigos.Enabled = true;

            bwrFormId.RunWorkerAsync();
        }

        private void tspEscolher_Click_1(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvFormId.CurrentRow;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Close();
                this.Dispose();
            }
             
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Int32)dgvFormId.CurrentRow.Cells[0].Value > 0)
                {
                    if (MessageBox.Show("Deseja realmente 'Excluir' o registro: " + dgvFormId.CurrentRow.Cells[2].Value + "?", "Aplisoft", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsFormIdBLL clsEscFormBLL = new clsFormIdBLL(tabela);
                        clsEscFormBLL.Excluir((Int32)dgvFormId.CurrentRow.Cells[0].Value, conexao);
                        bwrFormId.RunWorkerAsync();
                    }
                }
                else
                {
                    MessageBox.Show("Primeiro selecione um registro.", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvFormId.Select();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void ControlEnter(object sender, EventArgs e)
        {

            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            CamposFormId(sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void CamposFormId(object _ctrl)
        {
            if (_ctrl is TextBox)
            {
            }
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(toolStripTextBox1.Text, dgvFormId);
        }
    }

}
