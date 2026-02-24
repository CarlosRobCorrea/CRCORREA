using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace CRCorreaFuncoes
{
    public partial class frmReportView2 : Form
    {
        private String dir;
        private DataTable dt;

        private String texto01;
        private String texto02;
        private String texto03;
        private String texto04;
        private String texto05;

        public frmReportView2()
        {
            InitializeComponent();
        }

        public void Init(String _dir,
                        DataTable _dt,
                        String _texto01,
                        String _texto02,
                        String _texto03,
                        String _texto04,
                        String _texto05)
        {
            dir = _dir;
            dt = _dt;

            texto01 = _texto01;
            texto02 = _texto02;
            texto03 = _texto03;
            texto04 = _texto04;
            texto05 = _texto05;
        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmReportView2_Load(object sender, EventArgs e)
        {
            if (texto02 == "")
            {
                texto02 = texto03;
                texto03 = texto04;
                texto04 = texto05;
                texto05 = "";
            }

            if (texto03 == "")
            {
                texto03 = texto04;
                texto04 = texto05;
                texto05 = "";
            }

            if (texto04 == "")
            {
                texto04 = texto05;
                texto05 = "";
            }

            ReportDocument rdt = new ReportDocument();
            rdt.Load(dir);
            ((TextObject)rdt.ReportDefinition.ReportObjects["TEXTO01"]).Text = texto01;
            ((TextObject)rdt.ReportDefinition.ReportObjects["TEXTO02"]).Text = texto02;
            ((TextObject)rdt.ReportDefinition.ReportObjects["TEXTO03"]).Text = texto03;
            ((TextObject)rdt.ReportDefinition.ReportObjects["TEXTO04"]).Text = texto04;
            ((TextObject)rdt.ReportDefinition.ReportObjects["TEXTO05"]).Text = texto05;
            rdt.SetDataSource(dt);
            crvrReport.ReportSource = rdt;
        }
    }
}
