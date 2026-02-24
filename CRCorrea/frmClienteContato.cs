using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRCorreaBLL;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorrea
{
    public partial class frmClienteContato : Form
    {
        Int32 posicao;
        Int32 id;
        Int32 idcliente;
        String email;
        String contato;

        DataTable dtClienteprospeccaocontato;

        clsClienteContatoBLL clsClienteContatoBLL;
        clsClienteContatoInfo clsClienteContatoInfo;
        clsClienteContatoInfo clsClienteContatoInfoOld;

        
        

        public frmClienteContato()
        {
            InitializeComponent();
        }

        public void Init(Int32 _posicao,
                         Int32 _idcliente,
                         DataTable _dtClienteprospeccaocontato,
                         String _numero,
                         String _Cognome,
                         String _Email,
                         String _Contato)
        {
            posicao = _posicao;
            idcliente = _idcliente;
            tbxClienteprospeccao_numero.Text = _numero;
            tbxClienteprospeccao_cognome.Text = _Cognome;
            if (Application.ProductName == "ApliEsquadria")
            {
                _Contato =_Cognome;
            }
            email = _Email;
            contato = _Contato;
            dtClienteprospeccaocontato = _dtClienteprospeccaocontato;

            clsClienteContatoBLL = new clsClienteContatoBLL();
        }

        private void frmClienteProspeccaoContato_Load(object sender, EventArgs e)
        {
            ClienteprospeccaocontatoRegistro();
        }

        private void ClienteprospeccaocontatoRegistro()
        {
            if (posicao == 0)
            {
                clsClienteContatoInfo = new clsClienteContatoInfo();

                clsClienteContatoInfo.celular = "";
                clsClienteContatoInfo.contato = contato;
                clsClienteContatoInfo.ddd = "";
                clsClienteContatoInfo.dddcelular = "";
                clsClienteContatoInfo.dddfax = "";
                clsClienteContatoInfo.email = email;
                clsClienteContatoInfo.idcliente = idcliente;
                clsClienteContatoInfo.setor = "";
                clsClienteContatoInfo.telefax = "";
                clsClienteContatoInfo.telefone = "";
                clsClienteContatoInfo.tipocontato = "";
            }
            else
            {
                clsClienteContatoInfo = ClienteprospeccaocontatoCarregar();
            }

            clsClienteContatoInfoOld = new clsClienteContatoInfo();
            ClienteprospeccaocontatoCampos(clsClienteContatoInfo);
            ClienteprospeccaocontatoInfo(clsClienteContatoInfoOld);

            lbxTipocontato.Select();
        }

        private clsClienteContatoInfo ClienteprospeccaocontatoCarregar()
        {
            clsClienteContatoInfo = null;
            foreach (DataRow row in dtClienteprospeccaocontato.Select("posicao=" + posicao.ToString()))
            {
                clsClienteContatoInfo = new clsClienteContatoInfo();
                clsClienteContatoInfo.celular = row["celular"].ToString();
                clsClienteContatoInfo.contato = row["contato"].ToString();
                clsClienteContatoInfo.ddd = row["ddd"].ToString();
                clsClienteContatoInfo.dddcelular = row["dddcelular"].ToString();
                clsClienteContatoInfo.dddfax = row["dddfax"].ToString();
                clsClienteContatoInfo.email = row["email"].ToString();
                clsClienteContatoInfo.id = clsParser.Int32Parse(row["id"].ToString());
                clsClienteContatoInfo.idcliente = clsParser.Int32Parse(row["idcliente"].ToString());
                clsClienteContatoInfo.observa = row["observa"].ToString();
                clsClienteContatoInfo.ramal = clsParser.Int32Parse(row["ramal"].ToString());
                clsClienteContatoInfo.ramalfax = clsParser.Int32Parse(row["ramalfax"].ToString());
                clsClienteContatoInfo.setor = row["setor"].ToString();
                clsClienteContatoInfo.telefax = row["telefax"].ToString();
                clsClienteContatoInfo.telefone = row["telefone"].ToString();
                clsClienteContatoInfo.tipocontato = row["tipocontato"].ToString();
            }

            return clsClienteContatoInfo;
        }

        private void ClienteprospeccaocontatoCampos(clsClienteContatoInfo Info)
        {
            id = Info.id;
            idcliente = Info.idcliente;
            // tirei porque qdo inclui fica nome errado (06/2011) carlos
            //tbxClienteprospeccao_cognome.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select cognome from clienteprospeccao where id=" + idcliente.ToString());
            //tbxClienteprospeccao_numero.Text = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select numero from clienteprospeccao where id=" + idcliente.ToString());

            lbxTipocontato.SelectedIndex = lbxTipocontato.FindString(Info.tipocontato);
            tbxTipoContato.Text = Info.tipocontato;
            tbxContato.Text = Info.contato; 
            tbxSetor.Text = Info.setor;
            tbxEmail.Text = Info.email;
            tbxDdd.Text = Info.ddd;
            tbxTelefone.Text = Info.telefone;
            tbxRamal.Text = Info.ramal.ToString();
            tbxDddfax.Text = Info.dddfax;
            tbxTelefax.Text = Info.telefax;
            tbxRamalfax.Text = Info.ramalfax.ToString();
            tbxDddcelular.Text = Info.dddcelular;
            tbxCelular.Text = Info.celular;
            tbxObserva.Text = Info.observa;

            if (tbxTipoContato.Text == "")
            {
                tbxTipoContato.Text = lbxTipocontato.Text;
            }
        }

        private void ClienteprospeccaocontatoInfo(clsClienteContatoInfo Info)
        {
            Info.id = id;
            Info.idcliente = idcliente;

            Info.celular = tbxCelular.Text;
            Info.contato = tbxContato.Text;
            Info.ddd = tbxDdd.Text;
            Info.dddcelular = tbxDddcelular.Text;
            Info.dddfax = tbxDddfax.Text;
            Info.email = tbxEmail.Text;
            Info.observa = tbxObserva.Text;
            Info.ramal = clsParser.Int32Parse(tbxRamal.Text);
            Info.ramalfax = clsParser.Int32Parse(tbxRamalfax.Text);
            Info.setor = tbxSetor.Text;
            Info.telefax = tbxTelefax.Text;
            Info.telefone = tbxTelefone.Text;
            Info.tipocontato = tbxTipoContato.Text;
        }

        private void ClienteprospeccaocontatoSalvar()
        {
            clsClienteContatoInfo = new clsClienteContatoInfo();
            ClienteprospeccaocontatoInfo(clsClienteContatoInfo);
            clsClienteContatoBLL.VerificaInfo(clsClienteContatoInfo);

            if (posicao == 0)
            {
                DataRow row = dtClienteprospeccaocontato.NewRow();

                row["id"] = id;
                row["idcliente"] = idcliente;
                row["celular"] = clsClienteContatoInfo.celular;
                row["contato"] = clsClienteContatoInfo.contato;
                row["ddd"] = clsClienteContatoInfo.ddd;
                row["dddcelular"] = clsClienteContatoInfo.dddcelular;
                row["dddfax"] = clsClienteContatoInfo.dddfax;
                row["email"] = clsClienteContatoInfo.email;
                row["idcliente"] = clsClienteContatoInfo.idcliente;
                row["observa"] = clsClienteContatoInfo.observa;
                row["ramal"] = clsClienteContatoInfo.ramal;
                row["ramalfax"] = clsClienteContatoInfo.ramalfax;
                row["setor"] = clsClienteContatoInfo.setor;
                row["telefax"] = clsClienteContatoInfo.telefax;
                row["telefone"] = clsClienteContatoInfo.telefone;
                row["tipocontato"] = clsClienteContatoInfo.tipocontato;
                row["posicao"] = dtClienteprospeccaocontato.Rows.Count + 1;

                dtClienteprospeccaocontato.Rows.Add(row);
            }
            else
            {
                DataRow row = dtClienteprospeccaocontato.Select("posicao=" + posicao.ToString())[0];

                row["id"] = id;
                row["idcliente"] = idcliente;
                row["celular"] = clsClienteContatoInfo.celular;
                row["contato"] = clsClienteContatoInfo.contato;
                row["ddd"] = clsClienteContatoInfo.ddd;
                row["dddcelular"] = clsClienteContatoInfo.dddcelular;
                row["dddfax"] = clsClienteContatoInfo.dddfax;
                row["email"] = clsClienteContatoInfo.email;
                row["idcliente"] = clsClienteContatoInfo.idcliente;
                row["observa"] = clsClienteContatoInfo.observa;
                row["ramal"] = clsClienteContatoInfo.ramal;
                row["ramalfax"] = clsClienteContatoInfo.ramalfax;
                row["setor"] = clsClienteContatoInfo.setor;
                row["telefax"] = clsClienteContatoInfo.telefax;
                row["telefone"] = clsClienteContatoInfo.telefone;
                row["tipocontato"] = clsClienteContatoInfo.tipocontato;
            }
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void tspSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado = DialogResult.Cancel;
                resultado = MessageBox.Show("Deseja Salvar e Retornar?", Application.CompanyName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (resultado == DialogResult.Yes)
                {
                    ClienteprospeccaocontatoSalvar();
                }
                else if (resultado == DialogResult.Cancel)
                {
                    tbxContato.Select();
                    tbxContato.SelectAll();
                    return;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tbxContato.Select();
                tbxContato.SelectAll();
            }
        }

        private void lbxTipocontato_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbxTipoContato.Text = lbxTipocontato.Text;
        }

        private void ControlKeyDownTelefone(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownTelefone((TextBox)sender, e);
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void tbxDddfax_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxTelefone_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
