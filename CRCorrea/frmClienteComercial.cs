using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRCorrea;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmClienteComercial : Form
    {
        DataTable dtFichaComercial;
        Int32 id;
        public frmClienteComercial()
        {
            InitializeComponent();
        }
        public void Init(Int32 _id)
        {
            id = _id;
        }

        private void frmClienteComercial_Load(object sender, EventArgs e)
        {
            tbxCognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id=" + id, "");
            tbxNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from cliente where id=" + id, "");

            tbxDatadeC.Text = clsParser.DateTimeParse("01/01/1900").ToString("dd/MM/yyy");
            tbxDataateC.Text = clsParser.DateTimeParse(DateTime.Now.AddYears(10).ToString()).ToString("dd/MM/yyyy");


            dtFichaComercial = clsFinanceiro.FichaComercial(0, id, tbxCognome.Text, clsParser.DateTimeParse(tbxDatadeC.Text), clsParser.DateTimeParse(tbxDataateC.Text));
            clsFinanceiro.GridFichaComercialMonta(dgvFichaComercial, dtFichaComercial, 0);

        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
