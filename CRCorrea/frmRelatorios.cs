using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

using CRCorreaBarCod;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmRelatorios : Form
    {
        
        
        

        public frmRelatorios()
        {
            InitializeComponent();
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnRelNFCompra_Click(object sender, EventArgs e)
        {
            frmRelNFCompra frmRelNFCompra = new frmRelNFCompra();
            frmRelNFCompra.Init();

            clsFormHelper.AbrirForm(this.MdiParent, frmRelNFCompra, clsInfo.conexaosqldados);

        }

        private void btnRelNFVenda_Click(object sender, EventArgs e)
        {
            frmRelNFVenda frmRelNFVenda = new frmRelNFVenda();
            frmRelNFVenda.Init();
            clsFormHelper.AbrirForm(this.MdiParent, frmRelNFVenda, clsInfo.conexaosqldados);
        }

        private void btnRelPagar_Click(object sender, EventArgs e)
        {
            frmRelPagar frmRelPagar = new frmRelPagar();
            frmRelPagar.Init();

            clsFormHelper.AbrirForm(this.MdiParent, frmRelPagar, clsInfo.conexaosqldados);
        }

        private void btnRelReceber_Click(object sender, EventArgs e)
        {
            frmRelReceber frmRelReceber = new frmRelReceber();
            frmRelReceber.Init();

            clsFormHelper.AbrirForm(this.MdiParent, frmRelReceber, clsInfo.conexaosqldados);
        }

        private void frmRelatoriosFerpal_Load(object sender, EventArgs e)
        {

        }

        private void btnRelPecas_Click(object sender, EventArgs e)
        {
            frmRelPecas frmRelPecas = new frmRelPecas();
            frmRelPecas.Init();
            clsFormHelper.AbrirForm(this.MdiParent, frmRelPecas, clsInfo.conexaosqldados);
        }

        private void btnRelPedido_Click(object sender, EventArgs e)
        {
            frmRelPedidos frmRelPedidos = new frmRelPedidos();
            frmRelPedidos.Init();
            clsFormHelper.AbrirForm(this.MdiParent, frmRelPedidos, clsInfo.conexaosqldados);
        }

        private void btnRelOrdemServico_Click(object sender, EventArgs e)
        {
            //frmRelOrdemServicoFerpal frmRelOrdemServicoFerpal = new frmRelOrdemServicoFerpal();
            //frmRelOrdemServicoFerpal.Init();

            //FormHelper.AbrirForm(this.MdiParent, frmRelOrdemServicoFerpal, clsInfo.conexaosqldados);
        }

        private void btnRelEdificio_Click(object sender, EventArgs e)
        {
            //frmRelEdificioOrc frmRelEdificioOrc = new frmRelEdificioOrc();
            //frmRelEdificioOrc.Init();

            //FormHelper.AbrirForm(this.MdiParent, frmRelEdificioOrc, clsInfo.conexaosqldados);
        }

        private void btnRelOrcamento_Click(object sender, EventArgs e)
        {
            //frmRelOrcamento frmRelOrcamento = new frmRelOrcamento();
            //frmRelOrcamento.Init();
            //FormHelper.AbrirForm(this.MdiParent, frmRelOrcamento, clsInfo.conexaosqldados);
        }

        private void btnRelCliente_Click(object sender, EventArgs e)
        {
            frmRelCliente frmRelCliente = new frmRelCliente();
            frmRelCliente.Init();

            clsFormHelper.AbrirForm(this.MdiParent, frmRelCliente, clsInfo.conexaosqldados);

        }


    }
}
