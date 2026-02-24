using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Transactions;

using System.Data.SqlClient;

using CRCorreaBLL;
using CRCorreaInfo;

using CRCorreaFuncoes;

namespace CRCorrea
{
    public partial class frmTiponotaPes : Form
    {
        DataTable dtTiponota;
        GridColuna[] dtTiponotaColunas;

        clsTiponotaBLL clsTiponotaBLL;

       Int32 id;
        Int32 ixTiponota;

        SqlDataAdapter sda;
        String query;

        Boolean carregar = true;

        clsBasicReport clsBr;

        public frmTiponotaPes()
        {
            InitializeComponent();
        }

        public void Init(Int32 _id)
        {
            id = _id;
            
            
            clsBr = new clsBasicReport(this, dgvTiponota, toolTip1);

            clsTiponotaBLL = new clsTiponotaBLL();

            dtTiponotaColunas = new GridColuna[]
                                {
                                    new GridColuna("Código", "codigo", 120, true, DataGridViewContentAlignment.MiddleLeft),
                                    new GridColuna("Nome", "nome", 240, true, DataGridViewContentAlignment.MiddleLeft),
                                    new GridColuna("Mov.", "movimentacao", 55, true, DataGridViewContentAlignment.MiddleCenter),
                                    new GridColuna("Fat.", "fatura", 55, true, DataGridViewContentAlignment.MiddleCenter),
                                    new GridColuna("Dev.", "devolucao", 55, true, DataGridViewContentAlignment.MiddleCenter),
                                    new GridColuna("CFOP", "cfopok", 70, true, DataGridViewContentAlignment.MiddleCenter),
                                    new GridColuna("ICMS", "csticms", 55, true, DataGridViewContentAlignment.MiddleCenter),
                                    new GridColuna("IPI", "cstipi", 55, true, DataGridViewContentAlignment.MiddleCenter),
                                    new GridColuna("PIS", "cstpis", 55, true, DataGridViewContentAlignment.MiddleCenter),
                                    new GridColuna("COFINS", "cstcofins", 55, true, DataGridViewContentAlignment.MiddleCenter)
                                };
        }

        private void frmTiponotaPes_Load(object sender, EventArgs e)
        {
            
        }

        void PedidoCarregar()
        {
            if (carregar == true)
            {
                carregar = false;

                dtTiponota = new DataTable();

                query = "select " +
                            "tiponota.id, " +
                            "tiponota.codigo, " +
                            "tiponota.nome, " +
                            "tiponota.movimentacao, " +
                            "tiponota.fatura, " +
                            "tiponota.devolucao, " +
                            "cfop.cfop as cfopok, " +
                            "sittributariab.codigo as csticms, " +
                            "sittribipi.codigo as cstipi, " +
                            "sittribpis.codigo as cstpis, " +
                            "sittribcofins.codigo as cstcofins " +
                        "from " +
                            "tiponota " +
                                " inner join cfop on cfop.id = tiponota.idcfopok " +
                                " inner join sittributariab on sittributariab.id = idcsticms " +
                                " inner join sittribipi on sittribipi.id = idcstipi " +
                                " inner join sittribpis on sittribpis.id = idcstpis " +
                                " inner join sittribcofins on sittribcofins.id = idcstcofins " +
                        "order by tiponota.codigo";

                sda = new SqlDataAdapter(query, clsInfo.conexaosqldados);
                sda.Fill(dtTiponota);

                dgvTiponota.DataSource = dtTiponota;

                clsGridHelper.MontaGrid2(dgvTiponota, dtTiponotaColunas, true);

                if (ixTiponota > 0)
                {
                    for (int x = 0; x < dgvTiponota.Rows.Count; x++)
                    {
                        if (dgvTiponota.Rows[x].Cells["id"].Value.ToString() == ixTiponota.ToString())
                        {
                            dgvTiponota.Rows[x].Selected = true;
                            dgvTiponota.Rows[x].Cells["codigo"].Selected = true;
                        }
                    }
                }
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTiponotaPes_Activated(object sender, EventArgs e)
        {
            PedidoCarregar();
        }

        private void tspEscolher_Click(object sender, EventArgs e)
        {
            try 
            {
                clsInfo.zrow = dgvTiponota.CurrentRow;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Close();
                this.Dispose();
            }
        }


        private void tspImprimir_Click(object sender, EventArgs e)
        {
            clsBr.Imprimir(clsInfo.caminhorelatorios, "Tipos de Itens - NF", clsInfo.conexaosqldados);
        }

        private void dgvTiponota_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTiponota.CurrentRow != null)
            {
                ixTiponota = clsParser.Int32Parse(dgvTiponota.CurrentRow.Cells["id"].Value.ToString());
            }
            else
            {
                ixTiponota = 0;
            }
        }

        private void dgvTiponota_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tspEscolher.PerformClick();
        }

        private void tstbxLocalizar_KeyUp(object sender, KeyEventArgs e)
        {
            clsGridHelper.Filtrar(tstbxLocalizar.Text, dgvTiponota);
        }

        private void tspAlterar_Click(object sender, EventArgs e)
        {

        }

    }
}
