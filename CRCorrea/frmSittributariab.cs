using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{ 
    public partial class frmSittributariab : Form
    {
        Int32 id;
        clsSittributariabBLL clsSittributariabBLL;
        clsSittributariabInfo clsSittributariabInfo;
        clsSittributariabInfo clsSittributariabInfoOld;
        private GroupBox groupBox4;
        private Label label11;
        private Label label12;
        private TextBox tbxNome;
        private TextBox tbxCodigo;
        private ComboBox cbxReferencia;
        private ToolStrip tspTool;
        private ToolStripButton tspSalvar;
        private ToolStripButton tspPrimeiro;
        private ToolStripButton tspAnterior;
        private ToolStripButton tspProximo;
        private ToolStripButton tspUltimo;
        private ToolStripButton tspRetornar;
        DataGridViewRowCollection rows;
        

        public frmSittributariab()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridViewRowCollection _rows)
        {
            id = _id;
            rows = _rows;
            clsVisual.FillComboBox(cbxReferencia,
                "select codigo + '-' + nome from regimetributario ",
                clsInfo.conexaosqldados);

            
            clsSittributariabBLL = new clsSittributariabBLL();
        }

        private void frmSittributariab_Load(object sender, EventArgs e)
        {
            try
            {
                CarregaSittributariab();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Message: " + ex.Message + "/nStack Stace: " + ex.StackTrace, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void CarregaSittributariab()
        {
            try
            {
                SittributariabCarregar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Message: " + ex.Message + "/nStack Stace: " + ex.StackTrace, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SittributariabCarregar()
        {
            clsSittributariabInfoOld = new clsSittributariabInfo();
            if (id == 0)
            {
                //preenchimento com dados padrões
                clsSittributariabInfo = new clsSittributariabInfo();
                clsSittributariabInfo.codigo = "";
                clsSittributariabInfo.nome = "";
                clsSittributariabInfo.idreferencia = 0;       
            }
            else
            {
                //preenchimento com dados da base de dados
                clsSittributariabInfo = clsSittributariabBLL.Carregar(id,
                    clsInfo.conexaosqldados);
            }
            //preenchimento dos campos da formulario
            PreencheCamposSittributariab(clsSittributariabInfo);
            //preenchimento da info
            PreencheInfoSittributariab(clsSittributariabInfoOld);

        }

        private void PreencheCamposSittributariab(clsSittributariabInfo _info)
        {
            id = _info.id;
            tbxCodigo.Text = _info.codigo;
            tbxNome.Text = _info.nome;
            cbxReferencia.SelectedIndex = SelecionarIndex(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, 
                "select codigo from regimetributario where id = " + _info.idreferencia, "0"), 1, cbxReferencia);
        }

        Int32 SelecionarIndex(String valor, Int32 nLetras, ComboBox c)
        {
            String s = "";
            Int32 index = 0;
            Int32 resultado = 0;

            foreach (Object obj in c.Items)
            {
                s = obj.ToString();

                if (s.Substring(0, nLetras) == valor)
                {
                    resultado = index;
                    break;
                }

                index++;
            }

            return resultado;
        }

        private void PreencheInfoSittributariab(clsSittributariabInfo _info)
        {
            _info.id = id;
            _info.codigo = tbxCodigo.Text;
            _info.nome = tbxNome.Text;
            _info.idreferencia = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select id from regimetributario where codigo = '" +
                cbxReferencia.Text.Substring(0, 1) + "'", "0"));
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
        }

        private Boolean HouveModificacoes()
        {
            clsSittributariabInfo = new clsSittributariabInfo();
            PreencheInfoSittributariab(clsSittributariabInfo);
            if (clsSittributariabBLL.Equals(clsSittributariabInfo, clsSittributariabInfoOld) == false)
            {
                return true;
            }
            return false;
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {

        }

        private DialogResult Salvar()
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja Salvar este Registro e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                SittributariabSalvar();
            }
            return drt;
        }

        private void SittributariabSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: Frota
                clsSittributariabInfo = new clsSittributariabInfo();
                PreencheInfoSittributariab(clsSittributariabInfo);

                if (id == 0)
                {
                    clsSittributariabInfo.id = clsSittributariabBLL.Incluir(clsSittributariabInfo, clsInfo.conexaosqldados);
                    frmSittributariabVis.id = clsSittributariabInfo.id;
                }
                else
                {
                    clsSittributariabBLL.Alterar(clsSittributariabInfo, clsInfo.conexaosqldados);
                }
                id = clsSittributariabInfo.id;
                tse.Complete();
            }
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);           
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {

        }

        private void tspAnterior_Click(object sender, EventArgs e)
        {

        }

        private void tspProximo_Click(object sender, EventArgs e)
        {

        }

        private void tspUltimo_Click(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSittributariab));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbxNome = new System.Windows.Forms.TextBox();
            this.tbxCodigo = new System.Windows.Forms.TextBox();
            this.cbxReferencia = new System.Windows.Forms.ComboBox();
            this.tspTool = new System.Windows.Forms.ToolStrip();
            this.tspSalvar = new System.Windows.Forms.ToolStripButton();
            this.tspPrimeiro = new System.Windows.Forms.ToolStripButton();
            this.tspAnterior = new System.Windows.Forms.ToolStripButton();
            this.tspProximo = new System.Windows.Forms.ToolStripButton();
            this.tspUltimo = new System.Windows.Forms.ToolStripButton();
            this.tspRetornar = new System.Windows.Forms.ToolStripButton();
            this.groupBox4.SuspendLayout();
            this.tspTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.AccessibleDescription = "Dados";
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.tbxNome);
            this.groupBox4.Controls.Add(this.tbxCodigo);
            this.groupBox4.Enabled = false;
            this.groupBox4.Location = new System.Drawing.Point(121, 113);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(531, 62);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(56, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(140, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Breve Descrição do Código:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Código:";
            // 
            // tbxNome
            // 
            this.tbxNome.AccessibleDescription = "Descrição";
            this.tbxNome.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxNome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNome.Location = new System.Drawing.Point(59, 34);
            this.tbxNome.MaxLength = 25;
            this.tbxNome.Name = "tbxNome";
            this.tbxNome.Size = new System.Drawing.Size(464, 21);
            this.tbxNome.TabIndex = 1;
            // 
            // tbxCodigo
            // 
            this.tbxCodigo.AccessibleDescription = "Código";
            this.tbxCodigo.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxCodigo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigo.Location = new System.Drawing.Point(6, 34);
            this.tbxCodigo.MaxLength = 2;
            this.tbxCodigo.Name = "tbxCodigo";
            this.tbxCodigo.Size = new System.Drawing.Size(47, 21);
            this.tbxCodigo.TabIndex = 0;
            // 
            // cbxReferencia
            // 
            this.cbxReferencia.AccessibleDescription = "UF";
            this.cbxReferencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxReferencia.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxReferencia.FormattingEnabled = true;
            this.cbxReferencia.Location = new System.Drawing.Point(368, 52);
            this.cbxReferencia.Name = "cbxReferencia";
            this.cbxReferencia.Size = new System.Drawing.Size(52, 21);
            this.cbxReferencia.TabIndex = 15;
            // 
            // tspTool
            // 
            this.tspTool.AccessibleDescription = "Barra de Opções";
            this.tspTool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tspTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tspTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tspTool.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspTool.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tspTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspSalvar,
            this.tspPrimeiro,
            this.tspAnterior,
            this.tspProximo,
            this.tspUltimo,
            this.tspRetornar});
            this.tspTool.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspTool.Location = new System.Drawing.Point(127, 178);
            this.tspTool.Name = "tspTool";
            this.tspTool.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tspTool.Size = new System.Drawing.Size(397, 45);
            this.tspTool.TabIndex = 16;
            this.tspTool.TabStop = true;
            this.tspTool.Text = "toolStrip1";
            // 
            // tspSalvar
            // 
            this.tspSalvar.AccessibleDescription = "Salvar";
            this.tspSalvar.AutoSize = false;
            this.tspSalvar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspSalvar.Image = ((System.Drawing.Image)(resources.GetObject("tspSalvar.Image")));
            this.tspSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSalvar.Name = "tspSalvar";
            this.tspSalvar.Size = new System.Drawing.Size(66, 42);
            this.tspSalvar.Text = "&Salvar";
            this.tspSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tspPrimeiro
            // 
            this.tspPrimeiro.AccessibleDescription = "Primeiro";
            this.tspPrimeiro.AutoSize = false;
            this.tspPrimeiro.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspPrimeiro.Image = ((System.Drawing.Image)(resources.GetObject("tspPrimeiro.Image")));
            this.tspPrimeiro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspPrimeiro.Name = "tspPrimeiro";
            this.tspPrimeiro.Size = new System.Drawing.Size(66, 42);
            this.tspPrimeiro.Text = "Pr&imeiro";
            this.tspPrimeiro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tspAnterior
            // 
            this.tspAnterior.AccessibleDescription = "Anterior";
            this.tspAnterior.AutoSize = false;
            this.tspAnterior.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspAnterior.Image = ((System.Drawing.Image)(resources.GetObject("tspAnterior.Image")));
            this.tspAnterior.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAnterior.Name = "tspAnterior";
            this.tspAnterior.Size = new System.Drawing.Size(66, 42);
            this.tspAnterior.Text = "&Anterior";
            this.tspAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tspProximo
            // 
            this.tspProximo.AccessibleDescription = "Próximo";
            this.tspProximo.AutoSize = false;
            this.tspProximo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspProximo.Image = ((System.Drawing.Image)(resources.GetObject("tspProximo.Image")));
            this.tspProximo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspProximo.Name = "tspProximo";
            this.tspProximo.Size = new System.Drawing.Size(66, 42);
            this.tspProximo.Text = "Pró&ximo";
            this.tspProximo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tspUltimo
            // 
            this.tspUltimo.AccessibleDescription = "Último";
            this.tspUltimo.AutoSize = false;
            this.tspUltimo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspUltimo.Image = ((System.Drawing.Image)(resources.GetObject("tspUltimo.Image")));
            this.tspUltimo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspUltimo.Name = "tspUltimo";
            this.tspUltimo.Size = new System.Drawing.Size(66, 42);
            this.tspUltimo.Text = "&Último";
            this.tspUltimo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tspRetornar
            // 
            this.tspRetornar.AccessibleDescription = "Retornar";
            this.tspRetornar.AutoSize = false;
            this.tspRetornar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspRetornar.Image = ((System.Drawing.Image)(resources.GetObject("tspRetornar.Image")));
            this.tspRetornar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspRetornar.Name = "tspRetornar";
            this.tspRetornar.Size = new System.Drawing.Size(66, 42);
            this.tspRetornar.Text = "Retorna&r";
            this.tspRetornar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // frmSittributariab
            // 
            this.ClientSize = new System.Drawing.Size(772, 288);
            this.Controls.Add(this.tspTool);
            this.Controls.Add(this.cbxReferencia);
            this.Controls.Add(this.groupBox4);
            this.Name = "frmSittributariab";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tspTool.ResumeLayout(false);
            this.tspTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
