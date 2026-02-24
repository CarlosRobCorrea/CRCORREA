using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using CrystalDecisions.Shared;
using System;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmZonas : Form
    {
        //Variaveis Globais
        
        
        ParameterFields pfields = new ParameterFields();

        // Frota
        clsZonasBLL clsZonasBLL;
        clsZonasInfo clsZonasInfo;
        clsZonasInfo clsZonasInfoOld;

        Int32 id;

        DataGridViewRowCollection rows;

        public frmZonas()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridViewRowCollection _rows)
        {
            id = _id;
            rows = _rows;
            clsZonasBLL = new clsZonasBLL();
        }

        private void frmZonas_Load(object sender, EventArgs e)
        {
            try
            {
                ZonasCarregar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Message: " + ex.Message + "/nStack Stace: " + ex.StackTrace, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ZonasCarregar()
        {
            clsZonasInfoOld = new clsZonasInfo();
            if (id == 0)
            {
                //preenchimento com dados padrões
                clsZonasInfo = new clsZonasInfo();
                clsZonasInfo.ativo = "S";
                clsZonasInfo.codigo = "";
                clsZonasInfo.nome = "";
                clsZonasInfo.iduf = clsInfo.zempresa_ufid;
                clsZonasInfo.qtde = 0;

            }
            else
            {
                //preenchimento com dados da base de dados
                clsZonasInfo = clsZonasBLL.Carregar(id, clsInfo.conexaosqldados);
            }
            //preenchimento dos campos da formulario
            ZonasCampos(clsZonasInfo);
            //preenchimento da info
            ZonasFillInfo(clsZonasInfoOld);
        }

        private void ZonasCampos(clsZonasInfo info)
        {
            id = info.id;
            if (info.ativo == "N")
            {
                rbnAtivoN.Checked = true;
            }
            else
            {
                rbnAtivoS.Checked = true;
            }
            tbxCodigo.Text = info.codigo;
            tbxNome.Text = info.nome;
        }

        private void ZonasFillInfo(clsZonasInfo info)
        {
            info.id = id;
            //Preenche ativo
            if (rbnAtivoS.Checked == true)
            {
                info.ativo = "S";
            }
            if (rbnAtivoN.Checked == true)
            {
                info.ativo = "N";
            }
            info.codigo = tbxCodigo.Text;
            info.iduf = clsInfo.zempresa_ufid;
            info.nome = tbxNome.Text;
            info.qtde = 0;
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
                MessageBox.Show("Message: " + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    ZonasCarregar();
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
                                ZonasCarregar();
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
                                ZonasCarregar();
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
                    ZonasCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            if (HouveModificacoes() == true)
            {
                tspSalvar.PerformClick();
            }
            else
            {
                this.Close();
            }
        }

        private Boolean HouveModificacoes()
        {
            clsZonasInfo = new clsZonasInfo();
            ZonasFillInfo(clsZonasInfo);
            if (clsZonasBLL.Equals(clsZonasInfo, clsZonasInfoOld) == false)
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
                ZonasSalvar();
            }
            return drt;
        }

        private void ZonasSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: Zonas
                clsZonasInfo = new clsZonasInfo();
                ZonasFillInfo(clsZonasInfo);

                if (id == 0)
                {
                    clsZonasInfo.id = clsZonasBLL.Incluir(clsZonasInfo, clsInfo.conexaosqldados);
                    frmZonasVis.linhagrid = clsZonasInfo.id;
                }
                else
                {
                    clsZonasBLL.Alterar(clsZonasInfo, clsInfo.conexaosqldados);
                }
                id = clsZonasInfo.id;
                tse.Complete();
            }
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }
    }
}

