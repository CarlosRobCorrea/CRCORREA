using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CRCorreaFuncoes;

namespace CRCorrea
{
    public partial class frmReceberBaixaCheque : Form
    {
        
        public frmReceberBaixaCheque()
        {
            InitializeComponent();
        }

        public void Init()
        {
            
        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void frmReceberBaixaCheque_Shown(object sender, EventArgs e)
        {
            
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dataGridView2);
        }

        private void frmReceberBaixaCheque_Load(object sender, EventArgs e)
        {

        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {

            this.Close();

        }
                
    }
}
