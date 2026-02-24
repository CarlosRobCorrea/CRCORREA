using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CRCorreaFuncoes;

namespace CRCorreaFuncoes
{
    public class clsSqlFactoryValue
    {
        public string Nome { get; set; }
        public object Valor { get; set; }
    }
    public abstract class SQLFactory<T> where T : class
    {
        public virtual T Carregar(int id, string cnn)
        {
            T t = Activator.CreateInstance<T>();

            var query = new StringBuilder();

            query.Append("select ");
            query.Append(Campos(t, "", ""));
            query.Append(" from " + ObterNome(t));
            query.Append(" where id=" + id);

            var scn = new SqlConnection(cnn);
            var scd = new SqlCommand(query.ToString(), scn);

            scn.Open();

            var sdr = scd.ExecuteReader();

            if (sdr.Read())
            {
                for (int coluna = 0; coluna < sdr.FieldCount; coluna++)
                {
                    SetPropriedade(t, sdr.GetName(coluna), sdr[coluna]);
                }
            }

            sdr.Close();
            scn.Close();

            return t;
        }

        public virtual T Carregar(int id, SqlTransaction transaction)
        {
            T t = Activator.CreateInstance<T>();

            var query = new StringBuilder();

            query.Append("select ");
            query.Append(Campos(t, "", ""));
            query.Append(" from " + ObterNome(t));
            query.Append(" where id=" + id);

            var scd = new SqlCommand(query.ToString(), transaction.Connection, transaction);

            var sdr = scd.ExecuteReader();

            if (sdr.Read())
            {
                for (int coluna = 0; coluna < sdr.FieldCount; coluna++)
                {
                    SetPropriedade(t, sdr.GetName(coluna), sdr[coluna]);
                }
            }

            sdr.Close();

            return t;
        }

        public virtual T Carregar(List<clsSqlFactoryValue> chave_valor, String cnn)
        {
            T t = Activator.CreateInstance<T>();

            var query = new StringBuilder();

            query.Append("select ");
            query.Append(Campos(t, "", ""));
            query.Append(" from " + ObterNome(t));
            query.Append(" where ");

            for (int i = 0; i < chave_valor.Count; i++)
            {
                if (i > 0)
                {
                    query.Append(" and ");
                }

                if (chave_valor[i].Valor.GetType().FullName == "System.DateTime" && chave_valor[i].Valor != null)
                {
                    query.Append(" convert(nvarchar(10), " + chave_valor[i].Nome + ", 103) = ");
                }
                else
                {
                    query.Append(" " + chave_valor[i].Nome + " = ");
                }

                if (chave_valor[i].Valor.GetType().FullName == "System.Int32")
                {
                    query.Append(" " + chave_valor[i].Valor + " ");
                }
                else if (chave_valor[i].Valor.GetType().FullName == "System.DateTime" && chave_valor[i].Valor != null)
                {
                    query.Append(" '" + clsParser.DateTimeParse(chave_valor[i].Valor.ToString()).ToString("dd/MM/yyyy") + "' ");
                }
                else
                {
                    query.Append(" '" + chave_valor[i].Valor + "' ");
                }
            }

            var scn = new SqlConnection(cnn);
            var scd = new SqlCommand(query.ToString(), scn);

            scn.Open();

            var sdr = scd.ExecuteReader();

            if (sdr.Read())
            {
                for (int coluna = 0; coluna < sdr.FieldCount; coluna++)
                {
                    SetPropriedade(t, sdr.GetName(coluna), sdr[coluna]);
                }
            }

            sdr.Close();
            scn.Close();

            return t;
        }

