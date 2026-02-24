using CRCorreaFuncoes;
using CrystalDecisions.Shared;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRCorrea
{
    public partial class frmRelCliente : Form
    {
        
        

        delegate void AddnewDelegate(TextBox textbox, TextBox colecao);

        ParameterFields pfields = new ParameterFields();

        String sql;
        String query;
        String ordem;
        String cabecalho;

        public frmRelCliente()
        {
            InitializeComponent();
        }

        public void Init()
        {
            
            
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxResFornecedorDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente order by cognome", tbxResFornecedorAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cep from cliente order by cep", tbxResCepDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cep from cliente order by cep", tbxResCepAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select distinct cidade from cliente order by cidade", tbxResCidadeDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select distinct cidade from cliente order by cidade", tbxResCidadeAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select estado from estados order by estado", tbxResEstadoDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select estado from estados order by estado", tbxResEstadoAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from zonas order by codigo", tbxResZonaDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from zonas order by codigo", tbxResZonaAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select codigo from ramo order by codigo", tbxResRamoDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select nome from ramo order by nome", tbxResRamoAte);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo = 'V' order by cognome", tbxResVendedorDe);
            clsVisual.AutoCompletar(clsInfo.conexaosqldados, "select cognome from cliente where tipo ='V' order by cognome", tbxResVendedorAte);
        }

        private void frmRelCliente_Load(object sender, EventArgs e)
        {
            lbxResOrdem.SelectedIndex = 0;
            rbnResSimples01.Select();
        }

        private void ControlEnter(object sender, EventArgs e)
        {
            clsVisual.ControlEnter(sender);
        }

        private void ControlLeave(object sender, EventArgs e)
        {
            clsVisual.ControlLeave(sender);
            clsVisual.FormatarCampoNumerico(sender);
            TrataCampos((Control)sender);
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDown(sender, e);
        }

        private void ControlKeyDownData(object sender, KeyEventArgs e)
        {
            clsVisual.ControlKeyDownData((TextBox)sender, e);
        }

        private void ControlKeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisual.ControlKeyPressNumero((TextBox)sender, e, true);
        }

        private void bntResFornecedorDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = bntResFornecedorDe.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnResFornecedorAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnResFornecedorAte.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("Todos", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void btnResCidadeDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnResCidadeDe.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "cliente", "distinct cidade", "",
                "cidade", new GridColuna[]{                                        
                                        new GridColuna("Cidade", "Cidade", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);
        }

        private void btnResCidadeAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnResCidadeAte.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "cliente", "cidade", "",
                "cidade", new GridColuna[]{                                        
                                        new GridColuna("Cidade", "Cidade", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);
        }

        private void btnResUfDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnResUfDe.Name;
            frmEstadosPes frmEstadosPes = new frmEstadosPes();
            frmEstadosPes.Init(0);

            clsFormHelper.AbrirForm(this.MdiParent, frmEstadosPes, clsInfo.conexaosqldados);
        }

        private void btnResUfAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnResUfAte.Name;
            frmEstadosPes frmEstadosPes = new frmEstadosPes();
            frmEstadosPes.Init(0);

            clsFormHelper.AbrirForm(this.MdiParent, frmEstadosPes, clsInfo.conexaosqldados);
        }
       
        private void btnResZonaDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnResZonaDe.Name;
            frmZonasPes frmZonasPes = new frmZonasPes();
            frmZonasPes.Init("");
//Todos
            clsFormHelper.AbrirForm(this.MdiParent, frmZonasPes, clsInfo.conexaosqldados);
        }

        private void btnResZonaAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnResZonaAte.Name;
            frmZonasPes frmZonasPes = new frmZonasPes();
            frmZonasPes.Init("");
