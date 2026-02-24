using CRCorreaFuncoes;
using System;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmSitTribPIS : Form
    {
      
        private Boolean ok;
        //
        private DataGridView dgvSitTribPIS_fil;
        private DataGridView dgvSitTribPIS_compl;

        private String[] valoresantigos = new String[] { };
        private String[] valoresatuais = new String[] { };
       
        private delegate void AddnewDelegate(TextBox textbox, TextBox colecao);
        
        private DialogResult resultado = new DialogResult();
        
        
        

        private Int32 idSitTribPISX;


        private Boolean leaveNocheck;

        public frmSitTribPIS()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridView _dgvSitTribPIS_fil,
                         DataGridView _dgvSitTribPIS_compl)
        {
            frmSitTribPISVis.indexSitTribPISGeral = 0;

            leaveNocheck = false;
            idSitTribPISX = _id;
            dgvSitTribPIS_fil = _dgvSitTribPIS_fil;
            dgvSitTribPIS_compl = _dgvSitTribPIS_compl;

        }

        private void frmSitTribPIS_Load(object sender, EventArgs e)
        {
            CarregaSitTribPIS(idSitTribPISX);
        }

        private void CarregaSitTribPIS(Int32 _id)
        {
            try
            {
                //Carrega os Dados 
                tspSitTribPIS.Cursor = Cursors.Hand;
                PreencheCamposSitTribPIS();
                valoresantigos = PreencheValoresSitTribPIS();
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
                MessageBox.Show("Carrega SitTribPIS - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                leaveNocheck = true;
            }
        }

        private void PreencheCamposSitTribPIS()
        {
            if (idSitTribPISX > 0)
            {
                foreach (DataRow dgvrSitTribPIS in ((DataTable)dgvSitTribPIS_compl.DataSource).Rows)
                {
                    if (dgvrSitTribPIS.RowState != DataRowState.Deleted)
                    {
                        if (clsParser.Int32Parse(dgvrSitTribPIS["ID"].ToString()) == idSitTribPISX)
                        {
                            tbxCodigo.Text = dgvrSitTribPIS["CODIGO"].ToString();                            
                            tbxNome.Text = dgvrSitTribPIS["NOME"].ToString();                            
                        }
                    }
                }
            }
        }

        private String[] PreencheValoresSitTribPIS()
        {
            String[] _valores = {tbxCodigo.Text,
                                 tbxNome.Text};

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

        private void frmSitTribPIS_Shown(object sender, EventArgs e)
        {
            
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                resultado = DialogResult.No;
                valoresatuais = PreencheValoresSitTribPIS();
                if (clsSelectInsertUpdateBLL.ComparaValores(valoresatuais, valoresantigos) == false)
                {
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

                    if (resultado == DialogResult.Yes)
                    {
                        using (TransactionScope tsSitTribPIS = new TransactionScope())
                        {
                            SalvarSitTribPIS();
                            tsSitTribPIS.Complete();
                            tsSitTribPIS.Dispose();
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
                    if (idSitTribPISX > 0)
                    {
                        frmSitTribPISVis.indexSitTribPISGeral = idSitTribPISX;
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
                    using (TransactionScope tsSitTribPIS = new TransactionScope())
                    {
                        SalvarSitTribPIS();
                        tsSitTribPIS.Complete();
                        tsSitTribPIS.Dispose();
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
                    if (idSitTribPISX > 0)
                    {
                        frmSitTribPISVis.indexSitTribPISGeral = idSitTribPISX; ;
                    }
                    leaveNocheck = true;
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void SalvarSitTribPIS()
        {
            if (VerificaGravacaoSitTribPIS("TODOS") == true)
            {
                if (idSitTribPISX == 0)    // Incluindo os valores do Cabeçalho -- valores locais
                {
                    idSitTribPISX = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqldados, "SitTribPIS",
                                                                             "CODIGO," +                                                                             
                                                                             "NOME",
                                                               clsParser.SqlStringFormat(tbxCodigo.Text, false, "CODIGO") +        
                                                               clsParser.SqlStringFormat(tbxNome.Text, true, "NOME"));
                }
                else
                {
                    // Alterando os valores do Cabeçalho -- valores locais
                    clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "SitTribPIS",
                                                               "CODIGO = " + clsParser.SqlStringFormat(tbxCodigo.Text, false, "CODIGO") +
                                                               "NOME = " + clsParser.SqlStringFormat(tbxNome.Text, true, "NOME"),
                                                              "ID = " + idSitTribPISX.ToString());


                }

                //logo devemos atualizar o grid de movimentaçao
                dgvSitTribPIS_compl.DataSource = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                   "SitTribPIS ",
                   "ID,CODIGO,NOME",
                   "", "ID");
            }            
        }      

        private Boolean VerificaGravacaoSitTribPIS(String _tipo)
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

                    default:
                        break;
                }
                return false;
            }
            return true;
        }

        private Boolean MovimentaSitTribPIS()
        {
            try
            {
                ok = false;
                valoresatuais = PreencheValoresSitTribPIS();
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
                            SalvarSitTribPIS();
                        }
                    }
                    else if (resultado == DialogResult.No)
                    {
                        if (VerificaGravacaoSitTribPIS("TODOS") == false)
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
                    if (VerificaGravacaoSitTribPIS("TODOS") == false)
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
                MessageBox.Show("MovimentaSitTribPIS - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                if (MovimentaSitTribPIS())
                {
                    idSitTribPISX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvSitTribPIS_fil.Rows)[0].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                     dgvSitTribPIS_fil[2, 0].Selected = true;                   
                    CarregaSitTribPIS(idSitTribPISX);
                    leaveNocheck = true;
                    frmSitTribPIS_Activated(sender, e);
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
                if (MovimentaSitTribPIS())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection) dgvSitTribPIS_fil.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idSitTribPISX)
                        {
                            if (dgvr.Index != 0)
                            {
                                idSitTribPISX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvSitTribPIS_fil.Rows)[((DataGridViewRowCollection) dgvSitTribPIS_fil.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente                                
                                 dgvSitTribPIS_fil[2, ((DataGridViewRowCollection) dgvSitTribPIS_fil.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }                   
                    CarregaSitTribPIS(idSitTribPISX);
                    leaveNocheck = true;
                    frmSitTribPIS_Activated(sender, e);

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
                if (MovimentaSitTribPIS())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection) dgvSitTribPIS_fil.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idSitTribPISX)
                        {
                            if (dgvr.Index < ((DataGridViewRowCollection) dgvSitTribPIS_fil.Rows).Count - 1)
                            {
                                idSitTribPISX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvSitTribPIS_fil.Rows)[((DataGridViewRowCollection) dgvSitTribPIS_fil.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente
                                 dgvSitTribPIS_fil[2, ((DataGridViewRowCollection) dgvSitTribPIS_fil.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }                    
                    CarregaSitTribPIS(idSitTribPISX);
                    leaveNocheck = true;
                    frmSitTribPIS_Activated(sender, e);
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
                if (MovimentaSitTribPIS())
                {
                    idSitTribPISX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvSitTribPIS_fil.Rows)[((DataGridViewRowCollection) dgvSitTribPIS_fil.Rows).Count - 1].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                     dgvSitTribPIS_fil[2, ((DataGridViewRowCollection) dgvSitTribPIS_fil.Rows).Count - 1].Selected = true;
                    CarregaSitTribPIS(idSitTribPISX);
                    leaveNocheck = true;
                    frmSitTribPIS_Activated(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void frmSitTribPIS_Activated(object sender, EventArgs e)
        {

        }
    }
}
