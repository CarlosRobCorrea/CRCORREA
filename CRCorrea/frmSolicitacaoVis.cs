using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmSolicitacaoVis : Form
    {
        String filtro_status;
        DataTable dtSolicitacao;
        clsSolicitacaoBLL clsSolicitacaoBLL;
        
        Int32 filial;
        DateTime filtro_periodode;
        DateTime filtro_periodoate;

        public static Int32 id = 0;
        Int32 id_anterior = 0;
        Int32 id_proximo = 0;

        BackgroundWorker bwrSolicitacao;
        Boolean carregarSolicitacao;
        Control ultimoObjetoFiltroFocado;


        public frmSolicitacaoVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            bwrSolicitacao = new BackgroundWorker();
            bwrSolicitacao.WorkerSupportsCancellation = true;
            bwrSolicitacao.DoWork += new DoWorkEventHandler(bwrSolicitacao_DoWork);
            bwrSolicitacao.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrSolicitacao_RunWorkerCompleted);
            carregarSolicitacao = false;
            
            clsVisual.FillComboBox(cbxFilial, "select right('00000' + CONVERT(VARCHAR, CODIGO), 2) + ' - ' +  COGNOME from empresa order by CODIGO", clsInfo.conexaosqldados);

            clsSolicitacaoBLL = new clsSolicitacaoBLL();
        }
        private void frmSolicitacaoVis_Load(object sender, EventArgs e)
        {
            tbxPeriodoDe.Text = DateTime.Now.AddDays(-DateTime.Now.Day + 1).AddMonths(-6).ToString("dd/MM/yyyy");
            tbxPeriodoAte.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            cbxFilial.SelectedIndex = 0;
        }
        private void frmSolicitacaoVis_Activated(object sender, EventArgs e)
        {
            bwrSolicitacao_Run();

        }


        private void bwrSolicitacao_Run() 
        {
            if (carregarSolicitacao == false)
            {
                filial = clsParser.Int32Parse(cbxFilial.Text.Substring(0, 2));
                //FillFiltros();
                DesabilitaFiltros();
                carregarSolicitacao = true;
                //pbxCarregarSolicitacao.Visible = true;
                bwrSolicitacao.RunWorkerAsync(); 
            }
        }

        private void bwrSolicitacao_DoWork(object sender, DoWorkEventArgs e)
        {
            solicitacaoGrid();
            if (bwrSolicitacao.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        public void solicitacaoGrid()
        {
            if (rbnStatusA.Checked == true)
            { // Aberto
                filtro_status = "A";
            }
            else if (rbnStatusC.Checked == true)
            { // em Cotacao
                filtro_status = "C";
            }
            else
            {  // Fechado ja emitiu Pedido
                filtro_status = "F";
            }
            dtSolicitacao = clsSolicitacaoBLL.GridDados(filtro_periodode, filtro_periodoate, filial, filtro_status);
        }

        private void bwrSolicitacao_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled != true)
                {
                    //pbxCarregarSolicitacao.Visible = false;
                    clsSolicitacaoBLL.GridMonta(dgvSolicitacao, dtSolicitacao, clsInfo.zrowid);                    
                    PesquisaRapida();
                    HabilitaFiltros();
                    rbnStatusC.BackColor = SystemColors.Control;
                    rbnStatusF.BackColor = SystemColors.Control;
                    rbnStatusA.BackColor = SystemColors.Control;
                    if (ultimoObjetoFiltroFocado != null)
                    {
                        ultimoObjetoFiltroFocado.Select();
                    }

                    carregarSolicitacao = false;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregarSolicitacao = false;
            }
        }

        private void DesabilitaFiltros()
        {
            gbxStatus.Enabled = false;
            gbxPeriodo.Enabled = false;
            gbxUsuario.Enabled = false;
            gbxFilial.Enabled = false;
        }

        private void HabilitaFiltros()
        {
            gbxStatus.Enabled = true;
            gbxPeriodo.Enabled = true;
            gbxUsuario.Enabled = false;
            gbxFilial.Enabled = true;
        }

        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvSolicitacao);
            if (id > 0 || id_anterior > 0 || id_proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvSolicitacao, "NROSOLICITA") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_proximo,
                                   dgvSolicitacao, "NROSOLICITA") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_anterior,
                                dgvSolicitacao, "NROSOLICITA") == false)
                        {
                            if (dgvSolicitacao.Rows.Count > 0)
                            {
                                dgvSolicitacao.CurrentCell = dgvSolicitacao.Rows[0].Cells["NROSOLICITA"];
                            }
                        }
                    }
                }
            }
            else if (dgvSolicitacao.Rows.Count > 0)
            {
                dgvSolicitacao.CurrentCell = dgvSolicitacao.Rows[0].Cells["NROSOLICITA"];
            }

            if (dgvSolicitacao.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                      dgvSolicitacao.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvSolicitacao.CurrentRow.Index > 0)
                {
                    id_anterior = clsParser.Int32Parse(dgvSolicitacao.Rows[
           dgvSolicitacao.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_anterior = 0;
                }
                if (dgvSolicitacao.CurrentRow.Index < dgvSolicitacao.Rows.Count - 1)
                {
                    id_proximo = clsParser.Int32Parse(dgvSolicitacao.Rows[
         dgvSolicitacao.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_proximo = 0;
                }
            }
            else
            {
                id = 0;
                id_anterior = 0;
                id_proximo = 0;
            }
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            //clsBR.Imprimir(clsInfo.caminhorelatorios, "Cadastro de Solicitações", clsInfo.conexaosqldados);
        }

        private void rbn_CheckedChanged(object sender, EventArgs e)
        {
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

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void tstPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
            tstbxLocalizar.Focus();
        }

        private void tstPesquisa_MouseUp(object sender, MouseEventArgs e)
        {
            PesquisaRapida();
            tstbxLocalizar.Focus();
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            //cancela o BackgroundWorker fazendo isso ele set o CancellationPending do BackgroundWorker                                                                             //bwrAbertas_RunWorkerCompleted 
            //e no evento DoWork verifica o pedido de cancelamento e cancela a operação 
            //evitando assim a mensagem de referencia de objeto
            carregarSolicitacao = true;
            bwrSolicitacao.CancelAsync();
            bwrSolicitacao.Dispose();
            this.Close();
            this.Dispose();
        }

        private void tspImprimirMnu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Em Desenvolvimento.");
        }
        private void tspExcluir_Click(object sender, EventArgs e)
        {
            if (dgvSolicitacao.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvSolicitacao.CurrentRow.Cells["ID"].Value.ToString());

                if (clsParser.Int32Parse(dgvSolicitacao.CurrentRow.Cells["COTANUMERO"].Value.ToString()) == 0)
                {  // não fez a cotação pode apagar

                    DialogResult drt;
                    drt = MessageBox.Show("Deseja mesmo Apagar esta Solicitação nro : " + dgvSolicitacao.CurrentRow.Cells["NROSOLICITA"].Value.ToString() + " ?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
                    if (drt == DialogResult.Yes)
                    {
                        String query;
                        SqlConnection scn;
                        SqlCommand scd;
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        scn.Open();
                        // Apagar as Entregas
                        query = "delete solicitacaoentrega where idsolicitacao = @id";
                        scd = new SqlCommand(query, scn);
                        scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        scd.ExecuteNonQuery();
                        // Apagar os Itens
                        query = "delete solicitacaofornecedor where idsolicitacao = @id";
                        scd = new SqlCommand(query, scn);
                        scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        scd.ExecuteNonQuery();
                        // Apagar o Cabeçalho
                        query = "delete solicitacao where id = @id";
                        scd = new SqlCommand(query, scn);
                        scd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        scd.ExecuteNonQuery();
                        scn.Close();
                        bwrSolicitacao_Run();
                    }
                }
                else
                {
                    MessageBox.Show("Já existe Cotação não pode simplesmente Apagar - Exclua da Cotação");
                }

            }
            else
            {
                MessageBox.Show("De um click no Registro!", "Aplisoft", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                dgvSolicitacao.Select();
            }

        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvSolicitacao.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvSolicitacao.CurrentRow.Cells["ID"].Value.ToString());

                if (dgvSolicitacao.CurrentRow.Index > 0)
                {
                    id_anterior = clsParser.Int32Parse(dgvSolicitacao.Rows[dgvSolicitacao.CurrentRow.Index -
                                   1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_anterior = 0;
                }

                if (dgvSolicitacao.CurrentRow.Index < dgvSolicitacao.Rows.Count - 1)
                {
                    id_proximo = clsParser.Int32Parse(dgvSolicitacao.Rows[dgvSolicitacao.CurrentRow.Index + 1
                              ].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_proximo = 0;
                }

                frmSolicitacao frmSolicitacao = new frmSolicitacao();
                frmSolicitacao.Init(id, dgvSolicitacao.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmSolicitacao, clsInfo.conexaosqldados);
            }
            else
            {
                MessageBox.Show("Primeiro selecione um Registro!", "Aplisoft", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                dgvSolicitacao.Select();
            }
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvSolicitacao.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvSolicitacao.CurrentRow.Cells["ID"].Value.ToString());
            }
            else
            {
                id = 0;
            }
            id_proximo = 0;
            id_anterior = 0;

            frmSolicitacao frmSolicitacao = new frmSolicitacao();
            frmSolicitacao.Init(0, dgvSolicitacao.Rows);

            clsFormHelper.AbrirForm(this.MdiParent, frmSolicitacao, clsInfo.conexaosqldados);
        }


        private void dgvSolicitacao_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void rbnStatusA_Click(object sender, EventArgs e)
        {
            bwrSolicitacao_Run();
        }

        private void rbnStatusC_Click(object sender, EventArgs e)
        {
            bwrSolicitacao_Run();
        }

        private void rbnStatusF_Click(object sender, EventArgs e)
        {
            bwrSolicitacao_Run();
        }

       
    }
}