//Todos
            clsFormHelper.AbrirForm(this.MdiParent, frmZonasPes, clsInfo.conexaosqldados);
        }

        private void btnResRamoDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnResRamoDe.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "ramo", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código ", "codigo", 150, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);
        }

        private void btnResRamoAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnResRamoAte.Name;
            frmGeralVis frmGeralVis = new frmGeralVis();
            frmGeralVis.Init(clsInfo.conexaosqldados, "ramo", "codigo, nome", "",
                "codigo", new GridColuna[]{                                        
                                        new GridColuna("Código", "codigo", 240, true, DataGridViewContentAlignment.MiddleLeft),
                                        new GridColuna("Nome ", "Nome", 240, true, DataGridViewContentAlignment.MiddleLeft)},
                                        true,
                                        8,
                                        null);

                                
            clsFormHelper.AbrirForm(this.MdiParent, frmGeralVis, clsInfo.conexaosqldados);
        }
        private void btnResRepresentanteDe_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnResRepresentanteDe.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("V",0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }
         
        private void btnResRepresentanteAte_Click(object sender, EventArgs e)
        {
            clsInfo.znomegrid = btnResRepresentanteAte.Name;
            frmClientePes frmClientePes = new frmClientePes();
            frmClientePes.Init("V", 0);

            clsFormHelper.AbrirForm(this.MdiParent, frmClientePes, clsInfo.conexaosqldados);
        }

        private void tspImprimir_Click(object sender, EventArgs e)
        {
            //
            sql = "";
            cabecalho = "";
            query = "";
            ordem = "";

            // Cabeçalho  -- objetivo da lista
            if (rbnResSimples01.Checked == true)
            {
                cabecalho = "Relatorio Parceiros - Simples";
            }
            else if (rbnItemContato.Checked == true)
            {
                cabecalho = "Relatorio de Contatos de Parceiros";
            }

            // Filtros
            // Cliente
            if (tbxResFornecedorDe.Text.Length > 0 && tbxResFornecedorAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                query = "CLIENTE.COGNOME >= '" + tbxResFornecedorDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Cognome: a Partir do " + tbxResFornecedorDe.Text;
            }
            if (tbxResFornecedorDe.Text.Length == 0 && tbxResFornecedorAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                query = "CLIENTE.COGNOME <= '" + tbxResFornecedorAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Cognome: abaixo do " + tbxResFornecedorAte.Text;
            }
            if (tbxResFornecedorDe.Text.Length > 0 && tbxResFornecedorAte.Text.Length > 0)
            {
                query = "CLIENTE.COGNOME >= '" + tbxResFornecedorDe.Text + "' AND CLIENTE.COGNOME <= '" + tbxResFornecedorAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Cognome: do " + tbxResFornecedorDe.Text + "  até o " + tbxResFornecedorAte.Text;
            }
            if (tbxResFornecedorDe.Text.Length == 0 && tbxResFornecedorAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //CEP             
            if (tbxResCepDe.Text.Length > 0 && tbxResCepAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "CLIENTE.CEP >= '" + tbxResCepDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " CEP: a Partir do " + tbxResCepDe.Text;
            }
            if (tbxResCepDe.Text.Length == 0 && tbxResCepAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "CLIENTE.CEP <= '" + tbxResCepAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "CEP: abaixo do " + tbxResCepAte.Text;
            }
            if (tbxResCepDe.Text.Length > 0 && tbxResCepAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "CLIENTE.CEP >= '" + tbxResCepDe.Text + "' AND CLIENTE.CEP <= '" + tbxResCepAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " CEP: do " + tbxResCepDe.Text + "  até 0 " + tbxResCepAte.Text;
            }
            if (tbxResCepDe.Text.Length == 0 && tbxResCepAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //Cidade

            if (tbxResCidadeDe.Text.Length > 0 && tbxResCidadeAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "CLIENTE.CIDADE >= '" + tbxResCidadeDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Cidade: a Partir da " + tbxResCidadeDe.Text;
            }
            if (tbxResCidadeDe.Text.Length == 0 && tbxResCidadeAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "CLIENTE.CIDADE <= '" + tbxResCidadeAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Cidade: abaixo da " + tbxResCidadeAte.Text;
            }
            if (tbxResCidadeDe.Text.Length > 0 && tbxResCidadeAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "CLIENTE.CIDADE >= '" + tbxResCidadeDe.Text + "' AND CLIENTE.CIDADE <= '" + tbxResCidadeAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Cidade: da " + tbxResCidadeDe.Text + "  até a " + tbxResCidadeAte.Text;
            }
            if (tbxResCidadeDe.Text.Length == 0 && tbxResCidadeAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //Estado

            if (tbxResEstadoDe.Text.Length > 0 && tbxResEstadoAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "ESTADOS.ESTADO >= '" + tbxResEstadoDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Estado: a Partir do " + tbxResEstadoDe.Text;
            }
            if (tbxResEstadoDe.Text.Length == 0 && tbxResEstadoAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "ESTADOS.ESTADO <= '" + tbxResEstadoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Estado: abaixo do " + tbxResEstadoAte.Text;
            }
            if (tbxResEstadoDe.Text.Length > 0 && tbxResEstadoAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "ESTADOS.ESTADO >= '" + tbxResEstadoDe.Text + "' AND ESTADOS.ESTADO <= '" + tbxResEstadoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Estado: do " + tbxResEstadoDe.Text + "  até o " + tbxResEstadoAte.Text;
            }
            if (tbxResEstadoDe.Text.Length == 0 && tbxResEstadoAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //Zona

            if (tbxResZonaDe.Text.Length > 0 && tbxResZonaAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "ZONAS.CODIGO >= '" + tbxResZonaDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Zona: a Partir da " + tbxResZonaDe.Text;
            }
            if (tbxResZonaDe.Text.Length == 0 && tbxResZonaAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "ZONAS.CODIGO <= '" + tbxResZonaAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Zona: abaixo da " + tbxResZonaAte.Text;
            }
            if (tbxResZonaDe.Text.Length > 0 && tbxResZonaAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "ZONAS.CODIGO >= '" + tbxResZonaDe.Text + "' AND ZONAS.CODIGO <= '" + tbxResZonaAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Zona: da " + tbxResZonaDe.Text + "  até o " + tbxResZonaAte.Text;
            }
            if (tbxResZonaDe.Text.Length == 0 && tbxResZonaAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //Ramo

            if (tbxResRamoDe.Text.Length > 0 && tbxResRamoAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "RAMO.CODIGO >= '" + tbxResRamoDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Ramo de Atividade: a Partir do " + tbxResRamoDe.Text;
            }
            if (tbxResRamoDe.Text.Length == 0 && tbxResRamoAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "RAMO.CODIGO <= '" + tbxResRamoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Ramo de Atividade: abaixo do " + tbxResRamoAte.Text;
            }
            if (tbxResRamoDe.Text.Length > 0 && tbxResRamoAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "RAMO.CODIGO >= '" + tbxResRamoDe.Text + "' AND RAMO.CODIGO <= '" + tbxResRamoAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Ramo de Atividade: do " + tbxResRamoDe.Text + "  até o " + tbxResRamoAte.Text;
            }
            if (tbxResRamoDe.Text.Length == 0 && tbxResRamoAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }

            //Representante

            if (tbxResVendedorDe.Text.Length > 0 && tbxResVendedorAte.Text.Length == 0)
            { // DA EMPRESA EM DIANTE ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "REPRESENTANTE.COGNOME >= '" + tbxResVendedorDe.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + " Representante: a Partir do " + tbxResVendedorDe.Text;
            }
            if (tbxResVendedorDe.Text.Length == 0 && tbxResVendedorAte.Text.Length > 0)
            { // DA EMPRESA INFERIOR ALFABETICAMENTE
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "REPRESENTANTE.COGNOME  <= '" + tbxResVendedorAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Representante: abaixo do " + tbxResVendedorAte.Text;
            }
            if (tbxResVendedorDe.Text.Length > 0 && tbxResVendedorAte.Text.Length > 0)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + "REPRESENTANTE.COGNOME  >= '" + tbxResVendedorDe.Text + "' AND REPRESENTANTE.COGNOME  <= '" + tbxResVendedorAte.Text + "'";
                cabecalho = cabecalho + Environment.NewLine + "Representante: do " + tbxResVendedorDe.Text + "  até o " + tbxResVendedorAte.Text;
            }
            if (tbxResVendedorDe.Text.Length == 0 && tbxResVendedorAte.Text.Length == 0)
            {
                // cabecalho = cabecalho + Environment.NewLine + "Clientes " + tipocli2.ToString();
            }
            
            // filtro cliente //////////////////////////////////////////////////////////////////////////////////////////////////////

            if (rbnTodosRes.Checked == true)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " (cliente.tipo = 'C' or cliente.tipo = 'F' or cliente.tipo = 'T')";
                cabecalho = cabecalho + Environment.NewLine + " Tipo Cliente: Todos";
            }

            if (rbnItemMp.Checked == true)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " cliente.tipo = 'M' ";
                cabecalho = cabecalho + Environment.NewLine + " Tipo Cliente: Mp Contaminada";
            }

            if (rbnClienteRes.Checked == true)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " cliente.tipo = 'C' ";
                cabecalho = cabecalho + Environment.NewLine + " Tipo Cliente: Cliente";
            }

            if (rbnFornecedorRes.Checked == true)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " cliente.tipo = 'F' ";
                cabecalho = cabecalho + Environment.NewLine + " Tipo Cliente: Fornecedor";
             }

            if (rbnTransportadorasRes.Checked == true)
            {
                if (query.ToString().Length > 0)
                {
                    query = query + " AND ";
                }
                query = query + " cliente.tipo = 'T' ";
                cabecalho = cabecalho + Environment.NewLine + " Tipo Cliente: Transportadora";
            }

            //Ordenação
            if (lbxResOrdem.SelectedIndex == 0)
            {
                ordem = " cliente.cognome ";
            }
            else if (lbxResOrdem.SelectedIndex == 1)
            {
                ordem = " cliente.cidade, cliente.bairro, cliente.cognome ";
            }
            else if (lbxResOrdem.SelectedIndex == 2)
            {
                ordem = " cliente.cep, cliente.cognome ";
            }
            else if (lbxResOrdem.SelectedIndex == 3)
            {
                ordem = " estados.estado, cliente.cep, cliente.cognome ";
            }
            else if (lbxResOrdem.SelectedIndex == 4)
            {
                ordem = " zonas.nome, cliente.cognome ";
            }
            else if (lbxResOrdem.SelectedIndex == 5)
            {
                ordem = " ramo.nome, cliente.cognome ";
            }


            // Lista

            cabecalho = cabecalho + Environment.NewLine + " Ordenação: " + lbxResOrdem.Text;

            // Parametros 

            DataTable Rel = new DataTable();
            DataTable carregarcampos = clsSelectInsertUpdateBLL.Select(clsInfo.conexaosqldados, "EMPRESAS", "*", "ID = " + clsInfo.zempresaid.ToString(), "ID");
            ParameterField pfieldEmpresa = new ParameterField();
            ParameterDiscreteValue disvalEmpresa = new ParameterDiscreteValue();
            if (carregarcampos.Rows.Count > 0)
            {
                disvalEmpresa.Value = carregarcampos.Rows[0]["NOME"].ToString();
                pfieldEmpresa.Name = "EMPRESA";
                pfieldEmpresa.CurrentValues.Add(disvalEmpresa);
            }
            ParameterField pfieldCabecalho = new ParameterField();
            ParameterDiscreteValue disvalCabecalho = new ParameterDiscreteValue();
            disvalCabecalho.Value = cabecalho.ToString();
            pfieldCabecalho.Name = "CABECALHO";
            pfieldCabecalho.CurrentValues.Add(disvalCabecalho);

            pfields.Add(pfieldEmpresa);
            pfields.Add(pfieldCabecalho);

            //Dados
            if (rbnResSimples01.Checked == true)
            {
                sql = "select cliente.id, " +
                     "CASE  " +
                     "   WHEN cliente.tipo = 'C' THEN 'Cliente' " +
                     "   WHEN cliente.tipo = 'F' THEN 'Fornecedor' " +
                     "   WHEN cliente.tipo = 'V' THEN 'Vendedor' " +
                     "   WHEN cliente.tipo = 'T' THEN 'Transportadora' " +
                     "   WHEN cliente.tipo = 'U' THEN 'Funcionário' " +
                     "   WHEN cliente.tipo = 'E' THEN 'Empresa' " +
                     "   WHEN cliente.tipo = 'M' THEN 'MP Contaminada' " +
                     " 		ELSE 'Cliente' END AS cliente_tipo,  " +
                    "cliente.cognome, " +
                    "cliente.nome, " +
                    "cliente.contato, "+
                    "cliente.ddd, " +
                    "cliente.telefone, " +
                    "cliente.endtipo, " +
                    "cliente.endereco, " +
                    "cliente.tiponumero, " +
                    "cliente.numeroend, " +
                    "cliente.andar, " +
                    "cliente.tipocomple, " +
                    "cliente.comple, " +
                    "cliente.bairro, " +
                    "cliente.cidade, " +
                    "estados.estado, " +
                    "cliente.pais, " +
                    "cliente.cep, " +
                    "cliente.regiao, " +
                    "cliente.email, " +
                    "zonas.codigo as zona_codigo, " +
                    "zonas.nome as zona, " +
                    "ramo.codigo as ramo_codigo, " +
                    "ramo.nome as ramo, " +
                    "representante.cognome as representante_cognome, " +
                    "representante.nome as representante_nome " +
                    "from cliente inner join estados on cliente.idestado = estados.id " +
                    "inner join zonas on cliente.idzona = zonas.id " +
                    "inner join ramo on cliente.idramo = ramo.id " +
                    "left join cliente as representante on representante.id = cliente.idvendedor ";
                   
            }
            else if (rbnItemContato.Checked == true) 
            {
                sql = "select cliente.id, "+  
                     "CASE "+ 
                      "  WHEN cliente.tipo = 'C' THEN 'Cliente' "+ 
                      "  WHEN cliente.tipo = 'F' THEN 'Fornecedor' "+ 
                      "  WHEN cliente.tipo = 'V' THEN 'Vendedor' "+ 
                      "  WHEN cliente.tipo = 'T' THEN 'Transportadora' "+ 
                      "  WHEN cliente.tipo = 'U' THEN 'Funcionário' "+ 
                      "  WHEN cliente.tipo = 'E' THEN 'Empresa' "+ 
                      "  WHEN cliente.tipo = 'M' THEN 'MP Contaminada' "+ 
                      "      ELSE 'Cliente' END AS cliente_tipo, "+  
                    "cliente.cognome, "+     
                    "cliente.nome, "+     
                    "cliente.ddd, "+   
                    "cliente.telefone, "+   
                    "cliente.endtipo, "+    
                    "cliente.endereco, "+    
                    "cliente.tiponumero, "+    
                    "cliente.numeroend, "+    
                    "cliente.andar, "+    
                    "cliente.tipocomple, "+    
                    "cliente.comple, "+    
                    "cliente.bairro, "+    
                    "cliente.cidade, "+    
                    "estados.estado,  "+   
                    "cliente.pais, "+    
                    "cliente.cep,  "+   
                    "cliente.regiao, "+
                    "cliente.email, "+
                    "clienteprospeccaocontato.id, "+
                    "clienteprospeccaocontato.tipocontato as contato_tipo, "+
                    "clienteprospeccaocontato.contato as contato_nome, "+
                    "clienteprospeccaocontato.setor as contato_setor, "+
                    "clienteprospeccaocontato.email as contato_email, "+
                    "clienteprospeccaocontato.ddd as contato_ddd , "+
                    "clienteprospeccaocontato.telefone as contato_tel, "+
                    "clienteprospeccaocontato.ramal as contato_ramal, "+
                    "clienteprospeccaocontato.dddfax as contato_dddfax, "+
                    "clienteprospeccaocontato.ramalfax as contato_ramalfax, "+
                    "clienteprospeccaocontato.dddcelular as contato_dddcelular, "+
                    "clienteprospeccaocontato.celular as contato_celular, "+
                    "clienteprospeccaocontato.observa as contato_observa, "+    
                    "zonas.codigo as zona_codigo, "+    
                    "zonas.nome as zona, "+    
                    "ramo.codigo as ramo_codigo, "+    
                    "ramo.nome as ramo,  "+   
                    "representante.cognome as representante_cognome, "+    
                    "representante.nome as representante_nome "+    
                    "from cliente inner join estados on cliente.idestado = estados.id "+   
                    "inner join zonas on cliente.idzona = zonas.id "+     
                    "inner join ramo on cliente.idramo = ramo.id "+    
                    "left join cliente as representante on representante.id = cliente.idvendedor "+
                    "left join clienteprospeccao on cliente.id = clienteprospeccao.idcliente " +
                    "left join CLIENTEPROSPECCAOCONTATO on clienteprospeccao.id = clienteprospeccaocontato.idcliente";
            }
            if (query.ToString().Length > 0)
            {
                sql = sql + " where " + query + " ";
            }
            sql = sql + " order by " + ordem;

            SqlDataAdapter sda = new SqlDataAdapter(sql, clsInfo.conexaosqldados);
            Rel = new DataTable();
            sda.Fill(Rel);


 //Dados
            if (rbnResSimples01.Checked == true)
            {
                // Chamndo o Relatório
                frmCrystalReport frmCrystalReport = new frmCrystalReport();
                //frmCrystalReport.Init(clsInfo.caminhorelatorios, "DADOS_CLIENTE_SIMPLES.RPT", Rel, pfields, "", clsInfo.conexaosqldados);
                frmCrystalReport.Init(clsInfo.caminhorelatorios, "TESTE_COADADOS001.RPT", Rel, pfields, "", clsInfo.conexaosqldados);

                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }
            else if (rbnItemContato.Checked == true)
            {
                // Chamndo o Relatório
                frmCrystalReport frmCrystalReport = new frmCrystalReport();
                frmCrystalReport.Init(clsInfo.caminhorelatorios,
                     "DADOS_CLIENTE_CONTATO.RPT",
                     Rel, pfields, "", clsInfo.conexaosqldados);

                clsFormHelper.AbrirForm(this.MdiParent, frmCrystalReport, clsInfo.conexaosqldados);
            }
        }

        private void tspRetornar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void TrataCampos(Control ctl)
        {
            if (clsInfo.zrow != null &&
                clsInfo.zrow.Index != -1 &&
                clsInfo.znomegrid != null &&
                clsInfo.znomegrid != "")
            {
                // ###############################
                // Verifica os botões de pesquisa
                // ###############################
                // Cliente
                if (clsInfo.znomegrid == bntResFornecedorDe.Name)
                {
                    tbxResFornecedorDe.Text = clsInfo.zrow.Cells["cognome"].Value.ToString();
                    if (tbxResFornecedorAte.Text.Length <= 0)
                    {
                        tbxResFornecedorAte.Text = tbxResFornecedorDe.Text;
                    }
                    tbxResFornecedorDe.Select();
                    tbxResFornecedorDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnResFornecedorAte.Name)
                {
                    tbxResFornecedorAte.Text = clsInfo.zrow.Cells["cognome"].Value.ToString();
                    tbxResFornecedorAte.Select();
                    tbxResFornecedorAte.SelectAll();
                }
                //Cidade

                if (clsInfo.znomegrid == btnResCidadeDe.Name)
                {
                    tbxResCidadeDe.Text = clsInfo.zrow.Cells["cidade"].Value.ToString();
                    if (tbxResCidadeAte.Text.Length <= 0)
                    {
                        tbxResCidadeAte.Text = tbxResCidadeDe.Text;
                    }
                    tbxResCidadeDe.Select();
                    tbxResCidadeDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnResCidadeAte.Name)
                {
                    tbxResCidadeAte.Text = clsInfo.zrow.Cells["cidade"].Value.ToString();
                    tbxResCidadeAte.Select();
                    tbxResCidadeAte.SelectAll();
                }
                
                //Estado

                if (clsInfo.znomegrid == btnResUfDe.Name)
                {
                    tbxResEstadoDe.Text = clsInfo.zrow.Cells["estado"].Value.ToString();
                    if (tbxResEstadoAte.Text.Length <= 0)
                    {
                        tbxResEstadoAte.Text = tbxResEstadoDe.Text;
                    }
                    tbxResEstadoDe.Select();
                    tbxResEstadoDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnResUfAte.Name)
                {
                    tbxResEstadoAte.Text = clsInfo.zrow.Cells["estado"].Value.ToString();
                    tbxResEstadoAte.Select();
                    tbxResEstadoAte.SelectAll();
                }

                //Zona

                if (clsInfo.znomegrid == btnResZonaDe.Name)
                {
                    tbxResZonaDe.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    if (tbxResZonaAte.Text.Length <= 0)
                    {
                        tbxResZonaAte.Text = tbxResZonaDe.Text;
                    }
                    tbxResZonaDe.Select();
                    tbxResZonaDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnResZonaAte.Name)
                {
                    tbxResZonaAte.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxResZonaAte.Select();
                    tbxResZonaAte.SelectAll();
                }

                //Ramo

                if (clsInfo.znomegrid == btnResRamoDe.Name)
                {
                    tbxResRamoDe.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    if (tbxResRamoAte.Text.Length <= 0)
                    {
                        tbxResRamoAte.Text = tbxResRamoDe.Text;
                    }
                    tbxResRamoDe.Select();
                    tbxResRamoDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnResRamoAte.Name)
                {
                    tbxResRamoAte.Text = clsInfo.zrow.Cells["codigo"].Value.ToString();
                    tbxResRamoAte.Select();
                    tbxResRamoAte.SelectAll();
                }

                //Representante

                if (clsInfo.znomegrid == btnResRepresentanteDe.Name)
                {
                    tbxResVendedorDe.Text = clsInfo.zrow.Cells["cognome"].Value.ToString();
                    if (tbxResVendedorAte.Text.Length <= 0)
                    {
                        tbxResVendedorAte.Text = tbxResVendedorDe.Text;
                    }
                    tbxResVendedorDe.Select();
                    tbxResVendedorDe.SelectAll();
                }

                if (clsInfo.znomegrid == btnResRepresentanteAte.Name)
                {
                    tbxResVendedorAte.Text = clsInfo.zrow.Cells["cognome"].Value.ToString();
                    tbxResVendedorAte.Select();
                    tbxResVendedorAte.SelectAll();
                }
                

            }
            else
            {
                // ###############################
                // Verifica os campos de pesquisa
                // ###############################
                
                //Cliente

                if (ctl.Name == tbxResFornecedorDe.Name)    // 
                {
                    if (tbxResFornecedorDe.Text.Length > 0 && tbxResFornecedorAte.Text.Length <= 0)
                    {
                        tbxResFornecedorAte.Text = tbxResFornecedorDe.Text;
                    }
                }

                //cep

                if (ctl.Name == tbxResCepDe.Name)
                {
                    if (tbxResCepDe.Text.Length > 0 && tbxResCepAte.Text.Length <= 0)
                    {
                        tbxResCepAte.Text = tbxResCepDe.Text;
                    }
                }

                //Cidade

                if (ctl.Name == tbxResCidadeDe.Name)    // 
                {
                    if (tbxResCidadeDe.Text.Length > 0 && tbxResCidadeAte.Text.Length <= 0)
                    {
                        tbxResCidadeAte.Text = tbxResCidadeDe.Text;
                    }
                }

                //Estado

                if (ctl.Name == tbxResEstadoDe.Name)    // 
                {
                    if (tbxResEstadoDe.Text.Length > 0 && tbxResEstadoAte.Text.Length <= 0)
                    {
                        tbxResEstadoAte.Text = tbxResEstadoDe.Text;
                    }
                }

                //Zona

                if (ctl.Name == tbxResZonaDe.Name)    // 
                {
                    if (tbxResZonaDe.Text.Length > 0 && tbxResZonaAte.Text.Length <= 0)
                    {
                        tbxResZonaAte.Text = tbxResZonaDe.Text;
                    }
                }

                //Ramo

                if (ctl.Name == tbxResRamoDe.Name)    // 
                {
                    if (tbxResRamoDe.Text.Length > 0 && tbxResRamoAte.Text.Length <= 0)
                    {
                        tbxResRamoAte.Text = tbxResRamoDe.Text;
                    }
                }

                //Representante

                if (ctl.Name == tbxResVendedorDe.Name)    // 
                {
                    if (tbxResVendedorDe.Text.Length > 0 && tbxResVendedorAte.Text.Length <= 0)
                    {
                        tbxResVendedorAte.Text = tbxResVendedorDe.Text;
                    }
                }
            }


            clsInfo.znomegrid = "";
            clsInfo.zrow = null;
        }

        private void frmRelCliente_Activated(object sender, EventArgs e)
        {
            TrataCampos((Control)sender);
        }

       
    }
}
