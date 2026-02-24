using CRCorreaFuncoes;
using System;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmTab_CNAE_UF : Form
    {
        private DataGridView dgvTab_CNAE_UF; 

        private String[] valoresantigos = new String[] { };
        private String[] valoresatuais = new String[] { };
        
        private delegate void AddnewDelegate(TextBox textbox, TextBox colecao);

        private Int32 idTab_CNAEX;
        private Int32 idTab_CNAE_UFX;
        private Int32 idUFX;       

        private DialogResult resultado = new DialogResult();
        
        
        

        private Boolean leaveNocheck;

        private Int32 iddizeresnfv;

        public frmTab_CNAE_UF()
        {
            InitializeComponent();
        }

        public void Init(Int32 _idTab_CNAE_UF,
                         Int32 _idTab_CNAE,
                         Int32 _idUF,
                         DataGridView _dgvTab_CNAE_UF )
        {
            leaveNocheck = false;
            idTab_CNAEX = _idTab_CNAE;
            idTab_CNAE_UFX = _idTab_CNAE_UF;
            idUFX = _idUF;           
            dgvTab_CNAE_UF = _dgvTab_CNAE_UF;
        }

        private void frmTab_CNAE_UF_Load(object sender, EventArgs e)
        {
            CarregaTab_CNAE_UF(idTab_CNAE_UFX);
        }

        private void CarregaTab_CNAE_UF(Int32 _id)
        {
            try
            {
                //Carrega os Dados 
                tspTab_CNAE_UF.Cursor = Cursors.Hand;
                PreencheCamposTab_CNAE_UF();
                valoresantigos = PreencheValoresTab_CNAE_UF();

                if (_id == 0)
                {
                    tspPrimeiro.Enabled = false;
                    tspAnterior.Enabled = false;
                    tspProximo.Enabled = false;
                    tspUltimo.Enabled = false;
                }
                btnUF_IVA.Select();
                //tbxUF.SelectAll();
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

        private void PreencheCamposTab_CNAE_UF()
        {          
          
            if (idTab_CNAE_UFX > 0)
            {
                foreach (DataRow dgvrTab_CNAE_UF in ((DataTable)dgvTab_CNAE_UF.DataSource).Rows)
                {
                    if (dgvrTab_CNAE_UF.RowState != DataRowState.Deleted)
                    {
                        if (clsParser.Int32Parse(dgvrTab_CNAE_UF["ID"].ToString()) == idTab_CNAE_UFX)
                        {
                            tbxUF.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from ESTADOS where ID=" + clsParser.Int32Parse(dgvrTab_CNAE_UF["IDUF"].ToString()).ToString(), "");
                            //tbxUF.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "ESTADOS", "ESTADO", "ID", clsParser.Int32Parse(dgvrTab_CNAE_UF["IDUF"].ToString()).ToString());
                            tbxIVA.Text = clsParser.DecimalParse(dgvrTab_CNAE_UF["IVA"].ToString()).ToString("N2");
                            tbxDizeresNFV_Codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from DIZERESNFV where ID=" + clsParser.Int32Parse(dgvrTab_CNAE_UF["IDDIZERESNFV"].ToString()).ToString(), "");
                            //tbxDizeresNFV_Codigo.Text = Procedure.PesquisaValor(clsInfo.conexaosqldados, "DIZERESNFV", "CODIGO", "ID", clsParser.Int32Parse(dgvrTab_CNAE_UF["IDDIZERESNFV"].ToString()).ToString());
                            iddizeresnfv = clsParser.Int32Parse(dgvrTab_CNAE_UF["IDDIZERESNFV"].ToString());
                            tbxIcmsST.Text  = clsParser.DecimalParse(dgvrTab_CNAE_UF["ICMSST"].ToString()).ToString("N2");
                        }
                    }
                }
            }
        }

        private String[] PreencheValoresTab_CNAE_UF()
        {
            String[] _valores = {tbxIVA.Text, 
                                tbxUF.Text,
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
                    if (VerificaGravacaoTab_CNAE_UF("TODOS") == true)
                    {
                        SalvarTab_CNAE_UF();
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
                    if (idTab_CNAE_UFX > 0)
                    {
                        frmTab_CNAE_Vis.indexTab_CNAE_UFGeral = idTab_CNAE_UFX;
                    }
                    leaveNocheck = true;
                    this.Close();
                    this.Dispose();
                }
            }
        }


        private void SalvarTab_CNAE_UF()
        {
            if (idTab_CNAE_UFX == 0)
            {
               frmTab_CNAE_Vis.drTab_CNAE_UFGeral = clsGridHelper.Autoincrement(dgvTab_CNAE_UF,frmTab_CNAE_Vis.drTab_CNAE_UFGeral);

               //frmTab_CNAE_Vis.drTab_CNAE_UFGeral["ID"] = idTab_CNAE_UFX;
               frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IDTABCNAE"] = idTab_CNAEX;
               frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IDUF"] = idUFX;
               frmTab_CNAE_Vis.drTab_CNAE_UFGeral["ESTADO"] = tbxUF.Text;
               frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IVA"] = clsParser.DecimalParse(tbxIVA.Text);
               frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IDDIZERESNFV"] = iddizeresnfv;
               frmTab_CNAE_Vis.drTab_CNAE_UFGeral["ICMSST"] = clsParser.DecimalParse(tbxIcmsST.Text);
               frmTab_CNAE_Vis.drTab_CNAE_UFGeral["TEMP"] = "S";
                //                
               ((DataTable)dgvTab_CNAE_UF.DataSource).Rows.Add(frmTab_CNAE_Vis.drTab_CNAE_UFGeral);
                //
               idTab_CNAE_UFX = clsParser.Int32Parse(frmTab_CNAE_Vis.drTab_CNAE_UFGeral["ID"].ToString());
                //
                // adicionando na tabela geral
               frmTab_CNAE_Vis.dtTab_CNAE_UFGeral.ImportRow(frmTab_CNAE_Vis.drTab_CNAE_UFGeral);
                //                   
            }
            else
            {
                foreach (DataRow dgvrTab_CNAE_UF in ((DataTable)dgvTab_CNAE_UF.DataSource).Rows)
                {
                    if (dgvrTab_CNAE_UF.RowState != DataRowState.Deleted)
                    {
                        if (clsParser.Int32Parse(dgvrTab_CNAE_UF["ID"].ToString()) == idTab_CNAE_UFX)
                        {
                            frmTab_CNAE_Vis.drTab_CNAE_UFGeral = dgvrTab_CNAE_UF;

                            //frmTab_CNAE_Vis.drTab_CNAE_UFGeral["ID"] = idTab_CNAE_UFX;
                            frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IDTABCNAE"] = idTab_CNAEX;
                            frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IDUF"] = idUFX;
                            frmTab_CNAE_Vis.drTab_CNAE_UFGeral["ESTADO"] = tbxUF.Text;
                            frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IVA"] = clsParser.DecimalParse(tbxIVA.Text);
                            frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IDDIZERESNFV"] = iddizeresnfv;
                            frmTab_CNAE_Vis.drTab_CNAE_UFGeral["ICMSST"] = clsParser.DecimalParse(tbxIcmsST.Text);
                            frmTab_CNAE_Vis.drTab_CNAE_UFGeral["TEMP"] = "S";
                            //                             
                            // atualizando a tabela geral 
                           frmTab_CNAE_Vis.achouTab_CNAE_UFGeral = false;
                            foreach (DataRow dgvrwTab_CNAE_UF in frmTab_CNAE_Vis.dtTab_CNAE_UFGeral.Rows)
                            {
                                if (dgvrwTab_CNAE_UF.RowState != DataRowState.Deleted)
                                {
                                    if (clsParser.Int32Parse(dgvrwTab_CNAE_UF["ID"].ToString()) == idTab_CNAE_UFX &&
                                        clsParser.Int32Parse(dgvrwTab_CNAE_UF["IDTABCNAE"].ToString()) == idTab_CNAEX)
                                    {
                                        frmTab_CNAE_Vis.drTab_CNAE_UFGeral = dgvrwTab_CNAE_UF;

                                        //frmTab_CNAE_Vis.drTab_CNAE_UFGeral["ID"] = idTab_CNAE_UFX;
                                        frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IDTABCNAE"] = idTab_CNAEX;
                                        frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IDUF"] = idUFX;
                                        frmTab_CNAE_Vis.drTab_CNAE_UFGeral["ESTADO"] =tbxUF.Text;
                                        frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IVA"] = clsParser.DecimalParse(tbxIVA.Text);
                                        frmTab_CNAE_Vis.drTab_CNAE_UFGeral["IDDIZERESNFV"] = iddizeresnfv;  
                                        frmTab_CNAE_Vis.drTab_CNAE_UFGeral["ICMSST"] = clsParser.DecimalParse(tbxIcmsST.Text);
                           
                                        frmTab_CNAE_Vis.drTab_CNAE_UFGeral["TEMP"] = "S";

                                        frmTab_CNAE_Vis.achouTab_CNAE_UFGeral = true;
                                        break;
                                    }
                                }
                            }
                            if (frmTab_CNAE_Vis.achouTab_CNAE_UFGeral == false)
                            {
                                frmTab_CNAE_Vis.dtTab_CNAE_UFGeral.ImportRow(frmTab_CNAE_Vis.drTab_CNAE_UFGeral);
                            }
                            break;
                        }
                    }
                }
            }

        }

        private Boolean VerificaGravacaoTab_CNAE_UF(String _tipo)
        {
            try
            {
                // Verficando se a referencia esta em branco
                if (_tipo == "TODOS")
                {
                    if (tbxUF.Text.Trim() == "")
                    {
                        throw new Exception("Informe a UF");
                    }
                    if (tbxIVA.Text.Trim() == "")
                    {
                        throw new Exception("Informe o IVA");
                    }
                    if (iddizeresnfv == 0)
                    {
                        throw new Exception("Informe o Codigo dos dizeres");
                    }

                    foreach (DataRow dgvrTab_CNAE_UF in ((DataTable)dgvTab_CNAE_UF.DataSource).Rows)
                    {
                        if (dgvrTab_CNAE_UF.RowState != DataRowState.Deleted)
                        {
                            if (dgvrTab_CNAE_UF["ESTADO"].ToString() == tbxUF.Text &&
                                clsParser.Int32Parse(dgvrTab_CNAE_UF["ID"].ToString()) != idTab_CNAE_UFX)
                            {
                                throw new Exception("Não é permitido cadastrar o mesmo Estado mais de uma vez,para uma CNAE!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = DialogResult.Cancel; // Retorna para o foco
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                switch (ex.Message)
                {
                    case "Informe a UF":
                        btnUF_IVA.Select();
                        //tbxUF.SelectAll();
                        break;

                    case "Informe o IVA":
                        tbxIVA.Select();
                        tbxIVA.SelectAll();
                        break;

                    case "Informe o Codigo dos dizeres":
                        btnIdDizeresNFV.Select();                       
                        break;

                    case "Não é permitido cadastrar o mesmo Estado mais de uma vez,para uma CNAE!":
                        btnUF_IVA.Select();
                        //tbxUF.SelectAll();
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
                valoresatuais = PreencheValoresTab_CNAE_UF();
                if (clsSelectInsertUpdateBLL.ComparaValores(valoresatuais, valoresantigos) == false)
                {
                    resultado = MessageBox.Show("Deseja Salvar e Retornar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

                    if (resultado == DialogResult.Yes)
                    {
                        SalvarTab_CNAE_UF();
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
                    if (idTab_CNAE_UFX > 0)
                    {
                        frmTab_CNAE_Vis.indexTab_CNAE_UFGeral = idTab_CNAE_UFX;
                    }
                    leaveNocheck = true;
                    this.Close();
                    this.Dispose();
                }
            }
        }

        private void frmTab_CNAE_UF_Activated(object sender, EventArgs e)
        {        
            Lupa();            
        }

        private Boolean MovimentaTab_CNAE_UF()
        {
            try
            {
                valoresatuais = PreencheValoresTab_CNAE_UF();
                if (clsSelectInsertUpdateBLL.ComparaValores(valoresatuais, valoresantigos) == false)
                {
                    resultado = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        if (resultado != DialogResult.Cancel)
                        {
                            SalvarTab_CNAE_UF();
                        }
                    }
                    else if (resultado == DialogResult.No)
                    {
                        if (VerificaGravacaoTab_CNAE_UF("TODOS") == false)
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
                    if (VerificaGravacaoTab_CNAE_UF("TODOS") == false)
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
                MessageBox.Show("MovimentaTab_CNAE_UF - " + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                if (MovimentaTab_CNAE_UF())
                {
                    idTab_CNAE_UFX = clsParser.Int32Parse(((DataGridViewRowCollection)dgvTab_CNAE_UF.Rows)[0].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                    dgvTab_CNAE_UF[3, 0].Selected = true;
                    CarregaTab_CNAE_UF(idTab_CNAE_UFX);
                    leaveNocheck = true;
                    frmTab_CNAE_UF_Activated(sender, e);
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
                if (MovimentaTab_CNAE_UF())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection)dgvTab_CNAE_UF.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idTab_CNAE_UFX)
                        {
                            if (dgvr.Index != 0)
                            {
                                idTab_CNAE_UFX = clsParser.Int32Parse(((DataGridViewRowCollection)dgvTab_CNAE_UF.Rows)[((DataGridViewRowCollection)dgvTab_CNAE_UF.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente                                
                                dgvTab_CNAE_UF[3, ((DataGridViewRowCollection)dgvTab_CNAE_UF.Rows).
                                     GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaTab_CNAE_UF(idTab_CNAE_UFX);
                    leaveNocheck = true;
                    frmTab_CNAE_UF_Activated(sender, e);

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
                if (MovimentaTab_CNAE_UF())
                {
                    foreach (DataGridViewRow dgvr in ((DataGridViewRowCollection)dgvTab_CNAE_UF.Rows))
                    {
                        if (Int32.Parse(dgvr.Cells["ID"].Value.ToString()) == idTab_CNAE_UFX)
                        {
                            if (dgvr.Index < ((DataGridViewRowCollection)dgvTab_CNAE_UF.Rows).Count - 1)
                            {
                                idTab_CNAE_UFX = clsParser.Int32Parse(((DataGridViewRowCollection)dgvTab_CNAE_UF.Rows)[((DataGridViewRowCollection)dgvTab_CNAE_UF.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells["ID"].Value.ToString());

                                // seleciona o registro corrente
                                dgvTab_CNAE_UF[3, ((DataGridViewRowCollection)dgvTab_CNAE_UF.Rows).
                                     GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Selected = true;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    CarregaTab_CNAE_UF(idTab_CNAE_UFX);
                    leaveNocheck = true;
                    frmTab_CNAE_UF_Activated(sender, e);
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
                if (MovimentaTab_CNAE_UF())
                {
                    idTab_CNAE_UFX = clsParser.Int32Parse(((DataGridViewRowCollection)dgvTab_CNAE_UF.Rows)[((DataGridViewRowCollection)dgvTab_CNAE_UF.Rows).Count - 1].Cells["ID"].Value.ToString());

                    // seleciona o registro corrente
                    dgvTab_CNAE_UF[3, ((DataGridViewRowCollection)dgvTab_CNAE_UF.Rows).Count - 1].Selected = true;
                    CarregaTab_CNAE_UF(idTab_CNAE_UFX);
                    leaveNocheck = true;
                    frmTab_CNAE_UF_Activated(sender, e);
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

        private void frmTab_CNAE_UF_Shown(object sender, EventArgs e)
        {
            
        }

        private void btnUF_IVA_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = "dgvUF";
            //frmGeralVis frmGeralVis = new frmGeralVis(clsInfo.conexaosqldados,
            //                "ESTADOS INNER JOIN ESTADOSICMS ON ESTADOS.ID=ESTADOSICMS.IDESTADO",
            //                "ESTADOS.ID," +
            //                "ESTADOS.ESTADO," +
            //                "ESTADOSICMS.IVA",
            //                "", "ID", new GridColuna[]{
            //                new GridColuna("Id", "ID", 1, false, DataGridViewContentAlignment.MiddleLeft),
            //                new GridColuna("UF", "ESTADO", 40, true, DataGridViewContentAlignment.MiddleLeft),
            //                new GridColuna("IVA", "IVA", 70, true, DataGridViewContentAlignment.MiddleRight)},
            //                true,
            //                8,
            //               null
            //                );

            //
            //FormHelper.AbrirForm(this.MdiParent, frmGeralVis);

            frmEstadosPes frmEstadosPes = new frmEstadosPes();
            frmEstadosPes.Init(0);

            
            clsFormHelper.AbrirForm(this.MdiParent, frmEstadosPes, clsInfo.conexaosqldados);
            tbxIVA.Select();
            tbxIVA.SelectAll();
        }

        private void Lupa()
        {
            if (clsInfo.zrow != null)
            {
                if (clsInfo.znomegrid == "dgvUF")
                {
                    idUFX = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxUF.Text = clsInfo.zrow.Cells["ESTADO"].Value.ToString();                    
                    tbxIVA.Select();
                    tbxIVA.SelectAll();
                }

                if (clsInfo.znomegrid == "dgvDizeres")
                {
                    iddizeresnfv = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxDizeresNFV_Codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                }
                clsInfo.zrow = null;
                clsInfo.znomegrid = "";

                clsInfo.zrow = null;
                clsInfo.znomegrid = "";
            }
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
    }
}