//        Int32 id;

//        Int32 filial;
//        String filtro_status;
//        DateTime filtro_periodode;
//        DateTime filtro_periodoate;

//        clsGrid clsGrid;
//        
//        clsProcedures clsProcedures;

//        clsBasicReport clsBr;

//        SqlDataAdapter sda;

//        BackgroundWorker bwrSolicitacao;

//        DataTable dtSolicitacao;

//        DialogResult resultado = new DialogResult();

//        clsSolicitacaoBLL clsSolicitacaoBLL;

//        Boolean carregandoSolicitacao;

//        public frmSolicitacaoVis()
//        {
//            InitializeComponent();
//        }

//        public void Init()
//        {
//            clsGrid = new clsGrid();
//            
//            clsProcedures = new clsProcedures();

//            clsBr = new clsBasicReport(this, dgvSolicitacao, toolTip1);

//            carregandoSolicitacao = false;

//            clsSolicitacaoBLL = new clsSolicitacaoBLL();

//            clsVisual.FillComboBox(cbxFilial, "select right('00000' + CONVERT(VARCHAR, CODIGO), 2) + ' - ' +  COGNOME from empresas order by CODIGO", clsInfo.conexaosqldados);

//            clsInfo.zrowid = 0;
//        }

//        private Boolean FiltroMudou()
//        {
//            if (rbnStatusS.Checked == true && filtro_status != "S")
//            {
//                return true;
//            }
//            else if (rbnStatusN.Checked == true && filtro_status != "N")
//            {
//                return true;
//            }
//            else if (rbnStatusT.Checked == true && filtro_status != "T")
//            {
//                return true;
//            }

