# CRCorrea - Sistema ERP Desktop

Sistema de gestao empresarial (ERP) desktop desenvolvido para a **Casa Correa**.

## Tecnologias

- **C# / .NET Framework 4.7.2** - Windows Forms
- **SQL Server** - Banco de dados (CRCDADOS, CRCBANCO)
- **Crystal Reports** - Relatorios
- **NF-e** - Nota Fiscal Eletronica (NFe.Classes / DFe)
- **Boleto.Net** - Geracao de boletos bancarios
- **ZXing / BarcodeLib** - Codigo de barras

## Estrutura do Projeto

| Projeto | Descricao |
|---|---|
| **CRCorrea** | Interface grafica (Windows Forms) - ~160 formularios |
| **CRCorreaBLL** | Camada de regras de negocio (Business Logic Layer) |
| **CRCorreaInfo** | DTOs - objetos de transferencia de dados |
| **CRCorreaFuncoes** | Utilitarios: SQL, financeiro, NF-e, criptografia, helpers |
| **CRCorreaBarCod** | Geracao de codigos de barras |

## Modulos

- **Clientes** - Cadastro completo (endereco, contato, comercial)
- **Pecas/Produtos** - Estoque, custos, classificacao, NCM, precos
- **Compras** - Notas fiscais de compra, importacao XML NF-e
- **Vendas** - Pedidos, orcamentos, PDV, NF-e de venda
- **Financeiro** - Contas a pagar/receber, boletos, promissorias
- **Bancario** - Integracao bancaria, FEBRABAN, conciliacao
- **Fiscal** - CFOP, IPI, PIS, COFINS, situacao tributaria
- **Relatorios** - Crystal Reports para diversas areas
- **Usuarios** - Controle de acesso e permissoes

## Requisitos

- Visual Studio 2022
- .NET Framework 4.7.2
- SQL Server (SQLEXPRESS)
- Crystal Reports Runtime
