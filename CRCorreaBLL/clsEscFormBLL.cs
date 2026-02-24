using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


using CRCorreaFuncoes;
using CRCorreaInfo;
using System.Data.SqlClient;


namespace CRCorreaBLL
{
    public class clsEscFormBLL
    {
        private String tabela;

        public clsEscFormBLL(String tabela)
        {
            this.tabela = tabela;
        }

        public clsEscFormInfo Carregar(Int32 id, String cnn)
        {
            clsEscFormInfo clsFormInfo = null;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("SELECT * FROM " + tabela + " WHERE ID=@ID", scn);
            scd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            scn.Open();

            sdr = scd.ExecuteReader();

            if (sdr.Read() == true)
            {
                clsFormInfo = CarregaInfo(sdr);
            }

            scn.Close();

            return clsFormInfo;
        }

        public Int32 Incluir(clsEscFormInfo info, String cnn)
        {
            VerificaInfo(info);

            Int32 result = -1;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("INSERT " +
                                    "INTO " + tabela + " (CODIGO, NOME) VALUES (@CODIGO, @NOME); " +
                                    "select SCOPE_IDENTITY()", scn);

            scd.Parameters.Add("@CODIGO", SqlDbType.NVarChar).Value = info.codigo;
            scd.Parameters.Add("@NOME", SqlDbType.NVarChar).Value = info.nome;

            scn.Open();

            sdr = scd.ExecuteReader();

            if (sdr.Read() == true)
            {
                result = clsParser.Int32Parse(sdr[0].ToString());
            }

            scn.Close();

            return result;
        }

        public Int32 Alterar(clsEscFormInfo info, String cnn)
        {
            VerificaInfo(info);

            Int32 result = -1;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("UPDATE " + tabela + " SET " +
                                    "CODIGO=@CODIGO, " +
                                    "NOME=@NOME " +
                                "WHERE " +
                                    "ID=@ID ", scn);

            scd.Parameters.Add("@ID", SqlDbType.Int).Value = info.id;
            scd.Parameters.Add("@CODIGO", SqlDbType.NVarChar).Value = info.codigo;
            scd.Parameters.Add("@NOME", SqlDbType.NVarChar).Value = info.nome;

            scn.Open();

            sdr = scd.ExecuteReader();

            if (sdr.Read() == true)
            {
                result = clsParser.Int32Parse(sdr[0].ToString());
            }

            scn.Close();

            return result;
        }

        public void Excluir(Int32 id, String cnn)
        {
            SqlConnection scn;
            SqlCommand scd;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("DELETE " + tabela + " WHERE ID=@ID ", scn);

            scd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            scn.Open();

            scd.ExecuteNonQuery();

            scn.Close();
        }

        private clsEscFormInfo CarregaInfo(SqlDataReader _sdr)
        {
            clsEscFormInfo clsFormInfo = new clsEscFormInfo();

            clsFormInfo.id = clsParser.Int32Parse(_sdr["ID"].ToString());
            clsFormInfo.codigo = _sdr["CODIGO"].ToString();
            clsFormInfo.nome = _sdr["NOME"].ToString();

            return clsFormInfo;
        }

        private void VerificaInfo(clsEscFormInfo info)
        {
            if (info.codigo.Trim().Length == 0) throw new Exception("Código inválido.");
            if (info.nome.Trim().Length == 0) throw new Exception("Nome inválido.");
        }

        public bool Equals(clsEscFormInfo _info, clsEscFormInfo _info2)
        {
            if (_info.codigo != _info2.codigo)
            {
                return false;
            }
            if (_info.nome != _info2.nome)
            {
                return false;
            }
            return true;
        }

        public DataTable GridDados(String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT ID, CODIGO, NOME FROM " + tabela + " ORDER BY CODIGO ";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dt);

            return dt;
        }

        public DataTable GridDados(String campo, String filtro, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            if (filtro == null)
            {
                filtro = "";
            }

            if (filtro == "")
            {
                query = "SELECT ID, CODIGO, NOME FROM " + tabela + " ORDER BY CODIGO";
            }
            else
            {
                query = "SELECT ID, CODIGO, NOME FROM " + tabela + " WHERE " + campo + " = '" + filtro + "'";
            }

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);

            if (filtro != "")
            {
                sda.SelectCommand.Parameters.Add("@" + campo, SqlDbType.NVarChar).Value = filtro;
            }

            sda.Fill(dt);

            return dt;
        }

        public DataTable GridDados(String campo, int filtro, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT ID, CODIGO, NOME FROM " + tabela + " WHERE " + campo + " = " + filtro + " ";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@" + campo, SqlDbType.Int).Value = filtro;

            sda.Fill(dt);

            return dt;
        }

        public DataTable GridDadosPes(String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            if (tabela == "")
            {
                query = "SELECT ID, CODIGO, NOME FROM " + tabela + " ORDER BY CODIGO";
            }
            else
            {
                query = "";
            }

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dt);

            return dt;
        }
    }
}
