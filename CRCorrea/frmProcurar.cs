using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CRCorreaFuncoes;

namespace CRCorrea
{
    public partial class frmProcurar : Form
    {
        
        DataGridView[] dgvs;
        ArrayList colunas;

        public frmProcurar()
        {
            InitializeComponent();
        }

        public void Init(DataGridView[] _dgvs,
                         Boolean pesquisar)
        {
            dgvs = _dgvs;
            for (Int32 x = 0; x < dgvs.Length; x++)
            {
                cbxGrid.Items.Add(dgvs[x].AccessibleDescription.ToString());
            }

            colunas = new ArrayList();
            for (Int32 x = 0; x < dgvs.Length; x++ )
            {
                for (Int32 y = 0; y < dgvs[x].ColumnCount; y++)
                {
                    if (dgvs[x].Columns[y].Visible == true)
                    {
                        colunas.Add(dgvs[x].Name + ":" + dgvs[x].Columns[y].Name);
                    }
                }
            }

            
        }

        private void frmProcurar_Load(object sender, EventArgs e)
        {   
            cbxGrid.SelectedIndex = 0;
            if (cbxGrid.Items.Count == 1)
            {
                cbxGrid.Enabled = false;
                foreach (DataGridViewColumn dgvc in dgvs[0].Columns)
                {
                    if (dgvc.Visible == true)
                        cbxColuna.Items.Add(dgvc.HeaderText);
                }
                cbxColuna.SelectedIndex = 0;
                cbxColuna.Select();
            }
            else
            {
                cbxGrid.Select();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDados;
                dtDados = (DataTable)dgvs[cbxGrid.SelectedIndex].DataSource;
                dtDados.DefaultView.RowFilter = "";
            }
            catch
            {

            }

            this.Close();
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DataTable dtDados;
            dtDados = (DataTable)dgvs[cbxGrid.SelectedIndex].DataSource;

            String coluna_nome = "";

            for (Int32 x = 0; x < dgvs[cbxGrid.SelectedIndex].Columns.Count; x++)
            {
                if (dgvs[cbxGrid.SelectedIndex].Columns[x].HeaderText == cbxColuna.Text)
                {
                    coluna_nome = dgvs[cbxGrid.SelectedIndex].Columns[x].Name;
                }
            }

            if (coluna_nome == "" || dtDados.Columns.Contains(coluna_nome) == false)
            {
                this.Close();
                return;
            }

            if (tbxInformar.Text != "")
            {
                dtDados.DefaultView.RowFilter = "convert(" + coluna_nome + ", 'System.String') LIKE '%" + tbxInformar.Text + "%'";
            }

            this.Close();
        }
    }
}
