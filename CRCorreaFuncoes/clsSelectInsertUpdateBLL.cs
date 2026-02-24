using System;
using System.Data;
using System.Windows.Forms;

namespace CRCorreaFuncoes
{
    public class clsSelectInsertUpdateBLL
    {
        private static clsSelectInsertUpdateDAL clsSelectInsertUpdateDAL = new clsSelectInsertUpdateDAL();

        public static TextBox AutoComplete(String _conexao, String _tabela, String _campos, String _where, String _ordem, Boolean _com_ou_sem_ID)
        {
            TextBox _text = new TextBox();
            DataTable _table = clsSelectInsertUpdateDAL.Select(_conexao, _tabela, _campos, _where, _ordem);
            foreach (DataRow dgvrAutoComplete in _table.Rows)
            {
                if (_com_ou_sem_ID == true)
                {
                    _text.AutoCompleteCustomSource.Add(dgvrAutoComplete[_ordem].ToString() + " = " + dgvrAutoComplete["ID"].ToString());
                }
                else
                {
                    _text.AutoCompleteCustomSource.Add(dgvrAutoComplete[_ordem].ToString());
                }
            }
            return _text;
        }

        public static DataTable Select(String _conexao, String _tabela, String _campos_tabela, String _where, String _ordem)
        {           
            return clsSelectInsertUpdateDAL.Select(_conexao, _tabela, _campos_tabela, _where, _ordem);
        }

        public static DataRow SelectCollectionFields(String _conexao, String _campos_tabela, String _tabela, String _where, String _ordem)
        {
            return clsSelectInsertUpdateDAL.SelectCollectionFields(_conexao, _campos_tabela, _tabela, _where, _ordem);
        }

        public static String SelectOnlyField(String _conexao, String _campo_tabela, String _tabela, String _where, String _ordem)
        {
            return clsSelectInsertUpdateDAL.SelectOnlyField(_conexao, _campo_tabela, _tabela, _where, _ordem);
        }

        public static Int32 Insert(String _conexao, String _tabela, String _campos_tabela, String _campos_programa)
        {           
            return clsSelectInsertUpdateDAL.Insert(_conexao, _tabela, _campos_tabela, _campos_programa);
        }

        public static Int32 Update(String _conexao, String _tabela, String _campos_tabela_programa, String _where)
        {            
            return clsSelectInsertUpdateDAL.Update(_conexao, _tabela, _campos_tabela_programa, _where);
        }

        public static void Delete(String _conexao, String _tabela, String _where)
        {            
            clsSelectInsertUpdateDAL.Delete(_conexao, _tabela, _where);
        }

        public static Boolean ComparaValores(String[] _valoresatuais, String[] _valoresantigos)
        {
            for (Int32 I = _valoresatuais.Length - 1; I >= 0; I--)
            {
                if (_valoresatuais[I].ToString().Trim() != _valoresantigos[I].ToString().Trim())
                {
                    return false;
                }
            }
            return true;
        }
    }
}