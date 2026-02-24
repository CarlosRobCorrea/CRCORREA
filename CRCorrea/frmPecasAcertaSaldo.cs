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
    public partial class frmPecasAcertaSaldo : Form
    {
        
        clsRequisicaoInfo clsRequisicaoInfo = new clsRequisicaoInfo();
        clsRequisicaoBLL clsRequisicaoBLL = new clsRequisicaoBLL();

        clsRequisicao1Info clsRequisicao1Info = new clsRequisicao1Info();
        clsRequisicao1BLL clsRequisicao1BLL = new clsRequisicao1BLL();

        clsPecasInfo clsPecasInfo = new clsPecasInfo();
        clsPecasBLL clsPecasBLL = new clsPecasBLL();
        clsMovPecasBLL clsMovPecasBLL = new clsMovPecasBLL();
        
        String query;
        String filtro;

        DataTable dtPecas;

        Int32 idcodigo;
        Int32 idunidade;

        Int32 idclassifica;
        Int32 idclassifica1;
        Int32 idclassifica2;

        Int32 posicao = 0;
        public frmPecasAcertaSaldo()
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
                        idcodigo = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                        MostraItem();

                        //if (tbxCodigoAte.Text == "")
                        //{
                        //    tbxCodigoAte.Text = tbxCodigoDe.Text;
                        //    tbxCodigoNomeAte.Text = tbxCodigoNomeDe.Text;
                        //}
                    }
                    btnExecutar.Select();
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
                //else if (clsInfo.znomegrid == btnPecasGrupo.Name)
                //{
                //    idclassifica = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                //    tbxClassificacaoDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECASCLASSIFICA where id = " + idclassifica);
                //    tbxClassificacaoDe.Select();
                //    tbxClassificacaoDe.SelectAll();
                //}
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
                        idcodigo = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from PECAS WHERE CODIGO= '" + tbxCodigoDe.Text + "' "));
                        MostraItem();

                        //tbxCodigoDe.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from PECAS WHERE CODIGO= '" + tbxCodigoDe.Text + "' ");
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
        private void MostraItem()
        {
            clsPecasInfo = clsPecasBLL.Carregar(idcodigo, clsInfo.conexaosqldados);
            pbxFoto.Image = clsPecasInfo.foto;
            tbxCodigoDe.Text = clsPecasInfo.codigo;// dtPecas.Rows[posicao]["codigo"].ToString();
            tbxCodigoNomeDe.Text = clsPecasInfo.nome; //dtPecas.Rows[posicao]["nome"].ToString();
            idunidade = clsPecasInfo.idunidade;//  clsParser.Int32Parse(dtPecas.Rows[posicao]["idunidade"].ToString());
            tbxQtdeSaldo.Text = clsPecasInfo.qtdesaldo.ToString("N2");
        }

        private void btnCodigoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCodigoDe.Name;
            frmPecasPes frmPecasPes = new frmPecasPes();
            frmPecasPes.Init(idcodigo);
            clsFormHelper.AbrirForm(this.MdiParent, frmPecasPes, clsInfo.conexaosqldados);

        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            //calculos
            //Calculos();

            //Depois de Salvar preenche a tela com a proxima linha do dtPecas

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
            
            clsMovPecasBLL.AtualizarCadastroItem(idcodigo, clsInfo.conexaosqldados, DateTime.Now,"","", 0);

            tbxQtdeSaldoAtual.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select qtdesaldo from PECAS where id = " + idcodigo);
        }

        private void gbxEstoqueManual_Enter(object sender, EventArgs e)
        {

        }

    }
}

