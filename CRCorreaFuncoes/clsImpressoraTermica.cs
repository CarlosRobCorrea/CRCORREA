using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace CRCorreaFuncoes
{
    public class RawPrinterHelper
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            IntPtr hPrinter;
            Int32 dwWritten = 0;

            DOCINFOA di = new DOCINFOA();
            di.pDocName = "Cupom";
            di.pDataType = "RAW";

            if (!OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
                return false;

            if (!StartDocPrinter(hPrinter, 1, di))
            {
                ClosePrinter(hPrinter);
                return false;
            }

            if (!StartPagePrinter(hPrinter))
            {
                EndDocPrinter(hPrinter);
                ClosePrinter(hPrinter);
                return false;
            }

            bool success = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);

            EndPagePrinter(hPrinter);
            EndDocPrinter(hPrinter);
            ClosePrinter(hPrinter);

            return success;
        }

        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;

            dwCount = szString.Length;
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);

            bool result = SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);

            return result;
        }

        public static bool SendBytesToPrinter(string szPrinterName, byte[] data)
        {
            IntPtr pUnmanagedBytes = Marshal.AllocCoTaskMem(data.Length);
            Marshal.Copy(data, 0, pUnmanagedBytes, data.Length);
            bool result = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, data.Length);
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return result;
        }
    }

    public class clsImpressoraTermica
    {
        private List<byte> _buffer;
        private string _nomePrinter;
        private int _colunas;

        private static readonly byte[] ESC_INIT = { 0x1B, 0x40 };
        private static readonly byte[] ESC_BOLD_ON = { 0x1B, 0x45, 0x01 };
        private static readonly byte[] ESC_BOLD_OFF = { 0x1B, 0x45, 0x00 };
        private static readonly byte[] ESC_CENTER = { 0x1B, 0x61, 0x01 };
        private static readonly byte[] ESC_LEFT = { 0x1B, 0x61, 0x00 };
        private static readonly byte[] ESC_RIGHT = { 0x1B, 0x61, 0x02 };
        private static readonly byte[] ESC_DOUBLE_HEIGHT = { 0x1B, 0x21, 0x10 };
        private static readonly byte[] ESC_NORMAL = { 0x1B, 0x21, 0x00 };
        private static readonly byte[] ESC_CUT = { 0x1D, 0x56, 0x01 };
        private static readonly byte[] ESC_FEED = { 0x0A };

        public clsImpressoraTermica(string nomePrinter, int colunas = 48)
        {
            _nomePrinter = nomePrinter;
            _colunas = colunas;
            _buffer = new List<byte>();
            _buffer.AddRange(ESC_INIT);
        }

        public void LinhaVazia()
        {
            _buffer.AddRange(ESC_FEED);
        }

        public void Texto(string texto)
        {
            _buffer.AddRange(Encoding.GetEncoding(850).GetBytes(texto));
            _buffer.AddRange(ESC_FEED);
        }

        public void TextoCentralizado(string texto)
        {
            _buffer.AddRange(ESC_CENTER);
            _buffer.AddRange(Encoding.GetEncoding(850).GetBytes(texto));
            _buffer.AddRange(ESC_FEED);
            _buffer.AddRange(ESC_LEFT);
        }

        public void TextoNegrito(string texto)
        {
            _buffer.AddRange(ESC_BOLD_ON);
            _buffer.AddRange(Encoding.GetEncoding(850).GetBytes(texto));
            _buffer.AddRange(ESC_FEED);
            _buffer.AddRange(ESC_BOLD_OFF);
        }

        public void TextoCentralizadoNegrito(string texto)
        {
            _buffer.AddRange(ESC_CENTER);
            _buffer.AddRange(ESC_BOLD_ON);
            _buffer.AddRange(Encoding.GetEncoding(850).GetBytes(texto));
            _buffer.AddRange(ESC_FEED);
            _buffer.AddRange(ESC_BOLD_OFF);
            _buffer.AddRange(ESC_LEFT);
        }

        public void TextoGrande(string texto)
        {
            _buffer.AddRange(ESC_CENTER);
            _buffer.AddRange(ESC_DOUBLE_HEIGHT);
            _buffer.AddRange(Encoding.GetEncoding(850).GetBytes(texto));
            _buffer.AddRange(ESC_FEED);
            _buffer.AddRange(ESC_NORMAL);
            _buffer.AddRange(ESC_LEFT);
        }

        public void Separador(char c = '-')
        {
            Texto(new string(c, _colunas));
        }

        public void DuasColunas(string esquerda, string direita)
        {
            int espacos = _colunas - esquerda.Length - direita.Length;
            if (espacos < 1) espacos = 1;
            Texto(esquerda + new string(' ', espacos) + direita);
        }

        public void TresColunas(string col1, string col2, string col3)
        {
            int terco = _colunas / 3;
            string linha = col1.PadRight(terco) + col2.PadRight(terco) + col3.PadLeft(_colunas - terco * 2);
            Texto(linha);
        }

        public void Cortar()
        {
            _buffer.AddRange(ESC_FEED);
            _buffer.AddRange(ESC_FEED);
            _buffer.AddRange(ESC_FEED);
            _buffer.AddRange(ESC_CUT);
        }

        public bool Imprimir()
        {
            return RawPrinterHelper.SendBytesToPrinter(_nomePrinter, _buffer.ToArray());
        }

        public static bool ImprimirCupomPedido(string nomePrinter, string nomeEmpresa,
            string cnpjEmpresa, string numeroPedido, string data,
            string nomeCliente, DataTable itens,
            string formaPagamento, string valorTotal)
        {
            var imp = new clsImpressoraTermica(nomePrinter);

            imp.TextoGrande(nomeEmpresa);
            imp.TextoCentralizado("CNPJ: " + cnpjEmpresa);
            imp.Separador('=');
            imp.TextoCentralizadoNegrito("PEDIDO #" + numeroPedido);
            imp.Separador();
            imp.DuasColunas("Data:", data);
            imp.DuasColunas("Cliente:", nomeCliente);
            imp.Separador();

            imp.TextoNegrito(" QTD  DESCRICAO              TOTAL");
            imp.Separador();

            foreach (DataRow row in itens.Rows)
            {
                string qtd = row["QTDE"].ToString().PadRight(5);
                string desc = row["DESCRICAO"].ToString();
                if (desc.Length > 22) desc = desc.Substring(0, 22);
                desc = desc.PadRight(22);
                string total = "R$ " + Decimal.Parse(row["TOTALNOTA"].ToString()).ToString("N2");
                imp.Texto(qtd + desc + total.PadLeft(imp._colunas - 27));
            }

            imp.Separador();
            imp.DuasColunas("Pagamento:", formaPagamento);
            imp.TextoNegrito("TOTAL: R$ " + valorTotal);
            imp.Separador('=');
            imp.LinhaVazia();
            imp.TextoCentralizado("Obrigado pela preferencia!");
            imp.LinhaVazia();
            imp.Cortar();

            return imp.Imprimir();
        }

        public static bool ImprimirDanfeNfce(string nomePrinter, FocusNFeResponse resposta,
            string nomeEmpresa, string cnpjEmpresa, string numeroPedido,
            string data, string nomeCliente, DataTable itens,
            string formaPagamento, string valorTotal)
        {
            var imp = new clsImpressoraTermica(nomePrinter);

            imp.TextoGrande(nomeEmpresa);
            imp.TextoCentralizado("CNPJ: " + cnpjEmpresa);
            imp.Separador('=');
            imp.TextoCentralizadoNegrito("DANFE NFC-e");
            imp.Separador();
            imp.DuasColunas("Pedido:", numeroPedido);
            imp.DuasColunas("Data:", data);
            imp.DuasColunas("Cliente:", nomeCliente);
            imp.Separador();

            imp.TextoNegrito(" QTD  DESCRICAO              TOTAL");
            imp.Separador();

            foreach (DataRow row in itens.Rows)
            {
                string qtd = row["QTDE"].ToString().PadRight(5);
                string desc = row["DESCRICAO"].ToString();
                if (desc.Length > 22) desc = desc.Substring(0, 22);
                desc = desc.PadRight(22);
                string total = "R$ " + Decimal.Parse(row["TOTALNOTA"].ToString()).ToString("N2");
                imp.Texto(qtd + desc + total.PadLeft(imp._colunas - 27));
            }

            imp.Separador();
            imp.DuasColunas("Pagamento:", formaPagamento);
            imp.TextoNegrito("TOTAL: R$ " + valorTotal);
            imp.Separador('=');

            if (!String.IsNullOrEmpty(resposta.chave_nfe))
            {
                imp.LinhaVazia();
                imp.TextoCentralizado("Chave de Acesso:");
                imp.TextoCentralizado(resposta.chave_nfe);
            }

            if (!String.IsNullOrEmpty(resposta.numero))
            {
                imp.DuasColunas("NFC-e:", resposta.numero);
            }

            if (!String.IsNullOrEmpty(resposta.serie))
            {
                imp.DuasColunas("Serie:", resposta.serie);
            }

            if (!String.IsNullOrEmpty(resposta.status_sefaz))
            {
                imp.DuasColunas("Status SEFAZ:", resposta.status_sefaz + " - " + (resposta.mensagem_sefaz ?? ""));
            }

            if (!String.IsNullOrEmpty(resposta.url_consulta))
            {
                imp.LinhaVazia();
                imp.TextoCentralizado("Consulte em:");
                imp.TextoCentralizado(resposta.url_consulta);
            }

            imp.Separador('=');
            imp.LinhaVazia();
            imp.Cortar();

            return imp.Imprimir();
        }
    }
}
