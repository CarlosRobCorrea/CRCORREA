using CRCorreaFuncoes;
using CRCorreaInfo;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;


namespace CRCorreaBLL
{
    public class clsCadClienteContatoBLL : SQLFactory<clsCadClienteContatoInfo>
    {
        public override int Incluir(clsCadClienteContatoInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsCadClienteContatoInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Alterar(info, cnn);
        }

        public void VerificaInfo(clsCadClienteContatoInfo info)
        {
            if (info.contato.Length < 3)
            {
                throw new Exception("Coloque o nome do contato");
            }
            if (info.ddd.Length < 2)
            {
                throw new Exception("Coloque o ddd do Telefone");
            }
            if (info.telefone.Length < 8)
            {
                throw new Exception("Coloque o Numero do Telefone");
            }
            if (info.observa .Length > 50)
            {
                throw new Exception("Campo Observação de um contato tem mais de 50 caracteres");
            }
        }

        public void ExcluirCliente(Int32 idcliente, String cnn)
        {
            DataTable dt;
            SqlDataAdapter sda;

            dt = new DataTable();

            sda = new SqlDataAdapter("select id from clienteobserva where idcliente=@idcliente", cnn);

            sda.SelectCommand.Parameters.Add("@idcliente", SqlDbType.Int).Value = idcliente;

            sda.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Excluir(clsParser.Int32Parse(row["id"].ToString()), cnn);
            }
        }

        public DataTable GridDados(Int32 idcliente, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "ID, " +
                        "IDCLIENTE, " +
                        "TIPOCONTATO, " +
                        "CONTATO, " +
                        "SETOR, " +
                        "EMAIL, " +
                        "DDD, " +
                        "TELEFONE,  " +
                        "RAMAL, " +
                        "DDDFAX,  " +
                        "TELEFAX,  " +
                        "RAMALFAX, " +
                        "DDDCELULAR,  " +
                        "CELULAR,  " +
                        "OBSERVA " +
                    "FROM  " +
                        "CLIENTEPROSPECCAOCONTATO " +
                    "WHERE " +
                        "IDCLIENTE = @idcliente ";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@idcliente", SqlDbType.Int).Value = idcliente;

            sda.Fill(dt);

            return dt;
        }
        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
                new GridColuna("Tipo", "tipocontato", 120, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Contato", "contato", 100, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Setor", "setor", 100, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("DDD", "ddd", 45, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Telefone", "telefone", 80, true, DataGridViewContentAlignment.MiddleCenter),
                new GridColuna("Email", "email", 190, true, DataGridViewContentAlignment.MiddleCenter),
        };
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 8, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "CONTATO");
        }

    }
}
