using CRCorreaFuncoes;
using System;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmSitTribIPI : Form
    {
      
        private Boolean ok;
        //
        private DataGridView dgvSitTribIPI_fil;
        private DataGridView dgvSitTribIPI_compl;

        private String[] valoresantigos = new String[] { };
        private String[] valoresatuais = new String[] { };
        
        private delegate void AddnewDelegate(TextBox textbox, TextBox colecao);
        
        private DialogResult resultado = new DialogResult();
        
        
        

        private Int32 idSitTribIPIX;


        private Boolean leaveNocheck;

        public frmSitTribIPI()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridView _dgvSitTribIPI_fil,
                         DataGridView _dgvSitTribIPI_compl)
        {
            frmSitTribIPIVis.indexSitTribIPIGeral = 0;

            leaveNocheck = false;
            idSitTribIPIX = _id;
            dgvSitTribIPI_fil = _dgvSitTribIPI_fil;
            dgvSitTribIPI_compl = _dgvSitTribIPI_compl;

        }

        private void frmSitTribIPI_Load(object sender, EventArgs e)
        {
            CarregaSitTribIPI(idSitTribIPIX);
        }

        private void CarregaSitTribIPI(Int32 _id)
        {
            try
            {
                //Carrega os Dados 
                tspSitTribIPI.Cursor = Cursors.Hand;
                PreencheCamposSitTribIPI();
                valoresantigos = PreencheValoresSitTribIPI();
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
                MessageBox.Show("Carrega SitTribIPI - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                leaveNocheck = true;
            }
        }

        private void PreencheCamposSitTribIPI()
        {
            if (idSitTribIPIX > 0)
            {
                foreach (DataRow dgvrSitTribIPI in ((DataTable)dgvSitTribIPI_compl.DataSource).Rows)
                {
                    if (dgvrSitTribIPI.RowState != DataRowState.Deleted)
                    {
                        if (clsParser.Int32Parse(dgvrSitTribIPI["ID"].ToString()) == idSitTribIPIX)
                        {
                            tbxCodigo.Text = dgvrSitTribIPI["CODIGO"].ToString();                            
                            tbxNome.Text = dgvrSitTribIPI["NOME"].ToString();                            
                        }
                    }
                }
            }
        }

        private String[] PreencheValoresSitTribIPI()
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

        private void frmSitTribIPI_Shown(object sender, EventArgs e)
        {
            
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                resultado = DialogResult.No;
                valoresatuais = PreencheValoresSitTribIPI();
                if (clsSelectInsertUpdateBLL.ComparaValores(valoresatuais, valoresantigos) == false)
                {
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

                    if (resultado == DialogResult.Yes)
                    {
                        using (TransactionScope tsSitTribIPI = new TransactionScope())
                        {
                            SalvarSitTribIPI();
                            tsSitTribIPI.Complete();
                            tsSitTribIPI.Dispose();
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
                    if (idSitTribIPIX > 0)
                    {
                        frmSitTribIPIVis.indexSitTribIPIGeral = idSitTribIPIX;
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
                    using (TransactionScope tsSitTribIPI = new TransactionScope())
                    {
                        SalvarSitTribIPI();
                        tsSitTribIPI.Complete();
                        tsSitTribIPI.Dispose();
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
                    if (idSitTribIPIX > 0)
                    {
                        frmSitTribIPIVis.indexSitTribIPIGeral = idSitTribIPIX; ;
                    }
                    leaveNocheck = true;
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void SalvarSitTribIPI()
        {
            if (VerificaGravacaoSitTribIPI("TODOS") == true)
            {
                if (idSitTribIPIX == 0)    // Incluindo os valores do Cabeçalho -- valores locais
                {
                    idSitTribIPIX = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqldados, "SitTribIPI",
                                                                             "CODIGO," +                                                                             
                                                                             "NOME",
                                                               clsParser.SqlStringFormat(tbxCodigo.Text.Trim(), false, "CODIGO") +        
                                                               clsParser.SqlStringFormat(tbxNome.Text, true, "NOME"));
                }
                else
                {
                    // Alterando os valores do Cabeçalho -- valores locais
                    clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "SitTribIPI",
                                                               "CODIGO = " + clsParser.SqlStringFormat(tbxCodigo.Text.Trim(), false, "CODIGO") +
                                                               "NOME = " + clsParser.SqlStringFormat(tbxNome.Text, true, "NOME"),
                                                              "ID = " + idSitTribIPIX.ToString());


                }

                //logo devemos atualizar o grid de movimentaçao
                dgvSitTribIPI_compl.DataSource = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                   "SitTribIPI ",
                   "ID,CODIGO,NOME",
                   "", "ID");
            }            
        }      

        private Boolean VerificaGravacaoSitTribIPI(String _tipo)
        {
            try
            {
                //if (Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                //    "select codigo from SITTRIBIPI where codigo= '" + tbxCodigo.Text.Trim() +
                //    "' and id <> " + idSitTribIPIX, "X").ToString() != "X")
                //{
                //    throw new Exception("Codigo já registrado, escolha outro");
                //}
                //else
                //{
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
                //}
               
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

        private Boolean MovimentaSitTribIPI()
        {
            try
            {
                ok = false;
                valoresatuais = PreencheValoresSitTribIPI();
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
                            SalvarSitTribIPI();
                        }
                    }
                    else if (resultado == DialogResult.No)
                    {
                        if (VerificaGravacaoSitTribIPI("TODOS") == false)
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
                    if (VerificaGravacaoSitTribIPI("TODOS") == false)
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
                MessageBox.Show("MovimentaSitTribIPI - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                if (MovimentaSitTribIPI())
                {
                    idSitTribIPIX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvSitTribIPI_fil.Rows)[0].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                     dgvSitTribIPI_fil[2, 0].Selected = true;                   
                    CarregaSitTribIPI(idSitTribIPIX);
                    leaveNocheck = true;
                    frmSitTribIPI_Activated(sender, e);
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
                if (MovimentaSitTribIPI())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection) dgvSitTribIPI_fil.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idSitTribIPIX)
                        {
                            if (dgvr.Index != 0)
                            {
                                idSitTribIPIX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvSitTribIPI_fil.Rows)[((DataGridViewRowCollection) dgvSitTribIPI_fil.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente                                
                                 dgvSitTribIPI_fil[2, ((DataGridViewRowCollection) dgvSitTribIPI_fil.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }                   
                    CarregaSitTribIPI(idSitTribIPIX);
                    leaveNocheck = true;
                    frmSitTribIPI_Activated(sender, e);

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
                if (MovimentaSitTribIPI())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection) dgvSitTribIPI_fil.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idSitTribIPIX)
                        {
                            if (dgvr.Index < ((DataGridViewRowCollection) dgvSitTribIPI_fil.Rows).Count - 1)
                            {
                                idSitTribIPIX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvSitTribIPI_fil.Rows)[((DataGridViewRowCollection) dgvSitTribIPI_fil.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente
                                 dgvSitTribIPI_fil[2, ((DataGridViewRowCollection) dgvSitTribIPI_fil.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }                    
                    CarregaSitTribIPI(idSitTribIPIX);
                    leaveNocheck = true;
                    frmSitTribIPI_Activated(sender, e);
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
                if (MovimentaSitTribIPI())
                {
                    idSitTribIPIX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvSitTribIPI_fil.Rows)[((DataGridViewRowCollection) dgvSitTribIPI_fil.Rows).Count - 1].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                     dgvSitTribIPI_fil[2, ((DataGridViewRowCollection) dgvSitTribIPI_fil.Rows).Count - 1].Selected = true;
                    CarregaSitTribIPI(idSitTribIPIX);
                    leaveNocheck = true;
                    frmSitTribIPI_Activated(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void frmSitTribIPI_Activated(object sender, EventArgs e)
        {

        }
    }
}