        public virtual T Carregar(List<clsSqlFactoryValue> chave_valor, SqlTransaction transaction)
        {
            T t = Activator.CreateInstance<T>();

            var query = new StringBuilder();

            query.Append("select ");
            query.Append(Campos(t, "", ""));
            query.Append(" from " + ObterNome(t));
            query.Append(" where ");

            for (int i = 0; i < chave_valor.Count; i++)
            {
                if (i > 0)
                {
                    query.Append(" and ");
                }

                if (chave_valor[i].Valor.GetType().FullName == "System.DateTime" && chave_valor[i].Valor != null)
                {
                    query.Append(" convert(nvarchar(10), " + chave_valor[i].Nome + ", 103) = ");
                }
                else
                {
                    query.Append(" " + chave_valor[i].Nome + " = ");
                }

                if (chave_valor[i].Valor.GetType().FullName == "System.Int32")
                {
                    query.Append(" " + chave_valor[i].Valor + " ");
                }
                else if (chave_valor[i].Valor.GetType().FullName == "System.DateTime" && chave_valor[i].Valor != null)
                {
                    query.Append(" '" + clsParser.DateTimeParse(chave_valor[i].Valor.ToString()).ToString("dd/MM/yyyy") + "' ");
                }
                else
                {
                    query.Append(" '" + chave_valor[i].Valor + "' ");
                }
            }

            var scd = new SqlCommand(query.ToString(), transaction.Connection, transaction);

            var sdr = scd.ExecuteReader();

            if (sdr.Read())
            {
                for (int coluna = 0; coluna < sdr.FieldCount; coluna++)
                {
                    SetPropriedade(t, sdr.GetName(coluna), sdr[coluna]);
                }
            }

            sdr.Close();

            return t;
        }

        public static string ObterValor(string query, string cnn)
        {
            string result = null;

            var scn = new SqlConnection(cnn);
            var scd = new SqlCommand(query, scn);

            scn.Open();
            var sdr = scd.ExecuteReader();

            if (sdr.Read())
            {
                result = sdr[0].ToString();
            }

            sdr.Close();
            scn.Close();

            return result;
        }

        public static string ObterValor(string query, SqlTransaction transaction)
        {
            string result = null;

            var scd = new SqlCommand(query, transaction.Connection);

            var sdr = scd.ExecuteReader();

            if (sdr.Read())
            {
                result = sdr[0].ToString();
            }

            sdr.Close();

            return result;
        }

