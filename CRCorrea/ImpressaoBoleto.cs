using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;

namespace CRCorrea
{
    public partial class ImpressaoBoleto : Form
    {
        public WebBrowser webBrowser;
        public ImpressaoBoleto()
        {
            InitializeComponent();
        }

        private void visualizarImagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser.Size = pl_browser.Size;
            webBrowser.Dock = DockStyle.Fill;
            webBrowser.ScrollBarsEnabled = true;

            pl_browser.Controls.Add(webBrowser);

            FormVisualizarImagem form = new FormVisualizarImagem();
            form.init(GerarImagem());
            form.ShowDialog();
        }

        private string GerarImagem()
        {
            string address = webBrowser.Url.ToString();
            int width = 670;
            int height = 805;

            int webBrowserWidth = 670;
            int webBrowserHeight = 805;

            Bitmap bmp = 
                WebsiteThumbnailImageGenerator.GetWebSiteThumbnail(address, webBrowserWidth, webBrowserHeight, width, height);

            string file = Path.Combine(Environment.CurrentDirectory, "boleto.bmp");

            bmp.Save(file);

            return file;
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser.ShowPrintDialog();
        }
    }
}