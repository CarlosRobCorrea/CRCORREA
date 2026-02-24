using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CRCorrea;

using CrystalDecisions.Shared;
using System;
using System.Data;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmRequisicao_Apli : Form
    {
        clsFinanceiro clsFinanceiro = new clsFinanceiro();

        ParameterFields pfields = new ParameterFields();

        clsRequisicaoBLL clsRequisicaoBLL;
        clsRequisicaoInfo clsRequisicaoInfo;
        clsRequisicaoInfo clsRequisicaoInfoOld; 

        DataGridViewRowCollection rows;

        Int32 id;

        // REQUISICAO1
        clsRequisicao1BLL clsRequisicao1BLL;
        clsRequisicao1Info clsRequisicao1Info;
        clsRequisicao1Info clsRequisicao1InfoOld; 

        DataTable dtRequisicao1;
        Int32 requisicao1_id;
        Int32 requisicao1_posicao;
        Int32 requisicao1_numero;
        Int32 requisicao1_idcodigo;
        Int32 requisicao1_idfuncionario;
        Int32 requisicao1_idmaquina;
        Int32 requisicao1_idordemservico;
        Int32 requisicao1_idordemservicoop;
        Int32 requisicao1_idunidade;



        //
        Int32 _requisicao_guiaatual;
        Int32 requisicao_guiaatual
        {
            get { return _requisicao_guiaatual; }
            set
            {
                if (value == 0 || value == 1 || value == 2 || value == 3)   // Posíveis posição para o SelectedIndex do TabControl
                {
                    _requisicao_guiaatual = value;
                }
                else
                {
                    return;
                }

                tclRequisicao.SelectedIndex = _requisicao_guiaatual;
            }
        }

        public frmRequisicao_Apli()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridViewRowCollection _rows)
        {
            this.id = _id;
            this.rows = _rows;
            clsRequisicaoBLL = new clsRequisicaoBLL();

            clsRequisicao1BLL = new clsRequisicao1BLL();

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", Requisicao1_tbxCodigo);

        }

        private void frmRequisicao_Apli_Load(object sender, EventArgs e)
        {
            RequisicaoCarregar();
        }

        private void frmRequisicao_Apli_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void tspIncluir_Click(object sender, EventArgs e)
        {
            requisicao1_posicao = 0;
            Requisicao1Carregar();
            tspIncluir.Enabled = true;
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvRequisicao1.CurrentRow != null)
            {
                requisicao1_posicao = clsParser.Int32Parse(dgvRequisicao1.CurrentRow.Cells["requisicao1_posicao"].Value.ToString());
                Requisicao1Carregar();
            }

            tspSalvar1.Enabled = false;
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Salvar() == DialogResult.Cancel)
                {
                    return;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    id = Int32.Parse(rows[0].Cells[0].Value.ToString());
                    RequisicaoCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index != 0)
                            {
                                id = Int32.Parse(rows[rows.GetPreviousRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                RequisicaoCarregar();
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 

        }

        private void tspProximo_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index < rows.Count - 1)
                            {
                                id = Int32.Parse(rows[rows.GetNextRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                RequisicaoCarregar();
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 

        }

        private void tspUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    id = Int32.Parse(rows[rows.Count - 1].Cells[0].Value.ToString());
                    RequisicaoCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private Boolean HouveModificacoes()
        {
            clsRequisicaoInfo = new clsRequisicaoInfo();
            RequisicaoFillInfo(clsRequisicaoInfo);
            if (clsRequisicaoBLL.Equals(clsRequisicaoInfo, clsRequisicaoInfoOld) == false)
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
                RequisicaoSalvar();
            }
            return drt;

        }
        private void RequisicaoSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: ORCAMENTO
                clsRequisicaoInfo = new clsRequisicaoInfo();
                RequisicaoFillInfo(clsRequisicaoInfo);
                if (id == 0)
                {
                    clsRequisicaoInfo.id = clsRequisicaoBLL.Incluir(clsRequisicaoInfo, clsInfo.conexaosqldados);
                }
                else
                {
                    clsRequisicaoBLL.Alterar(clsRequisicaoInfo, clsInfo.conexaosqldados);
                }

                // ITENS Da REQUISICAO (REQUISICAO1) - marca os registros filho da principal
                foreach (DataRow row in dtRequisicao1.Rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                        row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Unchanged)
                    {
                        row["numero"] = clsRequisicaoInfo.id;
                    }
                }

                foreach (DataRow row in dtRequisicao1.Rows)
                {
                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        clsRequisicao1BLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    {
                        clsRequisicao1Info = new clsRequisicao1Info();
                        Requisicao1GridToInfo(clsRequisicao1Info, Int32.Parse(row["requisicao1_posicao"].ToString()));

                        if (clsRequisicao1Info.id == 0)
                        {
                            clsRequisicao1Info.id = clsRequisicao1BLL.Incluir(clsRequisicao1Info, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsRequisicao1BLL.Alterar(clsRequisicao1Info, clsInfo.conexaosqldados);
                        }
                    }
                }
                //
                tse.Complete();
            }
        }


        private void tbxPesquisa_MouseUp(object sender, MouseEventArgs e)
        {
            PesquisaRapida();
        }

        private void tbxPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            PesquisaRapida();
        }
        private void PesquisaRapida()
        {
            dgvRequisicao1 = clsGridHelper.PesquisaRapida(dgvRequisicao1, dtRequisicao1, tbxPesquisa.Text);
            clsGridHelper.SelecionaLinha(id, dgvRequisicao1, "CODIGO");
            tbxPesquisa.Focus();
            if (dgvRequisicao1.CurrentRow != null)
            {
                requisicao1_id = clsParser.Int32Parse(dgvRequisicao1.CurrentRow.Cells["ID"].Value.ToString());
            }
            else
            {
                requisicao1_id = 0;
            }
        }
        private void RequisicaoCarregar()
        {
            clsRequisicaoInfoOld = new clsRequisicaoInfo();

            if (id == 0)
            {
                clsRequisicaoInfo = new clsRequisicaoInfo();
                clsRequisicaoInfo.ano = DateTime.Now.Year;
                clsRequisicaoInfo.data = DateTime.Now;
                clsRequisicaoInfo.emitente = clsInfo.zusuario;
                clsRequisicaoInfo.filial = clsInfo.zfilial;
                clsRequisicaoInfo.id = 0;
                clsRequisicaoInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NUMERO from REQUISICAO ORDER BY DATA DESC, NUMERO DESC"));
                clsRequisicaoInfo.numero += 1;
            }
            else
            {
                clsRequisicaoInfo = clsRequisicaoBLL.Carregar(id, clsInfo.conexaosqldados);
            }
            RequisicaoCampos(clsRequisicaoInfo);
            RequisicaoFillInfo(clsRequisicaoInfoOld);
            //
            //
            // carregando os itens do orçamento
            dtRequisicao1 = clsRequisicao1BLL.GridDados(clsRequisicaoInfo.id, clsInfo.conexaosqldados);

            DataColumn dcPosicao = new DataColumn("requisicao1_posicao", Type.GetType("System.Int32"));
            dtRequisicao1.Columns.Add(dcPosicao);

            for (Int32 i = 1; i <= dtRequisicao1.Rows.Count; i++)
            {
                dtRequisicao1.Rows[i - 1]["requisicao1_posicao"] = i;
            }

            dtRequisicao1.AcceptChanges();
            clsRequisicao1BLL.GridMonta(dgvRequisicao1, dtRequisicao1, requisicao1_posicao);

            //Requisicao1SomaTotal();

            requisicao_guiaatual = 0;


        }
        private void RequisicaoCampos(clsRequisicaoInfo info)
        {
            id = info.id;

            //Pecastipo_Carrega();  // CARREGAR ESTE TIPO 
            tbxAno.Text = info.ano.ToString();
            tbxData.Text = info.data.ToString("dd/MM/yyyy");
            tbxEmitente.Text = info.emitente;
            tbxFilial.Text = info.filial.ToString();
            tbxNumero.Text = info.numero.ToString("N0");



        }
        private void RequisicaoFillInfo(clsRequisicaoInfo info)
        {

            info.id = id;
            info.ano = clsParser.Int32Parse(tbxAno.Text);
            info.data = DateTime.Parse(tbxData.Text);
            info.emitente = tbxEmitente.Text;
            info.filial = clsParser.Int32Parse(tbxFilial.Text);
            info.numero = clsParser.Int32Parse(tbxNumero.Text);

            if (info.data.ToString("dd/MM/yyyy") != DateTime.Now.ToString("dd/MM/yyyy"))
            {
                tspSalvar.Enabled = false;
                tspSalvar1.Enabled = false;
                tspIncluir.Enabled = false;
                MessageBox.Show("Requisição Não pode ser alterada !!");
            }
            if (tbxEmitente.Text != clsInfo.zusuario)
            {
                tspSalvar.Enabled = false;
                tspSalvar1.Enabled = false;
                tspIncluir.Enabled = false;
                MessageBox.Show("Usuario Diferente do emitente - Não pode alterar/incluir item na requisição !!");
            }


        }



        private void tspSalvar1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja salvar / alterar este Item?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    clsRequisicao1Info = new clsRequisicao1Info();
                    Requisicao1FillInfo(clsRequisicao1Info);
                    if (clsRequisicao1Info.qtde <= 0)
                    {
                        //caso não houve nenhuma quantidade na requisição lançar uma exceção

                        throw new Exception("favor inserir uma quantidade para esta requisição");
                    }
                    Requisicao1FillInfoToGrid(clsRequisicao1Info);
                }
                else if (drt == DialogResult.Cancel)
                {
                    return;
                }

                requisicao_guiaatual = 0;
                gbxRequisicao1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspPrimeiro1_Click(object sender, EventArgs e)
        {

        }

        private void tspAnterior1_Click(object sender, EventArgs e)
        {

        }

        private void tspProximo1_Click(object sender, EventArgs e)
        {

        }

        private void tspUltimo1_Click(object sender, EventArgs e)
        {

        }

        private void tspRetornar1_Click(object sender, EventArgs e)
        {
            requisicao_guiaatual = 0;    // Volta para a guia principal do item
            gbxRequisicao1.Visible = false;

        }
        void Requisicao1Carregar()
        {
            clsRequisicao1Info = new clsRequisicao1Info();
            clsRequisicao1InfoOld = new clsRequisicao1Info();

            if (requisicao1_posicao == 0)
            {
                requisicao1_posicao = dtRequisicao1.Rows.Count + 1;

                clsRequisicao1Info.dataentrega = DateTime.Now;
                clsRequisicao1Info.dataretorno = DateTime.Now;
                clsRequisicao1Info.funcionario = "";
                clsRequisicao1Info.idcodigo = clsInfo.zpecas;
                clsRequisicao1Info.idfuncionario = 0;
                clsRequisicao1Info.idmaquina = 0;
                clsRequisicao1Info.idordemservico = clsInfo.zordemservico;
                clsRequisicao1Info.idordemservicoop = 0;
                clsRequisicao1Info.idunidade = clsInfo.zunidade;
                clsRequisicao1Info.motivo = "N";
                clsRequisicao1Info.numero = id;
                clsRequisicao1Info.observar = "";
                clsRequisicao1Info.qtde = 0;
                clsRequisicao1Info.qtdedev = 0;
                clsRequisicao1Info.qtdeentrega = 0;
                clsRequisicao1Info.qtdesaldo = 0;
                clsRequisicao1Info.tipo = "S";
                clsRequisicao1Info.valor = 0;
            }
            else
            {
                Requisicao1GridToInfo(clsRequisicao1Info, requisicao1_posicao);
            }

            Requisicao1Campos(clsRequisicao1Info);
            Requisicao1FillInfo(clsRequisicao1InfoOld);

            requisicao_guiaatual = 1;
            gbxRequisicao1.Visible = true;
            Requisicao1_tbxMaquina.Select();
        }
        private void Requisicao1GridToInfo(clsRequisicao1Info info, Int32 posicao)
        {
            DataRow row = dtRequisicao1.Select("requisicao1_posicao = " + posicao)[0];

            info.id = clsParser.Int32Parse(row["id"].ToString());
            if (row["dataentrega"].ToString() == "")
            {
                row["dataentrega"] = DateTime.MinValue.ToString(); 
            }
            info.dataentrega = DateTime.Parse(row["dataentrega"].ToString());
            if (row["dataretorno"].ToString() == "")
            {
                row["dataretorno"] = DateTime.MinValue.ToString();
            }
            info.dataretorno = DateTime.Parse(row["dataretorno"].ToString());
            info.funcionario = row["funcionario"].ToString();
            info.idcodigo = clsParser.Int32Parse(row["idcodigo"].ToString());
            info.idfuncionario = clsParser.Int32Parse(row["idfuncionario"].ToString());
            info.idmaquina = clsParser.Int32Parse(row["idmaquina"].ToString());
            info.idordemservico = clsParser.Int32Parse(row["idordemservico"].ToString());
            info.idordemservicoop = clsParser.Int32Parse(row["idordemservicoop"].ToString());
            info.idunidade = clsParser.Int32Parse(row["idunidade"].ToString());
            info.motivo = row["motivo"].ToString();
            info.numero = clsParser.Int32Parse(row["numero"].ToString());
            info.observar = row["observar"].ToString();
            info.qtde = clsParser.DecimalParse(row["qtde"].ToString());
            info.qtdedev = clsParser.DecimalParse(row["qtdedev"].ToString());
            info.qtdesaldo = clsParser.DecimalParse(row["qtdesaldo"].ToString());
            info.tipo = row["tipo"].ToString();
            info.valor = clsParser.DecimalParse(row["valor"].ToString());
        }
        private void Requisicao1Campos(clsRequisicao1Info info)
        {
            requisicao1_id = info.id;
            requisicao1_numero = info.numero;

            Requisicao1_tbxDataEntrega.Text = info.dataentrega.ToString("dd/MM/yyyy");
            Requisicao1_tbxDataRetorno.Text = info.dataretorno.ToString("dd/MM/yyyy"); 
            Requisicao1_tbxFuncionario.Text = info.funcionario.ToString(); 
            requisicao1_idcodigo = info.idcodigo;
            if (info.idcodigo == 0)
            {
                info.idcodigo = clsInfo.zpecas;
                requisicao1_idcodigo = info.idcodigo;
            }
            Requisicao1_tbxCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS where ID= " + requisicao1_idcodigo);
            Requisicao1_tbxNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from PECAS where ID= " + requisicao1_idcodigo);
            requisicao1_idfuncionario =  info.idfuncionario;
            requisicao1_idmaquina = info.idmaquina;
            if (info.idmaquina > 0)
            {
               Requisicao1_tbxMaquina.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NUMERO from MAQUINAS where ID= " + requisicao1_idmaquina);
            }
            requisicao1_idordemservico = info.idordemservico;
            if (info.idordemservico == 0)
            {
                requisicao1_idordemservico = clsInfo.zordemservico;
            }
            Requisicao1_tbxOs.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NUMERO from ORDEMSERVICO where ID= " + requisicao1_idordemservico);
            requisicao1_idordemservicoop = info.idordemservicoop;
            requisicao1_idunidade = info.idunidade;
            if (info.idunidade == 0)
            {
                requisicao1_idunidade = clsInfo.zunidade;
            }
            Requisicao1_tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE where ID= " + requisicao1_idunidade);
            if (info.motivo == "E")
            {
                Requisicao1_rbnMotivoEntrada.Checked = true;
            }
            else
            {
                Requisicao1_rbnMotivoSaida.Checked = true;
            }
            Requisicao1_tbxObserva.Text = info.observar;
            Requisicao1_tbxQtde.Text = info.qtde.ToString("N3");
            Requisicao1_tbxQtdeDev.Text = info.qtdedev.ToString("N3"); ;
            Requisicao1_tbxQtdeEntrega.Text = info.qtdeentrega.ToString("N3"); ;
            Requisicao1_tbxQtdeSaldo.Text = info.qtdesaldo.ToString("N3"); ;
            if (info.tipo == "R")
            {
                Requisicao1_rbnTipoRetorna.Checked = true;
            }
            else
            {
                Requisicao1_rbnTipoNaoretorna.Checked = true;
            }
            Requisicao1_tbxValor.Text = info.valor.ToString("N6");
        }
        private void Requisicao1FillInfo(clsRequisicao1Info info)
        {
            info.id = requisicao1_id;
            info.numero = requisicao1_numero;

            info.dataentrega = DateTime.Parse(Requisicao1_tbxDataEntrega.Text);
            info.dataretorno = DateTime.Parse(Requisicao1_tbxDataRetorno.Text);
            info.funcionario = Requisicao1_tbxFuncionario.Text;
            info.idcodigo = requisicao1_idcodigo;
            info.idfuncionario = requisicao1_idfuncionario;
            info.idmaquina = requisicao1_idmaquina;
            info.idordemservico = requisicao1_idordemservico;
            info.idordemservicoop = requisicao1_idordemservicoop;
            info.idunidade = requisicao1_idunidade;
            if (Requisicao1_rbnMotivoEntrada.Checked == true)
            {
                info.motivo = "E";
            }
            else
            {
                info.motivo = "S";
            }

            info.observar = Requisicao1_tbxObserva.Text;
            info.qtde = clsParser.DecimalParse(Requisicao1_tbxQtde.Text);
            info.qtdedev = clsParser.DecimalParse(Requisicao1_tbxQtdeDev.Text);
            info.qtdeentrega = clsParser.DecimalParse(Requisicao1_tbxQtdeEntrega.Text);
            info.qtdesaldo = clsParser.DecimalParse(Requisicao1_tbxQtdeSaldo.Text);
            if (Requisicao1_rbnTipoNaoretorna.Checked == true)
            {
                info.tipo = "N";
            }
            else
            {
                info.tipo = "R";
            }
            info.valor = clsParser.DecimalParse(Requisicao1_tbxValor.Text);
        }
        void Requisicao1FillInfoToGrid(clsRequisicao1Info info)
        {
            DataRow row;
            DataRow[] rows = dtRequisicao1.Select("requisicao1_posicao = " + requisicao1_posicao);

            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtRequisicao1.NewRow();
            }

            row["id"] = info.id;

            row["dataentrega"] = info.dataentrega;
            row["dataretorno"] = info.dataretorno;
            row["funcionario"] = info.funcionario;
            row["idcodigo"] = info.idcodigo;
            row["idfuncionario"] = info.idfuncionario;
            row["idmaquina"] = info.idmaquina;
            row["idordemservico"] = info.idordemservico;
            row["idordemservicoop"] = info.idordemservicoop;
            row["idunidade"] = info.idunidade;
            row["motivo"] = info.motivo;
            row["numero"] = info.numero;
            row["observar"] = info.observar;
            row["qtde"] = info.qtde;
            row["qtdedev"] =info.qtdedev;
            row["qtdesaldo"] = info.qtdesaldo;
            row["tipo"] = info.tipo;
            row["valor"] = info.valor;

            // Colunas que petencem a outras tabelas
            row["codigo"] = Requisicao1_tbxCodigo.Text;
            row["nome"] = Requisicao1_tbxNome.Text;
            //row["unid"] = Orcamento1_Pecas_tbxUnidade.Text;

            if (rows.Length == 0)
            {
                row["requisicao1_posicao"] = requisicao1_posicao;
                dtRequisicao1.Rows.Add(row);
            }
        }


        private void dgvRequisicao1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspAlterar.PerformClick();
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
        private void ControlKeyDownHora(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownHora((TextBox)sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void Requisicao1_btnIdMaquina_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Requisicao1_btnIdMaquina.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "MAQUINAS", requisicao1_idmaquina, "Maquinas");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);

        }
        private void Requisicao1_btnIdFuncionario_Click(object sender, EventArgs e)
        {
            //clsInfo.znomegrid = Requisicao1_btnIdFuncionario.Name;
            //frmFuncionarioPes frmFuncionarioPes = new frmFuncionarioPes();
            //frmFuncionarioPes.Init(clsInfo.conexaosqlfolha, requisicao1_idfuncionario);
            //clsFormHelper.AbrirForm(this.MdiParent, frmFuncionarioPes, clsInfo.conexaosqldados);
        }

        private void Requisicao1_btnIdOrdemServico_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Requisicao1_btnIdOrdemServico.Name;
            /*
            frmOrdemServicoPes frmOrdemServicoPes = new frmOrdemServicoPes();
            frmOrdemServicoPes.Init();

            clsFormHelper.AbrirForm(this.MdiParent, frmOrdemServicoPes); 
             */
            //frmOrdemServicoPes frmOrdemServicoPes = new frmOrdemServicoPes();
            //frmOrdemServicoPes.Init(requisicao1_idordemservico);
            //clsFormHelper.AbrirForm(this.MdiParent, frmOrdemServicoPes, clsInfo.conexaosqldados); 

        }
        private void Requisicao1_btnIdCodigo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = Requisicao1_btnIdCodigo.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(0);

            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);
        }
        void TrataCampos(Control ctl)
        {
            //REQUISICAO1
            // Maquina
            if (clsInfo.znomegrid == Requisicao1_btnIdMaquina.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        requisicao1_idmaquina = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        Requisicao1_tbxMaquina.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                        Requisicao1_tbxMaquinaNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                    }
                }
                Requisicao1_tbxMaquina.Select();
            }
            if (ctl.Name == Requisicao1_tbxMaquina.Name)
            {
                requisicao1_idmaquina = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from MAQUINAS where CODIGO='" + Requisicao1_tbxCodigo.Text + "'"));
                Requisicao1_tbxMaquina.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from MAQUINAS where ID=" + requisicao1_idmaquina + "");
                Requisicao1_tbxMaquinaNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from MAQUINAS where ID=" + requisicao1_idmaquina + "");
            }
            // funcionario
            if (clsInfo.znomegrid == Requisicao1_btnIdFuncionario.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        requisicao1_idfuncionario = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        Requisicao1_tbxChapa.Text = clsInfo.zrow.Cells["CHAPA"].Value.ToString();
                        Requisicao1_tbxFuncionario.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                    }
                }
                Requisicao1_tbxChapa.Select();
            }
            if (ctl.Name == Requisicao1_tbxChapa.Name)
            {
                requisicao1_idfuncionario = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlfolha, "select ID from funcionario where chapa=" + Requisicao1_tbxChapa.Text + " ","0"));
                Requisicao1_tbxFuncionario.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlfolha, "select nome from funcionario where ID=" + requisicao1_idfuncionario + "", "");
            }

            // ORDEM SERVICO

            if (clsInfo.znomegrid == Requisicao1_btnIdOrdemServico.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        requisicao1_idordemservico = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        Requisicao1_tbxOs.Text = clsInfo.zrow.Cells["NUMERO"].Value.ToString();
                        Requisicao1_tbxAnoOS.Text = clsInfo.zrow.Cells["ANO"].Value.ToString();
                    }
                }
                ////Requisicao1_tbxOs.Select();
            }
            if (ctl.Name == Requisicao1_tbxOs.Name)
            {
                requisicao1_idordemservico = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from ordemservico where numero=" + Requisicao1_tbxOs.Text + " "));
                Requisicao1_tbxAnoOS.Text = (DateTime.Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select data from ordemservico where ID=" + requisicao1_idordemservico + "").ToString()).Year).ToString();
            }
            // Peças
            if (clsInfo.znomegrid == Requisicao1_btnIdCodigo.Name)
            {
                if (clsInfo.zrow != null)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        requisicao1_idcodigo = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        Requisicao1_tbxCodigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                        Requisicao1_tbxNome.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                        requisicao1_idunidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDUNIDADE from PECAS where ID=" + requisicao1_idcodigo + ""));
                        Requisicao1_tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE where ID=" + requisicao1_idunidade + "");
                    }
                }

                Requisicao1_tbxCodigo.Select();
            }
            if (ctl.Name == Requisicao1_tbxCodigo.Name)
            {
                requisicao1_idcodigo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from PECAS where CODIGO='" + Requisicao1_tbxCodigo.Text + "'"));
                if (requisicao1_idcodigo == 0)
                {
                    requisicao1_idcodigo = clsInfo.zpecas;
                }
                Requisicao1_tbxCodigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from PECAS where ID=" + requisicao1_idcodigo + "");
                Requisicao1_tbxNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS where ID=" + requisicao1_idcodigo + "");
                requisicao1_idunidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDUNIDADE from PECAS where ID=" + requisicao1_idcodigo + ""));
                Requisicao1_tbxUnidade.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from UNIDADE where ID=" + requisicao1_idunidade + "");

                // pegar o custo medio.
                Requisicao1_tbxLocacao.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select LOCACAO from PECAS where ID=" + requisicao1_idcodigo + "");
                tbxQtdeSaldo.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select QTDESALDO from PECAS where ID=" + requisicao1_idcodigo + "")).ToString("N3");
                //tbxQtdeFabrica.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDUNIDADE from PECAS where ID=" + requisicao1_idcodigo + "")).ToString("N3");
                //tbxQtdeFinal.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDUNIDADE from PECAS where ID=" + requisicao1_idcodigo + "")).ToString("N3");
                Requisicao1_tbxValor.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CUSTOMEDIOMES from MOVPECASMES where IDCODIGO=" + requisicao1_idcodigo + " ORDER BY ANOMES DESC")).ToString("N6");
            }
            if (Requisicao1_rbnMotivoEntrada.Checked == true)
            {
                Requisicao1_tbxValor.ReadOnly = false;
                Requisicao1_tbxValor.BackColor = Color.White;
            }
            else
            {
                Requisicao1_tbxValor.ReadOnly = true;
                Requisicao1_tbxValor.BackColor = Color.LemonChiffon;
            }
            Requisicao1_tbxQtde.Text = clsParser.DecimalParse(Requisicao1_tbxQtde.Text).ToString("N3");
            Requisicao1_tbxValor.Text = clsParser.DecimalParse(Requisicao1_tbxValor.Text).ToString("N4");
            clsInfo.znomegrid = "";
            clsInfo.zrow = null;


        }

    }
}
