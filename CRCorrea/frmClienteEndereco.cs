using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmClienteEndereco : Form
    {
        Int32 posicao;
        Int32 id;
        Int32 idcliente;
        Int32 idestado;
        Int32 idzona;
        String Cognome;
        String Nome;
        String Cgc;
        String Cep;
        String Endereco;
        String Numeroend;
        String Andar;
        String Tipocomple;
        String Bairro;
        String Cidade;
        String Uf;
        String Ibge;
        String Email;
        String ddd;
        String telefone;


        DataTable dtClienteEndereco;
        
        clsClienteEnderecoBLL clsClienteEnderecoBLL;
        clsClienteEnderecoInfo clsClienteEnderecoInfo;
        clsClienteEnderecoInfo clsClienteEnderecoInfoOld;

        public frmClienteEndereco()
        {
            InitializeComponent();
        }

        public void Init(Int32 _posicao,
                         Int32 _idcliente,
                         DataTable _dtClienteEndereco,
                         String _Cognome,
                         String _Nome,
                         String _Cgc,
                         String _Cep,
                         String _Endereco,
                         String _Numeroend,
                         String _Andar,
                         String _Tipocomple,
                         String _Bairro,
                         String _Cidade,
                         String _Uf,
                         String _Ibge,
                         String _Email,
                         String _ddd,
                         String _telefone)
        {
            posicao = _posicao;
            idcliente = _idcliente;
            dtClienteEndereco = _dtClienteEndereco;
            //clsVisual.FillComboBox(cbxCidade, "select nome from cidades order by nome", clsInfo.conexaosqldados);
            //clsVisual.FillComboBox(cbxUf, "select estado from estados order by estado", clsInfo.conexaosqldados);
            Cognome = _Cognome;
            Nome = _Nome;
            Cgc = _Cgc;
            Cep = _Cep;
            Endereco = _Endereco;
            Numeroend = _Numeroend;
            Andar = _Andar;
            Tipocomple = _Tipocomple;
            Bairro = _Bairro;
            Cidade = _Cidade;
            Uf = _Uf;
            Ibge = _Ibge;
            Email = _Email;
            ddd = _ddd;
            telefone = _telefone;
        


                clsClienteEnderecoBLL = new clsClienteEnderecoBLL();
        }

        private void frmClienteEndereco_Load(object sender, EventArgs e)
        {
            ClienteEnderecoRegistro();
        }


        private void ClienteEnderecoRegistro()
        {
            if (posicao == 0)
            {
                clsClienteEnderecoInfo = new clsClienteEnderecoInfo();

                clsClienteEnderecoInfo.bairro = Bairro;
                clsClienteEnderecoInfo.cep = Cep;
                clsClienteEnderecoInfo.cgc = Cgc;
                clsClienteEnderecoInfo.cidade = Cidade;
                clsClienteEnderecoInfo.cobnome = Cognome;
                clsClienteEnderecoInfo.contato = "";
                clsClienteEnderecoInfo.ddd = ddd;
                clsClienteEnderecoInfo.dddfax = "";
                clsClienteEnderecoInfo.dddi = "";
                clsClienteEnderecoInfo.email = Email;
                clsClienteEnderecoInfo.endereco = Endereco;
                clsClienteEnderecoInfo.ibge = Ibge;
                clsClienteEnderecoInfo.idcliente = idcliente;
                clsClienteEnderecoInfo.idcidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idcidade from cliente where id=" + idcliente));
                clsClienteEnderecoInfo.idestado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idestado from cliente where id=" + idcliente));
                clsClienteEnderecoInfo.idzona = clsInfo.zzona;
                clsClienteEnderecoInfo.ie = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ie from cliente where id=" + idcliente);
                clsClienteEnderecoInfo.observa = "";
                clsClienteEnderecoInfo.postal = "";
                clsClienteEnderecoInfo.setor = "";
                clsClienteEnderecoInfo.telefax = "";
                clsClienteEnderecoInfo.telefone = telefone;
                clsClienteEnderecoInfo.tipoendnome = "";
                clsClienteEnderecoInfo.uf = Uf; //Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + clsClienteEnderecoInfo.idestado);
            }
            else
            {
                clsClienteEnderecoInfo = ClienteEnderecoCarregar();
            }

            clsClienteEnderecoInfoOld = new clsClienteEnderecoInfo();
            ClienteEnderecoCampos(clsClienteEnderecoInfo);
            ClienteEnderecoInfo(clsClienteEnderecoInfoOld);

            //cbxTipoendnome.Select();
        }

        private clsClienteEnderecoInfo ClienteEnderecoCarregar()
        {
            clsClienteEnderecoInfo = null;
            foreach (DataRow row in dtClienteEndereco.Select("posicao=" + posicao.ToString()))
            {
                clsClienteEnderecoInfo = new clsClienteEnderecoInfo();

                clsClienteEnderecoInfo.bairro = row["bairro"].ToString();
                clsClienteEnderecoInfo.cep = row["cep"].ToString();
                clsClienteEnderecoInfo.cgc = row["cgc"].ToString();
                clsClienteEnderecoInfo.cidade = row["cidade"].ToString();
                clsClienteEnderecoInfo.cobnome = row["cobnome"].ToString();
                clsClienteEnderecoInfo.contato = row["contato"].ToString();
                clsClienteEnderecoInfo.ddd = row["ddd"].ToString();
                clsClienteEnderecoInfo.dddfax = row["dddfax"].ToString();
                clsClienteEnderecoInfo.dddi = row["dddi"].ToString();
                clsClienteEnderecoInfo.email = row["email"].ToString();
                clsClienteEnderecoInfo.endereco = row["endereco"].ToString();
                clsClienteEnderecoInfo.ibge = row["ibge"].ToString();
                clsClienteEnderecoInfo.id = clsParser.Int32Parse(row["id"].ToString());
                clsClienteEnderecoInfo.idcidade = clsParser.Int32Parse(row["idcidade"].ToString());
                clsClienteEnderecoInfo.idcliente = clsParser.Int32Parse(row["idcliente"].ToString());
                clsClienteEnderecoInfo.idestado = clsParser.Int32Parse(row["idestado"].ToString());
                clsClienteEnderecoInfo.idzona = clsParser.Int32Parse(row["idzona"].ToString());
                clsClienteEnderecoInfo.ie = row["ie"].ToString();
                clsClienteEnderecoInfo.observa = row["observa"].ToString();
                clsClienteEnderecoInfo.postal = row["postal"].ToString();
                clsClienteEnderecoInfo.setor = row["setor"].ToString();
                clsClienteEnderecoInfo.telefax = row["telefax"].ToString();
                clsClienteEnderecoInfo.telefone = row["telefone"].ToString();
                clsClienteEnderecoInfo.tipoendnome = row["tipoendnome"].ToString();
                clsClienteEnderecoInfo.uf = row["uf"].ToString();
            }

            return clsClienteEnderecoInfo;
        }

        private void ClienteEnderecoCampos(clsClienteEnderecoInfo Info)
        {
            id = Info.id;
            idcliente = Info.idcliente;
            idestado = Info.idestado;
            idzona = Info.idzona;

            tbxCliente_cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from cliente where id=" + idcliente.ToString());
            tbxCliente_numero.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from cliente where id=" + idcliente.ToString());

            cbxTipoendnome.SelectedIndex = cbxTipoendnome.FindString(Info.tipoendnome);
            tbxCobnome.Text = Info.cobnome;
            tbxCgc.Text = Info.cgc;
            tbxIe.Text = Info.ie;
            tbxDdd.Text = Info.ddd;
            tbxTelefone.Text = Info.telefone;
            tbxRamal.Text = Info.ramal.ToString();
            tbxDddfax.Text = Info.dddfax;
            tbxTelefax.Text = Info.telefax;
            tbxCep.Text = Info.cep;
            tbxEndereco.Text = Info.endereco;
            tbxBairro.Text = Info.bairro;
            tbxCidade.Text = Info.cidade;
            String cidade = "";
            if (Info.idcidade == 0)
            {
                Info.idcidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cidades where nome='" + clsVisual.RemoveAcentos(Info.cidade).ToUpper() + "' ", "0"));

            }
            tbxCidade.Text = Info.cidade;
            cidade = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select nome from cidades where id=" + Info.idcidade, "");
            //cbxCidade.Text = clsVisual.SelecionarIndex(cidade, int(cidade.Length), cbxCidade);
            cbxCidade.SelectedIndex = cbxCidade.FindString(cidade);
            if (cbxCidade.SelectedIndex == -1)
            {
                cbxCidade.SelectedIndex = 0;
            }

            tbxCep.Text = clsVisual.CamposVisual("CEP", tbxCep.Text);
            cbxUf.SelectedIndex = cbxUf.FindString(Info.uf);
            tbxIbge.Text = Info.ibge;
            tbxObserva.Text = Info.observa;
            tbxEmail.Text = Info.email;

            if (idzona == 0) idzona = clsInfo.zzona;

            tbxZona.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select codigo from zonas where id=" + idzona.ToString());
        }

        private void ClienteEnderecoInfo(clsClienteEnderecoInfo Info)
        {
            Info.id = id;
            Info.idcidade = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from cidades where nome='" + cbxCidade.Text.Trim() + "' ", ""));
            Info.idcliente = idcliente;
            Info.idestado = idestado;
            Info.idzona = idzona;

            Info.tipoendnome = cbxTipoendnome.Text;
            Info.cobnome = tbxCobnome.Text;
            Info.cgc = tbxCgc.Text;
            Info.ie = tbxIe.Text;
            Info.ddd = tbxDdd.Text;
            Info.telefone = tbxTelefone.Text;
            Info.ramal = clsParser.Int32Parse(tbxRamal.Text);
            Info.dddfax = tbxDddfax.Text;
            Info.telefax = tbxTelefax.Text;
            Info.cep = tbxCep.Text;
            Info.endereco = tbxEndereco.Text;
            Info.bairro = tbxBairro.Text;
            Info.cidade = tbxCidade.Text;
            Info.uf = cbxUf.Text;
            Info.ibge = tbxIbge.Text;
            Info.observa = tbxObserva.Text;
            Info.email = tbxEmail.Text;
        }

        private void ClienteEnderecoSalvar()
        {
            clsClienteEnderecoInfo = new clsClienteEnderecoInfo();
            ClienteEnderecoInfo(clsClienteEnderecoInfo);
            clsClienteEnderecoBLL.VerificaInfo(clsClienteEnderecoInfo);

            if (posicao == 0)
            {
                DataRow row = dtClienteEndereco.NewRow();

                row["id"] = clsClienteEnderecoInfo.id;
                row["idcliente"] = clsClienteEnderecoInfo.idcliente;
                row["idcidade"] = clsClienteEnderecoInfo.idcidade;
                row["idestado"] = clsClienteEnderecoInfo.idestado;
                row["idzona"] = clsClienteEnderecoInfo.idzona;

                row["bairro"] = clsClienteEnderecoInfo.bairro;
                row["cep"] = clsClienteEnderecoInfo.cep;
                row["cgc"] = clsClienteEnderecoInfo.cgc;
                row["cidade"] = clsClienteEnderecoInfo.cidade;
                row["cobnome"] = clsClienteEnderecoInfo.cobnome;
                row["contato"] = clsClienteEnderecoInfo.contato;
                row["ddd"] = clsClienteEnderecoInfo.ddd;
                row["dddfax"] = clsClienteEnderecoInfo.dddfax;
                row["dddi"] = clsClienteEnderecoInfo.dddi;
                row["email"] = clsClienteEnderecoInfo.email;
                row["endereco"] = clsClienteEnderecoInfo.endereco;
                row["ibge"] = clsClienteEnderecoInfo.ibge;
                row["ie"] = clsClienteEnderecoInfo.ie;
                row["observa"] = clsClienteEnderecoInfo.observa;
                row["postal"] = clsClienteEnderecoInfo.postal;
                row["setor"] = clsClienteEnderecoInfo.setor;
                row["telefax"] = clsClienteEnderecoInfo.telefax;
                row["telefone"] = clsClienteEnderecoInfo.telefone;
                row["tipoendnome"] = clsClienteEnderecoInfo.tipoendnome;
                row["uf"] = clsClienteEnderecoInfo.uf;
                row["posicao"] = dtClienteEndereco.Rows.Count + 1;

                dtClienteEndereco.Rows.Add(row);
            }
            else
            {
                DataRow row = dtClienteEndereco.Select("posicao=" + posicao.ToString())[0];

                row["id"] = clsClienteEnderecoInfo.id;
                row["idcliente"] = clsClienteEnderecoInfo.idcliente;
                row["idcidade"] = clsClienteEnderecoInfo.idcidade;
                row["idestado"] = clsClienteEnderecoInfo.idestado;
                row["idzona"] = clsClienteEnderecoInfo.idzona;
                row["bairro"] = clsClienteEnderecoInfo.bairro;
                row["cep"] = clsClienteEnderecoInfo.cep;
                row["cgc"] = clsClienteEnderecoInfo.cgc;
                row["cidade"] = clsClienteEnderecoInfo.cidade;
                row["cobnome"] = clsClienteEnderecoInfo.cobnome;
                row["contato"] = clsClienteEnderecoInfo.contato;
                row["ddd"] = clsClienteEnderecoInfo.ddd;
                row["dddfax"] = clsClienteEnderecoInfo.dddfax;
                row["dddi"] = clsClienteEnderecoInfo.dddi;
                row["email"] = clsClienteEnderecoInfo.email;
                row["endereco"] = clsClienteEnderecoInfo.endereco;
                row["ibge"] = clsClienteEnderecoInfo.ibge;
                row["ie"] = clsClienteEnderecoInfo.ie;
                row["observa"] = clsClienteEnderecoInfo.observa;
                row["postal"] = clsClienteEnderecoInfo.postal;
                row["setor"] = clsClienteEnderecoInfo.setor;
                row["telefax"] = clsClienteEnderecoInfo.telefax;
                row["telefone"] = clsClienteEnderecoInfo.telefone;
                row["tipoendnome"] = clsClienteEnderecoInfo.tipoendnome;
                row["uf"] = clsClienteEnderecoInfo.uf;
            }
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            TrataCampos((Control)sender);
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownCep(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownCep((TextBox)sender, e);
        }
        
        private void ControlKeyDownTelefone(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownTelefone((TextBox)sender, e);
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado = DialogResult.Cancel;
                resultado = MessageBox.Show("Deseja Salvar e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (resultado == DialogResult.Yes)
                {
                    ClienteEnderecoSalvar();
                }
                else if (resultado == DialogResult.Cancel)
                {
                    cbxTipoendnome.Select();
                    return;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbxTipoendnome.Select();
            }
        }

        private void tbxTelefone_KeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownTelefone((TextBox)sender, e);
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void tbxCobnome_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCep_Click(object sender, EventArgs e)
        {

            BuscaCEP Cep = new BuscaCEP();
            Cep.Buscar(tbxCep.Text);

            tbxEndereco.Text = Cep.Logradouro;
            tbxBairro.Text = Cep.Bairro;
            String Cidade = Cep.Localidade;
            Cidade = clsVisual.RemoveAcentos(Cidade);
            Cidade = Cidade.Replace("-", " ");
            //cbxCidade.SelectedIndex = SelecionarIndex(Cidade, 0, Cidade);
            cbxCidade.SelectedIndex = cbxCidade.FindString(Cidade + "   -   " + Cep.UF);

            cbxUf.Text = Cep.UF;
            tbxIbge.Text = Cep.IBGE;

            tbxEndereco.Select();
            tbxEndereco.SelectAll();
        }      

        void TrataCampos(Control ctl)
        {

            if (!String.IsNullOrEmpty(cbxTipoendnome.Text))
            {
                if (cbxTipoendnome.Text.Substring(0, 1) == "1")
                {
                    label20.Text = "Logradouro: capacidade para 37 caracteres)";
                    tbxEndereco.MaxLength = 37;
                    label3.Text = "Bairro: capacidade para 12 caracteres)";
                    tbxBairro.MaxLength = 12;
                    label2.Text = "Cidade: capacidade para 15 caracteres)";
                    tbxCidade.MaxLength = 15;
                }
                else
                {
                    label20.Text = "Logradouro:";
                    tbxEndereco.MaxLength = 125;
                    label3.Text = "Bairro:";
                    tbxBairro.MaxLength = 72;
                    label2.Text = "Cidade:";
                    tbxCidade.MaxLength = 40;
                }
            }
            if (!String.IsNullOrEmpty(cbxTipoendnome.Text))
            {
                if (cbxTipoendnome.Text.Substring(0, 1) == "2")
                {
                    gbxZona.Visible = true;
                }
            }

            else
            {
                gbxZona.Visible = false;
            }

            if (ctl is ComboBox)
            {
                if (((ComboBox)ctl).Name == cbxUf.Name)
                {
                    //idestado = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "ESTADOS", "ID", "ESTADO", cbxUf.Text));
                    idestado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select ID from ESTADOS where ESTADO='" + cbxUf.Text + "' ", "0"));

                    if (idestado == 0 && ((TextBox)ctl).Text.Trim().Length > 0)
                    {
                        ((ComboBox)ctl).SelectedIndex = 0;
                    }
                }
                if (((ComboBox)ctl).Name == cbxCidade.Name)
                {
                    //idestado = clsParser.Int32Parse(Procedure.PesquisaValor(clsInfo.conexaosqldados, "ESTADOS", "ID", "ESTADO", cbxUf.Text));
                    idestado = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select idestado from cidades where nome='" + cbxCidade.Text + "' ", "0"));
                    cbxUf.SelectedIndex = cbxUf.FindString(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select estado from estados where id=" + idestado.ToString(), ""));
                    if (cbxUf.SelectedIndex == -1)
                    {
                        cbxUf.SelectedIndex = 0;
                    }

                }

            }
            if (ctl.Name == tbxZona.Name)
            {
                idzona = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from zonas where codigo = '" + tbxZona.Text + "'", "0"));
                if (idzona == 0 && tbxZona.Text.Length > 0)
                {
                    tbxZona.Text = "";
                    tbxZona.Select();
                }
            }
            if (clsInfo.zrow != null && clsInfo.znomegrid != "")
            {
                if (clsInfo.znomegrid == btnIdZona.Name)
                {
                    idzona = clsParser.Int32Parse(clsInfo.zrow.Cells["ID"].Value.ToString());
                    tbxZona.Text = clsInfo.zrow.Cells["CODIGO"].Value.ToString();
                    tbxZona.Select();
                    tbxZona.SelectAll();
                }
            }
            else if (ctl.Name == tbxCep.Name)
            {
                if (tbxEndereco.Text == "")
                {
                    BuscaCEP Cep = new BuscaCEP();
                    Cep.Buscar(tbxCep.Text);

                    tbxEndereco.Text = Cep.Logradouro;
                    tbxBairro.Text = Cep.Bairro;
                    String Cidade = Cep.Localidade;
                    Cidade = clsVisual.RemoveAcentos(Cidade);
                    Cidade = Cidade.Replace("-", " ");
                    //cbxCidade.SelectedIndex = SelecionarIndex(Cidade, 0, Cidade);
                    cbxCidade.SelectedIndex = cbxCidade.FindString(Cidade + "   -   " + Cep.UF);

                    cbxUf.Text = Cep.UF;
                    tbxIbge.Text = Cep.IBGE;


                }
            }
                clsInfo.znomegrid = "";
            clsInfo.zrow = null;
        }
        String TipoRua(String _EndTipo)
        {
            switch (_EndTipo.PadRight(2).Substring(0, 2).ToUpper())
            {
                case "RU":
                    _EndTipo = "R";
                    break;
                case "AV":
                    _EndTipo = "AV";
                    break;
                case "AL":
                    _EndTipo = "R";
                    break;
                case "ES":
                    _EndTipo = "EST";
                    break;
                case "PR":
                    _EndTipo = "PÇ";
                    break;
                case "TR":
                    _EndTipo = "TV";
                    break;
                default:
                    break;
            }
            return _EndTipo;
        }

        private void btnIdZona_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnIdZona.Name;
            frmEscFormVisPes frmEscFormVisPes = new frmEscFormVisPes();
            frmEscFormVisPes.Init(clsInfo.conexaosqldados, "ZONAS", idzona, "Regiões e Zonas");
            clsFormHelper.AbrirForm(this.MdiParent, frmEscFormVisPes, clsInfo.conexaosqldados);
        }

        private void frmClienteEndereco_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void cbxCidade_KeyUp(object sender, KeyEventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void cbxCidade_Click(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void cbxCidade_MouseClick(object sender, MouseEventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void cbxCidade_MouseDown(object sender, MouseEventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void cbxCidade_MouseMove(object sender, MouseEventArgs e)
        {
            TrataCampos((Control)sender);
        }

        private void cbxCidade_MouseUp(object sender, MouseEventArgs e)
        {
            TrataCampos((Control)sender);
        }

    }
}
