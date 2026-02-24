using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmClienteObserva : Form
    {
        Int32 posicao;
        Int32 id;
        Int32 idcliente;
        Int32 idemitente;
        Int32 idreferencia;
        Int32 idvendedor;

        DataTable dtClienteprospeccaoobserva;

        clsClienteObservaInfo clsClienteObservaInfo;
        clsClienteObservaInfo clsClienteObservaInfoOld;
        clsClienteObservaBLL clsClienteObservaBLL;

        
        

        Boolean cliente = false;

        public frmClienteObserva()
        {
            InitializeComponent();
        }

        public void Init(Int32 _posicao,
                         Int32 _idclienteobserva,
                         DataTable _dtClienteprospeccaoobserva,
                         Boolean _cliente,
                         Int32 _idcliente)
        {
            posicao = _posicao;
            id = _idclienteobserva;
            idcliente = _idcliente;
            dtClienteprospeccaoobserva = _dtClienteprospeccaoobserva;
            cliente = _cliente;

            clsClienteObservaBLL = new clsClienteObservaBLL();

            
            

            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select usuario from usuario where ativo=1", tbxUsuario_Usuario);
        }
        
        private void frmClienteObserva_Load(object sender, EventArgs e)
        {           
            ClienteprospeccaoobservaRegistro();
        }

        void ClienteprospeccaoobservaRegistro()
        {
            if (posicao == 0 && id == 0)
            {
                clsClienteObservaInfo = new clsClienteObservaInfo();
                clsClienteObservaInfo.id = 0;
                clsClienteObservaInfo.contatado = clsInfo.zusuario;
                clsClienteObservaInfo.contatoagenda = "";
                clsClienteObservaInfo.contatofim = "";
                clsClienteObservaInfo.data = DateTime.Now;
                clsClienteObservaInfo.dataagenda = DateTime.MinValue;
                clsClienteObservaInfo.datafim = DateTime.MinValue;
                clsClienteObservaInfo.emitente = clsInfo.zusuario;
                clsClienteObservaInfo.fechada = "N";
                clsClienteObservaInfo.idcliente = idcliente;
                clsClienteObservaInfo.idemitente = clsInfo.zusuarioid;
                clsClienteObservaInfo.idreferencia = 0;
                clsClienteObservaInfo.idvendedor = clsInfo.zusuarioid;
                clsClienteObservaInfo.ligacao = "I";
                clsClienteObservaInfo.observar = "";
                clsClienteObservaInfo.observaragenda = "";
                clsClienteObservaInfo.documento = "";
                lbxFechada.SelectedIndex = 0;
                gbxFinalizado.Enabled = false;
            }
            else
            {
                clsClienteObservaInfo = ClienteprospeccaoobservaCarregar();
                gbxClienteProspeccaoSituacao.Enabled = false;
                gbxClienteProspeccaoConversamos.Enabled = false;
                gbxClienteProspeccaoConsultor.Enabled = false;
                gbxClienteProspeccaoObservacao.Enabled = false;
                tbxUsuario_Usuario.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select USUARIO from USUARIO where ID=" + clsClienteObservaInfo.idvendedor + "");

                if (clsClienteObservaInfo.fechada == "A")
                {
                    lbxFechada.SelectedIndex = 0;
                }
                else if (clsClienteObservaInfo.fechada == "F")
                {
                    lbxFechada.SelectedIndex = 1;
                }
                else if (clsClienteObservaInfo.fechada == "G")
                {
                    lbxFechada.SelectedIndex = 2;
                }
                if (tbxUsuario_Usuario.Text.Trim() != clsInfo.zusuario.Trim())
                {
                    if (clsClienteObservaInfo.fechada != "S")
                    {
                        gbxFinalizado.Enabled = false;
                    }
                }
                gbxClienteProspeccaoAgendar.Enabled = false;
                tspSalvar.Enabled = false;
            }

            clsClienteObservaInfoOld = new clsClienteObservaInfo();
            ClienteprospeccaoobservaCampos(clsClienteObservaInfo);
            ClienteprospeccaoobservaInfo(clsClienteObservaInfoOld);

            if (id == 0)
            {

                if (rbnLigacaoE.Checked == true)
                {
                    rbnLigacaoE.Select();
                }
                else if (rbnLigacaoS.Checked == true)
                {
                    rbnLigacaoS.Select();
                }
                else if (rbnLigacaoA.Checked == true)
                {
                    rbnLigacaoA.Select();
                }
                else
                {
                    rbnLigacaoI.Select();
                }
            }
            else
            {
                lbxFechada.Select();
            }
        }

        private clsClienteObservaInfo ClienteprospeccaoobservaCarregar()
        {
            clsClienteObservaInfo = null;
            DataRow[] rows;

            if (posicao == 0)
            {
                rows = dtClienteprospeccaoobserva.Select("id=" + id.ToString());
            }
            else
            {
                rows = dtClienteprospeccaoobserva.Select("posicao=" + posicao.ToString());                
            }
            foreach (DataRow row in rows)
            {
                clsClienteObservaInfo = new clsClienteObservaInfo();
                clsClienteObservaInfo.contatado = row["contatado"].ToString();
                clsClienteObservaInfo.contatoagenda = row["contatoagenda"].ToString();
                clsClienteObservaInfo.contatofim = row["contatofim"].ToString();
                clsClienteObservaInfo.data = clsParser.SqlDateTimeParse(row["data"].ToString()).Value;
                if (row["dataagenda"].ToString().Length > 0)
                {
                    clsClienteObservaInfo.dataagenda = clsParser.DateTimeParse(row["dataagenda"].ToString());
                }
                else
                {
                    //clsClienteObservaInfo.dataagenda = "";
                    clsClienteObservaInfo.dataagenda = DateTime.MinValue;
                }
                if (row["datafim"].ToString().Length > 0)
                {
                    clsClienteObservaInfo.datafim = clsParser.DateTimeParse(row["datafim"].ToString());
                }
                else
                {
                    clsClienteObservaInfo.datafim = DateTime.MinValue;
                }
                clsClienteObservaInfo.documento = row["documento"].ToString();
                clsClienteObservaInfo.emitente = row["emitente"].ToString();
                clsClienteObservaInfo.fechada = row["fechada"].ToString();
                clsClienteObservaInfo.id = clsParser.Int32Parse(row["id"].ToString());
                clsClienteObservaInfo.idcliente = clsParser.Int32Parse(row["idcliente"].ToString());
                clsClienteObservaInfo.idemitente = clsParser.Int32Parse(row["idemitente"].ToString());
                clsClienteObservaInfo.idreferencia = clsParser.Int32Parse(row["idreferencia"].ToString());
                clsClienteObservaInfo.idvendedor = clsParser.Int32Parse(row["idvendedor"].ToString());
                if (row["ligacao"].ToString().Length > 0)
                {
                    clsClienteObservaInfo.ligacao = row["ligacao"].ToString().Substring(0, 1);
                }
                else
                {
                    clsClienteObservaInfo.ligacao = "E";
                }

                if (clsClienteObservaInfo.ligacao == "E")
                {
                    rbnLigacaoE.Checked = true;
                }
                else if (clsClienteObservaInfo.ligacao == "S")
                {
                    rbnLigacaoS.Checked = true;
                }
                else if (clsClienteObservaInfo.ligacao == "A")
                {
                    rbnLigacaoA.Checked = true;
                }
                else
                {
                    rbnLigacaoI.Checked = true;
                }

                clsClienteObservaInfo.observar = row["observar"].ToString();
                clsClienteObservaInfo.observaragenda = row["observaragenda"].ToString();
            }
            return clsClienteObservaInfo;
        }
        private void frmClienteObserva_Activated(object sender, EventArgs e)
        {
            Lupa();
        }
        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Deseja Salvar e Retornar?", 
                                            Application.CompanyName,
                                            MessageBoxButtons.YesNoCancel, 
                                            MessageBoxIcon.Question,
                                            MessageBoxDefaultButton.Button1);

                if (resultado == DialogResult.Yes)
                {
                    ClienteprospeccaoobservaSalvar();
                }
                else if (resultado == DialogResult.Cancel)
                {
                    tbxData.Select();
                    tbxData.SelectAll();
                    return;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tbxData.Select();
                tbxData.SelectAll();
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClienteProspeccaoConsultor_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnClienteProspeccaoConsultor.Name;
            frmUsuarioPes frmUsuarioPes = new frmUsuarioPes();
            frmUsuarioPes.Init(idvendedor, clsInfo.conexaosqldados);

            clsFormHelper.AbrirForm(frmUsuarioPes, clsInfo.conexaosqldados);
        }

        private void ClienteprospeccaoobservaCampos(clsClienteObservaInfo Info)
        {
            id = Info.id;
            idcliente = Info.idcliente;
            idemitente = Info.idemitente;
            idreferencia = Info.idreferencia;
            idvendedor = Info.idvendedor;

            tbxClienteprospeccao_cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from clienteprospeccao where id=" + idcliente.ToString());
            tbxUsuario_Usuario.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select usuario from usuario where id=" + idvendedor.ToString());

            tbxEmitente.Text = Info.emitente;
            tbxContatado.Text = Info.contatado;
            tbxContatoagenda.Text = Info.contatoagenda;
            tbxContatofim.Text = Info.contatofim;
            tbxData.Text = Info.data.ToString("dd/MM/yyyy HH:mm");

            if (Info.dataagenda != null)
            {
                if (Info.dataagenda >= dtpDataagenda_data.MinDate && Info.dataagenda <= dtpDataagenda_data.MaxDate)
                {
                    dtpDataagenda_data.Value = Info.dataagenda;
                    mtbDataagenda_hora.Text = Info.dataagenda.ToString("HH:mm");
                }
            }

            if (Info.datafim != null)
            {
                tbxDatafim.Text = Info.datafim.ToString("dd/MM/yyyy HH:mm");
            }
            tbxDocumento.Text = Info.documento;
            tbxEmitente.Text = Info.emitente;
            tbxFechada.Text = Info.fechada;
            rbnLigacaoE.Checked = (Info.ligacao.Substring(0,1) == "E");
            rbnLigacaoS.Checked = (Info.ligacao.Substring(0, 1) == "S");
            rbnLigacaoI.Checked = (Info.ligacao.Substring(0, 1) == "I");
            rbnLigacaoA.Checked = (Info.ligacao.Substring(0, 1) == "A");
            tbxObservar.Text = Info.observar;
            tbxObservaragenda.Text = Info.observaragenda;


            //apenas para o ApliEsquadria
            if (Application.ProductName.ToString().ToUpper() == "APLIESQUADRIA")
            {                
                if (Info.idreferencia > 0)
                {
                    lblEdificio.Visible = true;
                    lblApartamento.Visible = true;
                    tbxnroEdificio.Visible = true;
                    tbxEdificioNome.Visible = true;
                    tbxApartamento.Visible = true;

                    tbxnroEdificio.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                        "select edificio.numero from edificio inner join edificioapartamento on " +
                        "edificio.id = edificioapartamento.idedificio where " +
                        "edificioapartamento.id= " + Info.idreferencia, "0");

                    tbxEdificioNome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                            "select edificio.nome from edificio inner join edificioapartamento on " +
                            "edificio.id = edificioapartamento.idedificio where " +
                            "edificioapartamento.id= " + Info.idreferencia, "0");

                    tbxApartamento.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados,
                        "select nroapto from edificioapartamento where " +
                        "id= " + Info.idreferencia, "0");
                }
                else
                {
                    lblEdificio.Visible = false;
                    lblApartamento.Visible = false;
                    tbxnroEdificio.Visible = false;
                    tbxEdificioNome.Visible = false;
                    tbxApartamento.Visible = false;
                }
            }
            else
            {
                lblEdificio.Visible = false;
                lblApartamento.Visible = false;
                tbxnroEdificio.Visible = false;
                tbxEdificioNome.Visible = false;                
                tbxApartamento.Visible = false;
            }
        }

        private void ClienteprospeccaoobservaInfo(clsClienteObservaInfo Info)
        {
            Info.id = id;
            Info.idcliente = idcliente;
            Info.idemitente = idemitente;
            Info.idreferencia = idreferencia;
            Info.idvendedor = idvendedor;

            Info.emitente = tbxEmitente.Text;
            Info.contatado = tbxContatado.Text;
            Info.contatoagenda = tbxContatoagenda.Text;
            Info.contatofim = tbxContatofim.Text;
            Info.data = clsParser.DateTimeParse(tbxData.Text);
            if (rbnAgendarS.Checked == true)
            {
                Info.dataagenda = clsParser.DateTimeParse(dtpDataagenda_data.Value.ToString("dd/MM/yyyy") + " " + mtbDataagenda_hora.Text);
            }
            else
            {
                Info.dataagenda = DateTime.MinValue;
            }

            if (Info.datafim != null)
            {
                Info.datafim = clsParser.DateTimeParse(tbxDatafim.Text);
            }
            else
            {
                if (tbxDatafim.Text != "")
                {
                    Info.datafim = clsParser.DateTimeParse(tbxDatafim.Text);
                }
                else
                {
                    Info.datafim = DateTime.MinValue;
                }
            }

            Info.emitente = tbxEmitente.Text;
            Info.documento = tbxDocumento.Text;
            Info.fechada = tbxFechada.Text;

            if (rbnLigacaoE.Checked == true)
            {
                Info.ligacao = "E";
            }
            else if (rbnLigacaoS.Checked == true)
            {
                Info.ligacao = "S";
            }
            else if (rbnLigacaoA.Checked == true)
            {
                Info.ligacao = "A";
            }
            else
            {
                Info.ligacao = "I";
            }

            Info.observar = tbxObservar.Text;
            Info.observaragenda = tbxObservaragenda.Text;
        }

        private void ClienteprospeccaoobservaSalvar()
        {
            tbxFechada.Text = lbxFechada.Text.Substring(0, 1);

            clsClienteObservaInfo = new clsClienteObservaInfo();
            ClienteprospeccaoobservaInfo(clsClienteObservaInfo);
            clsClienteObservaBLL.VerificaInfo(clsClienteObservaInfo);

            if (cliente == true)
            {
                DataRow row;
                DataRow[] rows;

                if (posicao == 0 && id == 0)
                {
                    row = dtClienteprospeccaoobserva.NewRow();
                }
                else
                {
                    rows = dtClienteprospeccaoobserva.Select("posicao=" + posicao.ToString());
                    row = rows[0];
                }

                row["id"] = id;
                row["data"] = clsClienteObservaInfo.data;
                row["emitente"] = clsClienteObservaInfo.emitente;
                row["destinatario"] = tbxUsuario_Usuario.Text;
                row["documento"] = tbxDocumento.Text;

                if (rbnLigacaoE.Checked == true) clsClienteObservaInfo.ligacao = "E";
                else if (rbnLigacaoS.Checked == true) clsClienteObservaInfo.ligacao = "S";
                else if (rbnLigacaoA.Checked == true) clsClienteObservaInfo.ligacao = "A";
                else clsClienteObservaInfo.ligacao = "I";

                if (rbnLigacaoE.Checked == true)
                {
                    row["ligacao"] = "E";
                }
                else if (rbnLigacaoS.Checked == true)
                {
                    row["ligacao"] ="S";
                }
                else if (rbnLigacaoA.Checked == true)
                {
                    row["ligacao"] = "A";
                }
                else
                {
                    row["ligacao"] = "I";
                }


                //row["contatado"] = clsClienteObservaInfo.contatado;
                //row["vendedor"] = tbxUsuario_Usuario.Text;
                row["observar"] = clsClienteObservaInfo.observar;
                row["fechada"] = clsClienteObservaInfo.fechada;
                row["idcliente"] = clsClienteObservaInfo.idcliente;
                row["idreferencia"] = clsClienteObservaInfo.idreferencia;
                row["idvendedor"] = clsClienteObservaInfo.idvendedor;
                if (clsClienteObservaInfo.dataagenda != null)
                {
                    row["dataagenda"] = clsClienteObservaInfo.dataagenda;
                }
                row["observaragenda"] = clsClienteObservaInfo.observaragenda;
                row["contatado"] = clsClienteObservaInfo.contatado;
                row["contatoagenda"] = clsClienteObservaInfo.contatoagenda;
                 row["contatofim"] = clsClienteObservaInfo.contatofim;
                if (clsClienteObservaInfo.datafim != null)
                {
                    row["datafim"] = clsClienteObservaInfo.datafim;
                }
                row["idemitente"] = clsClienteObservaInfo.idemitente;
                row["idreferencia"] = clsClienteObservaInfo.idreferencia;

                if (posicao == 0 && id == 0)
                {
                    row["posicao"] = dtClienteprospeccaoobserva.Rows.Count + 1;
                    dtClienteprospeccaoobserva.Rows.Add(row);
                }
            }
            else
            {
                if (id == 0)
                {
                    id = clsClienteObservaBLL.Incluir(clsClienteObservaInfo, clsInfo.conexaosqldados);
                    clsClienteObservaInfo.id = id;
                }
                else
                {
                    clsClienteObservaBLL.Alterar(clsClienteObservaInfo, clsInfo.conexaosqldados);
                }
            }

        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            ClienteprospeccaoobservaCampos(sender);
            Lupa();
            if (((Control)sender).Name == lbxFechada.Name)
            {
                btnOK.Select();
            }

        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);

            if (((Control)sender).Name == tbxObservar.Name && e.KeyCode == Keys.Enter)
            {
                //tbxObservar.Select(tbxObservar.Text.Length - 1, 0);
                e.SuppressKeyPress = true;
            }
            else if (((Control)sender).Name == tbxObservaragenda.Name && e.KeyCode == Keys.Enter)
            {
                //tbxObservaragenda.Select(tbxObservar.Text.Length - 1, 0);
                e.SuppressKeyPress = true;
            }
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }
        private void Lupa()
        {
            if (clsInfo.zrow != null)
            {
                if (clsInfo.znomegrid == btnClienteProspeccaoConsultor.Name)
                {
                    idvendedor = (Int32)clsInfo.zrow.Cells["ID"].Value;
                    tbxUsuario_Usuario.Text = clsInfo.zrow.Cells["USUARIO"].Value.ToString();
                    tbxUsuario_Usuario.Select();
                }
                clsInfo.znomegrid = "";
                clsInfo.zrow = null;
            }

            gbxClienteProspeccaoDataHora1.Visible = (rbnAgendarS.Checked == true);
            gbxClienteProspeccaoAgendamento.Visible = (rbnAgendarS.Checked == true);

            if (id == 0)
            {
                if (rbnAgendarS.Checked == true)
                {
                    if (dtpDataagenda_data.Value == dtpDataagenda_data.MinDate)
                    {
                        dtpDataagenda_data.Value = DateTime.Now;
                        mtbDataagenda_hora.Text = "10:00";
                    }
                }
                else
                {
                    dtpDataagenda_data.Value = dtpDataagenda_data.MinDate;
                    mtbDataagenda_hora.Text = "";
                }
            }

            /*
            if (rbnAgendarS.Checked == true)
            {
                gbxClienteProspeccaoDataHora1.Visible = true;
                gbxClienteProspeccaoAgendar.Visible = true;
                gbxClienteProspeccaoAgendamento.Visible = true;

                //DateTime dDataAgenda = DateTime.Parse(dtpDataagenda_data.Value.ToString("dd/MM/yyyy") + " " + mtbDataagenda_hora.Text);
                tbxDataAgenda.Text = dtpDataagenda_data.Value.ToString("dd/MM/yyyy") + " " + mtbDataagenda_hora.Text;

                if (mtbDataagenda_hora.Text.Length == 3)
                {
                    mtbDataagenda_hora.Text = "10:00";
                }
            }*/

            if (rbnLigacaoE.Checked == true)
            {
                gbxClienteProspeccaoConversamos.Text = "Nome de quem ligou";
            }
            else if (rbnLigacaoS.Checked == true)
            {
                gbxClienteProspeccaoConversamos.Text = "Com quem voce conversou";
            }
            else if (rbnLigacaoA.Checked == true)
            {
                gbxClienteProspeccaoConversamos.Text = "Com quem foi Agendado";
            }
            else
            {
                gbxClienteProspeccaoConversamos.Text = "Com quem voce conversou";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tbxContatofim.Text == "")
            {
                tbxDatafim.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                tbxContatofim.Text = clsInfo.zusuario;
                //lbxFechada.SelectedItem = clsVisual.SelecionarIndex("F", 1, lbxFechada);
                lbxFechada.SelectedIndex = 1;
                tbxFechada.Text = "F";
                btnOK.Enabled = false;
            }
            else
            {
                tbxDatafim.Text = "";
                tbxContatofim.Text = "";
                //lbxFechada.SelectedItem = clsVisual.SelecionarIndex("A", 1, lbxFechada);
                lbxFechada.SelectedIndex = 0;
                tbxFechada.Text = "A";
            }

            DialogResult resultado;
            resultado = MessageBox.Show("Deseja realmente Fechar e Salvar ?",
                                        Application.CompanyName,
                                        MessageBoxButtons.YesNoCancel,
                                        MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button1);

            if (resultado == DialogResult.Yes)
            {
                ClienteprospeccaoobservaSalvar();
            }

            this.Close();
        }

        private void ClienteprospeccaoobservaCampos(object _ctrl)
        {
            if (_ctrl is TextBox)
            {
                if (((TextBox)_ctrl).Name == tbxUsuario_Usuario.Name)
                {
                    //idvendedor = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "USUARIO", "ID", "USUARIO", tbxUsuario_Usuario.Text));
                    idvendedor = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from USUARIO where USUARIO='" + tbxUsuario_Usuario.Text + "' ","0"));
                    if (idvendedor == 0 && tbxUsuario_Usuario.Text != "")
                    {
                        ((TextBox)_ctrl).Text = "";
                        ((TextBox)_ctrl).Select();
                        ((TextBox)_ctrl).SelectAll();
                    }
                }
            }
            



        }

        private void tbxContatado_TextChanged(object sender, EventArgs e)
        {

        }

        private void rbnLigacaoS_Click(object sender, EventArgs e)
        {
            Lupa();
        }

        private void rbnLigacaoE_Click(object sender, EventArgs e)
        {
            Lupa();
        }

        private void rbnLigacaoA_Click(object sender, EventArgs e)
        {
            Lupa();
        }

        private void rbnLigacaoI_Click(object sender, EventArgs e)
        {
            Lupa();
        }

        private void lbxFechada_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbxFechada.Text = lbxFechada.SelectedItem.ToString().Substring(0, 1);
        }

        private void tspTool2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void frmClienteprospeccaoobserva_Click(object sender, EventArgs e)
        {
            Lupa();
        }

        private void rbnLigacaoE_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbnAgendarS_CheckedChanged(object sender, EventArgs e)
        {
            Lupa();
        }


    }
}
