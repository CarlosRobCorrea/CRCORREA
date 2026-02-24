using CRCorreaFuncoes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace CRCorrea
{
    public partial class frmImportarFebraban : Form
    {
        private Int32 qtcaracter;
        private Int32 posicaoatual;
        private Int32 posicaoinicial;
        private Int32 posicaofinal;
        private Boolean ok;
        private String conexao;

        SqlConnection scn;
        SqlCommand scd;

        Int32 nRegistros = 0;
        Int32 x = 0;
        Int32 y = 0;
        String campo;

        String codigo = "";
        String nome = "";
        Int32 nrobanco = 0;
        Int32 idbanco = 0;
        Int32 id = 0;

        public frmImportarFebraban()
        {
            InitializeComponent();
        }

        public void Init(String _conexao)
        {
            conexao = _conexao;

        }

        private void frmImportarFebraban_Load(object sender, EventArgs e)
        {
            cbxCadastro.SelectedIndex = 0;
            tbxArquivoOrigem.Text = "\\" + clsInfo.arquivos; 
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dadosOrigemProcurar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Procurar...";
            ofd.Filter = "Arquivos Textos |*.txt";

            ofd.ShowDialog();

            tbxArquivoOrigem.Text = ofd.FileName;
        }

        private void tspOrdem_Click(object sender, EventArgs e)
        {
            lbxOrigem.Sorted = true;
            lbxDestino.Sorted = true;
            lbxDefeitos.Sorted = true;
        }

        private void btnCaptura_Click(object sender, EventArgs e)
        {
            if (tbxArquivoOrigem.Text.ToString().Length > 5)
            {
                String[] str;
                str = File.ReadAllLines(tbxArquivoOrigem.Text);
                tbxRegistros.Text = "0";
                tbxTransferiu.Text = "0";
                tbxFalta.Text = "0";
                if (str.LongCount() > 0)
                {
                    nRegistros = (Int32)str.LongCount();
                    tbxRegistros.Text = nRegistros.ToString();
                    pbr.Visible = true;
                    this.Refresh();
                }
                foreach (String linha in str)
                {
                    lbxOrigem.Items.Add(linha);
                    x += 1;
                    y += 1;
                    if ((100 / nRegistros) * x <= 100)
                        pbr.Value = Decimal.ToInt32(Decimal.Divide(100, Decimal.Parse(nRegistros.ToString())) * x);

                    pbr.PerformStep();
                    tbxTransferiu.Text = x.ToString();
                    tbxFalta.Text = (clsParser.Int32Parse(tbxRegistros.Text) - clsParser.Int32Parse(tbxTransferiu.Text)).ToString();
                    if (y == 50)
                    {
                        this.Refresh();
                        y = 0;
                    }
                }

                pbr.Visible = false;
                btnTransfere.Visible = true;
                btnCaptura.Enabled = false;
                tbxTransferiu.Text = "0";
                tbxFalta.Text = "0";
            }
            else
            {
                MessageBox.Show("indique o arquivo ?");
            }
        }

        private void btnTransfere_Click(object sender, EventArgs e)
        {
            String cadastro = "";
            cadastro = cbxCadastro.Text.Substring(0, 2);
            switch (cadastro)
            {
                case "00":
                    MessageBox.Show("Indique a tabela que deseja transferir !!");
                    break;
                case "01":  // Aceite
                    transfAceite();
                    break;
                case "02":  // Especie
                    //transfCliente();
                    break;
                case "03":  // Estado
                    //transfCliente();
                    break;
                case "04":  // Impressao
                    //transfCliente();
                    break;
                case "05":  // Modalidade
                    //transfCliente();
                    break;
                default:
                    break;
            }
        }

        private void transfAceite()
        {
            tbxTransferiu.Text = "0";
            tbxFalta.Text = "0";
            x = 0;

            foreach (String linha in lbxOrigem.Items)
            {
                x += 1;
                tbxTransferiu.Text = x.ToString();
                tbxFalta.Text = (clsParser.Int32Parse(tbxRegistros.Text) - clsParser.Int32Parse(tbxTransferiu.Text)).ToString();
                this.Refresh();
                // transferir para cliente
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
                        if (posicaoinicial == 0)
                        {
                            //clsClienteItaInfo = new clsClienteItaInfo();
                        }
                        switch (posicaoatual)
                        {
                            case 1:   // NRO DO BANCO (fEBRABAN)
                                nrobanco = clsParser.Int32Parse(campo);
                                posicaoinicial = (posicaofinal + 1);
                                break;
                            case 2:   // codigo
                                codigo = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                                posicaoinicial = (posicaofinal + 1);
                                break;
                            case 3:  // nome
                                nome = linha.Substring(posicaoinicial, (posicaofinal - posicaoinicial));
                                nome = nome.Replace("|", "");
                                posicaoinicial = (posicaofinal + 1);
                                break;
                        }
                    }
                }
                lbxDestino.Items.Add(linha);
                // gravar aceite
                // Localizar o Banco
                idbanco = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from TAB_BANCOS where CODIGO= " + nrobanco + " ").ToString());
                if (idbanco > 0)
                {
                    id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from TAB_BANCOSACEITE where IDCODIGO= " + idbanco + " and codigo='" + codigo + "' ").ToString());
                    scn = new SqlConnection(conexao);

                    if (id == 0)
                    {   // Incluir
                        scd = new SqlCommand("INSERT INTO TAB_BANCOSACEITE (" +
                                 "IDCODIGO, CODIGO, NOME " +
                                 ") VALUES ( " +
                                 "@IDCODIGO, @CODIGO, @NOME );" +
                                 "SELECT SCOPE_IDENTITY()", scn);
                    }
                    else
                    {
                        // alterar
                        scd = new SqlCommand("UPDATE TAB_BANCOSACEITE SET " +
                                    "CODIGO=@CODIGO, NOME=@NOME " +
                                    "WHERE ID = @ID", scn);
                        scd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = id;
                    }
                    scd.Parameters.AddWithValue("@IDCODIGO", SqlDbType.Int).Value = idbanco;
                    scd.Parameters.AddWithValue("@CODIGO", SqlDbType.NVarChar).Value = codigo;
                    scd.Parameters.AddWithValue("@NOME", SqlDbType.NVarChar).Value = nome;

                    scn.Open();
                    scd.ExecuteNonQuery();
                    scn.Close();
                }

                else
                {
                    MessageBox.Show("Banco não cadastrado nro +" + nrobanco + "");
                }
            }
        }
    }
}

