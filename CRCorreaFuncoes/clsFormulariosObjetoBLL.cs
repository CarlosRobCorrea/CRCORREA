using System;
using System.Data;
using CRCorreaInfo;

namespace CRCorreaFuncoes
{
    public class clsFormulariosObjetoBLL
    {
        // - Functions
        public clsFormulariosObjetoInfo Carregar(String _conexao, Int32 _id)
        {
            if (_id > 0 && _conexao.Length > 0)
            {
                clsFormulariosObjetoDAL clsFormulariosobjetoDAL = new clsFormulariosObjetoDAL();
                clsFormulariosObjetoInfo clsFormulariosobjetoInfo;
                clsFormulariosobjetoInfo = clsFormulariosobjetoDAL.Carregar(_conexao, _id);
                if (clsFormulariosobjetoInfo != null)
                {
                    return clsFormulariosobjetoInfo;
                }
                else
                {
                    return null;
                    //throw new Exception("Registro não encontrado.");
                }
            }
            else
            {
                //throw new Exception("Erro.");
                return null;
            }
        }

        public clsFormulariosObjetoInfo CarregarName(String _conexao, String _Name, Int32 _idFormulario)
        {
            if (_idFormulario > 0 && _conexao.Length > 0)
            {
                clsFormulariosObjetoDAL clsFormulariosobjetoDAL = new clsFormulariosObjetoDAL();
                clsFormulariosObjetoInfo clsFormulariosobjetoInfo;
                clsFormulariosobjetoInfo = clsFormulariosobjetoDAL.CarregarName(_conexao, _Name, _idFormulario);
                if (clsFormulariosobjetoInfo != null)
                {
                    return clsFormulariosobjetoInfo;
                }
                else
                {
                    return clsFormulariosobjetoInfo;
                }
            }
            else
            {
                return null;
                //throw new Exception("Erro.");
            }
        }

        public Int32 Incluir(String _conexao, clsFormulariosObjetoInfo _info)
        {
            VerificaInfo(_info);

            clsFormulariosObjetoDAL clsFormulariosobjetoDAL = new clsFormulariosObjetoDAL();
            return clsFormulariosobjetoDAL.Incluir(_conexao, _info);
        }

        public Int32 Alterar(String _conexao, clsFormulariosObjetoInfo _info)
        {
            VerificaInfo(_info);

            clsFormulariosObjetoDAL clsFormulariosobjetoDAL = new clsFormulariosObjetoDAL();
            return clsFormulariosobjetoDAL.Alterar(_conexao, _info);
        }

        public void Excluir(String _conexao, Int32 _id)
        {
            if (_id > 0 && _conexao.Length > 0)
            {
                clsFormulariosObjetoDAL clsFormulariosobjetoDAL = new clsFormulariosObjetoDAL();
                clsFormulariosobjetoDAL.Excluir(_conexao, _id);
            }
            else
            {
                throw new Exception("Erro.");
            }
        }

        public void VerificaInfo(clsFormulariosObjetoInfo _info)
        {
            if (_info.idformulario == 0)
            {
                throw new Exception("id do formulario = 0");
            }

        }

        public Boolean ComparaInfo(clsFormulariosObjetoInfo _info, clsFormulariosObjetoInfo _info2)
        {
            if (_info.id != _info2.id)
            {
                return false;
            }
            if (_info.idformulario != _info2.idformulario)
            {
                return false;
            }
            if (_info.nomeobjeto != _info2.nomeobjeto)
            {
                return false;
            }
            if (_info.nome != _info2.nome)
            {
                return false;
            }
            if (_info.ativo != _info2.ativo)
            {
                return false;
            }
            if (_info.liberado != _info2.liberado)
            {
                return false;
            }
            if (_info.aparecelista != _info2.aparecelista)
            {
                return false;
            }
            return true;
        }
        public DataTable CarregaGrid(String _conexao, Int32 _idFormulario) //Todos   
        {
            clsFormulariosObjetoDAL clsFormulariosObjetoDAL = new clsFormulariosObjetoDAL();
            return clsFormulariosObjetoDAL.CarregaGrid(_conexao, _idFormulario);
        }
    }
}
