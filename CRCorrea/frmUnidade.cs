using CRCorreaFuncoes;
using System;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmUnidade : Form
    {
        private Boolean ok;
        
        private DataGridView dgvUnidade_fil;
        private DataGridView dgvUnidade_compl;

        private String[] valoresantigos = new String[] { };
        private String[] valoresatuais = new String[] { };
        
        private delegate void AddnewDelegate(TextBox textbox, TextBox colecao);
        
        private DialogResult resultado = new DialogResult();
        
        
        

        private Int32 idUnidadeX;


        private Boolean leaveNocheck;

        public frmUnidade()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridView _dgvUnidade_fil,
                         DataGridView _dgvUnidade_compl)
        {
            frmUnidadeVis.indexUnidadeGeral = 0;

            leaveNocheck = false;
            idUnidadeX = _id;
            dgvUnidade_fil = _dgvUnidade_fil;
            dgvUnidade_compl = _dgvUnidade_compl;

        }

        private void frmUnidade_Load(object sender, EventArgs e)
        {
            CarregaUnidade(idUnidadeX);
        }

        private void CarregaUnidade(Int32 _id)
        {
            try
            {
                //Carrega os Dados 
                tspUnidade.Cursor = Cursors.Hand;
                PreencheCamposUnidade();
                valoresantigos = PreencheValoresUnidade();
                if (_id == 0)
                {                   
                    tspPrimeiro.Enabled = false;
                    tspAnterior.Enabled = false;
                    tspProximo.Enabled = false;
                    tspUltimo.Enabled = false;                    
                }
                tbxCodigo.Select();
                tbxCodigo.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Carrega Unidade - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                leaveNocheck = true;
            }
        }

        private void PreencheCamposUnidade()
        {
            if (idUnidadeX > 0)
            {
                foreach (DataRow dgvrUnidade in ((DataTable)dgvUnidade_compl.DataSource).Rows)
                {
                    if (dgvrUnidade.RowState != DataRowState.Deleted)
                    {
                        if (clsParser.Int32Parse(dgvrUnidade["ID"].ToString()) == idUnidadeX)
                        {
                            tbxCodigo.Text = dgvrUnidade["CODIGO"].ToString();                            
                            tbxNome.Text = dgvrUnidade["NOME"].ToString();
                            tbxUnidDec.Text = dgvrUnidade["UNIDDEC"].ToString();
                        }
                    }
                }
            }
        }

        private String[] PreencheValoresUnidade()
        {
            String[] _valores = {tbxCodigo.Text,
                                 tbxNome.Text,
                                tbxUnidDec.Text};

            return _valores;
        }

       

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            if (leaveNocheck == false)
            {
                clsVisual.ControlLeave(sender);               
            }
            else
            {
                leaveNocheck = false;
                clsVisual.ControlLeave(sender);
            }
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void frmUnidade_Shown(object sender, EventArgs e)
        {
            
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                resultado = DialogResult.No;
                valoresatuais = PreencheValoresUnidade();
                if (clsSelectInsertUpdateBLL.ComparaValores(valoresatuais, valoresantigos) == false)
                {
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

                    if (resultado == DialogResult.Yes)
                    {
                        using (TransactionScope tsUnidade = new TransactionScope())
                        {
                            SalvarUnidade();
                            tsUnidade.Complete();
                            tsUnidade.Dispose();
                        }
                    }
                    else if (resultado == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            finally
            {
                if (resultado != DialogResult.Cancel)
                {
                    if (idUnidadeX > 0)
                    {
                        frmUnidadeVis.indexUnidadeGeral = idUnidadeX;
                    }
                    leaveNocheck = true;
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft",
                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button1);

                if (resultado == DialogResult.Yes)
                {
                    using (TransactionScope tsUnidade = new TransactionScope())
                    {
                        SalvarUnidade();
                        tsUnidade.Complete();
                        tsUnidade.Dispose();
                    }
                }
                else if (resultado == DialogResult.Cancel)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                if (resultado != DialogResult.Cancel)
                {
                    if (idUnidadeX > 0)
                    {
                        frmUnidadeVis.indexUnidadeGeral = idUnidadeX; ;
                    }
                    leaveNocheck = true;
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void SalvarUnidade()
        {
            if (VerificaGravacaoUnidade("TODOS") == true)
            {
                if (idUnidadeX == 0)    // Incluindo os valores do Cabeçalho -- valores locais
                {
                    idUnidadeX = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqldados, "Unidade",
                                                                             "CODIGO," +                                                                             
                                                                             "NOME," +
                                                                             "UNIDDEC",
                                                               clsParser.SqlStringFormat(tbxCodigo.Text, false, "CODIGO") +        
                                                               clsParser.SqlStringFormat(tbxNome.Text, false, "NOME") +
                                                               clsParser.SqlStringFormat(tbxUnidDec.Text, true, "UNIDDEC"));
                }
                else
                {
                    // Alterando os valores do Cabeçalho -- valores locais
                    clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "Unidade",
                                                               "CODIGO = " + clsParser.SqlStringFormat(tbxCodigo.Text, false, "CODIGO") +
                                                               "NOME = " + clsParser.SqlStringFormat(tbxNome.Text, false, "NOME") +
                                                               "UNIDDEC= " + clsParser.SqlStringFormat(tbxUnidDec.Text,true,"UNIDDEC"),
                                                              "ID = " + idUnidadeX.ToString());


                }

                //logo devemos atualizar o grid de movimentaçao
                dgvUnidade_compl.DataSource = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                   "Unidade ",
                   "ID,CODIGO,NOME,UNIDDEC",
                   "", "ID");
            }            
        }      

        private Boolean VerificaGravacaoUnidade(String _tipo)
        {
            try
            {
                // Verficando os campos
                if (_tipo == "TODOS")
                {
                    if (tbxCodigo.Text.Trim() == "")
                    {
                        throw new Exception("Informe o Codigo");
                    }

                    if (tbxNome.Text.Trim() == "")
                    {
                        throw new Exception("Informe o Nome");
                    }

                    if (tbxUnidDec.Text.Trim() == "")
                    {
                        throw new Exception("Informe as Casas decimais");
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = DialogResult.Cancel; // Retorna para o foco
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                switch (ex.Message)
                {
                    case "Informe o Codigo":
                        tbxCodigo.Select();
                        tbxCodigo.SelectAll();
                        break;

                    case "Informe o Nome":
                        tbxNome.Select();
                        tbxNome.SelectAll();
                        break;

                    case "Informe as Casas decimais":
                        tbxUnidDec.Select();
                        tbxUnidDec.SelectAll();
                        break;

                    default:
                        break;
                }
                return false;
            }
            return true;
        }

        private Boolean MovimentaUnidade()
        {
            try
            {
                ok = false;
                valoresatuais = PreencheValoresUnidade();
                if (clsSelectInsertUpdateBLL.ComparaValores(valoresatuais, valoresantigos) == false)
                {
                    ok = true;
                }
               

                if (ok)
                {
                    resultado = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        if (resultado != DialogResult.Cancel)
                        {
                            SalvarUnidade();
                        }
                    }
                    else if (resultado == DialogResult.No)
                    {
                        if (VerificaGravacaoUnidade("TODOS") == false)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else if (resultado == DialogResult.Cancel)
                    {
                        return false;
                    }
                }
                else
                {
                    if (VerificaGravacaoUnidade("TODOS") == false)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = DialogResult.None;
                MessageBox.Show("MovimentaUnidade - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            if (resultado == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                resultado = DialogResult.No;
                if (MovimentaUnidade())
                {
                    idUnidadeX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvUnidade_fil.Rows)[0].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                     dgvUnidade_fil[2, 0].Selected = true;                   
                    CarregaUnidade(idUnidadeX);
                    leaveNocheck = true;
                    frmUnidade_Activated(sender, e);
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
                resultado = DialogResult.No;
                if (MovimentaUnidade())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection) dgvUnidade_fil.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idUnidadeX)
                        {
                            if (dgvr.Index != 0)
                            {
                                idUnidadeX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvUnidade_fil.Rows)[((DataGridViewRowCollection) dgvUnidade_fil.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente                                
                                 dgvUnidade_fil[2, ((DataGridViewRowCollection) dgvUnidade_fil.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }                   
                    CarregaUnidade(idUnidadeX);
                    leaveNocheck = true;
                    frmUnidade_Activated(sender, e);

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
                resultado = DialogResult.No;
                if (MovimentaUnidade())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection) dgvUnidade_fil.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idUnidadeX)
                        {
                            if (dgvr.Index < ((DataGridViewRowCollection) dgvUnidade_fil.Rows).Count - 1)
                            {
                                idUnidadeX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvUnidade_fil.Rows)[((DataGridViewRowCollection) dgvUnidade_fil.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente
                                 dgvUnidade_fil[2, ((DataGridViewRowCollection) dgvUnidade_fil.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }                    
                    CarregaUnidade(idUnidadeX);
                    leaveNocheck = true;
                    frmUnidade_Activated(sender, e);
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
                resultado = DialogResult.No;
                if (MovimentaUnidade())
                {
                    idUnidadeX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvUnidade_fil.Rows)[((DataGridViewRowCollection) dgvUnidade_fil.Rows).Count - 1].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                     dgvUnidade_fil[2, ((DataGridViewRowCollection) dgvUnidade_fil.Rows).Count - 1].Selected = true;
                    CarregaUnidade(idUnidadeX);
                    leaveNocheck = true;
                    frmUnidade_Activated(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void frmUnidade_Activated(object sender, EventArgs e)
        {

        }
    }
}
