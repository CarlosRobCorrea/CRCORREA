using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorrea
{

    public partial class frmMaqCtVis : Form
    {
        clsMaqctBLL clsMaqctBLL;
        clsMaqctprecoBLL clsMaqctPrecoBLL;        

        DataTable dtMaqct;      

        DataTable dtMaqctPreco;       

        clsBasicReport BR;

        String filtro_ativo;
        String situacao;

        Boolean carregarMaqCt;    
        Boolean carregarMaqCtPreco;

        BackgroundWorker bwrMaqct;
        BackgroundWorker bwrMaqctPreco;

        public static Int32 maqct_id;     
        Int32 id_Anterior = 0;
        Int32 id_Proximo = 0;
        Int32 maqctpreco_id = 0;  

        Control ultimoObjetodeFiltroFocado;

        public frmMaqCtVis()
        {
            InitializeComponent();
        }

        public void Init()
        {
            //Instanciamos, colocamos os eventos e habilitamos o suporte e cancelamento do BackgroundWorker 
            bwrMaqct = new BackgroundWorker();
            //apenas para permitir a funcionalidade de cancelamento
            bwrMaqct.WorkerSupportsCancellation = true;
            bwrMaqct.DoWork += new DoWorkEventHandler(bwrMaqct_DoWork);
            bwrMaqct.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrMaqct_RunWorkerCompleted);
            //aqui inicializamos a variavel carregarCliente com false 
            //para o BackgroundWorker poder rodar na primeira chamada
            bwrMaqctPreco = new BackgroundWorker();
            //apenas para permitir a funcionalidade de cancelamento
            bwrMaqctPreco.WorkerSupportsCancellation = true;
            bwrMaqctPreco.DoWork += new DoWorkEventHandler(bwrMaqctPreco_DoWork);
            bwrMaqctPreco.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrMaqctPreco_RunWorkerCompleted);
            //aqui inicializamos a variavel carregarCliente com false 
            //para o BackgroundWorker poder rodar na primeira chamada
            carregarMaqCt = false;
            carregarMaqCtPreco = false; 
            clsMaqctPrecoBLL = new clsMaqctprecoBLL();

            clsMaqctBLL = new clsMaqctBLL();
            
            BR = new clsBasicReport(this, dgvMaqCt, ToolTip);           
        }

        private void frmMaqCtVis_Load(object sender, EventArgs e)
        {
            
        }

        private void FillFiltros()
        {
            if (rbnAtivo.Checked == true)
            { 
                filtro_ativo = "T";
            }
            else if (rbnAtivoS.Checked == true)
            {
                filtro_ativo = "S";
            }
            else if (rbnAtivoN.Checked == true)
            {
                filtro_ativo = "N";
            }
            else
            {
                filtro_ativo = "";
            }
        }

        private bool FiltroMudancas()
        {
            if (filtro_ativo == "T" && rbnAtivo.Checked == false)
            {
                return false;
            }
            else if (filtro_ativo == "S" && rbnAtivoS.Checked == false)
            {
                return false;
            }
            else if (filtro_ativo == "N" && rbnAtivoN.Checked == false)
            {
                return false;
            }

            return false;
        }

        void Filtrar()
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvMaqCt);
        }

        private void StatusCheckedChanged(object sender, EventArgs e)
        {
            ultimoObjetodeFiltroFocado = (Control)sender;

            if (FiltroMudancas() == false)
            {
                DesabilitaFiltros();
                bwrMaqct_Run();
            }
        }

        private void bwrMaqct_Run()
        {
            if (carregarMaqCt == false)
            {
                carregarMaqCt = true;
                pbxMaqCt.Visible = true;
                FillFiltros();
                DesabilitaFiltros();

                bwrMaqct.RunWorkerAsync();
            }
        }        

        private void bwrMaqct_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                MaqctGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (bwrMaqct.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        public void MaqctGrid()
        {
            switch (filtro_ativo)
            {
                case "S":
                    situacao = "S";
                    break;
                case "N":
                    situacao = "N";
                    break;
                default:
                    situacao = "T";
                    break;
            }

            dtMaqct = clsMaqctBLL.CarregaGrid(clsInfo.conexaosqldados, situacao);
        }

        private void bwrMaqct_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled != true)
                {
                    //o BackgroundWorker completou-se com suscesso e sem pedido de cancelamento
                    //logo podemos trabalhar com os objetos da tela
                    ArrayList altOrcamento = BR.GetColunas();
                    clsMaqctBLL.GridMontaMaqCt(dgvMaqCt, dtMaqct, maqct_id);
                    BR.RecalculaGrid(altOrcamento);                 
                    
                    pbxMaqCt.Visible = false;                    
                    PesquisaRapidaMaqCt(); // a pesquisarapida que controla a posição do ponteiro do grid                   
                    if (dgvMaqCt.CurrentRow != null)
                    {
                        maqct_id = clsParser.Int32Parse(dgvMaqCt.CurrentRow.Cells["id"].Value.ToString());
                        bwrMaqctPreco_Run();
                    }
                    else 
                    {
                        maqct_id = 0;                        
                        bwrMaqctPreco_Run();
                    }
                }
                //carregarCliente  so recebe false agora porque o BackgroundWorker ja 
                //se completou assim ele fica liberado para nova execução
                //Nota no caso de haver pedido de cancelamento o BackgroundWorker não executa o codigo acima 
                //Nota geralmente nos nossos formularios o pedido de cancelamento veem junto com a saida do formulario
                //assim o sistema fecha e não teremos problema de BackgroundWorker em segundo plano chamando os 
                //objetos do form
                carregarMaqCt = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                //abertas so recebe false agora porque o 
                //BackgroundWorker ja se completou assim ele fica liberado para nova exexução
                carregarMaqCt = false;
            }
        }

        private void bwrMaqctPreco_Run()
        {
            if (carregarMaqCtPreco == false)
            {          
                pbxMaqCtPreco.Visible = true;            
                carregarMaqCtPreco = true;               
                
                FillFiltros();              

                bwrMaqctPreco.RunWorkerAsync();
            }
        }

        private void bwrMaqctPreco_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                MaqctPrecoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (bwrMaqctPreco.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        public void MaqctPrecoGrid()
        {
            dtMaqctPreco = clsMaqctPrecoBLL.CarregaGrid(clsInfo.conexaosqldados, maqct_id);
        }

        private void bwrMaqctPreco_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled != true)
                {
                    //o BackgroundWorker completou-se com suscesso e sem pedido de cancelamento
                    //logo podemos trabalhar com os objetos da tela
                    clsMaqctprecoBLL.GridMontaMaqCtPreco(dgvMaqCtPreco, dtMaqctPreco, maqctpreco_id);

                    pbxMaqCtPreco.Visible = false;
                    HabilitaFiltros(); //como o BackgroundWorker se completou habilitamos as oções de filtro
                   
                    //coloca o foco.  Deve ser o ultimo procedimento de foco para um objeto
                    //dentro do RunWorkerCompleted
                    rbnAtivoS.BackColor = SystemColors.Control;
                    rbnAtivoN.BackColor = SystemColors.Control;
                    rbnAtivo.BackColor = SystemColors.Control;

                    if (ultimoObjetodeFiltroFocado != null)
                    {
                        ultimoObjetodeFiltroFocado.Select();
                    }

                }
                //carregarCliente  so recebe false agora porque o BackgroundWorker ja 
                //se completou assim ele fica liberado para nova execução
                //Nota no caso de haver pedido de cancelamento o BackgroundWorker não executa o codigo acima 
                //Nota geralmente nos nossos formularios o pedido de cancelamento veem junto com a saida do formulario
                //assim o sistema fecha e não teremos problema de BackgroundWorker em segundo plano chamando os 
                //objetos do form
                carregarMaqCtPreco = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                //abertas so recebe false agora porque o 
                //BackgroundWorker ja se completou assim ele fica liberado para nova exexução
                carregarMaqCt = false;
            }
        }

        private void HabilitaFiltros()
        {
            gbxFiltro.Enabled = true;           
        }

        private void DesabilitaFiltros()
        {
            gbxFiltro.Enabled = false;       
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            if (dgvMaqCt.CurrentRow != null)
            {
                maqct_id = clsParser.Int32Parse(
                       dgvMaqCt.CurrentRow.Cells["ID"].Value.ToString());
            }
            else
            {
              maqct_id = 0;
            }
            id_Proximo = 0;
            id_Anterior = 0;
            frmMaqCt frmMaqCt = new frmMaqCt();
            frmMaqCt.Init(0, dgvMaqCt.Rows);
           
            clsFormHelper.AbrirForm(this.MdiParent, frmMaqCt, clsInfo.conexaosqldados);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvMaqCt.CurrentRow != null)
            {
                maqct_id = clsParser.Int32Parse(dgvMaqCt.CurrentRow.Cells["ID"].Value.ToString());
             
                if (dgvMaqCt.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvMaqCt.Rows[dgvMaqCt.CurrentRow.Index -
                    1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }

                if (dgvMaqCt.CurrentRow.Index < dgvMaqCt.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvMaqCt.Rows[dgvMaqCt.CurrentRow.Index + 1
                              ].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }


                frmMaqCt frmMaqCt = new frmMaqCt();
                frmMaqCt.Init((Int32)dgvMaqCt.CurrentRow.Cells[0].Value, dgvMaqCt.Rows);

                clsFormHelper.AbrirForm(this.MdiParent, frmMaqCt, clsInfo.conexaosqldados);
            }
            else
            {
                MessageBox.Show("Primeiro selecione um Registro!", "Aplisoft", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                dgvMaqCt.Select();
            }

        }
        
        private void tspImprimir_Click(object sender, EventArgs e)
        {
            BR.Imprimir(clsInfo.caminhorelatorios, "Máquinas", clsInfo.conexaosqldados);
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvMaqCt.CurrentRow;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Close();
            }
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMaqCt.CurrentRow != null)
                {
                    if (MessageBox.Show("Deseja realmente 'Excluir' o registro: " + dgvMaqCt.CurrentRow.Cells[2].Value + "?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsMaqctBLL.Excluir(clsParser.Int32Parse(dgvMaqCt.CurrentRow.Cells["id"].Value.ToString()), clsInfo.conexaosqldados);
                        dgvMaqCt.Rows.Remove(dgvMaqCt.CurrentRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            carregarMaqCt = true;
            bwrMaqct.CancelAsync();
            bwrMaqct.Dispose();
            bwrMaqctPreco.CancelAsync();
            bwrMaqctPreco.Dispose();
            this.Close();
            this.Dispose();
        }

        private void dgvIndices_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                tspAlterar.PerformClick();
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

   
        private void dgvIndices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void frmIndicesVis_Activated(object sender, EventArgs e)
        {
            bwrMaqct_Run();
        }
        
        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvMaqCt);
        }

        private void dgvMaqCt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMaqCt.CurrentRow != null)
            {
                maqct_id = clsParser.Int32Parse(dgvMaqCt.CurrentRow.Cells["id"].Value.ToString());
                bwrMaqctPreco_Run();
            }
        }
        public Boolean MaqMovimenta()
        {
            //clsClienteInfo = new clsClienteInfo();
            //ClienteInfo(clsClienteInfo);

            //if (clsClienteBLL.Equals(clsClienteInfoOld, clsClienteInfo) == false)
            //{
            //    DialogResult drt;
            //    drt = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //    if (drt == DialogResult.Yes)
            //    {
            //        ClienteSalvar();
            //    }
            //    else if (drt == DialogResult.Cancel)
            //    {
            //        tbxCognome.Select();
            //        tbxCognome.SelectAll();
            //        return false;
            //    }
            //}

            return true;
        }

        private void PesquisaRapidaMaqCt()
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvMaqCt);
            
            if (maqct_id > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(maqct_id, dgvMaqCt, "CODIGO") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo,
                                   dgvMaqCt, "CODIGO") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior,
                                dgvMaqCt, "CODIGO") == false)
                        {
                            if (dgvMaqCt.Rows.Count > 0)
                            {
                                dgvMaqCt.CurrentCell = dgvMaqCt.Rows[0].Cells["CODIGO"];
                            }
                        }
                    }
                }
            }
            else if (dgvMaqCt.Rows.Count > 0)
            {
                dgvMaqCt.CurrentCell = dgvMaqCt.Rows[0].Cells["CODIGO"];
            }

            if (dgvMaqCt.CurrentRow != null)
            {
                maqct_id = clsParser.Int32Parse(
                dgvMaqCt.CurrentRow.Cells["CODIGO"].Value.ToString());
                if (dgvMaqCt.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvMaqCt.Rows[
           dgvMaqCt.CurrentRow.Index - 1].Cells["CODIGO"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvMaqCt.CurrentRow.Index < dgvMaqCt.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvMaqCt.Rows[
         dgvMaqCt.CurrentRow.Index + 1].Cells["CODIGO"].Value.ToString());
                }
                else
                {
                    id_Proximo = 0;
                }
            }
            else
            {
                maqct_id = 0;
                id_Anterior = 0;
                id_Proximo = 0;
            }
        }
    }
}