//            if (filtro_periodode != clsParser.DateTimeParse(tbxPeriodoDe.Text))
//            {
//                return true;
//            }
//            else if (filtro_periodoate != clsParser.DateTimeParse(tbxPeriodoAte.Text))
//            {
//                return true;
//            }

//            if (filial != clsParser.Int32Parse(cbxFilial.Text.Substring(0, 2)))
//            {
//                return true;
//            }

//            return false;
//        }

//        private void frmSolicitacaoVis_Load(object sender, EventArgs e)
//        {
//            tbxPeriodoDe.Text = DateTime.Now.AddDays(-DateTime.Now.Day + 1).AddMonths(-6).ToString("dd/MM/yyyy");
//            tbxPeriodoAte.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
//            cbxFilial.SelectedIndex = 0;
//            rbnStatusS.Checked = true;
//        }

//        private void frmSolicitacaoVis_Activated(object sender, EventArgs e)
//        {
//            bwrSolicitacao_Run();
//        }

//        private void tspIncluir_Click(object sender, EventArgs e)
//        {
//            frmSolicitacao frmSolicitacao = new frmSolicitacao();
//            frmSolicitacao.Init(0, dgvSolicitacao.Rows);
//            clsFormHelper.AbrirForm(this.MdiParent, frmSolicitacao, clsInfo.conexaosqldados);
//        }

