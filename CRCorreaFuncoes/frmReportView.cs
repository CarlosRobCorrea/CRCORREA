using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Reflection;

namespace CRCorreaFuncoes
{
    public partial class frmReportView : Form
    {
        String dir;
        String displaytitulo;

        String[] displayfields;
        String[] nomeoriginal;
        String[] grupos;
        Int32[] sizecolunas;
        DataGridViewContentAlignment[] aligncolunas;

        DataTable dt;

        public frmReportView()
        {
            InitializeComponent();
        }

        public void Init(String _dir,
                        DataTable _dt,
                        String _displaytitulo,
                        String[] _grupos,
                        DataGridView _dgvw)
        {
            dir = _dir;
            dt = _dt.Copy();
            displaytitulo = _displaytitulo;
            nomeoriginal = new String[_dgvw.Columns.GetColumnCount(DataGridViewElementStates.Visible)];
            displayfields = new String[_dgvw.Columns.GetColumnCount(DataGridViewElementStates.Visible)];
            sizecolunas = new Int32[_dgvw.Columns.GetColumnCount(DataGridViewElementStates.Visible)];
            aligncolunas = new DataGridViewContentAlignment[_dgvw.Columns.GetColumnCount(DataGridViewElementStates.Visible)];
            grupos = _grupos;

            Int32 x = 0;
            Type t = _dgvw.Columns.GetType();
            FieldInfo field = t.GetField("itemsSorted", BindingFlags.Instance | BindingFlags.NonPublic);
            ArrayList itemsSorted = field.GetValue(_dgvw.Columns) as ArrayList;

            foreach (DataGridViewColumn coluna in itemsSorted)
            {
                if (coluna.Visible)
                {
                    nomeoriginal.SetValue(coluna.Name, x);
                    displayfields.SetValue(coluna.HeaderText, x);
                    sizecolunas.SetValue(coluna.Width, x);
                    aligncolunas.SetValue(coluna.DefaultCellStyle.Alignment, x);
                    x++;
                }
            }
        }

        private void frmReportView_Load(object sender, EventArgs e)
        {
            ReportObjects ros;
            ReportDocument rdt = new ReportDocument();

            Int32 sizepadrao = 120;
            Int32 sizelastcol = 60;
            Int32 sizemultiplica = 15;
            Int32 sizelimite = 10600;
            Int32 nColunasMax = displayfields.Length;

            if (nColunasMax > 14)
            {
                nColunasMax = 14;
            }

            Int32[] sizeporcentagem = new Int32[nColunasMax];

            for (Int32 X = 0; X < nomeoriginal.Length; X++)
            {
                dt.Columns[nomeoriginal[X]].ColumnName = "coluna" + X.ToString();
            }

            for (Int32 X = 0; X < grupos.Length; X++)
            {
                dt.Columns[grupos[X]].ColumnName = "grupo" + X.ToString();
            }
            // Calcula o tamanho total para ver se não irá ultrapassar o limite
            for (Int32 X = 0; X < nColunasMax; X++)
            {
                sizelastcol += sizecolunas[X] * sizemultiplica;
            }
            // Se ultrapassar, ajustar os valores para
            // manter todos com a mesma proporção só que menores
            if (sizelastcol > sizelimite)
            {
                Decimal sizecolunanovo;
                // Redimensiona coluna por coluna
                for (Int32 X = 0; X < nColunasMax; X++)
                {
                    sizecolunanovo = Decimal.Divide((sizecolunas[X] * 100) , sizelastcol);
                    sizecolunanovo = (sizecolunanovo * sizelimite) / 100;
                    sizecolunas.SetValue(Decimal.ToInt32(sizecolunanovo), X);
                }
            }
            sizelastcol = 60;
            for (Int32 X = 0; X < grupos.Length; X++)
            {
                sizelastcol += 500;
            }

            if (grupos != null && grupos.Length > 0)
            {
                MessageBox.Show(dir + "crtPadrao" + grupos.Length.ToString() + ".rpt");
                rdt.Load(dir + "crtPadrao" + grupos.Length.ToString() + ".rpt");
            }
            else
            {
                // MessageBox.Show(dir + "crtPadrao.rpt");
                try
                {
                rdt.Load(dir + "crtPadrao.rpt");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                    throw;
                }
            }

            // MessageBox.Show("Carregou");

            rdt.SummaryInfo.ReportTitle = displaytitulo;
            ros = rdt.ReportDefinition.ReportObjects;
            for (Int32 X = 0; X < nColunasMax; X++)
            {
                ((TextObject)ros["display" + X.ToString()]).Text = displayfields[X];
                ros["display" + X.ToString()].Left = sizelastcol;
                ros["display" + X.ToString()].Width = sizecolunas[X] * sizemultiplica;
                ros["field" + X.ToString()].Left = sizelastcol;
                ros["field" + X.ToString()].Width = sizecolunas[X] * sizemultiplica;
                if (aligncolunas[X] == DataGridViewContentAlignment.MiddleLeft)
                    ros["field" + X.ToString()].ObjectFormat.HorizontalAlignment = CrystalDecisions.Shared.Alignment.LeftAlign;
                else if (aligncolunas[X] == DataGridViewContentAlignment.MiddleRight)
                    ros["field" + X.ToString()].ObjectFormat.HorizontalAlignment = CrystalDecisions.Shared.Alignment.RightAlign;
                else if (aligncolunas[X] == DataGridViewContentAlignment.MiddleCenter)
                    ros["field" + X.ToString()].ObjectFormat.HorizontalAlignment = CrystalDecisions.Shared.Alignment.HorizontalCenterAlign;
                else
                    ros["field" + X.ToString()].ObjectFormat.HorizontalAlignment = CrystalDecisions.Shared.Alignment.LeftAlign;

                sizelastcol += ros["field" + X.ToString()].Width + sizepadrao;
            }
            rdt.SetDataSource(dt);
            crvrReport.ReportSource = rdt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
