using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace CRCorreaFuncoes
{
    public class clsGridHelper
    {
        /// <summary>
        /// Método que ajusta o controle DataGridView de acordo com as Normas 
        /// padrões da Aplisoft.
        /// </summary>
        /// <param name="dgv">DataGridView a se adaptar as normas.</param>
        /// <param name="colunasnome">Coleção de nome visíveis das colunas.</param>
        /// <param name="colunasdados">Coleção de nome sql das colunas.</param>
        /// <param name="colunaslargura">Coleção de largura das colunas.</param>
        /// <param name="colunasalinhamento">Coleção de alinhamento das colunas.</param>
        /// <param name="colunasvisible">Coleção de visibilidade das colunas(true || false).</param>
        /// <param name="corlinha">Se terá ou não variação de cor entre uma linha e outra.</param>
        /// <param name="colunaprincipal">O index(número a partir de zero) da coluna que deverá ser considerada como Principal.</param>
        /// <param name="ordem">Se a ordem da coluna principal deverá ser Ascendente ou Descendente.</param>
        public static void MontaGrid(DataGridView dgv,
                                String[] colunasnome,
                                String[] colunasdados,
                                Int32[] colunaslargura,
                                DataGridViewContentAlignment[] colunasalinhamento,
                                Boolean[] colunasvisible,
                                Boolean corlinha,
                                Int32 colunaprincipal,
                                System.ComponentModel.ListSortDirection ordem)
        {
            byte X = 0;

            if (SystemInformation.HighContrast)
            {
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.Black; //Color.DodgerBlue;
                dgv.DefaultCellStyle.BackColor = Color.Black;
                dgv.DefaultCellStyle.ForeColor = Color.Yellow;
                dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Yellow;

            }
            else
            {
                if (corlinha == true)
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            }

            dgv.AllowDrop = true;
            dgv.RowHeadersVisible = false;
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 25;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            foreach (String coluna in colunasdados)
            {
                if (dgv.Columns.Contains(coluna) == false)
                {
                    dgv.Columns[coluna].Visible = false;
                }
                else
                {
                    dgv.Columns[coluna].Width = Int32.Parse(colunaslargura.GetValue(X).ToString());
                    dgv.Columns[coluna].Visible = bool.Parse(colunasvisible.GetValue(X).ToString());
                    dgv.Columns[coluna].HeaderText = colunasnome.GetValue(X).ToString();
                    dgv.Columns[coluna].DefaultCellStyle.Alignment = colunasalinhamento[X];
                }
                X++;
            }

            dgv.Columns[colunaprincipal].SortMode = DataGridViewColumnSortMode.Automatic;
            dgv.Sort(dgv.Columns[colunaprincipal], ordem);
            try
            {
                dgv[colunaprincipal, 0].Selected = true;
            }
            catch
            {
                // Não informa o erro pois se ocorre o erro, quer dizer que a tabela não contém dados
            }
        }

        public static void MontaGrid(DataGridView dgv,
                                String[] colunasnome,
                                String[] colunasdados,
                                Int32[] colunaslargura,
                                DataGridViewContentAlignment[] colunasalinhamento,
                                Boolean[] colunasvisible,
                                Boolean corlinha)
        {
            byte X = 0;

            if (SystemInformation.HighContrast)
            {
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.Black; //Color.DodgerBlue;
                dgv.DefaultCellStyle.BackColor = Color.Black;
                dgv.DefaultCellStyle.ForeColor = Color.Yellow;
                dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Yellow;

            }
            else
            {
                if (corlinha == true)
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            }

            dgv.AllowDrop = true;
            dgv.RowHeadersVisible = false;
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 25;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            foreach (String coluna in colunasdados)
            {
                if (dgv.Columns.Contains(coluna) == false)
                {
                    dgv.Columns[coluna].Visible = false;
                }
                else
                {
                    dgv.Columns[coluna].Width = Int32.Parse(colunaslargura.GetValue(X).ToString());
                    dgv.Columns[coluna].Visible = bool.Parse(colunasvisible.GetValue(X).ToString());
                    dgv.Columns[coluna].HeaderText = colunasnome.GetValue(X).ToString();
                    dgv.Columns[coluna].DefaultCellStyle.Alignment = colunasalinhamento[X];
                }
                X++;
            }
        }



        public static void MontaGrid(DataGridView dgv, GridColuna[] cols, Boolean corlinha)
        {
            // Verifica a qtde de colunas
            if (dgv.Columns.Count == 0)
                throw new Exception("É necessário que pelo menos se tenha uma coluna.");

            if ((dgv.Columns.Count == cols.Length) == false)
                throw new Exception("Número de colunas inválido.");

            // Verifica se o nome de cada campo está correto
            String cols2 = "";
            try
            {
                foreach (GridColuna col in cols)
                {
                    cols2 = col.Name;
                    if (dgv.Columns[col.Name.Split(':').GetValue(0).ToString()].Name.ToUpper() != col.Name.Split(':').GetValue(0).ToString().ToUpper())
                        throw new Exception("Nunca essa linha será executada, porém será gerado um erro no código, caso a coluna esteja com o nome errado ou não exista.");
                }
            }
            catch
            {
                throw new Exception("'" + cols2 + "' - Coluna inválida.");
            }

            // Verifica qual coluna deve estar com Visible = true ou false
            for (Byte col_position = 0; col_position < dgv.Columns.Count; col_position++)
            {
                dgv.Columns[col_position].Visible = cols[col_position].Visible;
                if (cols[col_position].Name.IndexOf(':') != -1)
                    dgv.Columns[col_position].DefaultCellStyle.Format = cols[col_position].Name.Split(':').GetValue(1).ToString();

                dgv.Columns[col_position].DefaultCellStyle.Alignment = cols[col_position].Align;
                dgv.Columns[col_position].HeaderText = cols[col_position].Header;
                dgv.Columns[col_position].Width = cols[col_position].Width;
            }

            // Ativa ou não a mudança de cor de uma linha para outra
            if (SystemInformation.HighContrast)
            {
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.Black; //Color.DodgerBlue;
                dgv.DefaultCellStyle.BackColor = Color.Black;
                dgv.DefaultCellStyle.ForeColor = Color.Yellow;
                dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Yellow;

            }
            else
            {
                if (corlinha == true)
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            }


            // Desativa Novo, Editar, Excluir, Redimensionar linha e coluna
            dgv.AllowDrop = true;
            dgv.AllowUserToOrderColumns = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
        }

        public static void MontaGrid2(DataGridView dgv, GridColuna[] cols, Boolean corlinha)
        {
            // Verifica se o nome de cada campo está correto
            String cols2 = "";
            try
            {
                foreach (GridColuna col in cols)
                {
                    cols2 = col.Name;
                    if (dgv.Columns[col.Name.Split(':').GetValue(0).ToString()].Name.ToUpper() != col.Name.Split(':').GetValue(0).ToString().ToUpper())
                        throw new Exception("Nunca essa linha será executada, porém será gerado um erro no código, caso a coluna esteja com o nome errado ou não exista.");
                }
            }
            catch
            {
                throw new Exception("'" + cols2 + "' - Coluna inválida.");
            }

            ArrayList colunas_existentes = new ArrayList();
            Int32 ordem_colunas = 0;

            foreach (GridColuna col in cols)
            {
                try
                {
                    cols2 = col.Name.Split(':').GetValue(0).ToString();
                    dgv.Columns[cols2].Visible = col.Visible;
                    if (col.Name.IndexOf(':') != -1)
                        dgv.Columns[cols2].DefaultCellStyle.Format = col.Name.Split(':').GetValue(1).ToString();

                    dgv.Columns[cols2].DefaultCellStyle.Alignment = col.Align;
                    dgv.Columns[cols2].HeaderText = col.Header;
                    dgv.Columns[cols2].Width = col.Width;
                    dgv.Columns[cols2].DisplayIndex = ordem_colunas;

                    colunas_existentes.Add(dgv.Columns[cols2].Index.ToString());

                    ordem_colunas++;
                }
                catch
                {
                    continue;
                }
            }

            // Verifica qual coluna deve estar com Visible = true ou false
            for (int col_position = 0; col_position < dgv.Columns.Count; col_position++)
            {
                if (colunas_existentes.Contains(col_position.ToString()) == false)
                {
                    dgv.Columns[col_position].Visible = false;
                }
            }

            // Ativa ou não a mudança de cor de uma linha para outra
            if (SystemInformation.HighContrast)
            {
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.Black; //Color.DodgerBlue;
                dgv.DefaultCellStyle.BackColor = Color.Black;
                dgv.DefaultCellStyle.ForeColor = Color.Yellow;
                dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Yellow;

            }
            else
            {
                if (corlinha == true)
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            }


            // Desativa Novo, Editar, Excluir, Redimensionar linha e coluna
            dgv.AllowDrop = true;
            dgv.AllowUserToOrderColumns = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
        }


        public static void ColunaPosicao(DataTable dt)
        {
            DataColumn dc = new DataColumn("posicao", Type.GetType("System.Int32"));
            dt.Columns.Add(dc);

            for (Int32 x = 0; x < dt.Rows.Count; x++)
            {
                dt.Rows[x]["posicao"] = x + 1;
            }
        }

        public static Int32 NextIndex(Int32 _index, DataGridView _dgvw)
        {
            try
            {
                _index = Int32.Parse(_dgvw.Rows[_index + 1].Cells[0].Value.ToString());
            }
            catch
            {
                try
                {
                    _index = Int32.Parse(_dgvw.Rows[_index - 1].Cells[0].Value.ToString());
                }
                catch
                {
                    _index = -1;
                }
            }
            return _index;
        }

        public static Boolean Filtrar(String pesquisa, DataGridView dgv)
        {
            if (pesquisa == null || dgv.DataSource == null)
            {
                return false;
            }

            DataTable dtTable = (DataTable)dgv.DataSource;

            if (pesquisa == "")
            {
                if (dtTable.DefaultView.RowFilter == "")
                {
                    return false;
                }
                else
                {
                    dtTable.DefaultView.RowFilter = "";
                    return true;
                }
            }
            else
            {
                String resultado = "";

                foreach (DataGridViewColumn coluna in dgv.Columns)
                {
                    if (coluna.Visible == true)
                    {
                        if (dtTable.Columns.Contains(coluna.Name) == true)
                        {
                            if (resultado.Length > 0)
                            {
                                resultado += " or ";
                            }

                            resultado += String.Format(" convert({0}, System.String) like '*{1}*' ", coluna.Name, pesquisa);
                        }
                    }
                }

                dtTable.DefaultView.RowFilter = resultado;
                return true;
            }
        }

        public static Boolean Filtrar(string pesquisa, string colunaPesquisa, DataGridView dgv)
        {
            if (string.IsNullOrEmpty(pesquisa) || string.IsNullOrEmpty(colunaPesquisa) || dgv.DataSource == null)
            {
                return false;
            }

            DataTable dtTable = (DataTable)dgv.DataSource;

            string resultado = string.Empty;

            if (dgv.Columns.Contains(colunaPesquisa))
            {
                var coluna = dgv.Columns[colunaPesquisa];

                if (coluna.Visible == true &&
                    dtTable.Columns.Contains(coluna.Name))
                {
                    resultado += String.Format(" convert({0}, System.String) like '*{1}*' ", coluna.Name, pesquisa);
                }
            }

            dtTable.DefaultView.RowFilter = resultado;

            return true;
        }

        public static DataGridView PesquisaRapida(DataGridView datagrid, DataTable datatable, String texpesquisa)
        {
            ArrayList comandoinicial = new ArrayList();
            String comandofinal = "";
            BindingSource bdsDataGrid = new BindingSource();
            bdsDataGrid.DataSource = datatable;
            try
            {
                if (texpesquisa.Trim() != "")
                {
                    foreach (DataGridViewColumn dgvrDataGrid in datagrid.Columns)
                    {
                        if (dgvrDataGrid.Visible == true)
                        {
                            if (dgvrDataGrid.ValueType.Name == "Int64" || dgvrDataGrid.ValueType.Name == "Int32")
                            {
                                if (clsParser.Int64Parse(texpesquisa) > 0)
                                {
                                    comandoinicial.Add(dgvrDataGrid.Name.ToString() + " = " + texpesquisa);
                                }
                            }
                            if (dgvrDataGrid.ValueType.Name == "Decimal")
                            {
                                //comandoinicial.Add(dgvrDataGrid.Name.ToString() + " = " + texpesquisa);
                                comandoinicial.Add(dgvrDataGrid.Name.ToString() + " = '" + texpesquisa + "'");
                            }

                            if (dgvrDataGrid.ValueType.Name == "DateTime")
                            {
                                if (clsParser.SqlDateTimeParse(texpesquisa).IsNull == false)
                                {
                                    comandoinicial.Add("(" + dgvrDataGrid.Name.ToString() + " >= '" + clsParser.SqlDateTimeParse(texpesquisa).Value.ToString("dd/MM/yyyy") + " 00:00'" +
                                    " AND " + dgvrDataGrid.Name.ToString() + " <= '" + clsParser.SqlDateTimeParse(texpesquisa).Value.ToString("dd/MM/yyyy") + " 23:59'" + ")");
                                }
                            }
                            //
                            if (dgvrDataGrid.ValueType.Name == "String")
                            {
                                if (clsParser.Int64Parse(texpesquisa) > 0)
                                {
                                    comandoinicial.Add(dgvrDataGrid.Name.ToString() + " = '" + texpesquisa + "'");
                                }
                                else
                                {
                                    comandoinicial.Add(dgvrDataGrid.Name.ToString() + " LIKE '%" + texpesquisa + "%'");
                                }
                            }
                        }
                    }
                    //
                    for (Int32 X = comandoinicial.Count - 1; X >= 0; X--)
                    {
                        if (X == 0)
                        {
                            comandofinal = comandofinal + comandoinicial[X].ToString();
                        }
                        else
                        {
                            comandofinal = comandofinal + comandoinicial[X].ToString() + " OR ";
                        }
                    }
                    //             
                    comandofinal = comandofinal.Replace(",", ".");
                    bdsDataGrid.Filter = comandofinal;
                    datagrid.DataSource = bdsDataGrid;
                }
                else
                {
                    bdsDataGrid.RemoveFilter();
                    datagrid.DataSource = bdsDataGrid;
                }
                return datagrid;
            }
            catch
            {
                bdsDataGrid.RemoveFilter();
                datagrid.DataSource = bdsDataGrid;
                return datagrid;
            }
        }

        public static void SelecionaLinha(Int32 _id, DataGridView _dgvw, Int32 _coluna)
        {
            if (_id > 0)
            {
                foreach (DataGridViewRow dgvr in _dgvw.Rows)
                {
                    if (dgvr.Cells["id"].Value != null && clsParser.Int32Parse(dgvr.Cells["id"].Value.ToString()) == _id)
                    {
                        _dgvw[_coluna, dgvr.Index].Selected = true;
                        break;
                    }
                }
            }
        }

        public static Boolean SelecionaLinha_ReturnBoolean(Int32 _id, DataGridView _dgvw, String _coluna)
        {
            Boolean ok = false;
            if (_id > 0)
            {
                foreach (DataGridViewRow dgvr in _dgvw.Rows)
                {
                    if ((Int32)dgvr.Cells[0].Value == _id)
                    {
                        _dgvw[_coluna, dgvr.Index].Selected = true;
                        ok = true;
                        break;
                    }
                }
            }

            return ok;
        }

        public static Boolean SelecionaLinha_ReturnBoolean(String _codigo, DataGridView _dgvw, String _coluna)
        {
            Boolean ok = false;
            if (_codigo.ToString().Length > 0 && _dgvw.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in _dgvw.Rows)
                {
                    if (dgvr.Cells["codigo"].Value.ToString() == _codigo)
                    {
                        _dgvw[_coluna, dgvr.Index].Selected = true;
                        ok = true;
                        break;
                    }
                }
            }
            return ok;
        }

        public static void SelecionaLinha(Int32 _id, DataGridView _dgvw, String _coluna)
        {
            if (_id > 0)
            {
                foreach (DataGridViewRow dgvr in _dgvw.Rows)
                {
                    if ((Int32)dgvr.Cells[0].Value == _id)
                    {
                        _dgvw[_coluna, dgvr.Index].Selected = true;
                        break;
                    }
                }
            }
        }

        public static void SelecionaLinha(Int32 _id, DataGridView _dgvw, Int32 _coluna, String _campo)
        {
            if (_id > 0)
            {
                foreach (DataGridViewRow dgvr in _dgvw.Rows)
                {
                    if (dgvr.Cells[_campo].Value != null && clsParser.Int32Parse(dgvr.Cells[_campo].Value.ToString()) == _id)
                    {
                        _dgvw[_coluna, dgvr.Index].Selected = true;
                        break;
                    }
                }
            }
        }

        public static Int32 ProximaLinha(Int32 _index, DataGridView _dgvw)
        {
            if (_dgvw.RowCount > 1)
            {
                if (_dgvw.CurrentRow.Index == 0)
                {
                    _index = (Int32)_dgvw.Rows[_dgvw.CurrentRow.Index + 1].Cells[0].Value;
                }
                else
                {
                    _index = (Int32)_dgvw.Rows[_dgvw.CurrentRow.Index - 1].Cells[0].Value;
                }
            }
            else
            {
                _index = 0;
            }
            return _index;
        }

        public static void FontGrid(DataGridView _grid, float _tamanho)
        {
            _grid.Font = new System.Drawing.Font("Tahoma", _tamanho, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            _grid.ForeColor = System.Drawing.Color.Black;
        }

        public static DataRow Autoincrement(DataGridView dataGridView, DataRow dataRow)
        {
            ((DataTable)dataGridView.DataSource).DefaultView.Sort = ((DataTable)dataGridView.DataSource).Columns[0].ToString() + " ASC";
            ((DataTable)dataGridView.DataSource).DefaultView.ToTable();
            DataColumn[] pk = new DataColumn[1];
            pk[0] = ((DataTable)dataGridView.DataSource).Columns[0];
            ((DataTable)dataGridView.DataSource).PrimaryKey = pk;
            ((DataTable)dataGridView.DataSource).Columns[0].AutoIncrement = true;
            ((DataTable)dataGridView.DataSource).Columns[0].AutoIncrementSeed = 987654321;
            ((DataTable)dataGridView.DataSource).Columns[0].ReadOnly = true;
            //            
            dataRow = ((DataTable)dataGridView.DataSource).NewRow();
            //
            foreach (DataRow dgvr in ((DataTable)dataGridView.DataSource).Rows)
            {
                if (dgvr.RowState != DataRowState.Deleted)
                {
                    if (clsParser.Int32Parse(dgvr.ItemArray[0].ToString()) == clsParser.Int32Parse(dataRow.ItemArray[0].ToString()))
                    {
                        dataRow = ((DataTable)dataGridView.DataSource).NewRow();
                    }
                }
            }

            return dataRow;
        }

        public static DataTable SelectDistinct(DataTable SourceTable, params String[] FieldNames)
        {
            object[] lastValues;
            DataTable newTable;
            DataRow[] orderedRows;

            if (FieldNames == null || FieldNames.Length == 0)
            {
                throw new Exception("Informe a Coluna");
            }
            lastValues = new object[FieldNames.Length];
            newTable = new DataTable();
            foreach (String fieldName in FieldNames)
            {
                newTable.Columns.Add(fieldName, SourceTable.Columns[fieldName].DataType);
            }
            orderedRows = SourceTable.Select("", String.Join(", ", FieldNames));
            foreach (DataRow row in orderedRows)
            {
                if (!fieldValuesAreEqual(lastValues, row, FieldNames))
                {
                    newTable.Rows.Add(createRowClone(row, newTable.NewRow(), FieldNames));

                    setLastValues(lastValues, row, FieldNames);
                }
            }

            return newTable;
        }

        private static bool fieldValuesAreEqual(object[] lastValues, DataRow currentRow, String[] fieldNames)
        {
            bool areEqual = true;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (lastValues[i] == null || !lastValues[i].Equals(currentRow[fieldNames[i]]))
                {
                    areEqual = false;
                    break;
                }
            }

            return areEqual;
        }

        private static DataRow createRowClone(DataRow sourceRow, DataRow newRow, String[] fieldNames)
        {
            foreach (string field in fieldNames)
            {
                newRow[field] = sourceRow[field];
            }
            return newRow;
        }

        private static void setLastValues(object[] lastValues, DataRow sourceRow, String[] fieldNames)
        {
            for (int i = 0; i < fieldNames.Length; i++)
            {
                lastValues[i] = sourceRow[fieldNames[i]];
            }
        }

        public static Decimal SomaGrid(DataGridView _dgv, String coluna)
        {
            Decimal resultado = 0;

            foreach (DataGridViewRow dgvr in _dgv.Rows)
            {
                resultado = Decimal.Add(resultado, clsParser.DecimalParse(dgvr.Cells[coluna].Value.ToString()));
            }

            return resultado;
        }

        public static DataTable CarregaLista(String query, String conexao)
        {
            DataTable dtTabela = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(query, conexao);

            sda.Fill(dtTabela);

            return dtTabela;
        }

    }
    public class GridColuna
    {
        public String Name;
        public String Header;
        public Int32 Width;
        public Boolean Visible;
        public DataGridViewContentAlignment Align;

        public GridColuna(String _header, String _name, Int32 _width, Boolean _visible, DataGridViewContentAlignment _align)
        {
            Header = _header;
            Name = _name;
            Width = _width;
            Visible = _visible;
            Align = _align;
        }
    }

}
