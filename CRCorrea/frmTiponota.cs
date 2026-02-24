using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmTiponota : Form
    {
        Int32 id;
        Int32 idcfopok;
        Int32 idcfopent;
        Int32 idcfopdev;
        Int32 idcfopdef;
        Int32 idcfopmor;
        Int32 idcfopbx;
        Int32 idcfopbxman;
        Int32 iddizeres;
        Int32 iddizeres2;
        //Int32 idcsticms;
        //Int32 idcstipi;
        //Int32 idcstpis;
        //Int32 idcstcofins;

        Int32 idcliente;
        //Int32 idicms_orig;

        DataGridViewRowCollection rows;

        clsTiponotaBLL clsTiponotaBLL;
        clsTiponotaInfo clsTiponotaInfo;
        clsTiponotaInfo clsTiponotaInfoOld;

        DataTable dtClientes;

        // Para Importar arquivos Texto
        SqlConnection scn;

        String[] str;
        Int32 nRegistros;
        private Int32 qtcaracter;
        private Int32 posicaoatual;
        private Int32 posicaoinicial;
        private Int32 posicaofinal;
        private Boolean ok;
        String campo;

        // Situação Tributaria Cofins
        clsSittribcofinsInfo clsSittribcofinsInfo = new clsSittribcofinsInfo();
        clsSittribcofinsBLL clsSittribcofinsBLL = new clsSittribcofinsBLL();
        // Situação Tributaria Pis
        clsSittribpisInfo clsSittribpisInfo = new clsSittribpisInfo();
        clsSittribpisBLL clsSittribpisBLL = new clsSittribpisBLL();
        // Situação Tributaria IPI
        clsSitTribIpiInfo clsSitTribIpiInfo = new clsSitTribIpiInfo();
        clsSittribipiBLL clsSittribipiBLL = new clsSittribipiBLL();
        // ISSQN IND
        clsTab_Danfe_IndISSInfo clsTab_Danfe_IndISSInfo = new clsTab_Danfe_IndISSInfo();
        clsTab_Danfe_IndISSBLL clsTab_Danfe_IndISSBLL = new clsTab_Danfe_IndISSBLL();
        // ICMS_CST
        clsSittributariabInfo clsSittributariabInfo = new clsSittributariabInfo();
        clsSittributariabBLL clsSittributariabBLL = new clsSittributariabBLL();
        // ICMS_ORIG
        clsSittributariaaInfo clsSittributariaaInfo = new clsSittributariaaInfo();
        clsSittributariaaBLL clsSittributariaaBLL = new clsSittributariaaBLL();
        // ICMS_MODBC
        clsTab_Danfe_ModBcInfo clsTab_Danfe_ModBcInfo = new clsTab_Danfe_ModBcInfo();
        clsTab_Danfe_ModBcBLL clsTab_Danfe_ModBcBLL = new clsTab_Danfe_ModBcBLL();
        // ICMS_MODBCST
        clsTab_Danfe_ModBcStInfo clsTab_Danfe_ModBcStInfo = new clsTab_Danfe_ModBcStInfo();
        clsTab_Danfe_ModBcStBLL clsTab_Danfe_ModBcStBLL = new clsTab_Danfe_ModBcStBLL();
        // ICMS_MOTDESICMS
        clsTab_Danfe_MotDesIcmsInfo clsTab_Danfe_MotDesIcmsInfo = new clsTab_Danfe_MotDesIcmsInfo();
        clsTab_Danfe_MotDesIcmsBLL clsTab_Danfe_MotDesIcmsBLL = new clsTab_Danfe_MotDesIcmsBLL();

        public frmTiponota()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id, DataGridViewRowCollection _rows)
        {
            clsTiponotaBLL = new clsTiponotaBLL();
            clsSittribcofinsBLL = new clsSittribcofinsBLL();
            clsSittribpisInfo clsSittribpisInfo = new clsSittribpisInfo();
            clsSittribipiBLL = new clsSittribipiBLL();
            clsTab_Danfe_IndISSBLL = new clsTab_Danfe_IndISSBLL();
            clsSittributariabBLL = new clsSittributariabBLL();
            clsSittributariaaBLL = new clsSittributariaaBLL();
            clsTab_Danfe_ModBcBLL = new clsTab_Danfe_ModBcBLL();
            clsTab_Danfe_ModBcStBLL = new clsTab_Danfe_ModBcStBLL();
            clsTab_Danfe_MotDesIcmsBLL = new clsTab_Danfe_MotDesIcmsBLL();
            id = _id;
            rows = _rows;

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "CFOP", "CFOP", " len(cfop) = 4 and len(nomenota) > 0 ", tbxCfopok);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "TIPONOTA", "CODIGO", "", tbxCfopent);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "TIPONOTA", "CODIGO", "", tbxCfopdev);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "TIPONOTA", "CODIGO", "", tbxCfopdef);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "TIPONOTA", "CODIGO", "", tbxCfopmor);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "TIPONOTA", "CODIGO", "", tbxCfopbx);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "TIPONOTA", "CODIGO", "", tbxCfopbxman);

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "DIZERESNFV", "CODIGO", "", tbxDizeresnfv_codigo);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "DIZERESNFV", "CODIGO", "", tbxDizeresnfv_codigo_2);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "CLIENTE", "NOME", "", tbxCliente);

            Int32 idRegimeTributario = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select IDREGIMETRIBUTARIO FROM EMPRESAgere where empresa='" + clsInfo.zempresaid + "' ", "1"));
            clsVisual.FillComboBox(cbxIcmscst, "select codigo + ' - ' + nome from sittributariab " +
                                            " where idreferencia = " + idRegimeTributario + " order by codigo ", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(cbxIpicst, "select codigo + ' - ' + nome from sittribipi order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(cbxPiscst, "select codigo + ' - ' + nome from sittribpis order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(cbxCofinscst, "select codigo + ' - ' + nome from sittribcofins order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(cbxISSQN_cindISS, "select cast(codigo as char(1)) + ' - ' + nome from tab_danfe_indiss order by codigo", clsInfo.conexaosqldados);

            clsVisual.FillComboBox(cbxICMS_Orig, "select codigo + ' - ' + nome from sittributariaa order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(cbxICMS_ModBC, "select cast(codigo as char(1)) + ' - ' + nome from TAB_DANFE_MODBC order by codigo", clsInfo.conexaosqldados);
            clsVisual.FillComboBox(cbxICMS_ModBCST, "select cast(codigo as char(1)) + ' - ' + nome from TAB_DANFE_MODBCST order by codigo", clsInfo.conexaosqldados);

            clsVisual.FillComboBox(cbxIcms_MotDesIcms, "select codigo  + ' - ' + nome from TAB_DANFE_MOTDESICMS order by codigo", clsInfo.conexaosqldados);

        }

        private void frmTiponota_Load(object sender, EventArgs e)
        {
            TiponotaCarregar();
        }

        void TiponotaCarregar()
        {
            if (id == 0)
            {
                clsTiponotaInfo = new clsTiponotaInfo();
                clsTiponotaInfo.codigo = "";
                clsTiponotaInfo.nome = "";
                clsTiponotaInfo.devolucao = "N";
                clsTiponotaInfo.fatura = "N";
                clsTiponotaInfo.iddizeres = clsInfo.zdizeresnf;
                clsTiponotaInfo.iddizeres2 = clsInfo.zdizeresnf;
                clsTiponotaInfo.movimentacao = "1";
                clsTiponotaInfo.idcfopok = clsInfo.zcfop;
                clsTiponotaInfo.idcfopent = clsInfo.zcfop;

                clsTiponotaInfo.idcfopdev = clsInfo.zcfop;
                clsTiponotaInfo.idcfopdef =  clsInfo.zcfop;
                clsTiponotaInfo.idcfopmor = clsInfo.zcfop;
                clsTiponotaInfo.idcfopbx = clsInfo.zcfop;
                clsTiponotaInfo.idcfopbxman = clsInfo.zcfop;

                clsTiponotaInfo.idcsticms = clsInfo.zsituacaotribb;
                clsTiponotaInfo.idicms_orig = clsInfo.zsituacaotriba;
                clsTiponotaInfo.idicms_modbc = 0;
                clsTiponotaInfo.idicms_modbcst = 0;
                clsTiponotaInfo.idicms_motdesicms = 0;

                clsTiponotaInfo.idcstipi = clsInfo.zsittribipi; 
                clsTiponotaInfo.idcstpis = clsInfo.zsittribpis;
                clsTiponotaInfo.idcstcofins = clsInfo.zsittribcofins;
                clsTiponotaInfo.idissqn_indISS = 0;

                clsTiponotaInfo.somaproduto = "S";
            }
            else
            {
                clsTiponotaInfo = clsTiponotaBLL.Carregar(id, clsInfo.conexaosqldados);
            }

            clsTiponotaInfoOld = new clsTiponotaInfo();

            TiponotaCampos(clsTiponotaInfo);
            TiponotaInfo(clsTiponotaInfoOld);

            TiponotaClientes();

            label93.Text = "Regime Tributario da " + clsInfo.zempresacliente_cognome;

            tbxRegimeTributario.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select codigo +  '-' + nome from regimetributario where id = " +
                clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select IDREGIMETRIBUTARIO FROM EMPRESAgere where empresa = " + clsInfo.zempresaid, "0")), "0");

            tbxCodigo.Select();
            tbxCodigo.SelectAll();
        }

        void TiponotaCampos(clsTiponotaInfo info)
        {
            id = info.id;
            idcfopok = info.idcfopok;
            idcfopent = info.idcfopent;
            idcfopdev = info.idcfopdev;
            idcfopdef = info.idcfopdef;
            idcfopmor = info.idcfopmor;
            idcfopbx = info.idcfopbx;
            idcfopbxman = info.idcfopbxman;

            iddizeres = info.iddizeres;
            iddizeres2 = info.iddizeres2;
            //idcsticms = info.idcsticms;
            //idcstipi = info.idcstipi;
            //idcstpis = info.idcstpis;
            //idcstcofins = info.idcstcofins;
            String Cst_ICM = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittributariab where id=" + info.idcsticms + "", "0");
            cbxIcmscst.SelectedIndex = SelecionarIndex(Cst_ICM, 2, cbxIcmscst);

            String Cst_IPI = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittribipi where id=" + info.idcstipi + "", "0");
            cbxIpicst.SelectedIndex = SelecionarIndex(Cst_IPI, 2, cbxIpicst);
            String Cst_Cofins = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittribcofins where id=" + info.idcstcofins + "", "0");
            cbxCofinscst.SelectedIndex = SelecionarIndex(Cst_Cofins, 2, cbxCofinscst);
            String Cst_Pis = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittribpis where id=" + info.idcstpis + "", "0");
            cbxPiscst.SelectedIndex = SelecionarIndex(Cst_Pis, 2, cbxCofinscst);

            //idicms_orig = info.idicms_orig;

            String Issqn_IndIss = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from TAB_DANFE_INDISS where id=" + info.idissqn_indISS + "", "0");
            cbxISSQN_cindISS.SelectedIndex = SelecionarIndex(Issqn_IndIss, 1, cbxISSQN_cindISS);

            tbxCodigo.Text = info.codigo;
            tbxNome.Text = info.nome;
            cbxMovimentacao.SelectedIndex = SelecionarIndex(info.movimentacao, 1, cbxMovimentacao);
            cbxFatura.SelectedIndex = SelecionarIndex(info.fatura, 1, cbxFatura);
            cbxDevolucao.SelectedIndex = SelecionarIndex(info.devolucao, 1, cbxDevolucao);
            cbxIndTot.SelectedIndex = SelecionarIndex(info.somaproduto, 1, cbxIndTot);
            String Icms_Orig = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from SITTRIBUTARIAA where id=" + info.idicms_orig + "", "0");
            cbxICMS_Orig.SelectedIndex = SelecionarIndex(Icms_Orig, 1, cbxICMS_Orig);

            String Icms_ModBC = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from TAB_DANFE_MODBC where id=" + info.idicms_modbc + "", "0");
            cbxICMS_ModBC.SelectedIndex = SelecionarIndex(Icms_ModBC, 1, cbxICMS_ModBC);

            String Icms_ModBCST = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from TAB_DANFE_MODBCST where id=" + info.idicms_modbcst + "", "0");
            cbxICMS_ModBCST.SelectedIndex = SelecionarIndex(Icms_ModBCST, 1, cbxICMS_ModBCST);

            String Icms_MotDesIcms = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from TAB_DANFE_MOTDESICMS where id=" + info.idicms_motdesicms + "", "0");
            cbxIcms_MotDesIcms.SelectedIndex = SelecionarIndex(Icms_MotDesIcms, 2, cbxIcms_MotDesIcms);

            cbxRetornoDeSaida.SelectedIndex = SelecionarIndex(info.retornodesaida, 1, cbxRetornoDeSaida);
            if (info.retornodesaida == null)
            {
                cbxRetornoDeSaida.SelectedIndex = 1;
            }
           
            if (idcfopok == 0)
            {
                idcfopok = clsInfo.zcfop;
            }          

            Fill_tbxCfopok();
            Fill_tbxCfopent();
            Fill_tbxCfopdev();
            Fill_tbxCfopdef();
            Fill_tbxCfopmor();
            Fill_tbxCfopbx();
            Fill_tbxCfopbxman();

            Fill_tbxDizeresnfv_codigo();
            Fill_tbxDizeresnfv_codigo_2();

            //Cstpis_Carrega();
            //Cstipi_Carrega();
            //Csticms_Carrega();


            if (info.tipo == "V")
            {
                rbnVenda.Checked = true;
            }
            else if (info.tipo == "B")
            {
                rbnBeneficiamento.Checked = true;
            }
            else if (info.tipo == "M")
            {
                rbnMaoObra.Checked = true;
            }
            else
            {
               rbnOutros.Checked = true;
            }
        }

        void TiponotaInfo(clsTiponotaInfo info)
        {
            info.id = id;
            info.idcfopok = idcfopok;
            info.idcfopent = idcfopent;
            info.idcfopdev = idcfopdev;
            info.idcfopdef = idcfopdef;
            info.idcfopmor = idcfopmor;
            info.idcfopbx = idcfopbx;
            info.idcfopbxman = idcfopbxman;
            info.iddizeres = iddizeres;
            info.iddizeres2 = iddizeres2;
            //info.idcsticms = idcsticms;
            //info.idcstipi = idcstipi;
            info.idcsticms = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittributariab where codigo='" + cbxIcmscst.Text.Substring(0, 2) + "' ", "0"));
            info.idcstcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribcofins where codigo='" + cbxCofinscst.Text.Substring(0, 2) + "' ", "0"));
            info.idcstipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribipi where codigo='" + cbxIpicst.Text.Substring(0, 2) + "' ", "0"));
            info.idcstpis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribpis where codigo='" + cbxPiscst.Text.Substring(0, 2) + "' ", "0"));
            info.idicms_orig = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from SITTRIBUTARIAA where codigo='" + cbxICMS_Orig.Text.Substring(0,1) + "' ", "0"));
            info.idicms_modbc = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from TAB_DANFE_MODBC where codigo=" +  clsParser.Int32Parse(cbxICMS_ModBC.Text.Substring(0,1)) + " ", "0"));
            info.idicms_modbcst = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from TAB_DANFE_MODBCST where codigo=" + clsParser.Int32Parse(cbxICMS_ModBCST.Text.Substring(0, 1)) + " ", "0"));
            info.idicms_motdesicms = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from TAB_DANFE_MOTDESICMS where codigo=" + clsParser.Int32Parse(cbxIcms_MotDesIcms.Text.Substring(0, 2)) + " ", "0"));
            info.idissqn_indISS = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from TAB_DANFE_INDISS where codigo='" + cbxISSQN_cindISS.Text.Substring(0, 1) + "' ", "0"));

            info.codigo = tbxCodigo.Text;
            info.nome = tbxNome.Text;
            info.movimentacao = cbxMovimentacao.Text.Substring(0, 1);
            info.fatura = cbxFatura.Text.Substring(0, 1);
            info.devolucao = cbxDevolucao.Text.Substring(0, 1);
            info.somaproduto = cbxIndTot.Text.Substring(0, 1);
            info.retornodesaida = cbxRetornoDeSaida.Text.Substring(0, 1);

            if (rbnVenda.Checked == true)
            {
                info.tipo = "V";
            }
            else if (rbnBeneficiamento.Checked == true)
            {
                info.tipo = "B";
            }
            else if (rbnMaoObra.Checked == true)
            {
                info.tipo = "M";
            }
            else
            {
                info.tipo = "O";
            }
        }

        void TiponotaClientes()
        {
            String query;
            SqlDataAdapter sda;

            query = "select " +
                        "tnc.id, " +
                        "tnc.idcliente, " +
                        "cliente.nome " +
                    "from " +
                        "tiponotacliente as tnc " +
                            " inner join cliente on cliente.id = tnc.idcliente " +
                    "where " +
                        "tnc.idtiponota = @idtiponota";

            dtClientes = new DataTable();
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@idtiponota", SqlDbType.Int).Value = id;

            sda.Fill(dtClientes);

            DataColumn dcPosicao = new DataColumn("posicao", Type.GetType("System.Int32"));
            dtClientes.Columns.Add(dcPosicao);

            for (int x = 0; x < dtClientes.Rows.Count; x++)
            {
                dtClientes.Rows[x]["posicao"] = x + 1;
            }
            dtClientes.AcceptChanges();

            dgvClientes.DataSource = dtClientes;

            
            clsGridHelper.MontaGrid2(dgvClientes,
                            new GridColuna[] 
                            { 
                                new GridColuna("Cliente", "nome", 285, true, DataGridViewContentAlignment.MiddleLeft) 
                            }, true);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            TrataCampos((Control)sender);
            Control ctl = (Control)sender;
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

        private void frmTiponota_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);           
        }

        private Boolean ModificouRegistro()
        {
            TiponotaInfo(clsTiponotaInfo);
            return (clsTiponotaBLL.Equals(clsTiponotaInfo, clsTiponotaInfoOld) == false);
        }

        private void Salvar()
        {
            TiponotaInfo(clsTiponotaInfo);

            TiponotaVerificaInfo(clsTiponotaInfo);

            if (id == 0)
            {
                id = clsTiponotaBLL.Incluir(clsTiponotaInfo, clsInfo.conexaosqldados);
                clsTiponotaInfo.id = id;
            }
            else
            {
                clsTiponotaBLL.Alterar(clsTiponotaInfo, clsInfo.conexaosqldados);
            }


            String query;
            SqlConnection scn;
            SqlCommand scd;

            scn = new SqlConnection(clsInfo.conexaosqldados);
            scn.Open();

            // Salva os dados do Grid
            foreach (DataRow row in dtClientes.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    query = "insert into tiponotacliente(idtiponota, idcliente) values (@idtiponota, @idcliente)";
                    scd = new SqlCommand(query, scn);

                    scd.Parameters.Add("@idtiponota", SqlDbType.Int).Value = clsTiponotaInfo.id;
                    scd.Parameters.Add("@idcliente", SqlDbType.Int).Value = row["idcliente"];

                    scd.ExecuteNonQuery();
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    query = "update tiponotacliente set idtiponota = @idtiponota, idcliente = @idcliente where id = @id";
                    scd = new SqlCommand(query, scn);

                    scd.Parameters.Add("@idtiponota", SqlDbType.Int).Value = clsTiponotaInfo.id;
                    scd.Parameters.Add("@idcliente", SqlDbType.Int).Value = row["idcliente"];
                    scd.Parameters.Add("@id", SqlDbType.Int).Value = row["id"];

                    scd.ExecuteNonQuery();
                }
                else if (row.RowState == DataRowState.Deleted)
                {
                    query = "delete tiponotacliente where id = @id";
                    scd = new SqlCommand(query, scn);

                    scd.Parameters.Add("@id", SqlDbType.Int).Value = row["id", DataRowVersion.Original];

                    scd.ExecuteNonQuery();
                }
            }

            scn.Close();
        }

        void TiponotaVerificaInfo(clsTiponotaInfo info)
        {
            if (info.nome == null ||
                info.nome.Length <= 2)
            {
                throw new Exception("O nome deve ter no mínimo 2 caracteres.");
            }

            if (info.codigo == null)
            {
                throw new Exception("É necessário determinar o CFOP principal do Tipo.");
            }
            //if (info.codigo == null ||
            //    info.codigo.Length != 8 ||
            //    info.codigo.Substring(0, 4) == "0000")
            //{
            //    throw new Exception("É necessário determinar o CFOP principal do Tipo.");
            //}

            if (info.iddizeres <= 0)
            {
                throw new Exception("Informações Complementares 01 não foi escolhido. É necessário determiná-lo.");
            }

            if (info.iddizeres2 <= 0)
            {
                throw new Exception("Informações Complementares 02 não foi escolhido. É necessário determiná-lo.");
            }

            if (info.idcfopent <= 0)
            {
                throw new Exception("É necessário determinar o CFOP de Entrada. Caso não queira determiná-lo, escolha a opção '0000-000'.");
            }

            if (info.idcfopdev <= 0)
            {
                throw new Exception("É necessário determinar o CFOP de Devolução (caso este tipo caracterize uma devolução, Devolução = 'Sim'). Caso não queira determiná-lo ou não exista essa operação na devida situação, escolha a opção '0000-000'.");
            }

            if (info.idcfopdef <= 0)
            {
                throw new Exception("É necessário determinar o CFOP de Defeito. Caso não queira determiná-lo, escolha a opção '0000-000'.");
            }

            if (info.idcfopmor <= 0)
            {
                throw new Exception("É necessário determinar o CFOP de Morta. Caso não queira determiná-lo, escolha a opção '0000-000'.");
            }

            if (info.idcfopbx <= 0)
            {
                throw new Exception("É necessário determinar o CFOP de Baixadas. Caso não queira determiná-lo, escolha a opção '0000-000'.");
            }

            if (info.idcfopbxman <= 0)
            {
                throw new Exception("É necessário determinar o CFOP de Baixas Manuais. Caso não queira determiná-lo, escolha a opção '0000-000'.");
            }

            if (info.idcsticms <= 0)
            {
                throw new Exception("CST do ICMS não foi escolhido. É necessário determiná-lo.");
            }

            if (info.idcstipi <= 0)
            {
                throw new Exception("CST do IPI não foi escolhido. É necessário determiná-lo.");
            }

            if (info.idcstpis <= 0)
            {
                throw new Exception("CST do Pis/Pasep não foi escolhido. É necessário determiná-lo.");
            }

            if (info.idcstcofins <= 0)
            {
                throw new Exception("CST do Cofins não foi escolhido. É necessário determiná-lo.");
            }
        }

        private void tsbSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult msg;

                msg = MessageBox.Show("Deseja Salvar este registro?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    Salvar();
                }
                else if (msg == DialogResult.Cancel)
                {
                    return;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbRetornar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModificouRegistro() == true)
                {
                    DialogResult msg;
                    msg = MessageBox.Show("Deseja Salvar este registro?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (msg == DialogResult.Yes)
                    {
                        Salvar();
                    }
                    else if (msg == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (rows != null)
                {
                    if (ModificouRegistro())
                    {
                        id = Int32.Parse(rows[0].Cells[0].Value.ToString());
                        TiponotaCarregar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Primeiro Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tspAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (rows != null)
                {
                    if (ModificouRegistro())
                    {
                        foreach (DataGridViewRow row in rows)
                        {
                            if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                            {
                                if (row.Index != 0)
                                {
                                    id = Int32.Parse(rows[rows.GetPreviousRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                    TiponotaCarregar();
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Registro Anterior:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tspProximo_Click(object sender, EventArgs e)
        {
            try
            {
                if (rows != null)
                {
                    if (ModificouRegistro())
                    {
                        foreach (DataGridViewRow row in rows)
                        {
                            if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                            {
                                if (row.Index < rows.Count - 1)
                                {
                                    id = Int32.Parse(rows[rows.GetNextRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                    TiponotaCarregar();
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Próximo Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tspUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (rows != null)
                {
                    if (ModificouRegistro())
                    {
                        id = Int32.Parse(rows[rows.Count - 1].Cells[0].Value.ToString());
                        TiponotaCarregar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível ir para o Último Registro:" + Environment.NewLine + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
       
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (tbxCliente.Text.Length > 0)
            {
                DataRow row;               
                idcliente = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cliente where nome='" + tbxCliente.Text + "'"));

                if (idcliente > 0)
                {
                    for (int x = 0; x < dgvClientes.Rows.Count; x++)
                    {
                        if (dgvClientes.Rows[x].Cells["idcliente"].Value.ToString() == idcliente.ToString())
                        {
                            return;
                        }
                    }

                    row = dtClientes.NewRow();

                    row["id"] = id;
                    row["idcliente"] = idcliente;
                    row["nome"] = tbxCliente.Text;
                    row["posicao"] = dtClientes.Rows.Count + 1;

                    dtClientes.Rows.Add(row);
                }
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow != null)
            {
                DialogResult drt;

                drt = MessageBox.Show("Deseja realmente Excluir essa ligação?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (drt == DialogResult.Yes)
                {
                    dtClientes.Select("posicao = " + dgvClientes.CurrentRow.Cells["posicao"].Value.ToString())[0].Delete();
                }
            }
        }

        private void CombosSelectedIndexChanged(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;

            //if (ctl.Name == cbxIcmscst.Name)
            //{
            //    Csticms_Check();
            //    Csticms_Carrega();
            //}
            //else if (ctl.Name == cbxIpicst.Name)
            //{
            //    Cstipi_Check();
            //    Cstipi_Carrega();
            //}
            //else if (ctl.Name == cbxPiscst.Name)
            //{
            //    Cstpis_Check();
            //    Cstpis_Carrega();
            //}
            //else if (ctl.Name == cbxCofinscst.Name)
            //{
            //    Cstcofins_Check();
            //    Cstcofins_Carrega();
            //}
        }

       
        private void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null &&
                    clsInfo.zrow.Index != -1 &&
                    clsInfo.znomegrid != null &&
                    clsInfo.znomegrid != "")
            {
                // ###############################
                // Verifica os botões de pesquisa
                // ###############################
                //cfop
                if (clsInfo.znomegrid == btnCfopok.Name)
                {
                    tbxCfopok.Text = clsInfo.zrow.Cells["cfop"].Value.ToString();
                    tbxCfopok_nome.Text = clsInfo.zrow.Cells["nomenota"].Value.ToString();

                    if (Check_tbxCfopok() == true) 
                    {
                        Fill_tbxCfopok();          
                    }
                    tbxCfopok.Select();
                    tbxCfopok.SelectAll();
                }
                //cfop entrada
                if (clsInfo.znomegrid == btnCfopent.Name)
                {
                    tbxCfopent.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxCfopent_nome.Text = clsInfo.zrow.Cells["nome"].Value.ToString();

                    if (Check_tbxCfopent() == true)
                    {
                        Fill_tbxCfopent();
                    }
                    tbxCfopent.Select();
                    tbxCfopent.SelectAll();
                }
                //cfop devolução
                if (clsInfo.znomegrid == btnCfopdev.Name)
                {
                    tbxCfopdev.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxCfopdev_nome.Text = clsInfo.zrow.Cells["nome"].Value.ToString();

                    if (Check_tbxCfopdev() == true)
                    {
                        Fill_tbxCfopdev();
                    }
                    tbxCfopdev.Select();
                    tbxCfopent.SelectAll();
                }
                //cfop morta
                if (clsInfo.znomegrid == btnCfopmor.Name)
                {
                    tbxCfopmor.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxCfopmor_nome.Text = clsInfo.zrow.Cells["nome"].Value.ToString();

                    if (Check_tbxCfopmor() == true)
                    {
                        Fill_tbxCfopmor();
                    }
                    tbxCfopmor.Select();
                    tbxCfopmor.SelectAll();
                }
                //cfop baixada
                if (clsInfo.znomegrid == btnCfopbx.Name)
                {
                    tbxCfopbx.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxCfopbx_nome.Text = clsInfo.zrow.Cells["nome"].Value.ToString();

                    if (Check_tbxCfopbx() == true)
                    {
                        Fill_tbxCfopbx();
                    }
                    tbxCfopbx.Select();
                    tbxCfopbx.SelectAll();
                }
                //cfop baixada manual
                if (clsInfo.znomegrid == btnCfopbxman.Name)
                {
                    tbxCfopbxman.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxCfopbxman_nome.Text = clsInfo.zrow.Cells["nome"].Value.ToString();

                    if (Check_tbxCfopbxman() == true)
                    {
                        Fill_tbxCfopbxman();
                    }
                    tbxCfopbxman.Select();
                    tbxCfopbxman.SelectAll();
                }
                // Dizeres
                if (clsInfo.znomegrid == btnDizeresnfv.Name)
                {
                    tbxDizeresnfv_codigo.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxDizeresnfv_nome.Text = clsInfo.zrow.Cells["nome"].Value.ToString();
                    if (Check_tbxDizeresnfv_codigo() == true)
                    {
                        Fill_tbxDizeresnfv_codigo();
                    }
                    tbxDizeresnfv_codigo.Select();
                    tbxDizeresnfv_codigo.SelectAll();
                }
                // Dizeres 2
                if (clsInfo.znomegrid == btnDizeresnfv_2.Name)
                {
                    tbxDizeresnfv_codigo_2.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxDizeresnfv_nome_2.Text = clsInfo.zrow.Cells["nome"].Value.ToString();
                    if (Check_tbxDizeresnfv_codigo_2() == true)
                    {
                        Fill_tbxDizeresnfv_codigo_2();
                    }
                    tbxDizeresnfv_codigo_2.Select();
                    tbxDizeresnfv_codigo_2.SelectAll();
                }
                // Cliente
                if (clsInfo.znomegrid == btnCliente.Name)
                {
                    tbxCliente.Text = clsInfo.zrow.Cells["nome"].Value.ToString();

                    if (Check_tbxCliente() == true)
                    {
                        Fill_tbxCliente();
                    }                   
                    tbxCliente.Select();
                    tbxCliente.SelectAll();
                }

            }
            else
            {
                //###############################
                //Verifica os campos de pesquisa
                //###############################
                //cfop
                if (ctl.Name == tbxCfopok.Name)    
                {
                    if (Check_tbxCfopok() == true) 
                    {
                        Fill_tbxCfopok();          

                    }
                }
                //cfop ent
                if (ctl.Name == tbxCfopent.Name)
                {
                    if (Check_tbxCfopent() == true)
                    {
                        Fill_tbxCfopent();

                    }
                }
                //cfop dev
                if (ctl.Name == tbxCfopdev.Name)
                {
                    if (Check_tbxCfopdev() == true)
                    {
                        Fill_tbxCfopdev();

                    }
                }
                //cfop defeito
                if (ctl.Name == tbxCfopdef.Name)
                {
                    if (Check_tbxCfopdef() == true)
                    {
                        Fill_tbxCfopdef();

                    }
                }
                //cfop morta
                if (ctl.Name == tbxCfopmor.Name)
                {
                    if (Check_tbxCfopmor() == true)
                    {
                        Fill_tbxCfopmor();

                    }
                }
                //cfop baixada
                if (ctl.Name == tbxCfopbx.Name)
                {
                    if (Check_tbxCfopbx() == true)
                    {
                        Fill_tbxCfopbx();

                    }
                }
                //cfop baixada manual
                if (ctl.Name == tbxCfopbxman.Name)
                {
                    if (Check_tbxCfopbxman() == true)
                    {
                        Fill_tbxCfopbxman();

                    }
                }
                //cliente
                if (ctl.Name == tbxCliente.Name)
                {
                    if (Check_tbxCliente() == true)
                    {
                        Fill_tbxCliente();
                    }
                }
                //Dizeres
                if (ctl.Name == tbxDizeresnfv_codigo.Name)
                {
                    if (Check_tbxDizeresnfv_codigo() == true)
                    {
                        Fill_tbxDizeresnfv_codigo();
                    }
                }
                //Dizeres 2
                if (ctl.Name == tbxDizeresnfv_codigo_2.Name)
                {
                    if (Check_tbxDizeresnfv_codigo_2() == true)
                    {
                        Fill_tbxDizeresnfv_codigo_2();
                    }
                }

                //verifica o combo
                //if(ctl.Name == cbxIcmscst.Name)
                //{
                //    Csticms_Check();
                //    Csticms_Carrega();
                //}
                //else if (ctl.Name == cbxIpicst.Name)
                //{
                //    Cstipi_Check();
                //    Cstipi_Carrega();
                //}
                //else if (ctl.Name == cbxPiscst.Name)
                //{
                //    Cstpis_Check();
                //    Cstpis_Carrega();
                //}
                //else if (ctl.Name == cbxCofinscst.Name)
                //{
                //    Cstcofins_Check();
                //    Cstcofins_Carrega();
                //}

            }
            clsInfo.znomegrid = "";
            clsInfo.zrow = null;
        }

        //cfop
        private Boolean Check_tbxCfopok()
        {
            Int32 idtmp =  clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select id from cfop where cfop='" + tbxCfopok.Text + "'"));

            if (idtmp == 0)

                if (idtmp == idcfopok && idtmp != 0)
            {
                return false;
            }
                else if (idtmp == 0 && idcfopok == 0)
            {
                idtmp = 0;
            }
            idcfopok = idtmp;
            return true;
        }

        private void Fill_tbxCfopok()
        {
            if (idcfopok > 0)
            {
                tbxCfopok.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cfop from cfop where id=" + idcfopok);
                tbxCfopok_nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nomenota from cfop where id=" + idcfopok);
            }
            else
            {
                tbxCfopok.Text = "";
                tbxCfopok_nome.Text = "";
            }
        }
        // cfop entrada 
        private Boolean Check_tbxCfopent()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select id from tiponota where codigo='" + tbxCfopent.Text + "'"));

            if (idtmp == 0)

                if (idtmp == idcfopent && idtmp != 0)
                {
                    return false;
                }
                else if (idtmp == 0 && idcfopent == 0)
                {
                    idtmp = 0;
                }
            idcfopent = idtmp;
            return true;
        }

        private void Fill_tbxCfopent()
        {
            if (idcfopent > 0)
            {
                tbxCfopent.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from tiponota where id=" + idcfopent);
                tbxCfopent_nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from tiponota where id=" + idcfopent);
            }
            else
            {
                tbxCfopent.Text = "";
                tbxCfopent_nome.Text = "";
            }
        }
        //devolução
        private Boolean Check_tbxCfopdev()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select id from tiponota where codigo='" + tbxCfopdev.Text + "'"));
   
            if (idtmp == 0)

                if (idtmp == idcfopdev && idtmp != 0)
                {
                    return false;
                }
                else if (idtmp == 0 && idcfopdev == 0)
                {
                    idtmp = 0;
                }
            idcfopdev = idtmp;
            return true;
        }

        private void Fill_tbxCfopdev()
        {
            if (idcfopdev > 0)
            {
                tbxCfopdev.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from tiponota where id=" + idcfopdev);
                tbxCfopdev_nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from tiponota where id=" + idcfopdev);
            }
            else
            {
                tbxCfopdev.Text = "";
                tbxCfopdev_nome.Text = "";
            }
        }
        
        //cfop Defeito
        private Boolean Check_tbxCfopdef()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select id from tiponota where codigo='" + tbxCfopdef.Text + "'"));

            if (idtmp == 0)

                if (idtmp == idcfopdef && idtmp != 0)
                {
                    return false;
                }
                else if (idtmp == 0 && idcfopdef == 0)
                {
                    idtmp = 0;
                }
            idcfopdef = idtmp;
            return true;
        }

        private void Fill_tbxCfopdef()
        {
            if (idcfopdef > 0)
            {
                tbxCfopdef.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from tiponota where id=" + idcfopdef);
                tbxCfopdef_nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from tiponota where id=" + idcfopdef);
            }
            else
            {
                tbxCfopdef.Text = "";
                tbxCfopdef_nome.Text = "";
            }
        }
        
        //cfop morta
        private Boolean Check_tbxCfopmor()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select id from tiponota where codigo='" + tbxCfopmor.Text + "'"));

            if (idtmp == 0)

                if (idtmp == idcfopmor && idtmp != 0)
                {
                    return false;
                }
                else if (idtmp == 0 && idcfopmor == 0)
                {
                    idtmp = 0;
                }
            idcfopmor = idtmp;
            return true;
        }

        private void Fill_tbxCfopmor()
        {
            if (idcfopmor > 0)
            {
                tbxCfopmor.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from tiponota where id=" + idcfopmor);
                tbxCfopmor_nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from tiponota where id=" + idcfopmor);

            }
            else
            {
                tbxCfopmor.Text = "";
                tbxCfopmor_nome.Text = "";
            }
        }

        // cfop Baixa
        private Boolean Check_tbxCfopbx()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select id from tiponota where codigo='" + tbxCfopbx.Text + "'"));

            if (idtmp == 0)

                if (idtmp == idcfopbx && idtmp != 0)
                {
                    return false;
                }
                else if (idtmp == 0 && idcfopbx == 0)
                {
                    idtmp = 0;
                }
            idcfopbx = idtmp;
            return true;
        }

        private void Fill_tbxCfopbx()
        {
            if (idcfopbx > 0)
            {
                tbxCfopbx.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from tiponota where id=" + idcfopbx);
                tbxCfopbx_nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from tiponota where id=" + idcfopbx);
            }
            else
            {
                tbxCfopbx.Text = "";
                tbxCfopbx_nome.Text = "";
            }
        }

        // cfop Baixa Manual
        private Boolean Check_tbxCfopbxman()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select id from tiponota where codigo='" + tbxCfopbxman.Text + "'"));

            if (idtmp == 0)

                if (idtmp == idcfopbxman && idtmp != 0)
                {
                    return false;
                }
                else if (idtmp == 0 && idcfopbxman == 0)
                {
                    idtmp = 0;
                }
            idcfopbxman = idtmp;
            return true;
        }

        private void Fill_tbxCfopbxman()
        {
            if (idcfopbxman > 0)
            {
                tbxCfopbxman.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from tiponota where id=" + idcfopbxman);
                tbxCfopbxman_nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from tiponota where id=" + idcfopbxman);
            }
            else
            {
                tbxCfopbxman.Text = "";
                tbxCfopbxman_nome.Text = "";
            }
        }
        
        //dizeres
        private Boolean Check_tbxDizeresnfv_codigo()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from dizeresnfv where codigo='" + tbxDizeresnfv_codigo.Text + "'"));

            if (idtmp == 0)

                if (idtmp == iddizeres && idtmp != 0)
                {
                    return false;
                }
                else if (idtmp == 0 && iddizeres == 0)
                {
                    idtmp = 0;
                }
            iddizeres = idtmp;
            return true;
        }

        private void Fill_tbxDizeresnfv_codigo()
        {
            if (iddizeres > 0)
            {
                tbxDizeresnfv_codigo.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from dizeresnfv where id=" + iddizeres);
                tbxDizeresnfv_nome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from dizeresnfv where id=" + iddizeres);
            }
            else
            {
                tbxDizeresnfv_codigo.Text = "";
                tbxDizeresnfv_nome.Text = "";
            }
        }

        //dizeres 2
        private Boolean Check_tbxDizeresnfv_codigo_2()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select id from dizeresnfv where codigo='" + tbxDizeresnfv_codigo_2.Text + "'"));

            if (idtmp == 0)

                if (idtmp == iddizeres2 && idtmp != 0)
                {
                    return false;
                }
                else if (idtmp == 0 && iddizeres2 == 0)
                {
                    idtmp = 0;
                }
            iddizeres2 = idtmp;
            return true;
        }

        private void Fill_tbxDizeresnfv_codigo_2()
        {
            if (iddizeres2 > 0)
            {
                tbxDizeresnfv_codigo_2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from dizeresnfv where id=" + iddizeres2);
                tbxDizeresnfv_nome_2.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from dizeresnfv where id=" + iddizeres2);

            }
            else
            {
                tbxDizeresnfv_codigo_2.Text = "";
                tbxDizeresnfv_nome_2.Text = "";
            }
        }  

        //  Cliente
        private Boolean Check_tbxCliente()
        {
            Int32 idtmp = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                "select id from cliente where nome='" + tbxCliente.Text + "'"));

            if (idtmp == 0)

                if (idtmp == idcliente && idtmp != 0)
                {
                    return false;
                }
                else if (idtmp == 0 && idcliente == 0)
                {
                    idtmp = 0;
                }
            idcliente = idtmp;
            return true;
        }

        private void Fill_tbxCliente()
        {
            if (idcliente > 0)
            {
                tbxCliente.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                    "select nome from cliente where id=" + idcliente);
            }
            else
            {
                tbxCliente.Text= "";   
            }
        }  
        //void Csticms_Check()
        //{
        //    idcsticms = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittributariab where codigo='" + cbxIcmscst.Text.Substring(0, 3).Trim() + "'"));

        //    if (idcsticms == 0)
        //    {
        //        idcsticms = clsInfo.zsituacaotribb;
        //    }
        //}

        //void Csticms_Carrega()
        //{
        //    if (idcsticms == 0)
        //    {
        //        idcsticms = clsInfo.zsituacaotribb;
        //    }

        //    String texto = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittributariab where id=" + idcsticms, "00");

        //    cbxIcmscst.SelectedIndex = SelecionarIndex(texto, texto.Length, cbxIcmscst);
        //}

        //void Cstipi_Check()
        //{
        //    idcstipi = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribipi where codigo='" + cbxIpicst.Text.Substring(0, 2) + "'"));

        //    if (idcstipi == 0)
        //    {
        //        idcstipi = clsInfo.zsittribipi;
        //    }
        //}

        //void Cstipi_Carrega()
        //{
        //    if (idcstipi == 0)
        //    {
        //        idcstipi = clsInfo.zsittribipi;
        //    }

        //    cbxIpicst.SelectedIndex = SelecionarIndex(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittribipi where id=" + idcstipi).Substring(0, 2), 2, cbxIpicst);
        //}

        //void Cstpis_Check()
        //{
        //    idcstpis = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribpis where codigo='" + cbxPiscst.Text.Substring(0, 2) + "'"));

        //    if (idcstpis == 0)
        //    {
        //        idcstpis = clsInfo.zsittribpis;
        //    }
        //}

        //void Cstpis_Carrega()
        //{
        //    if (idcstpis == 0)
        //    {
        //        idcstpis = clsInfo.zsittribpis;
        //    }

        //    cbxPiscst.SelectedIndex = SelecionarIndex(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittribpis where id=" + idcstpis).Substring(0, 2), 2, cbxPiscst);
        //}

        //void Cstcofins_Check()
        //{
        //    idcstcofins = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribcofins where codigo='" + cbxCofinscst.Text.Substring(0, 2) + "'"));

        //    if (idcstcofins == 0)
        //    {
        //        idcstcofins = clsInfo.zsittribcofins;
        //    }
        //}

        //void Cstcofins_Carrega()
        //{
        //    if (idcstcofins == 0)
        //    {
        //        idcstcofins = clsInfo.zsittribcofins;
        //    }

        //    cbxCofinscst.SelectedIndex = SelecionarIndex(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from sittribcofins where id=" + idcstcofins).Substring(0, 2), 2, cbxCofinscst);
        //}



        private void btnCfopok_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCfopok.Name;
            frmCfopPes frmCfopPes = new frmCfopPes();
            frmCfopPes.Init(idcfopok);

            clsFormHelper.AbrirForm(this.MdiParent, frmCfopPes, clsInfo.conexaosqldados);
        }


        private void btnCfopent_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCfopent.Name;
            frmTiponotaPes frmTiponotaPes = new frmTiponotaPes();
            frmTiponotaPes.Init(idcfopent);

            clsFormHelper.AbrirForm(this.MdiParent, frmTiponotaPes, clsInfo.conexaosqldados);
        }

        private void btnCfopdev_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCfopdev.Name;
            frmTiponotaPes frmTiponotaPes = new frmTiponotaPes();
            frmTiponotaPes.Init(idcfopdev);

            clsFormHelper.AbrirForm(this.MdiParent, frmTiponotaPes, clsInfo.conexaosqldados);
        }

        private void btnCfopdef_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCfopdef.Name;
            frmTiponotaPes frmTiponotaPes = new frmTiponotaPes();
            frmTiponotaPes.Init(idcfopdef);

            clsFormHelper.AbrirForm(this.MdiParent, frmTiponotaPes, clsInfo.conexaosqldados);
        }

        private void btnCfopmor_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCfopmor.Name;
            frmTiponotaPes frmTiponotaPes = new frmTiponotaPes();
            frmTiponotaPes.Init(idcfopmor);

            clsFormHelper.AbrirForm(this.MdiParent, frmTiponotaPes, 
                        clsInfo.conexaosqldados);
        }

        private void btnCfopbx_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCfopbx.Name;
            frmTiponotaPes frmTiponotaPes = new frmTiponotaPes();
            frmTiponotaPes.Init(idcfopbx);

            clsFormHelper.AbrirForm(this.MdiParent, frmTiponotaPes,
                        clsInfo.conexaosqldados);
        }

        private void btnCfopbxman_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCfopbxman.Name;
            frmTiponotaPes frmTiponotaPes = new frmTiponotaPes();
            frmTiponotaPes.Init(idcfopbxman);

            clsFormHelper.AbrirForm(this.MdiParent, frmTiponotaPes,
                        clsInfo.conexaosqldados);
        }

        private void btnDizeresnfv_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnDizeresnfv.Name;
            frmDizeresNFVVis frmDizeresNFVVis = new frmDizeresNFVVis();
            frmDizeresNFVVis.Init(iddizeres);

            clsFormHelper.AbrirForm(this.MdiParent, frmDizeresNFVVis, clsInfo.conexaosqldados);
        }

        private void btnDizeresnfv_2_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnDizeresnfv_2.Name;
            frmDizeresNFVVis frmDizeresNFVVis = new frmDizeresNFVVis();
            frmDizeresNFVVis.Init(iddizeres2);

            clsFormHelper.AbrirForm(this.MdiParent, frmDizeresNFVVis, clsInfo.conexaosqldados);
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnCliente.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void tbxCfopdef_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxCfopbx_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnImportar_SITTRIBCOFINS_Click(object sender, EventArgs e)
        {
            // SITTRIBCOFINS
            nRegistros = 0;
            str = File.ReadAllLines(clsInfo.arquivos + "\\SittribCofins.txt");
            if (str.LongCount() > 0)
            {
                nRegistros = (Int32)str.LongCount();
            }
            if (nRegistros > 0)
            {
                lbxOrigem.Items.Clear();
                foreach (String linha in str)
                {
                    lbxOrigem.Items.Add(linha);
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))     // separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsSittribcofinsInfo = new clsSittribcofinsInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSittribcofinsInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    campo = clsVisual.RemoveAcentos(campo);
                                    clsSittribcofinsInfo.nome = campo.PadRight(100, ' ').Substring(0, 99);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsSittribcofinsInfo clsSittribcofinsInfo1 = new clsSittribcofinsInfo();
                    clsSittribcofinsInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribcofins where codigo='" + clsSittribcofinsInfo.codigo + "' "));
                    clsSittribcofinsInfo1 = clsSittribcofinsBLL.Carregar(clsSittribcofinsInfo1.id, clsInfo.conexaosqldados);
                    if (clsSittribcofinsInfo1.codigo == null)
                    {
                        clsSittribcofinsInfo1 = new clsSittribcofinsInfo();
                    }
                    if (clsSittribcofinsInfo1.codigo == null || clsSittribcofinsInfo1.codigo == clsSittribcofinsInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsSittribcofinsInfo1.codigo == null)
                        { // incluir
                            clsSittribcofinsBLL.Incluir(clsSittribcofinsInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsSittribcofinsBLL.Alterar(clsSittribcofinsInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nada Consta pasta : " + clsInfo.arquivos + "\\SittribCofins.txt");
            }
        }

        private void cbxCofinscst_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnImportar_SITTRIBPIS_Click(object sender, EventArgs e)
        {
            // SITTRIBPIS
            nRegistros = 0;
            str = File.ReadAllLines(clsInfo.arquivos + "\\SittribPis.txt");
            if (str.LongCount() > 0)
            {
                nRegistros = (Int32)str.LongCount();
            }
            if (nRegistros > 0)
            {
                lbxOrigem.Items.Clear();
                foreach (String linha in str)
                {
                    lbxOrigem.Items.Add(linha);
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))     // separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsSittribpisInfo = new clsSittribpisInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSittribpisInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    campo = clsVisual.RemoveAcentos(campo);
                                    clsSittribpisInfo.nome = campo.PadRight(100, ' ').Substring(0, 99);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsSittribpisInfo clsSittribpisInfo1 = new clsSittribpisInfo();
                    clsSittribpisInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribcofins where codigo='" + clsSittribpisInfo.codigo + "' "));
                    clsSittribpisInfo1 = clsSittribpisBLL.Carregar(clsSittribpisInfo1.id, clsInfo.conexaosqldados);
                    if (clsSittribpisInfo1.codigo == null)
                    {
                        clsSittribpisInfo1 = new clsSittribpisInfo();
                    }
                    if (clsSittribpisInfo1.codigo == null || clsSittribpisInfo1.codigo == clsSittribpisInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsSittribpisInfo1.codigo == null)
                        { // incluir
                            clsSittribpisBLL.Incluir(clsSittribpisInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsSittribpisBLL.Alterar(clsSittribpisInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nada Consta pasta : " + clsInfo.arquivos + "\\SittribPis.txt");
            }

        }

        private void btnImportar_SITTRIBIPI_Click(object sender, EventArgs e)
        {
            // SITTRIBIPI
            nRegistros = 0;
            str = File.ReadAllLines(clsInfo.arquivos + "\\SittribIPI.txt");
            if (str.LongCount() > 0)
            {
                nRegistros = (Int32)str.LongCount();
            }
            if (nRegistros > 0)
            {
                lbxOrigem.Items.Clear();
                foreach (String linha in str)
                {
                    lbxOrigem.Items.Add(linha);
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))     // separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsSitTribIpiInfo = new clsSitTribIpiInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSitTribIpiInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    campo = clsVisual.RemoveAcentos(campo);
                                    clsSitTribIpiInfo.nome = campo.PadRight(100, ' ').Substring(0, 99);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsSitTribIpiInfo clsSittribipiInfo1 = new clsSitTribIpiInfo();
                    clsSittribipiInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittribipi where codigo='" + clsSitTribIpiInfo.codigo + "' "));
                    clsSittribipiInfo1 = clsSittribipiBLL.Carregar(clsSittribipiInfo1.id, clsInfo.conexaosqldados);
                    if (clsSittribipiInfo1.codigo == null)
                    {
                        clsSittribipiInfo1 = new clsSitTribIpiInfo();
                    }
                    if (clsSittribipiInfo1.codigo == null || clsSittribipiInfo1.codigo == clsSitTribIpiInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsSittribipiInfo1.codigo == null)
                        { // incluir
                            clsSittribipiBLL.Incluir(clsSitTribIpiInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsSittribipiBLL.Alterar(clsSitTribIpiInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nada Consta pasta : " + clsInfo.arquivos + "\\SittribIPI.txt");
            }

        }

        private void btnImportar_ISSQN_cindISS_Click(object sender, EventArgs e)
        {
            // ISSQN CINDISS
            nRegistros = 0;
            str = File.ReadAllLines(clsInfo.arquivos + "\\tab_danfe_indiss.txt"); 
            if (str.LongCount() > 0)
            {
                nRegistros = (Int32)str.LongCount();
            }
            if (nRegistros > 0)
            {
                lbxOrigem.Items.Clear();
                foreach (String linha in str)
                {
                    lbxOrigem.Items.Add(linha);
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))     // separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsTab_Danfe_IndISSInfo = new clsTab_Danfe_IndISSInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsTab_Danfe_IndISSInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    campo = clsVisual.RemoveAcentos(campo);
                                    clsTab_Danfe_IndISSInfo.nome = campo.PadRight(100, ' ').Substring(0, 99);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsTab_Danfe_IndISSInfo clsTab_Danfe_IndISSInfo1 = new clsTab_Danfe_IndISSInfo();
                    clsTab_Danfe_IndISSInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from Tab_Danfe_IndISS where codigo='" + clsTab_Danfe_IndISSInfo.codigo + "' "));
                    clsTab_Danfe_IndISSInfo1 = clsTab_Danfe_IndISSBLL.Carregar(clsTab_Danfe_IndISSInfo1.id, clsInfo.conexaosqldados);
                    if (clsTab_Danfe_IndISSInfo1.codigo == null)
                    {
                        clsTab_Danfe_IndISSInfo1 = new clsTab_Danfe_IndISSInfo();
                    }
                    if (clsTab_Danfe_IndISSInfo1.codigo == null || clsTab_Danfe_IndISSInfo1.codigo == clsTab_Danfe_IndISSInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsTab_Danfe_IndISSInfo1.codigo == null)
                        { // incluir
                            clsTab_Danfe_IndISSBLL.Incluir(clsTab_Danfe_IndISSInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsTab_Danfe_IndISSBLL.Alterar(clsTab_Danfe_IndISSInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nada Consta pasta : " + clsInfo.arquivos + "\\SittribCofins.txt");
            }

        }

        private void btnImportar_SITTRIBUTARIAB_Click(object sender, EventArgs e)
        {
            // SITTRIBUTARIAb  ... cst icms
            nRegistros = 0;
            str = File.ReadAllLines(clsInfo.arquivos + "\\Sittributariab.txt");
            if (str.LongCount() > 0)
            {
                nRegistros = (Int32)str.LongCount();
            }
            if (nRegistros > 0)
            {
                lbxOrigem.Items.Clear();
                foreach (String linha in str)
                {
                    lbxOrigem.Items.Add(linha);
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))     // separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsSittributariabInfo = new clsSittributariabInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSittributariabInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    campo = clsVisual.RemoveAcentos(campo);
                                    clsSittributariabInfo.nome = campo.PadRight(100, ' ').Substring(0, 99);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;


                                case 3:  // IDREFERENCIA DO IMPOSTO
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    clsSittributariabInfo.idreferencia = clsParser.Int32Parse(campo);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;

                            }
                        }
                    }
                    // 
                    clsSittributariabInfo clsSittributariabInfo1 = new clsSittributariabInfo();
                    clsSittributariabInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittributariab where codigo='" + clsSittributariabInfo.codigo + "' "));
                    clsSittributariabInfo1 = clsSittributariabBLL.Carregar(clsSittributariabInfo1.id, clsInfo.conexaosqldados);
                    if (clsSittributariabInfo1.codigo == null)
                    {
                        clsSittributariabInfo1 = new clsSittributariabInfo();
                    }
                    if (clsSittributariabInfo1.codigo == null || clsSittributariabInfo1.codigo == clsSittributariabInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsSittributariabInfo1.codigo == null)
                        { // incluir
                            clsSittributariabBLL.Incluir(clsSittributariabInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsSittributariabBLL.Alterar(clsSittributariabInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nada Consta pasta : " + clsInfo.arquivos + "\\Sittributariab.txt");
            }

        }

        private void btnImportar_SITTRIBUTARIAA_Click(object sender, EventArgs e)
        {
            // SITTRIBUTARAA  - Situacao tribiutaria origem
            nRegistros = 0;
            str = File.ReadAllLines(clsInfo.arquivos + "\\Sittributariaa.txt");
            if (str.LongCount() > 0)
            {
                nRegistros = (Int32)str.LongCount();
            }
            if (nRegistros > 0)
            {
                lbxOrigem.Items.Clear();
                foreach (String linha in str)
                {
                    lbxOrigem.Items.Add(linha);
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))     // separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsSittributariaaInfo = new clsSittributariaaInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsSittributariaaInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    campo = clsVisual.RemoveAcentos(campo);
                                    clsSittributariaaInfo.nome = campo.PadRight(100, ' ').Substring(0, 99);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsSittributariaaInfo clsSittributariaaInfo1 = new clsSittributariaaInfo();
                    clsSittributariaaInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from sittributariaa where codigo='" + clsSittributariaaInfo.codigo + "' "));
                    clsSittributariaaInfo1 = clsSittributariaaBLL.Carregar(clsSittributariaaInfo1.id, clsInfo.conexaosqldados);
                    if (clsSittributariaaInfo1.codigo == null)
                    {
                        clsSittributariaaInfo1 = new clsSittributariaaInfo();
                    }
                    if (clsSittributariaaInfo1.codigo == null || clsSittributariaaInfo1.codigo == clsSittributariaaInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsSittributariaaInfo1.codigo == null)
                        { // incluir
                            clsSittributariaaBLL.Incluir(clsSittributariaaInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsSittributariaaBLL.Alterar(clsSittributariaaInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nada Consta pasta : " + clsInfo.arquivos + "\\Sittributariaa.txt");
            }


        }

        private void btnImportar_tab_danfe_modbc_Click(object sender, EventArgs e)
        {
            // tab_danfe_modbcst  - Modalidade icms
            nRegistros = 0;
            str = File.ReadAllLines(clsInfo.arquivos + "\\tab_danfe_modbc.txt");
            if (str.LongCount() > 0)
            {
                nRegistros = (Int32)str.LongCount();
            }
            if (nRegistros > 0)
            {
                lbxOrigem.Items.Clear();
                foreach (String linha in str)
                {
                    lbxOrigem.Items.Add(linha);
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))     // separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsTab_Danfe_ModBcInfo = new clsTab_Danfe_ModBcInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsTab_Danfe_ModBcInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    campo = clsVisual.RemoveAcentos(campo);
                                    clsTab_Danfe_ModBcInfo.nome = campo.PadRight(100, ' ').Substring(0, 99);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsTab_Danfe_ModBcInfo clsTab_Danfe_ModBcInfo1 = new clsTab_Danfe_ModBcInfo();
                    clsTab_Danfe_ModBcInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from tab_danfe_modbc where codigo='" + clsTab_Danfe_ModBcInfo.codigo + "' "));
                    clsTab_Danfe_ModBcInfo1 = clsTab_Danfe_ModBcBLL.Carregar(clsTab_Danfe_ModBcInfo1.id, clsInfo.conexaosqldados);
                    if (clsTab_Danfe_ModBcInfo1.codigo == null)
                    {
                        clsTab_Danfe_ModBcInfo1 = new clsTab_Danfe_ModBcInfo();
                    }
                    if (clsTab_Danfe_ModBcInfo1.codigo == null || clsTab_Danfe_ModBcInfo1.codigo == clsTab_Danfe_ModBcInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsTab_Danfe_ModBcInfo1.codigo == null)
                        { // incluir
                            clsTab_Danfe_ModBcBLL.Incluir(clsTab_Danfe_ModBcInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsTab_Danfe_ModBcBLL.Alterar(clsTab_Danfe_ModBcInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nada Consta pasta : " + clsInfo.arquivos + "\\tab_danfe_modbc.txt");
            }


        }

        private void btnImportar_tab_danfe_modbcst_Click(object sender, EventArgs e)
        {
            // tab_danfe_modbcst  - Modalidade icms ST
            nRegistros = 0;
            str = File.ReadAllLines(clsInfo.arquivos + "\\tab_danfe_modbcst.txt");
            if (str.LongCount() > 0)
            {
                nRegistros = (Int32)str.LongCount();
            }
            if (nRegistros > 0)
            {
                lbxOrigem.Items.Clear();
                foreach (String linha in str)
                {
                    lbxOrigem.Items.Add(linha);
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))     // separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsTab_Danfe_ModBcStInfo = new clsTab_Danfe_ModBcStInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsTab_Danfe_ModBcStInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    campo = clsVisual.RemoveAcentos(campo);
                                    clsTab_Danfe_ModBcStInfo.nome = campo.PadRight(100, ' ').Substring(0, 99);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsTab_Danfe_ModBcStInfo clsTab_Danfe_ModBcStInfo1 = new clsTab_Danfe_ModBcStInfo();
                    clsTab_Danfe_ModBcStInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from tab_danfe_ModBcSt where codigo='" + clsTab_Danfe_ModBcStInfo.codigo + "' "));
                    clsTab_Danfe_ModBcStInfo1 = clsTab_Danfe_ModBcStBLL.Carregar(clsTab_Danfe_ModBcStInfo1.id, clsInfo.conexaosqldados);
                    if (clsTab_Danfe_ModBcStInfo1.codigo == null)
                    {
                        clsTab_Danfe_ModBcStInfo1 = new clsTab_Danfe_ModBcStInfo();
                    }
                    if (clsTab_Danfe_ModBcStInfo1.codigo == null || clsTab_Danfe_ModBcStInfo1.codigo == clsTab_Danfe_ModBcStInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsTab_Danfe_ModBcStInfo1.codigo == null)
                        { // incluir
                            clsTab_Danfe_ModBcStBLL.Incluir(clsTab_Danfe_ModBcStInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsTab_Danfe_ModBcStBLL.Alterar(clsTab_Danfe_ModBcStInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nada Consta pasta : " + clsInfo.arquivos + "\\tab_danfe_ModBcSt.txt");
            }


        }

        private void btnImportar_tab_danfe_motdesicms_Click(object sender, EventArgs e)
        {
            // tab_danfe_modbcst  - Modalidade icms ST
            nRegistros = 0;
            str = File.ReadAllLines(clsInfo.arquivos + "\\tab_danfe_Motdesicms.txt");
            if (str.LongCount() > 0)
            {
                nRegistros = (Int32)str.LongCount();
            }
            if (nRegistros > 0)
            {
                lbxOrigem.Items.Clear();
                foreach (String linha in str)
                {
                    lbxOrigem.Items.Add(linha);
                }
                // Descarregando a tabela dentro do arquivo de destino
                foreach (String linha in lbxOrigem.Items)
                {
                    qtcaracter = linha.ToString().Length;
                    posicaoatual = 0;
                    posicaoinicial = 0;
                    posicaofinal = 0;

                    ok = false;
                    for (Int32 i = 1; i < qtcaracter; i++)
                    {
                        if (linha.ToString().Substring(i, 1) == "|" || i == (qtcaracter - 1))     // separacao |
                        {
                            posicaoatual++;
                            posicaofinal = i;
                            if (i == (qtcaracter - 1))
                            {
                                posicaofinal = i + 1;
                            }
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                        }

                        if (ok == true)
                        {
                            campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                            // posicaoinicial = (posicaofinal + 1);
                            if (posicaoinicial == 0)
                            {
                                clsTab_Danfe_MotDesIcmsInfo = new clsTab_Danfe_MotDesIcmsInfo();
                            }
                            switch (posicaoatual)
                            {
                                case 1:   // CODIGO
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    clsTab_Danfe_MotDesIcmsInfo.codigo = campo;
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                                case 2:  // NOME
                                    campo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial)).ToUpper();
                                    campo = campo.Replace("|", "");
                                    campo = clsVisual.RemoveAcentos(campo);
                                    clsTab_Danfe_MotDesIcmsInfo.nome = campo.PadRight(100, ' ').Substring(0, 99);
                                    posicaoinicial = (posicaofinal + 1);
                                    break;
                            }
                        }
                    }
                    // 
                    clsTab_Danfe_MotDesIcmsInfo clsTab_Danfe_MotDesIcmsInfo1 = new clsTab_Danfe_MotDesIcmsInfo();
                    clsTab_Danfe_MotDesIcmsInfo1.id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from tab_danfe_MotDesIcms where codigo='" + clsTab_Danfe_MotDesIcmsInfo.codigo + "' "));
                    clsTab_Danfe_MotDesIcmsInfo1 = clsTab_Danfe_MotDesIcmsBLL.Carregar(clsTab_Danfe_MotDesIcmsInfo1.id, clsInfo.conexaosqldados);
                    if (clsTab_Danfe_MotDesIcmsInfo1.codigo == null)
                    {
                        clsTab_Danfe_MotDesIcmsInfo1 = new clsTab_Danfe_MotDesIcmsInfo();
                    }
                    if (clsTab_Danfe_MotDesIcmsInfo1.codigo == null || clsTab_Danfe_MotDesIcmsInfo1.codigo == clsTab_Danfe_MotDesIcmsInfo.codigo)
                    {
                        scn = new SqlConnection(clsInfo.conexaosqldados);
                        if (clsTab_Danfe_MotDesIcmsInfo1.codigo == null)
                        { // incluir
                            clsTab_Danfe_MotDesIcmsBLL.Incluir(clsTab_Danfe_MotDesIcmsInfo, clsInfo.conexaosqldados);
                        }
                        else
                        { // alterar
                            clsTab_Danfe_MotDesIcmsBLL.Alterar(clsTab_Danfe_MotDesIcmsInfo, clsInfo.conexaosqldados);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nada Consta pasta : " + clsInfo.arquivos + "\\tab_danfe_Motdesicms.txt");
            }

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            tclTipoNota.SelectedIndex = 0;
        }

        private void btnCfopdef_Click_1(object sender, EventArgs e)
        {

        }

    }

}
