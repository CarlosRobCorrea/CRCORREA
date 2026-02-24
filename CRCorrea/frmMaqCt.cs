using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.ComponentModel;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmMaqCt : Form
    {
        // ###############################
        // Tabela: MAQCT
        Int32 id;

        clsMaqctBLL MaqctBLL;
        clsMaqctInfo MaqctInfo;
        clsMaqctInfo MaqctInfoOld;

        // ###############################
        // Tabela: MAQCTPRECO
        Int32 maqctpreco_id;
        Int32 maqctpreco_idmaqct;
        Int32 maqctpreco_idindice;

        Int32 maqctpreco_posicao;

        clsMaqctprecoBLL MaqctprecoBLL;
        clsMaqctprecoInfo MaqctprecoInfo;
        clsMaqctprecoInfo MaqctprecoInfoOld;

        DataTable dtMaqctPreco;
        GridColuna[] dtMaqctPrecoColunas;

        BackgroundWorker bwrMaqctPreco;

        // ###############################
        // Comum
        clsBasicReport BR;
        
        

        DataGridViewRowCollection rows;

        Int32 _maqctpreco_guiaatual;
        Int32 maqctpreco_guiaatual
        {
            get { return _maqctpreco_guiaatual; }
            set
            {
                if (_maqctpreco_guiaatual != 0 && _maqctpreco_guiaatual != 1)
                {
                    return;
                }
                else
                {
                    _maqctpreco_guiaatual = value;
                }

                tclMaqctPreco.SelectedIndex = _maqctpreco_guiaatual;
            }
        }

        public frmMaqCt()
        {
            InitializeComponent();
        }

        public void Init(Int32 id,
                         DataGridViewRowCollection rows)
        {
            this.id = id;
            this.rows = rows;

            BR = new clsBasicReport(this, dgvMaqctpreco, new ToolTip());
            
            

            MaqctBLL = new clsMaqctBLL();
            MaqctprecoBLL = new clsMaqctprecoBLL();

            dtMaqctPrecoColunas = new GridColuna[]
            {
                new GridColuna("Data", "DATA", 90, true,DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Valor", "VALOR", 90, true, DataGridViewContentAlignment.MiddleRight)
            };

            tspMaqctPreco.Visible = true;
            tspMaqctPrecoVis.Visible = true;
            tspMaqct.Visible = true;
        }

        private void frmMaqct_Load(object sender, EventArgs e)
        {
            MaqctCarregar();
        }

        private void MaqctCarregar()
        {
            MaqctInfoOld = new clsMaqctInfo();

            if (id == 0)
            {
                tsbPrimeiro.Enabled = false;
                tsbAnterior.Enabled = false;
                tsbProximo.Enabled = false;
                tsbUltimo.Enabled = false;

                MaqctInfo = new clsMaqctInfo();
                MaqctInfo.ativo = "S";
                MaqctInfo.codigo = "";
                MaqctInfo.nome = "";
            }
            else
            {
                MaqctInfo = MaqctBLL.Carregar(id, clsInfo.conexaosqldados);
            }

            MaqctCampos(MaqctInfo);
            MaqctFillInfo(MaqctInfoOld);

            bwrMaqctPreco_Run();

            tbxCodigo.Select();
            tbxCodigo.SelectAll();
        }

        private void MaqctCampos(clsMaqctInfo info)
        {
            id = info.id;

            tbxCodigo.Text = info.codigo;
            tbxNome.Text = info.nome;
            cbxAtivo.SelectedIndex = clsVisual.SelecionarIndex(info.ativo, 1, cbxAtivo);
        }

        private void MaqctFillInfo(clsMaqctInfo info)
        {
            info.id = id;

            info.codigo = tbxCodigo.Text;
            info.nome = tbxNome.Text;
            info.ativo = cbxAtivo.Text.Substring(0, 1);
        }

        private void MaqctPrecoFillInfo(clsMaqctprecoInfo info)
        {
            info.id = maqctpreco_id;
            info.idindice = maqctpreco_idindice;
            info.idmaqct = maqctpreco_idmaqct;

            info.data = clsParser.DateTimeParse(tbxMaqctPreco_Data.Text);
            info.valor = clsParser.DecimalParse(tbxMaqctPreco_Valor.Text);
        }

        private void MaqctPrecoCampos(clsMaqctprecoInfo info)
        {
            maqctpreco_id = info.id;
            maqctpreco_idindice = info.idindice;
            maqctpreco_idmaqct = info.idmaqct;

            tbxMaqctPreco_Data.Text = info.data.ToString("dd/MM/yyyy");
            tbxMaqctPreco_Valor.Text = info.valor.ToString("n5");
        }

        private Boolean MaqctPrecoNovo()
        {
            DataRow[] rows = dtMaqctPreco.Select("maqctpreco_posicao=" + maqctpreco_posicao);

            if (rows.Length > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void MaqctPrecoFillInfoToGrid(clsMaqctprecoInfo info)
        {
            DataRow row;
            DataRow[] rows = dtMaqctPreco.Select("maqctpreco_posicao=" + maqctpreco_posicao);

            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtMaqctPreco.NewRow();
            }

            row["id"] = info.id;
            row["idindice"] = info.idindice;
            row["idmaqct"] = id;

            row["data"] = info.data;
            row["valor"] = info.valor;

            if (rows.Length == 0)
            {
                row["maqctpreco_posicao"] = maqctpreco_posicao;
                dtMaqctPreco.Rows.Add(row);
            }
        }

        private void MaqctPrecoFillGridToInfo(clsMaqctprecoInfo info, Int32 posicao)
        {
            DataRow row = dtMaqctPreco.Select("maqctpreco_posicao=" + posicao)[0];

            info.id = clsParser.Int32Parse(row["id"].ToString());
            info.idindice = clsParser.Int32Parse(row["idindice"].ToString());
            info.idmaqct = clsParser.Int32Parse(row["idmaqct"].ToString());

            info.data = clsParser.DateTimeParse(row["data"].ToString());
            info.valor = clsParser.DecimalParse(row["valor"].ToString());
        }

        private void MaqctPrecoCarregar()
        {
            MaqctprecoInfo = new clsMaqctprecoInfo();
            MaqctprecoInfoOld = new clsMaqctprecoInfo();

            if (maqctpreco_posicao == 0)
            {
                MaqctprecoInfo.idindice = 0;
                MaqctprecoInfo.idmaqct = 0;
                MaqctprecoInfo.data = DateTime.Now;
                MaqctprecoInfo.valor = 0;

                maqctpreco_posicao = dtMaqctPreco.Rows.Count + 1;
            }
            else
            {
                MaqctPrecoFillGridToInfo(MaqctprecoInfo, maqctpreco_posicao);
            }

            MaqctPrecoCampos(MaqctprecoInfo);
            MaqctPrecoFillInfo(MaqctprecoInfoOld);

            maqctpreco_guiaatual = 1;

            tbxMaqctPreco_Data.Select();
            tbxMaqctPreco_Data.SelectAll();
        }

        private Boolean HouveMudancas()
        {
            MaqctInfo = new clsMaqctInfo();

            MaqctFillInfo(MaqctInfo);

            if (MaqctBLL.Equals(MaqctInfo, MaqctInfoOld) == false)
            {
                return true;
            }

            foreach (DataRow row in dtMaqctPreco.Rows)
            {
                if (row.RowState == DataRowState.Added ||
                    row.RowState == DataRowState.Deleted ||
                    row.RowState == DataRowState.Modified)
                {
                    return true;
                }
            }

            return false;
        }

        private void bwrMaqctPreco_Run()
        {
            dgvMaqctpreco.DataSource = null;

            bwrMaqctPreco = new BackgroundWorker();
            bwrMaqctPreco.DoWork += new DoWorkEventHandler(bwrMaqctPreco_DoWork);
            bwrMaqctPreco.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwrMaqctPreco_RunWorkerCompleted);
            bwrMaqctPreco.RunWorkerAsync();
        }

        void bwrMaqctPreco_DoWork(object sender, DoWorkEventArgs e)
        {
            dtMaqctPreco = new DataTable();

            dtMaqctPreco = MaqctprecoBLL.CarregaGrid(clsInfo.conexaosqldados, id);

            DataColumn dcPosicao = new DataColumn("maqctpreco_posicao", Type.GetType("System.Int32"));
            dtMaqctPreco.Columns.Add(dcPosicao);

            for (Int32 i = 0; i < dtMaqctPreco.Rows.Count; i++)
            {
                dtMaqctPreco.Rows[i]["maqctpreco_posicao"] = i + 1;
            }
            dtMaqctPreco.AcceptChanges();
        }

        void bwrMaqctPreco_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvMaqctpreco.DataSource = dtMaqctPreco;
            clsGridHelper.MontaGrid2(dgvMaqctpreco, dtMaqctPrecoColunas, true);
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);

            Control ctl = (Control)sender;
            if (ctl.Name == tbxMaqctPreco_Valor.Name)
            {
                ctl.Text = clsParser.DecimalParse(ctl.Text).ToString("n5");
            }
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Salvar() == DialogResult.Cancel)
                {
                    tbxCodigo.Select();
                    tbxCodigo.SelectAll();

                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Message: " + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            if (HouveMudancas() == true)
            {
                tsbSalvar.PerformClick();
            }
            this.Close();
        }

        private DialogResult Salvar() 
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja Salvar este Registro e Retornar?",
        Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                MaqctSalvar();
            }
            return drt;
        }

        private void MaqctSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                MaqctInfo = new clsMaqctInfo();

                MaqctFillInfo(MaqctInfo);

                if (id == 0)
                {
                    id = MaqctBLL.Incluir(MaqctInfo, clsInfo.conexaosqldados);
                }
                else
                {
                    MaqctBLL.Alterar(MaqctInfo, clsInfo.conexaosqldados);
                }


                foreach (DataRow row in dtMaqctPreco.Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                    {
                        MaqctprecoBLL.Excluir(clsParser.Int32Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else if (row.RowState == DataRowState.Detached ||
                                row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else
                    {
                        row["idmaqct"] = id;

                        MaqctprecoInfo = new clsMaqctprecoInfo();
                        MaqctPrecoFillGridToInfo(MaqctprecoInfo, clsParser.Int32Parse(row["maqctpreco_posicao"].ToString()));
                        if (MaqctprecoInfo.id == 0)
                        {
                            MaqctprecoInfo.id = MaqctprecoBLL.Incluir(MaqctprecoInfo, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            MaqctprecoBLL.Alterar(MaqctprecoInfo, clsInfo.conexaosqldados);
                        }
                    }
                }

                frmMaqCtVis.maqct_id = id;                                         
                tse.Complete();
            }
        }

        private void tsbMaqctPrecoVis_Incluir_Click(object sender, EventArgs e)
        {
            try
            {
                maqctpreco_posicao = 0;
                MaqctPrecoCarregar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbMaqctPrecoVis_Imprimir_Click(object sender, EventArgs e)
        {
            BR.Imprimir(clsInfo.caminhorelatorios, "Evolução de Preços", clsInfo.conexaosqldados);
        }

        private void tsbMaqctPrecoVis_Alterar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMaqctpreco.CurrentRow != null)
                {
                    maqctpreco_posicao = clsParser.Int32Parse(dgvMaqctpreco.CurrentRow.Cells["maqctpreco_posicao"].Value.ToString());
                    MaqctPrecoCarregar();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbMaqctPrecoVis_Excluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMaqctpreco.CurrentRow == null)
                {
                    return;
                }

                DialogResult drt;

                drt = MessageBox.Show("Deseja realmente Excluir o registro selecionado?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (drt == DialogResult.Yes)
                {
                    dtMaqctPreco.Select("maqctpreco_posicao = " + dgvMaqctpreco.CurrentRow.Cells["maqctpreco_posicao"].Value.ToString())[0].Delete();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbMaqctPreco_Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drt;
                drt = MessageBox.Show("Deseja Salvar e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    MaqctprecoInfo = new clsMaqctprecoInfo();
                    MaqctPrecoFillInfo(MaqctprecoInfo);

                    MaqctPrecoFillInfoToGrid(MaqctprecoInfo);
                }
                else if (drt == DialogResult.Cancel)
                {
                    return;
                }

                maqctpreco_guiaatual = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspMaqctPreco_Retornar_Click(object sender, EventArgs e)
        {
            try
            {
                MaqctprecoInfo = new clsMaqctprecoInfo();
                MaqctPrecoFillInfo(MaqctprecoInfo);

                if (MaqctprecoBLL.Equals(MaqctprecoInfo, MaqctprecoInfoOld) == false)
                {
                    tsbMaqctPreco_Salvar.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            maqctpreco_guiaatual = 0;
        }

        private void tclMaqctPreco_SelectedIndexChanged(object sender, EventArgs e)
        {
            tclMaqctPreco.SelectedIndex = maqctpreco_guiaatual;
        }

        private void tsbProximo_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveMudancas() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                if (rows != null)
                {
                    foreach (DataGridViewRow dgvr in rows)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index < rows.Count - 1)
                            {
                                id = Int32.Parse(rows[rows.GetNextRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                MaqctCarregar();
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
                MessageBox.Show("Não foi possível ir para o Próximo Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void tsbAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveMudancas() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                if (rows != null)
                {
                    foreach (DataGridViewRow dgvr in rows)
                    {
                        if (Int32.Parse(dgvr.Cells[0].Value.ToString()) == id)
                        {
                            if (dgvr.Index != 0)
                            {
                                id = Int32.Parse(rows[rows.GetPreviousRow(dgvr.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                MaqctCarregar();
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
                MessageBox.Show("Não foi possível ir para o Registro Anterior:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveMudancas() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if(rows !=null)
                {
                    id = Int32.Parse(rows[0].Cells[0].Value.ToString());
                    MaqctCarregar();
                }            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Primeiro Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveMudancas() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                    {

                        id = Int32.Parse(rows[rows.Count - 1].Cells[0].Value.ToString());
                        MaqctCarregar();
                    }
                }
        
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMaqctpreco_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsbMaqctPrecoVis_Alterar.PerformClick();
        }
    }
}
