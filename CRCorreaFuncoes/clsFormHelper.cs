
using CRCorreaInfo;
using CRCorreaFuncoes.Properties;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

using System.Reflection;
using System.Runtime.Remoting;
using System.Threading;
using System.Windows.Forms;


namespace CRCorreaFuncoes
{
    public static class clsFormHelper
    {
        static Form frmEdit;
        static DataRow[] dtObjects;
        static ContextMenuStrip cmsControl;

        // Para quase todos os controles da tela
        static Control actual;

        // Para os controles dentro do ToolStrip
        static ToolStripItem actualItem;

        // Cores de controle
        static Color colorAtivoVisivel = Color.PaleGreen;
        static Color colorVisivelDesativado = Color.Yellow;
        static Color colorNone = Color.Red;

        public static void FormLoad<T>() where T : Form
        {
            T t = Activator.CreateInstance<T>();

            RemoveEventsAllControls(t);

            t.FormBorderStyle = FormBorderStyle.FixedDialog;
            t.StartPosition = FormStartPosition.CenterScreen;
            t.MaximizeBox = false;
            t.MinimizeBox = false;

            t.Show();
        }

        public static void FormLoad(string form, string assemblyName)
        {
            ObjectHandle inst;

            if (string.IsNullOrEmpty(form))
                throw new ArgumentNullException("form");
            else if (string.IsNullOrEmpty(assemblyName))
                throw new ArgumentNullException("assemblyName");

            inst = Activator.CreateInstance(assemblyName, form);
            frmEdit = (Form)inst.Unwrap();

            RemoveEventsAllControls(frmEdit);

            frmEdit.FormBorderStyle = FormBorderStyle.FixedDialog;
            frmEdit.StartPosition = FormStartPosition.CenterScreen;
            frmEdit.MaximizeBox = false;
            frmEdit.MinimizeBox = false;

            frmEdit.ShowDialog();
        }

        public static void FormLoadWithData(string form,
                                            string assemblyName,
                                            int idForm,
                                            ref DataTable dtUsuarioFormsObjeto)
        {
            ObjectHandle inst;

            dtObjects = dtUsuarioFormsObjeto.Select("IDFORMS=" + idForm);

            if (string.IsNullOrEmpty(form))
                throw new ArgumentNullException("form");
            else if (string.IsNullOrEmpty(assemblyName))
                throw new ArgumentNullException("assemblyName");
            else if (dtUsuarioFormsObjeto == null)
                throw new ArgumentNullException("dtUsuarioFormsObjeto");

            // Prepara o menu de contexto para usabilidade
            cmsControl = new ContextMenuStrip();
            cmsControl.Opening += new CancelEventHandler(cmsControl_Opening);
            cmsControl.Items.Add("Visível e Ativo", null, cmsControl_Mostrar_Click);
            cmsControl.Items.Add("Visível e Desativado", null, cmsControl_MostrarDesativado_Click);
            cmsControl.Items.Add("Invisível e Desativado", null, cmsControl_NaoMostrar_Click);

            inst = Activator.CreateInstance(assemblyName, form);
            frmEdit = (Form)inst.Unwrap();

            RemoveEventsAllControls(frmEdit);

            frmEdit.FormBorderStyle = FormBorderStyle.FixedDialog;
            frmEdit.StartPosition = FormStartPosition.CenterScreen;
            frmEdit.MaximizeBox = false;
            frmEdit.MinimizeBox = false;
            frmEdit.Load += new EventHandler(formInstance_Load);

            ApplyColor(frmEdit);

            frmEdit.ShowDialog();
        }

        static void cmsControl_Opening(object sender, CancelEventArgs e)
        {
            Control ctl = ((ContextMenuStrip)sender).SourceControl;

            actual = ctl;

            if (ctl is ToolStrip)
            {
                foreach (ToolStripItem tsi in ((ToolStrip)ctl).Items)
                {
                    if (tsi.Selected)
                    {
                        actualItem = tsi;
                        actual = null;
                        break;
                    }
                }
            }

            if (actual != null)
            {
                cmsControl.Items[0].Visible = (actual.BackColor != colorAtivoVisivel);
                cmsControl.Items[1].Visible = (actual.BackColor != colorVisivelDesativado);
                cmsControl.Items[2].Visible = (actual.BackColor != colorNone);
            }
            else
            {
                cmsControl.Items[0].Visible = (actualItem.BackColor != colorAtivoVisivel);
                cmsControl.Items[1].Visible = (actualItem.BackColor != colorVisivelDesativado);
                cmsControl.Items[2].Visible = (actualItem.BackColor != colorNone);
            }
        }

        static void cmsControl_Mostrar_Click(object sender, EventArgs e)
        {
            if (actual != null)
            {
                actual.BackColor = colorAtivoVisivel;
            }
            else
            {
                actualItem.BackColor = colorAtivoVisivel;
            }
        }

        static void cmsControl_MostrarDesativado_Click(object sender, EventArgs e)
        {
            if (actual != null)
            {
                actual.BackColor = colorVisivelDesativado;
            }
            else
            {
                actualItem.BackColor = colorVisivelDesativado;
            }
        }

        static void cmsControl_NaoMostrar_Click(object sender, EventArgs e)
        {
            if (actual != null)
            {
                actual.BackColor = colorNone;
            }
            else
            {
                actualItem.BackColor = colorNone;
            }
        }

        static void formInstance_Load(object sender, EventArgs e)
        {
            // Adiciona a funcionalidade de Salvar
            // alterações aplicadas no formulário.
            Panel plForm;
            Button btnSave;
            Button btnCancel;

            plForm = new Panel();
            plForm.BorderStyle = BorderStyle.FixedSingle;
            plForm.Size = new Size(73, 35);

            btnSave = new Button();
            btnSave.Image = Resources.Salvar;
            btnSave.Location = new Point(1, 1);
            btnSave.Size = new Size(34, 31);
            btnSave.TabIndex = 0;
            btnSave.Click += new EventHandler(btnSave_Click);

            btnCancel = new Button();
            btnCancel.Image = Resources.Cancelar;
            btnCancel.Location = new Point(36, 1);
            btnCancel.Size = new Size(34, 31);
            btnCancel.TabIndex = 1;
            btnCancel.Click += new EventHandler(btnCancel_Click);

            plForm.Controls.Add(btnSave);
            plForm.Controls.Add(btnCancel);

            frmEdit.Controls.Add(plForm);

            plForm.Location = new Point(frmEdit.Width - plForm.Width - 8, 0);
            plForm.BringToFront();
        }

        static void btnSave_Click(object sender, EventArgs e)
        {
            // Aplica as alterações aplicadas no formulário
            // no objeto definido FormLoadWithData. 
            // dtObjects
            Update(frmEdit);
            frmEdit.Close();
        }

        static void btnCancel_Click(object sender, EventArgs e)
        {
            frmEdit.Close();
        }

        private static void RemoveEventsAllControls(Control ctl)
        {
            if (!ctl.Visible)
            {
                if (!(ctl is Form))
                {
                    ctl.Visible = true;
                }
            }

            RemoveEvents(ctl);

            if (ctl.Controls != null)
            {
                foreach (Control ctlIntern in ctl.Controls)
                {
                    RemoveEventsAllControls(ctlIntern);
                }
            }

            if (ctl is TabControl)
            {
                System.Windows.Forms.TabControl.TabPageCollection tps = ((TabControl)ctl).TabPages;

                if (tps != null && tps.Count > 0)
                {
                    foreach (TabPage ctlIntern in tps)
                    {
                        RemoveEventsAllControls(ctlIntern);
                    }
                }
            }

            if (ctl is ToolStrip)
            {
                if (ctl is MenuStrip)
                {
                    foreach (ToolStripItem ctlIntern in ((MenuStrip)ctl).Items)
                    {
                        ctlIntern.Visible = true;

                        RemoveEventsItemMenu(ctlIntern);
                    }
                }
                else
                {
                    int x = 0;
                    ToolStripItem[] tsiItens = new ToolStripItem[((ToolStrip)ctl).Items.Count];

                    foreach (ToolStripItem ctlIntern in ((ToolStrip)ctl).Items)
                    {
                        if (ctlIntern is ToolStripButton)
                        {
                            ToolStripButton tsb = new ToolStripButton();

                            tsb.Name = ctlIntern.Name;
                            tsb.Image = ctlIntern.Image;
                            tsb.Text = ctlIntern.Text;
                            tsb.Font = ctlIntern.Font;
                            tsb.Alignment = ctlIntern.Alignment;
                            tsb.AutoSize = ctlIntern.AutoSize;
                            tsb.Size = ctlIntern.Size;
                            tsb.TextImageRelation = ctlIntern.TextImageRelation;
                            tsb.Margin = ctlIntern.Margin;

                            tsiItens[x] = tsb;
                        }
                        else if (ctlIntern is ToolStripSeparator)
                        {
                            ToolStripSeparator tss = new ToolStripSeparator();

                            tss.Name = ctlIntern.Name;
                            tss.Image = ctlIntern.Image;
                            tss.Text = ctlIntern.Text;
                            tss.Font = ctlIntern.Font;
                            tss.Alignment = ctlIntern.Alignment;
                            tss.AutoSize = ctlIntern.AutoSize;
                            tss.Size = ctlIntern.Size;
                            tss.TextImageRelation = ctlIntern.TextImageRelation;
                            tss.Margin = ctlIntern.Margin;

                            tsiItens[x] = tss;
                        }
                        else if (ctlIntern is ToolStripLabel)
                        {
                            ToolStripLabel tsl = new ToolStripLabel();

                            tsl.Name = ctlIntern.Name;
                            tsl.Image = ctlIntern.Image;
                            tsl.Text = ctlIntern.Text;
                            tsl.Font = ctlIntern.Font;
                            tsl.Alignment = ctlIntern.Alignment;
                            tsl.AutoSize = ctlIntern.AutoSize;
                            tsl.Size = ctlIntern.Size;
                            tsl.TextImageRelation = ctlIntern.TextImageRelation;
                            tsl.Margin = ctlIntern.Margin;

                            tsiItens[x] = tsl;
                        }
                        else if (ctlIntern is ToolStripTextBox)
                        {
                            ToolStripTextBox tst = new ToolStripTextBox();

                            tst.Name = ctlIntern.Name;
                            tst.Image = ctlIntern.Image;
                            tst.Text = ctlIntern.Text;
                            tst.Font = ctlIntern.Font;
                            tst.Alignment = ctlIntern.Alignment;
                            tst.AutoSize = ctlIntern.AutoSize;
                            tst.Size = ctlIntern.Size;
                            tst.TextImageRelation = ctlIntern.TextImageRelation;
                            tst.Margin = ctlIntern.Margin;
                            tst.BorderStyle = ((ToolStripTextBox)ctlIntern).BorderStyle;
                            tst.CharacterCasing = ((ToolStripTextBox)ctlIntern).CharacterCasing;

                            tsiItens[x] = tst;
                        }
                        else if (ctlIntern is ToolStripDropDownButton)
                        {
                            ToolStripDropDownButton tst = new ToolStripDropDownButton();

                            tst.Name = ctlIntern.Name;
                            tst.Image = ctlIntern.Image;
                            tst.Text = ctlIntern.Text;
                            tst.Font = ctlIntern.Font;
                            tst.Alignment = ctlIntern.Alignment;
                            tst.AutoSize = ctlIntern.AutoSize;
                            tst.Size = ctlIntern.Size;
                            tst.TextImageRelation = ctlIntern.TextImageRelation;
                            tst.Margin = ctlIntern.Margin;

                            tsiItens[x] = tst;
                        }
                        else
                        {
                            throw new Exception(string.Format("Controle de tipo {0} não tratado no ToolBar.", ctlIntern.GetType()));
                        }

                        x++;
                    }

                    ((ToolStrip)ctl).BackColor = Color.FromArgb(224, 224, 224);
                    ((ToolStrip)ctl).Items.Clear();
                    ((ToolStrip)ctl).Items.AddRange(tsiItens);
                }
            }
        }

