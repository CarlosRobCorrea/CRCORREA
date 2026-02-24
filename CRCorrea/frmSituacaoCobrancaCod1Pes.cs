using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmSituacaoCobrancaCod1Pes : Form
    {
        
        
        
        
        clsBasicReport clsBr;
        BackgroundWorker bwrSituacaoCobrancaCod1;
        Int32 id;
        Int32 idSituacaoCobrancaCod;
        static DataTable dtSituacaoCobrancaCod1;
        Boolean carregandoSituacao;
        clsSituacaocobrancacod1BLL clsSituacaocobrancacod1BLL;

        public frmSituacaoCobrancaCod1Pes()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         Int32 _idSituacaoCobrancaCod)
        {
            carregandoSituacao = false;
            clsSituacaocobrancacod1BLL = new clsSituacaocobrancacod1BLL();
            clsInfo.zrowid = 0;
            clsBr = new clsBasicReport(this, dgvSituacao , toolTip1);
            id = _id;
            idSituacaoCobrancaCod = _idSituacaoCobrancaCod;
        }

        private void frmSituacaoCobrancaCod1Pes_Activated(object sender, EventArgs e)
        {
            bwrSituacaoCobrancaCod1_Run();
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBr.Imprimir(clsInfo.caminhorelatorios, "Cadastro de Historicos de Cobrança", clsInfo.conexaosqldados);
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void bwrSituacaoCobrancaCod1_Run()
        {
            if (carregandoSituacao == false)
            {
                carregandoSituacao = true;
                bwrSituacaoCobrancaCod1 = new BackgroundWorker();
                bwrSituacaoCobrancaCod1.DoWork += new DoWorkEventHandler(bwrSituacaoCobrancaCod1_DoWork);
                bwrSituacaoCobrancaCod1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrSituacaoCobrancaCod1_RunWorkerCompleted);
                bwrSituacaoCobrancaCod1.RunWorkerAsync();
            }
        }

        private void bwrSituacaoCobrancaCod1_DoWork(object sender, DoWorkEventArgs e)
        {
            dtSituacaoCobrancaCod1 = clsSituacaocobrancacod1BLL.GridDados(idSituacaoCobrancaCod, clsInfo.conexaosqldados);    
        }

        private void bwrSituacaoCobrancaCod1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                clsSituacaocobrancacod1BLL.GridMonta(dgvSituacao, dtSituacaoCobrancaCod1, clsInfo.zrowid);
                clsGridHelper.SelecionaLinha(id, dgvSituacao, 1);
                carregandoSituacao = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandoSituacao = false;
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

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            clsInfo.zrow = dgvSituacao.CurrentRow;
            this.Close();
        }
    }
}
