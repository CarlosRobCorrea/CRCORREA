using CRCorreaFuncoes;
using System;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmSitTribCOFINS : Form
    {      
        private Boolean ok;
        //
        private DataGridView dgvSitTribCOFINS_fil;
        private DataGridView dgvSitTribCOFINS_compl;

        private String[] valoresantigos = new String[] { };
        private String[] valoresatuais = new String[] { };
        
        private delegate void AddnewDelegate(TextBox textbox, TextBox colecao);
        
        private DialogResult resultado = new DialogResult();
        
        
        

        private Int32 idSitTribCOFINSX;


        private Boolean leaveNocheck;

        public frmSitTribCOFINS()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridView _dgvSitTribCOFINS_fil,
                         DataGridView _dgvSitTribCOFINS_compl)
        {
            frmSitTribCOFINSVis.indexSitTribCOFINSGeral = 0;

            leaveNocheck = false;
            idSitTribCOFINSX = _id;
            dgvSitTribCOFINS_fil = _dgvSitTribCOFINS_fil;
            dgvSitTribCOFINS_compl = _dgvSitTribCOFINS_compl;

        }

        private void frmSitTribCOFINS_Load(object sender, EventArgs e)
        {
            CarregaSitTribCOFINS(idSitTribCOFINSX);
        }

        private void CarregaSitTribCOFINS(Int32 _id)
        {
            try
            {
                //Carrega os Dados 
                tspSitTribCOFINS.Cursor = Cursors.Hand;
                PreencheCamposSitTribCOFINS();
                valoresantigos = PreencheValoresSitTribCOFINS();
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
                MessageBox.Show("Carrega SitTribCOFINS - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                leaveNocheck = true;
            }
        }

        private void PreencheCamposSitTribCOFINS()
        {
            if (idSitTribCOFINSX > 0)
            {
                foreach (DataRow dgvrSitTribCOFINS in ((DataTable)dgvSitTribCOFINS_compl.DataSource).Rows)
                {
                    if (dgvrSitTribCOFINS.RowState != DataRowState.Deleted)
                    {
                        if (clsParser.Int32Parse(dgvrSitTribCOFINS["ID"].ToString()) == idSitTribCOFINSX)
                        {
                            tbxCodigo.Text = dgvrSitTribCOFINS["CODIGO"].ToString();                            
                            tbxNome.Text = dgvrSitTribCOFINS["NOME"].ToString();                            
                        }
                    }
                }
            }
        }

        private String[] PreencheValoresSitTribCOFINS()
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

        private void frmSitTribCOFINS_Shown(object sender, EventArgs e)
        {
            
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                resultado = DialogResult.No;
                valoresatuais = PreencheValoresSitTribCOFINS();
                if (clsSelectInsertUpdateBLL.ComparaValores(valoresatuais, valoresantigos) == false)
                {
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

                    if (resultado == DialogResult.Yes)
                    {
                        using (TransactionScope tsSitTribCOFINS = new TransactionScope())
                        {
                            SalvarSitTribCOFINS();
                            tsSitTribCOFINS.Complete();
                            tsSitTribCOFINS.Dispose();
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
                    if (idSitTribCOFINSX > 0)
                    {
                        frmSitTribCOFINSVis.indexSitTribCOFINSGeral = idSitTribCOFINSX;
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
                    using (TransactionScope tsSitTribCOFINS = new TransactionScope())
                    {
                        SalvarSitTribCOFINS();
                        tsSitTribCOFINS.Complete();
                        tsSitTribCOFINS.Dispose();
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
                    if (idSitTribCOFINSX > 0)
                    {
                        frmSitTribCOFINSVis.indexSitTribCOFINSGeral = idSitTribCOFINSX; ;
                    }
                    leaveNocheck = true;
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void SalvarSitTribCOFINS()
        {
            if (VerificaGravacaoSitTribCOFINS("TODOS") == true)
            {
                if (idSitTribCOFINSX == 0)    // Incluindo os valores do Cabeçalho -- valores locais
                {
                    idSitTribCOFINSX = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqldados, "SitTribCOFINS",
                                                                             "CODIGO," +                                                                             
                                                                             "NOME",
                                                               clsParser.SqlStringFormat(tbxCodigo.Text, false, "CODIGO") +        
                                                               clsParser.SqlStringFormat(tbxNome.Text, true, "NOME"));
                }
                else
                {
                    // Alterando os valores do Cabeçalho -- valores locais
                    clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "SitTribCOFINS",
                                                               "CODIGO = " + clsParser.SqlStringFormat(tbxCodigo.Text, false, "CODIGO") +
                                                               "NOME = " + clsParser.SqlStringFormat(tbxNome.Text, true, "NOME"),
                                                              "ID = " + idSitTribCOFINSX.ToString());


                }

                //logo devemos atualizar o grid de movimentaçao
                dgvSitTribCOFINS_compl.DataSource = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                   "SitTribCOFINS ",
                   "ID,CODIGO,NOME",
                   "", "ID");
            }            
        }      

        private Boolean VerificaGravacaoSitTribCOFINS(String _tipo)
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

        private Boolean MovimentaSitTribCOFINS()
        {
            try
            {
                ok = false;
                valoresatuais = PreencheValoresSitTribCOFINS();
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
                            SalvarSitTribCOFINS();
                        }
                    }
                    else if (resultado == DialogResult.No)
                    {
                        if (VerificaGravacaoSitTribCOFINS("TODOS") == false)
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
                    if (VerificaGravacaoSitTribCOFINS("TODOS") == false)
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
                MessageBox.Show("MovimentaSitTribCOFINS - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                if (MovimentaSitTribCOFINS())
                {
                    idSitTribCOFINSX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvSitTribCOFINS_fil.Rows)[0].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                     dgvSitTribCOFINS_fil[2, 0].Selected = true;                   
                    CarregaSitTribCOFINS(idSitTribCOFINSX);
                    leaveNocheck = true;
                    frmSitTribCOFINS_Activated(sender, e);
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
                if (MovimentaSitTribCOFINS())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection) dgvSitTribCOFINS_fil.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idSitTribCOFINSX)
                        {
                            if (dgvr.Index != 0)
                            {
                                idSitTribCOFINSX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvSitTribCOFINS_fil.Rows)[((DataGridViewRowCollection) dgvSitTribCOFINS_fil.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente                                
                                 dgvSitTribCOFINS_fil[2, ((DataGridViewRowCollection) dgvSitTribCOFINS_fil.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }                   
                    CarregaSitTribCOFINS(idSitTribCOFINSX);
                    leaveNocheck = true;
                    frmSitTribCOFINS_Activated(sender, e);

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
                if (MovimentaSitTribCOFINS())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection) dgvSitTribCOFINS_fil.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idSitTribCOFINSX)
                        {
                            if (dgvr.Index < ((DataGridViewRowCollection) dgvSitTribCOFINS_fil.Rows).Count - 1)
                            {
                                idSitTribCOFINSX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvSitTribCOFINS_fil.Rows)[((DataGridViewRowCollection) dgvSitTribCOFINS_fil.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente
                                 dgvSitTribCOFINS_fil[2, ((DataGridViewRowCollection) dgvSitTribCOFINS_fil.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }                    
                    CarregaSitTribCOFINS(idSitTribCOFINSX);
                    leaveNocheck = true;
                    frmSitTribCOFINS_Activated(sender, e);
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
                if (MovimentaSitTribCOFINS())
                {
                    idSitTribCOFINSX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvSitTribCOFINS_fil.Rows)[((DataGridViewRowCollection) dgvSitTribCOFINS_fil.Rows).Count - 1].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                     dgvSitTribCOFINS_fil[2, ((DataGridViewRowCollection) dgvSitTribCOFINS_fil.Rows).Count - 1].Selected = true;
                    CarregaSitTribCOFINS(idSitTribCOFINSX);
                    leaveNocheck = true;
                    frmSitTribCOFINS_Activated(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void frmSitTribCOFINS_Activated(object sender, EventArgs e)
        {

        }
    }
}
