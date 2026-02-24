using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Net;

using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmFeriados : Form
    {
        Int32 id;

        clsFeriadosBLL FeriadosBLL;
        clsFeriadosInfo FeriadosInfo;
        clsFeriadosInfo FeriadosInfoOld;

        DataGridViewRowCollection rows;

        

        public frmFeriados()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridViewRowCollection _rows)
        {
            id = _id;
            rows = _rows;

            

            FeriadosBLL = new clsFeriadosBLL();
        }

        private void frmFeriados_Load(object sender, EventArgs e)
        {
            FeriadosCarregar();
        }

        private void FeriadosCarregar()
        {
            FeriadosInfoOld = new clsFeriadosInfo();

            if (id == 0)
            {
                FeriadosInfo = new clsFeriadosInfo();
                FeriadosInfo.data = DateTime.Now;
                FeriadosInfo.descricao = "";

                FeriadosCampos(FeriadosInfo);

                tspPrimeiro.Enabled = false;
                tspAnterior.Enabled = false;
                tspProximo.Enabled = false;
                tspUltimo.Enabled = false;
            }
            else
            {
                FeriadosInfo = FeriadosBLL.Carregar(id, clsInfo.conexaosqldados);
                FeriadosCampos(FeriadosInfo);
            }

            FeriadosFillInfo(FeriadosInfoOld);

            tbxData.Select();
            tbxData.SelectAll();
        }

        private void FeriadosCampos(clsFeriadosInfo info)
        {
            tbxData.Text = info.data.ToString("dd/MM/yyyy");
            tbxDescricao.Text = info.descricao;
        }

        private void FeriadosFillInfo(clsFeriadosInfo info)
        {
            info.id = id;
            info.data = clsParser.DateTimeParse(tbxData.Text);
            info.descricao = tbxDescricao.Text;
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

        private void FeriadosSalvar()
        {
            FeriadosFillInfo(FeriadosInfo);

            if (id == 0)
            {
                id = FeriadosBLL.Incluir(FeriadosInfo, clsInfo.conexaosqldados);
            }
            else
            {
                FeriadosBLL.Alterar(FeriadosInfo, clsInfo.conexaosqldados);
            }
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar ?", 
                                                "Aplisoft",
                                                MessageBoxButtons.YesNoCancel, 
                                                MessageBoxIcon.Question,
                                                MessageBoxDefaultButton.Button1);
                if (resultado == DialogResult.Yes)
                {
                    FeriadosSalvar();
                }
                else if (resultado == DialogResult.Cancel)
                {
                    tbxData.Select();
                    tbxData.SelectAll();

                    return;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                tbxDescricao.Select();
                tbxDescricao.SelectAll();
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                FeriadosInfo = new clsFeriadosInfo();
                FeriadosFillInfo(FeriadosInfo);

                if (FeriadosBLL.Equals(FeriadosInfo, FeriadosInfoOld) == false)
                {
                    tspSalvar.PerformClick();
                    return;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                tbxDescricao.Select();
                tbxDescricao.SelectAll();
            }
        }

        public Boolean MovimentaFeriados()
        {
            FeriadosInfo = new clsFeriadosInfo();
            FeriadosFillInfo(FeriadosInfo);

            if (FeriadosBLL.Equals(FeriadosInfoOld, FeriadosInfo) == false)
            {
                DialogResult drt;
                drt = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (drt == DialogResult.Yes)
                {
                    FeriadosSalvar();
                }
                else if (drt == DialogResult.Cancel)
                {
                    tbxData.Select();
                    tbxData.SelectAll();

                    return false;
                }
            }

            return true;
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (MovimentaFeriados())
                {
                    id = Int32.Parse(rows[0].Cells[0].Value.ToString());
                    FeriadosCarregar();
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
                if (MovimentaFeriados())
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index != 0)
                            {
                                id = Int32.Parse(rows[rows.GetPreviousRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    FeriadosCarregar();
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
                if (MovimentaFeriados())
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index < rows.Count - 1)
                            {
                                id = Int32.Parse(rows[rows.GetNextRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    FeriadosCarregar();
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
                if (MovimentaFeriados())
                {
                    id = Int32.Parse(rows[rows.Count - 1].Cells[0].Value.ToString());
                    FeriadosCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void frmFeriados_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsInfo.zlastid = id;
        }
    }
}

