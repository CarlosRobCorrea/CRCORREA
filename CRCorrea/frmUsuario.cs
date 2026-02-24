using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmUsuario : Form
    {
        Int32 id;
        int idmaqct;
        String senha;
        Int32 idCliente;
        String conexao;

        
        clsInfo clsInfo = new clsInfo();

        clsUsuarioBLL UsuarioBLL;
        clsUsuarioInfo UsuarioInfo;
        clsUsuarioInfo UsuarioInfoOld;

        clsUsuarioFormsBLL UsuarioFormsBLL;
        clsUsuarioFormsInfo UsuarioFormsInfo;

        DataTable dtUsuarioForms;

        BackgroundWorker bwrUsuarioForms;

        clsUsuarioFormsObjetoBLL UsuarioFormsObjetoBLL;
        clsUsuarioFormsObjetoInfo UsuarioFormsObjetoInfo;

        BackgroundWorker bwrUsuarioFormsObjeto;

        DataTable dtUsuarioFormsObjeto;

        DataGridViewRowCollection rows;

        DialogResult resultado = new DialogResult();

        public frmUsuario()
        {
            InitializeComponent();
        }

        public void Init(Int32 id,
                         DataGridViewRowCollection rows,
                         String _conexao)
        {
            tbxEmail.CharacterCasing = CharacterCasing.Normal;
            tbxEmailSenha.CharacterCasing = CharacterCasing.Normal;

            conexao = _conexao;

            this.id = id;
            this.rows = rows;

            

            UsuarioBLL = new clsUsuarioBLL();
            UsuarioFormsBLL = new clsUsuarioFormsBLL();
            UsuarioFormsObjetoBLL = new clsUsuarioFormsObjetoBLL();

            clsVisual.AutoCompletar(conexao, "select usuario from usuario where ativo=1 order by usuario", tbxUsuarioCopiarPermissoes);
            clsVisual.FillComboBox(cbxMaqct_codigo, "select codigo from maqct where ativo='S' order by codigo", conexao);
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            UsuarioCarregar();
        }

        private void UsuarioCarregar()
        {
            UsuarioInfoOld = new clsUsuarioInfo();

            if ( id == 0)
            {
                UsuarioInfo = new clsUsuarioInfo();
                UsuarioInfo.Usuario = "";
                UsuarioInfo.Senha = "";
                UsuarioInfo.Ativo = true;
                UsuarioInfo.cognome = "";   
                UsuarioInfo.idfolha = 0;
                UsuarioInfo.Weekend = false;
                UsuarioInfo.Trocar = DateTime.Now.AddMonths(3);//NARCISIO DIA 02/03/2011
                UsuarioInfo.Horaini = DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute);
                UsuarioInfo.Horafim = DateTime.Now.AddHours(23-DateTime.Now.Hour).AddMinutes(59-DateTime.Now.Minute);
                UsuarioInfo.Dataval = DateTime.Now.AddYears(1);
                UsuarioInfo.Datault = DateTime.Now;
                UsuarioInfo.Nivelusuario = "Normal";
                UsuarioInfo.Gruposystem = "N";
                UsuarioInfo.Grupo = "";
            }
            else
            {
                UsuarioInfo = UsuarioBLL.Carregar(id, conexao);
            }

            UsuarioCampos(UsuarioInfo);
            UsuarioFillInfo(UsuarioInfoOld);

            EnabledControls(false);
            bwrUsuarioForms_Run();

            tbxUsuario.Select();
            tbxUsuario.SelectAll();
        }

        private Boolean Usuario_HouveMudanca()
        {
            UsuarioInfo = new clsUsuarioInfo();
            UsuarioFillInfo(UsuarioInfo);

            if (UsuarioBLL.Equals(UsuarioInfo, UsuarioInfoOld) == false)
            {
                return true;
            }

            return false;
            
        }

        void UsuarioFillInfo(clsUsuarioInfo info)
        {
            info.Id = id;
            info.Senha = senha;
            info.idmaqct = idmaqct;

            info.Usuario = tbxUsuario.Text;
            info.Dataval = DateTime.Parse(tbxDataDeValidade.Text);
            info.Datault = DateTime.Parse(tbxDataUltimoAcesso.Text);
            info.filial = clsInfo.zfilial; /////////////
            if (cbxAtivo.Text.Substring(0, 1) == "S")
            {
                info.Ativo = true;
            }
            else
            {
                info.Ativo = false;
            }

            if (cbxPodeAcessar.Text.Substring(0, 1) == "S")
            {
                info.Weekend = true;
            }
            else
            {
                info.Weekend = false;
            }
            info.Horaini = DateTime.Parse(tbxHoraInicio.Text);
            info.Horafim = DateTime.Parse(tbxHoraFim.Text);
            info.Trocar = DateTime.Parse(tbxDataProximaTroca.Text);
            info.Trocasenha = cbxTrocaASenha.Text.Substring(0, 1);
            info.Email = tbxEmail.Text;
            info.Nivelusuario = cbxNivelUsuario.Text;
            info.Emailsenha = tbxEmailSenha.Text;
            info.Gruposystem = cbxGrupoSystem.Text.Substring(0, 1);
            info.Grupo = cbxGrupo.Text.Substring(0, 1);

            info.cognome = tbxChapa.Text;
            info.idfolha = idCliente;
        }

        void UsuarioCampos(clsUsuarioInfo info)
        {
            id = info.Id;
            senha = info.Senha;
            idmaqct = info.idmaqct;
            
            tbxUsuario.Text = info.Usuario;
            tbxDataDeValidade.Text = info.Dataval.ToString("dd/MM/yyyy");
            tbxDataUltimoAcesso.Text = info.Datault.ToString("dd/MM/yyyy");
            tbxHoraInicio.Text = info.Horaini.ToString("HH:mm");
            tbxHoraFim.Text = info.Horafim.ToString("HH:mm");
            tbxDataProximaTroca.Text = info.Trocar.ToString("dd/MM/yyyy");
            tbxEmail.Text = info.Email;
            tbxEmailSenha.Text = info.Emailsenha;
            cbxNivelUsuario.SelectedIndex = clsVisual.SelecionarIndex(info.Nivelusuario, 1, cbxNivelUsuario);
            cbxTrocaASenha.SelectedIndex = clsVisual.SelecionarIndex(info.Trocasenha, 1, cbxTrocaASenha);
            cbxGrupo.SelectedIndex = clsVisual.SelecionarIndex(info.Grupo, 1, cbxGrupo);

            String maqct_codigo = Procedure.PesquisaoPrimeiro(conexao, "select codigo from maqct where id=" + idmaqct);
            if (maqct_codigo != null)
            {
                cbxMaqct_codigo.SelectedIndex = clsVisual.SelecionarIndex(maqct_codigo, 0, cbxMaqct_codigo);
            }

            if (info.Ativo == true)
            {
                cbxAtivo.SelectedIndex = clsVisual.SelecionarIndex("S", 1, cbxAtivo);
            }
            else
            {
                cbxAtivo.SelectedIndex = clsVisual.SelecionarIndex("N", 1, cbxAtivo);
            }

            if (info.Weekend == true)
            {
                cbxPodeAcessar.SelectedIndex = clsVisual.SelecionarIndex("S", 1, cbxPodeAcessar);
            }
            else
            {
                cbxPodeAcessar.SelectedIndex = clsVisual.SelecionarIndex("N", 1, cbxPodeAcessar);
            }
            cbxGrupoSystem.SelectedIndex = clsVisual.SelecionarIndex(info.Gruposystem, 1, cbxGrupoSystem);

            tbxChapa.Text = info.cognome;
            idCliente = info.idfolha;
        }
 
        private void UsuarioSalvar()
        {
            UsuarioInfo = new clsUsuarioInfo();
            UsuarioFillInfo(UsuarioInfo);

            if (id == 0)
            {
                UsuarioInfo.Id = UsuarioBLL.Incluir(UsuarioInfo,conexao);
            }
            else
            {
                UsuarioBLL.Alterar(UsuarioInfo, conexao);
            }

            // ####################################
            // Tabela: USUARIOFORMS
            foreach (DataRow row in dtUsuarioForms.Rows)
            {
                if (row.RowState == DataRowState.Modified)
                {
                    UsuarioFormsInfo = UsuarioFormsBLL.Carregar(clsParser.Int32Parse(row["id"].ToString()), conexao);

                    if (UsuarioFormsInfo == null ||
                        UsuarioFormsInfo.id == 0)
                    {
                        UsuarioFormsInfo.idforms = clsParser.Int32Parse(row["FORMSID"].ToString());
                        UsuarioFormsInfo.idusuario = UsuarioInfo.Id;
                        UsuarioFormsInfo.permitido = row["PERMITIDO"].ToString();

                        UsuarioFormsBLL.Incluir(UsuarioFormsInfo, conexao);
                    }
                    else
                    {
                        UsuarioFormsInfo.permitido = row["permitido"].ToString();

                        UsuarioFormsBLL.Alterar(UsuarioFormsInfo, conexao);
                    }
                }
            }

            // ####################################
            // Tabela: USUARIOFORMSOBJETO
            foreach (DataRow row in dtUsuarioFormsObjeto.Rows)
            {
                if (row.RowState == DataRowState.Modified)
                {
                    UsuarioFormsObjetoInfo = UsuarioFormsObjetoBLL.Carregar(clsParser.Int32Parse(row["id"].ToString()), conexao);

                    if (UsuarioFormsObjetoInfo == null ||
                        UsuarioFormsObjetoInfo.id == 0)
                    {
                        UsuarioFormsObjetoInfo.id = 0;
                        UsuarioFormsObjetoInfo.idformsobjeto = clsParser.Int32Parse(row["IDFORMSOBJETO"].ToString());
                        UsuarioFormsObjetoInfo.idusuario = UsuarioInfo.Id;
                        UsuarioFormsObjetoInfo.visivel = row["VISIVEL"].ToString();
                        UsuarioFormsObjetoInfo.ativo = row["ATIVO"].ToString();

                        UsuarioFormsObjetoBLL.Incluir(UsuarioFormsObjetoInfo, conexao);
                    }
                    else
                    {
                        UsuarioFormsObjetoInfo.visivel = row["VISIVEL"].ToString();
                        UsuarioFormsObjetoInfo.ativo = row["ATIVO"].ToString();

                        UsuarioFormsObjetoBLL.Alterar(UsuarioFormsObjetoInfo, conexao);
                    }
                }
            }
        }

        private DialogResult Salvar()
        {
            DialogResult drt;

            drt = MessageBox.Show("Deseja Salvar esse registro?", Application.CompanyName, MessageBoxButtons.YesNoCancel);

            if (drt == DialogResult.Yes)
            {
                UsuarioSalvar();
            }

            return drt;
        }

        void bwrUsuarioForms_Run()
        {
            pbxUsuarioForms.Visible = true;

            dgvUsuarioForms.DataSource = null;

            bwrUsuarioForms = new BackgroundWorker();
            bwrUsuarioForms.DoWork += new DoWorkEventHandler(bwrUsuarioForms_DoWork);
            bwrUsuarioForms.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrUsuarioForms_RunWorkerCompleted);
            bwrUsuarioForms.RunWorkerAsync();

            bwrUsuarioFormsObjeto_Run();
        }

        void bwrUsuarioForms_DoWork(object sender, DoWorkEventArgs e)
        {
            dtUsuarioForms = clsUsuarioFormsBLL.GridDados2(id, conexao);
        }

        void bwrUsuarioForms_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                clsUsuarioFormsBLL.GridMonta2(dgvUsuarioForms, dtUsuarioForms);

                if (dgvUsuarioForms.CurrentRow != null && dgvUsuarioForms.Rows.Count > 0)
                {
                    dgvUsuarioForms.Rows[0].Cells["nome"].Selected = true;
                    SelecionaLinha();
                }

                FiltrarForms();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                pbxUsuarioForms.Visible = false;
            }
        }

        void bwrUsuarioFormsObjeto_Run()
        {
            pbxUsuarioFormsObjeto.Visible = true;

            dgvUsuarioFormsObjeto.DataSource = null;

            bwrUsuarioFormsObjeto = new BackgroundWorker();
            bwrUsuarioFormsObjeto.DoWork += new DoWorkEventHandler(bwrUsuarioFormsObjeto_DoWork);
            bwrUsuarioFormsObjeto.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrUsuarioFormsObjeto_RunWorkerCompleted);
            bwrUsuarioFormsObjeto.RunWorkerAsync();
        }

        void bwrUsuarioFormsObjeto_DoWork(object sender, DoWorkEventArgs e)
        {
            dtUsuarioFormsObjeto = clsUsuarioFormsObjetoBLL.GridDados2(id, conexao);
        }

        void bwrUsuarioFormsObjeto_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                clsUsuarioFormsObjetoBLL.GridMonta2(dgvUsuarioFormsObjeto, dtUsuarioFormsObjeto);

                SelecionaLinha();

                FiltrarItens();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                pbxUsuarioFormsObjeto.Visible = false;
                EnabledControls(true);

                dtUsuarioFormsObjeto.DefaultView.RowFilter = "idforms = 0";
            }
        }

        private void tsbSalvar_Click(object sender, EventArgs e)
        {
            if (Salvar() == DialogResult.Cancel)
            {
                return;
            }

            this.Close();
        }

        private void tsbRetornar_Click(object sender, EventArgs e)
        {
            if (Usuario_HouveMudanca() == true)
            {
                tsbSalvar.PerformClick();
            }
            else
            {
                this.Close();
            }
        }

        private void tsbPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (Usuario_HouveMudanca() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                if (rows != null)
                {
                    id = Int32.Parse(rows[0].Cells[0].Value.ToString());
                    UsuarioCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (Usuario_HouveMudanca() == true)
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
                                UsuarioCarregar();
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

        private void tsbProximo_Click(object sender, EventArgs e)
        {
            try
            {
                if (Usuario_HouveMudanca() == true)
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
                                UsuarioCarregar();
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

        private void tsbUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (Usuario_HouveMudanca() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                if (rows != null)
                {
                    id = Int32.Parse(rows[rows.Count - 1].Cells[0].Value.ToString());
                    UsuarioCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            TrataCampos((Control)sender);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void ControlKeyDownHora(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownHora((TextBox)sender, e);
        }

        void SelecionaLinha()
        {
            if (dtUsuarioFormsObjeto == null)
            {
                return;
            }

            FiltrarItens();

            FillCores();
        }

        private void dgvForms_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            SelecionaLinha();
        }

        private void dgvUsuarioForms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsuarioForms.CurrentRow != null)
            {
                if (dgvUsuarioForms.Columns[dgvUsuarioForms.CurrentCell.ColumnIndex].Name == "PERMITIDO")
                {
                    DataRow row = dtUsuarioForms.Select("FORMSID=" + dgvUsuarioForms.CurrentRow.Cells["FORMSID"].Value.ToString())[0];

                    if (row["PERMITIDO"] == null ||
                        row["PERMITIDO"].ToString() == string.Empty ||
                        row["PERMITIDO"].ToString() == "N")
                    {
                        row["PERMITIDO"] = "S";
                    }
                    else if (row["PERMITIDO"].ToString() == "S")
                    {
                        row["PERMITIDO"] = "N";
                    }

                    FillCores();
                }
                else
                {
                    string formName;
                    string assemblyName;

                    formName = dgvUsuarioForms.CurrentRow.Cells["NOMEFORM"].Value.ToString();
                    assemblyName = dgvUsuarioForms.CurrentRow.Cells["PROJETO"].Value.ToString();

                    assemblyName = assemblyName.Substring(0, assemblyName.Length - 4);
                    formName = assemblyName + "." + formName;

                    clsFormHelper.FormLoadWithData(formName, assemblyName, clsParser.Int32Parse(dgvUsuarioForms.CurrentRow.Cells["FORMSID"].Value.ToString()), ref dtUsuarioFormsObjeto);
                }
            }
        }

        void FillCores()
        {
            for (int x = 0; x < dgvUsuarioForms.Rows.Count; x++)
            {
                if (SystemInformation.HighContrast)
                {
                    if (dgvUsuarioForms.Rows[x].Cells["permitido"].Value == null ||
                        dgvUsuarioForms.Rows[x].Cells["permitido"].Value.ToString() == "N")
                    {
                        dgvUsuarioForms.Rows[x].DefaultCellStyle.ForeColor = System.Drawing.Color.Tomato;

                    }
                    else if (dgvUsuarioForms.Rows[x].Cells["permitido"].Value.ToString() == "S")
                    {
                        dgvUsuarioForms.Rows[x].DefaultCellStyle.ForeColor = System.Drawing.Color.SpringGreen;
                    }
                    else
                    {
                        dgvUsuarioForms.Rows[x].DefaultCellStyle.ForeColor = System.Drawing.Color.MediumSpringGreen;
                    }
                }
                else
                {
                    if (dgvUsuarioForms.Rows[x].Cells["permitido"].Value == null ||
                        dgvUsuarioForms.Rows[x].Cells["permitido"].Value.ToString() == "N")
                    {
                        dgvUsuarioForms.Rows[x].DefaultCellStyle.BackColor = System.Drawing.Color.Tomato;

                    }
                    else if (dgvUsuarioForms.Rows[x].Cells["permitido"].Value.ToString() == "S")
                    {
                        dgvUsuarioForms.Rows[x].DefaultCellStyle.BackColor = System.Drawing.Color.SpringGreen;
                    }
                    else
                    {
                        dgvUsuarioForms.Rows[x].DefaultCellStyle.BackColor = System.Drawing.Color.MediumSpringGreen;
                    }
                }
            }

            for (int x = 0; x < dgvUsuarioFormsObjeto.Rows.Count; x++)
            {
                if (SystemInformation.HighContrast)
                {
                    if (dgvUsuarioFormsObjeto.Rows[x].Cells["visivel"].Value == null ||
                        dgvUsuarioFormsObjeto.Rows[x].Cells["visivel"].Value.ToString() == "N")
                    {
                        dgvUsuarioFormsObjeto.Rows[x].DefaultCellStyle.ForeColor = System.Drawing.Color.Tomato;

                    }
                    else if (dgvUsuarioFormsObjeto.Rows[x].Cells["visivel"].Value.ToString() == "S")
                    {
                        dgvUsuarioFormsObjeto.Rows[x].DefaultCellStyle.ForeColor = System.Drawing.Color.SpringGreen;
                    }
                    else
                    {
                        dgvUsuarioFormsObjeto.Rows[x].DefaultCellStyle.ForeColor = System.Drawing.Color.MediumSpringGreen;
                    }

                }
                else
                {
                    if (dgvUsuarioFormsObjeto.Rows[x].Cells["visivel"].Value == null ||
                        dgvUsuarioFormsObjeto.Rows[x].Cells["visivel"].Value.ToString() == "N")
                    {
                        dgvUsuarioFormsObjeto.Rows[x].DefaultCellStyle.BackColor = System.Drawing.Color.Tomato;

                    }
                    else if (dgvUsuarioFormsObjeto.Rows[x].Cells["visivel"].Value.ToString() == "S")
                    {
                        dgvUsuarioFormsObjeto.Rows[x].DefaultCellStyle.BackColor = System.Drawing.Color.SpringGreen;
                    }
                    else
                    {
                        dgvUsuarioFormsObjeto.Rows[x].DefaultCellStyle.BackColor = System.Drawing.Color.MediumSpringGreen;
                    }
                }
            }
        }

        private void dgvUsuarioFormsObjeto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsuarioFormsObjeto.CurrentRow != null)
            {
                if (dgvUsuarioFormsObjeto.Columns[dgvUsuarioFormsObjeto.CurrentCell.ColumnIndex].Name == "ATIVO")
                {
                    DataRow row = dtUsuarioFormsObjeto.Select("IDFORMSOBJETO=" + dgvUsuarioFormsObjeto.CurrentRow.Cells["IDFORMSOBJETO"].Value.ToString())[0];

                    if (row["ATIVO"] == null ||
                        row["ATIVO"].ToString() == string.Empty ||
                        row["ATIVO"].ToString() == "N")
                    {
                        row["ATIVO"] = "S";
                    }
                    else if (row["ATIVO"].ToString() == "S")
                    {
                        row["ATIVO"] = "N";
                    }

                    FillCores();
                }
                else if (dgvUsuarioFormsObjeto.Columns[dgvUsuarioFormsObjeto.CurrentCell.ColumnIndex].Name == "VISIVEL")
                {
                    DataRow row = dtUsuarioFormsObjeto.Select("IDFORMSOBJETO=" + dgvUsuarioFormsObjeto.CurrentRow.Cells["IDFORMSOBJETO"].Value.ToString())[0];

                    if (row["VISIVEL"] == null ||
                        row["VISIVEL"].ToString() == string.Empty ||
                        row["VISIVEL"].ToString() == "N")
                    {
                        row["VISIVEL"] = "S";
                    }
                    else if (row["VISIVEL"].ToString() == "S")
                    {
                        row["VISIVEL"] = "N";
                    }

                    FillCores();
                }
            }
        }

        void FiltrarForms()
        {
            clsGridHelper.Filtrar(tstbxFiltrarForms.Text, dgvUsuarioForms);

            FillCores();
        }

        void FiltrarItens()
        {
            clsGridHelper.Filtrar(tstbxFiltrarItens.Text, dgvUsuarioFormsObjeto);

            if (dgvUsuarioForms.CurrentRow != null)
            {
                if (dtUsuarioFormsObjeto.DefaultView.RowFilter != "")
                {
                    dtUsuarioFormsObjeto.DefaultView.RowFilter = "(" + dtUsuarioFormsObjeto.DefaultView.RowFilter + ") and idforms = " + dgvUsuarioForms.CurrentRow.Cells["FORMSID"].Value.ToString();
                }
                else
                {
                    dtUsuarioFormsObjeto.DefaultView.RowFilter = "idforms = " + dgvUsuarioForms.CurrentRow.Cells["FORMSID"].Value.ToString();
                }
            }

            FillCores();
        }

        private void tstbxFiltrarForms_KeyUp(object sender, KeyEventArgs e)
        {
            FiltrarForms();
        }

        private void tstbxFiltrarItens_KeyUp(object sender, KeyEventArgs e)
        {
            FiltrarItens();
        }

        void EnabledControls(Boolean b)
        {
            gbxUsuario.Enabled = b;
            gbxHorario.Enabled = b;
            gbxAtivo.Enabled = b;
            gbxTrocaDeSenha.Enabled = b;
            gbxEmail.Enabled = b;
            gbxFuncao.Enabled = b;
            gbxGrupoPermissao.Enabled = b;
            tspUsuario.Enabled = b;
            gbxCentrocusto.Enabled = b;
            gbxChapaFuncionario.Enabled = b;
            gbxUsuarioForms.Enabled = b;
            dgvUsuarioFormsObjeto.Enabled = b;
        }

        private void tsbZerarSenha_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                resultado = MessageBox.Show("Deseja mesmo zerar a senha deste Usuário ?", "Aplisoft", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (resultado == DialogResult.Yes)
                {
                    senha = "";
                    MessageBox.Show("Sua senha foi zerada com sucesso");
                }
            }
            else
            {
                MessageBox.Show("Não é possível zerar a senha pois o usuário ainda não existe");
            }
        }

        private void tsbTrocarSenha_Click(object sender, EventArgs e)
        {
            if (id > 0)
            {
                DialogResult res;

                frmSenhaTroca SenhaTroca = new frmSenhaTroca();
                SenhaTroca.Init(conexao, id);

                res = SenhaTroca.ShowDialog();
                senha = Procedure.PesquisaoPrimeiro(conexao, "select senha from USUARIO where ID=" + id, "");
                //senha = Procedure.PesquisaValor(conexao, "USUARIO", "SENHA", "ID", id);

                if (res == DialogResult.OK)
                {
                    MessageBox.Show("Sua senha foi alterada com sucesso");
                }
            }
            else
            {
                MessageBox.Show("Não é possivel trocar a senha pois o usuário ainda não existe");
            }
        }

        private void tsbCopiarPermissoes_Click(object sender, EventArgs e)
        {
            plCopiarPermissoes.Visible = true;

            gbxUsuario.Enabled = false;
            gbxHorario.Enabled = false;
            gbxAtivo.Enabled = false;
            gbxTrocaDeSenha.Enabled = false;
            //gbxCentrocusto.Enabled = false;
            gbxEmail.Enabled = false;
            gbxFuncao.Enabled = false;
            gbxGrupoPermissao.Enabled = false;
            gbxUsuarioForms.Enabled = false;
            gbxUsuarioFormsObjeto.Enabled = false;
            tspUsuario.Enabled = false;
        }

        private void btnUsuarioVis_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnUsuarioVis.Name;
            frmUsuarioVis frmUsuarioVis = new frmUsuarioVis();
            frmUsuarioVis.Init(conexao);

            clsFormHelper.AbrirForm(this.MdiParent, frmUsuarioVis, conexao);
        }

        private void frmUsuario_Activated(object sender, EventArgs e)
        {
            if (clsInfo.znomegrid != "" &&
                clsInfo.zrow != null)
            {
                if (clsInfo.znomegrid == btnUsuarioVis.Name)
                {
                    tbxUsuarioCopiarPermissoes.Text = clsInfo.zrow.Cells["usuario"].Value.ToString();
                }
            }
            TrataCampos((Control)sender);

        }

        private void btnCopiarPermissoesUsuarioOk_Click(object sender, EventArgs e)
        {
            btnCancelar.PerformClick();

            Int32 usuario_id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(conexao, "select id from usuario where usuario='" + tbxUsuarioCopiarPermissoes.Text + "'"));

            String query;

            SqlDataAdapter sda;

            DataTable dtCopyForms = new DataTable();
            DataTable dtCopyFormsObjetos = new DataTable();

            query = "select * from usuarioforms where idusuario = " + usuario_id;
            sda = new SqlDataAdapter(query, conexao);
            sda.Fill(dtCopyForms);

            query = "select * from usuarioformsobjeto where idusuario = " + usuario_id;
            sda = new SqlDataAdapter(query, conexao);
            sda.Fill(dtCopyFormsObjetos);

            foreach (DataRow copyFormRow in dtCopyForms.Rows)
            {
                foreach (DataRow formRow in dtUsuarioForms.Select("formsid=" + copyFormRow["idforms"].ToString() + " and permitido<>'" + copyFormRow["permitido"] + "'"))
                {
                    formRow["permitido"] = copyFormRow["permitido"];
                }
            }

            foreach (DataRow copyFormObjetoRow in dtCopyFormsObjetos.Rows)
            {
                foreach (DataRow formRow in dtUsuarioFormsObjeto.Select("idformsobjeto=" + copyFormObjetoRow["idformsobjeto"].ToString() + " and (ativo<>'" + copyFormObjetoRow["ativo"] + "' or visivel<>'" + copyFormObjetoRow["visivel"] + "')"))
                {
                    formRow["ativo"] = copyFormObjetoRow["ativo"];
                    formRow["visivel"] = copyFormObjetoRow["visivel"];
                }
            }

            MessageBox.Show("Permissões Copiadas com sucesso.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            FillCores();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            plCopiarPermissoes.Visible = false;

            gbxUsuario.Enabled = true;
            gbxHorario.Enabled = true;
            gbxAtivo.Enabled = true;
            gbxTrocaDeSenha.Enabled = true;
            //gbxCentrocusto.Enabled = true;
            gbxEmail.Enabled = true;
            gbxFuncao.Enabled = true;
            gbxGrupoPermissao.Enabled = true;
            gbxUsuarioForms.Enabled = true;
            gbxUsuarioFormsObjeto.Enabled = true;
            tspUsuario.Enabled = true;
        }

        private void btnidFolha_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnidCliente.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", idCliente);
            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }
        void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null && clsInfo.znomegrid != "")
            {
                if (clsInfo.zrow != null)
                {
                    // funcionario
                    if (clsInfo.znomegrid == btnidCliente.Name)
                    {
                        if (clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString()) > 0)
                        {
                            idCliente = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                            tbxChapa.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString();
                        }
                        tbxChapa.Select();
                    }
                    if (ctl.Name == tbxChapa.Name)
                    {
                        idCliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlfolha, "select ID from cliente where cognome='" + tbxChapa.Text + "' "));
                        //tbxChapa.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqlfolha, "select cognome from funcionario where ID=" + idCliente + "");
                    }
                }
                tbxUsuario.Text = clsVisual.RemoveAcentos(tbxUsuario.Text);

                clsInfo.znomegrid = "";
                clsInfo.zrow = null;
            }
        }

        private void cbxMaqct_codigo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxMaqct_codigo.SelectedIndex != -1)
            {
                idmaqct = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(conexao, "select id from maqct where codigo='" + cbxMaqct_codigo.Text + "'"));
            }
        }
    }
}
