using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CRCorreaFuncoes
{ 
    public class clsBasicReport
    {
        private String conexao;

        private Rectangle DragDropRectangle;
        private Int32 DragDropSourceIndex;
        private Int32 DragDropCurrentIndex;
        private Int32 DragDropTargetIndex;

        private Form frm;
        private DataGridView dgvw;
        private Panel plGrupos;
        private Label lblGrupos;
        private Panel plColunas;
        private Label lblColunas;
        private GroupBox gbxRelatorio;
        private Label lblAba;
        private ToolTip ttp;

        private Control controlepai;

        private enum clickwhere { Panel, DataGridView };
        private clickwhere click;

        /// <summary>
        /// Classe de Funções e controle de Relatórios emitidos a partir
        /// de uma tela de clsVisualização.
        /// </summary>
        /// <param name="_frm">Formulário de clsVisualização que será utilizado.</param>
        /// <param name="_dgvw">DataGridView em que a classe irá de basear.</param>
        public clsBasicReport(Form _frm, DataGridView _dgvw, String _conexao)
        {
            conexao = _conexao;
            frm = _frm;
            dgvw = _dgvw;

            gbxRelatorio = new GroupBox();
            
            lblAba = new Label();
            plGrupos = new Panel();
            lblGrupos = new Label();
            plColunas = new Panel();
            lblColunas = new Label();

            CriarControles();

            dgvw.MouseDown += new MouseEventHandler(GridMouseDown);
            dgvw.MouseMove += new MouseEventHandler(GridMouseMove);
            dgvw.DragOver += new DragEventHandler(GridDragOver);
            dgvw.DragDrop += new DragEventHandler(GridDragDrop);
            dgvw.CellPainting += new DataGridViewCellPaintingEventHandler(GridCellPainting);
            lblAba.Click += new EventHandler(LabelClick);
        }

        /// <summary>
        /// Classe de Funções e controle de Relatórios emitidos a partir
        /// de uma tela de clsVisualização.
        /// </summary>
        /// <param name="_frm">Formulário de clsVisualização que será utilizado.</param>
        /// <param name="_dgvw">DataGridView em que a classe irá de basear.</param>
        /// <param name="_ttp">ToolTip utilizado para a Etiqueta dos controles criados por esta classe.</param>
        public clsBasicReport(Form _frm, DataGridView _dgvw, ToolTip _ttp)
        {
            frm = _frm;
            dgvw = _dgvw;
            ttp = _ttp;

            gbxRelatorio = new GroupBox();
            lblAba = new Label();
            plGrupos = new Panel();
            lblGrupos = new Label();
            plColunas = new Panel();
            lblColunas = new Label();

            CriarControles();

            dgvw.MouseDown += new MouseEventHandler(GridMouseDown);
            dgvw.MouseMove += new MouseEventHandler(GridMouseMove);
            dgvw.DragOver += new DragEventHandler(GridDragOver);
            dgvw.DragDrop += new DragEventHandler(GridDragDrop);
            dgvw.CellPainting += new DataGridViewCellPaintingEventHandler(GridCellPainting);
            lblAba.Click += new EventHandler(LabelClick);

            ttp.SetToolTip(gbxRelatorio, "Grupo Controle Relatório");
            ttp.SetToolTip(plGrupos, "Grupos do Cotrole Relatório");
            ttp.SetToolTip(plColunas, "Colunas do Controle Relatório");
        }

        public clsBasicReport(Form _frm, DataGridView _dgvw, ToolTip _ttp, Control _controle)
        {
            frm = _frm;
            dgvw = _dgvw;
            ttp = _ttp;

            gbxRelatorio = new GroupBox();
            lblAba = new Label();
            plGrupos = new Panel();
            lblGrupos = new Label();
            plColunas = new Panel();
            lblColunas = new Label();

            CriarControles(_controle);

            dgvw.MouseDown += new MouseEventHandler(GridMouseDown);
            dgvw.MouseMove += new MouseEventHandler(GridMouseMove);
            dgvw.DragOver += new DragEventHandler(GridDragOver);
            dgvw.DragDrop += new DragEventHandler(GridDragDrop);
            dgvw.CellPainting += new DataGridViewCellPaintingEventHandler(GridCellPainting);
            lblAba.Click += new EventHandler(LabelClick);

            ttp.SetToolTip(gbxRelatorio, "Grupo Controle Relatório");
            ttp.SetToolTip(plGrupos, "Grupos do Cotrole Relatório");
            ttp.SetToolTip(plColunas, "Colunas do Controle Relatório");
        }

        public String NomeGrid()
        {
            return dgvw.Name;
        }

        /// <summary>
        /// Cria os controles na tela que manipulam e geram o Relatório Padrão.
        /// </summary>
        private void CriarControles()
        {
            gbxRelatorio.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gbxRelatorio.Name = "gbxRelatorio";
            gbxRelatorio.Size = new System.Drawing.Size(85, 18);
            gbxRelatorio.Text = "    Relatório";


            lblAba.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblAba.Name = "lblAba";
            lblAba.Text = "+";
            lblAba.AutoSize = true;

            plGrupos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            plGrupos.Name = "plGrupos";
            plGrupos.Size = new System.Drawing.Size(490, 55);
            plGrupos.AllowDrop = true;
            plGrupos.Visible = false;
            lblGrupos.AutoSize = true;
            lblGrupos.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblGrupos.Location = new System.Drawing.Point(226, 5);
            lblGrupos.Name = "lblGrupos";
            lblGrupos.Size = new System.Drawing.Size(484, 55);
            lblGrupos.TabIndex = 0;
            lblGrupos.Text = "Grupos";
            plGrupos.Controls.Add(lblGrupos);
            plGrupos.DragOver += new DragEventHandler(PanelDragOver);
            plGrupos.DragDrop += new DragEventHandler(PanelDragDrop);
            plGrupos.ControlRemoved += new ControlEventHandler(PanelControlRemoved);

            plColunas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            plColunas.Name = "plColunas";
            plColunas.Size = new System.Drawing.Size(489, 55);
            plColunas.AllowDrop = true;
            plColunas.Visible = false;
            lblColunas.AutoSize = true;
            lblColunas.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblColunas.Location = new System.Drawing.Point(218, 5);
            lblColunas.Name = "lblColunas";
            lblColunas.Size = new System.Drawing.Size(51, 13);
            lblColunas.TabIndex = 1;
            lblColunas.Text = "Colunas";
            plColunas.Controls.Add(lblColunas);
            plColunas.DragOver += new DragEventHandler(PanelDragOver);
            plColunas.DragDrop += new DragEventHandler(PanelDragDrop);
            plColunas.ControlRemoved += new ControlEventHandler(PanelControlRemoved);

            frm.Controls.Add(gbxRelatorio);
            gbxRelatorio.Controls.Add(lblAba);
            gbxRelatorio.Controls.Add(plGrupos);
            gbxRelatorio.Controls.Add(plColunas);

            gbxRelatorio.Location = new Point(12, dgvw.Location.Y);
            lblAba.Location = new Point(6, 0);
            plGrupos.Location = new Point(6, 20);
            plColunas.Location = new Point(502, 20);

            dgvw.Height -= 23;
            dgvw.Location = new Point(dgvw.Location.X, dgvw.Location.Y + 23);
        }

        private void CriarControles(Control controle)
        {
            controlepai = controle;

            gbxRelatorio.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gbxRelatorio.Name = "gbxRelatorio";
            gbxRelatorio.Size = new System.Drawing.Size(85, 18);
            gbxRelatorio.Text = "    Relatório";

            lblAba.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblAba.Name = "lblAba";
            lblAba.Text = "+";
            lblAba.AutoSize = true;

            plGrupos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            plGrupos.Name = "plGrupos";
            plGrupos.Size = new System.Drawing.Size((controle.Width/2) - 20, 55);
            plGrupos.AllowDrop = true;
            plGrupos.Visible = false;
            lblGrupos.AutoSize = true;
            lblGrupos.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblGrupos.Location = new System.Drawing.Point(226, 5);
            lblGrupos.Name = "lblGrupos";
            lblGrupos.Size = new System.Drawing.Size(484, 55);
            lblGrupos.TabIndex = 0;
            lblGrupos.Text = "Grupos";
            plGrupos.Controls.Add(lblGrupos);
            plGrupos.DragOver += new DragEventHandler(PanelDragOver);
            plGrupos.DragDrop += new DragEventHandler(PanelDragDrop);
            plGrupos.ControlRemoved += new ControlEventHandler(PanelControlRemoved);

            plColunas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            plColunas.Name = "plColunas";
            plColunas.Size = new System.Drawing.Size((controle.Width / 2) - 20, 55);
            plColunas.AllowDrop = true;
            plColunas.Visible = false;
            lblColunas.AutoSize = true;
            lblColunas.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblColunas.Location = new System.Drawing.Point(218, 5);
            lblColunas.Name = "lblColunas";
            lblColunas.Size = new System.Drawing.Size(51, 13);
            lblColunas.TabIndex = 1;
            lblColunas.Text = "Colunas";
            plColunas.Controls.Add(lblColunas);
            plColunas.DragOver += new DragEventHandler(PanelDragOver);
            plColunas.DragDrop += new DragEventHandler(PanelDragDrop);
            plColunas.ControlRemoved += new ControlEventHandler(PanelControlRemoved);

            if (controle is GroupBox)
            {
                controle.Controls.Add(gbxRelatorio);
            }
            else if (controle is Panel)
            {
                controle.Controls.Add(gbxRelatorio);
            }
            else if (controle is TabPage)
            {
                controle.Controls.Add(gbxRelatorio);
            }
            else
            {
                frm.Controls.Add(gbxRelatorio);
            }
            gbxRelatorio.Controls.Add(lblAba);
            gbxRelatorio.Controls.Add(plGrupos);
            gbxRelatorio.Controls.Add(plColunas);

            gbxRelatorio.Location = new Point(dgvw.Location.X, dgvw.Location.Y);
            lblAba.Location = new Point(6, 0);
            plGrupos.Location = new Point(6, 20);
            plColunas.Location = new Point(plGrupos.Width + 9, 20);

            dgvw.Height -= 23;
            dgvw.Location = new Point(dgvw.Location.X, dgvw.Location.Y + 23);
        }

        private void LabelClick(object sender, EventArgs e)
        {
            if (gbxRelatorio.Size == new Size(85, 18))
            {
                while (gbxRelatorio.Width < 996)
                {
                    if (gbxRelatorio.Height < 82)
                    {
                        gbxRelatorio.Height += 4;
                        dgvw.Height -= 4;
                        dgvw.Top += 4;
                    }
                    gbxRelatorio.Width += 16;
                    frm.Refresh();
                }
                plColunas.Visible = true;
                plGrupos.Visible = true;
                lblAba.Text = " -";
            }
            else if (lblAba.Text == " -")
            {
                plColunas.Visible = false;
                plGrupos.Visible = false;
                while (gbxRelatorio.Width > 85)
                {
                    if (gbxRelatorio.Height > 18)
                    {
                        gbxRelatorio.Height -= 4;
                        dgvw.Height += 4;
                        dgvw.Top -= 4;
                    }
                    gbxRelatorio.Width -= 16;
                    frm.Refresh();
                }
                lblAba.Text = "+";
                gbxRelatorio.Size = new Size(85, 18);
            }
            else
            {
                while (gbxRelatorio.Width < controlepai.Width)
                {
                    if (gbxRelatorio.Height < 82)
                    {
                        gbxRelatorio.Height += 4;
                        dgvw.Height -= 4;
                        dgvw.Top += 4;
                    }
                    gbxRelatorio.Width += 16;
                    frm.Refresh();
                }

                plColunas.Visible = true;
                plGrupos.Visible = true;
                lblAba.Text = " -";
            }
        }

        private void GridMouseDown(object sender, MouseEventArgs e)
        {
            DragDropRectangle = Rectangle.Empty;
            if (((DataGridView)sender).AllowDrop)
            {
                if (Cursor.Current != Cursors.SizeWE)
                {
                    if (((DataGridView)sender).HitTest(e.X, e.Y).ColumnIndex > -1)
                    {
                        click = clickwhere.DataGridView;
                        DragDropSourceIndex = ((DataGridView)sender).HitTest(e.X, e.Y).ColumnIndex;
                        Size DragSize = SystemInformation.DragSize;
                        DragDropRectangle = new Rectangle(new Point(e.X - (DragSize.Width / 2), e.Y - (DragSize.Height / 2)), DragSize);
                    }
                }
            }
        }

        private void GridMouseMove(object sender, MouseEventArgs e)
        {
            if (((DataGridView)sender).AllowDrop)
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    if (DragDropRectangle != Rectangle.Empty && !DragDropRectangle.Contains(e.X, e.Y))
                    {
                        DragDropEffects DropEffect = frm.DoDragDrop(((DataGridView)sender).Columns[DragDropSourceIndex], DragDropEffects.Move);
                    }
                }
            }
        }

        private void GridDragOver(object sender, DragEventArgs e)
        {
            if (((DataGridView)sender).AllowDrop)
            {
                e.Effect = DragDropEffects.Move;
                int CurCol = ((DataGridView)sender).HitTest(((DataGridView)sender).PointToClient(new Point(e.X, e.Y)).X, ((DataGridView)sender).PointToClient(new Point(e.X, e.Y)).Y).ColumnIndex;
                if (DragDropCurrentIndex != CurCol)
                {
                    DragDropCurrentIndex = CurCol;
                    ((DataGridView)sender).Invalidate();
                }
            }
        }

        private void GridDragDrop(object sender, DragEventArgs e)
        {
            if (((DataGridView)sender).AllowDrop)
            {
                if (e.Effect == DragDropEffects.Move)
                {
                    if (e.Data.GetDataPresent(typeof(Int32)))
                    {
                        if (((DataGridView)sender).Columns[DragDropSourceIndex].Visible == false)
                            ((DataGridView)sender).Columns[DragDropSourceIndex].Visible = true;

                        foreach (Control cl in plGrupos.Controls)
                        {
                            if (cl is TextBox && cl.TabIndex == DragDropSourceIndex)
                            {
                                plGrupos.Controls.Remove(cl);
                            }
                        }

                        foreach (Control cl in plColunas.Controls)
                        {
                            if (cl is TextBox && cl.TabIndex == DragDropSourceIndex)
                            {
                                plColunas.Controls.Remove(cl);
                            }
                        }
                    }
                    else
                    {
                        Point ClientPoint = ((DataGridView)sender).PointToClient(new Point(e.X, e.Y));
                        DragDropTargetIndex = ((DataGridView)sender).HitTest(ClientPoint.X, ClientPoint.Y).ColumnIndex;
                        if (DragDropTargetIndex != -1)
                        {
                            DragDropCurrentIndex = -1;
                            if (((DataGridView)sender).Columns[DragDropSourceIndex].Visible == false)
                                ((DataGridView)sender).Columns[DragDropSourceIndex].Visible = true;
                            ((DataGridView)sender).Columns[DragDropSourceIndex].DisplayIndex = ((DataGridView)sender).Columns[DragDropTargetIndex].DisplayIndex;
                        }
                    }
                }
            }
        }

        private void GridCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (DragDropCurrentIndex != -1)
            {
                if (e.ColumnIndex == DragDropCurrentIndex && click == clickwhere.DataGridView)
                {
                    Pen p = new Pen(Color.MediumBlue, 1);
                    e.Graphics.DrawLine(p, e.CellBounds.Left - 1, e.CellBounds.Top, e.CellBounds.Left - 1, e.CellBounds.Bottom);
                }
            }
        }

        private void PanelDragOver(object sender, DragEventArgs e)
        {
            if (((Panel)sender).AllowDrop)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void PanelDragDrop(object sender, DragEventArgs e)
        {
            if (((Panel)sender).AllowDrop)
            {
                if (e.Effect == DragDropEffects.Move)
                {
                    if (DragDropSourceIndex != -1)
                    {
                        DragDropCurrentIndex = -1;

                        Int32 x = 0;
                        TextBox tbx = new TextBox();
                        tbx.Name = dgvw.Columns[DragDropSourceIndex].Name;
                        tbx.Text = dgvw.Columns[DragDropSourceIndex].HeaderText;
                        tbx.TabIndex = dgvw.Columns[DragDropSourceIndex].Index;
                        tbx.BackColor = Color.LemonChiffon;
                        tbx.ReadOnly = true;
                        tbx.AllowDrop = true;
                        tbx.Font = new Font("Tahoma", 8, FontStyle.Bold);
                        tbx.Width = 92;
                        tbx.BorderStyle = BorderStyle.FixedSingle;

                        foreach (Control cl in ((Panel)sender).Controls)
                        {
                            if (cl is TextBox && cl.Name == tbx.Name)
                            {
                                return;
                            }
                            else if (cl is TextBox)
                            {
                                x++;
                            }
                        }

                        if (x == 5)
                            return;
                        else
                            x = 0;

                        if (dgvw.Columns.GetColumnCount(DataGridViewElementStates.Visible) == 1)
                            return;

                        tbx.MouseDown += new MouseEventHandler(TextBoxMouseDown);
                        ((Panel)sender).Controls.Add(tbx);
                        dgvw.Columns[tbx.TabIndex].Visible = false;

                        Graphics g = ((Panel)sender).CreateGraphics();
                        Pen p = new Pen(Color.Black, 1);
                        g.Clear(frm.BackColor);
                        foreach (Control cl in ((Panel)sender).Controls)
                        {
                            if (cl is TextBox)
                            {
                                if (x != 0)
                                    g.DrawLine(p, x + 5, 32, x, 32);
                                cl.Location = new Point(x + 5, 23);
                                x += cl.Width + 5;
                            }
                        }
                    }
                }
            }
        }

        private void TextBoxMouseDown(object sender, MouseEventArgs e)
        {
            click = clickwhere.Panel;
            DragDropRectangle = Rectangle.Empty;
            DragDropSourceIndex = ((TextBox)sender).TabIndex;
            frm.DoDragDrop(DragDropSourceIndex, DragDropEffects.Move);
        }

        private void PanelControlRemoved(object sender, ControlEventArgs e)
        {
            Int32 x = 0;

            Graphics g = ((Panel)sender).CreateGraphics();
            Pen p = new Pen(Color.Black, 1);
            g.Clear(frm.BackColor);
            foreach (Control cl in ((Panel)sender).Controls)
            {
                if (cl is TextBox)
                {
                    if (x != 0)
                        g.DrawLine(p, x + 5, 32, x, 32);
                    cl.Location = new Point(x + 5, 23);
                    x += cl.Width + 5;
                }
            }
        }

        private DataTable ParseGridTable(DataGridView _dgvw)
        {
            DataTable dt = new DataTable();

            Int32 x = 0;
            Type t = _dgvw.Columns.GetType();
            FieldInfo field = t.GetField("itemsSorted", BindingFlags.Instance | BindingFlags.NonPublic);
            ArrayList itemsSorted = field.GetValue(_dgvw.Columns) as ArrayList;

            foreach (DataGridViewColumn coluna in itemsSorted)
            {
                if (coluna.Visible)
                {
                    dt.Columns.Add(coluna.Name);
                }
            }

            foreach (Control cl in plGrupos.Controls)
            {
                if (cl is TextBox)
                {
                    dt.Columns.Add(cl.Name);
                    x++;
                }
            }

            String[] row = new String[_dgvw.Columns.GetColumnCount(DataGridViewElementStates.Visible) + x];
            x = 0;

            foreach (DataGridViewRow linha in _dgvw.Rows)
            {
                if (linha.Visible)
                {
                    foreach (DataGridViewColumn coluna in itemsSorted)
                    {
                        if (coluna.Visible)
                        {
                            if (linha.Cells[coluna.Index].Value != null)
                                row.SetValue(linha.Cells[coluna.Index].Value.ToString(), x);
                            x++;
                        }
                    }

                    foreach (Control cl in plGrupos.Controls)
                    {
                        if (cl is TextBox)
                        {
                            row.SetValue(linha.Cells[cl.Name].Value.ToString(), x);
                            x++;
                        }
                    }

                    dt.Rows.Add(row.ToArray());
                    row = new String[x];
                    x = 0;
                }
            }

            return dt;
        }

        /// <summary>
        /// Gera o Relatório e o clsVisualiza.
        /// </summary>
        /// <param name="_dir">Diretório onde estão os Relatórios Padrões.</param>
        /// <param name="_displaytitulo">Barra de Título da tela de clsVisualização do Relatório.</param>
        public void Imprimir(String _dir, String _displaytitulo, String _conexao)
        {
            conexao = _conexao;
            //frmReportView frmReportView;
            //frmReportView frmReportView = new frmReportView;

            String[] grupos;
            Int32 x = 0;
             
            foreach (Control cl in plGrupos.Controls)
            {
                if (cl is TextBox)
                {
                    x++;
                }
            }

            grupos = new String[x];
            x = 0;

            foreach (Control cl in plGrupos.Controls)
            {
                if (cl is TextBox)
                {
                    grupos.SetValue(cl.Name, x);
                    x++;
                }
            }

            //frmReportView = new frmReportView();
            //frmReportView.Init(_dir,
            //                    ParseGridTable(dgvw),
            //                    _displaytitulo,
            //                    grupos,
            //                    dgvw);
            
            //clsFormHelper.AbrirForm(frm.MdiParent, frmReportView, conexao);
        }

        public void RecalculaGrid(ArrayList _oldorder)
        {
            foreach (Control cl in plGrupos.Controls)
            {
                if (cl is TextBox)
                {
                    dgvw.Columns[cl.Name].Visible = false;
                }
            }

            foreach (Control cl in plColunas.Controls)
            {
                if (cl is TextBox)
                {
                    dgvw.Columns[cl.Name].Visible = false;
                }
            }

            Int32 x = 0;
            foreach (DataGridViewColumn coluna in _oldorder)
            {
                dgvw.Columns[coluna.Index].DisplayIndex = x;
                x++;
            }

            x = 0;
            Graphics g = plGrupos.CreateGraphics();
            Pen p = new Pen(Color.Black, 1);
            g.Clear(frm.BackColor);
            foreach (Control cl in plGrupos.Controls)
            {
                if (cl is TextBox)
                {
                    if (x != 0)
                        g.DrawLine(p, x + 5, 32, x, 32);
                    cl.Location = new Point(x + 5, 23);
                    x += cl.Width + 5;
                }
            }

            g = plColunas.CreateGraphics();
            p = new Pen(Color.Black, 1);
            g.Clear(frm.BackColor);
            foreach (Control cl in plColunas.Controls)
            {
                if (cl is TextBox)
                {
                    if (x != 0)
                        g.DrawLine(p, x + 5, 32, x, 32);
                    cl.Location = new Point(x + 5, 23);
                    x += cl.Width + 5;
                }
            }
        }

        public ArrayList GetColunas()
        {
            Type t = dgvw.Columns.GetType();
            FieldInfo field = t.GetField("itemsSorted", BindingFlags.Instance | BindingFlags.NonPublic);
            ArrayList itemsSorted = field.GetValue(dgvw.Columns) as ArrayList;
           
                return itemsSorted;
           
           
        }

        public void LiberaGrid()
        {
            foreach (Control cl in plGrupos.Controls)
            {
                if (cl is TextBox)
                    dgvw.Columns[cl.Name].Visible = true;
            }

            foreach (Control cl in plColunas.Controls)
            {
                if (cl is TextBox)
                    dgvw.Columns[cl.Name].Visible = true;
            }
        }

        public void TravaGrid()
        {
            foreach (Control cl in plGrupos.Controls)
            {
                if (cl is TextBox)
                    dgvw.Columns[cl.Name].Visible = false;
            }

            foreach (Control cl in plColunas.Controls)
            {
                if (cl is TextBox)
                    dgvw.Columns[cl.Name].Visible = false;
            }
        }
    }
}
