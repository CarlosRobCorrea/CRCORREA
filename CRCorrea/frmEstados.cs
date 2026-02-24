using CRCorreaBLL;
using CRCorreaFuncoes;

using System;
using System.ComponentModel;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{ 
    public partial class frmEstados : Form
    {
        private Boolean ok;
        //
        private DataGridView dgvESTADOS_fil;
        private DataGridView dgvESTADOS_compl;

        private String[] valoresantigos = new String[] { };
        private String[] valoresatuais = new String[] { };
        
        private delegate void AddnewDelegate(TextBox textbox, TextBox colecao);

        private Int32 indexESTADOSICMS;
        private Int32 idESTADOSICMS;
        private DataTable dtESTADOSICMS;

        private DialogResult resultado = new DialogResult();

        private Int32 idESTADOSX;

        private Boolean leaveNocheck;

        public frmEstados()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridView _dgvESTADOS_fil,
                         DataGridView _dgvESTADOS_compl)
        {
            frmEstadosVis.indexESTADOSGeral = 0;

            frmEstadosVis.dtESTADOSICMSGeral = null;
            frmEstadosVis.deleESTADOSICMSGeral.Clear();
            frmEstadosVis.drESTADOSICMSGeral = null;
            frmEstadosVis.indexESTADOSICMSGeral = 0;
            frmEstadosVis.achouESTADOSICMSGeral = false;

            leaveNocheck = false;
            idESTADOSX = _id;
            dgvESTADOS_fil = _dgvESTADOS_fil;
            dgvESTADOS_compl = _dgvESTADOS_compl;

        }

        private void CarregaESTADOS(Int32 _id)
        {
            try
            {
                //Carrega os Dados 
                tspESTADOS.Cursor = Cursors.Hand;
                PreencheCamposESTADOS();
                valoresantigos = PreencheValoresESTADOS();
                if (_id == 0)
                {                    
                    tspPrimeiro.Enabled = false;
                    tspAnterior.Enabled = false;
                    tspProximo.Enabled = false;
                    tspUltimo.Enabled = false;                    
                }
                tbxEstado.Select();
                tbxEstado.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Carrega ESTADOS - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                leaveNocheck = true;
            }
        }

        private void PreencheCamposESTADOS()
        {
            if (idESTADOSX > 0)
            {
                foreach (DataRow dgvrESTADOS in ((DataTable)dgvESTADOS_compl.DataSource).Rows)
                {
                    if (dgvrESTADOS.RowState != DataRowState.Deleted)
                    {
                        if (clsParser.Int32Parse(dgvrESTADOS["ID"].ToString()) == idESTADOSX)
                        {
                            tbxEstado.Text = dgvrESTADOS["ESTADO"].ToString();
                            tbxNomext.Text = dgvrESTADOS["NOMEEXT"].ToString();
                            tbxRegiao.Text = dgvrESTADOS["ZONAFRANCA"].ToString();
                            tbxAliquota.Text = dgvrESTADOS["ALIQUOTA"].ToString();
                            tbxCapital.Text = dgvrESTADOS["CAPITAL"].ToString();
                            tbxInicep.Text = dgvrESTADOS["INICEP"].ToString();
                            tbxFimcep.Text = dgvrESTADOS["FIMCEP"].ToString();
                            tbxRegiao.Text = dgvrESTADOS["REGIAO"].ToString();
                            tbxIbge.Text = dgvrESTADOS["IBGE"].ToString();
                            cbxZonaFranca.Text = dgvrESTADOS["ZONAFRANCA"].ToString();
                            tbxIEST.Text = dgvrESTADOS["IEST"].ToString();
                        }
                    }
                }
            }
            else
            {
                cbxZonaFranca.Text = "N";
            }
        }

        private String[] PreencheValoresESTADOS()
        {
            String[] _valores = { tbxEstado.Text,
                                tbxNomext.Text, 
                                tbxRegiao.Text,
                                tbxAliquota.Text,
                                tbxCapital.Text,
                                tbxInicep.Text,
                                tbxFimcep.Text,
                                tbxRegiao.Text,
                                tbxIbge.Text,
                                cbxZonaFranca.Text,
                                tbxIEST.Text};
            return _valores;
        }     
       

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                resultado = DialogResult.No;
                valoresatuais = PreencheValoresESTADOS();
                if (clsSelectInsertUpdateBLL.ComparaValores(valoresatuais, valoresantigos) == false)
                {
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

                    if (resultado == DialogResult.Yes)
                    {
                        using (TransactionScope tsESTADOS = new TransactionScope())
                        {
                            SalvarESTADOS();
                            tsESTADOS.Complete();
                            tsESTADOS.Dispose();
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
                    if (idESTADOSX > 0)
                    {
                        frmEstadosVis.indexESTADOSGeral = idESTADOSX;
                    }
                    leaveNocheck = false;
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
                    using (TransactionScope tsESTADOS = new TransactionScope())
                    {
                        SalvarESTADOS();
                        tsESTADOS.Complete();
                        tsESTADOS.Dispose();
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
                    if (idESTADOSX > 0)
                    {
                        frmEstadosVis.indexESTADOSGeral = idESTADOSX; ;
                    }
                    leaveNocheck =false;
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void SalvarESTADOS()
        {
            if (VerificaGravacaoESTADOS("TODOS") == true)
            {
                if (idESTADOSX == 0)    // Incluindo os valores do Cabeçalho -- valores locais
                {
                    idESTADOSX = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqldados, "ESTADOS",
                                    "ESTADO," +
                                    "ZONAFRANCA," +
                                    "ALIQUOTA," +
                                    "NOMEEXT," +
                                    "CAPITAL," +
                                    "INICEP," +
                                    "FIMCEP," +
                                    "REGIAO," +
                                    "IBGE," +
                                    "IEST",
                                    clsParser.SqlStringFormat(tbxEstado.Text, false, "ESTADO") +
                                    clsParser.SqlStringFormat(cbxZonaFranca.Text, false, "ZONAFRANCA") +
                                    clsParser.SqlDecimalFormat(tbxAliquota.Text, false, "ALIQUOTA") +
                                    clsParser.SqlStringFormat(tbxNomext.Text, false, "NOMEEXT") +
                                    clsParser.SqlStringFormat(tbxCapital.Text, false, "CAPITAL") +
                                    clsParser.SqlStringFormat(tbxInicep.Text, false, "INICEP") +
                                    clsParser.SqlStringFormat(tbxFimcep.Text, false, "FIMCEP") +
                                    clsParser.SqlStringFormat(tbxRegiao.Text, false, "REGIAO") +
                                    clsParser.SqlStringFormat(tbxIbge.Text, false, "IBGE") +
                                    clsParser.SqlStringFormat(tbxIEST.Text,true, "IEST"));
                }
                else
                {
                    //Alterando os valores do Cabeçalho -- valores locais
                    clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "ESTADOS",
                                                            "ESTADO = " + clsParser.SqlStringFormat(tbxEstado.Text, false, "ESTADO") +
                                                            "ZONAFRANCA = " + clsParser.SqlStringFormat(cbxZonaFranca.Text, false, "ZONAFRANCA") +
                                                            "ALIQUOTA = " + clsParser.SqlDecimalFormat(tbxAliquota.Text, false, "ALIQUOTA") +
                                                            "NOMEEXT = " + clsParser.SqlStringFormat(tbxNomext.Text, false, "NOMEEXT") +
                                                            "CAPITAL = " + clsParser.SqlStringFormat(tbxCapital.Text, false, "CAPITAL") +
                                                            "INICEP = " + clsParser.SqlStringFormat(tbxInicep.Text, false, "INICEP") +
                                                            "FIMCEP = " + clsParser.SqlStringFormat(tbxFimcep.Text, false, "FIMCEP") +
                                                            "REGIAO = " + clsParser.SqlStringFormat(tbxRegiao.Text, false, "REGIAO") +
                                                            "IBGE = " + clsParser.SqlStringFormat(tbxIbge.Text, false, "IBGE")+
                                                            "IEST = " + clsParser.SqlStringFormat(tbxIEST.Text, true, "IEST"),
                                                            "ID = " + idESTADOSX.ToString());
                }

            //logo devemos atualizar o grid de movimentaçao
            dgvESTADOS_compl.DataSource = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
               "ESTADOS ",
               "ID,ESTADO,ZONAFRANCA,ALIQUOTA,NOMEEXT,CAPITAL,INICEP,FIMCEP,REGIAO,IBGE,IEST ",
               "", "ID");
            //agora inclui ou altera os DataTable gerais temporarios

            if (frmEstadosVis.dtESTADOSICMSGeral.Rows.Count > 0)
            {
                foreach (DataRow dgvrESTADOSICMS in frmEstadosVis.dtESTADOSICMSGeral.Rows)
                {
                    if (dgvrESTADOSICMS.RowState == DataRowState.Added)
                    {
                        idESTADOSICMS = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqldados, "ESTADOSICMS",
                                                        "IDESTADO," +
                                                        "IDESTADODESTINO," +
                                                        "ALIQUOTA," +
                                                        "IVA," +
                                                        "IDDIZERESNFV",
                        clsParser.SqlInt32Format(idESTADOSX, false, "IDESTADO") +
                        clsParser.SqlInt32Format(dgvrESTADOSICMS["IDESTADODESTINO"].ToString(), false, "IDESTADODESTINO") +
                        clsParser.SqlDecimalFormat(dgvrESTADOSICMS["ALIQUOTA"].ToString(), false, "ALIQUOTA") +
                        clsParser.SqlDecimalFormat(dgvrESTADOSICMS["IVA"].ToString(), false, "IVA") +
                        clsParser.SqlInt32Format(dgvrESTADOSICMS["IDDIZERESNFV"].ToString(), true, "IDDIZERESNFV"));
                    }
                    else if (dgvrESTADOSICMS.RowState == DataRowState.Modified)
                    {
                        idESTADOSICMS = clsParser.Int32Parse(dgvrESTADOSICMS["ID"].ToString());

                        clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "ESTADOSICMS",
                        "IDESTADO = " + clsParser.SqlInt32Format(dgvrESTADOSICMS["IDESTADO"].ToString(), false, "IDESTADO") +
                        "IDESTADODESTINO = " + clsParser.SqlInt32Format(dgvrESTADOSICMS["IDESTADODESTINO"].ToString(), false, "IDESTADODESTINO") +
                        "ALIQUOTA = " + clsParser.SqlDecimalFormat(dgvrESTADOSICMS["ALIQUOTA"].ToString(), false, "ALIQUOTA") +
                        "IVA= " + clsParser.SqlDecimalFormat(dgvrESTADOSICMS["IVA"].ToString(), false, "IVA") +
                        "IDDIZERESNFV = " +  clsParser.SqlInt32Format(dgvrESTADOSICMS["IDDIZERESNFV"].ToString(),true,"IDDIZERESNFV"),
                         "ID = " + dgvrESTADOSICMS["ID"].ToString());
                    }
                }
            }
                //deletando
                for (Int32 X = 0; X < frmEstadosVis.deleESTADOSICMSGeral.Count; X++)
                {
                    //agora a principal
                    clsSelectInsertUpdateBLL.Delete(clsInfo.conexaosqldados, "ESTADOSICMS", "ID = " + frmEstadosVis.deleESTADOSICMSGeral[X].ToString());
                }
            }
        }      

        private Boolean VerificaGravacaoESTADOS(String _tipo)
        {
            try
            {
                // Verficando os campos
                if (_tipo == "TODOS")
                {
                    if (tbxEstado.Text.Trim() == "")
                    {
                        throw new Exception("Informe o Estado");
                    }       

                    if(tbxNomext.Text.Trim() == "")
                    {
                        throw new Exception("Informe Nome Ext.");
                    }

                    if(tbxCapital.Text.Trim() == "")
                    {
                        throw new Exception("Informe Nome Capital");
                    }

                    if(tbxRegiao.Text.Trim() == "")
                    {
                        throw new Exception("Informe Nome Região");
                    }

                    if( tbxIbge.Text.Trim() == "")
                    {
                        throw new Exception("Informe Nome IBGE");
                    }

                    if(  tbxAliquota.Text.Trim() == "")
                    {
                        throw new Exception("Informe Nome Aliquota");
                    }    

                    if(  tbxInicep.Text.Trim() == "")
                    {
                        throw new Exception("Informe CEP ini");
                    }   

                    if(   tbxFimcep.Text.Trim() == "")
                    {
                        throw new Exception("Informe CEP fim");
                    }

                    if (String.IsNullOrEmpty(tbxIEST.Text))
                    {
                        throw new Exception("Informe o numero da inscrição estadual situação tributária");
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = DialogResult.Cancel; // Retorna para o foco
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                switch (ex.Message)
                {
                    case "Informe o Estado":
                        tbxEstado.Select();
                        tbxEstado.SelectAll();
                        break;

                    case "Informe Nome Capital":
                        tbxCapital.Select();
                        tbxCapital.SelectAll();
                        break;

                    case "Informe Nome Região":
                        tbxRegiao.Select();
                        tbxRegiao.SelectAll();
                        break;

                    case "Informe Nome IBGE":
                        tbxIbge.Select();
                        tbxIbge.SelectAll();
                        break;

                    case "Informe Nome Aliquota":
                        tbxAliquota.Select();
                        tbxAliquota.SelectAll();
                        break;    

                    case "Informe CEP ini":
                        tbxInicep.Select();
                        tbxInicep.SelectAll();
                        break;
                   
                    case "Informe CEP fim":
                        tbxFimcep.Select();
                        tbxFimcep.SelectAll();
                        break;            

                    case "Informe o numero da inscrição estadual situação tributária":
                        tbxIEST.Select();
                        tbxIEST.SelectAll();
                        break;

                    default:
                        break;
                }
                return false;
            }
            return true;
        }

        private Boolean MovimentaESTADOS()
        {
            try
            {
                ok = false;
                valoresatuais = PreencheValoresESTADOS();
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
                            SalvarESTADOS();
                        }
                    }
                    else if (resultado == DialogResult.No)
                    {
                        if (VerificaGravacaoESTADOS("TODOS") == false)
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
                    if (VerificaGravacaoESTADOS("TODOS") == false)
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
                MessageBox.Show("MovimentaESTADOS - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            frmEstadosVis.dtESTADOSICMSGeral = null;
            frmEstadosVis.deleESTADOSICMSGeral.Clear();
            frmEstadosVis.drESTADOSICMSGeral = null;
            frmEstadosVis.indexESTADOSICMSGeral = 0;
            frmEstadosVis.achouESTADOSICMSGeral = false;
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                resultado = DialogResult.No;
                if (MovimentaESTADOS())
                {
                    idESTADOSX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvESTADOS_fil.Rows)[0].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                     dgvESTADOS_fil[1, 0].Selected = true;
                    LimpaResiduos();
                    CarregaESTADOS(idESTADOSX);
                    leaveNocheck = true;
                    frmEstados_Activated(sender, e);
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
                if (MovimentaESTADOS())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection) dgvESTADOS_fil.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idESTADOSX)
                        {
                            if (dgvr.Index != 0)
                            {
                                idESTADOSX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvESTADOS_fil.Rows)[((DataGridViewRowCollection) dgvESTADOS_fil.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente                                
                                 dgvESTADOS_fil[1, ((DataGridViewRowCollection) dgvESTADOS_fil.Rows).
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
                    CarregaESTADOS(idESTADOSX);
                    leaveNocheck = true;
                    frmEstados_Activated(sender, e);

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
                if (MovimentaESTADOS())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection) dgvESTADOS_fil.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idESTADOSX)
                        {
                            if (dgvr.Index < ((DataGridViewRowCollection) dgvESTADOS_fil.Rows).Count - 1)
                            {
                                idESTADOSX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvESTADOS_fil.Rows)[((DataGridViewRowCollection) dgvESTADOS_fil.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente
                                 dgvESTADOS_fil[1, ((DataGridViewRowCollection) dgvESTADOS_fil.Rows).
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
                    CarregaESTADOS(idESTADOSX);
                    leaveNocheck = true;
                    frmEstados_Activated(sender, e);
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
                if (MovimentaESTADOS())
                {
                    idESTADOSX = clsParser.Int32Parse(((DataGridViewRowCollection) dgvESTADOS_fil.Rows)[((DataGridViewRowCollection) dgvESTADOS_fil.Rows).Count - 1].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                     dgvESTADOS_fil[1, ((DataGridViewRowCollection) dgvESTADOS_fil.Rows).Count - 1].Selected = true;
                    LimpaResiduos();
                    CarregaESTADOS(idESTADOSX);
                    leaveNocheck = true;
                    frmEstados_Activated(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        ////
        ////
        //começa a partir daqui o controle sobre a tabela ESTADOSICMS
        private void bwrESTADOSICMS_RunWorkerAsync()
        {
            pbxESTADOSICMS.Visible = true;
            bwrESTADOSICMS = new BackgroundWorker();
            bwrESTADOSICMS.DoWork += new DoWorkEventHandler(bwrESTADOSICMS_DoWork);
            bwrESTADOSICMS.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrESTADOSICMS_RunWorkerCompleted);
            bwrESTADOSICMS.RunWorkerAsync();
        }

        private void bwrESTADOSICMS_DoWork(object sender, DoWorkEventArgs e)
        {
            dtESTADOSICMS = CarregaGridESTADOSICMS();
            dtESTADOSICMS.Columns.Add("TEMP", Type.GetType("System.String"));

            //
            if (frmEstadosVis.dtESTADOSICMSGeral == null || frmEstadosVis.dtESTADOSICMSGeral.Rows.Count == 0)
            {
                frmEstadosVis.dtESTADOSICMSGeral = dtESTADOSICMS.Clone();
            }
        }

        public DataTable CarregaGridESTADOSICMS()
        {
            try
            {

                return clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                       "ESTADOSICMS ",
                       "ID,IDESTADO,IDESTADODESTINO,(SELECT ESTADO FROM ESTADOS WHERE ID = ESTADOSICMS.IDESTADODESTINO) AS UF,ALIQUOTA,IVA,IDDIZERESNFV",
                       "IDESTADO = " + idESTADOSX, "ID");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrESTADOSICMS_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvESTADOSICMS.DataSource = dtESTADOSICMS;
                dgvESTADOSICMS.Sort(dgvESTADOSICMS.Columns["UF"], ListSortDirection.Ascending);


                clsGridHelper.FontGrid(dgvESTADOSICMS, 8);

                //agora verifica o geral para atualizar o local
                VerificaESTADOSICMSGeral();

                if (dgvESTADOSICMS.CurrentRow != null)
                {
                    indexESTADOSICMS = clsParser.Int32Parse(dgvESTADOSICMS.CurrentRow.Cells["ID"].Value.ToString());
                }
                else
                {
                    indexESTADOSICMS = 0;
                }

                if (frmEstadosVis.indexESTADOSICMSGeral > 0)
                {
                    clsGridHelper.SelecionaLinha(frmEstadosVis.indexESTADOSICMSGeral, dgvESTADOSICMS, "UF");
                }
                else
                {
                    clsGridHelper.SelecionaLinha(indexESTADOSICMS, dgvESTADOSICMS, "UF");
                }

                pbxESTADOSICMS.Visible = false;
                tbxEstado.Select();
                tbxEstado.SelectAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void VerificaESTADOSICMSGeral()
        {
            try
            {
                // Verificando 
                foreach (DataRow dgvrESTADOSICMSGeral in frmEstadosVis.dtESTADOSICMSGeral.Rows)
                {
                    frmEstadosVis.achouESTADOSICMSGeral = false;
                    if (dgvrESTADOSICMSGeral.RowState != DataRowState.Deleted)
                    {
                        foreach (DataRow dgvrwESTADOSICMS in dtESTADOSICMS.Rows)
                        {
                            if (dgvrwESTADOSICMS.RowState != DataRowState.Deleted)
                            {
                                if (clsParser.Int32Parse(dgvrESTADOSICMSGeral["ID"].ToString()) == clsParser.Int32Parse(dgvrwESTADOSICMS["ID"].ToString()) &&
                                    clsParser.Int32Parse(dgvrESTADOSICMSGeral["IDESTADO"].ToString()) == clsParser.Int32Parse(dgvrwESTADOSICMS["IDESTADO"].ToString()))
                                {
                                    //
                                    frmEstadosVis.drESTADOSICMSGeral = dgvrwESTADOSICMS;
                                    //
                                    frmEstadosVis.drESTADOSICMSGeral["IDESTADO"] = dgvrESTADOSICMSGeral["IDESTADO"];
                                    frmEstadosVis.drESTADOSICMSGeral["UF"] = dgvrESTADOSICMSGeral["UF"];
                                    frmEstadosVis.drESTADOSICMSGeral["IDESTADODESTINO"] = dgvrESTADOSICMSGeral["IDESTADODESTINO"];
                                    frmEstadosVis.drESTADOSICMSGeral["ALIQUOTA"] = dgvrESTADOSICMSGeral["ALIQUOTA"];
                                    frmEstadosVis.drESTADOSICMSGeral["IVA"] = dgvrESTADOSICMSGeral["IVA"];
                                    frmEstadosVis.drESTADOSICMSGeral["IDDIZERESNFV"] = dgvrESTADOSICMSGeral["IDDIZERESNFV"];
                                    frmEstadosVis.drESTADOSICMSGeral["TEMP"] = dgvrESTADOSICMSGeral["TEMP"];
                                       

                                    frmEstadosVis.achouESTADOSICMSGeral = true;
                                    break;
                                }
                            }
                        }
                        if (frmEstadosVis.achouESTADOSICMSGeral == false)
                        {
                            dtESTADOSICMS.ImportRow(dgvrESTADOSICMSGeral);
                        }

                    }
                }
                //
                if (frmEstadosVis.deleESTADOSICMSGeral.Count > 0)
                {
                    for (Int32 X = 0; X < frmEstadosVis.deleESTADOSICMSGeral.Count; X++)
                    {
                        for (Int32 I = dtESTADOSICMS.Rows.Count - 1; I >= 0; I--)
                        {
                            if (clsParser.Int32Parse(dtESTADOSICMS.Rows[I]["ID"].ToString()) == clsParser.Int32Parse(frmEstadosVis.deleESTADOSICMSGeral[X].ToString()))
                            {
                                dtESTADOSICMS.Rows[I].Delete();
                                dtESTADOSICMS.AcceptChanges();
                                break;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("VerificaESTADOSICMS - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (VerificaGravacaoESTADOS("TODOS") == true)
            {
                if (dgvESTADOSICMS.CurrentRow != null)
                {
                    if (dgvESTADOSICMS.Rows.Count > 0)
                    {
                        frmEstadosVis.indexESTADOSICMSGeral = clsParser.Int32Parse(dgvESTADOSICMS.CurrentRow.Cells["ID"].Value.ToString());
                    }
                    frmEstadosICMS frmEstadosICMS = new frmEstadosICMS();
                    frmEstadosICMS.Init(clsParser.Int32Parse(dgvESTADOSICMS.CurrentRow.Cells["ID"].Value.ToString()),
                    idESTADOSX, dgvESTADOSICMS);

                    frmEstadosICMS.ShowDialog();
                }
                tbxEstado.Select();
                tbxEstado.SelectAll();
            }
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (VerificaGravacaoESTADOS("TODOS") == true)
            {
                if (dgvESTADOS_fil.CurrentRow != null)
                {
                    if (dgvESTADOSICMS.Rows.Count > 0)
                    {
                        frmEstadosVis.indexESTADOSICMSGeral = clsParser.Int32Parse(dgvESTADOSICMS.CurrentRow.Cells["ID"].Value.ToString());
                    }
                }
                frmEstadosICMS frmEstadosICMS = new frmEstadosICMS();
                frmEstadosICMS.Init(0, idESTADOSX, dgvESTADOSICMS);

                frmEstadosICMS.ShowDialog();
                tbxEstado.Select();
                tbxEstado.SelectAll();
            }
        }

        private void dgvESTADOSICMS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            if (dgvESTADOSICMS.CurrentRow != null)
            {
                if (MessageBox.Show("Deseja excluir o registro " + dgvESTADOSICMS.Columns[3].HeaderText + " " + dgvESTADOSICMS.CurrentRow.Cells["UF"].Value + "?", "Aplisoft", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (dgvESTADOSICMS.CurrentRow.Cells["TEMP"].Value.ToString() != "S")
                    {
                        frmEstadosVis.deleESTADOSICMSGeral.Add(clsParser.Int32Parse(dgvESTADOSICMS.CurrentRow.Cells["ID"].Value.ToString()));
                    }
                    //
                    for (Int32 I = frmEstadosVis.dtESTADOSICMSGeral.Rows.Count - 1; I >= 0; I--)
                    {
                        if (frmEstadosVis.dtESTADOSICMSGeral.Rows[I].RowState != DataRowState.Deleted)
                        {
                            if (clsParser.Int32Parse(frmEstadosVis.dtESTADOSICMSGeral.Rows[I]["ID"].ToString()) == clsParser.Int32Parse(dgvESTADOSICMS.CurrentRow.Cells["ID"].Value.ToString()))
                            {
                                frmEstadosVis.dtESTADOSICMSGeral.Rows[I].Delete();
                                break;
                            }
                        }
                    }
                    clsGridHelper.ProximaLinha(indexESTADOSICMS, dgvESTADOSICMS);
                    if (dgvESTADOSICMS.RowCount > 0)
                    {
                        indexESTADOSICMS = clsParser.Int32Parse(dgvESTADOSICMS.CurrentRow.Cells["ID"].Value.ToString());
                    }
                    else
                    {
                        indexESTADOSICMS = 0;
                    }
                    leaveNocheck = true;
                    frmEstados_Activated(sender, e);
                }
            }
        }


        private void frmEstados_Load(object sender, EventArgs e)
        {
            CarregaESTADOS(idESTADOSX);
        }

        private void frmEstados_Shown(object sender, EventArgs e)
        {
            
        }

        private void ControlKeyKeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisual.ControlKeyPressNumero((TextBox)sender, e, false);
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            if (leaveNocheck == false)
            {
                clsVisual.FormatarCampoNumerico(sender);  
                clsVisual.ControlLeave(sender);
            }
            else
            {
                leaveNocheck = false;
                clsVisual.FormatarCampoNumerico(sender);  
                clsVisual.ControlLeave(sender);
            }
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void frmEstados_Activated(object sender, EventArgs e)
        {
            if (leaveNocheck == true)
            {
                bwrESTADOSICMS_RunWorkerAsync();
            }
        }

        private void tbxNomext_TextChanged(object sender, EventArgs e)
        {

        }

        private void tspESTADOS_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
