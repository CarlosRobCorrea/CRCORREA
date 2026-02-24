using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Drawing;

using CRCorreaFuncoes;
using CRCorreaInfo;
using System.Windows.Forms;

namespace CRCorreaBLL
{
    public class clsClienteObservaBLL : SQLFactory<clsClienteObservaInfo>
    {
        public override int Incluir(clsClienteObservaInfo info, string cnn)
        {
            Int32 result;

            VerificaInfo(info);

            result = base.Incluir(info, cnn);

            if (info.dataagenda != null && info.dataagenda != DateTime.MinValue)
            {
                clsClienteObservaInfo info2 = new clsClienteObservaInfo();

                info2.contatado = info.contatoagenda;
                info2.contatoagenda = "";
                info2.contatofim = info.contatofim;
                info2.data = new DateTime(info.dataagenda.Year, info.dataagenda.Month, info.dataagenda.Day, info.dataagenda.Hour, info.dataagenda.Minute, info.dataagenda.Second);
                info2.dataagenda = DateTime.MinValue;
                info2.datafim = info.datafim;
                info2.emitente = info.emitente;
                info2.fechada = info.fechada;
                info2.idcliente = info.idcliente;
                info2.idemitente = info.idemitente;
                info2.idreferencia = info.idreferencia;
                info2.idvendedor = info.idvendedor;
                info2.ligacao = "A";
                info2.observar = info.observaragenda;
                info2.observaragenda = "";
                info2.documento = info.documento;


                base.Incluir(info2, cnn);
            }
            return result;
        }

        public override int Alterar(clsClienteObservaInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Alterar(info, cnn);
        }

        public void ExcluirCliente(String conexao, Int32 idcliente)
        {
            SqlConnection scn;
            SqlCommand scd;

            scn = new SqlConnection(conexao);
            scd = new SqlCommand("delete clienteobserva where idcliente = @idcliente", scn);
            scd.Parameters.Add("@idcliente", SqlDbType.Int).Value = idcliente;

            scn.Open();
            scd.ExecuteNonQuery();
            scn.Close();
        }

        public void VerificaInfo(clsClienteObservaInfo info)
        {
            if (info.ligacao.Length > 1)
            {
                throw new Exception("clienteobserva - Ligacao ultrapassou o limite permitido (1 caracteres).");
            }

            if (info.emitente.Length > 20)
            {
                throw new Exception("clienteobserva - Emitente ultrapassou o limite permitido (20 caracteres).");
            }

            if (info.fechada.Length > 1)
            {
                throw new Exception("clienteobserva - Fechada ultrapassou o limite permitido (1 caracteres).");
            }

            if (info.contatado.Length > 120)
            {
                throw new Exception("clienteobserva - Contatado ultrapassou o limite permitido (20 caracteres).");
            }

            if (info.contatoagenda.Length > 20)
            {
                throw new Exception("clienteobserva - Contatoagenda ultrapassou o limite permitido (20 caracteres).");
            }

            if (info.contatofim.Length > 20)
            {
                throw new Exception("clienteobserva - Contatofim ultrapassou o limite permitido (20 caracteres).");
            }
            if (info.idvendedor <= 0)
            {
                throw new Exception("Clienteobserva - Follow - Up Falta preencher Destinatário."); 
            }
        }

        public DataTable GridDados(Int32 idcliente, String conexao)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;
                query = "select " +
                "clienteobserva.id, " +
                "clienteobserva.data, " +
                "usuario.usuario as [Emitente], " +
                "case clienteobserva.ligacao " +
                    "when 'S' then 'S - Ligou para'  " +
                    "when 'E' then 'E - Recebeu Ligação'  " +
                    "when 'I' then 'I - Interno para'  " +
                    "when 'A' then 'A - Agendamento'  " +
                    "when 'Z' then 'Z - Interno CAC'  " +
                "end as ligacao,  " +
                "clienteobserva.contatado [Contato], " +
                "usuariovendedor.usuario AS [destinatario], " +
                "clienteobserva.observar, " +
                "case clienteobserva.fechada " +
                    "when 'A' then 'A - Aberta'  " +
                    "when 'F' then 'F - Fechada'  " +
                    "when 'G' then 'G - Agenda'  " +
                "end as fechada,  " +
                "clienteobserva.IDCLIENTE, " +
                "clienteobserva.IDREFERENCIA, " +
                "clienteobserva.IDVENDEDOR, " +
                "clienteobserva.DATAAGENDA, " +
                "clienteobserva.OBSERVARAGENDA, " +
                "clienteobserva.CONTATADO, " +
                "clienteobserva.CONTATOAGENDA, " +
                "clienteobserva.CONTATOFIM, " +
                "clienteobserva.DATAFIM, " +
                "clienteobserva.IDEMITENTE, " +
                "clienteobserva.DOCUMENTO " +
        "from " +
            "clienteobserva   " +
                "left join usuario on usuario.id = clienteobserva.idemitente  " +
                "left join usuario usuariovendedor on usuariovendedor.id = clienteobserva.idvendedor " +
        "where " +
            "idcliente = @idcliente ";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, conexao);
            sda.SelectCommand.Parameters.Add("@idcliente", SqlDbType.Int).Value = idcliente;
            sda.Fill(dt);

