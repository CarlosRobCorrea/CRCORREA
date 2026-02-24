using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;
using CRCorreaFuncoes;
using CRCorreaInfo;

namespace CRCorreaBLL
{
    public class clsPedido2BLL : SQLFactory<clsPedido2Info>
    {
        /*
        // - Functions
        public clsPedido2Info Carregar(String _conexao, Int32 _id)
        {
            if (_id > 0 && _conexao.Length > 0)
            {
                clsPedido2DAL clsPedido2DAL = new clsPedido2DAL();
                clsPedido2Info clsPedido2Info;
                clsPedido2Info = clsPedido2DAL.Carregar(_conexao, _id);
                if (clsPedido2Info != null)
                {
                    return clsPedido2Info;
                }
                else
                {
                    //throw new Exception("Registro não encontrado.");
                    return null;
                }
            }
            else
            {
                //throw new Exception("Erro.");
                return null;

            }
        }

        public Int32 Incluir(String _conexao, clsPedido2Info _info)
        {
            VerificaInfo(_info);

            clsPedido2DAL clsPedido2DAL = new clsPedido2DAL();
            return clsPedido2DAL.Incluir(_conexao, _info);
        }

        public Int32 Alterar(String _conexao, clsPedido2Info _info)
        {
            VerificaInfo(_info);

            clsPedido2DAL clsPedido2DAL = new clsPedido2DAL();
            return clsPedido2DAL.Alterar(_conexao, _info);
        }

        public void Excluir(String _conexao, Int32 _id)
        {
            if (_id > 0 && _conexao.Length > 0)
            {
                clsPedido2DAL clsPedido2DAL = new clsPedido2DAL();
                clsPedido2DAL.Excluir(_conexao, _id);
            }
            else
            {
                throw new Exception("Erro.");
            }
        }

        public void VerificaInfo(clsPedido2Info _info)
        {

        }

        public Boolean ComparaInfo(clsPedido2Info _info, clsPedido2Info _info2)
        {
            if (_info.id != _info2.id)
            {
                return false;
            }
            if (_info.idpedido != _info2.idpedido)
            {
                return false;
            }
            if (_info.idpedido1 != _info2.idpedido1)
            {
                return false;
            }
            if (_info.entrega != _info2.entrega)
            {
                return false;
            }
            if (_info.qtde != _info2.qtde)
            {
                return false;
            }
            if (_info.qtdeentregue != _info2.qtdeentregue)
            {
                return false;
            }
            if (_info.qtdedefeito != _info2.qtdedefeito)
            {
                return false;
            }
            if (_info.qtdebaixada != _info2.qtdebaixada)
            {
                return false;
            }
            if (_info.qtdesucata != _info2.qtdesucata)
            {
                return false;
            }
            if (_info.qtdeosaux != _info2.qtdeosaux)
            {
                return false;
            }
            if (_info.qtdesaldo != _info2.qtdesaldo)
            {
                return false;
            }
            if (_info.divergencia != _info2.divergencia)
            {
                return false;
            }
            if (_info.motivo01 != _info2.motivo01)
            {
                return false;
            }
            if (_info.motivo02 != _info2.motivo02)
            {
                return false;
            }
            return true;
        }
        */
        //public DataTable CarregaGrid(String _conexao, Int32 pedido_id)
        //{
        //    clsPedido2DAL clsPedido2DAL = new clsPedido2DAL();
        //    return clsPedido2DAL.CarregaGrid(_conexao, pedido_id);
        //}
        /*
        public void CalculaProgramacao(String _conexao,
                                        Int32 pedido1_id,
                                        Int32 qtde_de_entregas,
                                        DateTime primeira_entrega,
                                        String tipo,
                                        Decimal qtde_pedido,
                                        Int32 casas_decimais)
        {
            clsPedido2DAL clsPedido2DAL = new clsPedido2DAL();
            clsPedido2DAL.CalculaProgramacao(_conexao,
                                                pedido1_id,
                                                qtde_de_entregas,
                                                primeira_entrega,
                                                tipo,
                                                qtde_pedido,
                                                casas_decimais);
        }

        public void VerificaProgramacao(Int32 pedido1)
        {
            clsPedido2DAL clsPedido2DAL = new clsPedido2DAL();
            clsPedido2DAL.VerificaProgramacao(pedido1);
        }
         */

        public static DataTable GridDados(Int32 idpedido, Int32 idpedido1, String cnn)
        {
            DataTable dt;
            String query = "";
            //String filtro = "";
            SqlDataAdapter sda;
            query = "SELECT PEDIDO2.ID,PEDIDO2.IDPEDIDO,PEDIDO2.IDPEDIDO1,PEDIDO2.IDORDEMSERVICO,PEDIDO2.IDORDEMSERVICOITEM, " +
            "ORDEMSERVICO.NUMERO AS [OS], " +
            "PEDIDO2.ENTREGA,PEDIDO2.QTDEENTREGUE,PEDIDO2.QTDE,PEDIDO2.QTDEDEFEITO,PEDIDO2.QTDESUCATA,PEDIDO2.QTDEBAIXADA, " +
            "PEDIDO2.QTDEOSAUX,PEDIDO2.QTDESALDO,PEDIDO2.DIVERGENCIA,PEDIDO2.MOTIVO01,PEDIDO2.MOTIVO02 " +
            "FROM PEDIDO2 " +
            "LEFT JOIN ORDEMSERVICO ON ORDEMSERVICO.ID=PEDIDO2.IDORDEMSERVICO " +
            "WHERE PEDIDO2.IDPEDIDO = " + idpedido + " " ;
            if (idpedido1 > 0)
            {
                query = query + " and PEDIDO2.IDPEDIDO1 = " + idpedido1 + " ";
            }
            query = query + " ORDER BY PEDIDO2.ENTREGA ";
            dt = new DataTable();
            sda = new SqlDataAdapter(query, cnn);
            sda.Fill(dt);
            return dt;
        }

        private static GridColuna[] dtGridColunas = new GridColuna[]
        {
            new GridColuna("Id", "ID", 0, false, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("O.S.", "OS", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Dt Entrega", "ENTREGA", 60, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qt Ent", "QTDEENTREGUE:n0", 50, false, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qt. Ok", "QTDE:n0", 50, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qt. Df", "QTDEDEFEITO:n0", 50, true, DataGridViewContentAlignment.MiddleLeft),
            new GridColuna("Qt. Mor.", "QTDESUCATA:n0", 50, true, DataGridViewContentAlignment.MiddleCenter),
            new GridColuna("Qt. Bai.", "QTDEBAIXADA:n0", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Qt. Aux.", "QTDEOSAUX:n0", 50, true, DataGridViewContentAlignment.MiddleRight),
            new GridColuna("Qt Saldo", "QTDESALDO:n0", 50, true, DataGridViewContentAlignment.MiddleRight)

        };
        public static void GridMonta(DataGridView dgv, DataTable dt, Int32 id)
        {
            
            dgv.DataSource = dt;
            clsGridHelper.MontaGrid2(dgv, dtGridColunas, true);
            dgv.Font = new Font("Tahoma", 7, FontStyle.Regular);
            clsGridHelper.SelecionaLinha(id, dgv, "ENTREGA");
        }

    }
}
