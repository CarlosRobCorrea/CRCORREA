using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using CRCorreaFuncoes;
//using Aplisoft.DAL;
using CRCorreaInfo;


namespace CRCorreaBLL
{
    public class clsMovPecasMesBLL : SQLFactory<clsMovPecasMesInfo>
    {
        public clsMovPecasMesInfo Carregar(Int32 idcodigo, String anomes, String cnn)
        {
            Int32 id;
            

            id = clsParser.Int32Parse(Procedure.PesquisaoPrimeiro(cnn, "select id from movpecasmes where anomes=" + anomes + " and idcodigo=" + idcodigo));

            clsMovPecasMesInfo info = Carregar(id, cnn);

            return info;
        }

        public override int Incluir(clsMovPecasMesInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Incluir(info, cnn);
        }

        public override int Alterar(clsMovPecasMesInfo info, string cnn)
        {
            VerificaInfo(info);

            return base.Alterar(info, cnn);
        }

        public void VerificaInfo(clsMovPecasMesInfo _info)
        {

        }

        public DataTable GridDados(Int32 idcodigo, String cnn)
        {
            String query;
            DataTable dt;
            SqlDataAdapter sda;

            query = "SELECT " +
                        "ID, " +
                        "IDCODIGO, " +
                        "ANOMES, " +
                        "QTDEINICIO, " +
                        "QTDEENTRA, " +
                        "QTDESAIDA, " +
                        "QTDESALDO, " +
                        "CUSTOMEDIOMES, " +
                        "VALORACUMULADO " +
                    "FROM " +
                        "MOVPECASMES " +
                    "WHERE " +
                        "IDCODIGO = @IDCODIGO ";

            dt = new DataTable();

            sda = new SqlDataAdapter(query, cnn);
            sda.SelectCommand.Parameters.Add("@idcodigo", SqlDbType.Int).Value = idcodigo;
            sda.Fill(dt);

            return dt;
        }
    }
}
