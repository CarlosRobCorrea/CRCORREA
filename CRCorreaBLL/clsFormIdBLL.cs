using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using System.Data.SqlClient;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorreaBLL
{
    public class clsFormIdBLL
    {
        private String tabela;

        public clsFormIdBLL(String _tabela)
        {
            this.tabela = _tabela;
        }

        public clsFormIdInfo Carregar(Int32 id, String cnn)
        {
            clsFormIdInfo clsFormIdInfo = null;

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
                clsFormIdInfo = CarregaInfo(sdr);
            }

            scn.Close();

            return clsFormIdInfo;
        }

        public Int32 Incluir(clsFormIdInfo info, String cnn)
        {
            VerificaInfo(info);

            Int32 result = -1;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("INSERT " +
                                    "INTO " + tabela + " (IDCODIGO, CODIGO, NOME) VALUES (@IDCODIGO, @CODIGO, @NOME); " +
                                    "select SCOPE_IDENTITY()", scn);

            scd.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = info.idcodigo;
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

        public Int32 Alterar(clsFormIdInfo info, String cnn)
        {
            VerificaInfo(info);

            Int32 result = -1;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(cnn);
            scd = new SqlCommand("UPDATE " + tabela + " SET " +
                                    "IDCODIGO=@IDCODIGO, " +
                                    "CODIGO=@CODIGO, " +
                                    "NOME=@NOME " +
                                "WHERE " +
                                    "ID=@ID ", scn);

            scd.Parameters.Add("@ID", SqlDbType.Int).Value = info.id;
            scd.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = info.idcodigo;
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

        private clsFormIdInfo CarregaInfo(SqlDataReader _sdr)
        {
            clsFormIdInfo clsFormInfo = new clsFormIdInfo();

            clsFormInfo.id = clsParser.Int32Parse(_sdr["ID"].ToString());
            clsFormInfo.idcodigo = clsParser.Int32Parse(_sdr["IDCODIGO"].ToString());
            clsFormInfo.codigo = _sdr["CODIGO"].ToString();
            clsFormInfo.nome = _sdr["NOME"].ToString();

            return clsFormInfo;
        }

        private void VerificaInfo(clsFormIdInfo info)
        {
            if (info.codigo.Trim().Length == 0) throw new Exception("Código inválido.");
            if (info.nome.Trim().Length == 0) throw new Exception("Nome inválido.");
        }

        public bool Equals(clsFormIdInfo _info, clsFormIdInfo _info2)
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

            query = "SELECT ID, IDCODIGO, CODIGO, NOME FROM " + tabela + " ORDER BY CODIGO ";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dt);

            return dt;
        }

        public DataTable GridDados(Int32 idcodigo, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT ID, IDCODIGO, CODIGO, NOME FROM " + tabela + " WHERE IDCODIGO = @IDCODIGO ORDER BY CODIGO ";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@IDCODIGO", SqlDbType.Int).Value = idcodigo;
            sda.Fill(dt);

            return dt;
        }
    }
}
