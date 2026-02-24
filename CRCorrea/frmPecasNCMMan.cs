using CRCorrea;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmPecasNCMMan : Form
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
        Int32 xQtde = 0;
        public frmPecasNCMMan()
        {
            InitializeComponent();
        }

        public void Init()
        {
            clsRequisicaoBLL = new clsRequisicaoBLL();
            clsRequisicao1BLL = new clsRequisicao1BLL();

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", tbxCodigoDe);
            //clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from pecas order by codigo", tbxCodigoAte);
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

                        //if (tbxCodigoAte.Text == "")
                        //{
                        //    tbxCodigoAte.Text = tbxCodigoDe.Text;
                        //    tbxCodigoNomeAte.Text = tbxCodigoNomeDe.Text;
                        //}
                    }
                    tbxCodigoDe.Select();
                }
                //else if (clsInfo.znomegrid == btnCodigoAte.Name)
                //{
                //    if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                //    {
                //        tbxCodigoAte.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString().Trim();
                //        tbxCodigoNomeAte.Text = clsInfo.zrow.Cells["NOME"].Value.ToString().Trim();
                //    }
                //    tbxCodigoAte.Select();
                //}
                else if (clsInfo.znomegrid == btnClassfiscal.Name)
                {
                    idclassifica = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxClassificacaoDe.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxClassificacaoDe.Select();
                    tbxClassificacaoDe.SelectAll();

                }
                //else if (clsInfo.znomegrid == btnPecasSubGrupo.Name)
                //{
                //    idclassifica1 = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                //    tbxClassificacao1De.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECASCLASSIFICA1 where id = " + idclassifica1);
                //    tbxClassificacao1De.Select();
                //    tbxClassificacao1De.SelectAll();
                //}
                //else if (clsInfo.znomegrid == btnPecasItemSubGrupo.Name)
                //{
                //    idclassifica2 = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                //    tbxClassificacao2De.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECASCLASSIFICA2 where id = " + idclassifica2);
                //    tbxClassificacao2De.Select();
                //    tbxClassificacao2De.SelectAll();
                //}

            }
            else
            {
                if (ctl.Name == tbxCodigoDe.Name)
                {
                    if (tbxCodigoDe.Text.Trim().Length > 0)
                    {
                        tbxCodigoDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECAS WHERE CODIGO= '" + tbxCodigoDe.Text + "' ");
                        tbxCodigoNomeDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS WHERE CODIGO= '" + tbxCodigoDe.Text + "' ");
                        //if (tbxCodigoAte.Text == "")
                        //{
                        //    tbxCodigoAte.Text = tbxCodigoDe.Text;
                        //    tbxCodigoNomeAte.Text = tbxCodigoNomeDe.Text;
                        //}

                    }
                    else
                    {
                        tbxCodigoDe.Text = "";
                        tbxCodigoNomeDe.Text = "";
                    }
                }
                else if (ctl.Name == tbxClassificacaoDe.Name)
                {
                    idclassifica = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from IPI where CODIGO='" + tbxClassificacaoDe.Text + "'"));
                    tbxClassificacaoDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from IPI where id = " + idclassifica);

                }

                //if (ctl.Name == tbxCodigoAte.Name)
                //{
                //    if (tbxCodigoAte.Text.Trim().Length > 0)
                //    {
                //        tbxCodigoAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECAS WHERE CODIGO= '" + tbxCodigoAte.Text + "' ");
                //        tbxCodigoNomeAte.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOME from PECAS WHERE CODIGO= '" + tbxCodigoAte.Text + "' ");
                //        btnExecutar.Select();
                //    }
                //    else
                //    {
                //        tbxCodigoAte.Text = "";
                //        tbxCodigoNomeAte.Text = "";
                //    }
                //}
            }
            clsInfo.znomegrid = "";
            clsInfo.zrow = null;           
        }


        private void btnCodigoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCodigoDe.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(idcodigo);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);

        }

        //private void btnCodigoAte_Click(object sender, EventArgs e)
        //{
        //    clsInfo.znomegrid = btnCodigoAte.Name;
        //    frmPecasPes frmPecasPes = new frmPecasPes();
        //    frmPecasPes.Init(idcodigo);
        //    clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);

        //}

        private void btnClassfiscal_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnClassfiscal.Name;
            frmIpiPes frmIpiPes = new frmIpiPes();
            frmIpiPes.Init(0);
            clsFormHelper.AbrirForm(this.MdiParent, frmIpiPes, clsInfo.conexaosqldados);
        }
        //private void btnPecasSubGrupo_Click(object sender, EventArgs e)
        //{
        //    clsInfo.znomegrid = btnPecasSubGrupo.Name;
        //    frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
        //    frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA1", idclassifica1, "IDCLASSIFICA", idclassifica.ToString());

        //    clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        //}
        //private void btnPecasItemSubGrupo_Click(object sender, EventArgs e)
        //{
        //    clsInfo.znomegrid = btnPecasItemSubGrupo.Name;
        //    frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
        //    frmEscFormVisPes.Init(clsInfo.conexaosqldados, "PECASCLASSIFICA2", idclassifica2, "IDCLASSIFICA1", idclassifica1.ToString());

        //    clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        //}

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            this.Close();
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
            if (idclassifica > 0)
            {

                xQtde = tbxNomeBusca.Text.Trim().Length;


                gbxParametros.Enabled = false;
                tspSalvar.Enabled = true;

                query = "select pecas.id, pecas.codigo, pecas.nome, pecas.idunidade, unidade.codigo as [unid], pecas.locacao, pecas.qtdesaldo " +
                        "from pecas " +
                        "left join unidade on unidade.id = pecas.idunidade " +
                        "where SUBSTRING(pecas.nome,1," + xQtde + ") = '" + tbxNomeBusca.Text.Substring(0, xQtde) + "' ";
                query = query + filtro + " order by pecas.nome ";
                SqlDataAdapter sda;
                sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                dtPecas = new DataTable();
                sda.Fill(dtPecas);


                //preencho a tela com primeira linha do dtPecas
                foreach (DataRow row in dtPecas.Rows)
                {
                    idcodigo = clsParser.Int32Parse(row["id"].ToString());
                    clsPecasInfo = clsPecasBLL.Carregar(idcodigo, clsInfo.conexaosqldados);
                    clsPecasInfo.idipi = idclassifica;
                    clsPecasBLL.Alterar(clsPecasInfo, clsInfo.conexaosqldados);
                }
                MessageBox.Show("Rotina executada com Sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);

                gbxParametros.Enabled = true;
                tspSalvar.Enabled = false;
                tbxCodigoDe.Text = "";
                tbxCodigoNomeDe.Text = "";
                tbxNomeBusca.Text = "";
                tbxClassificacaoDe.Text = "";

            }
            else
            {
                MessageBox.Show("Informe a Classificação Fiscal para executar a rotina.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void gbxEstoqueManual_Enter(object sender, EventArgs e)
        {

        }

        private void tbxQtdeAtual_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

