using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CRCorrea;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmPecasEstoqueMan : Form
    {
        
        clsRequisicaoInfo clsRequisicaoInfo = new clsRequisicaoInfo();
        clsRequisicaoBLL clsRequisicaoBLL = new clsRequisicaoBLL();

        clsRequisicao1Info clsRequisicao1Info = new clsRequisicao1Info();
        clsRequisicao1BLL clsRequisicao1BLL = new clsRequisicao1BLL();

        clsPecasInfo clsPecasInfo = new clsPecasInfo();
        clsPecasBLL clsPecasBLL = new clsPecasBLL();


        String query;
        String filtro;

        DataTable dtPecas;

        Int32 idcodigo;
        Int32 idunidade;

        Int32 idclassifica;
        Int32 idclassifica1;
        Int32 idclassifica2;

        Int32 posicao = 0;
        public frmPecasEstoqueMan()
        {
            InitializeComponent();
        }

        public void Init()
        {
            clsRequisicaoBLL = new clsRequisicaoBLL();
            clsRequisicao1BLL = new clsRequisicao1BLL();

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", tbxCodigoDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", tbxCodigoAte);
        }

        private void frmPecasEstoqueMan_Load(object sender, EventArgs e)
        {

        }

        private void frmPecasEstoqueMan_Activated(object sender, EventArgs e)
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

        private void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null &&
                clsInfo.zrow.Index != -1)
            {

                if (clsInfo.znomegrid == btnCodigoDe.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxCodigoDe.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxCodigoNomeDe.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();

                        if (tbxCodigoAte.Text == "")
                        {
                            tbxCodigoAte.Text = tbxCodigoDe.Text;
                            tbxCodigoNomeAte.Text = tbxCodigoNomeDe.Text;
                        }
                    }
                    tbxCodigoDe.Select();
                }
                else if (clsInfo.znomegrid == btnCodigoAte.Name)
                {
                    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                    {
                        tbxCodigoAte.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                        tbxCodigoNomeAte.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                    }
                    tbxCodigoAte.Select();
                }
                else if (clsInfo.znomegrid == btnPecasGrupo.Name)
                {
                    idclassifica = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxClassificacaoDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECASCLASSIFICA where id = " + idclassifica);
                    tbxClassificacaoDe.Select();
                    tbxClassificacaoDe.SelectAll();
                }
                else if (clsInfo.znomegrid == btnPecasSubGrupo.Name)
                {
                    idclassifica1 = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxClassificacao1De.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECASCLASSIFICA1 where id = " + idclassifica1);
                    tbxClassificacao1De.Select();
                    tbxClassificacao1De.SelectAll();
                }
                else if (clsInfo.znomegrid == btnPecasItemSubGrupo.Name)
                {
                    idclassifica2 = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxClassificacao2De.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECASCLASSIFICA2 where id = " + idclassifica2);
                    tbxClassificacao2De.Select();
                    tbxClassificacao2De.SelectAll();
                }

            }
            else
            {
                if (ctl.Name == tbxCodigoDe.Name)
                {
                    if (tbxCodigoDe.Text.Trim().Length > 0)
                    {
                        tbxCodigoDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECAS WHERE CODIGO= '" + tbxCodigoDe.Text + "' ");
                        tbxCodigoNomeDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS WHERE CODIGO= '" + tbxCodigoDe.Text + "' ");
                        if (tbxCodigoAte.Text == "")
                        {
                            tbxCodigoAte.Text = tbxCodigoDe.Text;
                            tbxCodigoNomeAte.Text = tbxCodigoNomeDe.Text;
                        }

                    }
                    else
                    {
                        tbxCodigoDe.Text = "";
                        tbxCodigoNomeDe.Text = "";
                    }
                }
                if (ctl.Name == tbxCodigoAte.Name)
                {
                    if (tbxCodigoAte.Text.Trim().Length > 0)
                    {
                        tbxCodigoAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECAS WHERE CODIGO= '" + tbxCodigoAte.Text + "' ");
                        tbxCodigoNomeAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS WHERE CODIGO= '" + tbxCodigoAte.Text + "' ");
                        btnExecutar.Select();
                    }
                    else
                    {
                        tbxCodigoAte.Text = "";
                        tbxCodigoNomeAte.Text = "";
                    }
                }
            }
            clsInfo.znomegrid = "";
            clsInfo.zrow = null;           
            Calculos();           
        }

        private void Calculos()
        {
            if (tbxUnidade.Text == "KG" || tbxUnidade.Text == "LT")
            {
                tbxQtdeSaldo.Text = clsParser.DecimalParse(tbxQtdeSaldo.Text).ToString("N3");
                tbxQtdeAtual.Text = clsParser.DecimalParse(tbxQtdeAtual.Text).ToString("N3");
                tbxQtdeDiferenca.Text = (clsParser.DecimalParse(tbxQtdeAtual.Text) - clsParser.DecimalParse(tbxQtdeSaldo.Text)).ToString("N3");

            }
            else
            {
                tbxQtdeSaldo.Text = clsParser.DecimalParse(tbxQtdeSaldo.Text).ToString("N0");
                tbxQtdeAtual.Text = clsParser.DecimalParse(tbxQtdeAtual.Text).ToString("N0");
                tbxQtdeDiferenca.Text = (clsParser.DecimalParse(tbxQtdeAtual.Text) - clsParser.DecimalParse(tbxQtdeSaldo.Text)).ToString("N0");
            }
            if (clsParser.DecimalParse(tbxQtdeDiferenca.Text) > 0)
            {
                if ((clsParser.DecimalParse(tbxCustoMedioAtual.Text) - clsParser.DecimalParse(tbxCustoMedioReal.Text)) != 0)
                {
                    // Acumular o saldo com o novo valor de custo medio
                    Decimal qtdeprevista = (clsParser.DecimalParse(tbxQtdeAtual.Text) * clsParser.DecimalParse(tbxCustoMedioReal.Text));
                    qtdeprevista = qtdeprevista - clsParser.DecimalParse(tbxValorAcumulado.Text);
                    if (qtdeprevista >= 0)
                    {
                        tbxCustoMedioFuturo.Text = (Math.Round(qtdeprevista / clsParser.DecimalParse(tbxQtdeDiferenca.Text), 6)).ToString("N6");
                    }
                    else
                    {
                        MessageBox.Show("Calculo do Valor do Custo Médio não pode ser negativo !!");
                        tbxCustoMedioReal.Text = clsParser.DecimalParse(tbxCustoMedioAtual.Text).ToString("N4");
                        tbxCustoMedioFuturo.Text = 0.ToString("N6");
                    }

                }
                else
                {
                    tbxCustoMedioFuturo.Text = clsParser.DecimalParse(tbxCustoMedioAtual.Text).ToString("N6");
                }

            }
            else
            {
                tbxCustoMedioReal.Text = clsParser.DecimalParse(tbxCustoMedioAtual.Text).ToString("N4");
                tbxCustoMedioDiferenca.Text = "0";
                tbxCustoMedioFuturo.Text = "0";
            }
            // Custo Médio
            tbxCustoMedioAtual.Text = clsParser.DecimalParse(tbxCustoMedioAtual.Text).ToString("N4");
            tbxCustoMedioReal.Text = clsParser.DecimalParse(tbxCustoMedioReal.Text).ToString("N4");
            tbxCustoMedioFuturo.Text = clsParser.DecimalParse(tbxCustoMedioFuturo.Text).ToString("N6");
            tbxCustoMedioDiferenca.Text = 0.ToString("N4");
        }

        private void btnCodigoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCodigoDe.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(idcodigo);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);

        }

        private void btnCodigoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCodigoAte.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(idcodigo);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);

        }

        private void btnPecasGrupo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnPecasGrupo.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA", idclassifica, "Peças Classifica");

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }
        private void btnPecasSubGrupo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnPecasSubGrupo.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA1", idclassifica1, "IDCLASSIFICA", idclassifica.ToString());

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }
        private void btnPecasItemSubGrupo_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnPecasItemSubGrupo.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA2", idclassifica2, "IDCLASSIFICA1", idclassifica1.ToString());

            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            //calculos
            Calculos();

            //Rotina de Salvar aqui
            if (clsParser.DecimalParse(tbxQtdeDiferenca.Text) != 0)
            {
                // Localizar uma requisição de material aberta com a data de hoje pelo mesmo usuario que esta fazendo
                // esta manutenção
                String DataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                Int32 idRequisicao;
                query = "select * from requisicao where emitente = '" + clsInfo.zusuario + "' " +
                        " and requisicao.data >= " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true);

                idRequisicao = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select * from requisicao where emitente = '" + clsInfo.zusuario + "' " +
                               " and requisicao.data >= " + clsParser.SqlDateTimeFormat(DataAtual + " 00:00", true)));
                if (idRequisicao == 0)
                {  // Incluir uma nova requisição
                    clsRequisicaoInfo = new clsRequisicaoInfo();
                    clsRequisicaoInfo.ano = DateTime.Now.Year;
                    clsRequisicaoInfo.data = DateTime.Now;
                    clsRequisicaoInfo.emitente = clsInfo.zusuario;
                    clsRequisicaoInfo.filial = clsInfo.zfilial;
                    clsRequisicaoInfo.id = 0;
                    clsRequisicaoInfo.numero = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NUMERO from REQUISICAO ORDER BY DATA DESC, NUMERO DESC"));
                    clsRequisicaoInfo.numero += 1;
                    // Incluir
                    idRequisicao = clsRequisicaoBLL.Incluir(clsRequisicaoInfo, clsInfo.conexaosqldados);
                }
                clsRequisicaoInfo = new clsRequisicaoInfo();
                clsRequisicao1Info.dataentrega = DateTime.Now;
                clsRequisicao1Info.dataretorno = DateTime.Now;
                clsRequisicao1Info.funcionario = "0";
                clsRequisicao1Info.id = 0;
                clsRequisicao1Info.idcodigo = idcodigo;
                clsRequisicao1Info.idfuncionario = 0;
                clsRequisicao1Info.idmaquina = 0;
                clsRequisicao1Info.idordemservico = clsInfo.zordemservico;
                clsRequisicao1Info.idordemservicoop = 0;
                clsRequisicao1Info.idunidade = idunidade;
                if (clsParser.DecimalParse(tbxQtdeDiferenca.Text) > 0)
                {
                    clsRequisicao1Info.motivo = "E";
                    clsRequisicao1Info.qtde = clsParser.DecimalParse(tbxQtdeDiferenca.Text);
                }
                else
                {
                    clsRequisicao1Info.motivo = "S";
                    clsRequisicao1Info.qtde = clsParser.DecimalParse(tbxQtdeDiferenca.Text)*-1;
                }
                clsRequisicao1Info.numero = idRequisicao;
                clsRequisicao1Info.observar = "Acerto Manual";
                //clsRequisicao1Info.qtde = clsParser.DecimalParse(tbxQtdeDiferenca.Text);
                clsRequisicao1Info.qtdedev = 0;
                clsRequisicao1Info.qtdeentrega = 0;
                clsRequisicao1Info.qtdesaldo = 0;
                clsRequisicao1Info.tipo = "N";
                clsRequisicao1Info.valor = clsParser.DecimalParse(tbxCustoMedioFuturo.Text);
                clsRequisicao1Info.id = clsRequisicao1BLL.Incluir(clsRequisicao1Info, clsInfo.conexaosqldados);

            }
            //Depois de Salvar preenche a tela com a proxima linha do dtPecas

            if (posicao < dtPecas.Rows.Count - 1)
            {
                posicao = posicao + 1;

                idcodigo = clsParser.Int32Parse(dtPecas.Rows[posicao]["id"].ToString());
                tbxCodigo.Text = dtPecas.Rows[posicao]["codigo"].ToString();
                tbxNome.Text = dtPecas.Rows[posicao]["nome"].ToString();
                tbxLocacao.Text = dtPecas.Rows[posicao]["locacao"].ToString();
                tbxUnidade.Text = dtPecas.Rows[posicao]["unid"].ToString();
                /*Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade WHERE id= " +
                                          clsParser.Int32Parse(dtPecas.Rows[posicao]["idunidade"].ToString()) + " "); */
                idunidade = clsParser.Int32Parse(dtPecas.Rows[posicao]["idunidade"].ToString());
                if (tbxUnidade.Text == "KG" || tbxUnidade.Text == "LT")
                {
                    tbxQtdeSaldo.Text = clsParser.DecimalParse(dtPecas.Rows[posicao]["qtdesaldo"].ToString()).ToString("N3");

                }
                else
                {
                    tbxQtdeSaldo.Text = clsParser.DecimalParse(dtPecas.Rows[posicao]["qtdesaldo"].ToString()).ToString("N0");
                }
                tbxQtdeAtual.Text = tbxQtdeSaldo.Text;

                // Custo Médio 
                tbxCustoMedioAtual.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select customedio from movpecas where idcodigo=" + idcodigo + " order by data desc, operacao desc, id desc")).ToString("N4");
                tbxValorAcumulado.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valoracumulado from movpecas where idcodigo=" + idcodigo + " order by data desc, operacao desc, id desc")).ToString("N4"); 
                tbxCustoMedioReal.Text = tbxCustoMedioAtual.Text;
                tbxCustoMedioDiferenca.Text = 0.ToString("N4");
                // Ultima Entrada
                Int32 idmovpecas = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from movpecas where idcodigo=" + idcodigo + " and qtde > 0 and operacao = 'E' order by data desc, operacao desc, id desc"));
                if (idmovpecas > 0)
                {
                    labelUltimaCompra.Text = "Ultima Compra em " + clsParser.DateTimeParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select data from movpecas where idcodigo=" + idcodigo + " and qtde > 0 and operacao = 'E' order by data desc, operacao desc, id desc")).ToString("dd/MM/yyyy");
                    labelUltimaCompra.Text = labelUltimaCompra.Text + " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select documento from movpecas where idcodigo=" + idcodigo + " and qtde > 0 and operacao = 'E' order by data desc, operacao desc, id desc").ToString();
                    labelUltimaCompra.Text = labelUltimaCompra.Text + " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from movpecas where idcodigo=" + idcodigo + " and qtde > 0 and operacao = 'E' order by data desc, operacao desc, id desc").ToString();
                    labelUltimaCompra.Text = labelUltimaCompra.Text + " Qtde =" + clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select qtde from movpecas where idcodigo=" + idcodigo + " and qtde > 0 and operacao = 'E' order by data desc, operacao desc, id desc")).ToString("N3");
                    labelUltimaCompra.Text = labelUltimaCompra.Text + " Vl Unit R$ " + clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valor from movpecas where idcodigo=" + idcodigo + " and qtde > 0 and operacao = 'E' order by data desc, operacao desc, id desc")).ToString("N6");
                }
                else
                {
                    labelUltimaCompra.Text = "Nenhuma Entrada anterior !!!!";
                }



                tbxLocacao.Select();
                tbxLocacao.SelectAll();
            }
            else
            {
                MessageBox.Show("Processo concluido", "Aplisoft", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);

                gbxParametros.Enabled = true;
                gbxPecasItem.Visible = false;
                tspSalvar.Enabled = false;
            }
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {

        }

        private void tspAnterior_Click(object sender, EventArgs e)
        {

        }

        private void tspProximo_Click(object sender, EventArgs e)
        {

        }

        private void tspUltimo_Click(object sender, EventArgs e)
        {

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            gbxParametros.Enabled = false;
            gbxPecasItem.Visible = true;
            tspSalvar.Enabled = true;


            query = "select pecas.id, pecas.codigo, pecas.nome, pecas.idunidade, unidade.codigo as [unid], pecas.locacao, pecas.qtdesaldo, " +
                    "pecasclassifica.codigo as grupo, pecasclassifica1.codigo as subgrupo, pecasclassifica2.codigo as itemsubgrupo    " +
                    "from pecas " +
                    "left join unidade on unidade.id = pecas.idunidade " +
                    "left join pecasclassifica on pecasclassifica.id = pecas.idclassifica " +
                    "left join pecasclassifica1 on pecasclassifica1.id = pecas.idclassifica1 " +
                    "left join pecasclassifica2 on pecasclassifica2.id = pecas.idclassifica2 ";

            filtro = " where pecas.ativo = 'S' ";
            if (tbxCodigoDe.Text.Length > 0)
            {
                filtro += " and pecas.CODIGO >= '" + tbxCodigoDe.Text + "' ";
            }
            if (tbxCodigoAte.Text.Length > 0)
            {
                if (filtro.Length > 0)
                {
                    filtro = filtro + " AND pecas.CODIGO <= '" + tbxCodigoAte.Text + "' ";
                }                
                
            }
            query = query + filtro + " order by pecas.codigo ";
            SqlDataAdapter sda;
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            dtPecas = new DataTable();
            sda.Fill(dtPecas);            

            posicao = 0;

            //preencho a tela com primeira linha do dtPecas

            idcodigo = clsParser.Int32Parse(dtPecas.Rows[posicao]["id"].ToString());
            clsPecasInfo = clsPecasBLL.Carregar(idcodigo, clsInfo.conexaosqldados);

            pbxFoto.Image = clsPecasInfo.foto;
            tbxCodigo.Text = clsPecasInfo.codigo;// dtPecas.Rows[posicao]["codigo"].ToString();
            tbxNome.Text = clsPecasInfo.nome; //dtPecas.Rows[posicao]["nome"].ToString();
            tbxLocacao.Text = clsPecasInfo.locacao; // dtPecas.Rows[posicao]["locacao"].ToString();
            idunidade = clsPecasInfo.idunidade;//  clsParser.Int32Parse(dtPecas.Rows[posicao]["idunidade"].ToString());
            tbxUnidade.Text = dtPecas.Rows[posicao]["unid"].ToString();
            /*Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from unidade WHERE id= " +
                                      clsParser.Int32Parse(dtPecas.Rows[posicao]["idunidade"].ToString()) + " "); */
            if (tbxUnidade.Text == "KG" || tbxUnidade.Text == "LT")
            {
                //tbxQtdeSaldo.Text = clsParser.DecimalParse(dtPecas.Rows[posicao]["qtdesaldo"].ToString()).ToString("N3");
                tbxQtdeSaldo.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select top 1 movpecas.saldo from movpecas where movpecas.idcodigo = " + idcodigo + " order by data desc, operacao desc, id desc ", "0")).ToString("N3");
               
            }
            else
            {
                //tbxQtdeSaldo.Text = clsParser.DecimalParse(dtPecas.Rows[posicao]["qtdesaldo"].ToString()).ToString("N0");
                tbxQtdeSaldo.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select top 1 movpecas.saldo from movpecas where movpecas.idcodigo = " + idcodigo + " order by data desc, operacao desc, id desc ", "0")).ToString("N0");
            }
            tbxQtdeAtual.Text = tbxQtdeSaldo.Text;
            // Custo Médio 
            tbxCustoMedioAtual.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select customedio from movpecas where idcodigo=" + idcodigo + " order by data desc, operacao desc, id desc")).ToString("N4");
            tbxValorAcumulado.Text = clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valoracumulado from movpecas where idcodigo=" + idcodigo + " order by data desc, operacao desc, id desc")).ToString("N4"); 
            tbxCustoMedioReal.Text = tbxCustoMedioAtual.Text;
            tbxCustoMedioDiferenca.Text = 0.ToString("N4");
            tbxQtdeDiferenca.Text = "0";
            Int32 idmovpecas = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from movpecas where idcodigo=" + idcodigo + " and qtde > 0 and operacao = 'E' order by data desc, operacao desc, id desc"));
            if (idmovpecas > 0)
            {
                labelUltimaCompra.Text = "Ultima Compra em " + clsParser.DateTimeParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select data from movpecas where idcodigo=" + idcodigo + " and qtde > 0 and operacao = 'E' order by data desc, operacao desc, id desc")).ToString("dd/MM/yyyy");
                labelUltimaCompra.Text = labelUltimaCompra.Text + " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select documento from movpecas where idcodigo=" + idcodigo + " and qtde > 0 and operacao = 'E' order by data desc, operacao desc, id desc").ToString();
                labelUltimaCompra.Text = labelUltimaCompra.Text + " - " + Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from movpecas where idcodigo=" + idcodigo + " and qtde > 0 and operacao = 'E' order by data desc, operacao desc, id desc").ToString();
                labelUltimaCompra.Text = labelUltimaCompra.Text + " Qtde =" + clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select qtde from movpecas where idcodigo=" + idcodigo + " and qtde > 0 and operacao = 'E' order by data desc, operacao desc, id desc")).ToString("N3");
                labelUltimaCompra.Text = labelUltimaCompra.Text + " Vl Unit R$ " + clsParser.DecimalParse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select valor from movpecas where idcodigo=" + idcodigo + " and qtde > 0 and operacao = 'E' order by data desc, operacao desc, id desc")).ToString("N6");
            }
            else
            {
                labelUltimaCompra.Text = "Nenhuma Entrada anterior !!!!";
            }
            
            tbxLocacao.Select();
            tbxLocacao.SelectAll(); 
        }

        private void gbxEstoqueManual_Enter(object sender, EventArgs e)
        {

        }

        private void tbxQtdeAtual_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

