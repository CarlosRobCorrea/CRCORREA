using CRCorreaFuncoes;
using System;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{ 
    public partial class frmGeralVis : Form
    {
        
        delegate void AddnewDelegate(TextBox textbox, TextBox colecao);

        String conexao;
        String tabela;
        String campos;
        String where;
        String ordem;
        GridColuna[] gridcoluna;
        Boolean cor;
        Int32 tamanhoFonte;
        String[] formataCampos;

        DataTable dtGeral = new DataTable();

        public frmGeralVis()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         String _tabela,
                         String _campos,
                         String _where,
                         String _ordem,
                         GridColuna[] _gridcoluna,
                         Boolean _cor,
                         Int32 _tamanhoFonte,
                         String[] _formataCampos)
        {
            conexao = _conexao;
            tabela = _tabela;
            campos = _campos;
            where = _where;
            ordem = _ordem;
            gridcoluna = _gridcoluna;  
            cor= _cor;
            tamanhoFonte =_tamanhoFonte;
            formataCampos = _formataCampos;
        }

        private void frmColetivoLinhaVis_Load(object sender, EventArgs e)
        {
            dtGeral = clsSelectInsertUpdateBLL.Select(conexao, tabela, campos, where, ordem);
            dgvGeral.DataSource = dtGeral;
            clsGridHelper.MontaGrid(dgvGeral, gridcoluna, cor);

            if (formataCampos!=null)
            {
                foreach (String s in formataCampos)
                {
                    dgvGeral.Columns[s.Split('|').GetValue(0).ToString()].DefaultCellStyle.Format = s.Split('|').GetValue(1).ToString();
                }
            }
            pbxGeralVis.Visible = false;
            clsGridHelper.FontGrid(dgvGeral, tamanhoFonte);
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            clsInfo.zrow = null;
            this.Close();
            this.Dispose();
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                clsInfo.zrow = dgvGeral.CurrentRow;
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

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvGeral);
        }

        
        private void dgvGeral_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspEscolher.PerformClick();
        }
    }
}