        public bool Equals(T info, T info_2)
        {
            PropertyInfo[] pi;
            PropertyInfo[] pi_2;

            object o;
            object o2;

            pi = info.GetType().GetProperties();
            pi_2 = info_2.GetType().GetProperties();

            for (int i = 0; i < pi.Length; i++)
            {
                if (pi[i].CanRead == true &&
                    pi[i].CanWrite == true)
                {
                    o = pi[i].GetValue(info, null);
                    o2 = pi_2[i].GetValue(info_2, null);

                    if ((o != null && o.Equals(o2) == false) ||
                        (o2 != null && o2.Equals(o) == false))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public virtual int Incluir(T info, string cnn)
        {
            var query = new StringBuilder();

            query.Append("insert into " + ObterNome(info) + "(");
            query.Append(Campos(info, "", "id"));
            query.Append(") values (");
            query.Append(Campos(info, "@", "id") + ");");
            query.Append("select SCOPE_IDENTITY()");

            var scn = new SqlConnection(cnn);
            var scd = new SqlCommand(query.ToString(), scn);
            scd.CommandTimeout = 100;
            foreach (PropertyInfo pi in info.GetType().GetProperties())
            {
                if (pi.CanRead == true &&
                    pi.CanWrite == true &&
                    pi.Name.ToLower() != "id")
                {
                    object o = pi.GetValue(info, null);

                    if (pi.PropertyType.FullName == "System.Int32")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Int).Value = 0;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Int).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Int64")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.BigInt).Value = 0;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.BigInt).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.String")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.NVarChar).Value = string.Empty;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.NVarChar).Value = o.ToString();
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Double" || (o != null && o.GetType().FullName == "System.Decimal"))
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Decimal).Value = 0;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Decimal).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName.IndexOf("System.DateTime") != -1)
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.DateTime).Value = SqlDateTime.Null;
                        }
                        else
                        {
                            DateTime data = (DateTime)o;
                            if (data.Day == 1 && data.Month == 1 && data.Year == 1)
                            {
                                scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.DateTime).Value = SqlDateTime.Null;
                            }
                            else
                            {
                                scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.DateTime).Value = o;
                            }
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Boolean")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Bit).Value = false;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Bit).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Drawing.Image")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.VarBinary).Value = SqlBinary.Null;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.VarBinary).Value = clsVisual.ImageToByteArray((System.Drawing.Image)o, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                }
            }

            scn.Open();

            var id = clsParser.Int32Parse(scd.ExecuteScalar().ToString());

            scn.Close();

            return id;
        }

        public virtual int Incluir(T info, SqlTransaction transaction)
        {
            var query = new StringBuilder();

            query.Append("insert into " + ObterNome(info) + "(");
            query.Append(Campos(info, "", "id"));
            query.Append(") values (");
            query.Append(Campos(info, "@", "id") + ");");
            query.Append("select SCOPE_IDENTITY()");

            var scd = new SqlCommand(query.ToString(), transaction.Connection, transaction);

            foreach (PropertyInfo pi in info.GetType().GetProperties())
            {
                if (pi.CanRead == true &&
                    pi.CanWrite == true &&
                    pi.Name.ToLower() != "id")
                {
                    object o = pi.GetValue(info, null);

                    if (pi.PropertyType.FullName == "System.Int32")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Int).Value = 0;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Int).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Int64")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.BigInt).Value = 0;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.BigInt).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.String")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.NVarChar).Value = string.Empty;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.NVarChar).Value = o.ToString();
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Double" || (o != null && o.GetType().FullName == "System.Decimal"))
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Decimal).Value = 0;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Decimal).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName.IndexOf("System.DateTime") != -1)
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.DateTime).Value = SqlDateTime.Null;
                        }
                        else
                        {
                            DateTime data = (DateTime)o;
                            if (data.Day == 1 && data.Month == 1 && data.Year == 1)
                            {
                                scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.DateTime).Value = SqlDateTime.Null;
                            }
                            else
                            {
                                scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.DateTime).Value = o;
                            }
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Boolean")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Bit).Value = false;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Bit).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Drawing.Image")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.VarBinary).Value = SqlBinary.Null;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.VarBinary).Value = clsVisual.ImageToByteArray((System.Drawing.Image)o, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                }
            }

            var id = clsParser.Int32Parse(scd.ExecuteScalar().ToString());

            return id;
        }

        public virtual int Alterar(T info, string cnn)
        {
            PropertyInfo piId = info.GetType().GetProperty("Id");

            if (piId == null)
            {
                piId = info.GetType().GetProperty("id");
            }

            if (piId == null)
            {
                piId = info.GetType().GetProperty("ID");
            }

            var query = new StringBuilder();

            query.Append("update " + ObterNome(info) + " set ");
            query.Append(CamposRepete(info, "id"));
            query.Append(" where id = " + piId.GetValue(info, null).ToString());

            var scn = new SqlConnection(cnn);
            var scd = new SqlCommand(query.ToString(), scn);

            foreach (PropertyInfo pi in info.GetType().GetProperties())
            {
                if (pi.CanRead == true && pi.CanWrite == true && pi.Name.ToLower() != "id")
                {
                    object o = pi.GetValue(info, null);

                    if (pi.PropertyType.FullName == "System.Int32")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Int).Value = SqlInt32.Null;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Int).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Int64")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.BigInt).Value = SqlInt64.Null;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.BigInt).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.String")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.NVarChar).Value = SqlString.Null;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.NVarChar).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Double" || (o != null && o.GetType().FullName == "System.Decimal"))
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Decimal).Value = SqlDecimal.Null;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Decimal).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName.IndexOf("System.DateTime") != -1)
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.DateTime).Value = SqlDateTime.Null;
                        }
                        else
                        {
                            DateTime data = (DateTime)o;
                            if (data.Day == 1 && data.Month == 1 && data.Year == 1)
                            {
                                scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.DateTime).Value = SqlDateTime.Null;
                            }
                            else
                            {
                                scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.DateTime).Value = o;
                            }
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Boolean")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Bit).Value = false;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Bit).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Drawing.Image")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.VarBinary).Value = SqlBinary.Null;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.VarBinary).Value = clsVisual.ImageToByteArray((System.Drawing.Image)o, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                }
            }

            scn.Open();

            var resultado = clsParser.Int32Parse(scd.ExecuteNonQuery().ToString());

            scn.Close();

            return resultado;
        }

        public virtual int Alterar(T info, SqlTransaction transaction)
        {
            PropertyInfo piId = info.GetType().GetProperty("Id");

            if (piId == null)
            {
                piId = info.GetType().GetProperty("id");
            }

            if (piId == null)
            {
                piId = info.GetType().GetProperty("ID");
            }

            var query = new StringBuilder();

            query.Append("update " + ObterNome(info) + " set ");
            query.Append(CamposRepete(info, "id"));
            query.Append(" where id = " + piId.GetValue(info, null).ToString());

            var scd = new SqlCommand(query.ToString(), transaction.Connection, transaction);

            foreach (PropertyInfo pi in info.GetType().GetProperties())
            {
                if (pi.CanRead == true && pi.CanWrite == true && pi.Name.ToLower() != "id")
                {
                    object o = pi.GetValue(info, null);

                    if (pi.PropertyType.FullName == "System.Int32")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Int).Value = SqlInt32.Null;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Int).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Int64")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.BigInt).Value = SqlInt64.Null;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.BigInt).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.String")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.NVarChar).Value = SqlString.Null;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.NVarChar).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Double" || (o != null && o.GetType().FullName == "System.Decimal"))
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Decimal).Value = SqlDecimal.Null;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Decimal).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName.IndexOf("System.DateTime") != -1)
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.DateTime).Value = SqlDateTime.Null;
                        }
                        else
                        {
                            DateTime data = (DateTime)o;
                            if (data.Day == 1 && data.Month == 1 && data.Year == 1)
                            {
                                scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.DateTime).Value = SqlDateTime.Null;
                            }
                            else
                            {
                                scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.DateTime).Value = o;
                            }
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Boolean")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Bit).Value = false;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.Bit).Value = o;
                        }
                    }
                    else if (pi.PropertyType.FullName == "System.Drawing.Image")
                    {
                        if (o == null)
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.VarBinary).Value = SqlBinary.Null;
                        }
                        else
                        {
                            scd.Parameters.Add("@" + pi.Name, System.Data.SqlDbType.VarBinary).Value = clsVisual.ImageToByteArray((System.Drawing.Image)o, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                }
            }

            var resultado = clsParser.Int32Parse(scd.ExecuteNonQuery().ToString());

            return resultado;
        }

        public virtual bool Excluir(int id, string cnn)
        {
            T t = Activator.CreateInstance<T>();

            var query = "delete " + ObterNome(t) + " where id = " + id;

            var scn = new SqlConnection(cnn);
            var scd = new SqlCommand(query, scn);

            scn.Open();

            var resultado = clsParser.Int32Parse(scd.ExecuteNonQuery().ToString());

            scn.Close();

            return resultado != 0;
        }

        public virtual bool Excluir(int id, SqlTransaction transaction)
        {
            T t = Activator.CreateInstance<T>();

            var query = "delete " + ObterNome(t) + " where id = " + id;

            var scd = new SqlCommand(query, transaction.Connection, transaction);

            var resultado = clsParser.Int32Parse(scd.ExecuteNonQuery().ToString());

            return resultado != 0;
        }

        private string ObterNome(T t)
        {
            var nome = t.GetType().Name;

            nome = nome.Substring(3);
            nome = nome.Substring(0, nome.Length - 4);

            if (nome.ToUpper() == "CADCLIENTE")
            {
                nome = "CLIENTE";
            }
            else if (nome.ToUpper() == "DANFE310")
            {
                nome = "NFVENDA";
            }
            else if (nome.ToUpper() == "DANFE310_ITEM")
            {
                nome = "NFVENDA1";
            }
            else if (nome.ToUpper() == "DANFE310REFERENCIA")
            {
                nome = "NFVENDAREFERENCIA";
            }
            else if (nome.ToUpper() == "MOVPECAS")
            {
                nome = "MOVPECAS";
            }
            else if (nome.ToUpper() == "NFE")
            {
                nome = "NFCOMPRA";
            }
            else if (nome.ToUpper() == "NFE1")
            {
                nome = "NFCOMPRA1";
            }
            else if (nome.ToUpper() == "NFEPAGAR")
            {
                nome = "NFCOMPRAPAGAR";
            }
            else if (nome == "MovPecas")
            {
                nome = "MovPecas";
            }
            else if (nome == "UsuarioNew")
            {
                nome = "Usuario";
            }
            else if (nome == "PecasCadastro")
            {
                nome = "Pecas";
            }

            //FOLHA
            if (nome.ToUpper() == "RELOGIOS")
            {
                nome = "Catraca";
            }

            nome = nome.ToUpper().Replace("_APLI", string.Empty);

            return nome;
        }

        private void SetPropriedade(object alvo, string propriedadeNome, object valor)
        {
            var property = alvo.GetType().GetProperty(propriedadeNome);

            try
            {
                if (property.PropertyType.FullName == "System.Drawing.Image")
                {
                    if (valor == null)
                    {
                        valor = Properties.Resources.Cancelar;
                    }
                    else
                    {
                        valor = clsVisual.ByteArrayToImage((byte[])valor);
                    }

                    property.SetValue(alvo, valor, null);
                }
                else if (property.PropertyType.FullName == "System.Decimal" &&
                         valor.GetType().FullName == "System.Double")
                {
                    property.SetValue(alvo, clsParser.DecimalParse(valor.ToString()), null);
                }
                else if (property.PropertyType.FullName == "System.Double" &&
                         valor.GetType().FullName == "System.Decimal")
                {
                    property.SetValue(alvo, clsParser.DoubleParse(valor.ToString()), null);
                }
                else if (property.PropertyType.FullName == "System.Decimal" &&
                         valor.GetType().FullName == "System.String")
                {
                    property.SetValue(alvo, clsParser.DecimalParse(valor.ToString()), null);
                }
                else if (property.PropertyType.FullName == "System.Decimal" &&
                         valor.GetType().FullName == "System.DBNull")
                {
                    property.SetValue(alvo, clsParser.DecimalParse(valor.ToString()), null);
                }
                else if (property.PropertyType.FullName == "System.Int32" &&
                         valor.GetType().FullName == "System.DBNull")
                {
                    property.SetValue(alvo, clsParser.Int32Parse(valor.ToString()), null);
                }
                else
                {
                    property.SetValue(alvo, valor, null);
                }
            }
            catch
            {

            }
        }

        private string Campos(T t, string prefixo, string ignorarcampo)
        {
            var query = string.Empty;

            foreach (PropertyInfo pi in t.GetType().GetProperties())
            {
                if (pi.CanRead == true && pi.CanWrite == true && pi.Name.ToLower() != ignorarcampo.ToLower())
                {
                    if (query != "")
                    {
                        query += ", ";
                    }

                    query += prefixo + pi.Name;
                }
            }

            return query;
        }

        private string CamposRepete(T t, string ignorarcampo)
        {
            string query = string.Empty;

            foreach (PropertyInfo pi in t.GetType().GetProperties())
            {
                if (pi.CanRead == true &&
                    pi.CanWrite == true &&
                    pi.Name.ToLower() != ignorarcampo.ToLower())
                {
                    if (query != string.Empty)
                    {
                        query += ", ";
                    }

                    query += pi.Name + " = @" + pi.Name;
                }
            }

            return query;
        }
    }

}
