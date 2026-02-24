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
    public partial class frmReceberBanco : Form
    {
        
        

        public frmReceberBanco()
        {
            InitializeComponent();
        }

        public void Init()
        {
            
            
        }

        private void frmReceberBanco_Load(object sender, EventArgs e)
        {
            /* HABILITAR QUANDO A PROPRIEDADE 'TAG' JÁ ESTIVER DEFINIDA
            if (FormHelper.ConfigForm(clsParser.Int32Parse(this.Tag.ToString()),
                                    this,
                                    this.Text,
                                    clsInfo.conexaosqldados,
                                    toolTip1) == false)
            {
                this.Close();
                this.Dispose();
            }

            if (FormHelper.AcessaForm(clsParser.Int32Parse(this.Tag.ToString()),
                                    this,
                                    this.Text,
                                    clsInfo.conexaosqldados,
                                    toolTip1) == false)
            {
                this.Close();
                this.Dispose();
            }

            
             */
        }

        private void frmReceberBanco_Shown(object sender, EventArgs e)
        {
            clsFormHelper.VerificarForm(this, toolTip1, clsInfo.conexaosqldados);
        }

        private void button27_Click(object sender, EventArgs e)
        {

        }

        private void tspTool_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
