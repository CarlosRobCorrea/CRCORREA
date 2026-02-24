using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

//using CrystalDecisions.Shared;


namespace CRCorreaFuncoes
{
    public partial class frmReportView3 : Form
    {
        private String dir;
        //private DataTable dt;

        private String sintaxecrystal;

        public frmReportView3()
        {
            InitializeComponent();
        }

        public void Init(String _dir,
                        String _sintaxecrystal)
        {
            dir = _dir;
            sintaxecrystal = _sintaxecrystal;
        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmReportView2_Load(object sender, EventArgs e)
        {
         /*   ReportDocument rdt = new ReportDocument();
            rdt.Load(dir);
            
     
            
          //  rdt.VerifyDatabase();
           rdt.SetDatabaseLogon("sa", "Apli5800");
           rdt.RecordSelectionFormula = sintaxecrystal;
           rdt.Refresh();
         //  rdt.SetDatabaseLogon("sa", "Apli5800","SERVIDOR", "PANDADOS001");
            crvrReport.ReportSource = rdt;
           // rdt.Load(dir.ToString());
            crvrReport.Refresh();

           */ 
            CrystalDecisions.CrystalReports.Engine.ReportDocument doc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
           
            doc.Load(dir.ToString());
            doc.SetDatabaseLogon("sa", "Apli5800");
            crvrReport.SelectionFormula = sintaxecrystal.ToString();
            
            crvrReport.ReportSource = doc;
           




        }




    }
}
