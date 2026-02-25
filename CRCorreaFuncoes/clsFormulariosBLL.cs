using System;
using System.Data;

namespace CRCorreaFuncoes
{
    public class clsFormulariosBLL
    {
        // - Functions
        public clsFormulariosInfo Carregar(String _conexao, Int32 _id)
        {
            if (_id > 0 && _conexao.Length > 0)
            {
                clsFormulariosDAL clsFormulariosDAL = new clsFormulariosDAL();
                clsFormulariosInfo clsFormulariosInfo;
                clsFormulariosInfo = clsFormulariosDAL.Carregar(_conexao, _id);
                if (clsFormulariosInfo != null)
                {
                    return clsFormulariosInfo;
                }
                else
                {
                    throw new Exception("Registro não encontrado.");
                }
            }
            else
            {
                throw new Exception("Erro.");
            }
        }
        public clsFormulariosInfo CarregarForm(String _conexao, Int32 _nroform, Int32 _modulo)
        {
            if (_nroform > 0 && _modulo > 0 && _conexao.Length > 0)
            {
                clsFormulariosDAL clsFormulariosDAL = new clsFormulariosDAL();
                clsFormulariosInfo clsFormulariosInfo;
                clsFormulariosInfo = clsFormulariosDAL.CarregarForm(_conexao, _nroform, _modulo);
                if (clsFormulariosInfo != null)
                {
                    return clsFormulariosInfo;
                }
                else
                {
                    return clsFormulariosInfo; // throw new Exception("Registro não encontrado.");
                }
            }
            else
            {
                throw new Exception("Erro.");
            }
        }

        public Int32 Incluir(String _conexao, clsFormulariosInfo _info)
        {
            VerificaInfo(_info);

            clsFormulariosDAL clsFormulariosDAL = new clsFormulariosDAL();
            return clsFormulariosDAL.Incluir(_conexao, _info);
        }

        public Int32 Alterar(String _conexao, clsFormulariosInfo _info)
        {
            VerificaInfo(_info);

            clsFormulariosDAL clsFormulariosDAL = new clsFormulariosDAL();
            return clsFormulariosDAL.Alterar(_conexao, _info);
        }

        public void Excluir(String _conexao, Int32 _id)
        {
            if (_id > 0 && _conexao.Length > 0)
            {
                clsFormulariosDAL clsFormulariosDAL = new clsFormulariosDAL();
                clsFormulariosDAL.Excluir(_conexao, _id);
            }
            else
            {
                throw new Exception("Erro.");
            }
        }

        public void VerificaInfo(clsFormulariosInfo _info)
        {

        }

        public Boolean ComparaInfo(clsFormulariosInfo _info, clsFormulariosInfo _info2)
        {
            if (_info.id != _info2.id)
            {
                return false;
            }
            if (_info.numero != _info2.numero)
            {
                return false;
            }
            if (_info.modulo != _info2.modulo)
            {
                return false;
            }
            if (_info.nome != _info2.nome)
            {
                return false;
            }
            if (_info.nomeform != _info2.nomeform)
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
            if (_info.verificarform != _info2.verificarform)
            {
                return false;
            }
            return true;
        }

        public DataTable CarregaGrid(String _conexao)
        {
            clsFormulariosDAL clsFormulariosDAL = new clsFormulariosDAL();
            return clsFormulariosDAL.CarregaGrid(_conexao);
        }

        public DataTable CarregaGridMod(String _conexao)
        {
            clsFormulariosDAL clsFormulariosDAL = new clsFormulariosDAL();
            return clsFormulariosDAL.CarregaGridMod(_conexao);
        }


    }
}
