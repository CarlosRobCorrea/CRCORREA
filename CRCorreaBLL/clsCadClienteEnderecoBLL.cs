using CRCorreaFuncoes;
using CRCorreaInfo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsCadClienteEnderecoBLL : SQLFactory<clsCadClienteEnderecoInfo>
    {
        public override int Incluir(clsCadClienteEnderecoInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsCadClienteEnderecoInfo info, string cnn)
        {
            VerificaInfo(info);
            
            return base.Alterar(info, cnn);
        }

        public void ExcluirCliente(Int32 idcliente, String cnn)
        {
            DataTable dt;
            SqlDataAdapter sda;

            dt = new DataTable();

            sda = new SqlDataAdapter("select id from Clienteendereco where idcliente=@idcliente", cnn);

            sda.SelectCommand.Parameters.Add("@idcliente", SqlDbType.Int).Value = idcliente;

            sda.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
            }
        }

        public void VerificaInfo(clsCadClienteEnderecoInfo info)
        {
            if (info.cobnome == "")
            {
                throw new Exception("Cadastro de Cliente >>> Falta NOME");
            }
            if (info.cidade == "")
            {
                throw new Exception("Cadastro de Cliente >>> Falta Cidade");
            }
            if (info.bairro == "")
            {
                throw new Exception("Cadastro de Cliente >>> Falta Bairro");
            }
            if (info.uf == "")
            {
                throw new Exception("Cadastro de Cliente >>> Falta UF");
            }
            if (info.cep == "")
            {
                throw new Exception("Cadastro de Cliente >>> Falta CEP");
            }
            if (info.tipoendnome.Substring(0,1)=="1")
            {
                if (info.cobnome.Trim().Length > 39)
                {
                    throw new Exception("Cadastro de Cliente >>> Nome do Cliente excedeu o numero de caracteres Permitido (39) para Cobrança");
                }
                if (info.endereco.Trim().Length > 37)
                {
                    throw new Exception("Cadastro de Cliente >>> Endereço do Cliente excedeu o numero de caracteres Permitido (37) para Cobrança");
                }
                if (info.bairro.Trim().Length > 12)
                {
                    throw new Exception("Cadastro de Cliente >>> Bairro excedeu o numero de caracteres Permitido (12) para Cobrança");
                }
                if (info.cidade.Trim().Length > 15)
                {
                    throw new Exception("Cadastro de Cliente >>> Cidade excedeu o numero de caracteres Permitido (15) para Cobrança");
                }
            }
            if (info.ibge == null || info.ibge.Length != 7)
            {
                throw new Exception("Cód. do IBGE do município deve ser preenchido.");
            }

            if (info.tipoendnome.Length > 15)
            {
                throw new Exception("Clienteendereco - Tipoendnome ultrapassou o limite permitido (15 caracteres).");
            }

            if (info.cobnome.Length > 40)
            {
                throw new Exception("Clienteendereco - Cobnome ultrapassou o limite permitido (40 caracteres).");
            }

            if (info.endereco.Length > 125)
            {
                throw new Exception("Clienteendereco - Endereco ultrapassou o limite permitido (125 caracteres).");
            }

            if (info.bairro.Length > 72)
            {
                throw new Exception("Clienteendereco - Bairro ultrapassou o limite permitido (72 caracteres).");
            }

            if (info.cidade.Length > 50)
            {
                throw new Exception("Clienteendereco - Cidade ultrapassou o limite permitido (50 caracteres).");
            }

            if (info.uf.Length > 2)
            {
                throw new Exception("Clienteendereco - UF ultrapassou o limite permitido (2 caracteres).");
            }

            if (info.cep.Length > 9)
            {
                throw new Exception("Clienteendereco - CEP ultrapassou o limite permitido (9 caracteres).");
            }

            if (info.email.Length > 50)
            {
                throw new Exception("Clienteendereco - Email ultrapassou o limite permitido (50 caracteres).");
            }

            if (info.ddd.Length > 4)
            {
                throw new Exception("Clienteendereco - DDD ultrapassou o limite permitido (4 caracteres).");
            }

            if (info.telefone.Length > 10)
            {
                throw new Exception("Clienteendereco - Telefone ultrapassou o limite permitido (10 caracteres).");
            }

            if (info.dddfax.Length > 4)
            {
                throw new Exception("Clienteendereco - DDDFAX ultrapassou o limite permitido (4 caracteres).");
            }

            if (info.telefax.Length > 10)
            {
                throw new Exception("Clienteendereco - Telefax ultrapassou o limite permitido (10 caracteres).");
            }

            if (info.observa.Length > 50)
            {
                throw new Exception("Clienteendereco - Observa ultrapassou o limite permitido (50 caracteres).");
            }

            if (info.ie.Length > 16)
            {
                throw new Exception("Clienteendereco - IE ultrapassou o limite permitido (16 caracteres).");
            }

            if (info.cgc.Length > 18)
            {
                throw new Exception("Clienteendereco - CGC ultrapassou o limite permitido (18 caracteres).");
            }

            if (info.ibge.Length > 7)
            {
                throw new Exception("Clienteendereco - IBGE ultrapassou o limite permitido (7 caracteres).");
            }

            if (info.tipoendnome == "2 - Entrega")
            {
                if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from clienteendereco where " +
                "TIPOENDNOME = '2 - Entrega' and idcliente = " + info.idcliente + " and id <> " + info.id, "0")) != 0)
                {
                    throw new Exception("Já existe um endereço de Entrega cadastrado.");
                }
            }

            if (info.tipoendnome == "1 - Cobrança")
            {
                if (clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select id from clienteendereco where " +
                "TIPOENDNOME = '1 - Cobrança' and idcliente = " + info.idcliente + " and id <> " + info.id, "0")) != 0)
                {
                    throw new Exception("Já existe um endereço de Cobrança cadastrado.");
                }
            }
        }

        public DataTable GridDados(Int32 idcliente, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "CLIENTEENDERECO.ID, " +
                        "IDCLIENTE, " +
                        "TIPOENDNOME, " +
                        "COBNOME, " +
                        "ENDERECO, " +
                        "BAIRRO, " +
                        "CIDADE, " +
                        "ESTADOS.ESTADO AS [UF], " +
                        "CEP, " +
                        "POSTAL, " +
                        "EMAIL, " +
                        "DDDI, " +
                        "DDD, " +
                        "TELEFONE, " +
                        "CONTATO, " +
                        "SETOR, " +
                        "RAMAL, " +
                        "DDDFAX, " +
                        "TELEFAX, " +
                        "OBSERVA, " +
                        "IE, " +
                        "CGC, " +
                        "IDESTADO, " +
                        "IDCIDADE, " +
                        "IBGE " +
                    "FROM " +
                        "CLIENTEENDERECO " +
                            "INNER JOIN ESTADOS ON ESTADOS.ID=CLIENTEENDERECO.IDESTADO " +
                    "WHERE " +
                        "idcliente = @idcliente";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@idcliente", SqlDbType.Int).Value = idcliente;

            sda.Fill(dt);

            return dt;
        }

        ////////////////////////////////////////////////////////////////////////
        public static DataTable GridDadosClienteEnderecoPes(Int32 idcliente)
        {
            DataTable dt;
            String query = "";
            //String filtro = "";
            SqlDataAdapter sda;
            query = "select clienteendereco.id, cliente.cognome, zonas.codigo as [Zona], clienteendereco.tipoendnome, " +
                    "clienteendereco.endereco, clienteendereco.bairro, clienteendereco.cidade, clienteendereco.UF, clienteendereco.cep " +
                    "from cliente  " +
                    "left join clienteendereco on clienteendereco.idcliente=cliente.id " +
                    "left join zonas on zonas.id=clienteendereco.idzona " +
                    "where left(clienteendereco.tipoendnome,1) = '2' and clienteendereco.id=@idcliente ";
            sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
            sda.SelectCommand.Parameters.Add("@idcliente", SqlDbType.Int).Value = idcliente;
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunasClienteEnderecoPes = new GridColuna[]
        {
                new GridColuna("Id.", "id", 10, false, DataGridViewContentAlignment.MiddleRight),
                new GridColuna("Cognome", "cognome", 90, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Zona", "Zona", 200, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Tipo End", "tipoendnome", 25, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Endereco", "endereco", 30, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Bairro", "bairro", 70, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Cidade", "cidade", 90, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("UF", "UF", 80, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Cep", "cep", 80, true, DataGridViewContentAlignment.MiddleLeft)
         };

        public static void GridMontaClienteEnderecoPes(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasClienteEnderecoPes, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "COGNOME");
            dgv.AllowUserToResizeColumns = true;
//            dgv.Columns["ultdatnf"].DefaultCellStyle.Format = "dd/MM/yyyy";
//            dgv.Columns["ultvalnf"].DefaultCellStyle.Format = "N2";

        }


    }
}
