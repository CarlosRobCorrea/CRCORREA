using CRCorreaFuncoes;
using System;
using System.ComponentModel;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmDizeresNFV : Form
    {

        private Boolean ok;

        private DataGridView dgvDizeresNFV_fil;
        private DataGridView dgvDizeresNFV_compl;

        private String[] valoresantigos = new String[] { };
        private String[] valoresatuais = new String[] { };

        private delegate void AddnewDelegate(TextBox textbox, TextBox colecao);

        private DialogResult resultado = new DialogResult();   
        
        
        

        private Int32 idDizeresNFVX;
        private DataTable dtDizeresNFVaux;
        private DataTable dtGeral;

        private String tipo = "S";
        private String destinatario = "C";
        private String ipiincide = "N";
        private String icmsporcentagem = "P";
        private String pisporcentagem = "P";
        private String cofinsporcetagem = "P";

        private Int32  idtribicms;
        private Int32 idtribipi;
        private Int32 idtribcofins;
        private Int32 idtribpis;

        private Int32 idicms;
        private Int32 idipi;
        private Int32 idpis;
        private Int32 idcofins;


        private Boolean leaveNocheck;

        public frmDizeresNFV()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridView _dgvDizeresNFV_fil,
                         DataGridView _dgvDizeresNFV_compl)
        {
            frmDizeresNFVVis.indexDizeresNFVGeral = 0;

            leaveNocheck = false;
            idDizeresNFVX = _id;
            dgvDizeresNFV_fil = _dgvDizeresNFV_fil;
            dgvDizeresNFV_compl = _dgvDizeresNFV_compl;

        }

        private void frmDizeresNFV_Load(object sender, EventArgs e)
        {
            CarregaDizeresNFV(idDizeresNFVX);
        }

        private void CarregaDizeresNFV(Int32 _id)
        {
            try
            {
                //Carrega os Dados 
                tspDizeresNFV.Cursor = Cursors.Hand;
                PreencheCamposDizeresNFV();
                valoresantigos = PreencheValoresDizeresNFV();
                if (_id == 0)
                {                  
                    tspPrimeiro.Enabled = false;
                    tspAnterior.Enabled = false;
                    tspProximo.Enabled = false;
                    tspUltimo.Enabled = false;
                }
                tbxCodigo.Select();
                tbxCodigo.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Carrega DizeresNFV - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                leaveNocheck = true;
            }
        }

        private void PreencheCamposDizeresNFV()
        {
            if (idDizeresNFVX > 0)
            {
                foreach (DataRow dgvrDizeresNFV in ((DataTable)dgvDizeresNFV_compl.DataSource).Rows)
                {
                    if (dgvrDizeresNFV.RowState != DataRowState.Deleted)
                    {
                        if (clsParser.Int32Parse(dgvrDizeresNFV["ID"].ToString()) == idDizeresNFVX)
                        {
                            tbxCodigo.Text = dgvrDizeresNFV["CODIGO"].ToString();
                            tbxNome.Text = dgvrDizeresNFV["NOME"].ToString();

                            dtDizeresNFVaux = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                                "DIZERESNFV",
                                "ID,CODIGO,NOME,LIN01,LIN02,LIN03,LIN04,LIN05,LIN06,LIN07," +
                                "LIN08,TIPOSAIDA,DESTINATARIO,IDICMS,ICMSTIPO,REDUCAOICMS," +
                                "INDICEIVAICMS,IDTRIBICMS,ICMSDIFERENCIADO,IDIPI,IPIINCIDE," +
                                "REDUCAOIPI,IDTRIBIPI,IDPIS,PISTIPO,REDUCAOPIS,INDICEIVAPIS," +
                                "IDTRIBPIS,PISDIFERENCIADO,IDCOFINS,COFINSTIPO,REDUCAOCOFINS," +
                                "INDICEIVACOFINS,IDTRIBCOFINS,COFINSDIFERENCIADO",
                                "ID = " + clsParser.Int32Parse(dgvrDizeresNFV["ID"].ToString()),
                                "ID");
                            if (dtDizeresNFVaux.Rows.Count > 0)
                            {
                                tbxLin01.Text = dtDizeresNFVaux.Rows[0]["LIN01"].ToString();
                                tbxLin02.Text = dtDizeresNFVaux.Rows[0]["LIN02"].ToString();
                                tbxLin03.Text = dtDizeresNFVaux.Rows[0]["LIN03"].ToString();
                                tbxLin04.Text = dtDizeresNFVaux.Rows[0]["LIN04"].ToString();
                                tbxLin05.Text = dtDizeresNFVaux.Rows[0]["LIN05"].ToString();
                                tbxLin06.Text = dtDizeresNFVaux.Rows[0]["LIN06"].ToString();
                                tbxLin07.Text = dtDizeresNFVaux.Rows[0]["LIN07"].ToString();
                                tbxLin08.Text = dtDizeresNFVaux.Rows[0]["LIN08"].ToString();
                                ////
                                if (dtDizeresNFVaux.Rows[0]["TIPOSAIDA"].ToString().ToUpper() == "S")
                                {
                                    rbntiposaidaS.Checked = true;
                                    tipo = "S";
                                }
                                if (dtDizeresNFVaux.Rows[0]["TIPOSAIDA"].ToString().ToUpper() == "E")
                                {
                                    rbntiposaidaE.Checked = true;
                                    tipo = "E";
                                }
                                if (dtDizeresNFVaux.Rows[0]["TIPOSAIDA"].ToString().ToUpper() == "M")
                                {
                                    rbntiposaidaM.Checked = true;
                                    tipo = "M";
                                }
                                ////
                                if (dtDizeresNFVaux.Rows[0]["DESTINATARIO"].ToString().ToUpper() == "C")
                                {
                                    rbnDestinatarioC.Checked = true;
                                    destinatario = "C";
                                }
                                if (dtDizeresNFVaux.Rows[0]["DESTINATARIO"].ToString().ToUpper() == "D")
                                {
                                    rbnDestinatarioD.Checked = true;
                                    destinatario = "D";
                                }
                                // icms
                                if (dtDizeresNFVaux.Rows[0]["ICMSTIPO"].ToString().ToUpper() == "P")
                                {
                                    rbnIcmsPorc.Checked = true;
                                    icmsporcentagem = "P";
                                }
                                if (dtDizeresNFVaux.Rows[0]["ICMSTIPO"].ToString().ToUpper() == "V")
                                {
                                    rbnIcmsVal.Checked = true;
                                    icmsporcentagem = "V";
                                }
                                // ipi
                                if (dtDizeresNFVaux.Rows[0]["IPIINCIDE"].ToString().ToUpper() == "S")
                                {
                                    rbnIpiIncideS.Checked = true;
                                    ipiincide = "S";
                                }
                                if (dtDizeresNFVaux.Rows[0]["IPIINCIDE"].ToString().ToUpper() == "N")
                                {
                                    rbnIpiIncideN.Checked = true;
                                    ipiincide = "N";
                                }
                                //pis
                                if (dtDizeresNFVaux.Rows[0]["PISTIPO"].ToString().ToUpper() == "P")
                                {
                                    rbnPisPorc.Checked = true;
                                    pisporcentagem = "P";
                                }
                                if (dtDizeresNFVaux.Rows[0]["PISTIPO"].ToString().ToUpper() == "V")
                                {
                                    rbnPisVal.Checked = true;
                                    pisporcentagem = "V";
                                }
                                //cofins
                                if (dtDizeresNFVaux.Rows[0]["COFINSTIPO"].ToString().ToUpper() == "P")
                                {
                                    rbnCofinsPorc.Checked = true;
                                    pisporcentagem = "P";
                                }
                                if (dtDizeresNFVaux.Rows[0]["COFINSTIPO"].ToString().ToUpper() == "V")
                                {
                                    rbnCofinsVal.Checked = true;
                                    pisporcentagem = "V";
                                }
                                //
                                tbxReducaoIcms.Text = clsParser.DecimalParse(dtDizeresNFVaux.Rows[0]["REDUCAOICMS"].ToString()).ToString();
                                tbxIndiceIVAIcms.Text = clsParser.DecimalParse(dtDizeresNFVaux.Rows[0]["INDICEIVAICMS"].ToString()).ToString();
                                tbxIcmsDiferenciado.Text = clsParser.DecimalParse(dtDizeresNFVaux.Rows[0]["ICMSDIFERENCIADO"].ToString()).ToString();
                                //
                                tbxReducaoIPI.Text = clsParser.DecimalParse(dtDizeresNFVaux.Rows[0]["REDUCAOIPI"].ToString()).ToString();
                                //
                                tbxReducaoPIS.Text = clsParser.DecimalParse(dtDizeresNFVaux.Rows[0]["REDUCAOPIS"].ToString()).ToString();
                                tbxIndiceIVAPIS.Text = clsParser.DecimalParse(dtDizeresNFVaux.Rows[0]["INDICEIVAPIS"].ToString()).ToString();
                                tbxPisDiferenciado.Text = clsParser.DecimalParse(dtDizeresNFVaux.Rows[0]["PISDIFERENCIADO"].ToString()).ToString();
                                //                               
                                tbxReducaoCofins.Text = clsParser.DecimalParse(dtDizeresNFVaux.Rows[0]["REDUCAOCOFINS"].ToString()).ToString();
                                tbxIndiceIVACOFINS.Text = clsParser.DecimalParse(dtDizeresNFVaux.Rows[0]["INDICEIVACOFINS"].ToString()).ToString();
                                tbxCofinsDiferenciado.Text = clsParser.DecimalParse(dtDizeresNFVaux.Rows[0]["COFINSDIFERENCIADO"].ToString()).ToString();
                                //// tribicms
                                dtGeral = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                                 "SITTRIB",
                                 "ID,CODIGO,NOME",
                                 "ID = " + clsParser.Int32Parse(dtDizeresNFVaux.Rows[0]["IDTRIBICMS"].ToString()),
                                 "ID");
                                if (dtGeral.Rows.Count > 0)
                                {
                                    idtribicms = clsParser.Int32Parse(dtGeral.Rows[0]["ID"].ToString());
                                    tbxTribIcms.Text = dtGeral.Rows[0]["NOME"].ToString();
                                }
                                //// tribpis
                                dtGeral = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                                 "SITTRIB",
                                 "ID,CODIGO,NOME",
                                 "ID = " + clsParser.Int32Parse(dtDizeresNFVaux.Rows[0]["IDTRIBPIS"].ToString()),
                                 "ID");
                                if (dtGeral.Rows.Count > 0)
                                {
                                    idtribpis = clsParser.Int32Parse(dtGeral.Rows[0]["ID"].ToString());
                                    tbxTribPIS.Text = dtGeral.Rows[0]["NOME"].ToString();
                                }
                                //// tribipi
                                dtGeral = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                                 "SITTRIB",
                                 "ID,CODIGO,NOME",
                                 "ID = " + clsParser.Int32Parse(dtDizeresNFVaux.Rows[0]["IDTRIBIPI"].ToString()),
                                 "ID");
                                if (dtGeral.Rows.Count > 0)
                                {
                                    idtribipi = clsParser.Int32Parse(dtGeral.Rows[0]["ID"].ToString());
                                    tbxTribIPI.Text = dtGeral.Rows[0]["NOME"].ToString();
                                }
                                //// tribcofins
                                dtGeral = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                                 "SITTRIB",
                                 "ID,CODIGO,NOME",
                                 "ID = " + clsParser.Int32Parse(dtDizeresNFVaux.Rows[0]["IDTRIBCOFINS"].ToString()),
                                 "ID");
                                if (dtGeral.Rows.Count > 0)
                                {
                                    idtribcofins = clsParser.Int32Parse(dtGeral.Rows[0]["ID"].ToString());
                                    tbxTribCofins.Text = dtGeral.Rows[0]["NOME"].ToString();
                                }
                                //// sittribicms
                                dtGeral = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                                 "SITTRIBUTARIAB",
                                 "ID,CODIGO,NOME",
                                 "ID = " + clsParser.Int32Parse(dtDizeresNFVaux.Rows[0]["IDICMS"].ToString()),
                                 "ID");
                                if (dtGeral.Rows.Count > 0)
                                {
                                    idicms = clsParser.Int32Parse(dtGeral.Rows[0]["ID"].ToString());
                                    tbxSitTribIcmsB.Text = dtGeral.Rows[0]["CODIGO"].ToString() + " - " + dtGeral.Rows[0]["NOME"].ToString();
                                }
                                //// sittribipi
                                dtGeral = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                                 "SITTRIBIPI",
                                 "ID,CODIGO,NOME",
                                 "ID = " + clsParser.Int32Parse(dtDizeresNFVaux.Rows[0]["IDIPI"].ToString()),
                                 "ID");
                                if (dtGeral.Rows.Count > 0)
                                {
                                    idipi = clsParser.Int32Parse(dtGeral.Rows[0]["ID"].ToString());
                                    tbxSitTribIpi.Text = dtGeral.Rows[0]["CODIGO"].ToString() + " - " + dtGeral.Rows[0]["NOME"].ToString();
                                }
                                //// sittribpis
                                dtGeral = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                                 "SITTRIBPIS",
                                 "ID,CODIGO,NOME",
                                 "ID = " + clsParser.Int32Parse(dtDizeresNFVaux.Rows[0]["IDPIS"].ToString()),
                                 "ID");
                                if (dtGeral.Rows.Count > 0)
                                {
                                    idpis = clsParser.Int32Parse(dtGeral.Rows[0]["ID"].ToString());
                                    tbxSitTribPIS.Text = dtGeral.Rows[0]["CODIGO"].ToString() + " - " + dtGeral.Rows[0]["NOME"].ToString();
                                }
                                //// sittribcofins
                                dtGeral = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                                 "SITTRIBCOFINS",
                                 "ID,CODIGO,NOME",
                                 "ID = " + clsParser.Int32Parse(dtDizeresNFVaux.Rows[0]["IDCOFINS"].ToString()),
                                 "ID");
                                if (dtGeral.Rows.Count > 0)
                                {
                                    idcofins = clsParser.Int32Parse(dtGeral.Rows[0]["ID"].ToString());
                                    tbxSitTribCofins.Text = dtGeral.Rows[0]["CODIGO"].ToString() + " - " + dtGeral.Rows[0]["NOME"].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        private String[] PreencheValoresDizeresNFV()
        {
            String[] _valores = {tbxCodigo.Text,                               
                                tbxNome.Text,
                                tbxLin01.Text,
                                tbxLin02.Text,
                                tbxLin03.Text,
                                tbxLin04.Text,
                                tbxLin05.Text,
                                tbxLin06.Text,
                                tbxLin07.Text,
                                tbxLin08.Text,
                                tipo.ToString(),
                                destinatario.ToString(),
                                icmsporcentagem.ToString(),
                                pisporcentagem.ToString(),
                                ipiincide.ToString(),
                                cofinsporcetagem.ToString(),
                                tbxReducaoIcms.Text,
                                tbxIndiceIVAIcms.Text,
                                tbxIcmsDiferenciado.Text,
                                tbxReducaoIPI.Text,
                                tbxReducaoPIS.Text,
                                tbxIndiceIVAPIS.Text,
                                tbxPisDiferenciado.Text,
                                tbxReducaoCofins.Text,
                                tbxIndiceIVACOFINS.Text,
                                tbxCofinsDiferenciado.Text,
                                idtribicms.ToString(),
                                idtribipi.ToString(),
                                idtribpis.ToString(),
                                idtribcofins.ToString(),
                                idicms.ToString(),
                                idipi.ToString(),
                                idpis.ToString(),
                                idcofins.ToString()};

            return _valores;
        }

        private void ControlKeyKeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisual.ControlKeyPressNumero((TextBox)sender, e, false);
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
            if (sender is TextBox)
            {
                ((TextBox)sender).SelectionLength = 0;
                ((TextBox)sender).SelectionStart = 0;
            }
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            if (leaveNocheck == false)
            {
                clsVisual.FormatarCampoNumerico(sender); 
                clsVisual.ControlLeave(sender);
            }
            else
            {
                leaveNocheck = false;
                clsVisual.FormatarCampoNumerico(sender); 
                clsVisual.ControlLeave(sender);
            }
            if (sender is TextBox)
            {

                if (((TextBox)sender).Name == "tbxTribIcms")
                {
                    idtribicms = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIB where NOME = '"+ tbxTribIcms.Text+"'","0"));
                    //idtribicms = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITTRIB", "ID", "NOME", tbxTribIcms.Text).ToString());
                    if (idtribicms <= 0)
                    {
                        tbxTribIcms.Text = "";
                    }
                }
                if (((TextBox)sender).Name == "tbxTribIPI")
                {
                    idtribipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIB where NOME = '" + tbxTribIPI.Text + "'", "0"));
                    //idtribipi = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITTRIB", "ID", "NOME", tbxTribIPI.Text).ToString());
                    if (idtribipi <= 0)
                    {
                        tbxTribIPI.Text = "";
                    }
                }
                if (((TextBox)sender).Name == "tbxTribPIS")
                {
                    idtribpis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIB where NOME = '" + tbxTribPIS.Text + "'", "0"));
                    //idtribpis = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITTRIB", "ID", "NOME", tbxTribPIS.Text).ToString());
                    if (idtribpis <= 0)
                    {
                        tbxTribPIS.Text = "";
                    }
                }
                if (((TextBox)sender).Name == "tbxTribCofins")
                {
                    idtribcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIB where NOME = '" + tbxTribCofins.Text + "'", "0"));
                    //idtribcofins = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITTRIB", "ID", "NOME", tbxTribCofins.Text).ToString());
                    if (idtribcofins <= 0)
                    {
                        tbxTribCofins.Text = "";
                    }
                }  
                //
                if (((TextBox)sender).Name == "tbxSitTribIcmsB")
                {
                    idicms = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBUTARIAB where (CODIGO + ' - ' + NOME) = '" + tbxSitTribIcmsB.Text + "'", "0"));
                    //idicms = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITTRIBUTARIAB", "ID", "(CODIGO + ' - ' + NOME)", tbxSitTribIcmsB.Text).ToString());
                    if (idicms <= 0)
                    {
                        tbxSitTribIcmsB.Text = "";
                    }
                }
                if (((TextBox)sender).Name == "tbxSitTribIpi")
                {
                    idipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBIPI where (CODIGO + ' - ' + NOME) = '" + tbxSitTribIpi.Text + "'", "0"));
                    //idipi = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITTRIBIPI", "ID", "(CODIGO + ' - ' + NOME)", tbxSitTribIpi.Text).ToString());
                    if (idipi <= 0)
                    {
                        tbxSitTribIpi.Text = "";
                    }
                }
                if (((TextBox)sender).Name == "tbxSitTribPIS")
                {
                    idpis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBPIS where (CODIGO + ' - ' + NOME) = '" + tbxSitTribPIS.Text + "'", "0"));
                    //idpis = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITTRIBPIS", "ID", "(CODIGO + ' - ' + NOME)", tbxSitTribPIS.Text).ToString());
                    if (idpis <= 0)
                    {
                        tbxSitTribPIS.Text = "";
                    }
                }
                if (((TextBox)sender).Name == "tbxSitTribCofins")
                {
                    idcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from SITTRIBCOFINS where (CODIGO + ' - ' + NOME) = '" + tbxSitTribCofins.Text + "'", "0"));
                    //idcofins = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "SITTRIBCOFINS", "ID", "(CODIGO + ' - ' + NOME)", tbxSitTribCofins.Text).ToString());
                    if (idcofins <= 0)
                    {
                        tbxSitTribCofins.Text = "";
                    }
                }

                ((TextBox)sender).SelectionLength = 0;
                ((TextBox)sender).SelectionStart = 0;
            }
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void frmDizeresNFV_Shown(object sender, EventArgs e)
        {
            
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                resultado = DialogResult.No;
                valoresatuais = PreencheValoresDizeresNFV();
                if (clsSelectInsertUpdateBLL.ComparaValores(valoresatuais, valoresantigos) == false)
                {
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

                    if (resultado == DialogResult.Yes)
                    {
                        using (TransactionScope tsDizeresNFV = new TransactionScope())
                        {
                            SalvarDizeresNFV();
                            tsDizeresNFV.Complete();
                            tsDizeresNFV.Dispose();
                        }
                    }
                    else if (resultado == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            finally
            {
                if (resultado != DialogResult.Cancel)
                {
                    if (idDizeresNFVX > 0)
                    {
                        frmDizeresNFVVis.indexDizeresNFVGeral = idDizeresNFVX;
                    }
                    leaveNocheck = true;
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft",
                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button1);

                if (resultado == DialogResult.Yes)
                {
                    using (TransactionScope tsDizeresNFV = new TransactionScope())
                    {
                        SalvarDizeresNFV();
                        tsDizeresNFV.Complete();
                        tsDizeresNFV.Dispose();
                    }
                }
                else if (resultado == DialogResult.Cancel)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                if (resultado != DialogResult.Cancel)
                {
                    if (idDizeresNFVX > 0)
                    {
                        frmDizeresNFVVis.indexDizeresNFVGeral = idDizeresNFVX; ;
                    }
                    leaveNocheck = true;
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void SalvarDizeresNFV()
        {
            if (VerificaGravacaoDizeresNFV("TODOS") == true)
            {
                if (idDizeresNFVX == 0)    // Incluindo os valores do Cabeçalho -- valores locais
                {
                    idDizeresNFVX = clsSelectInsertUpdateBLL.Insert(clsInfo.conexaosqldados, "DizeresNFV",
                                                                            "CODIGO," +                                                                           
                                                                            "NOME," +                                                                            
                                                                            "LIN01," +
                                                                            "LIN02," +
                                                                            "LIN03," +
                                                                            "LIN04," +
                                                                            "LIN05," +
                                                                            "LIN06," +
                                                                            "LIN07," +
                                                                            "LIN08," +
                                                                            "TIPOSAIDA," +
                                                                            "DESTINATARIO," +
                                                                            "ICMSTIPO," +
                                                                            "IPIINCIDE," +
                                                                            "PISTIPO," +
                                                                            "COFINSTIPO," +
                                                                            "REDUCAOICMS," +
                                                                            "INDICEIVAICMS," +
                                                                            "ICMSDIFERENCIADO," +
                                                                            "REDUCAOIPI," +
                                                                            "REDUCAOPIS," +
                                                                            "INDICEIVAPIS," +
                                                                            "PISDIFERENCIADO," +
                                                                            "REDUCAOCOFINS," +
                                                                            "INDICEIVACOFINS," +
                                                                            "COFINSDIFERENCIADO," +
                                                                            "IDTRIBICMS," +
                                                                            "IDTRIBIPI," +
                                                                            "IDTRIBPIS," +
                                                                            "IDTRIBCOFINS," + 
                                                                            "IDICMS," +
                                                                            "IDIPI," +
                                                                            "IDPIS," +
                                                                            "IDCOFINS", 
                                                                            clsParser.SqlStringFormat(tbxCodigo.Text, false, "CODIGO") +
                                                                            clsParser.SqlStringFormat(tbxNome.Text, false, "NOME") +
                                                                            clsParser.SqlStringFormat(tbxLin01.Text, false, "LIN01") +
                                                                            clsParser.SqlStringFormat(tbxLin02.Text, false, "LIN02") +
                                                                            clsParser.SqlStringFormat(tbxLin03.Text, false, "LIN03") +
                                                                            clsParser.SqlStringFormat(tbxLin04.Text, false, "LIN04") +
                                                                            clsParser.SqlStringFormat(tbxLin05.Text, false, "LIN05") +
                                                                            clsParser.SqlStringFormat(tbxLin06.Text, false, "LIN06") +
                                                                            clsParser.SqlStringFormat(tbxLin07.Text, false, "LIN07") +
                                                                            clsParser.SqlStringFormat(tbxLin08.Text, false, "LIN08") +
                                                                            clsParser.SqlStringFormat(tipo.ToString(), false, "TIPOSAIDA") +
                                                                            clsParser.SqlStringFormat(destinatario.ToString(), false, "DESTINATARIO") +
                                                                            clsParser.SqlStringFormat(icmsporcentagem.ToString(), false, "ICMSTIPO") +
                                                                            clsParser.SqlStringFormat(ipiincide.ToString(), false, "IPIINCIDE") +
                                                                            clsParser.SqlStringFormat(pisporcentagem.ToString(), false, "PISTIPO") +
                                                                            clsParser.SqlStringFormat(cofinsporcetagem.ToString(), false, "COFINSTIPO") +
                                                                            clsParser.SqlDecimalFormat(tbxReducaoIcms.Text, false, "REDUCAOICMS") +
                                                                            clsParser.SqlDecimalFormat(tbxIndiceIVAIcms.Text,false, "INDICEIVAICMS") +
                                                                            clsParser.SqlDecimalFormat(tbxIcmsDiferenciado.Text,false, "ICMSDIFERENCIADO") +
                                                                            clsParser.SqlDecimalFormat(tbxReducaoIPI.Text,false, "REDUCAOIPI") +
                                                                            clsParser.SqlDecimalFormat(tbxReducaoPIS.Text,false,"REDUCAOPIS") +
                                                                            clsParser.SqlDecimalFormat(tbxIndiceIVAPIS.Text,false,"INDICEIVAPIS") +
                                                                            clsParser.SqlDecimalFormat(tbxPisDiferenciado.Text,false,"PISDIFERENCIADO") +
                                                                            clsParser.SqlDecimalFormat(tbxReducaoCofins.Text,false,"REDUCAOCOFINS") +
                                                                            clsParser.SqlDecimalFormat(tbxIndiceIVACOFINS.Text,false,"INDICEIVACOFINS") +
                                                                            clsParser.SqlDecimalFormat(tbxCofinsDiferenciado.Text,false,"COFINSDIFERENCIADO") +
                                                                            clsParser.SqlInt32Format(idtribicms.ToString(),false,"IDTRIBICMS") +
                                                                            clsParser.SqlInt32Format(idtribipi.ToString(),false,"IDTRIBIPI") +
                                                                            clsParser.SqlInt32Format(idtribpis.ToString(),false,"IDTRIBPIS") +
                                                                            clsParser.SqlInt32Format(idtribcofins.ToString(),false,"IDTRIBCOFINS") + 
                                                                            clsParser.SqlInt32Format(idicms.ToString(),false,"IDICMS") + 
                                                                            clsParser.SqlInt32Format(idipi.ToString(),false,"IDIPI") + 
                                                                            clsParser.SqlInt32Format(idpis.ToString(),false,"IDPIS") + 
                                                                            clsParser.SqlInt32Format(idcofins.ToString(),true,"IDCOFINS"));

                }
                else
                {
                    // Alterando os valores do Cabeçalho -- valores locais
                    clsSelectInsertUpdateBLL.Update(clsInfo.conexaosqldados, "DizeresNFV",
                                                                "CODIGO = " + clsParser.SqlStringFormat(tbxCodigo.Text, false, "CODIGO") +
                                                                "NOME = " + clsParser.SqlStringFormat(tbxNome.Text, false, "NOME") +
                                                                "LIN01 = " + clsParser.SqlStringFormat(tbxLin01.Text, false, "LIN01") +
                                                                "LIN02 = " + clsParser.SqlStringFormat(tbxLin02.Text, false, "LIN02") +
                                                                "LIN03 = " + clsParser.SqlStringFormat(tbxLin03.Text, false, "LIN03") +
                                                                "LIN04 = " + clsParser.SqlStringFormat(tbxLin04.Text, false, "LIN04") + 
                                                                "LIN05 = " + clsParser.SqlStringFormat(tbxLin05.Text, false, "LIN05") +
                                                                "LIN06 = " + clsParser.SqlStringFormat(tbxLin06.Text, false, "LIN06") +
                                                                "LIN07 = " + clsParser.SqlStringFormat(tbxLin07.Text, false, "LIN07") +
                                                                "LIN08 = " + clsParser.SqlStringFormat(tbxLin08.Text, false, "LIN08") +
                                                                "TIPOSAIDA = " + clsParser.SqlStringFormat(tipo.ToString(),false, "TIPOSAIDA") +
                                                                "DESTINATARIO = " + clsParser.SqlStringFormat(destinatario.ToString(), false, "DESTINATARIO") +
                                                                "ICMSTIPO = " + clsParser.SqlStringFormat(icmsporcentagem.ToString(), false, "ICMSTIPO") +
                                                                "IPIINCIDE = " + clsParser.SqlStringFormat(ipiincide.ToString(), false, "IPIINCIDE") +
                                                                "PISTIPO = " + clsParser.SqlStringFormat(pisporcentagem.ToString(), false, "PISTIPO") +
                                                                "COFINSTIPO = " + clsParser.SqlStringFormat(cofinsporcetagem.ToString(), false, "COFINSTIPO") +                                                                
                                                                "REDUCAOICMS = " + clsParser.SqlDecimalFormat(tbxReducaoIcms.Text, false, "REDUCAOICMS") +
                                                                "INDICEIVAICMS = " + clsParser.SqlDecimalFormat(tbxIndiceIVAIcms.Text,false, "INDICEIVAICMS") +
                                                                "ICMSDIFERENCIADO = " + clsParser.SqlDecimalFormat(tbxIcmsDiferenciado.Text,false, "ICMSDIFERENCIADO") +
                                                                "REDUCAOIPI = " + clsParser.SqlDecimalFormat(tbxReducaoIPI.Text,false, "REDUCAOIPI") +
                                                                "REDUCAOPIS = " + clsParser.SqlDecimalFormat(tbxReducaoPIS.Text,false,"REDUCAOPIS") +
                                                                "INDICEIVAPIS = " + clsParser.SqlDecimalFormat(tbxIndiceIVAPIS.Text,false,"INDICEIVAPIS") +
                                                                "PISDIFERENCIADO = " + clsParser.SqlDecimalFormat(tbxPisDiferenciado.Text,false,"PISDIFERENCIADO") +
                                                                "REDUCAOCOFINS = " + clsParser.SqlDecimalFormat(tbxReducaoCofins.Text,false,"REDUCAOCOFINS") +
                                                                "INDICEIVACOFINS = " + clsParser.SqlDecimalFormat(tbxIndiceIVACOFINS.Text,false,"INDICEIVACOFINS") +
                                                                "COFINSDIFERENCIADO = " + clsParser.SqlDecimalFormat(tbxCofinsDiferenciado.Text,false,"COFINSDIFERENCIADO") +
                                                                "IDTRIBICMS = " + clsParser.SqlInt32Format(idtribicms.ToString(),false,"IDTRIBICMS") +
                                                                "IDTRIBIPI = " +  clsParser.SqlInt32Format(idtribipi.ToString(),false,"IDTRIBIPI") +
                                                                "IDTRIBPIS = " + clsParser.SqlInt32Format(idtribpis.ToString(),false,"IDTRIBPIS") +
                                                                "IDTRIBCOFINS = " + clsParser.SqlInt32Format(idtribcofins.ToString(),false,"IDTRIBCOFINS") +
                                                                "IDICMS = " + clsParser.SqlInt32Format(idicms.ToString(),false,"IDICMS") + 
                                                                "IDIPI = " + clsParser.SqlInt32Format(idipi.ToString(),false,"IDIPI") + 
                                                                "IDPIS = " + clsParser.SqlInt32Format(idpis.ToString(),false,"IDPIS") + 
                                                                "IDCOFINS = " + clsParser.SqlInt32Format(idcofins.ToString(),true,"IDCOFINS"),
                                                                "ID = " + idDizeresNFVX.ToString());


                }

                //logo devemos atualizar o grid de movimentaçao
                dgvDizeresNFV_compl.DataSource = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados,
                   "DizeresNFV ",
                   "ID,CODIGO,NOME",
                   "", "ID");
            }
        }

        private Boolean VerificaGravacaoDizeresNFV(String _tipo)
        {
            try
            {
                // Verficando os campos
                if (_tipo == "TODOS")
                {
                    if (tbxCodigo.Text.Trim() == "")
                    {
                        throw new Exception("Informe o Codigo");
                    }

                    if (tbxNome.Text.Trim() == "")
                    {
                        throw new Exception("Informe o Nome");
                    }
                   
                }
            }
            catch (Exception ex)
            {
                resultado = DialogResult.Cancel; // Retorna para o foco
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                switch (ex.Message)
                {
                    case "Informe o Codigo":
                        tbxCodigo.Select();
                        tbxCodigo.SelectAll();
                        break;

                    case "Informe o Nome":
                        tbxNome.Select();
                        tbxNome.SelectAll();
                        break;

                    default:
                        break;
                }
                return false;
            }
            return true;
        }

        private Boolean MovimentaDizeresNFV()
        {
            try
            {
                ok = false;
                valoresatuais = PreencheValoresDizeresNFV();
                if (clsSelectInsertUpdateBLL.ComparaValores(valoresatuais, valoresantigos) == false)
                {
                    ok = true;
                }


                if (ok)
                {
                    resultado = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        if (resultado != DialogResult.Cancel)
                        {
                            SalvarDizeresNFV();
                        }
                    }
                    else if (resultado == DialogResult.No)
                    {
                        if (VerificaGravacaoDizeresNFV("TODOS") == false)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else if (resultado == DialogResult.Cancel)
                    {
                        return false;
                    }
                }
                else
                {
                    if (VerificaGravacaoDizeresNFV("TODOS") == false)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = DialogResult.None;
                MessageBox.Show("MovimentaDizeresNFV - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            if (resultado == DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                resultado = DialogResult.No;
                if (MovimentaDizeresNFV())
                {
                    idDizeresNFVX = clsParser.Int32Parse(((DataGridViewRowCollection)dgvDizeresNFV_fil.Rows)[0].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                    dgvDizeresNFV_fil[2, 0].Selected = true;
                    CarregaDizeresNFV(idDizeresNFVX);
                    leaveNocheck = true;
                    frmDizeresNFV_Activated(sender, e);
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
                resultado = DialogResult.No;
                if (MovimentaDizeresNFV())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection)dgvDizeresNFV_fil.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idDizeresNFVX)
                        {
                            if (dgvr.Index != 0)
                            {
                                idDizeresNFVX = clsParser.Int32Parse(((DataGridViewRowCollection)dgvDizeresNFV_fil.Rows)[((DataGridViewRowCollection)dgvDizeresNFV_fil.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente                                
                                dgvDizeresNFV_fil[2, ((DataGridViewRowCollection)dgvDizeresNFV_fil.Rows).
                                    GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaDizeresNFV(idDizeresNFVX);
                    leaveNocheck = true;
                    frmDizeresNFV_Activated(sender, e);

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
                resultado = DialogResult.No;
                if (MovimentaDizeresNFV())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection)dgvDizeresNFV_fil.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idDizeresNFVX)
                        {
                            if (dgvr.Index < ((DataGridViewRowCollection)dgvDizeresNFV_fil.Rows).Count - 1)
                            {
                                idDizeresNFVX = clsParser.Int32Parse(((DataGridViewRowCollection)dgvDizeresNFV_fil.Rows)[((DataGridViewRowCollection)dgvDizeresNFV_fil.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente
                                dgvDizeresNFV_fil[2, ((DataGridViewRowCollection)dgvDizeresNFV_fil.Rows).
                                    GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaDizeresNFV(idDizeresNFVX);
                    leaveNocheck = true;
                    frmDizeresNFV_Activated(sender, e);
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
                resultado = DialogResult.No;
                if (MovimentaDizeresNFV())
                {
                    idDizeresNFVX = clsParser.Int32Parse(((DataGridViewRowCollection)dgvDizeresNFV_fil.Rows)[((DataGridViewRowCollection)dgvDizeresNFV_fil.Rows).Count - 1].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                    dgvDizeresNFV_fil[2, ((DataGridViewRowCollection)dgvDizeresNFV_fil.Rows).Count - 1].Selected = true;
                    CarregaDizeresNFV(idDizeresNFVX);
                    leaveNocheck = true;
                    frmDizeresNFV_Activated(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void frmDizeresNFV_Activated(object sender, EventArgs e)
        {
            Lupa();
            bwrCarregaAutoComplete_RunWorkerAsync();
        }

        private void rbntiposaidaE_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                if (((RadioButton)sender).Name == "rbntiposaidaE")
                {
                    tipo = "E";
                }
                if (((RadioButton)sender).Name == "rbntiposaidaS")
                {
                    tipo = "S";
                }
                if (((RadioButton)sender).Name == "rbntiposaidaM")
                {
                    tipo = "M";
                }
                ///////////////
                if (((RadioButton)sender).Name == "rbnDestinatarioC")
                {
                    destinatario = "C";
                }
                if (((RadioButton)sender).Name == "rbnDestinatarioD")
                {
                    destinatario= "D";
                }
                ////
                if (((RadioButton)sender).Name == "rbnIcmsPorc")
                {
                    icmsporcentagem = "P";
                }
                if (((RadioButton)sender).Name == "rbnIcmsVal")
                {
                 icmsporcentagem = "V";
                }
                ////
                if (((RadioButton)sender).Name == "rbnIpiIncideN")
                {
                    ipiincide = "N";
                }
                if (((RadioButton)sender).Name == "rbnIpiIncideS")
                {
                    ipiincide = "S";
                }
                ////
                if (((RadioButton)sender).Name == "rbnPisPorc")
                {
                    pisporcentagem = "P";
                }
                if (((RadioButton)sender).Name == "rbnPisVal")
                {
                    pisporcentagem = "V";
                }
                ////
                if (((RadioButton)sender).Name == "rbnCofinsPorc")
                {
                    cofinsporcetagem = "P";
                }
                if (((RadioButton)sender).Name == "rbnCofinsVal")
                {
                    cofinsporcetagem = "V";
                }

            }
        }

        private void Lupa()
        {
            if (clsInfo.zrow != null)
            {
                if (clsInfo.znomegrid == "dgvTribIcms")
                {
                   idtribicms = (Int32)clsInfo.zrow.Cells["ID"].Value;
                   tbxTribIcms.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                   tbxTribIcms.Select();
                   tbxTribIcms.SelectAll();
                   tbxTribIcms.Focus();
                   tbxTribIcms.SelectionLength = 0;
                   tbxTribIcms.SelectionStart = 0;
                }
                if (clsInfo.znomegrid == "dgvTribIpi")
                {
                    idtribipi = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxTribIPI.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                    tbxTribIPI.Select();
                    tbxTribIPI.SelectAll();
                    tbxTribIPI.Focus();
                    tbxTribIPI.SelectionLength = 0;
                    tbxTribIPI.SelectionStart = 0;
                }
                if (clsInfo.znomegrid == "dgvTribPis")
                {
                    idtribpis = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxTribPIS.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                    tbxTribPIS.Select();
                    tbxTribPIS.SelectAll();
                    tbxTribPIS.Focus();
                    tbxTribPIS.SelectionLength = 0;
                    tbxTribPIS.SelectionStart = 0;
                }
                if (clsInfo.znomegrid == "dgvTribCofins")
                {
                    idtribcofins = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxTribCofins.Text = clsInfo.zrow.Cells["NOME"].Value.ToString();
                    tbxTribCofins.Select();
                    tbxTribCofins.SelectAll();
                    tbxTribCofins.Focus();
                    tbxTribCofins.SelectionLength = 0;
                    tbxTribCofins.SelectionStart = 0;
                }
                if (clsInfo.znomegrid == "dgvIcms")
                {
                    idicms= (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxSitTribIcmsB.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString() + " - " + clsInfo.zrow.Cells["NOME"].Value.ToString();
                    tbxSitTribIcmsB.Select();
                    tbxSitTribIcmsB.SelectAll();
                    tbxSitTribIcmsB.Focus();
                    tbxSitTribIcmsB.SelectionLength = 0;
                    tbxSitTribIcmsB.SelectionStart = 0;
                }
                if (clsInfo.znomegrid == "dgvIpi")
                {
                    idipi = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxSitTribIpi.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString() + " - " + clsInfo.zrow.Cells["NOME"].Value.ToString();
                    tbxSitTribIpi.Select();
                    tbxSitTribIpi.SelectAll();
                    tbxSitTribIpi.Focus();
                    tbxSitTribIpi.SelectionLength = 0;
                    tbxSitTribIpi.SelectionStart = 0;
                }
                if (clsInfo.znomegrid == "dgvPis")
                {
                    idpis = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxSitTribPIS.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString() + " - " + clsInfo.zrow.Cells["NOME"].Value.ToString();
                    tbxSitTribPIS.Select();
                    tbxSitTribPIS.SelectAll();
                    tbxSitTribPIS.Focus();
                    tbxSitTribPIS.SelectionLength = 0;
                    tbxSitTribPIS.SelectionStart = 0;
                }
                if (clsInfo.znomegrid == "dgvCofins")
                {
                    idcofins = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxSitTribCofins.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString() + " - " + clsInfo.zrow.Cells["NOME"].Value.ToString();
                    tbxSitTribCofins.Select();
                    tbxSitTribCofins.SelectAll();
                    tbxSitTribCofins.Focus();
                    tbxSitTribCofins.SelectionLength = 0;
                    tbxSitTribCofins.SelectionStart = 0;
                }
                clsInfo.zrow = null;
                clsInfo.znomegrid = "";
            }
        }

        private void btnIdTribIcms_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvTribIcms";
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados,
                            "SITTRIB", "ID,CODIGO,NOME",
                            "", "ID", new GridColuna[]{
                            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Codigo", "CODIGO", 55, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Nome", "NOME", 900, true, DataGridViewContentAlignment.MiddleLeft)},
                            true,
                            8,
                           null
                            );

            
            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

            tbxTribIcms.Select();
            tbxTribIcms.SelectAll();
            tbxTribIcms.Focus();
            tbxTribIcms.SelectionLength = 0;
            tbxTribIcms.SelectionStart = 0;
        }

        private void btnIdTribIpi_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvTribIpi";
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados,
                            "SITTRIB", "ID,CODIGO,NOME",
                            "", "ID", new GridColuna[]{
                            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Codigo", "CODIGO", 55, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Nome", "NOME", 900, true, DataGridViewContentAlignment.MiddleLeft)},
                            true,
                            8,
                           null
                            );

            
            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

            tbxTribIPI.Select();
            tbxTribIPI.SelectAll();
            tbxTribIPI.Focus();
            tbxTribIPI.SelectionLength = 0;
            tbxTribIPI.SelectionStart = 0;
        }

        private void btnIdTribPis_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvTribPis";
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados,
                            "SITTRIB", "ID,CODIGO,NOME",
                            "", "ID", new GridColuna[]{
                            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Codigo", "CODIGO", 55, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Nome", "NOME", 900, true, DataGridViewContentAlignment.MiddleLeft)},
                            true,
                            8,
                           null
                            );

            
            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

            tbxTribPIS.Select();
            tbxTribPIS.SelectAll();
            tbxTribPIS.Focus();
            tbxTribPIS.SelectionLength = 0;
            tbxTribPIS.SelectionStart = 0;
        }

        private void btnIdTribCofins_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvTribCofins";
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados,
                            "SITTRIB", "ID,CODIGO,NOME",
                            "", "ID", new GridColuna[]{
                            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Codigo", "CODIGO", 55, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Nome", "NOME", 900, true, DataGridViewContentAlignment.MiddleLeft)},
                            true,
                            8,
                           null
                            );

            
            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

            tbxTribCofins.Select();
            tbxTribCofins.SelectAll();
            tbxTribCofins.Focus();
            tbxTribCofins.SelectionLength = 0;
            tbxTribCofins.SelectionStart = 0;
        }

        private void btnIdSitTribIcmsB_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvIcms";
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados,
                            "SITTRIBUTARIAB", "ID,CODIGO,NOME",
                            "", "ID", new GridColuna[]{
                            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Codigo", "CODIGO", 55, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Nome", "NOME", 900, true, DataGridViewContentAlignment.MiddleLeft)},
                            true,
                            8,
                           null
                            );

            
            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

            tbxSitTribIcmsB.Select();
            tbxSitTribIcmsB.SelectAll();
            tbxSitTribIcmsB.Focus();
            tbxSitTribIcmsB.SelectionLength = 0;
            tbxSitTribIcmsB.SelectionStart = 0;
        }

        private void btnIdSitTribIPI_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvIpi";
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados,
                            "SITTRIBIPI", "ID,CODIGO,NOME",
                            "", "ID", new GridColuna[]{
                            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Codigo", "CODIGO", 55, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Nome", "NOME", 900, true, DataGridViewContentAlignment.MiddleLeft)},
                            true,
                            8,
                           null
                            );

            
            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

            tbxSitTribIpi.Select();
            tbxSitTribIpi.SelectAll();
            tbxSitTribIpi.Focus();
            tbxSitTribIpi.SelectionLength = 0;
            tbxSitTribIpi.SelectionStart = 0;
        }

        private void btnIdSitTribPIS_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvPis";
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados,
                            "SITTRIBPIS", "ID,CODIGO,NOME",
                            "", "ID", new GridColuna[]{
                            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Codigo", "CODIGO", 55, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Nome", "NOME", 900, true, DataGridViewContentAlignment.MiddleLeft)},
                            true,
                            8,
                           null
                            );

            
            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

            tbxSitTribPIS.Select();
            tbxSitTribPIS.SelectAll();
            tbxSitTribPIS.Focus();
            tbxSitTribPIS.SelectionLength = 0;
            tbxSitTribPIS.SelectionStart = 0;
        }

        private void btnIdSitTribCOFINS_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvCofins";
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados,
                            "SITTRIBCOFINS", "ID,CODIGO,NOME",
                            "", "ID", new GridColuna[]{
                            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Codigo", "CODIGO", 55, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Nome", "NOME", 900, true, DataGridViewContentAlignment.MiddleLeft)},
                            true,
                            8,
                           null
                            );

            
            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

            tbxSitTribCofins.Select();
            tbxSitTribCofins.SelectAll();
            tbxSitTribCofins.Focus();
            tbxSitTribCofins.SelectionLength = 0;
            tbxSitTribCofins.SelectionStart = 0;
        }

        private void AddAutoComplete(TextBox _textbox, TextBox _colecao)
        {
            if (_textbox.InvokeRequired)
            {
                Invoke(new AddnewDelegate(AddAutoComplete), _textbox, _colecao);
            }
            else
            {
                _textbox.AutoCompleteCustomSource = _colecao.AutoCompleteCustomSource;
            }
        }

        private void bwrCarregaAutoComplete_RunWorkerAsync()
        {
            bwrCarregaAutoComplete = new BackgroundWorker();
            bwrCarregaAutoComplete.DoWork += new DoWorkEventHandler(bwrCarregaAutoComplete_DoWork);
            bwrCarregaAutoComplete.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrCarregaAutoComplete_RunWorkerCompleted);
            bwrCarregaAutoComplete.RunWorkerAsync();
        }


        private void bwrCarregaAutoComplete_DoWork(object sender, DoWorkEventArgs e)
        {
            AddAutoComplete(tbxTribIcms, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "SITTRIB", "NOME", "ID>0", "NOME", false));
            AddAutoComplete(tbxTribIPI, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "SITTRIB", "NOME", "ID>0", "NOME", false));
            AddAutoComplete(tbxTribPIS, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "SITTRIB", "NOME", "ID>0", "NOME", false));
            AddAutoComplete(tbxTribCofins, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "SITTRIB", "NOME", "ID>0", "NOME", false));
            //
            AddAutoComplete(tbxSitTribIcmsB, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "SITTRIBUTARIAB", "(CODIGO + ' - ' + NOME) AS NOME", "ID>0", "NOME", false));
            AddAutoComplete(tbxSitTribIpi, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "SITTRIBIPI", "(CODIGO + ' - ' + NOME) AS NOME", "ID>0", "NOME", false));
            AddAutoComplete(tbxSitTribPIS, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "SITTRIBPIS", "(CODIGO + ' - ' + NOME) AS NOME", "ID>0", "NOME", false));
            AddAutoComplete(tbxSitTribCofins, clsSelectInsertUpdateBLL.AutoComplete(clsInfo.conexaosqldados, "SITTRIBCOFINS", "(CODIGO + ' - ' + NOME) AS NOME", "ID>0", "NOME", false));            
        }

        private void bwrCarregaAutoComplete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }     
    }
}
