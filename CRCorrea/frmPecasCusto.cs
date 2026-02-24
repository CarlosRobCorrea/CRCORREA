
using CrystalDecisions.Shared;
using System.Transactions;


using CRCorrea;
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
    public partial class frmPecasCusto : Form
    {
        Boolean disposing = false;

        Int32 id;
        Int32 id_Anterior;
        Int32 id_Proximo;

        //DialogResult resultado;
        ParameterFields pfields = new ParameterFields();

        Int32 idPeca;
        Int32 idCodigo;
        Int32 idUnidade;
        Int32 idCodigoImportar = 0;

        //Int32 posicaoProcesso;

        clsPecasInfo ClsPecasInfo = new clsPecasInfo();
        clsPecasBLL clsPecasBLL = new clsPecasBLL();

        clsPecasCustoInfo clsPecasCustoInfo = new clsPecasCustoInfo();
        clsPecasCustoInfo clsPecasCustoInfoOld = new clsPecasCustoInfo();
        clsPecasCustoBLL clsPecasCustoBLL = new clsPecasCustoBLL();

        BackgroundWorker bwrPecasCusto;
        DataTable dtPecasCusto;
        //GridColuna[] dtPecasCustoColuna;
        Boolean carregandopecascusto = false;

        public frmPecasCusto()
        {
            InitializeComponent();
        }
        public void Init(Int32 _idpecas)
        {
            clsPecasBLL = new clsPecasBLL();
            clsPecasCustoBLL = new clsPecasCustoBLL();

            idPeca = _idpecas;
            ClsPecasInfo = clsPecasBLL.Carregar(idPeca, clsInfo.conexaosqldados);
            tbxCodigo.Text = ClsPecasInfo.codigo;
            tbxNome.Text = ClsPecasInfo.nome;
            //label78.Text = label78.Text + " ( " + ClsPecasInfo.categoriacusto + " ) ";

            bwrPecasCusto = new BackgroundWorker();
            bwrPecasCusto.WorkerSupportsCancellation = true;
            bwrPecasCusto.DoWork += new DoWorkEventHandler(bwrPecasCusto_DoWork);
            bwrPecasCusto.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrPecasCusto_RunWorkerCompleted);
            //aqui inicializamos a variavel carregarCliente com false 
            carregandopecascusto = false;


        }
        private void frmPecasCusto_Load(object sender, EventArgs e)
        {
            bwrPecasCusto_Run();
            //bwrPecasCusto_RunWorkerAsync();
            //tclProcesso.SelectedTab = tabProcesso;
            //plPecaProcesso.Enabled = true;
            //gbxItemProcesso.Visible = false;
            //tbxComissaoArquiteto.Text = "0";
            //tbxComissaoZelador.Text = "0";
            //tbxOutros.Text = "0";

            //tspRetornar.Select();
            
        }

        private void frmPecasCusto_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }
        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }
        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            TrataCampos((Control)sender);
        }
        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null && clsInfo.znomegrid != "")
            {
                if (clsInfo.znomegrid == btnidCodigo.Name)
                {
                    idCodigo = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    Fill_Pecas();
                    tbxQtde.Select();
                }
                if (clsInfo.znomegrid == btnIdCodigoImportar.Name)
                {
                    idCodigoImportar = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxPecas_Codigo_Importar.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecas where id=" + idCodigoImportar);
                    tbxPecas_Nome_Importar.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where id=" + idCodigoImportar);
//                    tbxQtde.Select();
                }

                clsInfo.znomegrid = "";
                clsInfo.zrow = null;
            }
            if (!disposing)
            {
                //TimeSpan CalcularMeses;
                //CalcularMeses = DateTime.Now.Subtract(clsParser.DateTimeParse(tbxData_Plantio.Text));
                //tbxQtde_Meses.Text = (CalcularMeses.TotalDays / 30).ToString("N0");

                //if (ctl.Name == tbxDataDe.Name)
                //{
                //    if (clsParser.DateTimeParse(tbxDataDe.Text) <= clsParser.DateTimeParse(tbxDataAte.Text))
                //    {
                //        datade = clsParser.DateTimeParse(tbxDataDe.Text);
                //    }
                //    else
                //    {
                //        MessageBox.Show("Verifique Data Ate !");
                //    }
                //}
            }

        }

        private void tspSimular_Click(object sender, EventArgs e)
        {
            CalcularCustos();
        }

        private void tspImportar_Click(object sender, EventArgs e)
        {
            tclProcesso.SelectedIndex = 2;
            gbxProcesso.Enabled = false;
            gbxImportar.Visible = true;

        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            id = 0;
            PecasCustoCarregar();
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvPecasCusto.CurrentRow != null)
            {
                id = clsParser.Int32Parse(dgvPecasCusto.CurrentRow.Cells["ID"].Value.ToString());
                PecasCustoCarregar();
            }
            else
            {
                MessageBox.Show("Primeiro selecione um registro!"); 
            }

        }

        private void tspExcluirItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPecasCusto.CurrentRow != null)
                {
                    DialogResult drt;

                    drt = MessageBox.Show("Deseja realmente Excluir o Codigo : " + dgvPecasCusto.CurrentRow.Cells["codigo"].Value.ToString() + "?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (drt == DialogResult.Yes)
                    {
                        clsPecasCustoBLL = new clsPecasCustoBLL();
                        clsPecasCustoBLL.Excluir(clsParser.Int32Parse(dgvPecasCusto.CurrentRow.Cells["id"].Value.ToString()), clsInfo.conexaosqldados);
                        bwrPecasCusto_Run();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {

        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            disposing = true;
            this.Close();
        }
        private void bwrPecasCusto_Run()
        {
            if (carregandopecascusto == false)
            {
                carregandopecascusto = true;
                //pbxCompras.Visible = true;
                bwrPecasCusto = new BackgroundWorker();
                bwrPecasCusto.DoWork += new DoWorkEventHandler(bwrPecasCusto_DoWork);
                bwrPecasCusto.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrPecasCusto_RunWorkerCompleted);
                bwrPecasCusto.RunWorkerAsync();
            }
        }


        private void bwrPecasCusto_DoWork(object sender, DoWorkEventArgs e)
        {
            dtPecasCusto = clsPecasCustoBLL.GridDados(idPeca);
        }


        private void bwrPecasCusto_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                clsPecasCustoBLL.GridMonta(dgvPecasCusto, dtPecasCusto, clsInfo.zrowid);
                clsGridHelper.SelecionaLinha(id, dgvPecasCusto, "Codigo");
                PesquisaRapida();
                carregandopecascusto = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                carregandopecascusto = false;
            }
        }
        private void PesquisaRapida()
        {
            clsGridHelper.Filtrar(tstPesquisa.Text, dgvPecasCusto);
            if (id > 0 || id_Anterior > 0 || id_Proximo > 0)
            {
                if (clsGridHelper.SelecionaLinha_ReturnBoolean(id, dgvPecasCusto, "codigo") == false)
                {
                    if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Proximo,
                                   dgvPecasCusto, "codigo") == false)
                    {
                        if (clsGridHelper.SelecionaLinha_ReturnBoolean(id_Anterior,
                                dgvPecasCusto, "codigo") == false)
                        {
                            if (dgvPecasCusto.Rows.Count > 0)
                            {
                                dgvPecasCusto.CurrentCell = dgvPecasCusto.Rows[0].Cells["codigo"];
                            }
                        }
                    }
                }
            }
            else if (dgvPecasCusto.Rows.Count > 0)
            {
                dgvPecasCusto.CurrentCell = dgvPecasCusto.Rows[0].Cells["codigo"];
            }

            if (dgvPecasCusto.CurrentRow != null)
            {
                id = clsParser.Int32Parse(
                      dgvPecasCusto.CurrentRow.Cells["ID"].Value.ToString());
                if (dgvPecasCusto.CurrentRow.Index > 0)
                {
                    id_Anterior = clsParser.Int32Parse(dgvPecasCusto.Rows[
           dgvPecasCusto.CurrentRow.Index - 1].Cells["ID"].Value.ToString());
                }
                else
                {
                    id_Anterior = 0;
                }
                if (dgvPecasCusto.CurrentRow.Index < dgvPecasCusto.Rows.Count - 1)
                {
                    id_Proximo = clsParser.Int32Parse(dgvPecasCusto.Rows[
         dgvPecasCusto.CurrentRow.Index + 1].Cells["ID"].Value.ToString());
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
        void PecasCustoCarregar()
        {
            clsPecasCustoInfo = new clsPecasCustoInfo();
            if (id == 0)
            {

                clsPecasCustoInfo.id = 0;
                clsPecasCustoInfo.idcodigo = 0;
                clsPecasCustoInfo.idpeca = 0;
                clsPecasCustoInfo.idunidade = 0;
                clsPecasCustoInfo.ordem = 0;
                clsPecasCustoInfo.participacao = 0;
                clsPecasCustoInfo.precototal = 0;
                clsPecasCustoInfo.precounitario = 0;
                clsPecasCustoInfo.qtde = 0;
                clsPecasCustoInfo.tipo = "";
            }
            else
            {
                clsPecasCustoInfo = clsPecasCustoBLL.Carregar(id, clsInfo.conexaosqldados);
            }
            PecasCustoCampos(clsPecasCustoInfo);
            PecasCustoFillInfo(clsPecasCustoInfoOld);
            tbxOrdem.Select();

        }
        void PecasCustoCampos(clsPecasCustoInfo info)
        {
            tclProcesso.SelectedIndex = 1;
            gbxProcesso.Enabled = false;
            gbxItemProcesso.Visible = true;

            id = info.id;
            idCodigo = info.idcodigo;
            Fill_Pecas();
            // info.idpeca;
            idUnidade = info.idunidade;
            tbxOrdem.Text = info.ordem.ToString();
            tbxParticipacao.Text = info.participacao.ToString("N2");
            tbxPrecoTotal.Text = info.precototal.ToString("N2");
            tbxPrecoUni.Text = info.precounitario.ToString("N2");
            tbxQtde.Text = info.qtde.ToString("N3");
            cbxFase.SelectedIndex = cbxFase.FindString(info.tipo, 1);
        }
        void PecasCustoFillInfo(clsPecasCustoInfo info)
        {
            info.id = id;
            info.idcodigo = idCodigo;
            info.idpeca = idPeca;
            info.idunidade = idUnidade;
            info.ordem = clsParser.Int32Parse(tbxOrdem.Text);
            info.participacao = clsParser.DecimalParse(tbxParticipacao.Text);
            info.precototal = clsParser.DecimalParse(tbxPrecoTotal.Text);
            info.precounitario = clsParser.DecimalParse(tbxPrecoUni.Text);
            info.qtde = clsParser.DecimalParse(tbxQtde.Text);
            info.tipo = cbxFase.Text.PadRight(1, ' ').Substring(0, 1);

        }
        private void Fill_Pecas()
        {
            tbxPecas_Codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from pecas where id=" + idCodigo);
            tbxPecas_Nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from pecas where id=" + idCodigo);
            idUnidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "SELECT IDUNIDADE FROM PECAS WHERE ID=" + idCodigo));
            tbxUnidade_Codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade where ID= " + idUnidade);

            // Alterado por solicitação do sr. Acacio em 10/05/2016
            //Compras1_tbxPreco.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
            //"select nfcompra1.preco from nfcompra1 inner join nfcompra on nfcompra1.numero = nfcompra.id where nfcompra1.idcodigo = " + compras1_idcodigo + " order by nfcompra.data desc ", "0").ToString()).ToString("N6");
            tbxPrecoUni.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
            "select valoraprazo from pecaspreco where idcodigo = " + idCodigo + " order by data desc ", "0").ToString()).ToString("N6");
            if (clsParser.DecimalParse(tbxPrecoUni.Text) == 0)
            {
                tbxPrecoUni.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select precouni from pecasforne where idcodigo=" + idCodigo + " order by database1 desc", "0")).ToString("N4");
            }
        }

        private void tspSalvarItem_Click(object sender, EventArgs e)
        {
            if (Salvar() == DialogResult.Cancel)
            {
                return;
            }
            tclProcesso.SelectedIndex = 0;
            gbxProcesso.Enabled = true;
            gbxItemProcesso.Visible = false;
            bwrPecasCusto_Run();

        }

        private void tspRetornarItem_Click(object sender, EventArgs e)
        {
            tclProcesso.SelectedIndex = 0;
            gbxProcesso.Enabled = true;
            gbxItemProcesso.Visible = false;

        }
        private DialogResult Salvar()
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja Salvar este Registro e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                PecasCustoSalvar();
            }
            return drt;
        }
        private void PecasCustoSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // Tabela: Tab_GarantiaPerda
                clsPecasCustoInfo  = new clsPecasCustoInfo();
                PecasCustoFillInfo(clsPecasCustoInfo);
                if (id == 0)
                {
                    clsPecasCustoInfo.id = clsPecasCustoBLL.Incluir(clsPecasCustoInfo, clsInfo.conexaosqldados);
                }
                else
                {
                    clsPecasCustoBLL.Alterar(clsPecasCustoInfo, clsInfo.conexaosqldados);
                }
                tse.Complete();
            }
        }

        private void btnidCodigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnidCodigo.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(idCodigo);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }

        private void dgvPecasCusto_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void btnRetornarImportar_Click(object sender, EventArgs e)
        {
            tclProcesso.SelectedIndex = 0;
            gbxProcesso.Enabled = true;
            gbxImportar.Visible = false;
            idCodigoImportar = 0;

        }

        private void btnIdCodigoImportar_Click(object sender, EventArgs e)
        {
            idCodigoImportar = 0;
            clsInfo.znomegrid = btnIdCodigoImportar.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(idCodigo);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (idCodigoImportar > 0 && idPeca != idCodigoImportar)
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja mesmo Importar o custo deste Item ?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
                if (drt == DialogResult.Yes)
                {
                    DataTable dtaux = new DataTable();
                    dtaux = Procedure.RetornaDataTable(clsInfo.conexaosqldados, "select * from pecascusto WHERE idpeca= " + idCodigoImportar);
                    foreach (DataRow row in dtaux.Rows)
                    {
                        // Procurar dentro da peça destino e verificar se este codigo já não existe
                        Int32 idcodigoexiste = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from pecascusto where idpeca=" + idPeca + " and idcodigo = " + clsParser.Int32Parse(row["idcodigo"].ToString()), "0"));
                        if (idcodigoexiste == 0)
                        {  // Importar
                            clsPecasCustoInfo clsPecasCustoOld = new clsPecasCustoInfo();
                            clsPecasCustoOld.idcodigo = clsParser.Int32Parse(row["idcodigo"].ToString());
                            clsPecasCustoOld.idpeca = idPeca;
                            clsPecasCustoOld.idunidade = clsParser.Int32Parse(row["idunidade"].ToString());
                            clsPecasCustoOld.ordem = clsParser.Int32Parse(row["ordem"].ToString());
                            clsPecasCustoOld.participacao = clsParser.DecimalParse(row["participacao"].ToString());
                            clsPecasCustoOld.precototal = clsParser.DecimalParse(row["precototal"].ToString());
                            clsPecasCustoOld.precounitario = clsParser.DecimalParse(row["precounitario"].ToString());
                            clsPecasCustoOld.qtde = clsParser.DecimalParse(row["qtde"].ToString());
                            clsPecasCustoOld.tipo = row["tipo"].ToString();
                            clsPecasCustoBLL.Incluir(clsPecasCustoOld, clsInfo.conexaosqldados);
                        }
                    }
                    MessageBox.Show("Termino de Importação");
                    idCodigoImportar = 0;
                    tbxPecas_Codigo_Importar.Text = "";
                    tbxPecas_Nome_Importar.Text = "";

                    tclProcesso.SelectedIndex = 0;
                    gbxProcesso.Enabled = true;
                    gbxImportar.Visible = false;

                    bwrPecasCusto_Run();

                }
            }
        }
        private void CalcularCustos()
        {
            //
            tbxTotalCusto.Text = "0";

            foreach (DataRow row in dtPecasCusto.Rows)
            {
                if (row.RowState == DataRowState.Deleted ||
                    row.RowState == DataRowState.Detached)
                {
                    continue;   // não somar se apagou
                }
                else
                {
                    // Somar os campos do cabeçalho
                    tbxTotalCusto.Text = (clsParser.DecimalParse(tbxTotalCusto.Text) + clsParser.DecimalParse(row["PRECOTOTAL"].ToString())).ToString("N2");
                }
            }

            tbxTotalCusto.Text = tbxTotalCusto.Text;
        }

    }
}
