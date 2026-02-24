using CRCorreaFuncoes;
using CRCorreaBLL;
using CRCorreaInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmFormsScanVis : Form
    {
        DataTable dtForms;

        BackgroundWorker bwrForms;
        BackgroundWorker bwrFormsEscanear;

        frmProgresso frmProgresso;

        String[] assemblyNameCollection;

        String conexao;

        clsFormsBLL FormsBLL = new clsFormsBLL();
        

        List<string> formsVerificados;

        public frmFormsScanVis()
        {
            InitializeComponent();
        }

        public void Init(String[] assemblyNameCollection,
                         String cnn)
        {
            this.conexao = cnn;

            this.assemblyNameCollection = assemblyNameCollection;

            this.frmProgresso = new frmProgresso();
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            FiltrarForms();
        }

        void FiltrarForms()
        {
            clsGridHelper.Filtrar(tstbxFiltrarForms.Text, dgvForms);

            lblFormsRegistros.Text = "Nº de Formulários: " + dgvForms.Rows.GetRowCount(DataGridViewElementStates.Visible);

            FillCores();
        }

        private void frmFormsVis_Load(object sender, EventArgs e)
        {
            bwrForms_Run();
        }

        void bwrForms_Run()
        {
            frmProgresso.Visible = true;

            dgvForms.DataSource = null;

            bwrForms = new BackgroundWorker();
            bwrForms.DoWork += new DoWorkEventHandler(bwrForms_DoWork);
            bwrForms.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrForms_RunWorkerCompleted);
            bwrForms.RunWorkerAsync();
        }

        void bwrForms_DoWork(object sender, DoWorkEventArgs e)
        {
            dtForms = clsFormsBLL.GridDados(conexao);
        }

        void bwrForms_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                clsFormsBLL.GridMonta(dgvForms, dtForms);

                if (dgvForms.CurrentRow != null && dgvForms.Rows.Count > 0)
                {
                    dgvForms.Rows[0].Cells["nome"].Selected = true;
                }

                FiltrarForms();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                frmProgresso.Visible = false;
            }
        }

        private void tsbEscanear_Click(object sender, EventArgs e)
        {
            DialogResult resultado;
            resultado = MessageBox.Show("Deseja realmente escanear o sistema? (Isso pode levar de 5 a 10 minutos dependendo do hardware)?",
                                            Application.CompanyName,
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question,
                                            MessageBoxDefaultButton.Button1);

            if (resultado == DialogResult.Yes)
            {
                bwrFormsEscanear_Run();
            }
        }

        void bwrFormsEscanear_Run()
        {
            frmProgresso.Show();

            enableControls(false);

            bwrFormsEscanear = new BackgroundWorker();
            bwrFormsEscanear.DoWork += new DoWorkEventHandler(bwrFormsEscanear_DoWork);
            bwrFormsEscanear.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrFormsEscanear_RunWorkerCompleted);
            bwrFormsEscanear.RunWorkerAsync();
        }

        void bwrFormsEscanear_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                formsVerificados = new List<string>();

                var threads = new List<Thread>();

                foreach (string s in assemblyNameCollection)
                {
                    Thread thread = new Thread(() => AssemblyScan(s));
                    thread.Start();

                    threads.Add(thread);
                }

                bool allThreadsComplete;

                while (true)
                {
                    Thread.Sleep(2000);

                    allThreadsComplete = true;

                    foreach (var item in threads)
                    {
                        if (item.IsAlive)
                        {
                            allThreadsComplete = false;
                            break;
                        }
                    }

                    if (allThreadsComplete)
                    {
                        break;
                    }
                }

                clsFormsBLL.ChecaFormsNaoExistem(formsVerificados, conexao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void AssemblyScan(string assemblyName)
        {
            Assembly asy = Assembly.LoadFrom(assemblyName);

            clsFormsBLL FormsBLL = new clsFormsBLL();
            clsFormsInfo FormsInfo = new clsFormsInfo();

            foreach (Type t in asy.GetTypes())
            {
                if (t.BaseType == null) continue;

                if (t.BaseType.Name == "Form")
                {
                    Form formInstance = null;

                    if (FormsBLL.JaExiste(t.Name, assemblyName, conexao) == true)
                    {
                        FormsInfo = FormsBLL.Carregar(t.Name, assemblyName, conexao);
                    }
                    else
                    {
                        FormsInfo.datac = DateTime.Now;
                        FormsInfo.nomeform = t.Name;
                        FormsInfo.projeto = assemblyName;
                        FormsInfo.lista = "S";
                    }

                    try
                    {
                        formInstance = (Form)Activator.CreateInstance(t);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Formulário {0} do Assembly {1} não possue o tratamento adequado para o seu método construtor.", t.Name, assemblyName), ex);
                    }

                    if (formInstance.Text == null)
                    {
                        throw new Exception(string.Format("Formulário {0} do Assembly {1} não possue a propriedade 'Text' preenchida.", t.Name, assemblyName));
                    }
                    else
                    {
                        FormsInfo.nome = formInstance.Text;
                    }

                    if (formInstance.AccessibleDescription == null)
                    {
                        FormsInfo.descricao = "";
                    }
                    else
                    {
                        FormsInfo.descricao = formInstance.AccessibleDescription;
                    }

                    formsVerificados.Add(FormsInfo.projeto + "." + FormsInfo.nomeform);

                    if (FormsInfo.id == 0)
                    {
                        FormsInfo.id = FormsBLL.Incluir(FormsInfo, conexao);
                    }
                    else
                    {
                        FormsBLL.Alterar(FormsInfo, conexao);
                    }

                    Thread thread = new Thread(() => CheckControls(FormsInfo.id, formInstance.Controls));
                    thread.Start();
                }
            }
        }

        void CheckControls(int idForm, Control.ControlCollection controls)
        {
            // Cadastra os controles
            foreach (Control ctl in controls)
            {
                clsFormHelper.CheckControl(ctl, 0, idForm, conexao);
            }
        }

        void bwrFormsEscanear_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            frmProgresso.Visible = false;

            enableControls(true);

            MessageBox.Show("O escaneamento foi concluído.", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            bwrForms_Run();
        }

        private void dgvForms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvForms.CurrentRow != null)
            {
                Object lista = dtForms.Select("id=" + dgvForms.CurrentRow.Cells["id"].Value.ToString())[0]["lista"];

                if (lista == null)
                {
                    dtForms.Select("id=" + dgvForms.CurrentRow.Cells["id"].Value.ToString())[0]["lista"] = "S";
                }
                else if (lista.ToString() == "S")
                {
                    dtForms.Select("id=" + dgvForms.CurrentRow.Cells["id"].Value.ToString())[0]["lista"] = "N";
                }
                else if (lista.ToString() == "N")
                {
                    dtForms.Select("id=" + dgvForms.CurrentRow.Cells["id"].Value.ToString())[0]["lista"] = "S";
                }

                FillCores();

                if (tsbconfirmarAlteracoes.Enabled == false)
                {
                    tsbconfirmarAlteracoes.Enabled = true;
                }
            }
        }

        void FillCores()
        {
            for (int x = 0; x < dgvForms.Rows.Count; x++)
            {
                if (dgvForms.Rows[x].Cells["lista"].Value == null ||
                    dgvForms.Rows[x].Cells["lista"].Value.ToString() == "N")
                {
                    dgvForms.Rows[x].DefaultCellStyle.BackColor = System.Drawing.Color.Tomato;
                }
                else if (dgvForms.Rows[x].Cells["lista"].Value.ToString() == "S")
                {
                    dgvForms.Rows[x].DefaultCellStyle.BackColor = System.Drawing.Color.SpringGreen;
                }
                else
                {
                    dgvForms.Rows[x].DefaultCellStyle.BackColor = System.Drawing.Color.MediumSpringGreen;
                }
            }
        }

        private void tsbconfirmarAlteracoes_Click(object sender, EventArgs e)
        {
            SqlConnection scn;
            SqlCommand scd;

            if (dtForms != null)
            {
                scn = new SqlConnection(conexao);
                scn.Open();

                foreach (DataRow row in dtForms.Rows)
                {
                    if (row.RowState == DataRowState.Modified)
                    {
                        scd = new SqlCommand("update forms set lista = @lista where id = @id", scn);

                        scd.Parameters.Add("@lista", SqlDbType.NVarChar).Value = row["lista"].ToString();
                        scd.Parameters.Add("@id", SqlDbType.Int).Value = clsParser.Int32Parse(row["id"].ToString());

                        scd.ExecuteNonQuery();
                    }
                }

                scn.Close();
            }

            tsbconfirmarAlteracoes.Enabled = false;

            bwrForms_Run();
        }

        void CheckFormsNotExist(string assemblyName)
        {
            SqlConnection scn;
            SqlCommand scd;

            scn = new SqlConnection(conexao);
            scn.Open();

            foreach (DataRow row in dtForms.Rows)
            {
                if (row.RowState == DataRowState.Modified)
                {
                    scd = new SqlCommand("update forms set lista = @lista where id = @id", scn);

                    scd.Parameters.Add("@lista", SqlDbType.NVarChar).Value = row["lista"].ToString();
                    scd.Parameters.Add("@id", SqlDbType.Int).Value = clsParser.Int32Parse(row["id"].ToString());

                    scd.ExecuteNonQuery();
                }
            }

            scn.Close();
        }

        void enableControls(bool b)
        {
            tspTool.Enabled = b;
            dgvForms.Enabled = b;
        }

        private void tspExcluir_Click(object sender, EventArgs e)
        {
            if (dgvForms.CurrentRow != null)
            {
                DataTable dt = new DataTable();
                dt = Procedure.RetornaDataTable(clsInfo.conexaosqldados,
                       "Select * from usuarioformsobjeto inner join  " +
                       "FormsObjeto on usuarioformsobjeto.idformsobjeto= FormsObjeto.id where idforms=" +
                       (Int32)dgvForms.CurrentRow.Cells[0].Value);
                if (dt.Rows.Count > 0)
                {
                    if (MessageBox.Show("O registro possui usuários utilizando esse formulário." +
                       Environment.NewLine + "Deseja realmente 'Excluir' o registro: "
                 + dgvForms.CurrentRow.Cells[2].Value + "?", "Aplisoft",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                 MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsFormsBLL clsFormsBLL = new clsFormsBLL();
                        clsFormsBLL.Excluir((Int32)dgvForms.CurrentRow.Cells[0].Value, conexao);
                        // Remove a linha do Grid
                        dgvForms.Rows.Remove(dgvForms.CurrentRow);
                    }
                }
                else
                {
                    if (MessageBox.Show("Deseja realmente 'Excluir' o registro: "
                    + dgvForms.CurrentRow.Cells[2].Value + "?", "Aplisoft",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        clsFormsBLL clsFormsBLL = new clsFormsBLL();
                        clsFormsBLL.Excluir((Int32)dgvForms.CurrentRow.Cells[0].Value, conexao);
                        // Remove a linha do Grid
                        dgvForms.Rows.Remove(dgvForms.CurrentRow);
                    }
                }
            }
        }

        private void dgvForms_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            FillCores();
        }
    }
}