            return dt;
        }

        public DataTable GridDadosFollowUp(Int32 idcliente, Int32 idorcamento, String conexao)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "clienteobserva.ID, " +
                        "clienteobserva.IDCLIENTE, " +
                        "clienteobserva.IDREFERENCIA, " +                       
                        "clienteobserva.LIGACAO, " +
                        "clienteobserva.DATA, " +
                        "clienteobserva.IDEMITENTE, " +
                        "USUARIO_EMITENTE.USUARIO AS EMITENTE, " +
                        "USUARIO_VENDEDOR.USUARIO AS USUARIO, " +
                        "clienteobserva.CONTATADO, " +
                        "clienteobserva.IDVENDEDOR, " +
                        "clienteobserva.OBSERVAR, " +
                        "clienteobserva.DATAAGENDA, " +
                        "clienteobserva.OBSERVARAGENDA, " +
                        "clienteobserva.CONTATOAGENDA, " +
                        "clienteobserva.CONTATOFIM, " +
                        "clienteobserva.DATAFIM, " +
                        "clienteobserva.FECHADA, " +
                        "clienteobserva.DOCUMENTO " +
                    "FROM " +
                        "clienteobserva " +
                            "INNER JOIN CLIENTEPROSPECCAO ON clienteobserva.IDCLIENTE = CLIENTEPROSPECCAO.ID " +
                            "LEFT JOIN USUARIO AS USUARIO_VENDEDOR ON clienteobserva.IDVENDEDOR = USUARIO_VENDEDOR.ID " +
                            "INNER JOIN USUARIO AS USUARIO_EMITENTE ON clienteobserva.IDEMITENTE = USUARIO_EMITENTE.ID " +
                    "WHERE " +
                        "clienteobserva.IDCLIENTE = @IDCLIENTE " +
                        "AND clienteobserva.IDREFERENCIA = @IDORCAMENTO";

            dt = new DataTable();
            sda = new SqlDataAdapter(query, conexao);
            sda.SelectCommand.Parameters.Add("@idcliente", SqlDbType.Int).Value = idcliente;
            sda.SelectCommand.Parameters.Add("@idorcamento", SqlDbType.Int).Value = idorcamento;
            sda.Fill(dt);

            return dt;
        }

        private static GridColuna[] dtGridColunasDadosFollowUp = new GridColuna[]
        {
            new GridColuna("Id","ID", 85, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("IDCLIENTE","IDCLIENTE", 85, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("IDREFERENCIA","IDREFERENCIA", 85, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Ligação","LIGACAO", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Data","DATA", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("IDEMITENTE","IDEMITENTE", 85, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Emitente","EMITENTE", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Usuario","USUARIO", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Contatado","CONTATADO", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("IDVENDEDOR","IDVENDEDOR", 85, false, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Observar","OBSERVAR", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Data Agenda","DATAAGENDA", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Obs Agenda","OBSERVARAGENDA", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Contato","CONTATOAGENDA", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Contato Final","CONTATOFIM", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Data Final","DATAFIM", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Fechada","FECHADA", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Doc","DOCUMENTO", 85, true, DataGridViewContentAlignment.MiddleCenter)
        };

        public static void GridMontaFollowUp(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunasDadosFollowUp, true);

            dgv.Font = new Font("Tahoma", 8, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "data");
            
        }

        public DataTable GridDadosAgenda(Int32 idemitente, Int32 idvendedor, String fechada, String conexao)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "clienteobserva.ID, " +
                        "clienteobserva.IDCLIENTE, " +
                        "clienteobserva.IDREFERENCIA, " +
                        "CLIENTEPROSPECCAO.COGNOME, " +
                        "clienteobserva.IDEMITENTE, " +
                        "USUARIO_EMITENTE.USUARIO AS EMITENTE, " +
                        "clienteobserva.IDVENDEDOR, " +
                        "USUARIO.USUARIO as DESTINATARIO, " +
                        "clienteobserva.LIGACAO, " +
                        "clienteobserva.DATA, " +
                        "clienteobserva.OBSERVAR, " +
                        "clienteobserva.CONTATADO, " +
                        "clienteobserva.DATAAGENDA, " +
                        "clienteobserva.OBSERVARAGENDA, " +
                        "clienteobserva.CONTATOAGENDA, " +
                        "clienteobserva.CONTATOFIM, " +
                        "clienteobserva.DATAFIM, " +
                        "clienteobserva.FECHADA, " +
                        "clienteobserva.DOCUMENTO " +
                    "FROM " +
                        "clienteobserva " +
                            "INNER JOIN CLIENTEPROSPECCAO ON clienteobserva.IDCLIENTE = CLIENTEPROSPECCAO.ID " +
                            "INNER JOIN USUARIO ON clienteobserva.IDVENDEDOR = USUARIO.ID " +
                            "INNER JOIN USUARIO AS USUARIO_EMITENTE ON clienteobserva.IDEMITENTE = USUARIO_EMITENTE.ID " +
                    "WHERE " +
                        "clienteobserva.FECHADA = @FECHADA ";

            if (idvendedor > 0)
            {
                query += " and clienteobserva.IDVENDEDOR = @IDVENDEDOR ";
            }
            else
            {
                query += " and clienteobserva.IDEMITENTE = @IDEMITENTE ";
            }

            dt = new DataTable();
            sda = new SqlDataAdapter(query, conexao);
            sda.SelectCommand.Parameters.Add("@fechada", SqlDbType.Int).Value = fechada;

            if (idvendedor > 0)
            {
                sda.SelectCommand.Parameters.Add("@idvendedor", SqlDbType.Int).Value = idvendedor;
            }
            else
            {
                sda.SelectCommand.Parameters.Add("@idemitente", SqlDbType.Int).Value = idemitente;
            }
            sda.Fill(dt);
            return dt;
        }

        public DataTable GridDadosCAC(Int32 idapartamento, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;
            query = "SELECT " +
                        "clienteobserva.ID, " +
                        "clienteobserva.IDCLIENTE, " +
                        "clienteobserva.IDREFERENCIA, " +
                        "clienteobserva.LIGACAO, " +
                        "clienteobserva.DATA, " +
                        "clienteobserva.OBSERVAR, " +
                        "clienteobserva.EMITENTE, " +
                        "clienteobserva.FECHADA, " +
                        "clienteobserva.IDVENDEDOR, " +
                        "clienteobserva.DATAAGENDA, " +
                        "clienteobserva.OBSERVARAGENDA, " +
                        "clienteobserva.CONTATADO, " +
                        "clienteobserva.CONTATOAGENDA, " +
                        "clienteobserva.CONTATOFIM, " +
                        "clienteobserva.DATAFIM, " +
                        "clienteobserva.IDEMITENTE, " +
                        "clienteobserva.IDANTIGO, " +
                        "clienteobserva.DOCUMENTO, " +
                        "USUARIO.USUARIO " +
                    "FROM " +
                        "clienteobserva " +
                            "LEFT JOIN USUARIO ON USUARIO.ID = clienteobserva.IDVENDEDOR " +
                    "WHERE " +
                        "IDREFERENCIA = @IDREFERENCIA " +
                    "ORDER BY clienteobserva.DATA DESC ";
            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDREFERENCIA", SqlDbType.Int).Value = idapartamento;
            sda.Fill(dt);
            return dt;
        }

        public GridColuna[] gcaCAC = new GridColuna[]
        {
            new GridColuna("Data", "DATA", 85, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("L.", "LIGACAO", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Emitente", "EMITENTE", 85, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Contatado", "CONTATADO", 85, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Destinado p/", "USUARIO", 85, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Observação", "OBSERVAR", 490, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("F.", "FECHADA", 20, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Data F.", "DATAFIM", 85, true, DataGridViewContentAlignment.MiddleCenter)
        };


        public void GridMontaCAC(DataGridView dgv, DataTable dt)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, gcaCAC, true);
            clsGridHelper.FontGrid(dgv, 7);
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgv.Columns["DATAFIM"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        public GridColuna[] clienteobservaColuna = new GridColuna[]
        {
                new GridColuna("Id", "Id", 0, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna(" Em : ", "DATA", 90, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Emitente", "EMITENTE", 75, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Tipo", "LIGACAO", 110, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Contatado", "CONTATO", 100, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Destinado", "destinatario", 75, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Assunto", "OBSERVAR", 330, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Ok", "FECHADA", 80, true, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Id", "IDCLIENTE", 0, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Id", "IDREFERENCIA", 0, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Id", "IDVENDEDOR", 0, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Id", "DATAAGENDA", 0, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Id", "OBSERVARAGENDA", 0, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Id", "CONTATADO", 0, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Id", "CONTATOAGENDA", 0, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Id", "CONTATOFIM", 0, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Id", "DATAFIM", 0, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Id", "IDEMITENTE", 0, false, DataGridViewContentAlignment.MiddleLeft),
                new GridColuna("Id", "DOCUMENTO", 0, false, DataGridViewContentAlignment.MiddleLeft)/*,
                new GridColuna("Id", "IDANTIGO", 0, false, DataGridViewContentAlignment.MiddleLeft)*/
            };


        public void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, clienteobservaColuna, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "data");
            dgv.Columns["DATA"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            //dgv.Columns["ultdatorc"].DefaultCellStyle.Format = "dd/MM/yyyy";

        }

    }
}
