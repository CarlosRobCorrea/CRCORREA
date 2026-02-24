using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

using CrystalDecisions.Shared;

using System;
using System.Data;
using System.Transactions;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmCondPagto : Form
    {

        ParameterFields pfields = new ParameterFields();

        clsCondpagtoBLL clsCondpagtoBLL;
        clsCondpagtoInfo clsCondpagtoInfo;
        clsCondpagtoInfo clsCondpagtoInfoOld;

        clsPagarBLL clsPagarBLL;
        clsPagarInfo clsPagarInfo;

        Int32 id;
        Int32 idFornecedor;

        DataGridViewRowCollection rows;

        // apagar abaix
        Int32 qtdeparcold;
        Int32 qtdediasold;
        Int32 caiold;

        DataTable dtTemp;

        public frmCondPagto()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id,
                         DataGridViewRowCollection _rows)
        {
            this.id = _id;
            this.rows = _rows;

            clsCondpagtoBLL = new clsCondpagtoBLL();
        }

        private void frmCondPagto_Load(object sender, EventArgs e)
        {
            CondpagtoCarregar();
        }

        private void CondpagtoCarregar()
        {
            clsCondpagtoInfoOld = new clsCondpagtoInfo();

            if (id == 0)
            {
                clsCondpagtoInfo = new clsCondpagtoInfo();

                clsCondpagtoInfo.id = 0;

                clsCondpagtoInfo.aplica_depto = "T";
                clsCondpagtoInfo.cai1 = 0;
                clsCondpagtoInfo.cai2 = 0;
                clsCondpagtoInfo.cai3 = 0;
                clsCondpagtoInfo.cai4 = 0;
                clsCondpagtoInfo.codigo = "";
                clsCondpagtoInfo.codigodespfinanc = "N";
                clsCondpagtoInfo.dadata = "E";
                clsCondpagtoInfo.despfinanc = "N";
                clsCondpagtoInfo.dia1 = 0;
                clsCondpagtoInfo.dia10 = 0;
                clsCondpagtoInfo.dia2 = 0;
                clsCondpagtoInfo.dia3 = 0;
                clsCondpagtoInfo.dia4 = 0;
                clsCondpagtoInfo.dia5 = 0;
                clsCondpagtoInfo.dia6 = 0;
                clsCondpagtoInfo.dia7 = 0;
                clsCondpagtoInfo.dia8 = 0;
                clsCondpagtoInfo.dia9 = 0;
                clsCondpagtoInfo.ipi = "N";
                clsCondpagtoInfo.juros = 0;
                clsCondpagtoInfo.nome = "";
                clsCondpagtoInfo.padrao = "";
                clsCondpagtoInfo.parcelas = 1;
                clsCondpagtoInfo.por1 = 0;
                clsCondpagtoInfo.por10 = 0;
                clsCondpagtoInfo.por2 = 0;
                clsCondpagtoInfo.por3 = 0;
                clsCondpagtoInfo.por4 = 0;
                clsCondpagtoInfo.por5 = 0;
                clsCondpagtoInfo.por6 = 0;
                clsCondpagtoInfo.por7 = 0;
                clsCondpagtoInfo.por8 = 0;
                clsCondpagtoInfo.por9 = 0;
                clsCondpagtoInfo.qtdedias = 0;
                clsCondpagtoInfo.semana = "QDA";
                clsCondpagtoInfo.st ="N";
                clsCondpagtoInfo.totaldias = 0;
                clsCondpagtoInfo.ativo = "S";
                clsCondpagtoInfo.tpag = "99";
            }
            else
            {
                clsCondpagtoInfo = clsCondpagtoBLL.Carregar(id, clsInfo.conexaosqldados);
            }
            CondPagtoCampos(clsCondpagtoInfo);
            CondPagtoFillInfo(clsCondpagtoInfoOld);
            //VerificaParcelas(false);

            tbxCodigo.Select();
            tbxCodigo.SelectAll();
        }

        private void CondPagtoCampos(clsCondpagtoInfo _info)
        {
            id = _info.id;

            if (_info.aplica_depto == "V")
            {
                rbnAplica_Depto_Vendas.Checked = true;
            }
            else
            {
                rbnAplica_Depto_Todos.Checked = true;
            }
            tbxCodigo.Text = _info.codigo.Trim();
            tbxNome.Text = _info.nome;
            if (_info.ipi == "S")
            {
                cbxIpi.Checked = true;
                cbxIpi.Text = "O IPI será cobrado no 1° Vencimento ";
            }
            else
            {
                cbxIpi.Checked = false;
                cbxIpi.Text = "O IPI será distribuiido nas parcelas ";
            }
            if (_info.despfinanc == "S")
            {
                cbxDespefinanc.Checked = true;
                cbxDespefinanc.Text = "Deverá ser cobrado a Despesa Financeira ";
            }
            else
            {
                cbxDespefinanc.Checked = false;
                cbxDespefinanc.Text = "Não deve ser cobrado a Despesa Financeira ";
            }

            tbxParcelas.Text = _info.parcelas.ToString();
            qtdeparcold = clsParser.Int32Parse(_info.parcelas.ToString());
            tbxQtdedias.Text = _info.qtdedias.ToString();
            qtdediasold = clsParser.Int32Parse(_info.qtdedias.ToString());
            tbxDia1.Text = _info.dia1.ToString();
            tbxDia2.Text = _info.dia2.ToString();
            tbxDia3.Text = _info.dia3.ToString();
            tbxDia4.Text = _info.dia4.ToString();
            tbxDia5.Text = _info.dia5.ToString();
            tbxDia6.Text = _info.dia6.ToString();
            tbxDia7.Text = _info.dia7.ToString();
            tbxDia8.Text = _info.dia8.ToString();
            tbxDia9.Text = _info.dia9.ToString();
            tbxDia10.Text = _info.dia10.ToString();
            tbxPor1.Text = _info.por1.ToString("N2");
            tbxPor2.Text = _info.por2.ToString("N2");
            tbxPor3.Text = _info.por3.ToString("N2");
            tbxPor4.Text = _info.por4.ToString("N2");
            tbxPor5.Text = _info.por5.ToString("N2");
            tbxPor6.Text = _info.por6.ToString("N2");
            tbxPor7.Text = _info.por7.ToString("N2");
            tbxPor8.Text = _info.por8.ToString("N2");
            tbxPor9.Text = _info.por9.ToString("N2");
            tbxPor10.Text = _info.por10.ToString("N2");
            rbnSemanaQDA.Checked = (_info.semana == "QDA");
            rbnSemanaSEG.Checked = (_info.semana == "SEG");
            rbnSemanaTER.Checked = (_info.semana == "TER");
            rbnSemanaQUA.Checked = (_info.semana == "QUA");
            rbnSemanaQUI.Checked = (_info.semana == "QUI");
            rbnSemanaSEX.Checked = (_info.semana == "SEX");
            rbnSemanaSAB.Checked = (_info.semana == "SAB");
            rbnSemanaDOM.Checked = (_info.semana == "DOM");
            rbnSemanaDIU.Checked = (_info.semana == "DIU");
            rbnDadataA.Checked = (_info.dadata == "A");
            rbnDadataD.Checked = (_info.dadata == "D");
            rbnDadataE.Checked = (_info.dadata == "E");
            rbnDadataL.Checked = (_info.dadata == "L");
            rbnDadataM.Checked = (_info.dadata == "M");
            rbnDadataQ.Checked = (_info.dadata == "Q");
            rbnDadataS.Checked = (_info.dadata == "S");
            tbxCai1.Text = _info.cai1.ToString();
            caiold = clsParser.Int32Parse(_info.cai1.ToString());
            tbxCai2.Text = _info.cai2.ToString();
            tbxCai3.Text = _info.cai3.ToString();
            tbxCai4.Text = _info.cai4.ToString();
            if (_info.st == "S")
            {
                cbxSt.Checked = true;
                cbxSt.Text = "Subst.Tributária será cobrada no 1º vencimento ";
            }
            else
            {
                cbxSt.Checked = false;
                cbxSt.Text = "Subst.Tributária será distribuida nas parcelas ";
            }
            tbxDataemissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            tbxJuros.Text = _info.juros.ToString("N2");

            if (_info.ativo == "S")
            {
                chkAtivo.Checked = true;
            }
            else
            {
                chkAtivo.Checked = false;
            }
            cbxtPag.SelectedIndex = clsVisual.SelecionarIndex(_info.tpag, 2, cbxtPag);

        }

        private void CondPagtoFillInfo(clsCondpagtoInfo _info)
        {
            _info.id = id;
            if (rbnAplica_Depto_Todos.Checked == true)
            {
                _info.aplica_depto = "T";
            }
            else
            {
                _info.aplica_depto = "V";
            }
            _info.codigo = tbxCodigo.Text.Trim();
            _info.nome = tbxNome.Text;
            if (cbxIpi.Checked)
                _info.ipi = "S";
            else
                _info.ipi = "N";

            if (cbxDespefinanc.Checked)
                _info.despfinanc = "S";
            else
                _info.despfinanc = "N";

            _info.parcelas = clsParser.Int32Parse(tbxParcelas.Text);
            _info.qtdedias = clsParser.Int32Parse(tbxQtdedias.Text);
            _info.dia1 = clsParser.Int32Parse(tbxDia1.Text);
            _info.dia2 = clsParser.Int32Parse(tbxDia2.Text);
            _info.dia3 = clsParser.Int32Parse(tbxDia3.Text);
            _info.dia4 = clsParser.Int32Parse(tbxDia4.Text);
            _info.dia5 = clsParser.Int32Parse(tbxDia5.Text);
            _info.dia6 = clsParser.Int32Parse(tbxDia6.Text);
            _info.dia7 = clsParser.Int32Parse(tbxDia7.Text);
            _info.dia8 = clsParser.Int32Parse(tbxDia8.Text);
            _info.dia9 = clsParser.Int32Parse(tbxDia9.Text);
            _info.dia10 = clsParser.Int32Parse(tbxDia10.Text);
            _info.por1 = clsParser.DecimalParse(tbxPor1.Text);
            _info.por2 = clsParser.DecimalParse(tbxPor2.Text);
            _info.por3 = clsParser.DecimalParse(tbxPor3.Text);
            _info.por4 = clsParser.DecimalParse(tbxPor4.Text);
            _info.por5 = clsParser.DecimalParse(tbxPor5.Text);
            _info.por6 = clsParser.DecimalParse(tbxPor6.Text);
            _info.por7 = clsParser.DecimalParse(tbxPor7.Text);
            _info.por8 = clsParser.DecimalParse(tbxPor8.Text);
            _info.por9 = clsParser.DecimalParse(tbxPor9.Text);
            _info.por10 = clsParser.DecimalParse(tbxPor10.Text);
            if (rbnSemanaDOM.Checked)
                _info.semana = "DOM";
            else if (rbnSemanaSEG.Checked)
                _info.semana = "SEG";
            else if (rbnSemanaTER.Checked)
                _info.semana = "TER";
            else if (rbnSemanaQUA.Checked)
                _info.semana = "QUA";
            else if (rbnSemanaQUI.Checked)
                _info.semana = "QUI";
            else if (rbnSemanaSEX.Checked)
                _info.semana = "SEX";
            else if (rbnSemanaSAB.Checked)
                _info.semana = "SAB";
            else if (rbnSemanaDIU.Checked)
                _info.semana = "DIU";
            else
                _info.semana = "QDA";

            if (rbnDadataA.Checked) _info.dadata = "A";
            else if (rbnDadataD.Checked) _info.dadata = "D";
            else if (rbnDadataE.Checked) _info.dadata = "E";
            else if (rbnDadataL.Checked) _info.dadata = "L";
            else if (rbnDadataM.Checked) _info.dadata = "M";
            else if (rbnDadataQ.Checked) _info.dadata = "Q";
            else if (rbnDadataS.Checked) _info.dadata = "S";

            _info.cai1 = clsParser.Int32Parse(tbxCai1.Text);
            _info.cai2 = clsParser.Int32Parse(tbxCai2.Text);
            _info.cai3 = clsParser.Int32Parse(tbxCai3.Text);
            _info.cai4 = clsParser.Int32Parse(tbxCai4.Text);

           
            _info.padrao = "S";
            

            _info.codigodespfinanc = "";

            if (cbxSt.Checked == true)
            {
                _info.st = "S";
            }
            else
            {
                _info.st = "N";
            }
            _info.juros = clsParser.DecimalParse(tbxJuros.Text);

            if (chkAtivo.Checked)
            {
                _info.ativo = "S";
            }
            else
            {
                _info.ativo = "N";
            }
            _info.tpag = cbxtPag.Text.Substring(0, 2);
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

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void ControlKeyDownNumero(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownNumero((TextBox)sender, e);
        }

        private void TrataCampos(Control ctl)
        {
            if (clsInfo.znomegrid == btnIdFornecedor.Name)
            {
                idFornecedor = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                tbxClienteCognome.Text = clsInfo.zrow.Cells["COGNOME"].Value.ToString().Trim();
                tbxObservacao.Select();
                tbxObservacao.SelectAll();
                clsInfo.znomegrid = "";
            }
            tbxDocumento.Text = clsParser.Int32Parse(tbxDocumento.Text).ToString();

            if (clsParser.Int32Parse(tbxParcelas.Text) > 10)
            {
                gbxDias.Enabled = false;
                tbxQtdedias.Enabled = true;
                if (ctl.Name == tbxParcelas.Name)
                {
                    tbxQtdedias.Select();
                    tbxQtdedias.SelectAll();
                }

                tbxDia1.Text = tbxDia2.Text = tbxDia3.Text = tbxDia4.Text = tbxDia5.Text = "0";
                tbxDia6.Text = tbxDia7.Text = tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                tbxPor1.Text = tbxPor2.Text = tbxPor3.Text = tbxPor4.Text = tbxPor5.Text = "0";
                tbxPor6.Text = tbxPor7.Text = tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = "0";

            }
            else
            {
                tbxQtdedias.Enabled = false;
                
                tbxQtdedias.Text = "0";
                if (clsParser.Int32Parse(tbxParcelas.Text) == 0)
                {
                    tbxParcelas.Text = "1";
                }
                gbxDias.Enabled = true;
                if (ctl.Name == tbxParcelas.Name)
                {
                    tbxDia1.Select();
                    tbxDia1.SelectAll();
                }
                if (clsParser.Int32Parse(tbxParcelas.Text) > 9)
                {
                    tbxDia1.Enabled = tbxDia2.Enabled = tbxDia3.Enabled = tbxDia4.Enabled = tbxDia5.Enabled = tbxDia6.Enabled = tbxDia7.Enabled = tbxDia8.Enabled = tbxDia9.Enabled = tbxDia10.Enabled = true;
                    tbxPor1.Enabled = tbxPor2.Enabled = tbxPor3.Enabled = tbxPor4.Enabled = tbxPor5.Enabled = tbxPor6.Enabled = tbxPor7.Enabled = tbxPor8.Enabled = tbxPor9.Enabled = tbxPor10.Enabled = true; 
                }
                else if (clsParser.Int32Parse(tbxParcelas.Text) > 8)
                {
                    tbxDia1.Enabled = tbxDia2.Enabled = tbxDia3.Enabled = tbxDia4.Enabled = tbxDia5.Enabled = tbxDia6.Enabled = tbxDia7.Enabled = tbxDia8.Enabled = tbxDia9.Enabled = true; 
                    tbxPor1.Enabled = tbxPor2.Enabled = tbxPor3.Enabled = tbxPor4.Enabled = tbxPor5.Enabled = tbxPor6.Enabled = tbxPor7.Enabled = tbxPor8.Enabled = tbxPor9.Enabled = true;  
                    tbxDia10.Enabled = false;
                    tbxDia10.Text = "0";
                    tbxPor10.Enabled = false;
                    tbxPor10.Text = clsParser.DecimalParse("0").ToString("N2");
                }
                else if (clsParser.Int32Parse(tbxParcelas.Text) > 7)
                {
                    tbxDia1.Enabled = tbxDia2.Enabled = tbxDia3.Enabled = tbxDia4.Enabled = tbxDia5.Enabled = tbxDia6.Enabled = tbxDia7.Enabled = tbxDia8.Enabled = true;
                    tbxPor1.Enabled = tbxPor2.Enabled = tbxPor3.Enabled = tbxPor4.Enabled = tbxPor5.Enabled = tbxPor6.Enabled = tbxPor7.Enabled = tbxPor8.Enabled = true;
                    tbxDia9.Enabled = tbxDia10.Enabled = false;
                    tbxDia9.Text = tbxDia10.Text = "0";
                    tbxPor9.Enabled =tbxPor10.Enabled = false;
                    tbxPor9.Text = tbxPor10.Text = clsParser.DecimalParse("0").ToString("N2");
                }
                else if (clsParser.Int32Parse(tbxParcelas.Text) > 6)
                {
                    tbxDia1.Enabled = tbxDia2.Enabled = tbxDia3.Enabled = tbxDia4.Enabled = tbxDia5.Enabled = tbxDia6.Enabled = tbxDia7.Enabled = true;
                    tbxPor1.Enabled = tbxPor2.Enabled = tbxPor3.Enabled = tbxPor4.Enabled = tbxPor5.Enabled = tbxPor6.Enabled = tbxPor7.Enabled = true;
                    tbxDia8.Enabled = tbxDia9.Enabled = tbxDia10.Enabled = false;
                    tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                    tbxPor8.Enabled = tbxPor9.Enabled = tbxPor10.Enabled = false;
                    tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = clsParser.DecimalParse("0").ToString("N2");
                }
                else if (clsParser.Int32Parse(tbxParcelas.Text) > 5)
                {
                    tbxDia1.Enabled = tbxDia2.Enabled = tbxDia3.Enabled = tbxDia4.Enabled = tbxDia5.Enabled = tbxDia6.Enabled = true;
                    tbxPor1.Enabled = tbxPor2.Enabled = tbxPor3.Enabled = tbxPor4.Enabled = tbxPor5.Enabled = tbxPor6.Enabled = true;
                    tbxDia7.Enabled = tbxDia8.Enabled = tbxDia9.Enabled = tbxDia10.Enabled = false;
                    tbxDia7.Text = tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                    tbxPor7.Enabled = tbxPor8.Enabled = tbxPor9.Enabled = tbxPor10.Enabled = false;
                    tbxPor7.Text = tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = clsParser.DecimalParse("0").ToString("N2");
                }
                else if (clsParser.Int32Parse(tbxParcelas.Text) > 4)
                {
                    tbxDia1.Enabled = tbxDia2.Enabled = tbxDia3.Enabled = tbxDia4.Enabled = tbxDia5.Enabled = true;
                    tbxPor1.Enabled = tbxPor2.Enabled = tbxPor3.Enabled = tbxPor4.Enabled = tbxPor5.Enabled =  true;
                    tbxDia6.Enabled = tbxDia7.Enabled = tbxDia8.Enabled = tbxDia9.Enabled = tbxDia10.Enabled = false;
                    tbxDia6.Text = tbxDia7.Text = tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                    tbxPor6.Enabled = tbxPor7.Enabled = tbxPor8.Enabled = tbxPor9.Enabled = tbxPor10.Enabled = false;
                    tbxPor6.Text = tbxPor7.Text = tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = clsParser.DecimalParse("0").ToString("N2");
                }
                else if (clsParser.Int32Parse(tbxParcelas.Text) > 3)
                {
                    tbxDia1.Enabled = tbxDia2.Enabled = tbxDia3.Enabled = tbxDia4.Enabled =  true;
                    tbxPor1.Enabled = tbxPor2.Enabled = tbxPor3.Enabled = tbxPor4.Enabled =  true;
                    tbxDia5.Enabled = tbxDia6.Enabled = tbxDia7.Enabled = tbxDia8.Enabled = tbxDia9.Enabled = tbxDia10.Enabled = false;
                    tbxDia5.Text = tbxDia6.Text = tbxDia7.Text = tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                    tbxPor5.Enabled = tbxPor6.Enabled = tbxPor7.Enabled = tbxPor8.Enabled = tbxPor9.Enabled = tbxPor10.Enabled = false;
                    tbxPor5.Text = tbxPor6.Text = tbxPor7.Text = tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = clsParser.DecimalParse("0").ToString("N2");
                }
                else if (clsParser.Int32Parse(tbxParcelas.Text) > 2)
                {
                    tbxDia1.Enabled = tbxDia2.Enabled = tbxDia3.Enabled = true;
                    tbxPor1.Enabled = tbxPor2.Enabled = tbxPor3.Enabled = true;
                    tbxDia4.Enabled = tbxDia5.Enabled = tbxDia6.Enabled = tbxDia7.Enabled = tbxDia8.Enabled = tbxDia9.Enabled = tbxDia10.Enabled = false;
                    tbxDia4.Text = tbxDia5.Text = tbxDia6.Text = tbxDia7.Text = tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                    tbxPor4.Enabled = tbxPor5.Enabled = tbxPor6.Enabled = tbxPor7.Enabled = tbxPor8.Enabled = tbxPor9.Enabled = tbxPor10.Enabled = false;
                    tbxPor4.Text = tbxPor5.Text = tbxPor6.Text = tbxPor7.Text = tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = clsParser.DecimalParse("0").ToString("N2");
                }
                else if (clsParser.Int32Parse(tbxParcelas.Text) > 1)
                {
                    tbxDia1.Enabled = tbxDia2.Enabled = true;
                    tbxPor1.Enabled = tbxPor2.Enabled = true;
                    tbxDia3.Enabled = tbxDia4.Enabled = tbxDia5.Enabled = tbxDia6.Enabled = tbxDia7.Enabled = tbxDia8.Enabled = tbxDia9.Enabled = tbxDia10.Enabled = false;
                    tbxDia3.Text = tbxDia4.Text = tbxDia5.Text = tbxDia6.Text = tbxDia7.Text = tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                    tbxPor3.Enabled = tbxPor4.Enabled = tbxPor5.Enabled = tbxPor6.Enabled = tbxPor7.Enabled = tbxPor8.Enabled = tbxPor9.Enabled = tbxPor10.Enabled = false;
                    tbxPor3.Text = tbxPor4.Text = tbxPor5.Text = tbxPor6.Text = tbxPor7.Text = tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = clsParser.DecimalParse("0").ToString("N2");
                }
                else if (clsParser.Int32Parse(tbxParcelas.Text) > 0)
                {
                    tbxDia1.Enabled =  true;
                    tbxPor1.Enabled =  true;
                    tbxDia2.Enabled = tbxDia3.Enabled = tbxDia4.Enabled = tbxDia5.Enabled = tbxDia6.Enabled = tbxDia7.Enabled = tbxDia8.Enabled = tbxDia9.Enabled = tbxDia10.Enabled = false;
                    tbxDia2.Text = tbxDia3.Text = tbxDia4.Text = tbxDia5.Text = tbxDia6.Text = tbxDia7.Text = tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                    tbxPor2.Enabled = tbxPor3.Enabled = tbxPor4.Enabled = tbxPor5.Enabled = tbxPor6.Enabled = tbxPor7.Enabled = tbxPor8.Enabled = tbxPor9.Enabled = tbxPor10.Enabled = false;
                    tbxPor2.Text = tbxPor3.Text = tbxPor4.Text = tbxPor5.Text = tbxPor6.Text = tbxPor7.Text = tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = clsParser.DecimalParse("0").ToString("N2");
                }




                if (clsParser.DecimalParse(tbxPor1.Text) + clsParser.DecimalParse(tbxPor2.Text) +
                    clsParser.DecimalParse(tbxPor3.Text) + clsParser.DecimalParse(tbxPor4.Text) +
                    clsParser.DecimalParse(tbxPor5.Text) + clsParser.DecimalParse(tbxPor6.Text) +
                    clsParser.DecimalParse(tbxPor7.Text) + clsParser.DecimalParse(tbxPor8.Text) +
                    clsParser.DecimalParse(tbxPor9.Text) + clsParser.DecimalParse(tbxPor10.Text) > 100)
                {
                    MessageBox.Show("Porcentagem acima de 100 % - acerte ... ");
                }

            }
            // A contagem será
            if (rbnDadataA.Checked == true || rbnDadataL.Checked == true || rbnDadataD.Checked == true ||
                rbnDadataS.Checked == true ||  rbnDadataQ.Checked == true || rbnDadataM.Checked == true)
            {
                if (clsParser.Int32Parse(tbxParcelas.Text) > 1)
                {
                    MessageBox.Show("Nesta condição de contagem não poderá ter mais de 1 (uma) parcela");
                    tbxParcelas.Text = "1";
                }

            }
            if (ctl.Name == rbnDadataA.Name || ctl.Name == rbnDadataL.Name)
            {
                if (clsParser.Int32Parse(tbxDia1.Text) > 0)
                {
                    MessageBox.Show("Nesta condição a qtde de dias deveria se 0(Zero) !!! ");
                }
            }


            if (ctl.Name == tbxValoripi.Name)
            {
                tbxValoripi.Text = clsParser.DecimalParse(tbxValoripi.Text).ToString("N2");
            }
            else if (ctl.Name == tbxValorst.Name)
            {
                tbxValorst.Text = clsParser.DecimalParse(tbxValorst.Text).ToString("N2");
            }
            else if (ctl.Name == tbxValortotal.Name)
            {
                tbxValortotal.Text = clsParser.DecimalParse(tbxValortotal.Text).ToString("N2");
            }
            else if (ctl.Name == tbxPor1.Name)
            {
                tbxPor1.Text = clsParser.DecimalParse(tbxPor1.Text).ToString("N2");
            }
            else if (ctl.Name == tbxPor2.Name)
            {
                tbxPor2.Text = clsParser.DecimalParse(tbxPor2.Text).ToString("N2");
            }
            else if (ctl.Name == tbxPor3.Name)
            {
                tbxPor3.Text = clsParser.DecimalParse(tbxPor3.Text).ToString("N2");
            }
            else if (ctl.Name == tbxPor4.Name)
            {
                tbxPor4.Text = clsParser.DecimalParse(tbxPor4.Text).ToString("N2");
            }
            else if (ctl.Name == tbxPor5.Name)
            {
                tbxPor5.Text = clsParser.DecimalParse(tbxPor5.Text).ToString("N2");
            }
            else if (ctl.Name == tbxPor6.Name)
            {
                tbxPor6.Text = clsParser.DecimalParse(tbxPor6.Text).ToString("N2");
            }
            else if (ctl.Name == tbxPor7.Name)
            {
                tbxPor7.Text = clsParser.DecimalParse(tbxPor7.Text).ToString("N2");
            }
            else if (ctl.Name == tbxPor8.Name)
            {
                tbxPor8.Text = clsParser.DecimalParse(tbxPor8.Text).ToString("N2");
            }
            else if (ctl.Name == tbxPor9.Name)
            {
                tbxPor9.Text = clsParser.DecimalParse(tbxPor9.Text).ToString("N2");
            }
            else if (ctl.Name == tbxPor10.Name)
            {
                tbxPor10.Text = clsParser.DecimalParse(tbxPor10.Text).ToString("N2");
            }
            else if (ctl.Name == tbxJuros.Name)
            {
                tbxJuros.Text = clsParser.DecimalParse(tbxJuros.Text).ToString("N2");
            }
            tbxCai1.Text = clsParser.Int32Parse(tbxCai1.Text).ToString();
            tbxCai2.Text = clsParser.Int32Parse(tbxCai2.Text).ToString();
            tbxCai3.Text = clsParser.Int32Parse(tbxCai3.Text).ToString();
            tbxCai4.Text = clsParser.Int32Parse(tbxCai4.Text).ToString();
            if (clsParser.Int32Parse(tbxCai1.Text) > 0)
            {
                rbnSemanaDIU.Checked = true;
                gbxSemana.Enabled = false;
            }
            else if (clsParser.Int32Parse(tbxCai2.Text) > 0)
            {
                rbnSemanaDIU.Checked = true;
                gbxSemana.Enabled = false;
            }
            else if (clsParser.Int32Parse(tbxCai3.Text) > 0)
            {
                rbnSemanaDIU.Checked = true;
                gbxSemana.Enabled = false;
            }
            else if (clsParser.Int32Parse(tbxCai4.Text) > 0)
            {
                rbnSemanaDIU.Checked = true;
                gbxSemana.Enabled = false;
            }
            else
            {
                gbxSemana.Enabled = true;
            }
            if (clsParser.Int32Parse(tbxParcelas.Text) > 10)
            {
                tbxDia1.Text = tbxDia2.Text = tbxDia3.Text = tbxDia4.Text = tbxDia5.Text = "0";
                tbxDia6.Text = tbxDia7.Text = tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                tbxPor1.Text = tbxPor2.Text = tbxPor3.Text = tbxPor4.Text = tbxPor5.Text = "0";
                tbxPor6.Text = tbxPor7.Text = tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = "0";
            }
            else if (clsParser.Int32Parse(tbxParcelas.Text) < 2)
            {
                tbxDia2.Text = tbxDia3.Text = tbxDia4.Text = tbxDia5.Text = "0";
                tbxDia6.Text = tbxDia7.Text = tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                tbxPor2.Text = tbxPor3.Text = tbxPor4.Text = tbxPor5.Text = "0";
                tbxPor6.Text = tbxPor7.Text = tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = "0";
            }
            else if (clsParser.Int32Parse(tbxParcelas.Text) < 3)
            {
                tbxDia3.Text = tbxDia4.Text = tbxDia5.Text = "0";
                tbxDia6.Text = tbxDia7.Text = tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                tbxPor3.Text = tbxPor4.Text = tbxPor5.Text = "0";
                tbxPor6.Text = tbxPor7.Text = tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = "0";
            }
            else if (clsParser.Int32Parse(tbxParcelas.Text) < 4)
            {
                tbxDia4.Text = tbxDia5.Text = "0";
                tbxDia6.Text = tbxDia7.Text = tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                tbxPor4.Text = tbxPor5.Text = "0";
                tbxPor6.Text = tbxPor7.Text = tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = "0";
            }
            else if (clsParser.Int32Parse(tbxParcelas.Text) < 5)
            {
                tbxDia5.Text = "0";
                tbxDia6.Text = tbxDia7.Text = tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                tbxPor5.Text = "0";
                tbxPor6.Text = tbxPor7.Text = tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = "0";
            }
            else if (clsParser.Int32Parse(tbxParcelas.Text) < 6)
            {
                tbxDia6.Text = tbxDia7.Text = tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                tbxPor6.Text = tbxPor7.Text = tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = "0";
            }
            else if (clsParser.Int32Parse(tbxParcelas.Text) < 7)
            {
                tbxDia7.Text = tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                tbxPor7.Text = tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = "0";
            }
            else if (clsParser.Int32Parse(tbxParcelas.Text) < 8)
            {
                tbxDia8.Text = tbxDia9.Text = tbxDia10.Text = "0";
                tbxPor8.Text = tbxPor9.Text = tbxPor10.Text = "0";
            }
            else if (clsParser.Int32Parse(tbxParcelas.Text) < 9)
            {
                tbxDia9.Text = tbxDia10.Text = "0";
                tbxPor9.Text = tbxPor10.Text = "0";
            }
            else if (clsParser.Int32Parse(tbxParcelas.Text) < 10)
            {
                tbxDia10.Text = "0";
                tbxPor10.Text = "0";
            }
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            if (clsParser.DecimalParse(tbxPor1.Text) + clsParser.DecimalParse(tbxPor2.Text) +
                clsParser.DecimalParse(tbxPor3.Text) + clsParser.DecimalParse(tbxPor4.Text) +
                clsParser.DecimalParse(tbxPor5.Text) + clsParser.DecimalParse(tbxPor6.Text) +
                clsParser.DecimalParse(tbxPor7.Text) + clsParser.DecimalParse(tbxPor8.Text) +
                clsParser.DecimalParse(tbxPor9.Text) + clsParser.DecimalParse(tbxPor10.Text) > 100)
            {
                MessageBox.Show("Porcentagem acima de 100 % - acerte não ira salvar ... ");
            }
            else
            {
                if (tbxCodigo.Text.Trim().Length > 0)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Falta o codigo da condição de pagamento");
                }
            }
        }


        private void tspPrimeiro_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    id = Int32.Parse(rows[0].Cells[0].Value.ToString());
                    CondpagtoCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index != 0)
                            {
                                id = Int32.Parse(rows[rows.GetPreviousRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                CondpagtoCarregar();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspProximo_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        if (Int32.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            if (row.Index < rows.Count - 1)
                            {
                                id = Int32.Parse(rows[rows.GetNextRow(row.Index, DataGridViewElementStates.None)].Cells[0].Value.ToString());
                                CondpagtoCarregar();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tspUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (HouveModificacoes() == true)
                {
                    if (Salvar() == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (rows != null)
                {
                    id = Int32.Parse(rows[rows.Count - 1].Cells[0].Value.ToString());
                    CondpagtoCarregar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            if (HouveModificacoes() == true)
            {
                tspSalvar.PerformClick();
            }
            else
            {
                this.Close();
            }
        }

        private void frmCondPagto_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsInfo.zrow = null;
        }


        private void frmCondPagto_Shown(object sender, EventArgs e)
        {
            
        }

        private void btnGerarResultado_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Vamos Salvar esta Condição de Pagamento antes de Testar !! ", "Aplisoft",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);

                if (resultado == DialogResult.Yes)
                {
                    CondPagtoSalvar();
                }
                else if (resultado == DialogResult.Cancel)
                {
                    tbxCodigo.Select();
                    tbxCodigo.SelectAll();
                    return;
                }

                clsCondpagtoBLL clsCondpagtoBLL = new clsCondpagtoBLL();

                clsCondpagtoInfo info = new clsCondpagtoInfo();
                //PreencheInfoCondpagto(info);
                if (clsParser.DecimalParse(tbxValortotal.Text) == 0)
                {
                    tbxValortotal.Text = clsParser.DecimalParse("1000").ToString("N2");
                    tbxValorst.Text = clsParser.DecimalParse("100").ToString("N2");
                    tbxValoripi.Text = clsParser.DecimalParse("50").ToString("N2");
                }



                if (clsParser.SqlDateTimeParse(tbxDataemissao.Text).Value != null)
                {
                    if (Application.ProductName.ToString().ToUpper() == "APLIESQUADRIA")
                    {
                        dtTemp = clsFinanceiro.GerarFatura(DateTime.Parse(tbxDataemissao.Text), clsParser.DecimalParse(tbxValortotal.Text),
                                  0, 0, clsParser.DecimalParse(tbxValorst.Text), false, 0, false, 0, clsInfo.zformapagto, id, "S", "S");
                        dgvResultado.DataSource = dtTemp;
                    }
                    else
                    {
                        dtTemp = clsFinanceiro.GerarFatura(DateTime.Parse(tbxDataemissao.Text), clsParser.DecimalParse(tbxValortotal.Text),
                                  0, 0, clsParser.DecimalParse(tbxValorst.Text), false, 0, false, 0, clsInfo.zformapagto, id, "N", "S");
                        dgvResultado.DataSource = dtTemp;
                    }
                }
                else
                {
                    MessageBox.Show("Favor digitar uma data", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbxDataemissao.Select();
                    tbxDataemissao.SelectAll();
                }

                dgvResultado.Columns["POSICAO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvResultado.Columns["POSICAO"].HeaderText = "PI";
                dgvResultado.Columns["POSICAO"].Width = 25;

                dgvResultado.Columns["POSICAOFIM"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvResultado.Columns["POSICAOFIM"].HeaderText = "PF";
                dgvResultado.Columns["POSICAOFIM"].Width = 25;

                dgvResultado.Columns["DATA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvResultado.Columns["DATA"].HeaderText = "Vencimento";
                dgvResultado.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvResultado.Columns["DATA"].Width = 75;

                dgvResultado.Columns["PAGOU"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvResultado.Columns["PAGOU"].HeaderText = "PI";
                dgvResultado.Columns["PAGOU"].Width = 25;
                dgvResultado.Columns["PAGOU"].Visible = false;

                dgvResultado.Columns["VALOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultado.Columns["VALOR"].DefaultCellStyle.Format = "N2";
                dgvResultado.Columns["VALOR"].HeaderText = "Valor";
                dgvResultado.Columns["VALOR"].Width = 90;

                dgvResultado.Columns["VALORCOMISSAO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultado.Columns["VALORCOMISSAO"].DefaultCellStyle.Format = "N2";
                dgvResultado.Columns["VALORCOMISSAO"].HeaderText = "Comissão R.";
                dgvResultado.Columns["VALORCOMISSAO"].Width = 90;
                dgvResultado.Columns["VALORCOMISSAO"].Visible = false;

                dgvResultado.Columns["IDTIPOPAGA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultado.Columns["IDTIPOPAGA"].DefaultCellStyle.Format = "N2";
                dgvResultado.Columns["IDTIPOPAGA"].HeaderText = "idtipopaga";
                dgvResultado.Columns["IDTIPOPAGA"].Width = 0;
                dgvResultado.Columns["IDTIPOPAGA"].Visible = false;

                dgvResultado.Columns["TIPOPAGA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultado.Columns["TIPOPAGA"].HeaderText = "tipopaga";
                dgvResultado.Columns["TIPOPAGA"].Width = 0;
                dgvResultado.Columns["TIPOPAGA"].Visible = false; 

                dgvResultado.Columns["BOLETONRO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultado.Columns["BOLETONRO"].HeaderText = "BOLETONRO";
                dgvResultado.Columns["BOLETONRO"].Width = 0;
                dgvResultado.Columns["BOLETONRO"].Visible = false;

                dgvResultado.Columns["DV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultado.Columns["DV"].HeaderText = "DV";
                dgvResultado.Columns["DV"].Width = 0;
                dgvResultado.Columns["DV"].Visible = false;

                dgvResultado.Columns["CAIXA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultado.Columns["CAIXA"].HeaderText = "Caixa";
                dgvResultado.Columns["CAIXA"].Width = 90;
                dgvResultado.Columns["CAIXA"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tbxCodigo.Select();
                tbxCodigo.SelectAll();
            }
        }

        private void rbnSemanaQDA_CheckedChanged(object sender, EventArgs e)
        {
        }
        private Boolean HouveModificacoes()
        {
            clsCondpagtoInfo = new clsCondpagtoInfo();
            CondPagtoFillInfo(clsCondpagtoInfo);
            if (clsCondpagtoBLL.Equals(clsCondpagtoInfo, clsCondpagtoInfoOld) == false)
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
                CondPagtoSalvar();
            }
            return drt;

        }
        private void CondPagtoSalvar()
        {
            using (TransactionScope tse = new TransactionScope())
            {
                // ###############################
                // Tabela: ORCAMENTO
                clsCondpagtoInfo = new clsCondpagtoInfo();
                CondPagtoFillInfo(clsCondpagtoInfo);



                if (id == 0)
                {
                    id = clsCondpagtoBLL.Incluir(clsCondpagtoInfo, clsInfo.conexaosqldados);                  
                }
                else
                {
                    clsCondpagtoBLL.Alterar(clsCondpagtoInfo, clsInfo.conexaosqldados);
                }
                tse.Complete();
            }
        }

        private void cbxIpi_Click(object sender, EventArgs e)
        {
            if (cbxIpi.Checked == true)
            {
                cbxIpi.Text = "O IPI será cobrado no 1° Vencimento ";
            }
            else
            {
                cbxIpi.Text = "O IPI será distribuiido nas parcelas ";
            }
        }
        private void cbxSt_Click(object sender, EventArgs e)
        {
            if (cbxSt.Checked == true)
            {
                cbxSt.Text = "Subst.Tributária será cobrada no 1º vencimento ";
            }
            else
            {
                cbxSt.Text = "Subst.Tributária será distribuida nas parcelas ";
            }
        }

        private void cbxDespefinanc_Click(object sender, EventArgs e)
        {
            if (cbxDespefinanc.Checked == true)
            {
                cbxDespefinanc.Text = "Deverá ser cobrado a Despesa Financeira ";
            }
            else
            {
                cbxDespefinanc.Text = "Não deve ser cobrado a Despesa Financeira ";
            }
        }

        private void rbnSemanaSAB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkAtivo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnIdFornecedor_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdFornecedor.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", idFornecedor);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);

        }

        private void tspCobrar_Click(object sender, EventArgs e)
        {
            gbxCtasPagar.Visible = true;
            tbxDocumento.Select();
        }

        private void frmCondPagto_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult drt;
            drt = MessageBox.Show("Deseja Incluir os Boletos no Contas a Pagar  ?", Application.CompanyName, MessageBoxButtons.YesNoCancel);
            if (drt == DialogResult.Yes)
            {
                clsPagarInfo = new clsPagarInfo();
                clsPagarBLL = new clsPagarBLL();

                clsPagarInfo.atevencimento = "S";
                clsPagarInfo.baixa = "N";
                clsPagarInfo.boleto = "";
                clsPagarInfo.boletonro = 0;
                clsPagarInfo.chegou = "N";
                clsPagarInfo.datalanca = DateTime.Now;
                clsPagarInfo.despesapublica = "N";
                clsPagarInfo.duplicata = 0;
                clsPagarInfo.dv = "";
                clsPagarInfo.emissao = DateTime.Now;
                clsPagarInfo.emitente = clsInfo.zusuario;
                clsPagarInfo.filial = clsInfo.zfilial;
                clsPagarInfo.id = 0;
                clsPagarInfo.idbanco = clsInfo.zbanco;
                clsPagarInfo.idbancoint = clsInfo.zbancoint;
                clsPagarInfo.idcentrocusto = clsInfo.zcentrocustos;
                clsPagarInfo.idcodigoctabil = clsInfo.zcontacontabil;
                clsPagarInfo.iddocumento = clsInfo.zdocumento;
                clsPagarInfo.idformapagto = clsInfo.zformapagto;
                clsPagarInfo.idfornecedor = clsInfo.zempresaclienteid;
                clsPagarInfo.idhistorico = clsInfo.zhistoricos;
                clsPagarInfo.idnotafiscal = clsInfo.znfcompra;
                clsPagarInfo.idpagarnfe = clsInfo.znfcomprapagar;
                clsPagarInfo.idsitbanco = clsInfo.zsituacaotitulo;
                clsPagarInfo.imprimir = "N";
                clsPagarInfo.observa = "";
                clsPagarInfo.posicao = 1;
                clsPagarInfo.posicaofim = 1;
                clsPagarInfo.setor = "N";
                clsPagarInfo.valor = 0;
                clsPagarInfo.valorbaixando = 0;
                clsPagarInfo.valordesconto = 0;
                clsPagarInfo.valordevolvido = 0;
                clsPagarInfo.valorjuros = 0;
                clsPagarInfo.valorliquido = 0;
                clsPagarInfo.valormulta = 0;
                clsPagarInfo.valorpago = 0;
                clsPagarInfo.vencimento = DateTime.Now;
                clsPagarInfo.vencimentoprev = DateTime.Now;
                // looping nas parcelas geradas
                foreach (DataRow row in  dtTemp.Rows)
                {
                    clsPagarInfo.emissao = DateTime.Parse(tbxDataemissao.Text);
                    clsPagarInfo.duplicata = clsParser.Int32Parse(tbxDocumento.Text);
                    clsPagarInfo.posicao = clsParser.Int32Parse(row["posicao"].ToString());
                    clsPagarInfo.posicaofim = clsParser.Int32Parse(row["posicaofim"].ToString());
                    clsPagarInfo.valor = clsParser.DecimalParse(row["valor"].ToString());
                    clsPagarInfo.valorliquido = clsParser.DecimalParse(row["valor"].ToString());
                    clsPagarInfo.vencimento = clsParser.SqlDateTimeParse(row["data"].ToString()).Value;
                    clsPagarInfo.vencimentoprev = clsParser.SqlDateTimeParse(row["data"].ToString()).Value;
                    clsPagarInfo.observa = tbxObservacao.Text;
                    clsPagarInfo.id = clsPagarBLL.Incluir(clsPagarInfo, clsInfo.conexaosqldados);

                }
                gbxCtasPagar.Visible = false;
                tbxDocumento.Text = "";
                tbxObservacao.Text = "";


            }
        }
        /*

public Boolean MovimentaCondpagto()
{
clsCondpagtoBLL clsCondpagtoBLL = new clsCondpagtoBLL();
clsCondpagtoInfo clsCondpagtoInfo = new clsCondpagtoInfo();
PreencheInfoCondpagto(clsCondpagtoInfo);
/*
if (clsCondpagtoBLL.ComparaInfo(clsCondpagtoInfoOld, clsCondpagtoInfo) == false)
{
DialogResult drt;
drt = MessageBox.Show("O registro foi alterado." + Environment.NewLine + "Deseja Salvar antes de continuar?", "Aplisoft", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
if (drt == DialogResult.Yes)
{
SalvarCondpagto();
}
else if (drt == DialogResult.Cancel)
{
tbxCodigo.Select();
tbxCodigo.SelectAll();
return false;
}
}

return true;
}

*/
    }
}
