using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmIpi : Form
    {
        
        

        clsIpiBLL clsIpiBLL;
        clsIpiInfo clsIpiInfo;
        clsIpiInfo clsIpiInfoOld;

        DataGridViewRowCollection rows;
        
        Int32 id;
        Int32 idEstadoOrigem;

        DataTable dtEstadoOrigem;

        //estados
        clsEstadosBLL clsEstadosBLL;
        clsEstadosicmsBLL clsEstadosicmsBLL;

        // IpiEstadosIcms
        clsIpiEstadosicmsBLL clsIpiEstadosicmsBLL;
        clsIpiEstadosicmsInfo clsIpiEstadosicmsInfo;
        clsIpiEstadosicmsInfo clsIpiEstadosicmsInfoOld;
        
        Int32 ipiestadosicms_id;
        Int32 ipiestadosicms_posicao;
        Int32 ipiestadosicms_idestado;
        Int32 ipiestadosicms_idestadodestino;
        Int32 ipiestadosicms_iddizeresnfv;        

        DataTable dtIpiEstadosIcms;

        //GridColuna[] dtIpiEstadosicmsColunas;

        public frmIpi()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridViewRowCollection _rows)
        {
            this.id = _id;
            this.rows = _rows;

            clsIpiBLL = new clsIpiBLL();
            clsIpiEstadosicmsBLL = new clsIpiEstadosicmsBLL();

            clsEstadosBLL = new clsEstadosBLL();
            dtEstadoOrigem = clsEstadosBLL.GridDadosUF(clsInfo.conexaosqldados);
            clsEstadosBLL.GridMontaUF(dgvEstadosOrigem, dtEstadoOrigem, clsInfo.zempresa_ufid);
            clsGridHelper.SelecionaLinha(clsInfo.zempresa_ufid, dgvEstadosOrigem,"estado");
            
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select estado from estados order by estado", tbxEstados_ufdestino);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from dizeresnfv order by codigo", tbxDizeresNFV_Codigo);

        }

        private void frmIpi_Load(object sender, EventArgs e)
        {
            IpiCarregar();
        }

        private void frmIpi_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void IpiCarregar()
        {
            clsIpiInfoOld = new clsIpiInfo();

            if (id == 0)
            {
                clsIpiInfo = new clsIpiInfo();
                clsIpiInfo.aliqicm = 0;
                clsIpiInfo.aliqii = 0;
                clsIpiInfo.aliqipi = 0;
                clsIpiInfo.aliqmer = 0;
                clsIpiInfo.aliquota = 0;
                clsIpiInfo.ativo = "S";
                clsIpiInfo.codigo = "";
                clsIpiInfo.cofins = "N";
                clsIpiInfo.id = 0;
                clsIpiInfo.nome = "";
                clsIpiInfo.pispasep = "N";
                clsIpiInfo.tipo = "";
                //clsIpiInfo.unidade = "";
            }
            else
            {
                clsIpiInfo = clsIpiBLL.Carregar(id, clsInfo.conexaosqldados);
            }
            IpiCampos(clsIpiInfo);
            IpiFillInfo(clsIpiInfoOld);
            idEstadoOrigem = clsInfo.zempresa_ufid;

            CarregaIPIEstadosICMS(idEstadoOrigem);
          }

        private void CarregaIPIEstadosICMS(Int32 _idestadoorigem)
        {
            // carregando os estados
            dtIpiEstadosIcms = clsIpiEstadosicmsBLL.GridDados(_idestadoorigem, id, clsInfo.conexaosqldados);
            DataColumn dcPosicao = new DataColumn("ipiestadosicms_posicao", Type.GetType("System.Int32"));
            dtIpiEstadosIcms.Columns.Add(dcPosicao);
            for (Int32 i = 1; i <= dtIpiEstadosIcms.Rows.Count; i++)
            {
                dtIpiEstadosIcms.Rows[i - 1]["ipiestadosicms_posicao"] = i;
            }
            dtIpiEstadosIcms.AcceptChanges();
            clsIpiEstadosicmsBLL.GridMonta(dgvIpiEstadosIcms, dtIpiEstadosIcms, ipiestadosicms_posicao);
        
        }

        void IpiCampos(clsIpiInfo info)
        {
            id = info.id;
            
            tbxAliquotaICM.Text =  info.aliqicm.ToString("N2");
            tbxAliquotaII.Text = info.aliqii.ToString("N2");
            tbxAliquotaIPI.Text = info.aliqipi.ToString("N2");
            tbxAliquotaMercosul.Text = info.aliqmer.ToString("N2");
            tbxAliquota.Text = info.aliquota.ToString("N2");
            if (info.ativo == "S")
            {
                rbnAtivoS.Checked = true;
            }
            else
            {
                rbnAtivoN.Checked = true;
            }
            tbxCodigo.Text = info.codigo;
            if (info.cofins == "S")
            {
                ckxCofins.Checked = true;
            }
            else
            {
                ckxCofins.Checked = false;
            }
            ipiestadosicms_id = info.id;
            tbxNome.Text = info.nome;
            if (info.pispasep == "S")
            {
                ckxPisPasep.Checked = true;
            }
            else
            {
                ckxPisPasep.Checked = false;
            }
            if (info.tipo == "T")
            {
                rbnTipoR.Checked = true;
            }
            else
            {
                rbnTipoN.Checked = true;
            }
        }



        void IpiFillInfo(clsIpiInfo info)
        {
            info.id = id;

            info.aliqicm = clsParser.DecimalParse(tbxAliquotaICM.Text);
            info.aliqii = clsParser.DecimalParse(tbxAliquotaII.Text);
            info.aliqipi = clsParser.DecimalParse(tbxAliquotaIPI.Text);
            info.aliqmer = clsParser.DecimalParse(tbxAliquotaMercosul.Text);
            info.aliquota = clsParser.DecimalParse(tbxAliquota.Text);
            if (rbnAtivoS.Checked == true)
            {
                info.ativo = "S";
            }
            else
            {
                info.ativo = "N";
            }
            info.codigo = tbxCodigo.Text;
            if (ckxCofins.Checked == true)
            {
                info.cofins = "S";
            }
            else
            {
                info.cofins = "N";
            }
            // ipiestadosicms_id = info.id;
            info.nome = tbxNome.Text;
            if (ckxPisPasep.Checked == true)
            {
                info.pispasep = "S";
            }
            else
            {
                info.pispasep = "N";
            }
            if (rbnTipoR.Checked == true)
            {
                info.tipo = "T";
            }
            else
            {
                info.tipo = "S";
            }
        }
        private void tspGravar_Click(object sender, EventArgs e)
        {
            if (Salvar() == DialogResult.Cancel)
            {
                return;
            }
            this.Close();
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            if (HouveModificacoes() == true)
            {
                tspSalvarItem.PerformClick();
            }
            else
            {
                this.Close();
            }

        }
        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            TrataCampos((Control)sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ckxPisPasep_Click(object sender, EventArgs e)
        {
            if (ckxPisPasep.Checked == true)
            {
                ckxPisPasep.Text = "Sim Tributa PIS/PASEP";
            }
            else
            {
                ckxPisPasep.Text = "Não Tributa PIS/PASEP";
            }
        }

        private void ckxCofins_Click(object sender, EventArgs e)
        {
            if (ckxCofins.Checked == true)
            {
                ckxCofins.Text = "Sim Tributa COFINS";
            }
            else
            {
                ckxCofins.Text = "Não Tributa COFINS";
            }
        }

        private Boolean HouveModificacoes()
        {
            clsIpiInfo = new clsIpiInfo();
            IpiFillInfo(clsIpiInfo);
            if (clsIpiBLL.Equals(clsIpiInfo, clsIpiInfoOld) == false)
            {
                return true;
            }
            return false;
        }

        private DialogResult Salvar()
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja Salvar este Registro e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                IpiSalvar();
            }
            return drt;
        }

        private void IpiSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // Tabela: IPI
                clsIpiInfo = new clsIpiInfo();
                IpiFillInfo(clsIpiInfo);
                if (id == 0)
                {
                    clsIpiInfo.id = clsIpiBLL.Incluir(clsIpiInfo, clsInfo.conexaosqldados);
                }
                else
                {
                    clsIpiBLL.Alterar(clsIpiInfo, clsInfo.conexaosqldados);
                }
                id = clsIpiInfo.id;
                // 
                
                foreach (DataRow row in dtIpiEstadosIcms.Rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                        row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Unchanged)
                    {
                        row["idestado"] = idEstadoOrigem;
                    }
                }

                foreach (DataRow row in dtIpiEstadosIcms.Rows)
                {
                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        
                        clsIpiEstadosicmsBLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    { 
                        clsIpiEstadosicmsInfo = new clsIpiEstadosicmsInfo();
                        IpiEstadosIcmsGridToInfo(clsIpiEstadosicmsInfo, Int32.Parse(row["ipiestadosicms_posicao"].ToString()));
                        if (clsIpiEstadosicmsInfo.id == 0)
                        {
                            clsIpiEstadosicmsInfo.id = clsIpiEstadosicmsBLL.Incluir(clsIpiEstadosicmsInfo, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsIpiEstadosicmsBLL.Alterar(clsIpiEstadosicmsInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
                tse.Complete();
            }
        }
        
        private void tspAlterar_Click(object sender, EventArgs e)
        {
            if (dgvIpiEstadosIcms.CurrentRow != null)
            {
                ipiestadosicms_posicao = Int32.Parse(dgvIpiEstadosIcms.CurrentRow.Cells["ipiestadosicms_posicao"].Value.ToString());
                IpiEstadosIcmsCarregar();
            }
        }       

        void IpiEstadosIcmsCarregar()
        {
            tclIpiEstadosIcms.SelectedIndex = 1;
            gbxIPIEstado.Visible = true;
            gbxEstadosIcms.Enabled = false;
            tspIpi.Enabled = false;

            clsIpiEstadosicmsInfo = new clsIpiEstadosicmsInfo();
            clsIpiEstadosicmsInfoOld = new clsIpiEstadosicmsInfo();

            if (ipiestadosicms_posicao == 0)
            {
                ipiestadosicms_posicao = dtIpiEstadosIcms.Rows.Count + 1;
            }
            else
            {
                IpiEstadosIcmsGridToInfo(clsIpiEstadosicmsInfo, ipiestadosicms_posicao);
            }

            IpiEstadosIcmsCampos(clsIpiEstadosicmsInfo);
            IpiEstadosIcmsFillInfo(clsIpiEstadosicmsInfoOld);

        }

        void IpiEstadosIcmsGridToInfo(clsIpiEstadosicmsInfo info, Int32 posicao)
        {
            DataRow row = dtIpiEstadosIcms.Select("ipiestadosicms_posicao = " + posicao)[0];

            info.id = Int32.Parse(row["id"].ToString());
            info.idipi = id;
            info.idestado = idEstadoOrigem;
            info.idestadodestino = clsParser.Int32Parse(row["idestadodestino"].ToString());

            info.aliquota = clsParser.DecimalParse(row["aliquota"].ToString());
            info.reducao= clsParser.DecimalParse(row["reducao"].ToString());
            
            info.iva = clsParser.DecimalParse(row["iva"].ToString());
            info.reducaoiva = clsParser.DecimalParse(row["reducaoiva"].ToString());

            info.iddizeresnfv = clsParser.Int32Parse(row["iddizeresnfv"].ToString());
        }

        void IpiEstadosIcmsCampos(clsIpiEstadosicmsInfo info)
        {
            ipiestadosicms_id = info.id;
            ipiestadosicms_idestado = info.idestado;

            tbxIpiEstadosIcms_aliquota.Text = info.aliquota.ToString("N2");
            tbxIpiEstadosIcms_reduzida.Text = info.reducao.ToString("N2");              

            tbxIpiEstadosIcms_iva.Text =  info.iva.ToString("N2");
            tbxIpiEstadosIcms_ivareduzida.Text = info.reducaoiva.ToString("N2");
            
            // Estado Destino
            ipiestadosicms_idestadodestino = info.idestadodestino;
            tbxEstados_ufdestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ESTADO from ESTADOS where ID=" + ipiestadosicms_idestadodestino + "");
            tbxEstados_nomeext.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOMEEXT from ESTADOS where ID=" + ipiestadosicms_idestadodestino + "");
            tbxEstados_capital.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CAPITAL from ESTADOS where ID=" + ipiestadosicms_idestadodestino + "");
            
            // Id Dizeres da Nota Fiscal
            ipiestadosicms_iddizeresnfv = info.iddizeresnfv;
            if (ipiestadosicms_iddizeresnfv == 0)
            {
                ipiestadosicms_iddizeresnfv = clsInfo.zdizeresnf;
            }
            tbxDizeresNFV_Codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from DIZERESNFV where ID=" + ipiestadosicms_iddizeresnfv + "");
            tbxEstados_ufdestino.Select();
        }

        void IpiEstadosIcmsFillInfo(clsIpiEstadosicmsInfo info)
        {
            info.id = ipiestadosicms_id;
            info.idipi = id;
            info.idestado = ipiestadosicms_idestado;
            info.idestadodestino = ipiestadosicms_idestadodestino;

            info.aliquota = clsParser.DecimalParse(tbxIpiEstadosIcms_aliquota.Text);
            info.reducao = clsParser.DecimalParse(tbxIpiEstadosIcms_reduzida.Text);

            info.iva = clsParser.DecimalParse(tbxIpiEstadosIcms_iva.Text);
            info.reducaoiva = clsParser.DecimalParse(tbxIpiEstadosIcms_ivareduzida.Text);

            info.iddizeresnfv = ipiestadosicms_iddizeresnfv;          
           
        }

        void IpiEstadosIcmsFillInfoToGrid(clsIpiEstadosicmsInfo info)
        {
            DataRow row;
            DataRow[] rows = dtIpiEstadosIcms.Select("ipiestadosicms_posicao = " + ipiestadosicms_posicao);
            if (rows.Length > 0)
            {
                row = rows[0];
            }
            else
            {
                row = dtIpiEstadosIcms.NewRow();
            }
            row["id"] = info.id;
            
            row["idestado"] = info.idestado;
            row["idestadodestino"] = info.idestadodestino;

            row["aliquota"] = info.aliquota.ToString("N2");
            row["reducao"] = info.reducao.ToString("N2");
           
            row["iva"] = info.iva.ToString("N2");
            row["reducaoiva"] = info.reducaoiva.ToString("N2");

            row["destino"] = tbxEstados_ufdestino.Text;            

            row["iddizeresnfv"] = info.iddizeresnfv; 
            row["CODIGO"] = tbxDizeresNFV_Codigo.Text;
            row["NOME"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                     "select nome from DIZERESNFV where id = " + info.iddizeresnfv, "");

            if (rows.Length == 0)
            {
                row["ipiestadosicms_posicao"] = ipiestadosicms_posicao;
                dtIpiEstadosIcms.Rows.Add(row);
            }
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja salvar e alterar?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (drt == DialogResult.Yes)
            {
                clsIpiEstadosicmsInfo = new clsIpiEstadosicmsInfo();
                IpiEstadosIcmsFillInfo(clsIpiEstadosicmsInfo);
                IpiEstadosIcmsFillInfoToGrid(clsIpiEstadosicmsInfo);
            }
            else if (drt == DialogResult.No)
            {
                this.Close();
            }
            else if (drt == DialogResult.Cancel)
            {
                return;
            }
            tclIpiEstadosIcms.SelectedIndex = 0;
            gbxIPIEstado.Visible = false;
            gbxEstadosIcms.Enabled = true;
            tspIpi.Enabled = true;


        }
        
        private void tspRetornarItem_Click(object sender, EventArgs e)
        {
            tclIpiEstadosIcms.SelectedIndex = 0;
            gbxIPIEstado.Visible = false;
            gbxEstadosIcms.Enabled = true;
            tspIpi.Enabled = true;
        }

        private void dgvIpiEstadosIcms_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tspAlterar.PerformClick();
        }

        private void btnIdEstados_ufDestino_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdEstados_ufDestino.Name;
            frmEstadosPes frmEstadosPes = new frmEstadosPes();
            frmEstadosPes.Init(ipiestadosicms_idestadodestino);

            clsFormHelper.AbrirForm(this.MdiParent, frmEstadosPes, clsInfo.conexaosqldados);
        }

        private void btnIdDizeresNFV_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdDizeresNFV.Name;
            frmDizeresNFVVis frmDizeresNFVVis = new frmDizeresNFVVis();
            frmDizeresNFVVis.Init(ipiestadosicms_iddizeresnfv);

            clsFormHelper.AbrirForm(this.MdiParent, frmDizeresNFVVis, clsInfo.conexaosqldados);
        }

        private void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null &&
                clsInfo.zrow.Index != -1)
            {                
                if (clsInfo.znomegrid == btnIdEstados_ufDestino.Name)
                {
                    ipiestadosicms_idestadodestino = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxEstados_ufdestino.Text = clsInfo.zrow.Cells["ESTADO"].Value.ToString();
                    tbxEstados_capital.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CAPITAL from ESTADOS where id = " + ipiestadosicms_idestadodestino + " ");
                    tbxEstados_nomeext.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOMEEXT from ESTADOS where id = " + ipiestadosicms_idestadodestino + " ");

                    tbxEstados_ufdestino.Select();
                    tbxEstados_ufdestino.SelectAll();
                }
                if (clsInfo.znomegrid == btnIdDizeresNFV.Name)
                {
                    ipiestadosicms_iddizeresnfv = clsParser.Int32Parse(clsInfo.zrow.Cells[0].Value.ToString());
                    tbxDizeresNFV_Codigo.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxDizeresNFV_Codigo.Select();
                    tbxDizeresNFV_Codigo.SelectAll();
                }

            }            
            if (ctl.Name == tbxEstados_ufdestino.Name)
            { // Verificar se já não existe codigo similar

                ipiestadosicms_idestadodestino = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from ESTADOS where ESTADO='" + tbxEstados_ufdestino.Text + "'"));
                if (ipiestadosicms_idestadodestino == 0)
                {
                    ipiestadosicms_idestadodestino = clsInfo.zempresa_ufid;
                }
                tbxEstados_ufdestino.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ESTADO from ESTADOS where id = " + ipiestadosicms_idestadodestino + " ");
                tbxEstados_capital.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CAPITAL from ESTADOS where id = " + ipiestadosicms_idestadodestino + " ");
                tbxEstados_nomeext.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select NOMEEXT from ESTADOS where id = " + ipiestadosicms_idestadodestino + " ");

            }
            if (ctl.Name == tbxDizeresNFV_Codigo.Name)
            { // Verificar se já não existe codigo similar

                ipiestadosicms_iddizeresnfv = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from DIZERESNFV where CODIGO='" + tbxDizeresNFV_Codigo.Text + "'"));
                if (ipiestadosicms_idestadodestino == 0)
                {
                    ipiestadosicms_iddizeresnfv = clsInfo.zempresa_ufid;
                }
                tbxDizeresNFV_Codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select CODIGO from DIZERESNFV where id = " + ipiestadosicms_iddizeresnfv + " ");
            }
            tbxIpiEstadosIcms_aliquota.Text = clsParser.DecimalParse(tbxIpiEstadosIcms_aliquota.Text).ToString("N2");
            tbxIpiEstadosIcms_reduzida.Text = clsParser.DecimalParse(tbxIpiEstadosIcms_reduzida.Text).ToString("N2");
            tbxIpiEstadosIcms_iva.Text = clsParser.DecimalParse(tbxIpiEstadosIcms_iva.Text).ToString("N2");
            tbxIpiEstadosIcms_ivareduzida.Text = clsParser.DecimalParse(tbxIpiEstadosIcms_ivareduzida.Text).ToString("N2");
        }

        private void dgvEstadosOrigem_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ListaDestino();  
        }

        private void dgvEstadosOrigem_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ListaDestino();   
        }

        private void ListaDestino()
        {
            if (dgvEstadosOrigem.CurrentRow != null)
            {
                if  (clsParser.Int32Parse(dgvEstadosOrigem.CurrentRow.Cells["ID"].Value.ToString()) != idEstadoOrigem
                    && VerificaAlteracaoGrid())
                {
                    MessageBox.Show("Necessário salvar os registros " +
                        Environment.NewLine + " de aliquotas dos estados de destino, " +
                        Environment.NewLine + "referente a origem anterior."
                        , Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SalvaIpiestadosICMS();
                    idEstadoOrigem = clsParser.Int32Parse(dgvEstadosOrigem.CurrentRow.Cells["ID"].Value.ToString());
                }

                idEstadoOrigem = clsParser.Int32Parse(dgvEstadosOrigem.CurrentRow.Cells["ID"].Value.ToString());
                CarregaIPIEstadosICMS (clsParser.Int32Parse(dgvEstadosOrigem.CurrentRow.Cells["ID"].Value.ToString()));
                //verifico se existe dados se não tem adiciona dados padroes
                if (dgvIpiEstadosIcms.Rows.Count <= 0)
                {
                    DialogResult drt;
                    drt = MessageBox.Show("Não existe aliquotas de estados de destino " +
                        Environment.NewLine + "para este estado de origem! " +
                         Environment.NewLine + "Criando automaticamente dados basicos ",
                         Application.CompanyName, MessageBoxButtons.YesNoCancel);

                    if (drt == DialogResult.Yes)
                    {
                        //dtIpiEstadosIcms.Rows.Add(
                        clsEstadosicmsBLL = new clsEstadosicmsBLL();
                        DataTable tabelaaux;
                        tabelaaux = clsEstadosicmsBLL.GridDadosDestino(idEstadoOrigem, clsInfo.conexaosqldados);

                        DataRow drTabelaaux2;
                        ipiestadosicms_posicao = 0;
                        foreach (DataRow dgvrTabela in tabelaaux.Rows)
                        {
                            drTabelaaux2 = dtIpiEstadosIcms.NewRow();
                            drTabelaaux2["id"] = 0;
                            drTabelaaux2["idestado"] = idEstadoOrigem;
                            drTabelaaux2["idestadodestino"] = dgvrTabela["idestadodestino"];
                            drTabelaaux2["origem"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                                     "select estado from estados where id = " + idEstadoOrigem, "");

                            drTabelaaux2["destino"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                                     "select estado from estados where id = "
                                                     + clsParser.Int32Parse(dgvrTabela["idestadodestino"].ToString()), "");

                            drTabelaaux2["aliquota"] = dgvrTabela["aliquota"];
                            drTabelaaux2["reducao"] = "0";
                            drTabelaaux2["iva"] = "0";
                            drTabelaaux2["reducaoiva"] = "0";

                            drTabelaaux2["iddizeresnfv"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                                     "select codigo from DIZERESNFV where id = "
                                                     + clsInfo.zdizeresnf, "");

                            drTabelaaux2["CODIGO"] = tbxDizeresNFV_Codigo.Text;
                            drTabelaaux2["NOME"] = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                                                      "select nome from DIZERESNFV where id = "
                                                      + clsInfo.zdizeresnf, "");

                            ipiestadosicms_posicao += 1;
                            drTabelaaux2["ipiestadosicms_posicao"] = ipiestadosicms_posicao;
                            dtIpiEstadosIcms.Rows.Add(drTabelaaux2);
                        }
                    }                   
                }
            }
        }

        private  Boolean VerificaAlteracaoGrid()
        {
            Boolean ok = false;
            foreach (DataRow row in dtIpiEstadosIcms.Rows)
            {
                if (row.RowState == DataRowState.Added ||
                    row.RowState == DataRowState.Deleted ||
                    row.RowState == DataRowState.Modified)                           
                {
                    ok = true;
                    break;
                }
            }
            return ok;
        }

        private void SalvaIpiestadosICMS()
        {
            using (TransactionScope tse = new TransactionScope())
            {  
                foreach (DataRow row in dtIpiEstadosIcms.Rows)
                {
                    if (row.RowState != DataRowState.Deleted &&
                        row.RowState != DataRowState.Detached &&
                        row.RowState != DataRowState.Unchanged)
                    {
                        row["idestado"] = idEstadoOrigem;
                    }
                }

                foreach (DataRow row in dtIpiEstadosIcms.Rows)
                {
                    if (row.RowState == DataRowState.Detached ||
                        row.RowState == DataRowState.Unchanged)
                    {
                        continue;
                    }
                    else if (row.RowState == DataRowState.Deleted)
                    {
                        clsIpiEstadosicmsBLL.Excluir(Int32.Parse(row["id", DataRowVersion.Original].ToString()), clsInfo.conexaosqldados);
                        continue;
                    }
                    else
                    {
                        clsIpiEstadosicmsInfo = new clsIpiEstadosicmsInfo();
                        IpiEstadosIcmsGridToInfo(clsIpiEstadosicmsInfo, Int32.Parse(row["ipiestadosicms_posicao"].ToString()));
                        if (clsIpiEstadosicmsInfo.id == 0)
                        {
                            clsIpiEstadosicmsInfo.id = clsIpiEstadosicmsBLL.Incluir(clsIpiEstadosicmsInfo, clsInfo.conexaosqldados);
                        }
                        else
                        {
                            clsIpiEstadosicmsBLL.Alterar(clsIpiEstadosicmsInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
                tse.Complete();
            }
        }          
    }
}