        private static void RemoveEventsItemMenu(ToolStripItem tsi)
        {
            RemoveEvents(tsi);

            tsi.Visible = true;
            tsi.MouseDown += new MouseEventHandler(tsi_MouseDown);

            if (tsi is ToolStripMenuItem)
            {
                foreach (ToolStripMenuItem tsi2 in ((ToolStripMenuItem)tsi).DropDownItems)
                {
                    RemoveEventsItemMenu(tsi2);
                }
            }
        }

        private static void tsi_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                actualItem = (ToolStripItem)sender;
                actual = null;

                cmsControl.Show(Form.MousePosition.X, Form.MousePosition.Y);
            }
        }

        public static void RemoveEvents(object obj)
        {
            FieldInfo[] fields = null;

            fields = typeof(Control).GetFields(BindingFlags.Static | BindingFlags.NonPublic);

            RemoveEvents(obj, fields);

            if (obj is Form)
            {
                fields = typeof(Form).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is MdiClient)
            {
                fields = typeof(MdiClient).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is RadioButton)
            {
                fields = typeof(RadioButton).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is DataGridView)
            {
                fields = typeof(DataGridView).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is ToolStrip)
            {
                fields = typeof(ToolStrip).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is ToolStripButton)
            {
                fields = typeof(ToolStripButton).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is TextBox)
            {
                fields = typeof(TextBox).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is GroupBox)
            {
                fields = typeof(GroupBox).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is Button)
            {
                fields = typeof(Button).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is ComboBox)
            {
                fields = typeof(ComboBox).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is Button)
            {
                fields = typeof(Button).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is CheckBox)
            {
                fields = typeof(CheckBox).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is Label)
            {
                fields = typeof(Label).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is PictureBox)
            {
                fields = typeof(PictureBox).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is ToolStripSeparator)
            {
                fields = typeof(ToolStripSeparator).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is ToolStripLabel)
            {
                fields = typeof(ToolStripLabel).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is ToolStripTextBox)
            {
                fields = typeof(ToolStripTextBox).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is HScrollBar)
            {
                fields = typeof(HScrollBar).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is VScrollBar)
            {
                fields = typeof(VScrollBar).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is TabControl)
            {
                fields = typeof(TabControl).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is TabPage)
            {
                fields = typeof(TabPage).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is Panel)
            {
                fields = typeof(Panel).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is ProgressBar)
            {
                fields = typeof(ProgressBar).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is NumericUpDown)
            {
                fields = typeof(NumericUpDown).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is CrystalReportViewer)
            {
                fields = typeof(CrystalReportViewer).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is ListBox)
            {
                fields = typeof(ListBox).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is DateTimePicker)
            {
                fields = typeof(DateTimePicker).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is ReportDocument)
            {
                fields = typeof(ReportDocument).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is PageView)
            {
                fields = typeof(PageView).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is Splitter)
            {
                fields = typeof(Splitter).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is ReportGroupTree)
            {
                fields = typeof(ReportGroupTree).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is TotallerTreeView)
            {
                fields = typeof(TotallerTreeView).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is ToolBar)
            {
                fields = typeof(ToolBar).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is ToolStripDropDownButton)
            {
                fields = typeof(ToolStripDropDownButton).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj is StatusBar)
            {
                fields = typeof(StatusBar).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (obj.GetType().ToString() == "System.Windows.Forms.UpDownBase+UpDownButtons" ||
                     obj.GetType().ToString() == "CrystalDecisions.Windows.Forms.InteractiveParameterPanel")
            {
                // Sem tratamento
                return;
            }
            else
            {
                throw new Exception(string.Format("Controle de tipo {0} não possue tratamento.", obj.GetType()));
            }

            RemoveEvents(obj, fields);
        }

        public static void RemoveEvents(ToolStripItem obj)
        {
            FieldInfo[] fields = null;

            fields = typeof(Component).GetFields(BindingFlags.Static | BindingFlags.NonPublic);

            RemoveEvents(obj, fields);

            fields = typeof(ToolStripItem).GetFields(BindingFlags.Static | BindingFlags.NonPublic);

            RemoveEvents(obj, fields);

            if (obj is ToolStripMenuItem)
            {
                fields = typeof(ToolStripMenuItem).GetFields(BindingFlags.Static | BindingFlags.NonPublic);
                RemoveEvents(obj, fields);
            }
        }

        public static void RemoveEvents(object obj, FieldInfo[] fields)
        {
            foreach (FieldInfo fi in fields)
            {
                if (fi.Name.Length >= 5 &&
                    fi.Name.Substring(0, 5).ToLower() == "event")
                {
                    object fieldValue = fi.GetValue(obj);

                    PropertyInfo pi = obj.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
                    EventHandlerList list = (EventHandlerList)pi.GetValue(obj, null);
                    list.RemoveHandler(fieldValue, list[fieldValue]);
                }
            }
        }

        private static void ApplyColor(Control ctl)
        {
            DataRow row = null;

            if (!(ctl is Form))
            {
                ctl.ContextMenuStrip = cmsControl;

                foreach (DataRow r in dtObjects)
                {
                    if (r["NOMEOBJETO"].ToString() == ctl.Name)
                    {
                        row = r;
                        break;
                    }
                }

                if (row != null &&
                    !(ctl is ToolStrip))
                {
                    if (row["ATIVO"].ToString() == "S" &&
                        row["VISIVEL"].ToString() == "S")
                    {
                        ctl.BackColor = colorAtivoVisivel;
                    }
                    else if (row["ATIVO"].ToString() == "N" &&
                             row["VISIVEL"].ToString() == "S")
                    {
                        ctl.BackColor = colorVisivelDesativado;
                    }
                    else if (row["ATIVO"].ToString() == "N" &&
                             row["VISIVEL"].ToString() == "N")
                    {
                        ctl.BackColor = colorNone;
                    }
                }
            }

            if (ctl.Controls != null)
            {
                foreach (Control ctlIntern in ctl.Controls)
                {
                    ApplyColor(ctlIntern);
                }
            }

            if (ctl is TabControl)
            {
                System.Windows.Forms.TabControl.TabPageCollection tps = ((TabControl)ctl).TabPages;

                if (tps != null && tps.Count > 0)
                {
                    foreach (TabPage ctlIntern in tps)
                    {
                        ApplyColor(ctlIntern);
                    }
                }
            }

            if (ctl is ToolStrip)
            {
                if (ctl is MenuStrip)
                {
                    foreach (ToolStripItem ctlIntern in ((MenuStrip)ctl).Items)
                    {
                        ctlIntern.Visible = true;

                        ApplyColorMenuItem(ctlIntern);
                    }
                }
                else
                {
                    foreach (ToolStripItem ctlIntern in ((ToolStrip)ctl).Items)
                    {
                        foreach (DataRow r in dtObjects)
                        {
                            if (r["NOMEOBJETO"].ToString() == ctlIntern.Name)
                            {
                                row = r;
                                break;
                            }
                        }

                        if (row != null)
                        {
                            if (row["ATIVO"].ToString() == "S" &&
                                row["VISIVEL"].ToString() == "S")
                            {
                                ctlIntern.BackColor = colorAtivoVisivel;
                            }
                            else if (row["ATIVO"].ToString() == "N" &&
                                     row["VISIVEL"].ToString() == "S")
                            {
                                ctlIntern.BackColor = colorVisivelDesativado;
                            }
                            else if (row["ATIVO"].ToString() == "N" &&
                                     row["VISIVEL"].ToString() == "N")
                            {
                                ctlIntern.BackColor = colorNone;
                            }
                        }
                    }
                }
            }
        }

        private static void ApplyColorMenuItem(ToolStripItem ctlIntern)
        {
            DataRow row = null;

            foreach (DataRow r in dtObjects)
            {
                if (r["NOMEOBJETO"].ToString() == ctlIntern.Name)
                {
                    row = r;
                    break;
                }
            }

            if (row != null)
            {
                if (row["ATIVO"].ToString() == "S" &&
                    row["VISIVEL"].ToString() == "S")
                {
                    ctlIntern.BackColor = colorAtivoVisivel;
                }
                else if (row["ATIVO"].ToString() == "N" &&
                         row["VISIVEL"].ToString() == "S")
                {
                    ctlIntern.BackColor = colorVisivelDesativado;
                }
                else if (row["ATIVO"].ToString() == "N" &&
                         row["VISIVEL"].ToString() == "N")
                {
                    ctlIntern.BackColor = colorNone;
                }
            }

            if (ctlIntern is ToolStripMenuItem)
            {
                if (((ToolStripMenuItem)ctlIntern).DropDownItems.Count > 0)
                {
                    foreach (ToolStripMenuItem tsmi in ((ToolStripMenuItem)ctlIntern).DropDownItems)
                    {
                        ApplyColorMenuItem(tsmi);
                    }
                }
            }
        }

        private static void Update(Control ctl)
        {
            if (!(ctl is Form))
            {
                // atualiza o dtObject com as alterações
                foreach (DataRow r in dtObjects)
                {
                    if (r["NOMEOBJETO"].ToString() == ctl.Name)
                    {
                        if (ctl.BackColor == colorAtivoVisivel)
                        {
                            r["ATIVO"] = "S";
                            r["VISIVEL"] = "S";
                        }
                        else if (ctl.BackColor == colorVisivelDesativado)
                        {
                            r["ATIVO"] = "N";
                            r["VISIVEL"] = "S";
                        }
                        else if (ctl.BackColor == colorNone)
                        {
                            r["ATIVO"] = "N";
                            r["VISIVEL"] = "N";
                        }

                        break;
                    }
                }
            }

            if (ctl.Controls != null)
            {
                foreach (Control ctlIntern in ctl.Controls)
                {
                    Update(ctlIntern);
                }
            }

            if (ctl is TabControl)
            {
                System.Windows.Forms.TabControl.TabPageCollection tps = ((TabControl)ctl).TabPages;

                if (tps != null && tps.Count > 0)
                {
                    foreach (TabPage ctlIntern in tps)
                    {
                        Update(ctlIntern);
                    }
                }
            }

            if (ctl is ToolStrip)
            {
                if (ctl is MenuStrip)
                {
                    foreach (ToolStripItem ctlIntern in ((MenuStrip)ctl).Items)
                    {
                        UpdateMenuItem(ctlIntern);
                    }
                }
                else
                {
                    foreach (ToolStripItem ctlIntern in ((ToolStrip)ctl).Items)
                    {
                        // atualiza o dtObject com as alterações
                        foreach (DataRow r in dtObjects)
                        {
                            if (r["NOMEOBJETO"].ToString() == ctlIntern.Name)
                            {
                                if (ctlIntern.BackColor == colorAtivoVisivel)
                                {
                                    r["ATIVO"] = "S";
                                    r["VISIVEL"] = "S";
                                }
                                else if (ctlIntern.BackColor == colorVisivelDesativado)
                                {
                                    r["ATIVO"] = "N";
                                    r["VISIVEL"] = "S";
                                }
                                else if (ctlIntern.BackColor == colorNone)
                                {
                                    r["ATIVO"] = "N";
                                    r["VISIVEL"] = "N";
                                }

                                break;
                            }
                        }
                    }
                }
            }
        }

        private static void UpdateMenuItem(ToolStripItem ctlIntern)
        {
            foreach (DataRow r in dtObjects)
            {
                if (r["NOMEOBJETO"].ToString() == ctlIntern.Name)
                {
                    if (ctlIntern.BackColor == colorAtivoVisivel)
                    {
                        r["ATIVO"] = "S";
                        r["VISIVEL"] = "S";
                    }
                    else if (ctlIntern.BackColor == colorVisivelDesativado)
                    {
                        r["ATIVO"] = "N";
                        r["VISIVEL"] = "S";
                    }
                    else if (ctlIntern.BackColor == colorNone)
                    {
                        r["ATIVO"] = "N";
                        r["VISIVEL"] = "N";
                    }

                    break;
                }
            }

            if (ctlIntern is ToolStripMenuItem)
            {
                if (((ToolStripMenuItem)ctlIntern).DropDownItems.Count > 0)
                {
                    foreach (ToolStripMenuItem tsmi in ((ToolStripMenuItem)ctlIntern).DropDownItems)
                    {
                        UpdateMenuItem(tsmi);
                    }
                }
            }
        }

        static Int32 _idFormulario;
        static SqlConnection scn;
        static SqlCommand scd;
        static SqlDataReader sdr;
        static String sql;

        public static void AbrirForm(Form _mdi, Form _frm, String _conexao)
        {
            _frm.MdiParent = _mdi;
            _frm.StartPosition = FormStartPosition.CenterParent;

            if (clsInfo.zempresacliente_cognome != null)
            {
                try
                {
                    UsuarioPermissoes(_frm, clsInfo.zusuarioid, _conexao);
                }
                catch (Exception ex)
                {
                    _frm.VisibleChanged += new EventHandler(_frm_VisibleChanged);
                    _frm.Visible = false;

                    MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Thread.Sleep(2000);
                    Thread th = new Thread(_frm.Close);
                    th.Start();

                    formVisibleChanged = false;

                    return;
                }

                VerificaControles(_frm, _conexao);
            }

            _frm.Show();
            _frm.Location = new Point(0, 0);
            _frm.Size = new Size(1020, 685);
        }

        public static void AbrirForm(Form _frm, String _conexao)
        {
            _frm.StartPosition = FormStartPosition.CenterScreen;
            _frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            _frm.ShowInTaskbar = false;
            _frm.MaximizeBox = false;
            _frm.MinimizeBox = false;

            if (clsInfo.zempresacliente_cognome != null)
            {
                try
                {
                    UsuarioPermissoes(_frm, clsInfo.zusuarioid, _conexao);
                }
                catch (Exception ex)
                {
                    _frm.VisibleChanged += new EventHandler(_frm_VisibleChanged);
                    _frm.Visible = false;

                    MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Thread.Sleep(2000);
                    Thread th = new Thread(_frm.Close);
                    th.Start();

                    formVisibleChanged = false;

                    return;
                }

                VerificaControles(_frm, _conexao);
            }

            _frm.Show();
        }
        public static void AbrirFormAdaptativo(Form _mdi, Form _frm, String _conexao)
        {
            _frm.MdiParent = _mdi;
            _frm.StartPosition = FormStartPosition.CenterParent;

            if (clsInfo.zempresacliente_cognome != null)
            {
                try
                {
                    //UsuarioPermissoes(_frm, clsInfo.zusuarioid, _conexao); // alterei de int para codigo 24/03/14 crc
                    UsuarioPermissoes(_frm, clsInfo.zusuarioid, _conexao);
                }
                catch (Exception ex)
                {
                    _frm.VisibleChanged += new EventHandler(_frm_VisibleChanged);
                    _frm.Visible = false;

                    MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Thread.Sleep(2000);
                    Thread th = new Thread(_frm.Close);
                    th.Start();

                    formVisibleChanged = false;

                    return;
                }

                VerificaControles(_frm, _conexao);
            }

            _frm.Show();
            _frm.Location = new Point(0, 0);
            //pixels de offset para que não pareça barra de rolagem
            int offset = 0;
            if (_frm.Parent.HasChildren)
            {
                offset = 10;
            }

            _frm.Size = new Size(_frm.Parent.Width - 5, _frm.Parent.Height - offset);
        }

        //abre o form ser acionar o activete caso seja uma 
        //chamada dentro de outro form
        public static void AbrirFormShowDialog(Form _frm, String _conexao)
        {
            _frm.StartPosition = FormStartPosition.Manual;
            if (clsInfo.zempresacliente_cognome != null)
            {
                try
                {
                    UsuarioPermissoes(_frm, clsInfo.zusuarioid, _conexao);
                }
                catch (Exception ex)
                {
                    _frm.VisibleChanged += new EventHandler(_frm_VisibleChanged);
                    _frm.Visible = false;
                    MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Thread.Sleep(2000);
                    Thread th = new Thread(_frm.Close);
                    th.Start();
                    formVisibleChanged = false;
                    return;
                }
                VerificaControles(_frm, _conexao);
            }
            _frm.Location = new Point(0, 0);
            _frm.Size = new Size(1020, 685);

            _frm.ShowDialog();
        }

        private static Boolean formVisibleChanged = false;

        static void _frm_VisibleChanged(object sender, EventArgs e)
        {
            if (formVisibleChanged == false)
            {
                ((Form)sender).Visible = false;
                formVisibleChanged = true;
            }
        }

        public static void VerificaControles(Form _frm, String _conexao)
        {
            // Agora Verifica os Controles
            try
            {
                String gruposystem = Procedure.PesquisaoPrimeiro(_conexao, "select gruposystem from usuario where id=" + clsInfo.zusuarioid);
                if (gruposystem == null || gruposystem == "N")
                {
                    UsuarioControles(_frm, clsInfo.zusuarioid, _conexao);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void UsuarioPermissoes(Form formatual, Int32 idusuario, String _conexao)
        {
            Assembly asy = Assembly.GetAssembly(formatual.GetType());
            String assemblyName = asy.ManifestModule.Name;

            if (clsUsuarioFormsBLL.UsuarioPermitido(idusuario, assemblyName, formatual.GetType().Name, _conexao, formatual) == false)
            {
                throw new Exception("Acesso não permitido.");
            }
        }

        public static void UsuarioControles(Form form_atual, Int32 idusuario, String _conexao)
        {
            // Formulários que devem passar sem validação
            if (form_atual == null ||
                form_atual.GetType().Name == "frmCrystalReport")
            {
                return;
            }

            clsFormsBLL FormsBLL = new clsFormsBLL();
            clsFormsInfo FormsInfo;

            clsUsuarioFormsObjetoBLL UsuarioFormsObjetoBLL = new clsUsuarioFormsObjetoBLL();
            List<clsUsuarioFormsObjetoInfo> objetosLista;

            Assembly asy = Assembly.GetAssembly(form_atual.GetType());
            String assemblyName = asy.ManifestModule.Name;

            if (assemblyName.IndexOf(',') != -1)
            {
                assemblyName = assemblyName.Substring(0, assemblyName.IndexOf(','));
            }

            FormsInfo = FormsBLL.Carregar(form_atual.GetType().Name, assemblyName, _conexao);
            if (FormsInfo == null)
            {
                throw new Exception("Formulário " + form_atual.GetType().Name + " não está cadastrado no sistema. Contate a Equipe de TI para que este seja cadastrado.");
            }

            objetosLista = UsuarioFormsObjetoBLL.Lista(idusuario, FormsInfo.id, _conexao);

            String controle_name = "";
            foreach (clsUsuarioFormsObjetoInfo info in objetosLista)
            {
                controle_name = Procedure.PesquisaoPrimeiro(_conexao, "select nomeobjeto from formsobjeto where id=" + info.idformsobjeto);

                if (controle_name != null && controle_name != "")
                {
                    ProcuraControle(controle_name, info.ativo, info.visivel, form_atual);
                }
            }
        }

        public static Object ProcuraControle(String name, String ativo, String visivel, Form frm)
        {
            Object obj = null;

            foreach (Control ctl in frm.Controls)
            {
                obj = ProcuraControles(name, ativo, visivel, ctl);

                if (obj != null)
                {
                    break;
                }
            }

            return obj;
        }

        public static Object ProcuraControles(String name, String ativo, String visivel, Object ctl)
        {
            Object obj = null;

            if (ctl is MenuStrip)
            {
                if (((MenuStrip)ctl).Name == name)
                {
                    if (ativo == "N")
                    {
                        ((MenuStrip)ctl).Enabled = false;
                        ((MenuStrip)ctl).EnabledChanged += new EventHandler(clsForms_EnabledChanged);
                    }

                    if (visivel == "N")
                    {
                        ((MenuStrip)ctl).Visible = false;
                        ((MenuStrip)ctl).VisibleChanged += new EventHandler(clsForms_VisibleChanged);
                    }

                    return (MenuStrip)ctl;
                }
                else if (((MenuStrip)ctl).Items != null && ((MenuStrip)ctl).Items.Count > 0)
                {
                    foreach (ToolStripItem item in ((MenuStrip)ctl).Items)
                    {
                        obj = ProcuraControles(name, ativo, visivel, item);

                        if (obj != null)
                        {
                            break;
                        }
                    }
                }
            }
            else if (ctl is ToolStripMenuItem)
            {
                if (((ToolStripMenuItem)ctl).Name == name)
                {
                    if (ativo == "N")
                    {
                        ((ToolStripMenuItem)ctl).Enabled = false;
                        ((ToolStripMenuItem)ctl).EnabledChanged += new EventHandler(clsForms_EnabledChanged);
                    }

                    if (visivel == "N")
                    {
                        ((ToolStripMenuItem)ctl).Visible = false;
                        ((ToolStripMenuItem)ctl).VisibleChanged += new EventHandler(clsForms_VisibleChanged);
                    }

                    return (ToolStripMenuItem)ctl;
                }
                else if (((ToolStripMenuItem)ctl).DropDownItems != null && ((ToolStripMenuItem)ctl).DropDownItems.Count > 0)
                {
                    foreach (ToolStripItem item in ((ToolStripMenuItem)ctl).DropDownItems)
                    {
                        obj = ProcuraControles(name, ativo, visivel, item);

                        if (obj != null)
                        {
                            break;
                        }
                    }
                }
            }
            else if (ctl is ToolStripButton)
            {
                if (((ToolStripButton)ctl).Name == name)
                {
                    if (ativo == "N")
                    {
                        ((ToolStripButton)ctl).Enabled = false;
                        ((ToolStripButton)ctl).EnabledChanged += new EventHandler(clsForms_EnabledChanged);
                    }

                    if (visivel == "N")
                    {
                        ((ToolStripButton)ctl).Visible = false;
                        ((ToolStripButton)ctl).VisibleChanged += new EventHandler(clsForms_VisibleChanged);
                    }

                    return (ToolStripButton)ctl;
                }
            }
            else if (ctl is ToolStrip)
            {
                if (((ToolStrip)ctl).Name == name)
                {
                    if (ativo == "N")
                    {
                        ((ToolStrip)ctl).Enabled = false;
                        ((ToolStrip)ctl).EnabledChanged += new EventHandler(clsForms_EnabledChanged);
                    }

                    if (visivel == "N")
                    {
                        ((ToolStrip)ctl).Visible = false;
                        ((ToolStrip)ctl).VisibleChanged += new EventHandler(clsForms_VisibleChanged);
                    }

                    return (ToolStrip)ctl;
                }
                else if (((ToolStrip)ctl).Items != null && ((ToolStrip)ctl).Items.Count > 0)
                {
                    foreach (ToolStripItem item in ((ToolStrip)ctl).Items)
                    {
                        obj = ProcuraControles(name, ativo, visivel, item);

                        if (obj != null)
                        {
                            break;
                        }
                    }
                }
            }
            else if (ctl is Control)
            {
                if (((Control)ctl).Name == name)
                {
                    if (ativo == "N")
                    {
                        ((Control)ctl).Enabled = false;
                        ((Control)ctl).EnabledChanged += new EventHandler(clsForms_EnabledChanged);
                    }

                    if (visivel == "N")
                    {
                        ((Control)ctl).Visible = false;
                        ((Control)ctl).VisibleChanged += new EventHandler(clsForms_VisibleChanged);
                    }

                    return (Control)ctl;
                }
                else if (ctl is Control && ((Control)ctl).Controls != null)
                {
                    foreach (Control item in ((Control)ctl).Controls)
                    {
                        obj = ProcuraControles(name, ativo, visivel, item);

                        if (obj != null)
                        {
                            break;
                        }
                    }
                }
            }

            return obj;
        }

        static void clsForms_EnabledChanged(object sender, EventArgs e)
        {
            if (sender is MenuStrip)
            {
                ((MenuStrip)sender).EnabledChanged -= clsForms_EnabledChanged;
                ((MenuStrip)sender).Enabled = false;
                ((MenuStrip)sender).EnabledChanged += clsForms_EnabledChanged;
            }
            else if (sender is ToolStripMenuItem)
            {
                ((ToolStripMenuItem)sender).EnabledChanged -= clsForms_EnabledChanged;
                ((ToolStripMenuItem)sender).Enabled = false;
                ((ToolStripMenuItem)sender).EnabledChanged += clsForms_EnabledChanged;
            }
            else if (sender is ToolStripButton)
            {
                ((ToolStripButton)sender).EnabledChanged -= clsForms_EnabledChanged;
                ((ToolStripButton)sender).Enabled = false;
                ((ToolStripButton)sender).EnabledChanged += clsForms_EnabledChanged;
            }
            else if (sender is ToolStrip)
            {
                ((ToolStrip)sender).EnabledChanged -= clsForms_EnabledChanged;
                ((ToolStrip)sender).Enabled = false;
                ((ToolStrip)sender).EnabledChanged += clsForms_EnabledChanged;
            }
            else if (sender is Control)
            {
                ((Control)sender).EnabledChanged -= clsForms_EnabledChanged;
                ((Control)sender).Enabled = false;
                ((Control)sender).EnabledChanged += clsForms_EnabledChanged;
            }
        }

        static void clsForms_VisibleChanged(object sender, EventArgs e)
        {
            if (sender is MenuStrip)
            {
                ((MenuStrip)sender).VisibleChanged -= clsForms_VisibleChanged;
                ((MenuStrip)sender).Visible = false;
                ((MenuStrip)sender).VisibleChanged += clsForms_VisibleChanged;
            }
            else if (sender is ToolStripMenuItem)
            {
                ((ToolStripMenuItem)sender).VisibleChanged -= clsForms_VisibleChanged;
                ((ToolStripMenuItem)sender).Visible = false;
                ((ToolStripMenuItem)sender).VisibleChanged += clsForms_VisibleChanged;
            }
            else if (sender is ToolStripButton)
            {
                ((ToolStripButton)sender).VisibleChanged -= clsForms_VisibleChanged;
                ((ToolStripButton)sender).Visible = false;
                ((ToolStripButton)sender).VisibleChanged += clsForms_VisibleChanged;
            }
            else if (sender is ToolStrip)
            {
                ((ToolStrip)sender).VisibleChanged -= clsForms_VisibleChanged;
                ((ToolStrip)sender).Visible = false;
                ((ToolStrip)sender).VisibleChanged += clsForms_VisibleChanged;
            }
            else if (sender is Control)
            {
                ((Control)sender).VisibleChanged -= clsForms_VisibleChanged;
                ((Control)sender).Visible = false;
                ((Control)sender).VisibleChanged += clsForms_VisibleChanged;
            }
        }

        public static void VerificarForm(Form f, ToolTip ttp, String _conexao)
        {
            try
            {
                if (Int32.Parse(f.Tag.ToString()) == 0)
                {
                    throw new Exception("Número de Formulário inválido.");
                }

                if (ConfigForm(Int32.Parse(f.Tag.ToString()),
                                        f,
                                        f.Text,
                                        _conexao,
                                        ttp) == false)
                {
                    f.Close();
                    return;
                }

                if (AcessaForm(Int32.Parse(f.Tag.ToString()),
                                        f,
                                        f.Text,
                                        _conexao,
                                        ttp) == false)
                {
                    f.Close();
                    return;
                }

                AcessaObjetos(Int32.Parse(f.Tag.ToString()), clsInfo.zmodulo, f, _conexao);
            }
            catch
            {
                //MessageBox.Show("VerificaForm: " + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static Boolean AcessaForm(Int32 _NroForm, Form _NomeForm, String _Descricao, String _conexao, ToolTip _legenda)
        {
            if (clsInfo.zmodulo == 0)
            {
                MessageBox.Show("Falta colocar o codigo interno do modulo  ==> informe (T.I.) : ");
                return false;
            }
            if (_NroForm == 0)
            { // retorna e cancela entrada do usuario
                MessageBox.Show("Este formulário não possui numero interno informe (T.I.) : "
                    + Environment.NewLine + "Nro  Form = " + _NroForm
                    + Environment.NewLine + "Name Form = " + _NomeForm.Name
                    + Environment.NewLine + "Descrição = " + _Descricao
                    , "Aplisoft - Segurança");
                return false;
            }
            else
            {
                if (clsInfo.zusuario != "SUPERVISOR")
                {
                    String sql;
                    SqlConnection scn = new SqlConnection(_conexao);
                    SqlCommand scd;
                    SqlDataReader sdrr;
                    scn.Open();
                    // gambi

                    sql = "SELECT USUARIOFORMULARIOS.LIBERADO AS [LIBERADO] " +
                          "FROM USUARIOFORMULARIOS " +
                          "INNER JOIN FORMULARIOS ON USUARIOFORMULARIOS.IDFORMULARIO=FORMULARIOS.ID " +
                          "WHERE FORMULARIOS.MODULO =" + clsInfo.zmodulo + " " +
                          "AND FORMULARIOS.NUMERO = " + _NroForm + " " +
                          "AND USUARIOFORMULARIOS.IDUSUARIO =" + clsInfo.zusuarioid;
                    scd = new SqlCommand(sql, scn);
                    sdrr = scd.ExecuteReader();
                    if (sdrr.Read())
                    { // achou
                        if (sdrr["LIBERADO"].ToString() == "S")
                        {   // liberado sim ou não
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Usuário você não possui acesso a este formulario : "
                                + Environment.NewLine + "Nro Modulo = " + clsInfo.zmodulo
                                + Environment.NewLine + "Nro  Form = " + _NroForm
                                + Environment.NewLine + "Name Form = " + _NomeForm.Name
                                + Environment.NewLine + "Descrição = " + _Descricao
                                , "Aplisoft - Segurança  << " + clsInfo.zusuario + " >>");
                            return false;
                        }
                    }
                    else
                    { // não achou

                        /*MessageBox.Show("Caro Usuário : " + clsInfo.zusuario + " você não possui acesso a este formulario : "
                            + Environment.NewLine + "Nro Modulo = " + clsInfo.zmodulo
                            + Environment.NewLine + "Nro  Form = " + _NroForm
                            + Environment.NewLine + "Name Form = " + _NomeForm.Name
                            + Environment.NewLine + "Descrição = " + _Descricao
                            , "Aplisoft - Segurança << " + clsInfo.zusuario + " >>");
                        return false;
                        */

                        MessageBox.Show("Contate a Equipe de TI do Software:"
                                        + Environment.NewLine + "N° Módulo  = " + clsInfo.zmodulo
                                        + Environment.NewLine + "N° Form.   = " + _NroForm
                                        + Environment.NewLine + "Nome Form. = " + _NomeForm.Name
                                        + Environment.NewLine + "Descrição  = " + _Descricao, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        private static void AcessaObjetos(Int32 _nroform, Int32 _modulo, Form _form, String _conexao)
        {
            if (clsInfo.zusuario != "SUPERVISOR")
            {
                SqlConnection scn;
                SqlCommand scd;
                SqlDataReader sdr;

                String sql;

                sql = "SELECT FORMULARIOSOBJETO.NOMEOBJETO AS [NOME], USUARIOFORMULARIOSOBJETO.LIBERADO AS [LIBERADO] " +
                      "FROM USUARIOFORMULARIOSOBJETO " +
                      "INNER JOIN FORMULARIOSOBJETO ON USUARIOFORMULARIOSOBJETO.IDFORMULARIOSOBJETO=FORMULARIOSOBJETO.ID " +
                      "INNER JOIN FORMULARIOS ON FORMULARIOSOBJETO.IDFORMULARIO=FORMULARIOS.ID " +
                      "WHERE FORMULARIOS.MODULO =" + _modulo + " " +
                      "AND FORMULARIOS.NUMERO = " + _nroform + " " +
                      "AND USUARIOFORMULARIOSOBJETO.IDUSUARIO =" + clsInfo.zusuarioid + " " +
                      "AND USUARIOFORMULARIOSOBJETO.LIBERADO='N'";

                scn = new SqlConnection(_conexao);
                scn.Open();
                scd = new SqlCommand(sql, scn);
                sdr = scd.ExecuteReader();
                while (sdr.Read())
                {
                    if (sdr["LIBERADO"].ToString() == "N")
                    {
                        try
                        {
                            foreach (Control c in _form.Controls)
                            {
                                if (c.Name == sdr["NOME"].ToString())
                                    AplicaPropriedades(_form.Controls[c.Name]);
                                else
                                {
                                    foreach (Control c2 in c.Controls)
                                    {
                                        if (c2.Name == sdr["NOME"].ToString())
                                            AplicaPropriedades(_form.Controls[c.Name].Controls[c2.Name]);
                                        else
                                        {
                                            foreach (Control c3 in c2.Controls)
                                            {
                                                if (c3.Name == sdr["NOME"].ToString())
                                                    AplicaPropriedades(_form.Controls[c.Name].Controls[c2.Name].Controls[c3.Name]);
                                                else
                                                {
                                                    foreach (Control c4 in c3.Controls)
                                                    {
                                                        if (c4.Name == sdr["NOME"].ToString())
                                                            AplicaPropriedades(_form.Controls[c.Name].Controls[c2.Name].Controls[c3.Name].Controls[c4.Name]);
                                                        else
                                                        {
                                                            foreach (Control c5 in c4.Controls)
                                                            {
                                                                if (c5.Name == sdr["NOME"].ToString())
                                                                    AplicaPropriedades(_form.Controls[c.Name].Controls[c2.Name].Controls[c3.Name].Controls[c4.Name].Controls[c5.Name]);
                                                                else
                                                                {
                                                                    foreach (Control c6 in c5.Controls)
                                                                    {
                                                                        if (c6.Name == sdr["NOME"].ToString())
                                                                            AplicaPropriedades(_form.Controls[c.Name].Controls[c2.Name].Controls[c3.Name].Controls[c4.Name].Controls[c5.Name].Controls[c6.Name]);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }

        private static void AplicaPropriedades(Control c)
        {
            try
            {
                c.EnabledChanged += new EventHandler(Control_EnabledChanged);
                c.Enabled = false;
            }
            catch
            {

            }
        }

        private static void Control_EnabledChanged(object sender, EventArgs e)
        {
            try
            {
                if (((Control)sender).Enabled != false)
                    ((Control)sender).Enabled = false;
            }
            catch
            {

            }
        }

        private static Boolean ConfigForm(Int32 _NroForm, Form _NomeForm, String _Descricao, String conexao, ToolTip _legenda)
        {
            if (clsInfo.zmodulo == 0)
            {
                MessageBox.Show("Falta colocar o codigo interno do modulo  ==> informe (T.I.) : ");
                return false;
            }
            if (_NroForm == 0)
            { // retorna e cancela entrada do usuario
                MessageBox.Show("Este formulário não possui numero interno informe (T.I.) : "
                    + Environment.NewLine + "Nro  Form = " + _NroForm
                    + Environment.NewLine + "Name Form = " + _NomeForm.Name
                    + Environment.NewLine + "Descrição = " + _Descricao
                    , "Aplisoft - Segurança");
                return false;
            }
            else
            {  // localizar o Form

                clsFormulariosBLL clsFormulariosBLL = new clsFormulariosBLL();
                clsFormulariosInfo clsFormulariosInfo = new clsFormulariosInfo();
                clsFormulariosInfo = clsFormulariosBLL.CarregarForm(conexao,
                                                       clsParser.Int32Parse(_NroForm.ToString()),
                                                       clsInfo.zmodulo);
                if (clsFormulariosInfo != null)
                { // achou
                    clsFormulariosInfo.nome = _Descricao.ToString();
                    clsFormulariosInfo.nomeform = _NomeForm.Name.ToString();
                    clsFormulariosBLL.Alterar(conexao, clsFormulariosInfo);
                    if (clsInfo.zmodulo == _NroForm)
                    { // é um modulo  -- verifica se é o for diferente solicite confirmação ?
                        if (clsFormulariosInfo.nome != _Descricao.ToString())
                        {
                            DialogResult resultado;
                            resultado = MessageBox.Show("Nome de Modulo diferente !! era: " + clsFormulariosInfo.nome + " passa para : " + _Descricao + " Deseja Continuar ?",
                                                            "Aplisoft",
                                                                MessageBoxButtons.YesNoCancel,
                                                            MessageBoxIcon.Question,
                                                            MessageBoxDefaultButton.Button1);
                            if (resultado == DialogResult.Yes)
                            {
                                // ok continua
                            }
                            else if (resultado == DialogResult.Cancel)
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                { // não achou
                    clsFormulariosInfo clsFormulariosInfo1 = new clsFormulariosInfo();
                    clsFormulariosInfo1.numero = clsParser.Int32Parse(_NroForm.ToString());
                    clsFormulariosInfo1.modulo = clsInfo.zmodulo;
                    clsFormulariosInfo1.nome = _Descricao.ToString();
                    clsFormulariosInfo1.nomeform = _NomeForm.Name.ToString();
                    clsFormulariosInfo1.ativo = "S";
                    clsFormulariosInfo1.liberado = "N";
                    clsFormulariosInfo1.aparecelista = "N";
                    clsFormulariosInfo1.verificarform = "";
                    _idFormulario = clsFormulariosBLL.Incluir(conexao, clsFormulariosInfo1);
                    clsFormulariosInfo = clsFormulariosInfo1;
                }
                if (clsFormulariosInfo.id > 0)
                {
                    _idFormulario = clsFormulariosInfo.id;
                }
                // colocar Não em todos os objetos que encontrar
                sql = "UPDATE FORMULARIOSOBJETO SET VERIFICARFORM='N' WHERE IDFORMULARIO=" + _idFormulario;
                scn = new SqlConnection(conexao);
                scd = new SqlCommand(sql, scn);
                scn.Open();
                sdr = scd.ExecuteReader();
                scn.Close();

                try
                {
                    VerificaObjetos(conexao, _NomeForm, _idFormulario, _legenda, _NroForm);
                }
                catch (Exception ex)
                {
                    String s;
                    s = "=================================================================" + Environment.NewLine;
                    s += "   Usuário: " + clsInfo.zusuario + " | Id: " + clsInfo.zusuarioid.ToString() + Environment.NewLine;
                    s += "ConfigForm: " + _NroForm.ToString() + " | " + _NomeForm + " | " + _Descricao + Environment.NewLine;
                    s += "   Message: " + ex.Message + Environment.NewLine;
                    System.IO.File.AppendAllText(Application.StartupPath + "\\erros.txt", s);
                    MessageBox.Show(s, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //foreach (Control mControl in _NomeForm.Controls)
                //{
                //    if (
                //        mControl.GetType().ToString() == "System.Windows.Forms.GroupBox" ||
                //        mControl.GetType().ToString() == "System.Windows.Forms.Panel" ||
                //        mControl.GetType().ToString() == "System.Windows.Forms.TabControl" ||
                //        mControl.GetType().ToString() == "System.Windows.Forms.TabPage" ||
                //        mControl.GetType().ToString() == "System.Windows.Forms.MenuStrip" ||
                //        mControl.GetType().ToString() == "System.Windows.Forms.TollStrip"
                //        )
                //    {
                //         clsForms y = new clsForms();
                //         y.LimpaForm(mControl, conexao, _idFormulario);
                //    }
                //    if (mControl.GetType().ToString() == "System.Windows.Forms.TextBox")
                //    {
                //        //mControl.Text = "";
                //    }
                //}

                // Apagar todos os objetos que tiver com N no VERIFICARFORM
                sql = "SELECT ID FROM FORMULARIOSOBJETO WHERE IDFORMULARIO=" + _idFormulario + " AND VERIFICARFORM='N'";
                scn = new SqlConnection(conexao);
                scd = new SqlCommand(sql, scn);

                SqlConnection scn2 = new SqlConnection(conexao);
                scn2.Open();
                SqlCommand scd2;

                scn.Open();
                sdr = scd.ExecuteReader();
                while (sdr.Read())
                {
                    sql = "DELETE USUARIOFORMULARIOSOBJETO WHERE IDFORMULARIOSOBJETO=" + sdr["ID"].ToString();
                    scd2 = new SqlCommand(sql, scn2);
                    scd2.ExecuteNonQuery();
                }
                sdr.Close();
                scn2.Close();

                sql = "DELETE FORMULARIOSOBJETO WHERE IDFORMULARIO=" + _idFormulario + " AND VERIFICARFORM='N'";
                scd = new SqlCommand(sql, scn);
                scd.ExecuteNonQuery();

                scn.Close();

                return true;
            }

        }
        private static void VerificaObjetos(String _conexao, Control _fForm, Int32 _idFormulario, ToolTip _legenda, Int32 _NroForm)
        {
            foreach (Control Controles0 in _fForm.Controls)
            {
                if (Controles0.GetType() == typeof(MenuStrip))
                {
                    foreach (ToolStripMenuItem MenuPrincipal in ((MenuStrip)Controles0).Items)
                    {
                        GravaObjetos(_conexao, _idFormulario, MenuPrincipal.Name, MenuPrincipal.ToolTipText, _NroForm, _fForm.Name);
                        //--
                        foreach (ToolStripItem SubMenu1Principal in MenuPrincipal.DropDownItems)
                        {
                            GravaObjetos(_conexao, _idFormulario, SubMenu1Principal.Name, SubMenu1Principal.ToolTipText, _NroForm, _fForm.Name);
                            //--
                            foreach (ToolStripItem SubMenu2Principal in ((ToolStripMenuItem)SubMenu1Principal).DropDownItems)
                            {
                                GravaObjetos(_conexao, _idFormulario, SubMenu2Principal.Name, SubMenu2Principal.ToolTipText, _NroForm, _fForm.Name);
                                //--
                                foreach (ToolStripItem SubMenu3Principal in ((ToolStripMenuItem)SubMenu2Principal).DropDownItems)
                                {
                                    GravaObjetos(_conexao, _idFormulario, SubMenu3Principal.Name, SubMenu3Principal.ToolTipText, _NroForm, _fForm.Name);
                                    //--
                                    foreach (ToolStripItem SubMenu4Principal in ((ToolStripMenuItem)SubMenu3Principal).DropDownItems)
                                    {
                                        GravaObjetos(_conexao, _idFormulario, SubMenu4Principal.Name, SubMenu4Principal.ToolTipText, _NroForm, _fForm.Name);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (Controles0.GetType() == typeof(ToolStrip))
                {
                    foreach (ToolStripItem ToolStrip in ((ToolStrip)Controles0).Items)
                    {
                        if (ToolStrip.GetType() != typeof(ToolStripLabel))
                        {
                            if (ToolStrip.GetType() != typeof(ToolStripSeparator))
                            {
                                if (ToolStrip.GetType() != typeof(ToolStripTextBox))
                                {
                                    GravaObjetos(_conexao, _idFormulario, ToolStrip.Name, ToolStrip.ToolTipText, _NroForm, _fForm.Name);
                                }
                            }
                        }
                    }
                }
                else if (Controles0.GetType() != typeof(ToolTip) && Controles0.GetType() != typeof(StatusStrip) &&
                         Controles0.GetType() != typeof(Label) && Controles0.GetType() != typeof(HScrollBar) &&
                         Controles0.GetType() != typeof(VScrollBar) && Controles0.GetType() != typeof(MdiClient))
                {
                    if (Controles0.GetType() == typeof(TextBox))
                    {
                        if (((TextBox)Controles0).ReadOnly == false)
                        {
                            GravaObjetos(_conexao, _idFormulario, Controles0.Name, _legenda.GetToolTip(Controles0), _NroForm, _fForm.Name);
                        }
                    }
                    else
                    {
                        GravaObjetos(_conexao, _idFormulario, Controles0.Name, _legenda.GetToolTip(Controles0), _NroForm, _fForm.Name);
                    }
                }
                //--
                foreach (Control Controles1 in Controles0.Controls)
                {
                    if (Controles1.GetType() != typeof(Label) && Controles1.GetType() != typeof(HScrollBar) &&
                        Controles1.GetType() != typeof(VScrollBar) && Controles1.GetType() != typeof(ToolStrip))
                    {
                        if (Controles1.GetType() == typeof(TextBox))
                        {
                            if (((TextBox)Controles1).ReadOnly == false)
                            {
                                GravaObjetos(_conexao, _idFormulario, Controles1.Name, _legenda.GetToolTip(Controles1), _NroForm, _fForm.Name);
                            }
                        }
                        else
                        {
                            GravaObjetos(_conexao, _idFormulario, Controles1.Name, _legenda.GetToolTip(Controles1), _NroForm, _fForm.Name);
                        }
                    }
                    if (Controles1.GetType() == typeof(ToolStrip))
                    {
                        foreach (ToolStripItem ToolStrip in ((ToolStrip)Controles1).Items)
                        {
                            if (ToolStrip.GetType() != typeof(ToolStripLabel))
                            {
                                if (ToolStrip.GetType() != typeof(ToolStripSeparator))
                                {
                                    if (ToolStrip.GetType() != typeof(ToolStripTextBox))
                                    {
                                        GravaObjetos(_conexao, _idFormulario, ToolStrip.Name, ToolStrip.ToolTipText, _NroForm, _fForm.Name);
                                    }
                                }
                            }
                        }
                    }
                    //--                    
                    foreach (Control Controles2 in Controles1.Controls)
                    {
                        if (Controles2.GetType() != typeof(Label) && Controles2.GetType() != typeof(HScrollBar) &&
                           Controles2.GetType() != typeof(VScrollBar) && Controles2.GetType() != typeof(ToolStrip))
                        {
                            if (Controles2.GetType() == typeof(TextBox))
                            {
                                if (((TextBox)Controles2).ReadOnly == false)
                                {
                                    GravaObjetos(_conexao, _idFormulario, Controles2.Name, _legenda.GetToolTip(Controles2), _NroForm, _fForm.Name);
                                }
                            }
                            else
                            {
                                GravaObjetos(_conexao, _idFormulario, Controles2.Name, _legenda.GetToolTip(Controles2), _NroForm, _fForm.Name);
                            }
                        }
                        if (Controles2.GetType() == typeof(ToolStrip))
                        {
                            foreach (ToolStripItem ToolStrip in ((ToolStrip)Controles2).Items)
                            {
                                if (ToolStrip.GetType() != typeof(ToolStripLabel))
                                {
                                    if (ToolStrip.GetType() != typeof(ToolStripSeparator))
                                    {
                                        if (ToolStrip.GetType() != typeof(ToolStripTextBox))
                                        {
                                            GravaObjetos(_conexao, _idFormulario, ToolStrip.Name, ToolStrip.ToolTipText, _NroForm, _fForm.Name);
                                        }
                                    }
                                }
                            }
                        }
                        //--                        
                        foreach (Control Controles3 in Controles2.Controls)
                        {
                            if (Controles3.GetType() != typeof(Label) && Controles3.GetType() != typeof(HScrollBar) &&
                               Controles3.GetType() != typeof(VScrollBar) && Controles3.GetType() != typeof(ToolStrip))
                            {
                                if (Controles3.GetType() == typeof(TextBox))
                                {
                                    if (((TextBox)Controles3).ReadOnly == false)
                                    {
                                        GravaObjetos(_conexao, _idFormulario, Controles3.Name, _legenda.GetToolTip(Controles3), _NroForm, _fForm.Name);
                                    }
                                }
                                else
                                {
                                    GravaObjetos(_conexao, _idFormulario, Controles3.Name, _legenda.GetToolTip(Controles3), _NroForm, _fForm.Name);
                                }
                            }
                            if (Controles3.GetType() == typeof(ToolStrip))
                            {
                                foreach (ToolStripItem ToolStrip in ((ToolStrip)Controles3).Items)
                                {
                                    if (ToolStrip.GetType() != typeof(ToolStripLabel))
                                    {
                                        if (ToolStrip.GetType() != typeof(ToolStripSeparator))
                                        {
                                            if (ToolStrip.GetType() != typeof(ToolStripTextBox))
                                            {
                                                GravaObjetos(_conexao, _idFormulario, ToolStrip.Name, ToolStrip.ToolTipText, _NroForm, _fForm.Name);
                                            }
                                        }
                                    }
                                }
                            }
                            //--
                            foreach (Control Controles4 in Controles3.Controls)
                            {
                                if (Controles4.GetType() != typeof(Label) && Controles4.GetType() != typeof(HScrollBar) &&
                                   Controles4.GetType() != typeof(VScrollBar) && Controles4.GetType() != typeof(ToolStrip))
                                {
                                    if (Controles4.GetType() == typeof(TextBox))
                                    {
                                        if (((TextBox)Controles4).ReadOnly == false)
                                        {
                                            GravaObjetos(_conexao, _idFormulario, Controles4.Name, _legenda.GetToolTip(Controles4), _NroForm, _fForm.Name);
                                        }
                                    }
                                    else
                                    {
                                        GravaObjetos(_conexao, _idFormulario, Controles4.Name, _legenda.GetToolTip(Controles4), _NroForm, _fForm.Name);
                                    }
                                }
                                if (Controles4.GetType() == typeof(ToolStrip))
                                {
                                    foreach (ToolStripItem ToolStrip in ((ToolStrip)Controles4).Items)
                                    {
                                        if (ToolStrip.GetType() != typeof(ToolStripLabel))
                                        {
                                            if (ToolStrip.GetType() != typeof(ToolStripSeparator))
                                            {
                                                if (ToolStrip.GetType() != typeof(ToolStripTextBox))
                                                {
                                                    GravaObjetos(_conexao, _idFormulario, ToolStrip.Name, ToolStrip.ToolTipText, _NroForm, _fForm.Name);
                                                }
                                            }
                                        }
                                    }
                                }
                                //--
                                foreach (Control Controles5 in Controles4.Controls)
                                {
                                    if (Controles5.GetType() != typeof(Label) && Controles5.GetType() != typeof(HScrollBar) &&
                                       Controles5.GetType() != typeof(VScrollBar) && Controles5.GetType() != typeof(ToolStrip))
                                    {
                                        if (Controles5.GetType() == typeof(TextBox))
                                        {
                                            if (((TextBox)Controles5).ReadOnly == false)
                                            {
                                                GravaObjetos(_conexao, _idFormulario, Controles5.Name, _legenda.GetToolTip(Controles5), _NroForm, _fForm.Name);
                                            }
                                        }
                                        else
                                        {
                                            GravaObjetos(_conexao, _idFormulario, Controles5.Name, _legenda.GetToolTip(Controles5), _NroForm, _fForm.Name);
                                        }
                                    }
                                    if (Controles5.GetType() == typeof(ToolStrip))
                                    {
                                        foreach (ToolStripItem ToolStrip in ((ToolStrip)Controles5).Items)
                                        {
                                            if (ToolStrip.GetType() != typeof(ToolStripLabel))
                                            {
                                                if (ToolStrip.GetType() != typeof(ToolStripSeparator))
                                                {
                                                    if (ToolStrip.GetType() != typeof(ToolStripTextBox))
                                                    {
                                                        GravaObjetos(_conexao, _idFormulario, ToolStrip.Name, ToolStrip.ToolTipText, _NroForm, _fForm.Name);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //--
                                    foreach (Control Controles6 in Controles5.Controls)
                                    {
                                        if (Controles6.GetType() != typeof(Label) && Controles6.GetType() != typeof(HScrollBar) &&
                                           Controles6.GetType() != typeof(VScrollBar) && Controles6.GetType() != typeof(ToolStrip))
                                        {
                                            if (Controles6.GetType() == typeof(TextBox))
                                            {
                                                if (((TextBox)Controles6).ReadOnly == false)
                                                {
                                                    GravaObjetos(_conexao, _idFormulario, Controles6.Name, _legenda.GetToolTip(Controles6), _NroForm, _fForm.Name);
                                                }
                                            }
                                            else
                                            {
                                                GravaObjetos(_conexao, _idFormulario, Controles6.Name, _legenda.GetToolTip(Controles6), _NroForm, _fForm.Name);
                                            }
                                        }
                                        if (Controles6.GetType() == typeof(ToolStrip))
                                        {
                                            foreach (ToolStripItem ToolStrip in ((ToolStrip)Controles6).Items)
                                            {
                                                if (ToolStrip.GetType() != typeof(ToolStripLabel))
                                                {
                                                    if (ToolStrip.GetType() != typeof(ToolStripSeparator))
                                                    {
                                                        if (ToolStrip.GetType() != typeof(ToolStripTextBox))
                                                        {
                                                            GravaObjetos(_conexao, _idFormulario, ToolStrip.Name, ToolStrip.ToolTipText, _NroForm, _fForm.Name);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        //--
                                        foreach (Control Controles7 in Controles6.Controls)
                                        {
                                            if (Controles7.GetType() != typeof(Label) && Controles7.GetType() != typeof(HScrollBar) &&
                                                Controles7.GetType() != typeof(VScrollBar) && Controles7.GetType() != typeof(ToolStrip))
                                            {
                                                if (Controles7.GetType() == typeof(TextBox))
                                                {
                                                    if (((TextBox)Controles7).ReadOnly == false)
                                                    {
                                                        GravaObjetos(_conexao, _idFormulario, Controles7.Name, _legenda.GetToolTip(Controles7), _NroForm, _fForm.Name);
                                                    }
                                                }
                                                else
                                                {
                                                    GravaObjetos(_conexao, _idFormulario, Controles7.Name, _legenda.GetToolTip(Controles7), _NroForm, _fForm.Name);
                                                }
                                            }
                                            if (Controles7.GetType() == typeof(ToolStrip))
                                            {
                                                foreach (ToolStripItem ToolStrip in ((ToolStrip)Controles7).Items)
                                                {
                                                    if (ToolStrip.GetType() != typeof(ToolStripLabel))
                                                    {
                                                        if (ToolStrip.GetType() != typeof(ToolStripSeparator))
                                                        {
                                                            if (ToolStrip.GetType() != typeof(ToolStripTextBox))
                                                            {
                                                                GravaObjetos(_conexao, _idFormulario, ToolStrip.Name, ToolStrip.ToolTipText, _NroForm, _fForm.Name);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void GravaObjetos(String _conexao, Int32 _idFormulario, String _name, String _texto, Int32 _NroForm, String nomeform)
        {
            clsFormulariosObjetoBLL clsFormulariosObjetoBLL = new clsFormulariosObjetoBLL();
            clsFormulariosObjetoInfo clsFormulariosObjetoInfo = new clsFormulariosObjetoInfo();

            clsFormulariosObjetoInfo = clsFormulariosObjetoBLL.CarregarName(_conexao, _name, _idFormulario);
            if (clsFormulariosObjetoInfo == null)
            {
                clsFormulariosObjetoInfo = new clsFormulariosObjetoInfo();
            }
            clsFormulariosObjetoInfo.idformulario = _idFormulario;
            clsFormulariosObjetoInfo.nomeobjeto = _name;
            if (_name != "")
            {
                if (_texto == null || _texto == "")
                {
                    try
                    {
                        String s;
                        s = "Cód. Form.:" + _NroForm.ToString("00000000") + "| Nome Formulário:" + nomeform + " |Nome Objeto:" + _name + Environment.NewLine;
                        System.IO.File.AppendAllText(Application.StartupPath + "\\ToolTips.txt", s);
                        MessageBox.Show(s, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch
                    {

                    }
                    _texto = "";
                    // não consegui encerrar o programa aqui (carlos)
                }
            }
            if (_texto != "" && _texto.ToUpper() != " ")
            {
                clsFormulariosObjetoInfo.nome = _texto;
                if (_name.ToString().Length > 50)
                {
                    MessageBox.Show("Qtde de caracteres como nome do objeto maior que a base de dados " + _name + " ");
                }

                clsFormulariosObjetoInfo.ativo = "S";
                clsFormulariosObjetoInfo.verificarform = "S";
                if (clsFormulariosObjetoInfo.id == 0)
                {  // Incluir novo objeto
                    if (_texto == "")
                    {
                        clsFormulariosObjetoInfo.liberado = "N";
                    }
                    else
                    {
                        clsFormulariosObjetoInfo.liberado = "S";
                    }
                    clsFormulariosObjetoInfo.aparecelista = "S";

                    clsFormulariosObjetoBLL.Incluir(_conexao, clsFormulariosObjetoInfo);
                }
                else
                {
                    clsFormulariosObjetoBLL.Alterar(_conexao, clsFormulariosObjetoInfo);
                }
            }
        }

        private static MenuStrip menuStripAtual;
        private static ToolStripMenuItem toolStripMenuItemAtual;
        private static ToolStripButton toolStripButtonAtual;

        public static void CheckControl(Object ctl, Int32 idformsobjeto, Int32 idformulario, String _conexao)
        {
            clsFormsObjetoBLL FormsObjetoBLL = new clsFormsObjetoBLL();
            clsFormsObjetoInfo FormsObjetoInfo;

            String pName = "";
            String pText = "";
            Int32 pLeft = 0;
            Int32 pTop = 0;
            Boolean pVisible = true;
            Boolean pEnabled = true;
            Control.ControlCollection pControlCollection = null;

            if (ctl is MenuStrip)
            {
                Control controle = new Control();
                menuStripAtual = ((MenuStrip)ctl);

                if (menuStripAtual.AccessibleDescription == null ||
                    menuStripAtual.AccessibleDescription.Trim() == "")
                {
                    controle.Text = menuStripAtual.Text;
                }
                else
                {
                    controle.Text = menuStripAtual.AccessibleDescription;
                }

                controle.Name = menuStripAtual.Name;
                controle.Left = menuStripAtual.Left;
                controle.Top = menuStripAtual.Top;
                //controle.Visible = menuStripAtual.Visible;
                controle.Enabled = menuStripAtual.Enabled;

                pName = controle.Name;
                pText = controle.Text;
                pLeft = controle.Left;
                pTop = controle.Top;
                //pVisible = controle.Visible;
                pEnabled = controle.Enabled;
                pControlCollection = controle.Controls;
            }
            else if (ctl is ToolStripMenuItem)
            {
                Control controle = new Control();

                toolStripMenuItemAtual = ((ToolStripMenuItem)ctl);

                if (toolStripMenuItemAtual.AccessibleDescription == null ||
                    toolStripMenuItemAtual.AccessibleDescription.Trim() == "")
                {
                    controle.Text = toolStripMenuItemAtual.Text;
                }
                else
                {
                    controle.Text = toolStripMenuItemAtual.AccessibleDescription;
                }

                controle.Name = toolStripMenuItemAtual.Name;
                //controle.Visible = toolStripMenuItemAtual.Visible;
                controle.Enabled = toolStripMenuItemAtual.Enabled;

                pName = controle.Name;
                pText = controle.Text;
                pLeft = controle.Left;
                pTop = controle.Top;
                //pVisible = controle.Visible;
                pEnabled = controle.Enabled;
                pControlCollection = controle.Controls;
            }
            else if (ctl is ToolStripButton)
            {
                Control controle = new Control();

                toolStripButtonAtual = ((ToolStripButton)ctl);

                if (toolStripButtonAtual.AccessibleDescription == null ||
                    toolStripButtonAtual.AccessibleDescription.Trim() == "")
                {
                    controle.Text = toolStripButtonAtual.Text;
                }
                else
                {
                    controle.Text = toolStripButtonAtual.AccessibleDescription;
                }

                controle.Name = toolStripButtonAtual.Name;
                //controle.Visible = toolStripButtonAtual.Visible;
                controle.Enabled = toolStripButtonAtual.Enabled;

                pName = controle.Name;
                pText = controle.Text;
                pLeft = controle.Left;
                pTop = controle.Top;
                //pVisible = controle.Visible;
                pEnabled = controle.Enabled;
                pControlCollection = controle.Controls;
            }
            else if (ctl is Control)
            {
                Control controle = new Control();
                controle = (Control)ctl;

                if (controle.AccessibleDescription == null ||
                    controle.AccessibleDescription.Trim() == "")
                {
                    pText = controle.Text;
                }
                else
                {
                    pText = controle.AccessibleDescription;
                }

                pName = controle.Name;
                pLeft = controle.Left;
                pTop = controle.Top;
                //pVisible = controle.Visible;
                pEnabled = controle.Enabled;
                pControlCollection = controle.Controls;
            }
            else
            {
                return;
            }

            if (FormsObjetoBLL.JaExiste(pName, idformulario, _conexao) == true)
            {
                FormsObjetoInfo = FormsObjetoBLL.Carregar(pName, idformulario, _conexao);
            }
            else
            {
                FormsObjetoInfo = new clsFormsObjetoInfo();
                FormsObjetoInfo.datac = DateTime.Now;
                FormsObjetoInfo.lista = "S";
                FormsObjetoInfo.nomeobjeto = pName;
            }

            FormsObjetoInfo.idforms = idformulario;
            FormsObjetoInfo.idformsobjeto = idformsobjeto;
            FormsObjetoInfo.nome = pText;
            FormsObjetoInfo.tipo = ctl.GetType().ToString();
            FormsObjetoInfo.posleft = pLeft;
            FormsObjetoInfo.postop = pTop;
            FormsObjetoInfo.posdif = FormsObjetoInfo.posleft - FormsObjetoInfo.postop;

            if (pVisible == true)
            {
                FormsObjetoInfo.visivel = "S";
            }
            else
            {
                FormsObjetoInfo.visivel = "N";
            }

            if (pEnabled == true)
            {
                FormsObjetoInfo.ativo = "S";
            }
            else
            {
                FormsObjetoInfo.ativo = "N";
            }

            if (FormsObjetoInfo.id == 0)
            {
                FormsObjetoInfo.id = FormsObjetoBLL.Incluir(FormsObjetoInfo, _conexao);
            }
            else
            {
                FormsObjetoBLL.Alterar(FormsObjetoInfo, _conexao);
            }

            if (ctl is MenuStrip)
            {
                menuStripAtual = (MenuStrip)ctl;

                if (menuStripAtual.Items != null && menuStripAtual.Items.Count > 0)
                {
                    foreach (ToolStripItem item in menuStripAtual.Items)
                    {
                        CheckControl(item, FormsObjetoInfo.id, idformulario, _conexao);
                    }
                }
            }
            else if (ctl is ToolStripMenuItem)
            {
                toolStripMenuItemAtual = (ToolStripMenuItem)ctl;

                if (toolStripMenuItemAtual.DropDownItems != null && toolStripMenuItemAtual.DropDownItems.Count > 0)
                {
                    foreach (ToolStripItem item in toolStripMenuItemAtual.DropDownItems)
                    {
                        CheckControl(item, FormsObjetoInfo.id, idformulario, _conexao);
                    }
                }
            }
            else if (ctl is ToolStrip)
            {
                if (((ToolStrip)ctl).Items != null && ((ToolStrip)ctl).Items.Count > 0)
                {
                    foreach (ToolStripItem item in ((ToolStrip)ctl).Items)
                    {
                        CheckControl(item, FormsObjetoInfo.id, idformulario, _conexao);
                    }
                }
            }
            else if (ctl is Control && ((Control)ctl).Controls != null)
            {
                foreach (Control ctlIn in pControlCollection)
                {
                    CheckControl(ctlIn, FormsObjetoInfo.id, idformulario, _conexao);
                }
            }
        }
    }
}
