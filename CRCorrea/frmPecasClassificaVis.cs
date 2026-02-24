using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmPecasClassificaVis : Form
    {
        
        
        
        

        // Grupo
        Int32 id = 0;    // Grupo
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;

        Boolean carregandoGrupo;

        clsPecasclassificaInfo clsPecasclassificaInfo = new clsPecasclassificaInfo();
        clsPecasclassificaInfo clsPecasclassificaInfoOld = new clsPecasclassificaInfo();
        clsPecasclassificaBLL clsPecasclassificaBLL = new clsPecasclassificaBLL();

        DataTable dtGrupo;
        // Sub-Grupo
        Int32 idSubGrupo = 0;    // Grupo
        Int32 id_AnteriorSubGrupo = 0;
        Int32 id_ProximoSubGrupo = 0;

        Boolean carregandoSubGrupo;

        clsPecasclassifica1Info clsPecasclassifica1Info = new clsPecasclassifica1Info();
        clsPecasclassifica1Info clsPecasclassifica1InfoOld = new clsPecasclassifica1Info();
        clsPecasclassifica1BLL clsPecasclassifica1BLL = new clsPecasclassifica1BLL();

        DataTable dtSubGrupo;

        // Item Sub-Grupo
        Int32 idItemSubGrupo = 0;    // Grupo
        Int32 id_AnteriorItemSubGrupo = 0;
        Int32 id_ProximoItemSubGrupo = 0;

        Boolean carregandoItemSubGrupo;

        clsPecasclassifica2Info clsPecasclassifica2Info = new clsPecasclassifica2Info();
        clsPecasclassifica2Info clsPecasclassifica2InfoOld = new clsPecasclassifica2Info();
        clsPecasclassifica2BLL clsPecasclassifica2BLL = new clsPecasclassifica2BLL();

        DataTable dtItemSubGrupo;

        public frmPecasClassificaVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            clsPecasclassificaBLL = new clsPecasclassificaBLL();
            clsPecasclassifica1BLL = new clsPecasclassifica1BLL();
            clsPecasclassifica2BLL = new clsPecasclassifica2BLL();
        }

        private void frmPecasClassificaVis_Load(object sender, EventArgs e)
        {
            if (bwrGrupo.IsBusy != true)
            {
                bwrGrupo_Run();
            }
            dgvGrupo.Select();
            dgvGrupo.SelectAll();
        }

        private void frmPecasClassificaVis_Activated(object sender, EventArgs e)
        {
            bwrGrupo_Run();
        }

        private void tspGrupoIncluir_Click(object sender, EventArgs e)
        {
            id = 0;
            id_Proximo = 0;
            id_Anterior = 0;
            gbxRegistrando.Visible = true;
            gbxRegistrando.Enabled = true;
            gbxRegistrandoSubGrupo.Visible = false;
            gbxRegistrandoItemSubGrupo.Visible = false;

            PecasClassificaCarregar();

            tclPecasClassifica.SelectedIndex = 1;
            PecasClassificaCarregar();
        }

        private void tspGrupoAlterar_Click(object sender, EventArgs e)
        {
            if (dgvGrupo.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvGrupo.CurrentRow.Cells["ID"].Value.ToString());

                if (dgvGrupo.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvGrupo.Rows[dgvGrupo.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }


                if (dgvGrupo.CurrentRow.Index < dgvGrupo.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvGrupo.Rows[dgvGrupo.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }
                gbxRegistrando.Visible = true;
                gbxRegistrando.Enabled = true;

                gbxRegistrandoSubGrupo.Visible = false;
                gbxRegistrandoItemSubGrupo.Visible = false;

                tclPecasClassifica.SelectedIndex = 1;
                PecasClassificaCarregar();
            }
        }

        private void tspGrupoExcluir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desabilitado");
        }

        private void bwrGrupo_Run()
        {
            if (carregandoGrupo == false)
            {
                carregandoGrupo = true;
                //instanciamos a bwrAbertas e adicionamos os eventos 
                bwrGrupo = new BackgroundWorker();
                bwrGrupo.DoWork += new DoWorkEventHandler(bwrGrupo_DoWork);
                bwrGrupo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrGrupo_RunWorkerCompleted);
                //apenas para permitir a funcionalidade de cancelamento
                bwrGrupo.WorkerSupportsCancellation = true;

                bwrGrupo.RunWorkerAsync();
            }
        }

        private void bwrGrupo_DoWork(object sender, DoWorkEventArgs e)
        {
            dtGrupo = clsPecasclassificaBLL.GridDados(clsInfo.conexaosqldados);
        }

        private void bwrGrupo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //se e.Cancel do bwrAbertas_DoWork recebeu true então foi cancelado a execução do BackgroundWorker
                if (e.Cancelled != true)
                {
                    clsPecasclassificaBLL.GridMonta(dgvGrupo, dtGrupo, 0);
                    lblTotalGrupos.Text = "Total Grupos:   " + dgvGrupo.Rows.Count.ToString();
                    PesquisaRapida();

                    //bloqueando a ordenação
                    foreach (DataGridViewColumn coluna in dgvGrupo.Columns)
                    {
                        coluna.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    // Carregando os Sub-Grupos do Grupo
                    if (id > 0)
                    {
                        if (dgvGrupo.CurrentRow != null)
                        {
                            gbxGrupo.Text = " Grupo " + dgvGrupo.CurrentRow.Cells["CODIGO"].Value.ToString() +
                                            " - " + dgvGrupo.CurrentRow.Cells["NOME"].Value.ToString();
                        }

                        if (bwrSubGrupo.IsBusy != true)
                        {
                            bwrSubGrupo_Run();
                        }

                    }

                }
                carregandoGrupo = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoGrupo = false;
            }           
        }

        // Sub-Grupo
        private void bwrSubGrupo_Run()
        {
            if (carregandoSubGrupo == false)
            {
                carregandoSubGrupo = true;
                //instanciamos a bwrAbertas e adicionamos os eventos 
                bwrSubGrupo = new BackgroundWorker();
                bwrSubGrupo.DoWork += new DoWorkEventHandler(bwrSubGrupo_DoWork);
                bwrSubGrupo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrSubGrupo_RunWorkerCompleted);
                //apenas para permitir a funcionalidade de cancelamento
                bwrSubGrupo.WorkerSupportsCancellation = true;

                bwrSubGrupo.RunWorkerAsync();
            }
        }

        private void bwrSubGrupo_DoWork(object sender, DoWorkEventArgs e)
        {
            dtSubGrupo = clsPecasclassifica1BLL.GridDados(id,clsInfo.conexaosqldados);
        }

        private void bwrSubGrupo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //se e.Cancel do bwrAbertas_DoWork recebeu true então foi cancelado a execução do BackgroundWorker
                if (e.Cancelled != true)
                {
                    clsPecasclassifica1BLL.GridMonta(dgvSubGrupo, dtSubGrupo, 0);
                    lblTotalSubGrupos.Text = "Total Sub-Grupos:   " + dgvSubGrupo.Rows.Count.ToString();
                    PesquisaRapidaSub();


                    // Carregando os Itens do Sub-Grupos do Grupo
                    if (idSubGrupo > 0)
                    {
                        if (dgvSubGrupo.CurrentRow != null)
                        {
                            gbxSubGrupo.Text = " Sub-Grupo " + dgvSubGrupo.CurrentRow.Cells["CODIGO"].Value.ToString() +
                                            " - " + dgvSubGrupo.CurrentRow.Cells["NOME"].Value.ToString();
                        }

                        if (bwrItemSubGrupo.IsBusy != true)
                        {
                            bwrItemSubGrupo_Run();
                        }
                    }




                }
                carregandoSubGrupo = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoSubGrupo = false;
            }
        }

        // Item Sub-Grupo
        private void bwrItemSubGrupo_Run()
        {
            if (carregandoItemSubGrupo == false)
            {
                carregandoItemSubGrupo = true;
                //instanciamos a bwrAbertas e adicionamos os eventos 
                bwrItemSubGrupo = new BackgroundWorker();
                bwrItemSubGrupo.DoWork += new DoWorkEventHandler(bwrItemSubGrupo_DoWork);
                bwrItemSubGrupo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrItemSubGrupo_RunWorkerCompleted);
                //apenas para permitir a funcionalidade de cancelamento
                bwrItemSubGrupo.WorkerSupportsCancellation = true;

                bwrItemSubGrupo.RunWorkerAsync();
            }
        }

        private void bwrItemSubGrupo_DoWork(object sender, DoWorkEventArgs e)
        {
            dtItemSubGrupo = clsPecasclassifica2BLL.GridDados(id, idSubGrupo, clsInfo.conexaosqldados);
        }

        private void bwrItemSubGrupo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //se e.Cancel do bwrAbertas_DoWork recebeu true então foi cancelado a execução do BackgroundWorker
                if (e.Cancelled != true)
                {
                    clsPecasclassifica2BLL.GridMonta(dgvItemSubGrupo, dtItemSubGrupo, 0);
                    lblTotalItemSubGrupos.Text = "Total Itens Sub-Grupos:   " + dgvItemSubGrupo.Rows.Count.ToString();
                    PesquisaRapidaItemSub();

                    if (dgvItemSubGrupo.CurrentRow != null)
                    {
                        gbxItemSubGrupo.Text = " Sub-Grupo " + dgvItemSubGrupo.CurrentRow.Cells["CODIGO"].Value.ToString() +
                                        " - " + dgvItemSubGrupo.CurrentRow.Cells["NOME"].Value.ToString();

                    }

                }
                carregandoItemSubGrupo = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoItemSubGrupo = false;
            }
        }

        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvGrupo);


            if (id > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvGrupo, "codigo") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo,
                                   dgvGrupo, "codigo") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior,
                                dgvGrupo, "codigo") == false)
                        {
                            if (dgvGrupo.Rows.Count > 0)
                            {
                                dgvGrupo.CurrentCell = dgvGrupo.Rows[0].Cells["codigo"];
                            }
                        }
                    }
                }
            }
            else if (dgvGrupo.Rows.Count > 0)
            {
                dgvGrupo.CurrentCell = dgvGrupo.Rows[0].Cells["codigo"];
            }

            if (dgvGrupo.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvGrupo.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvGrupo.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvGrupo.Rows[dgvGrupo.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvGrupo.CurrentRow.Index < dgvGrupo.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvGrupo.Rows[dgvGrupo.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }
            }
            else
            {
                id = 0;
                id_Anterior = 0;
                id_Proximo = 0;
            }
        }

        private void PesquisaRapidaSub()
        {
            clsGridHelper.Filtrar(tstbxLocalizarSubGrupo.Text, dgvSubGrupo);


            if (idSubGrupo > 0 || id_AnteriorSubGrupo > 0 || id_ProximoSubGrupo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(idSubGrupo, dgvSubGrupo, "codigo") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_ProximoSubGrupo,
                                   dgvSubGrupo, "codigo") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_AnteriorSubGrupo,
                                dgvSubGrupo, "codigo") == false)
                        {
                            if (dgvSubGrupo.Rows.Count > 0)
                            {
                                dgvSubGrupo.CurrentCell = dgvSubGrupo.Rows[0].Cells["codigo"];
                            }
                        }
                    }
                }
            }
            else if (dgvSubGrupo.Rows.Count > 0)
            {
                dgvSubGrupo.CurrentCell = dgvSubGrupo.Rows[0].Cells["codigo"];
            }

            if (dgvSubGrupo.CurrentRow != null)
            {
                idSubGrupo = clsParser.Int32Parse(dgvSubGrupo.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvSubGrupo.CurrentRow.Index > 0)
                {
                    id_AnteriorSubGrupo = clsParser.Int32Parse(dgvSubGrupo.Rows[dgvSubGrupo.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_AnteriorSubGrupo = 0;
                }
                if (dgvSubGrupo.CurrentRow.Index < dgvSubGrupo.Rows.Count - 1)
                {
                    id_ProximoSubGrupo = clsParser.Int32Parse(dgvSubGrupo.Rows[dgvSubGrupo.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_ProximoSubGrupo = 0;
                }
            }
            else
            {
                idSubGrupo = 0;
                id_AnteriorSubGrupo = 0;
                id_ProximoSubGrupo = 0;
            }
        }

        private void tspGrupoSalvar_Click(object sender, EventArgs e)
        {
            if (Salvar() == DialogResult.Cancel)
            {
                return;
            }

            tclPecasClassifica.SelectedIndex = 0;
            bwrGrupo_Run();
        }

        private void tspGrupoRetornar_Click(object sender, EventArgs e)
        {
            if (HouveModificacoes() == true)
            {
                tspGrupoSalvar.PerformClick();
            }
            else
            {
                tclPecasClassifica.SelectedIndex = 0;
            }

            bwrGrupo_Run();
        }

        private void PecasClassificaCarregar()
        {
            clsPecasclassificaInfoOld = new clsPecasclassificaInfo();

            if (id == 0)
            {
                clsPecasclassificaInfo = new clsPecasclassificaInfo();
                clsPecasclassificaInfo.codigo = "";
                clsPecasclassificaInfo.id = 0;
                clsPecasclassificaInfo.nome = "";
            }
            else
            {
                clsPecasclassificaInfo = clsPecasclassificaBLL.Carregar(id, clsInfo.conexaosqldados);
            }

            PecasClassificaCampos(clsPecasclassificaInfo);
            PecasClassificaFillInfo(clsPecasclassificaInfoOld);
        }

        private void PecasClassificaCampos(clsPecasclassificaInfo info)
        {
            id = info.id;
            tbxCodigo.Text = info.codigo;
            tbxNome.Text = info.nome;

            tbxCodigo.Select();
        }

        private void PecasClassificaFillInfo(clsPecasclassificaInfo info)
        {
            info.id = id;
            info.codigo = tbxCodigo.Text;
            info.nome = tbxNome.Text;
        }

        private Boolean HouveModificacoes()
        {
            clsPecasclassificaInfo = new clsPecasclassificaInfo();
            PecasClassificaFillInfo(clsPecasclassificaInfo);
            
            if (clsPecasclassificaBLL.Equals(clsPecasclassificaInfo, clsPecasclassificaInfoOld) == false)
            {
                return true;
            }

            return false;
        }

        private DialogResult Salvar()
        {
            DialogResult drt;

            drt = MessageBox.Show("Deseja Salvar este Registro e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);

            if (drt == DialogResult.Yes)
            {
                PecasClassificaSalvar();
            }

            return drt;
        }

        private void PecasClassificaSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: PECAS
                clsPecasclassificaInfo = new clsPecasclassificaInfo();
                PecasClassificaFillInfo(clsPecasclassificaInfo);
                if (id == 0)
                {
                    clsPecasclassificaInfo.id = clsPecasclassificaBLL.Incluir(clsPecasclassificaInfo, clsInfo.conexaosqldados);
                }
                else
                {
                    clsPecasclassificaBLL.Alterar(clsPecasclassificaInfo, clsInfo.conexaosqldados);
                }

                tse.Complete();
            }
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

        private void rbnTetoVis_Click(object sender, EventArgs e)
        {
            bwrGrupo_Run();
        }

        private void rbnTodos_Click(object sender, EventArgs e)
        {
            bwrGrupo_Run();
        }

        private void rbnBandeiraVis_Click(object sender, EventArgs e)
        {
            bwrGrupo_Run();
        }

        private void rbnGuardaCorpoVis_Click(object sender, EventArgs e)
        {
            bwrGrupo_Run();
        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
            tstbxLocalizar.Focus();
        }

        private void tstbxLocalizar_MouseUp(object sender, MouseEventArgs e)
        {
            PesquisaRapida();
            tstbxLocalizar.Focus();
        }

        private void dgvGrupo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspGrupoAlterar.PerformClick();
        }

        private void dgvGrupo_Click(object sender, EventArgs e)
        {
            if (dgvGrupo.CurrentRow != null)
            {
                gbxGrupo.Text = " Grupo " + dgvGrupo.CurrentRow.Cells["CODIGO"].Value.ToString() +
                                " - " + dgvGrupo.CurrentRow.Cells["NOME"].Value.ToString();

                id = clsParser.Int32Parse(dgvGrupo.CurrentRow.Cells["ID"].Value.ToString());
                // Carregando os Sub-Grupos do Grupo
                if (id > 0)
                {
                    if (bwrSubGrupo.IsBusy != true)
                    {
                        bwrSubGrupo_Run();
                    }
                }
            }
        }

        private void dgvGrupo_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvGrupo.CurrentRow != null)
            {
                gbxGrupo.Text = " Grupo " + dgvGrupo.CurrentRow.Cells["CODIGO"].Value.ToString() +
                                " - " + dgvGrupo.CurrentRow.Cells["NOME"].Value.ToString();

                id = clsParser.Int32Parse(dgvGrupo.CurrentRow.Cells["ID"].Value.ToString());
                // Carregando os Sub-Grupos do Grupo
                if (id > 0)
                {
                    if (bwrSubGrupo.IsBusy != true)
                    {
                        bwrSubGrupo_Run();
                    }
                }
            }
        }

        private void dgvGrupo_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvGrupo.CurrentRow != null)
            {
                gbxGrupo.Text = " Grupo " + dgvGrupo.CurrentRow.Cells["CODIGO"].Value.ToString() +
                                " - " + dgvGrupo.CurrentRow.Cells["NOME"].Value.ToString();

                id = clsParser.Int32Parse(dgvGrupo.CurrentRow.Cells["ID"].Value.ToString());
                // Carregando os Sub-Grupos do Grupo
                if (id > 0)
                {
                    if (bwrSubGrupo.IsBusy != true)
                    {
                        bwrSubGrupo_Run();
                    }
                }
            }
        }

        private void dgvSubGrupo_Click(object sender, EventArgs e)
        {
            if (dgvSubGrupo.CurrentRow != null)
            {
                gbxSubGrupo.Text = " Sub-Grupo " + dgvSubGrupo.CurrentRow.Cells["CODIGO"].Value.ToString() +
                                " - " + dgvSubGrupo.CurrentRow.Cells["NOME"].Value.ToString();

                idSubGrupo = clsParser.Int32Parse(dgvSubGrupo.CurrentRow.Cells["ID"].Value.ToString());
                // Carregando os Sub-Grupos do Grupo
                if (idSubGrupo > 0)
                {
                    if (bwrItemSubGrupo.IsBusy != true)
                    {
                        bwrItemSubGrupo_Run();
                    }
                }
            }
        }

        private void dgvSubGrupo_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvSubGrupo.CurrentRow != null)
            {
                gbxSubGrupo.Text = " Sub-Grupo " + dgvSubGrupo.CurrentRow.Cells["CODIGO"].Value.ToString() +
                                " - " + dgvSubGrupo.CurrentRow.Cells["NOME"].Value.ToString();

                idSubGrupo = clsParser.Int32Parse(dgvSubGrupo.CurrentRow.Cells["ID"].Value.ToString());
                // Carregando os Sub-Grupos do Grupo
                if (idSubGrupo > 0)
                {
                    if (bwrItemSubGrupo.IsBusy != true)
                    {
                        bwrItemSubGrupo_Run();
                    }
                }
            }
        }

        private void dgvSubGrupo_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvSubGrupo.CurrentRow != null)
            {
                gbxSubGrupo.Text = " Sub-Grupo " + dgvSubGrupo.CurrentRow.Cells["CODIGO"].Value.ToString() +
                                " - " + dgvSubGrupo.CurrentRow.Cells["NOME"].Value.ToString();

                idSubGrupo = clsParser.Int32Parse(dgvSubGrupo.CurrentRow.Cells["ID"].Value.ToString());
                // Carregando os Sub-Grupos do Grupo
                if (idSubGrupo > 0)
                {
                    if (bwrItemSubGrupo.IsBusy != true)
                    {
                        bwrItemSubGrupo_Run();
                    }
                }
            }
        }

        private void tspSubGrupoIncluir_Click(object sender, EventArgs e)
        {
            idSubGrupo = 0;
            id_ProximoSubGrupo = 0;
            id_AnteriorSubGrupo = 0;

            gbxRegistrando.Visible = true;
            gbxRegistrando.Enabled = false;
            PecasClassificaCarregar();

            gbxRegistrandoSubGrupo.Visible = true;
            gbxRegistrandoSubGrupo.Enabled = true;

            gbxRegistrandoItemSubGrupo.Visible = false;

            tclPecasClassifica.SelectedIndex = 1;
            PecasClassifica1Carregar();
        }

        private void tspSubGrupoAlterar_Click(object sender, EventArgs e)
        {
            if (dgvSubGrupo.CurrentRow != null)
            {
                idSubGrupo = clsParser.Int32Parse(dgvSubGrupo.CurrentRow.Cells["ID"].Value.ToString());

                if (dgvSubGrupo.CurrentRow.Index > 0)
                {
                    id_AnteriorSubGrupo = clsParser.Int32Parse(dgvSubGrupo.Rows[dgvSubGrupo.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_AnteriorSubGrupo = 0;
                }

                if (dgvSubGrupo.CurrentRow.Index < dgvSubGrupo.Rows.Count - 1)
                {
                    id_ProximoSubGrupo = clsParser.Int32Parse(dgvSubGrupo.Rows[dgvSubGrupo.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_ProximoSubGrupo = 0;
                }

                gbxRegistrando.Visible = true;
                gbxRegistrando.Enabled = false;
                PecasClassificaCarregar();

                gbxRegistrandoSubGrupo.Visible = true;
                gbxRegistrandoSubGrupo.Enabled = true;

                gbxRegistrandoItemSubGrupo.Visible = false;

                tclPecasClassifica.SelectedIndex = 1;
                PecasClassifica1Carregar();
            }
        }

        private void tspSubGrupoExcluir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desabilitado");
        }

        private void tstbxLocalizarSubGrupo_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapidaSub();
            tstbxLocalizarSubGrupo.Focus();
        }

        private void tstbxLocalizarSubGrupo_MouseUp(object sender, MouseEventArgs e)
        {
            PesquisaRapidaSub();
            tstbxLocalizarSubGrupo.Focus();
        }

        private void PecasClassifica1Carregar()
        {
            clsPecasclassifica1InfoOld = new clsPecasclassifica1Info();

            if (idSubGrupo == 0)
            {
                clsPecasclassifica1Info = new clsPecasclassifica1Info();
                clsPecasclassifica1Info.codigo = "";
                clsPecasclassifica1Info.id = 0;
                clsPecasclassifica1Info.idclassifica = id;
                clsPecasclassifica1Info.nome = "";
            }
            else
            {
                clsPecasclassifica1Info = clsPecasclassifica1BLL.Carregar(idSubGrupo, clsInfo.conexaosqldados);
            }

            PecasClassifica1Campos(clsPecasclassifica1Info);
            PecasClassifica1FillInfo(clsPecasclassifica1InfoOld);
        }

        private void PecasClassifica1Campos(clsPecasclassifica1Info info)
        {
            idSubGrupo = info.id;
            tbxCodigoSubGrupo.Text = info.codigo;
            tbxNomeSubGrupo.Text = info.nome;
            tbxCodigoSubGrupo.Select();
        }

        private void PecasClassifica1FillInfo(clsPecasclassifica1Info info)
        {
            info.id = idSubGrupo;
            info.idclassifica = id;
            info.codigo = tbxCodigoSubGrupo.Text;
            info.nome = tbxNomeSubGrupo.Text;
        }

        private void tspSubGrupoRetornar_Click(object sender, EventArgs e)
        {
            if (HouveModificacoesSubGrupo() == true)
            {
                tspSubGrupoSalvar.PerformClick();
            }
            else
            {
                tclPecasClassifica.SelectedIndex = 0;
            }

            bwrSubGrupo_Run();
        }

        private Boolean HouveModificacoesSubGrupo()
        {
            clsPecasclassifica1Info = new clsPecasclassifica1Info();
            PecasClassifica1FillInfo(clsPecasclassifica1Info);

            if (clsPecasclassifica1BLL.Equals(clsPecasclassifica1Info, clsPecasclassifica1InfoOld) == false)
            {
                return true;
            }

            return false;
        }

        private void tspSubGrupoSalvar_Click(object sender, EventArgs e)
        {
            if (SalvarSubGrupo() == DialogResult.Cancel)
            {
                return;
            }

            tclPecasClassifica.SelectedIndex = 0;
            bwrSubGrupo_Run();
        }

        private DialogResult SalvarSubGrupo()
        {
            var drt = MessageBox.Show("Deseja Salvar este Sub-Grupo e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);

            if (drt == DialogResult.Yes)
            {
                PecasClassifica1Salvar();
            }

            return drt;
        }

        private void PecasClassifica1Salvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                clsPecasclassifica1Info = new clsPecasclassifica1Info();
                PecasClassifica1FillInfo(clsPecasclassifica1Info);
                if (idSubGrupo == 0)
                {
                    clsPecasclassifica1Info.id = clsPecasclassifica1BLL.Incluir(clsPecasclassifica1Info, clsInfo.conexaosqldados);
                }
                else
                {
                    clsPecasclassifica1BLL.Alterar(clsPecasclassifica1Info, clsInfo.conexaosqldados);
                }
                tse.Complete();
            }
        }

        private void dgvSubGrupo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspSubGrupoAlterar.PerformClick();
        }

        private void tspItemSubGrupoIncluir_Click(object sender, EventArgs e)
        {
            idItemSubGrupo = 0;
            id_ProximoItemSubGrupo = 0;
            id_AnteriorItemSubGrupo = 0;


            gbxRegistrando.Visible = true;
            gbxRegistrando.Enabled = false;
            PecasClassificaCarregar();


            gbxRegistrandoSubGrupo.Visible = true;
            gbxRegistrandoSubGrupo.Enabled = false;
            tclPecasClassifica.SelectedIndex = 1;
            PecasClassifica1Carregar();

            gbxRegistrandoItemSubGrupo.Visible = true;
            tclPecasClassifica.SelectedIndex = 1;
            PecasClassifica2Carregar();
        }

        private void tspItemSubGrupoAlterar_Click(object sender, EventArgs e)
        {
            if (dgvItemSubGrupo.CurrentRow != null)
            {
                idItemSubGrupo = clsParser.Int32Parse(dgvItemSubGrupo.CurrentRow.Cells["ID"].Value.ToString());

                if (dgvItemSubGrupo.CurrentRow.Index > 0)
                {
                    id_AnteriorItemSubGrupo = clsParser.Int32Parse(dgvItemSubGrupo.Rows[dgvItemSubGrupo.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_AnteriorItemSubGrupo = 0;
                }


                if (dgvItemSubGrupo.CurrentRow.Index < dgvItemSubGrupo.Rows.Count - 1)
                {
                    id_ProximoItemSubGrupo = clsParser.Int32Parse(dgvItemSubGrupo.Rows[dgvItemSubGrupo.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_ProximoItemSubGrupo = 0;
                }
                gbxRegistrando.Visible = true;
                gbxRegistrando.Enabled = false;
                PecasClassificaCarregar();

                gbxRegistrandoSubGrupo.Visible = true;
                gbxRegistrandoSubGrupo.Enabled = false;
                tclPecasClassifica.SelectedIndex = 1;
                PecasClassifica1Carregar();

                gbxRegistrandoItemSubGrupo.Visible = true;
                tclPecasClassifica.SelectedIndex = 1;
                PecasClassifica2Carregar();
            }
        }

        private void tspItemSubGrupoExcluir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desabilitado");
        }

        private void PecasClassifica2Carregar()
        {
            clsPecasclassifica2InfoOld = new clsPecasclassifica2Info();

            if (idItemSubGrupo == 0)
            {
                clsPecasclassifica2Info = new clsPecasclassifica2Info();
                clsPecasclassifica2Info.codigo = "";
                clsPecasclassifica2Info.id = 0;
                clsPecasclassifica2Info.idclassifica = id;
                clsPecasclassifica2Info.idclassifica1 = idSubGrupo;
                clsPecasclassifica2Info.nome = "";
            }
            else
            {
                clsPecasclassifica2Info = clsPecasclassifica2BLL.Carregar(idItemSubGrupo, clsInfo.conexaosqldados);
            }

            PecasClassifica2Campos(clsPecasclassifica2Info);
            PecasClassifica2FillInfo(clsPecasclassifica2InfoOld);
        }

        private void PecasClassifica2Campos(clsPecasclassifica2Info info)
        {
            idItemSubGrupo = info.id;
            tbxCodigoItemSubGrupo.Text = info.codigo;
            tbxNomeItemSubGrupo.Text = info.nome;
            tbxCodigoItemSubGrupo.Select();
        }

        private void PecasClassifica2FillInfo(clsPecasclassifica2Info info)
        {
            info.id = idItemSubGrupo;
            info.idclassifica = id;
            info.idclassifica1 = idSubGrupo; 
            info.codigo = tbxCodigoItemSubGrupo.Text;
            info.nome = tbxNomeItemSubGrupo.Text;
        }

        private void tspItemSubGrupoRetornar_Click(object sender, EventArgs e)
        {
            if (HouveModificacoesItemSubGrupo() == true)
            {
                tspItemSubGrupoSalvar.PerformClick();
            }
            else
            {
                tclPecasClassifica.SelectedIndex = 0;
            }

            bwrItemSubGrupo_Run();
        }

        private Boolean HouveModificacoesItemSubGrupo()
        {
            clsPecasclassifica2Info = new clsPecasclassifica2Info();
            PecasClassifica2FillInfo(clsPecasclassifica2Info);
            if (clsPecasclassifica2BLL.Equals(clsPecasclassifica2Info, clsPecasclassifica2InfoOld) == false)
            {
                return true;
            }

            return false;
        }

        private void tspItemSubGrupoSalvar_Click(object sender, EventArgs e)
        {
            if (SalvarItemSubGrupo() == DialogResult.Cancel)
            {
                return;
            }

            tclPecasClassifica.SelectedIndex = 0;

            bwrItemSubGrupo_Run();
        }

        private DialogResult SalvarItemSubGrupo()
        {
            var drt = MessageBox.Show("Deseja Salvar este Item do Sub-Grupo e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);

            if (drt == DialogResult.Yes)
            {
                PecasClassifica2Salvar();
            }

            return drt;
        }

        private void PecasClassifica2Salvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                clsPecasclassifica2Info = new clsPecasclassifica2Info();
                PecasClassifica2FillInfo(clsPecasclassifica2Info);
                if (idItemSubGrupo == 0)
                {
                    clsPecasclassifica2Info.id = clsPecasclassifica2BLL.Incluir(clsPecasclassifica2Info, clsInfo.conexaosqldados);
                }
                else
                {
                    clsPecasclassifica2BLL.Alterar(clsPecasclassifica2Info, clsInfo.conexaosqldados);
                }
                tse.Complete();
            }
        }

        private void PesquisaRapidaItemSub()
        {
            clsGridHelper.Filtrar(tstbxLocalizarItemSubGrupo.Text, dgvItemSubGrupo);


            if (idItemSubGrupo > 0 || id_AnteriorItemSubGrupo > 0 || id_ProximoItemSubGrupo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(idItemSubGrupo, dgvItemSubGrupo, "codigo") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_ProximoItemSubGrupo,
                                   dgvItemSubGrupo, "codigo") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_AnteriorItemSubGrupo,
                                dgvItemSubGrupo, "codigo") == false)
                        {
                            if (dgvItemSubGrupo.Rows.Count > 0)
                            {
                                dgvItemSubGrupo.CurrentCell = dgvItemSubGrupo.Rows[0].Cells["codigo"];
                            }
                        }
                    }
                }
            }
            else if (dgvItemSubGrupo.Rows.Count > 0)
            {
                dgvItemSubGrupo.CurrentCell = dgvItemSubGrupo.Rows[0].Cells["codigo"];
            }

            if (dgvItemSubGrupo.CurrentRow != null)
            {
                idItemSubGrupo = clsParser.Int32Parse(dgvItemSubGrupo.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvItemSubGrupo.CurrentRow.Index > 0)
                {
                    id_AnteriorItemSubGrupo = clsParser.Int32Parse(dgvItemSubGrupo.Rows[dgvItemSubGrupo.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_AnteriorItemSubGrupo = 0;
                }
                if (dgvItemSubGrupo.CurrentRow.Index < dgvItemSubGrupo.Rows.Count - 1)
                {
                    id_ProximoItemSubGrupo = clsParser.Int32Parse(dgvItemSubGrupo.Rows[dgvItemSubGrupo.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_ProximoItemSubGrupo = 0;
                }
            }
            else
            {
                idItemSubGrupo = 0;
                id_AnteriorItemSubGrupo = 0;
                id_ProximoItemSubGrupo = 0;
            }
        }

        private void dgvItemSubGrupo_Click(object sender, EventArgs e)
        {
            if (dgvItemSubGrupo.CurrentRow != null)
            {
                gbxItemSubGrupo.Text = " Item Sub-Grupo " + dgvItemSubGrupo.CurrentRow.Cells["CODIGO"].Value.ToString() +
                                " - " + dgvItemSubGrupo.CurrentRow.Cells["NOME"].Value.ToString();
                idItemSubGrupo = clsParser.Int32Parse(dgvItemSubGrupo.CurrentRow.Cells["ID"].Value.ToString());
            }

        }

        private void dgvItemSubGrupo_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvItemSubGrupo.CurrentRow != null)
            {
                gbxItemSubGrupo.Text = " Item Sub-Grupo " + dgvItemSubGrupo.CurrentRow.Cells["CODIGO"].Value.ToString() +
                                " - " + dgvItemSubGrupo.CurrentRow.Cells["NOME"].Value.ToString();
                idItemSubGrupo = clsParser.Int32Parse(dgvItemSubGrupo.CurrentRow.Cells["ID"].Value.ToString());
            }

        }

        private void dgvItemSubGrupo_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvItemSubGrupo.CurrentRow != null)
            {
                gbxItemSubGrupo.Text = " Item Sub-Grupo " + dgvItemSubGrupo.CurrentRow.Cells["CODIGO"].Value.ToString() +
                                " - " + dgvItemSubGrupo.CurrentRow.Cells["NOME"].Value.ToString();
                idItemSubGrupo = clsParser.Int32Parse(dgvItemSubGrupo.CurrentRow.Cells["ID"].Value.ToString());
            }

        }

        private void dgvItemSubGrupo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspItemSubGrupoAlterar.PerformClick();
        }

        private void tspItemSubGrupoSalvar_Click_1(object sender, EventArgs e)
        {
            if (SalvarItemSubGrupo() == DialogResult.Cancel)
            {
                return;
            }
            tclPecasClassifica.SelectedIndex = 0;
            bwrItemSubGrupo_Run();
        }
    }
}
    
