using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmTab_Bancos : Form
    {
        String conexao;
        Int32 id;

        clsTab_BancosBLL clsTab_BancosBLL = new clsTab_BancosBLL();
        clsTab_BancosInfo clsTab_BancosInfo = new clsTab_BancosInfo();
        clsTab_BancosInfo clsTab_BancosInfoOld = new clsTab_BancosInfo();

        DataGridViewRowCollection dgvrcnTab_Bancos;

        BackgroundWorker bwrTab_BancosEspecie;
        Boolean carregandoTab_BancosEspecie;
        DataTable dtTab_BancosEspecie;
        Int32 indexTab_BancosEspecieValor;

        BackgroundWorker bwrTab_BancosMoeda;
        Boolean carregandoTab_BancosMoeda;
        DataTable dtTab_BancosMoeda;
        Int32 indexTab_BancosMoedaValor;

        Boolean carregandoTab_BancosAceite;
        DataTable dtTab_BancosAceite;
        Int32 indexTab_BancosAceiteValor;

        //BackgroundWorker bwrTab_BancosEstado;
        Boolean carregandoTab_BancosEstado;
        DataTable dtTab_BancosEstado;
        Int32 indexTab_BancosEstadoValor;

        //BackgroundWorker bwrTab_BancosProtestar;
        Boolean carregandoTab_BancosProtestar;
        DataTable dtTab_BancosProtestar;
        Int32 indexTab_BancosProtestarValor;

        //BackgroundWorker bwrTab_BancosImpressao;
        Boolean carregandoTab_BancosImpressao;
        DataTable dtTab_BancosImpressao;
        Int32 indexTab_BancosImpressaoValor;

        //BackgroundWorker bwrTab_BancosModalidade;
        Boolean carregandoTab_BancosModalidade;
        DataTable dtTab_BancosModalidade;
        Int32 indexTab_BancosModalidadeValor;

        public frmTab_Bancos()
        {
            InitializeComponent();
        }

        public void Init(String _conexao,
                         Int32 _id,
                         DataGridViewRowCollection _dgvrcnTab_Bancos)
        {
            conexao = _conexao;
            id = _id;
            dgvrcnTab_Bancos= _dgvrcnTab_Bancos;

            bwrTab_BancosEspecie = new BackgroundWorker();
            //apenas para permitir a funcionalidade de cancelamento
            bwrTab_BancosEspecie.WorkerSupportsCancellation = true;
            bwrTab_BancosEspecie.DoWork += new DoWorkEventHandler(bwrTab_BancosEspecie_DoWork);
            bwrTab_BancosEspecie.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrTab_BancosEspecie_RunWorkerCompleted);
            //aqui inicializamos a variavel carregarCliente com false 
            carregandoTab_BancosEspecie = false;

            bwrTab_BancosMoeda = new BackgroundWorker();
            //apenas para permitir a funcionalidade de cancelamento
            bwrTab_BancosMoeda.WorkerSupportsCancellation = true;
            bwrTab_BancosMoeda.DoWork += new DoWorkEventHandler(bwrTab_BancosMoeda_DoWork);
            bwrTab_BancosMoeda.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrTab_BancosMoeda_RunWorkerCompleted);
            //aqui inicializamos a variavel carregarCliente com false 
            carregandoTab_BancosMoeda = false;

            bwrTab_BancosAceite = new BackgroundWorker();
            //apenas para permitir a funcionalidade de cancelamento
            bwrTab_BancosAceite.WorkerSupportsCancellation = true;
            bwrTab_BancosAceite.DoWork += new DoWorkEventHandler(bwrTab_BancosAceite_DoWork);
            bwrTab_BancosAceite.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrTab_BancosAceite_RunWorkerCompleted);
            //aqui inicializamos a variavel carregarCliente com false 
            carregandoTab_BancosAceite = false;
            //para o BackgroundWorker poder rodar na primeira chamada
            //clsDanfe310BLL = new clsDanfe310BLL();

            bwrTab_BancosEstado = new BackgroundWorker();
            //apenas para permitir a funcionalidade de cancelamento
            bwrTab_BancosEstado.WorkerSupportsCancellation = true;
            bwrTab_BancosEstado.DoWork += new DoWorkEventHandler(bwrTab_BancosEstado_DoWork);
            bwrTab_BancosEstado.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrTab_BancosEstado_RunWorkerCompleted);
            carregandoTab_BancosEstado = false;

            bwrTab_BancosProtestar = new BackgroundWorker();
            //apenas para permitir a funcionalidade de cancelamento
            bwrTab_BancosProtestar.WorkerSupportsCancellation = true;
            bwrTab_BancosProtestar.DoWork += new DoWorkEventHandler(bwrTab_BancosProtestar_DoWork);
            bwrTab_BancosProtestar.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrTab_BancosProtestar_RunWorkerCompleted);
            carregandoTab_BancosProtestar = false;

            bwrTab_BancosImpressao = new BackgroundWorker();
            //apenas para permitir a funcionalidade de cancelamento
            bwrTab_BancosImpressao.WorkerSupportsCancellation = true;
            bwrTab_BancosImpressao.DoWork += new DoWorkEventHandler(bwrTab_BancosImpressao_DoWork);
            bwrTab_BancosImpressao.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrTab_BancosImpressao_RunWorkerCompleted);
            carregandoTab_BancosImpressao = false;

            bwrTab_BancosModalidade = new BackgroundWorker();
            //apenas para permitir a funcionalidade de cancelamento
            bwrTab_BancosModalidade.WorkerSupportsCancellation = true;
            bwrTab_BancosModalidade.DoWork += new DoWorkEventHandler(bwrTab_BancosModalidade_DoWork);
            bwrTab_BancosModalidade.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrTab_BancosModalidade_RunWorkerCompleted);
            carregandoTab_BancosModalidade = false;


        }

        private void frmTab_Bancos_Load(object sender, EventArgs e)
        {
            CarregaTab_Bancos(id);
            bwrTab_BancosEspecie_Run();
            bwrTab_BancosMoeda_Run();
            bwrTab_BancosAceite_Run();
            bwrTab_BancosEstado_Run();
            bwrTab_BancosProtestar_Run();
            bwrTab_BancosImpressao_Run();
            bwrTab_BancosModalidade_Run();
        }

        private void CarregaTab_Bancos(Int32 _id)
        {
            clsInfo.zidincluir = id;

            clsTab_BancosInfoOld = new clsTab_BancosInfo();
            if (_id > 0)    // Alterando / Visualizando
            {
                clsTab_BancosBLL clsTab_BancosBLL = new clsTab_BancosBLL();
                clsTab_BancosInfo clsTab_BancosInfo = new clsTab_BancosInfo();

                // Carrega os Dados
                clsTab_BancosInfo = clsTab_BancosBLL.Carregar(_id, conexao);
                PreencheCamposTab_Bancos(clsTab_BancosInfo);

                PreencheInfoTab_Bancos(clsTab_BancosInfoOld);
            }
            else
            {
               // tspPrimeiro.Enabled = false;
               // tspAnterior.Enabled = false;
              //  tspProximo.Enabled = false;
              //  tspUltimo.Enabled = false;
            }
            tbxCodigo.Select();
            tbxCodigo.SelectAll();
        }

        private void PreencheCamposTab_Bancos(clsTab_BancosInfo _info)
        {
            id = _info.id;
            tbxCodigo.Text = _info.codigo.ToString();
            tbxCognome.Text = _info.cognome.ToString();
            tbxNome.Text = _info.nome;
            if (_info.ativo == "S")
            {
                rbnAtivo.Checked = true;
            }
            else
            {
                rbnInativo.Checked = true;
            }
            tbxHomePage.Text = _info.homepage;
            tbxCogCnab.Text = _info.cogcnab;

        }

        private void PreencheInfoTab_Bancos(clsTab_BancosInfo _info)
        {
            

            _info.id = id; 
            _info.codigo = clsParser.Int32Parse(tbxCodigo.Text);
            _info.cognome = tbxCognome.Text;
            _info.nome = tbxNome.Text;
            if (rbnAtivo.Checked == true)
            {
                _info.ativo = "S";
            }
            else
            {
                if (_info.ativo != null)
                {
                    _info.ativo = "N";
                }
                else
                {
                    _info.ativo = "";
                }
            }
            _info.homepage = tbxHomePage.Text;
            _info.cogcnab = tbxCogCnab.Text;
        
        }

        private void CamposTab_Bancos(object _ctrl)
        {
            if (_ctrl is TextBox)
            {
            }
        }

        private void ControlEnter(object sender, EventArgs e)
        {

            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            CamposTab_Bancos(sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void frmTab_Bancos_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }


        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar ?", "Aplisoft",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);

                if (resultado == DialogResult.Yes)
                {
                    SalvarTab_Bancos();
                }
                else if (resultado == DialogResult.Cancel)
                {
                    tbxCodigo.Select();
                    tbxCodigo.SelectAll();
                    return;
                }
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tbxCodigo.Select();
                tbxCodigo.SelectAll();
            }

        }


        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (MovimentaTab_Bancos())
                {
                    id = Int32.Parse(dgvrcnTab_Bancos[0].Cells[0].Value.ToString());
                    CarregaTab_Bancos(id);
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
                if (MovimentaTab_Bancos())
                {
                    foreach (DataGridViewRow dgvr in dgvrcnTab_Bancos)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index != 0)
                            {
                                id = Int32.Parse(dgvrcnTab_Bancos[dgvrcnTab_Bancos.GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaTab_Bancos(id);
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
                if (MovimentaTab_Bancos())
                {
                    foreach (DataGridViewRow dgvr in dgvrcnTab_Bancos)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index < dgvrcnTab_Bancos.Count - 1)
                            {
                                id = Int32.Parse(dgvrcnTab_Bancos[dgvrcnTab_Bancos.GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaTab_Bancos(id);
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
                if (MovimentaTab_Bancos())
                {
                    id = Int32.Parse(dgvrcnTab_Bancos[dgvrcnTab_Bancos.Count - 1].Cells[0].Value.ToString());
                    CarregaTab_Bancos(id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                clsTab_BancosBLL clsTab_BancosBLL = new clsTab_BancosBLL();
                clsTab_BancosInfo clsTab_BancosInfo = new clsTab_BancosInfo();

                PreencheInfoTab_Bancos(clsTab_BancosInfo);


                if (clsTab_BancosBLL.Equals(clsTab_BancosInfoOld, clsTab_BancosInfo) == false)
                {
                    DialogResult resultado;
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (resultado == DialogResult.Yes)
                    {
                        SalvarTab_Bancos();
                    }
                    else if (resultado == DialogResult.Cancel)
                    {
                        tbxCodigo.Select();
                        tbxCodigo.SelectAll();
                        return;
                    }
                }
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tbxCodigo.Select();
                tbxCodigo.SelectAll();
            }
            this.Close();
            this.Dispose();
        }


        public Boolean MovimentaTab_Bancos()
        {
            clsTab_BancosBLL = new clsTab_BancosBLL();
            clsTab_BancosInfo = new clsTab_BancosInfo();
            PreencheInfoTab_Bancos(clsTab_BancosInfo);
            //if (clsTab_BancosBLL.ComparaInfo(clsTab_BancosInfoOld, clsTab_BancosInfo) == false || bnAlunoCurso == true)
            if (clsTab_BancosBLL.Equals(clsTab_BancosInfoOld, clsTab_BancosInfo) == false)
            {
                DialogResult drt;
                drt = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    SalvarTab_Bancos();
                }
                else if (drt == DialogResult.Cancel)
                {
                    tbxCodigo.Select();
                    tbxCodigo.SelectAll();
                    return false;
                }
            }
            return true;
        }
        private void SalvarTab_Bancos()
        {
            clsTab_BancosInfo clsTab_BancosInfo = new clsTab_BancosInfo();
            clsTab_BancosBLL clsTab_BancosBLL = new clsTab_BancosBLL();

            if (id == 0)    // Incluindo
            {
                PreencheInfoTab_Bancos(clsTab_BancosInfo);
                id = clsTab_BancosBLL.Incluir(clsTab_BancosInfo, conexao);
                clsInfo.zidincluir = id;
            }
            else
            {
                PreencheInfoTab_Bancos(clsTab_BancosInfo);
                // Verifica se algo foi alterado, se foi, Salva
                if (clsTab_BancosBLL.Equals(clsTab_BancosInfoOld, clsTab_BancosInfo) == false)
                {
                    clsTab_BancosBLL.Alterar(clsTab_BancosInfo, conexao);
                }
            }
        }
        ////////////////////////////////////////////////////
        // grids  =  Especie
        private void bwrTab_BancosEspecie_Run()
        {
            if (carregandoTab_BancosEspecie == false)
            {
                //pbxOrcamento.Visible = true;
                carregandoTab_BancosEspecie = true;
                bwrTab_BancosEspecie.RunWorkerAsync(); //inicia a execução do BackgroundWorker em segundo plano
            }
        }

        private void bwrTab_BancosEspecie_DoWork(object sender, DoWorkEventArgs e)
        {
            dtTab_BancosEspecie = CarregaGridTab_BancosEspecieGrid();
            if (bwrTab_BancosEspecie.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }
        public DataTable CarregaGridTab_BancosEspecieGrid()
        {
            clsFormIdBLL clsFormIdBLL = new clsFormIdBLL("TAB_BANCOSESPECIE");
            try
            {
                return clsFormIdBLL.GridDados(id, conexao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        private void bwrTab_BancosEspecie_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvTab_BancosEspecie.DataSource = dtTab_BancosEspecie;
            if (dtTab_BancosEspecie.Rows.Count > 0)
            {
                clsGridHelper.MontaGrid(dgvTab_BancosEspecie,
                    new String[] { "Id", "iDCodigo", "Codigo", "Descrição" },
                    new String[] { "ID", "IDCODIGO", "CODIGO", "NOME" },
                    new int[] { 1, 1, 40, 400 },
                    new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft},
                    new bool[] { false, true, true, true },
                    true, 1, ListSortDirection.Ascending);

                if (indexTab_BancosEspecieValor > 0)
                {
                    foreach (DataGridViewRow linha in dgvTab_BancosEspecie.Rows)
                    {
                        foreach (DataGridViewCell celula in dgvTab_BancosEspecie.Rows[linha.Index].Cells)
                        {
                            if (dgvTab_BancosEspecie.Columns[celula.ColumnIndex].Name == "ID")
                            {
                                {
                                    if (dgvTab_BancosEspecie.Rows[linha.Index].Cells["ID"].Value.ToString().ToLower() == indexTab_BancosEspecieValor.ToString().ToLower())
                                    {
                                        dgvTab_BancosEspecie.FirstDisplayedCell = dgvTab_BancosEspecie.Rows[celula.RowIndex].Cells[1];
                                        dgvTab_BancosEspecie[1, celula.RowIndex].Selected = true;
                                        dgvTab_BancosEspecie.Select();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        // grids  =  Moeda
        private void bwrTab_BancosMoeda_Run()
        {
            if (carregandoTab_BancosMoeda == false)
            {
                //pbxOrcamento.Visible = true;
                carregandoTab_BancosMoeda = true;
                bwrTab_BancosMoeda.RunWorkerAsync(); //inicia a execução do BackgroundWorker em segundo plano
            }
        }

        private void bwrTab_BancosMoeda_DoWork(object sender, DoWorkEventArgs e)
        {
            dtTab_BancosMoeda = CarregaGridTab_BancosMoedaGrid();
            if (bwrTab_BancosMoeda.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }
        public DataTable CarregaGridTab_BancosMoedaGrid()
        {
            clsFormIdBLL clsFormIdBLL = new clsFormIdBLL("TAB_BANCOSMOEDA");
            try
            {
                return clsFormIdBLL.GridDados(id, conexao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrTab_BancosMoeda_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvTab_BancosMoeda.DataSource = dtTab_BancosMoeda;
            if (dtTab_BancosMoeda.Rows.Count > 0)
            {
                clsGridHelper.MontaGrid(dgvTab_BancosMoeda,
                    new String[] { "Id", "IdCodigo", "Codigo", "Descrição" },
                    new String[] { "ID", "IDCODIGO", "CODIGO", "NOME" },
                    new int[] { 1, 1, 40, 400 },
                    new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft},
                    new bool[] { false, true, true, true },
                    true, 1, ListSortDirection.Ascending);

                if (indexTab_BancosMoedaValor > 0)
                {
                    foreach (DataGridViewRow linha in dgvTab_BancosMoeda.Rows)
                    {
                        foreach (DataGridViewCell celula in dgvTab_BancosMoeda.Rows[linha.Index].Cells)
                        {
                            if (dgvTab_BancosMoeda.Columns[celula.ColumnIndex].Name == "ID")
                            {
                                {
                                    if (dgvTab_BancosMoeda.Rows[linha.Index].Cells["ID"].Value.ToString().ToLower() == indexTab_BancosMoedaValor.ToString().ToLower())
                                    {
                                        dgvTab_BancosMoeda.FirstDisplayedCell = dgvTab_BancosMoeda.Rows[celula.RowIndex].Cells[1];
                                        dgvTab_BancosMoeda[1, celula.RowIndex].Selected = true;
                                        dgvTab_BancosMoeda.Select();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        // grids  =  Aceite
        private void bwrTab_BancosAceite_Run()
        {
            if (carregandoTab_BancosAceite == false)
            {
                //pbxOrcamento.Visible = true;
                carregandoTab_BancosAceite = true;
                bwrTab_BancosAceite.RunWorkerAsync(); //inicia a execução do BackgroundWorker em segundo plano
            }
        }

        private void bwrTab_BancosAceite_DoWork(object sender, DoWorkEventArgs e)
        {
            dtTab_BancosAceite = CarregaGridTab_BancosAceiteGrid();
            if (bwrTab_BancosAceite.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }
        public DataTable CarregaGridTab_BancosAceiteGrid()
        {
            clsFormIdBLL clsFormIdBLL = new clsFormIdBLL("TAB_BANCOSACEITE");
            try
            {
                return clsFormIdBLL.GridDados(id, conexao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        private void bwrTab_BancosAceite_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvTab_BancosAceite.DataSource = dtTab_BancosAceite;
            if (dtTab_BancosAceite.Rows.Count > 0)
            {

                clsGridHelper.MontaGrid(dgvTab_BancosAceite,
                    new String[] { "Id", "IdCodigo", "Codigo", "Descrição" },
                    new String[] { "ID", "IDCODIGO", "CODIGO", "NOME" },
                    new int[] { 1, 1, 40, 400 },
                    new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft},
                    new bool[] { false, true, true, true },
                    true, 1, ListSortDirection.Ascending);

                if (indexTab_BancosAceiteValor > 0)
                {
                    foreach (DataGridViewRow linha in dgvTab_BancosAceite.Rows)
                    {
                        foreach (DataGridViewCell celula in dgvTab_BancosAceite.Rows[linha.Index].Cells)
                        {
                            if (dgvTab_BancosAceite.Columns[celula.ColumnIndex].Name == "ID")
                            {
                                {
                                    if (dgvTab_BancosAceite.Rows[linha.Index].Cells["ID"].Value.ToString().ToLower() == indexTab_BancosAceiteValor.ToString().ToLower())
                                    {
                                        dgvTab_BancosAceite.FirstDisplayedCell = dgvTab_BancosAceite.Rows[celula.RowIndex].Cells[1];
                                        dgvTab_BancosAceite[1, celula.RowIndex].Selected = true;
                                        dgvTab_BancosAceite.Select();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        // grids  =  Estado
        private void bwrTab_BancosEstado_Run()
        {
            if (carregandoTab_BancosEstado == false)
            {
                //pbxOrcamento.Visible = true;
                carregandoTab_BancosEstado = true;
                bwrTab_BancosEstado.RunWorkerAsync(); //inicia a execução do BackgroundWorker em segundo plano
            }
        }

        private void bwrTab_BancosEstado_DoWork(object sender, DoWorkEventArgs e)
        {
            dtTab_BancosEstado = CarregaGridTab_BancosEstadoGrid();
            if (bwrTab_BancosEstado.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        public DataTable CarregaGridTab_BancosEstadoGrid()
        {
            clsFormIdBLL clsFormIdBLL = new clsFormIdBLL("TAB_BANCOSESTADO");
            try
            {
                return clsFormIdBLL.GridDados(id, conexao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrTab_BancosEstado_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvTab_BancosEstado.DataSource = dtTab_BancosEstado;
            if (dtTab_BancosEstado.Rows.Count > 0)
            {

                clsGridHelper.MontaGrid(dgvTab_BancosEstado,
                    new String[] { "Id", "idCodigo", "Codigo", "Descrição" },
                    new String[] { "ID", "IDCODIGO", "CODIGO", "NOME" },
                    new int[] { 1, 1, 40, 400 },
                    new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft},
                    new bool[] { false, true, true, true },
                    true, 1, ListSortDirection.Ascending);

                if (indexTab_BancosEstadoValor > 0)
                {
                    foreach (DataGridViewRow linha in dgvTab_BancosEstado.Rows)
                    {
                        foreach (DataGridViewCell celula in dgvTab_BancosEstado.Rows[linha.Index].Cells)
                        {
                            if (dgvTab_BancosEstado.Columns[celula.ColumnIndex].Name == "ID")
                            {
                                {
                                    if (dgvTab_BancosEstado.Rows[linha.Index].Cells["ID"].Value.ToString().ToLower() == indexTab_BancosEstadoValor.ToString().ToLower())
                                    {
                                        dgvTab_BancosEstado.FirstDisplayedCell = dgvTab_BancosEstado.Rows[celula.RowIndex].Cells[1];
                                        dgvTab_BancosEstado[1, celula.RowIndex].Selected = true;
                                        dgvTab_BancosEstado.Select();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        // grids  =  Protestar
        private void bwrTab_BancosProtestar_Run()
        {
            if (carregandoTab_BancosProtestar == false)
            {
                //pbxOrcamento.Visible = true;
                carregandoTab_BancosProtestar = true;
                bwrTab_BancosProtestar.RunWorkerAsync(); //inicia a execução do BackgroundWorker em segundo plano
            }
        }

        private void bwrTab_BancosProtestar_DoWork(object sender, DoWorkEventArgs e)
        {
            dtTab_BancosProtestar = CarregaGridTab_BancosProtestarGrid();
            if (bwrTab_BancosProtestar.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }
        public DataTable CarregaGridTab_BancosProtestarGrid()
        {
            clsFormIdBLL clsFormIdBLL = new clsFormIdBLL("TAB_BANCOSPROTESTAR");
            try
            {
                return clsFormIdBLL.GridDados(id, conexao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        private void bwrTab_BancosProtestar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvTab_BancosProtestar.DataSource = dtTab_BancosProtestar;
            if (dtTab_BancosProtestar.Rows.Count > 0)
            {

                clsGridHelper.MontaGrid(dgvTab_BancosProtestar,
                    new String[] { "Id", "idCodigo", "Codigo", "Descrição" },
                    new String[] { "ID", "IDCODIGO", "CODIGO", "NOME" },
                    new int[] { 1, 1, 40, 400 },
                    new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleCenter,
                            DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft},
                    new bool[] { false, true, true, true },
                    true, 1, ListSortDirection.Ascending);

                if (indexTab_BancosProtestarValor > 0)
                {
                    foreach (DataGridViewRow linha in dgvTab_BancosProtestar.Rows)
                    {
                        foreach (DataGridViewCell celula in dgvTab_BancosProtestar.Rows[linha.Index].Cells)
                        {
                            if (dgvTab_BancosProtestar.Columns[celula.ColumnIndex].Name == "ID")
                            {
                                {
                                    if (dgvTab_BancosProtestar.Rows[linha.Index].Cells["ID"].Value.ToString().ToLower() == indexTab_BancosProtestarValor.ToString().ToLower())
                                    {
                                        dgvTab_BancosProtestar.FirstDisplayedCell = dgvTab_BancosProtestar.Rows[celula.RowIndex].Cells[1];
                                        dgvTab_BancosProtestar[1, celula.RowIndex].Selected = true;
                                        dgvTab_BancosProtestar.Select();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // grids  =  Impressao
        private void bwrTab_BancosImpressao_Run()
        {
            if (carregandoTab_BancosImpressao == false)
            {
                //pbxOrcamento.Visible = true;
                carregandoTab_BancosImpressao = true;
                bwrTab_BancosImpressao.RunWorkerAsync(); //inicia a execução do BackgroundWorker em segundo plano
            }
        }

        private void bwrTab_BancosImpressao_DoWork(object sender, DoWorkEventArgs e)
        {
            dtTab_BancosImpressao = CarregaGridTab_BancosImpressaoGrid();
            if (bwrTab_BancosImpressao.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }
        public DataTable CarregaGridTab_BancosImpressaoGrid()
        {
            clsFormIdBLL clsFormIdBLL = new clsFormIdBLL("TAB_BANCOSIMPRESSAO");
            try
            {
                return clsFormIdBLL.GridDados(id, conexao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void bwrTab_BancosImpressao_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvTab_BancosImpressao.DataSource = dtTab_BancosImpressao;
            if (dtTab_BancosImpressao.Rows.Count > 0)
            {
                clsGridHelper.MontaGrid(dgvTab_BancosImpressao,
                    new String[] { "Id", "IdCodigo", "Codigo", "Descrição" },
                    new String[] { "ID", "IDCODIGO", "CODIGO", "NOME" },
                    new int[] { 1, 1, 40, 400 },
                    new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft},
                    new bool[] { false, true, true, true },
                    true, 1, ListSortDirection.Ascending);

                if (indexTab_BancosImpressaoValor > 0)
                {
                    foreach (DataGridViewRow linha in dgvTab_BancosImpressao.Rows)
                    {
                        foreach (DataGridViewCell celula in dgvTab_BancosImpressao.Rows[linha.Index].Cells)
                        {
                            if (dgvTab_BancosImpressao.Columns[celula.ColumnIndex].Name == "ID")
                            {
                                {
                                    if (dgvTab_BancosImpressao.Rows[linha.Index].Cells["ID"].Value.ToString().ToLower() == indexTab_BancosImpressaoValor.ToString().ToLower())
                                    {
                                        dgvTab_BancosImpressao.FirstDisplayedCell = dgvTab_BancosImpressao.Rows[celula.RowIndex].Cells[1];
                                        dgvTab_BancosImpressao[1, celula.RowIndex].Selected = true;
                                        dgvTab_BancosImpressao.Select();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        // grids  =  Modalidade
        private void bwrTab_BancosModalidade_Run()
        {
            if (carregandoTab_BancosModalidade == false)
            {
                //pbxOrcamento.Visible = true;
                carregandoTab_BancosModalidade = true;
                bwrTab_BancosModalidade.RunWorkerAsync(); //inicia a execução do BackgroundWorker em segundo plano
            }
        }

        private void bwrTab_BancosModalidade_DoWork(object sender, DoWorkEventArgs e)
        {
            dtTab_BancosModalidade = CarregaGridTab_BancosModalidadeGrid();
            if (bwrTab_BancosModalidade.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        public DataTable CarregaGridTab_BancosModalidadeGrid()
        {
            clsFormIdBLL clsFormIdBLL = new clsFormIdBLL("TAB_BANCOSMODALIDADE");
            try
            {
                return clsFormIdBLL.GridDados(id, conexao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            } 
        }

        private void bwrTab_BancosModalidade_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvTab_BancosModalidade.DataSource = dtTab_BancosModalidade;
            if (dtTab_BancosModalidade.Rows.Count > 0)
            {

                clsGridHelper.MontaGrid(dgvTab_BancosModalidade,
                    new String[] { "Id", "IdCodigo", "Codigo", "Descrição" },
                    new String[] { "ID", "IDCODIGO", "CODIGO", "NOME" },
                    new int[] { 1, 1, 40, 400 },
                    new DataGridViewContentAlignment[]
                        {DataGridViewContentAlignment.MiddleCenter,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft,
                         DataGridViewContentAlignment.MiddleLeft},
                    new bool[] { false, true, true, true },
                    true, 1, ListSortDirection.Ascending);

                if (indexTab_BancosModalidadeValor > 0)
                {
                    foreach (DataGridViewRow linha in dgvTab_BancosModalidade.Rows)
                    {
                        foreach (DataGridViewCell celula in dgvTab_BancosModalidade.Rows[linha.Index].Cells)
                        {
                            if (dgvTab_BancosModalidade.Columns[celula.ColumnIndex].Name == "ID")
                            {
                                {
                                    if (dgvTab_BancosModalidade.Rows[linha.Index].Cells["ID"].Value.ToString().ToLower() == indexTab_BancosModalidadeValor.ToString().ToLower())
                                    {
                                        dgvTab_BancosModalidade.FirstDisplayedCell = dgvTab_BancosModalidade.Rows[celula.RowIndex].Cells[1];
                                        dgvTab_BancosModalidade[1, celula.RowIndex].Selected = true;
                                        dgvTab_BancosModalidade.Select();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnEspecie_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnEspecie.Name;
            clsInfo.zrow = dgvTab_BancosEspecie.CurrentRow;
            clsTab_BancosInfo clsTab_BancosInfo = new clsTab_BancosInfo();
            PreencheInfoTab_Bancos(clsTab_BancosInfo);
            frmFormIdVis frmFormIdVis = new frmFormIdVis();
            frmFormIdVis.Init(conexao, "TAB_BANCOSESPECIE", id, clsTab_BancosInfo );

            
            clsFormHelper.AbrirForm(this.MdiParent, frmFormIdVis,conexao);
        }

        private void btnMoeda_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnMoeda.Name;
            clsTab_BancosInfo clsTab_BancosInfo = new clsTab_BancosInfo();
            PreencheInfoTab_Bancos(clsTab_BancosInfo);
            frmFormIdVis frmFormIdVis = new frmFormIdVis();
            frmFormIdVis.Init(conexao, "TAB_BANCOSMOEDA", id, clsTab_BancosInfo);
            clsFormHelper.AbrirForm(this.MdiParent, frmFormIdVis,conexao);
        }

        private void btnAceite_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnAceite.Name;
            clsTab_BancosInfo clsTab_BancosInfo = new clsTab_BancosInfo();
            PreencheInfoTab_Bancos(clsTab_BancosInfo);
            frmFormIdVis frmFormIdVis = new frmFormIdVis();
            frmFormIdVis.Init(conexao, "TAB_BANCOSACEITE", id, clsTab_BancosInfo);

            
            clsFormHelper.AbrirForm(this.MdiParent, frmFormIdVis,conexao);
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnEstado.Name;
            clsTab_BancosInfo clsTab_BancosInfo = new clsTab_BancosInfo();
            PreencheInfoTab_Bancos(clsTab_BancosInfo);
            frmFormIdVis frmFormIdVis = new frmFormIdVis();
            frmFormIdVis.Init(conexao, "TAB_BANCOSESTADO", id, clsTab_BancosInfo);
            clsFormHelper.AbrirForm(this.MdiParent, frmFormIdVis,conexao);

        }

        private void btnProtestar_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnProtestar.Name;
            clsTab_BancosInfo clsTab_BancosInfo = new clsTab_BancosInfo();
            PreencheInfoTab_Bancos(clsTab_BancosInfo);
            frmFormIdVis frmFormIdVis = new frmFormIdVis();
            frmFormIdVis.Init(conexao, "TAB_BANCOSPROTESTAR", id, clsTab_BancosInfo);

            
            clsFormHelper.AbrirForm(this.MdiParent, frmFormIdVis,conexao);

        }

        private void btnImpressao_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnImpressao.Name;
            clsTab_BancosInfo clsTab_BancosInfo = new clsTab_BancosInfo();
            PreencheInfoTab_Bancos(clsTab_BancosInfo);
            frmFormIdVis frmFormIdVis = new frmFormIdVis();
            frmFormIdVis.Init(conexao, "TAB_BANCOSIMPRESSAO", id, clsTab_BancosInfo);
            clsFormHelper.AbrirForm(this.MdiParent, frmFormIdVis,conexao);
        }

        private void btnModalidade_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnModalidade.Name;
            clsTab_BancosInfo clsTab_BancosInfo = new clsTab_BancosInfo();
            PreencheInfoTab_Bancos(clsTab_BancosInfo);
            frmFormIdVis frmFormIdVis = new frmFormIdVis();
            frmFormIdVis.Init(conexao, "TAB_BANCOSMODALIDADE", id, clsTab_BancosInfo);

            
            clsFormHelper.AbrirForm(this.MdiParent, frmFormIdVis,conexao);
        }

        private void dgvTab_BancosEstado_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTab_BancosEstado.CurrentRow != null)
            {
                indexTab_BancosEstadoValor = clsParser.Int32Parse(dgvTab_BancosEstado.CurrentRow.Cells["id"].Value.ToString());
            }
        }

        private void dgvTab_BancosEspecie_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTab_BancosEspecie.CurrentRow != null)
            {
                indexTab_BancosEspecieValor = clsParser.Int32Parse(dgvTab_BancosEspecie.CurrentRow.Cells["id"].Value.ToString());
            }
        }

        private void dgvTab_BancosMoeda_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTab_BancosMoeda.CurrentRow != null)
            {
                indexTab_BancosMoedaValor = clsParser.Int32Parse(dgvTab_BancosMoeda.CurrentRow.Cells["id"].Value.ToString());
            }
        }

        private void dgvTab_BancosAceite_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTab_BancosAceite.CurrentRow != null)
            {
                indexTab_BancosAceiteValor = clsParser.Int32Parse(dgvTab_BancosAceite.CurrentRow.Cells["id"].Value.ToString());
            }
        }

        private void dgvTab_BancosProtestar_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTab_BancosProtestar.CurrentRow != null)
            {
                indexTab_BancosProtestarValor = clsParser.Int32Parse(dgvTab_BancosProtestar.CurrentRow.Cells["id"].Value.ToString());
            }
        }

        private void dgvTab_BancosImpressao_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTab_BancosImpressao.CurrentRow != null)
            {
                indexTab_BancosImpressaoValor = clsParser.Int32Parse(dgvTab_BancosImpressao.CurrentRow.Cells["id"].Value.ToString());
            }
        }

        private void dgvTab_BancosModalidade_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTab_BancosModalidade.CurrentRow != null)
            {
                indexTab_BancosModalidadeValor = clsParser.Int32Parse(dgvTab_BancosModalidade.CurrentRow.Cells["id"].Value.ToString());
            }
        }
        void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null && clsInfo.znomegrid != "")
            {
                //Pegando o Documento
                if (clsInfo.znomegrid == btnEspecie.Name)
                {
                    bwrTab_BancosEspecie_Run();
                }
                clsInfo.znomegrid = "";
                clsInfo.zrow = null;
            }
            // 
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {

        }
    }
}
