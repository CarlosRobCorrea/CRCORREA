using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmTab_CNAE : Form
    {
        Boolean ok;

        DataGridView dgvTab_CNAE_fil;
        DataGridView dgvTab_CNAE_compl;

        String[] valoresantigos = new String[] { };
        String[] valoresatuais = new String[] { };
        
        delegate void AddnewDelegate(TextBox textbox, TextBox colecao);

        Int32 indexTab_CNAE_UF;
        Int32 idTab_CNAE_UF;
        DataTable dtTab_CNAE_UF;

        DialogResult resultado = new DialogResult();
        
        
        

        Int32 idTab_CNAEX;

        Boolean leaveNocheck;

        public frmTab_CNAE()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridView _dgvTab_CNAE_fil,
                         DataGridView _dgvTab_CNAE_compl)
        {
            frmTab_CNAE_Vis.indexTab_CNAEGeral = 0;

            frmTab_CNAE_Vis.dtTab_CNAE_UFGeral = null;
            frmTab_CNAE_Vis.deleTab_CNAE_UFGeral.Clear();
            frmTab_CNAE_Vis.drTab_CNAE_UFGeral = null;
            frmTab_CNAE_Vis.indexTab_CNAE_UFGeral = 0;
            frmTab_CNAE_Vis.achouTab_CNAE_UFGeral = false;

            leaveNocheck = false;
            idTab_CNAEX = _id;
            dgvTab_CNAE_fil = _dgvTab_CNAE_fil;
            dgvTab_CNAE_compl = _dgvTab_CNAE_compl;

        }

        private void frmTab_CNAE_Load(object sender, EventArgs e)
        {
            CarregaTab_CNAE(idTab_CNAEX);
        }

        private void CarregaTab_CNAE(Int32 _id)
        {
            try
            {
                //Carrega os Dados 
                tspTab_CNAE.Cursor = Cursors.Hand;
                PreencheCamposTab_CNAE();
                valoresantigos = PreencheValoresTab_CNAE();
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
                MessageBox.Show("Carrega Tab_CNAE - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                leaveNocheck = true;
            }
        }

        private void PreencheCamposTab_CNAE()
        {
            if (idTab_CNAEX > 0)
            {
                foreach (DataRow dgvrTab_CNAE in ((DataTable)dgvTab_CNAE_compl.DataSource).Rows)
                {
                    if (dgvrTab_CNAE.RowState != DataRowState.Deleted)
                    {
                        if (clsParser.Int32Parse(dgvrTab_CNAE["ID"].ToString()) == idTab_CNAEX)
                        {
                            tbxCodigo.Text = dgvrTab_CNAE["CODIGO"].ToString();
                            tbxNome.Text = dgvrTab_CNAE["NOME"].ToString();
                        }
                    }
                }
            }
        }

        private String[] PreencheValoresTab_CNAE()
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

        private void frmTab_CNAE_Shown(object sender, EventArgs e)
        {
            
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                resultado = DialogResult.No;
                valoresatuais = PreencheValoresTab_CNAE();
                if (clsSelectInsertUpdateBLL.ComparaValores(valoresatuais, valoresantigos) == false)
                {
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

                    if (resultado == DialogResult.Yes)
                    {
                        using (TransactionScope tsTab_CNAE = new TransactionScope())
                        {
                            SalvarTab_CNAE();
                            tsTab_CNAE.Complete();
                            tsTab_CNAE.Dispose();
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
                    if (idTab_CNAEX > 0)
                    {
                        frmTab_CNAE_Vis.indexTab_CNAEGeral = idTab_CNAEX;
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
                    using (TransactionScope tsTab_CNAE = new TransactionScope())
                    {
                        SalvarTab_CNAE();
                        tsTab_CNAE.Complete();
                        tsTab_CNAE.Dispose();
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
                    if (idTab_CNAEX > 0)
                    {
                        frmTab_CNAE_Vis.indexTab_CNAEGeral = idTab_CNAEX; ;
                    }
                    leaveNocheck = true;
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void SalvarTab_CNAE()
        {
            if (VerificaGravacaoTab_CNAE("TODOS") == true)
            {
            if (idTab_CNAEX == 0)    // Incluindo os valores do Cabeçalho -- valores locais
            {
                idTab_CNAEX = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqldados, "TAB_CNAE",
                                                                         "CODIGO," +
                                                                         "NOME",
                                                         clsParser.SqlStringFormat(tbxCodigo.Text, false, "CODIGO") +
                                                         clsParser.SqlStringFormat(tbxNome.Text, true, "NOME"));
            }
            else
            {
                // Alterando os valores do Cabeçalho -- valores locais
                clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "TAB_CNAE",
                                                          "CODIGO = " + clsParser.SqlStringFormat(tbxCodigo.Text, false, "CODIGO") +
                                                          "NOME = " + clsParser.SqlStringFormat(tbxNome.Text, true, "NOME"),
                                                          "ID = " + idTab_CNAEX.ToString());


            }

            //logo devemos atualizar o grid de movimentaçao
            dgvTab_CNAE_compl.DataSource = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
               "TAB_CNAE ",
               "ID,CODIGO,NOME",
               "", "ID");
            //agora inclui ou altera os DataTable gerais temporarios

            if (frmTab_CNAE_Vis.dtTab_CNAE_UFGeral.Rows.Count > 0)
            {
                foreach (DataRow dgvrTab_CNAE_UF in frmTab_CNAE_Vis.dtTab_CNAE_UFGeral.Rows)
                {
                    if (dgvrTab_CNAE_UF.RowState == DataRowState.Added)
                    {
                        idTab_CNAE_UF = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqldados, "TAB_CNAE_UF",
                                                         "IDTABCNAE," +
                                                         "IDUF," +
                                                         "IVA, " +
                                                         "IDDIZERESNFV, " +
                                                         "ICMSST" ,
                        clsParser.SqlInt32Format(idTab_CNAEX, false, "IDTABCNAE") +
                        clsParser.SqlInt32Format(dgvrTab_CNAE_UF["IDUF"].ToString(), false, "IDUF") +
                        clsParser.SqlDecimalFormat(dgvrTab_CNAE_UF["IVA"].ToString(), false, "IVA") +
                        clsParser.SqlInt32Format(dgvrTab_CNAE_UF["IDDIZERESNFV"].ToString(), false, "IDDIZERESNFV") +
                        clsParser.SqlDecimalFormat(dgvrTab_CNAE_UF["ICMSST"].ToString(), true, "ICMSST"));
                         
                    }
                    else if (dgvrTab_CNAE_UF.RowState == DataRowState.Modified)
                    {
                        idTab_CNAE_UF = clsParser.Int32Parse(dgvrTab_CNAE_UF["ID"].ToString());

                        clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "TAB_CNAE_UF",
                        "IDTABCNAE = " + clsParser.SqlInt32Format(dgvrTab_CNAE_UF["IDTABCNAE"].ToString(), false, "IDTABCNAE") +
                        "IDUF = " + clsParser.SqlInt32Format(dgvrTab_CNAE_UF["IDUF"].ToString(), false, "IDUF") +
                        "IVA = " + clsParser.SqlDecimalFormat(dgvrTab_CNAE_UF["IVA"].ToString(), false, "IVA") +
                        "IDDIZERESNFV = " + clsParser.SqlDecimalFormat(dgvrTab_CNAE_UF["IDDIZERESNFV"].ToString(), false, "IVA") +
                        "ICMSST = " + clsParser.SqlDecimalFormat(dgvrTab_CNAE_UF["ICMSST"].ToString(), true, "ICMSST"),
                        "ID = " + dgvrTab_CNAE_UF["ID"].ToString());
                    }
                }
            }
                //deletando
                for (Int32 X = 0; X < frmTab_CNAE_Vis.deleTab_CNAE_UFGeral.Count; X++)
                {
                    //agora a principal
                    clsSelectInsertUpdateBLL.Delete(clsInfo.conexaosqldados, "TAB_CNAE_UF", "ID = " + frmTab_CNAE_Vis.deleTab_CNAE_UFGeral[X].ToString());
                }
            }
        }      

        private Boolean VerificaGravacaoTab_CNAE(String _tipo)
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

        private Boolean MovimentaTab_CNAE()
        {
            try
            {
                ok = false;
                valoresatuais = PreencheValoresTab_CNAE();
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
                            SalvarTab_CNAE();
                        }
                    }
                    else if (resultado == DialogResult.No)
                    {
                        if (VerificaGravacaoTab_CNAE("TODOS") == false)
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
                    if (VerificaGravacaoTab_CNAE("TODOS") == false)
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
                MessageBox.Show("MovimentaTab_CNAE - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

        private void LimpaResiduos()
        {
            frmTab_CNAE_Vis.dtTab_CNAE_UFGeral = null;
            frmTab_CNAE_Vis.deleTab_CNAE_UFGeral.Clear();
            frmTab_CNAE_Vis.drTab_CNAE_UFGeral = null;
            frmTab_CNAE_Vis.indexTab_CNAE_UFGeral = 0;
            frmTab_CNAE_Vis.achouTab_CNAE_UFGeral = false;
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                resultado = DialogResult.No;
                if (MovimentaTab_CNAE())
                {
                    idTab_CNAEX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvTab_CNAE_fil.Rows)[0].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                     dgvTab_CNAE_fil[2, 0].Selected = true;
                    LimpaResiduos();
                    CarregaTab_CNAE(idTab_CNAEX);
                    leaveNocheck = true;
                    frmTab_CNAE_Activated(sender, e);
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
                if (MovimentaTab_CNAE())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection) dgvTab_CNAE_fil.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idTab_CNAEX)
                        {
                            if (dgvr.Index != 0)
                            {
                                idTab_CNAEX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvTab_CNAE_fil.Rows)[((DataGridViewRowCollection) dgvTab_CNAE_fil.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente                                
                                 dgvTab_CNAE_fil[2, ((DataGridViewRowCollection) dgvTab_CNAE_fil.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    LimpaResiduos();
                    CarregaTab_CNAE(idTab_CNAEX);
                    leaveNocheck = true;
                    frmTab_CNAE_Activated(sender, e);

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
                if (MovimentaTab_CNAE())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection) dgvTab_CNAE_fil.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idTab_CNAEX)
                        {
                            if (dgvr.Index < ((DataGridViewRowCollection) dgvTab_CNAE_fil.Rows).Count - 1)
                            {
                                idTab_CNAEX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvTab_CNAE_fil.Rows)[((DataGridViewRowCollection) dgvTab_CNAE_fil.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente
                                 dgvTab_CNAE_fil[2, ((DataGridViewRowCollection) dgvTab_CNAE_fil.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    LimpaResiduos();
                    CarregaTab_CNAE(idTab_CNAEX);
                    leaveNocheck = true;
                    frmTab_CNAE_Activated(sender, e);
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
                if (MovimentaTab_CNAE())
                {
                    idTab_CNAEX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvTab_CNAE_fil.Rows)[((DataGridViewRowCollection) dgvTab_CNAE_fil.Rows).Count - 1].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                     dgvTab_CNAE_fil[2, ((DataGridViewRowCollection) dgvTab_CNAE_fil.Rows).Count - 1].Selected = true;
                    LimpaResiduos();
                    CarregaTab_CNAE(idTab_CNAEX);
                    leaveNocheck = true;
                    frmTab_CNAE_Activated(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        ////
        ////
        //começa a partir daqui o controle sobre a tabela Tab_CNAE_UF
        private void bwrTab_CNAE_UF_RunWorkerAsync()
        {
            pbxTab_CNAE_UF.Visible = true;
            bwrTab_CNAE_UF = new BackgroundWorker();
            bwrTab_CNAE_UF.DoWork += new DoWorkEventHandler(bwrTab_CNAE_UF_DoWork);
            bwrTab_CNAE_UF.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrTab_CNAE_UF_RunWorkerCompleted);
            bwrTab_CNAE_UF.RunWorkerAsync();
        }

        private void bwrTab_CNAE_UF_DoWork(object sender, DoWorkEventArgs e)
        {
            dtTab_CNAE_UF = CarregaGridTab_CNAE_UF();
            dtTab_CNAE_UF.Columns.Add("TEMP", Type.GetType("System.String"));

            //
            if (frmTab_CNAE_Vis.dtTab_CNAE_UFGeral == null || frmTab_CNAE_Vis.dtTab_CNAE_UFGeral.Rows.Count == 0)
            {
                frmTab_CNAE_Vis.dtTab_CNAE_UFGeral = dtTab_CNAE_UF.Clone();
            }
        }

        public DataTable CarregaGridTab_CNAE_UF()
        {
            try
            {

                return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                       "TAB_CNAE_UF INNER JOIN ESTADOS ON TAB_CNAE_UF.IDUF = ESTADOS.ID",
                       "TAB_CNAE_UF.ID,TAB_CNAE_UF.IDTABCNAE,TAB_CNAE_UF.IDUF,ESTADOS.ESTADO,TAB_CNAE_UF.IVA," +
                       "TAB_CNAE_UF.IDDIZERESNFV,TAB_CNAE_UF.ICMSST",
                       "TAB_CNAE_UF.IDTABCNAE = " + idTab_CNAEX, "TAB_CNAE_UF.ID");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrTab_CNAE_UF_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvTab_CNAE_UF.DataSource = dtTab_CNAE_UF;
                dgvTab_CNAE_UF.Sort(dgvTab_CNAE_UF.Columns["ESTADO"], ListSortDirection.Ascending);


                clsGridHelper.FontGrid(dgvTab_CNAE_UF, 8);

                //agora verifica o geral para atualizar o local
                VerificaTab_CNAE_UFGeral();

                if (dgvTab_CNAE_UF.CurrentRow != null)
                {
                    indexTab_CNAE_UF = clsParser.Int32Parse(dgvTab_CNAE_UF.CurrentRow.Cells["ID"].Value.ToString());
                }
                else
                {
                    indexTab_CNAE_UF = 0;
                }

                if (frmTab_CNAE_Vis.indexTab_CNAE_UFGeral > 0)
                {
                    clsGridHelper.SelecionaLinha(frmTab_CNAE_Vis.indexTab_CNAE_UFGeral, dgvTab_CNAE_UF, "ESTADO");
                }
                else
                {
                    clsGridHelper.SelecionaLinha(indexTab_CNAE_UF, dgvTab_CNAE_UF, "ESTADO");
                }

                pbxTab_CNAE_UF.Visible = false;
                tbxCodigo.Select();
                tbxCodigo.SelectAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void VerificaTab_CNAE_UFGeral()
        {
            try
            {
                // Verificando 
                foreach (DataRow dgvrTab_CNAE_UFGeral in frmTab_CNAE_Vis.dtTab_CNAE_UFGeral.Rows)
                {
                    frmTab_CNAE_Vis.achouTab_CNAE_UFGeral = false;
                    if (dgvrTab_CNAE_UFGeral.RowState != DataRowState.Deleted)
                    {
                        foreach (DataRow dgvrwTab_CNAE_UF in dtTab_CNAE_UF.Rows)
                        {
                            if (dgvrwTab_CNAE_UF.RowState != DataRowState.Deleted)
                            {
                                if (clsParser.Int32Parse(dgvrTab_CNAE_UFGeral["ID"].ToString()) == clsParser.Int32Parse(dgvrwTab_CNAE_UF["ID"].ToString()) &&
                                    clsParser.Int32Parse(dgvrTab_CNAE_UFGeral["IDTABCNAE"].ToString()) == clsParser.Int32Parse(dgvrwTab_CNAE_UF["IDTABCNAE"].ToString()))
                                {
                                    //
                                    frmTab_CNAE_Vis.drTab_CNAE_UFGeral = dgvrwTab_CNAE_UF;
                                    //
                                    frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IDTABCNAE"] = dgvrTab_CNAE_UFGeral["IDTABCNAE"];
                                    frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IDUF"] = dgvrTab_CNAE_UFGeral["IDUF"];
                                    frmTab_CNAE_Vis.drTab_CNAE_UFGeral["ESTADO"] = dgvrTab_CNAE_UFGeral["ESTADO"];
                                    frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IVA"] = dgvrTab_CNAE_UFGeral["IVA"];   
                                    frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IDDIZERESNFV"] = dgvrTab_CNAE_UFGeral["IDDIZERESNFV"];
                                    frmTab_CNAE_Vis.drTab_CNAE_UFGeral["ICMSST"] = dgvrTab_CNAE_UFGeral["ICMSST"];
                                    frmTab_CNAE_Vis.drTab_CNAE_UFGeral["TEMP"] = dgvrTab_CNAE_UFGeral["TEMP"];

                                    frmTab_CNAE_Vis.achouTab_CNAE_UFGeral = true;
                                    break;
                                }
                            }
                        }
                        if (frmTab_CNAE_Vis.achouTab_CNAE_UFGeral == false)
                        {
                            dtTab_CNAE_UF.ImportRow(dgvrTab_CNAE_UFGeral);
                        }
                    }
                }
                //
                if (frmTab_CNAE_Vis.deleTab_CNAE_UFGeral.Count > 0)
                {
                    for (Int32 X = 0; X < frmTab_CNAE_Vis.deleTab_CNAE_UFGeral.Count; X++)
                    {
                        for (Int32 I = dtTab_CNAE_UF.Rows.Count - 1; I >= 0; I--)
                        {
                            if (clsParser.Int32Parse(dtTab_CNAE_UF.Rows[I]["ID"].ToString()) == clsParser.Int32Parse(frmTab_CNAE_Vis.deleTab_CNAE_UFGeral[X].ToString()))
                            {
                                dtTab_CNAE_UF.Rows[I].Delete();
                                dtTab_CNAE_UF.AcceptChanges();
                                break;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("VerificaTab_CNAE_UF - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void frmTab_CNAE_Activated(object sender, EventArgs e)
        {
            if (leaveNocheck == true)
            {
                bwrTab_CNAE_UF_RunWorkerAsync();
            }           
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (VerificaGravacaoTab_CNAE("TODOS") == true)
            {
                if (dgvTab_CNAE_UF.CurrentRow != null)
                {
                    if (dgvTab_CNAE_UF.Rows.Count > 0)
                    {
                        frmTab_CNAE_Vis.indexTab_CNAE_UFGeral = clsParser.Int32Parse(dgvTab_CNAE_UF.CurrentRow.Cells["ID"].Value.ToString());
                    }
                    frmTab_CNAE_UF frmTab_CNAEDados_UF = new frmTab_CNAE_UF();
                    frmTab_CNAEDados_UF.Init(clsParser.Int32Parse(dgvTab_CNAE_UF.CurrentRow.Cells["ID"].Value.ToString()),
                    idTab_CNAEX,clsParser.Int32Parse(dgvTab_CNAE_UF.CurrentRow.Cells["IDUF"].Value.ToString()), dgvTab_CNAE_UF);

                    //FormHelper.AbrirForm(this.MdiParent, frmTab_CNAEDados_UF);
                    frmTab_CNAEDados_UF.ShowDialog();
                }
                tbxCodigo.Select();
                tbxCodigo.SelectAll();
            }
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (VerificaGravacaoTab_CNAE("TODOS") == true)
            {
                if (dgvTab_CNAE_fil.CurrentRow != null)
                {
                    if (dgvTab_CNAE_UF.Rows.Count > 0)
                    {
                        frmTab_CNAE_Vis.indexTab_CNAE_UFGeral = clsParser.Int32Parse(dgvTab_CNAE_UF.CurrentRow.Cells["ID"].Value.ToString());
                    }
                }
                frmTab_CNAE_UF frmTab_CNAE_UF = new frmTab_CNAE_UF();
                frmTab_CNAE_UF.Init(0, idTab_CNAEX,0, dgvTab_CNAE_UF);

                //FormHelper.AbrirForm(this.MdiParent, frmTab_CNAE_UF);
                frmTab_CNAE_UF.ShowDialog();
                tbxCodigo.Select();
                tbxCodigo.SelectAll();
            }
        }

        private void dgvTab_CNAE_UF_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            if (dgvTab_CNAE_UF.CurrentRow != null)
            {
                if (MessageBox.Show("Deseja excluir o registro " + dgvTab_CNAE_UF.Columns[3].HeaderText + " " + dgvTab_CNAE_UF.CurrentRow.Cells["ESTADO"].Value + "?", "Aplisoft", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (dgvTab_CNAE_UF.CurrentRow.Cells["TEMP"].Value.ToString() != "S")
                    {
                        frmTab_CNAE_Vis.deleTab_CNAE_UFGeral.Add(clsParser.Int32Parse(dgvTab_CNAE_UF.CurrentRow.Cells["ID"].Value.ToString()));
                    }
                    //
                    for (Int32 I = frmTab_CNAE_Vis.dtTab_CNAE_UFGeral.Rows.Count - 1; I >= 0; I--)
                    {
                        if (frmTab_CNAE_Vis.dtTab_CNAE_UFGeral.Rows[I].RowState != DataRowState.Deleted)
                        {
                            if (clsParser.Int32Parse(frmTab_CNAE_Vis.dtTab_CNAE_UFGeral.Rows[I]["ID"].ToString()) == clsParser.Int32Parse(dgvTab_CNAE_UF.CurrentRow.Cells["ID"].Value.ToString()))
                            {
                                frmTab_CNAE_Vis.dtTab_CNAE_UFGeral.Rows[I].Delete();
                                break;
                            }
                        }
                    }
                    clsGridHelper.ProximaLinha(indexTab_CNAE_UF, dgvTab_CNAE_UF);
                    if (dgvTab_CNAE_UF.RowCount > 0)
                    {
                        indexTab_CNAE_UF = clsParser.Int32Parse(dgvTab_CNAE_UF.CurrentRow.Cells["ID"].Value.ToString());
                    }
                    else
                    {
                        indexTab_CNAE_UF = 0;
                    }
                    leaveNocheck = true;
                    frmTab_CNAE_Activated(sender, e);
                }
            }
        }

       
    }
}