//        private void tspAlterar_Click(object sender, EventArgs e)
//        {
///*            if (dgvSolicitacao.CurrentRow != null)
//            {
//                id = clsParser.Int32Parse(dgvSolicitacao.CurrentRow.Cells["ID"].Value.ToString());
//                frmSolicitacao frmSolicitacao = new frmSolicitacao(id, dgvSolicitacao.Rows);
//                clsFormHelper.AbrirForm(this.MdiParent, frmSolicitacao, clsInfo.conexaosqldados);
//            }*/
//            id = 0;
//            frmSolicitacao frmSolicitacao = new frmSolicitacao();
//            frmSolicitacao.Init(id, dgvSolicitacao.Rows);
//            clsFormHelper.AbrirForm(this.MdiParent, frmSolicitacao, clsInfo.conexaosqldados);

//        }

//        private void tspImprimir_Click(object sender, EventArgs e)
//        {
//            clsBr.Imprimir(clsInfo.caminhorelatorios, "Cadastro de Solicitações", clsInfo.conexaosqldados);
//        }

//        private void tspImprimirMnu_Click(object sender, EventArgs e)
//        {
//            MessageBox.Show("Em Desenvolvimento.");
//        }

//        private void tspRetornar_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }

//        private void bwrSolicitacao_Run()
//        {
//            if (FiltroMudou() == true)
//            {
//                if (carregandoSolicitacao == false)
//                {
//                    if (rbnStatusT.Checked == true)
//                    {
//                        filtro_status = "T";
//                    }
//                    else if (rbnStatusS.Checked == true)
//                    {
//                        filtro_status = "S";
//                    }
//                    else if (rbnStatusN.Checked == true)
//                    {
//                        filtro_status = "N";
//                    }

//                    filial = clsParser.Int32Parse(cbxFilial.Text.Substring(0, 2));
//                    filtro_periodode = clsParser.DateTimeParse(tbxPeriodoDe.Text);
//                    filtro_periodoate = clsParser.DateTimeParse(tbxPeriodoAte.Text);

//                    carregandoSolicitacao = true;
//                    pbxCarregarSolicitacao.Visible = true;
//                    bwrSolicitacao = new BackgroundWorker();
//                    bwrSolicitacao.DoWork += new DoWorkEventHandler(bwrSolicitacao_DoWork);
//                    bwrSolicitacao.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrSolicitacao_RunWorkerCompleted);
//                    bwrSolicitacao.RunWorkerAsync();
//                }
//            }
//        }


//        private void bwrSolicitacao_DoWork(object sender, DoWorkEventArgs e)
//        {
//            dtSolicitacao = clsSolicitacaoBLL.GridDados(filtro_periodode, filtro_periodoate, filial, filtro_status);
//        }


//        private void bwrSolicitacao_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
//        {
//            try
//            {
//                pbxCarregarSolicitacao.Visible = false;
//                clsSolicitacaoBLL.GridMonta(dgvSolicitacao, dtSolicitacao, clsInfo.zrowid);
//                clsGridHelper.SelecionaLinha(id, dgvSolicitacao, 1);
//                carregandoSolicitacao = false;

//                clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvSolicitacao);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            }
//            finally
//            {
//                carregandoSolicitacao = false;
//            }
//        }

//        private void ControlEnter(object sender, EventArgs e)
//        {
//            clsVisual.ControlEnter(sender);
//        }

//        private void ControlLeave(object sender, EventArgs e)
//        {
//            clsVisual.ControlLeave(sender);
//            if (sender is TextBox)
//            {
//                bwrSolicitacao_Run();
//            }
//        }

//        private void ControlKeyDown(object sender, KeyEventArgs e)
//        {
//            clsVisual.ControlKeyDown(sender, e);
//        }

//        private void ControlKeyDownData(object sender, KeyEventArgs e)
//        {
//            clsVisual.ControlKeyDownData((TextBox)sender, e);
//        }

//        private void dgvSolicitacao_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
//        {
//            tspAlterar.PerformClick();
//        }

//        private void pbxSolicitacao_Click(object sender, EventArgs e)
//        {

//        }

//        private void rbnStatusF_CheckedChanged(object sender, EventArgs e)
//        {
//            bwrSolicitacao_Run();
//        }

//        private void cbxFilial_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            bwrSolicitacao_Run();
//        }

//        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
//        {
//            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvSolicitacao);
//        }
//    }
//}
