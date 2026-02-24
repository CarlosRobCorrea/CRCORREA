using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmUsuarioPes : Form
    {
        Int32 idcliente;
        String conexao;

        DataTable dtUsuario;

        clsUsuarioBLL  clsUsuarioBLL;

        Boolean Carregando;

        clsBasicReport clsBR;
        

        public frmUsuarioPes()
        {
            InitializeComponent();
        }

        public void Init(Int32 _idCliente,
                         String _conexao)
        {
            idcliente = _idCliente;
            conexao = _conexao;

            Carregando = false;

            cbxStatus.SelectedIndex = 1;

            clsUsuarioBLL =  new clsUsuarioBLL();

            clsBR = new clsBasicReport(this, dgvUsuario, conexao);
        }

        private void frmUsuarioPes_Load(object sender, EventArgs e)
        {
            CarregaGridUsuario();
        }

        private void CarregaGridUsuario()
        {
            if (Carregando == false)
            {
                Carregando = true;
                dgvUsuario.DataSource = null;

                dtUsuario = clsUsuarioBLL.GridDados(cbxStatus.Text.Substring(0, 1), conexao);
                clsUsuarioBLL.GridMonta(dgvUsuario, dtUsuario, 0);

                Carregando = false;
            }
        }

        private void frmUsuarioPes_Activated(object sender, EventArgs e)
        {
            CarregaGridUsuario();
        }

        private void tstbxProcurar_Click(object sender, EventArgs e)
        {
             clsGridHelper.Filtrar(tstbxProcurar.Text, dgvUsuario);
        }

        private void tsbRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            clsBR.Imprimir(clsInfo.caminhorelatorios, "Usuário", conexao);
        }

        private void tsbEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvUsuario.CurrentRow;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Close();
                this.Dispose();
            }
        }

        void Filtrar()
        {
            clsGridHelper.Filtrar(tstbxProcurar.Text, dgvUsuario);
        }

        private void tstbxProcurar_KeyUp(object sender, KeyEventArgs e)
        {
            Filtrar();
        }

        private void cbxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridUsuario();
        }

        private void dgvUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvUsuario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //tsbAlterar.PerformClick();
        }

    }
}
