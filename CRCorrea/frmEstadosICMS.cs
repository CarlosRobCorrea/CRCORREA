using CRCorreaBLL;
using CRCorreaFuncoes;
using System;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmEstadosICMS : Form
    {
        private DataGridView dgvESTADOSICMS;

        private String[] valoresantigos = new String[] { };
        private String[] valoresatuais = new String[] { };
        
        private delegate void AddnewDelegate(TextBox textbox, TextBox colecao);

        private Int32 idestadoX;
        private Int32 idESTADOSICMSX;
       
        private DialogResult resultado = new DialogResult();
        
        
        

        private Boolean leaveNocheck;

        private Int32 idestadodestino;
        private Int32 iddizeresnfv;

        private DataTable dtEstadoIcmsaux;

        public frmEstadosICMS()
        {
            InitializeComponent();
        }

        public void Init(Int32 _idESTADOSICMS,
                         Int32 _idTab_CNAE,
                         DataGridView _dgvESTADOSICMS)
        {
            leaveNocheck = false;
            idestadoX = _idTab_CNAE;
            idESTADOSICMSX = _idESTADOSICMS;         
            dgvESTADOSICMS = _dgvESTADOSICMS;
        }

        private void frmEstadosICMS_Load(object sender, EventArgs e)
        {
            CarregaESTADOSICMS(idESTADOSICMSX);
        }

        private void CarregaESTADOSICMS(Int32 _id)
        {
            try
            {
                //Carrega os Dados 
                tspESTADOSICMS.Cursor = Cursors.Hand;
                PreencheCamposESTADOSICMS();
                valoresantigos = PreencheValoresESTADOSICMS();

                if (_id == 0)
                {
                    tspPrimeiro.Enabled = false;
                    tspAnterior.Enabled = false;
                    tspProximo.Enabled = false;
                    tspUltimo.Enabled = false;
                }
                btnEstados_uf.Select();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Carrega Tab CNAE UF " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                leaveNocheck = true;
            }
        }

        private void PreencheCamposESTADOSICMS()
        {

            if (idESTADOSICMSX > 0)
            {
                foreach (DataRow dgvrESTADOSICMS in ((DataTable)dgvESTADOSICMS.DataSource).Rows)
                {
                    if (dgvrESTADOSICMS.RowState != DataRowState.Deleted)
                    {
                        if (clsParser.Int32Parse(dgvrESTADOSICMS["ID"].ToString()) == idESTADOSICMSX)
                        {
                            dtEstadoIcmsaux = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "ESTADOS",
                                "ESTADO,NOMEEXT,CAPITAL", "ID = " + clsParser.Int32Parse(dgvrESTADOSICMS["IDESTADODESTINO"].ToString()), "ID");
                            if (dtEstadoIcmsaux.Rows.Count > 0)
                            {
                                tbxEstados_uf.Text = dtEstadoIcmsaux.Rows[0]["ESTADO"].ToString();
                                tbxEstados_nomeext.Text = dtEstadoIcmsaux.Rows[0]["NOMEEXT"].ToString();
                                tbxEstados_capital.Text = dtEstadoIcmsaux.Rows[0]["CAPITAL"].ToString();
                            }


                            tbxDizeresNFV_Codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from DIZERESNFV where ID = " + clsParser.Int32Parse(dgvrESTADOSICMS["IDDIZERESNFV"].ToString()), "");
                            //tbxDizeresNFV_Codigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "DIZERESNFV", "CODIGO", "ID", clsParser.Int32Parse(dgvrESTADOSICMS["IDDIZERESNFV"].ToString()).ToString());
                            
                            tbxEstadosicms_aliquota.Text = clsParser.DecimalParse(dgvrESTADOSICMS["ALIQUOTA"].ToString()).ToString("N2");
                            tbxEstadosicms_iva.Text = clsParser.DecimalParse(dgvrESTADOSICMS["IVA"].ToString()).ToString("N2");
                                                     
                            idestadodestino = clsParser.Int32Parse(dgvrESTADOSICMS["IDESTADODESTINO"].ToString());
                            iddizeresnfv = clsParser.Int32Parse(dgvrESTADOSICMS["IDDIZERESNFV"].ToString());
                        }
                    }
                }
            }
        }

        private String[] PreencheValoresESTADOSICMS()
        {
            String[] _valores = { tbxEstadosicms_aliquota.Text, 
                               tbxEstadosicms_iva.Text ,
                               idestadodestino.ToString(),
                                iddizeresnfv.ToString()};

            return _valores;
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
                    if (VerificaGravacaoESTADOSICMS("TODOS") == true)
                    {
                        SalvarESTADOSICMS();
                    }
                }
                else if (resultado == DialogResult.Cancel)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                resultado = DialogResult.Cancel;
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                if (resultado != DialogResult.Cancel)
                {
                    if (idESTADOSICMSX > 0)
                    {
                        frmEstadosVis.indexESTADOSICMSGeral = idESTADOSICMSX;
                    }
                    leaveNocheck = true;
                    this.Close();
                    this.Dispose();
                }
            }
        }


        private void SalvarESTADOSICMS()
        {
            if (idESTADOSICMSX == 0)
            {
                frmEstadosVis.drESTADOSICMSGeral = clsGridHelper.Autoincrement(dgvESTADOSICMS, frmEstadosVis.drESTADOSICMSGeral);

                //frmEstados_Vis.drESTADOSICMSGeral["ID"] = idESTADOSICMSX;
                frmEstadosVis.drESTADOSICMSGeral["IDESTADO"] = idestadoX;
                frmEstadosVis.drESTADOSICMSGeral["IDESTADODESTINO"] = idestadodestino;
                frmEstadosVis.drESTADOSICMSGeral["UF"] = tbxEstados_uf.Text;
                frmEstadosVis.drESTADOSICMSGeral["ALIQUOTA"] = clsParser.DecimalParse(tbxEstadosicms_aliquota.Text);
                frmEstadosVis.drESTADOSICMSGeral["IVA"] = clsParser.DecimalParse(tbxEstadosicms_iva.Text);
                frmEstadosVis.drESTADOSICMSGeral["IDDIZERESNFV"] = iddizeresnfv;


                frmEstadosVis.drESTADOSICMSGeral["TEMP"] = "S";

                ((DataTable)dgvESTADOSICMS.DataSource).Rows.Add(frmEstadosVis.drESTADOSICMSGeral);

                idESTADOSICMSX = clsParser.Int32Parse(frmEstadosVis.drESTADOSICMSGeral["ID"].ToString());

                //adicionando na tabela geral
                frmEstadosVis.dtESTADOSICMSGeral.ImportRow(frmEstadosVis.drESTADOSICMSGeral);

            }
            else
            {
                foreach (DataRow dgvrESTADOSICMS in ((DataTable)dgvESTADOSICMS.DataSource).Rows)
                {
                    if (dgvrESTADOSICMS.RowState != DataRowState.Deleted)
                    {
                        if (clsParser.Int32Parse(dgvrESTADOSICMS["ID"].ToString()) == idESTADOSICMSX)
                        {
                            frmEstadosVis.drESTADOSICMSGeral = dgvrESTADOSICMS;

                            frmEstadosVis.drESTADOSICMSGeral["ID"] = idESTADOSICMSX;
                            frmEstadosVis.drESTADOSICMSGeral["IDESTADO"] = idestadoX;
                            frmEstadosVis.drESTADOSICMSGeral["IDESTADODESTINO"] = idestadodestino;
                            frmEstadosVis.drESTADOSICMSGeral["UF"] = tbxEstados_uf.Text;
                            frmEstadosVis.drESTADOSICMSGeral["ALIQUOTA"] = clsParser.DecimalParse(tbxEstadosicms_aliquota.Text);
                            frmEstadosVis.drESTADOSICMSGeral["IVA"] = clsParser.DecimalParse(tbxEstadosicms_iva.Text);
                            frmEstadosVis.drESTADOSICMSGeral["IDDIZERESNFV"] = iddizeresnfv;

                            frmEstadosVis.drESTADOSICMSGeral["TEMP"] = "S";

                            // atualizando a tabela geral 
                            frmEstadosVis.achouESTADOSICMSGeral = false;
                            foreach (DataRow dgvrwESTADOSICMS in frmEstadosVis.dtESTADOSICMSGeral.Rows)
                            {
                                if (dgvrwESTADOSICMS.RowState != DataRowState.Deleted)
                                {
                                    if (clsParser.Int32Parse(dgvrwESTADOSICMS["ID"].ToString()) == idESTADOSICMSX &&
                                        clsParser.Int32Parse(dgvrwESTADOSICMS["IDESTADO"].ToString()) == idestadoX)
                                    {
                                        frmEstadosVis.drESTADOSICMSGeral = dgvrwESTADOSICMS;

                                        frmEstadosVis.drESTADOSICMSGeral["ID"] = idESTADOSICMSX;
                                        frmEstadosVis.drESTADOSICMSGeral["IDESTADO"] = idestadoX;
                                        frmEstadosVis.drESTADOSICMSGeral["IDESTADODESTINO"] = idestadodestino;
                                        frmEstadosVis.drESTADOSICMSGeral["UF"] = tbxEstados_uf.Text;
                                        frmEstadosVis.drESTADOSICMSGeral["ALIQUOTA"] = clsParser.DecimalParse(tbxEstadosicms_aliquota.Text);
                                        frmEstadosVis.drESTADOSICMSGeral["IVA"] = clsParser.DecimalParse(tbxEstadosicms_iva.Text);
                                        frmEstadosVis.drESTADOSICMSGeral["IDDIZERESNFV"] = iddizeresnfv;


                                        frmEstadosVis.drESTADOSICMSGeral["TEMP"] = "S";

                                        frmEstadosVis.achouESTADOSICMSGeral = true;
                                        break;
                                    }
                                }
                            }
                            if (frmEstadosVis.achouESTADOSICMSGeral == false)
                            {
                                frmEstadosVis.dtESTADOSICMSGeral.ImportRow(frmEstadosVis.drESTADOSICMSGeral);
                            }
                            break;
                        }
                    }
                }
            }

        }

        private Boolean VerificaGravacaoESTADOSICMS(String _tipo)
        {
            try
            {
                // Verficando se a referencia esta em branco
                if (_tipo == "TODOS")
                {
                    if (tbxEstadosicms_aliquota.Text.Trim() == "")
                    {
                        throw new Exception("Informe a Aliquota");
                    }

                    if (tbxEstadosicms_iva.Text.Trim() == "")
                    {
                        throw new Exception("Informe o IVA");
                    }

                    if(idestadodestino == 0)
                    {
                        throw new Exception("Informe o Estado destino");
                    }
                    if(iddizeresnfv == 0)
                    {
                        throw new Exception("Informe o Codigo dos dizeres");
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = DialogResult.Cancel; // Retorna para o foco
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                switch (ex.Message)
                {

                    case "Informe o Estado destino":
                        btnEstados_uf.Select();
                        break;

                    case "Informe a Aliquota":
                        tbxEstadosicms_aliquota.Select();
                        tbxEstadosicms_aliquota.SelectAll();
                        break;

                    case "Informe o IVA":
                        tbxEstadosicms_iva.Select();
                        tbxEstadosicms_iva.SelectAll();
                        break;

                    case "Informe o Codigo dos dizeres":
                        btnIdDizeresNFV.Select();                       
                        break;

                    default:
                        break;
                }
                return false;
            }
            return true;
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                resultado = DialogResult.No;
                valoresatuais = PreencheValoresESTADOSICMS();
                if (clsSelectInsertUpdateBLL.ComparaValores(valoresatuais, valoresantigos) == false)
                {
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

                    if (resultado == DialogResult.Yes)
                    {
                        SalvarESTADOSICMS();
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
                    if (idESTADOSICMSX > 0)
                    {
                        frmEstadosVis.indexESTADOSICMSGeral = idESTADOSICMSX;
                    }
                    leaveNocheck = true;
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void frmEstadosICMS_Activated(object sender, EventArgs e)
        {
            Lupa();
        }

        private Boolean MovimentaESTADOSICMS()
        {
            try
            {
                valoresatuais = PreencheValoresESTADOSICMS();
                if (clsSelectInsertUpdateBLL.ComparaValores(valoresatuais, valoresantigos) == false)
                {
                    resultado = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        if (resultado != DialogResult.Cancel)
                        {
                            SalvarESTADOSICMS();
                        }
                    }
                    else if (resultado == DialogResult.No)
                    {
                        if (VerificaGravacaoESTADOSICMS("TODOS") == false)
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
                    if (VerificaGravacaoESTADOSICMS("TODOS") == false)
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
                MessageBox.Show("MovimentaESTADOSICMS - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                if (MovimentaESTADOSICMS())
                {
                    idESTADOSICMSX = clsParser.Int32Parse(((DataGridViewRowCollection)dgvESTADOSICMS.Rows)[0].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                    dgvESTADOSICMS[3, 0].Selected = true;
                    CarregaESTADOSICMS(idESTADOSICMSX);
                    leaveNocheck = true;
                    frmEstadosICMS_Activated(sender, e);
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
                if (MovimentaESTADOSICMS())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection)dgvESTADOSICMS.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idESTADOSICMSX)
                        {
                            if (dgvr.Index != 0)
                            {
                                idESTADOSICMSX = clsParser.Int32Parse(((DataGridViewRowCollection)dgvESTADOSICMS.Rows)[((DataGridViewRowCollection)dgvESTADOSICMS.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente                                
                                dgvESTADOSICMS[3, ((DataGridViewRowCollection)dgvESTADOSICMS.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaESTADOSICMS(idESTADOSICMSX);
                    leaveNocheck = true;
                    frmEstadosICMS_Activated(sender, e);

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
                if (MovimentaESTADOSICMS())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection)dgvESTADOSICMS.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idESTADOSICMSX)
                        {
                            if (dgvr.Index < ((DataGridViewRowCollection)dgvESTADOSICMS.Rows).Count - 1)
                            {
                                idESTADOSICMSX = clsParser.Int32Parse(((DataGridViewRowCollection)dgvESTADOSICMS.Rows)[((DataGridViewRowCollection)dgvESTADOSICMS.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente
                                dgvESTADOSICMS[3, ((DataGridViewRowCollection)dgvESTADOSICMS.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaESTADOSICMS(idESTADOSICMSX);
                    leaveNocheck = true;
                    frmEstadosICMS_Activated(sender, e);
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
                if (MovimentaESTADOSICMS())
                {
                    idESTADOSICMSX = clsParser.Int32Parse(((DataGridViewRowCollection)dgvESTADOSICMS.Rows)[((DataGridViewRowCollection)dgvESTADOSICMS.Rows).Count - 1].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                    dgvESTADOSICMS[3, ((DataGridViewRowCollection)dgvESTADOSICMS.Rows).Count - 1].Selected = true;
                    CarregaESTADOSICMS(idESTADOSICMSX);
                    leaveNocheck = true;
                    frmEstadosICMS_Activated(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void ControlEnter(object sender, EventArgs e)
        {
              clsVisual.ControlEnter(sender);
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
        }

        private void ControlKeyKeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisual.ControlKeyPressNumero((TextBox)sender, e, false);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void frmEstadosICMS_Shown(object sender, EventArgs e)
        {
            
        }

        private void Lupa()
        {
            if (clsInfo.zrow != null)
            {
                if (clsInfo.znomegrid == "dgvUF")
                {
                    idestadodestino = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxEstados_uf.Text = clsInfo.zrow.Cells["ESTADO"].Value.ToString();
                    tbxEstados_nomeext.Text =clsInfo.zrow.Cells["NOMEEXT"].Value.ToString();
                    tbxEstados_capital.Text = clsInfo.zrow.Cells["CAPITAL"].Value.ToString();
                    tbxEstadosicms_aliquota.Select();
                    tbxEstadosicms_aliquota.SelectAll();
                }

                if (clsInfo.znomegrid == "dgvDizeres")
                {
                    iddizeresnfv = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxDizeresNFV_Codigo .Text= clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                }
                clsInfo.zrow = null;
                clsInfo.znomegrid = "";
            }
        }

        private void btnEstados_uf_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvUF";
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados,
                            "ESTADOS", "ID,ESTADO,NOMEEXT,CAPITAL",
                            "", "ID", new GridColuna[]{
                            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("UF", "ESTADO", 40, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Nome", "NOMEEXT", 150, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Capital", "CAPITAL", 150, true, DataGridViewContentAlignment.MiddleLeft)},
                            true,
                            8,
                           null
                            );

            
            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);

           tbxEstadosicms_aliquota.Select();
           tbxEstadosicms_aliquota.SelectAll();
        }

        private void btnIdDizeresNFV_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvDizeres";
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados,
                            "DIZERESNFV", "ID,CODIGO,NOME",
                            "", "ID", new GridColuna[]{
                            new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Codigo", "CODIGO", 40, true, DataGridViewContentAlignment.MiddleLeft),
                            new GridColuna("Nome", "NOME", 935, true, DataGridViewContentAlignment.MiddleLeft)},
                            true,
                            8,
                           null
                            );

            
            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);           
        }

        private void tbxDizeresNFV_Codigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
