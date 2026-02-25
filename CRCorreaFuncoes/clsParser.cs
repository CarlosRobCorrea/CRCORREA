using System;
using System.Data.SqlTypes;
using System.Drawing.Imaging;
using System.IO;


namespace CRCorreaFuncoes
{
    public static class clsParser
    {
        /// <summary>
        /// Converte um texto para Número Inteiro. Caso falhe retorna 0.
        /// </summary>
        /// <param name="s">Número em formato String a ser convertido.</param>
        /// <returns></returns>
        /// 
        
       public static byte[] imageToByteArray(System.Drawing.Image imageIn)
    {
        MemoryStream ms = new MemoryStream();
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
        return ms.ToArray();
    }

    public static Int32 Int32Parse(String s)
    {
        if (s == null)
        {
            return 0;
        }

        s = s.Replace(" ", "");

        Int32 resultado;
        Int32.TryParse(s, out resultado);

        return resultado;
    }

    public static Int64 Int64Parse(String s)
    {
        if (s == null)
        {
            return 0;
        }

        s = s.Replace(" ", "");

        Int64 resultado;
        Int64.TryParse(s, out resultado);

        return resultado;
    }

    public static SqlDateTime SqlDateTimeParse(String s)
    {
        DateTime resultado;
        SqlDateTime resultado2;
        try
        {
            DateTime.TryParse(s, out resultado);
            resultado2 = resultado;
        }
        catch
        {
            resultado2 = SqlDateTime.Null;
        }

        return resultado2;
    }

    public static DateTime DateTimeParse(String s)

    {
        DateTime resultado;
        DateTime resultado2;
        try
        {
            DateTime.TryParse(s, out resultado);
            resultado2 = resultado;
        }
        catch
        {
            resultado2 = DateTime.MinValue;
        }

        return resultado2;
    }

    public static float FloatParse(String s)
    {
        if (s == null)
        {
            return 0;
        }

        s = s.Replace(" ", "");

        float resultado;
        float.TryParse(s, out resultado);

        return resultado;
    }

    public static Decimal DecimalParse(String s)
    {
        if (s == null)
        {
            return 0;
        }

        s = s.Replace(" ", "");

        Decimal resultado;
        Decimal.TryParse(s, out resultado);

        return resultado;
    }

    public static Double DoubleParse(String s)
    {
        if (s == null)
        {
            return 0;
        }

        s = s.Replace(" ", "");

        Double resultado;
        Double.TryParse(s, out resultado);

        return resultado;
    }

    public static Char CharParse(String s)
    {
        Char resultado;

        Char.TryParse(s, out resultado);

        return resultado;
    }

    // SqlDateTimeFormat
    //
    public static String SqlDateTimeFormat(String valor, Boolean ultimocampo)
    {
        if (clsInfo.sqlLanguage.Rows[0]["COLUMN1"].ToString() == "27") // Linguagem Portugues Brasil
        {
            if (valor.Length == 10 || valor.Length == 16 || valor.Length == 19)
            {
                valor = SqlDateTimeParse(valor).Value.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }
        else
        {
            if (valor.Length == 10 || valor.Length == 16 || valor.Length == 19)
            {
                valor = SqlDateTimeParse(valor).Value.ToString("MM/dd/yyyy HH:mm:ss");
            }
        }
        if (ultimocampo == true)
        {
            if (valor.Replace('/', ' ').Trim() == "")
            {
                return valor = "NULL ";
            }
            else
            {
                return valor.Insert(0, "'").Insert((valor.Length + 1), "' ");
            }
        }
        else
        {
            if (valor.Replace('/', ' ').Trim() == "")
            {
                return valor = "NULL, ";
            }
            else
            {
                return valor.Insert(0, "'").Insert((valor.Length + 1), "', ");
            }
        }
    }

    public static String SqlDateTimeFormat(String valor, Boolean ultimocampo, String nomecampo)
    {
        return SqlDateTimeFormat(valor, ultimocampo);
    }

    public static String SqlDateTimeFormat(SqlDateTime valor, Boolean ultimocampo)
    {
        if (ultimocampo == true)
        {
            if (clsInfo.sqlLanguage.Rows[0]["COLUMN1"].ToString() == "27") // Linguagem Portugues Brasil
            {
                return valor.Value.ToString("dd/MM/yyyy HH:mm:ss").Insert(0, "'").Insert((valor.Value.ToString("dd/MM/yyyy HH:mm:ss").Length + 1), "' ");
            }
            else
            {
                return valor.Value.ToString("MM/dd/yyyy HH:mm:ss").Insert(0, "'").Insert((valor.Value.ToString("MM/dd/yyyy HH:mm:ss").Length + 1), "' ");
            }
        }
        else
        {
            if (clsInfo.sqlLanguage.Rows[0]["COLUMN1"].ToString() == "27") // Linguagem Portugues Brasil
            {
                return valor.Value.ToString("dd/MM/yyyy HH:mm:ss").Insert(0, "'").Insert((valor.Value.ToString("dd/MM/yyyy HH:mm:ss").Length + 1), "', ");
            }
            else
            {
                return valor.Value.ToString("MM/dd/yyyy HH:mm:ss").Insert(0, "'").Insert((valor.Value.ToString("MM/dd/yyyy HH:mm:ss").Length + 1), "', ");
            }
        }
    }

    public static String SqlDateTimeFormat(SqlDateTime valor, Boolean ultimocampo, String nomecampo)
    {
        return SqlDateTimeFormat(valor, ultimocampo);
    }

    // SqlStringFormat
    //
    public static String SqlStringFormat(String valor, Boolean ultimocampo)
    {
        if (ultimocampo == true)
        {
            return valor.Insert(0, "'").Insert((valor.Length + 1), "' ");
        }
        else
        {
            return valor.Insert(0, "'").Insert((valor.Length + 1), "', ");
        }
    }

    public static String SqlStringFormat(String valor, Boolean ultimocampo, String nomecampo)
    {
        return SqlStringFormat(valor, ultimocampo);
    }

    // SqlInt32Format
    //
    public static String SqlInt32Format(String valor, Boolean ultimocampo)
    {
        Int32 resultado;
        SqlInt32 resultado2;
        try
        {
            Int32.TryParse(valor, out resultado);
            resultado2 = resultado;
        }
        catch
        {
            resultado2 = 0;
        }
        if (ultimocampo == true)
        {
            return resultado2.ToString().Insert(resultado2.ToString().Length, " ");
        }
        else
        {
            return resultado2.ToString().Insert(resultado2.ToString().Length, ", ");
        }
    }

    public static String SqlInt32Format(String valor, Boolean ultimocampo, String nomecampo)
    {
        return SqlInt32Format(valor, ultimocampo);
    }

    public static String SqlInt32Format(Int32 valor, Boolean ultimocampo)
    {
        Int32 resultado;
        SqlInt32 resultado2;
        try
        {
            Int32.TryParse(valor.ToString(), out resultado);
            resultado2 = resultado;
        }
        catch
        {
            resultado2 = 0;
        }
        if (ultimocampo == true)
        {
            return resultado2.ToString().Insert(resultado2.ToString().Length, " ");
        }
        else
        {
            return resultado2.ToString().Insert(resultado2.ToString().Length, ", ");
        }
    }

    public static String SqlInt32Format(Int32 valor, Boolean ultimocampo, String nomecampo)
    {
        return SqlInt32Format(valor, ultimocampo);
    }

    // SqlInt64Format
    //
    public static String SqlInt64Format(String valor, Boolean ultimocampo)
    {
        Int64 resultado;
        SqlInt64 resultado2;
        try
        {
            Int64.TryParse(valor, out resultado);
            resultado2 = resultado;
        }
        catch
        {
            resultado2 = 0;
        }
        if (ultimocampo == true)
        {
            return resultado2.ToString().Insert(resultado2.ToString().Length, " ");
        }
        else
        {
            return resultado2.ToString().Insert(resultado2.ToString().Length, ", ");
        }
    }

    public static String SqlInt64Format(String valor, Boolean ultimocampo, String nomecampo)
    {
        return SqlInt64Format(valor, ultimocampo);
    }

    public static String SqlInt64Format(Int64 valor, Boolean ultimocampo)
    {
        Int64 resultado;
        SqlInt64 resultado2;
        try
        {
            Int64.TryParse(valor.ToString(), out resultado);
            resultado2 = resultado;
        }
        catch
        {
            resultado2 = 0;
        }
        if (ultimocampo == true)
        {
            return resultado2.ToString().Insert(resultado2.ToString().Length, " ");
        }
        else
        {
            return resultado2.ToString().Insert(resultado2.ToString().Length, ", ");
        }
    }

    public static String SqlInt64Format(Int64 valor, Boolean ultimocampo, String nomecampo)
    {
        return SqlInt64Format(valor, ultimocampo);
    }

    // SqlDecimalFormat
    //
    public static String SqlDecimalFormat(String valor, Boolean ultimocampo)
    {
        Decimal resultado;
        SqlDecimal resultado2;
        try
        {
            Decimal.TryParse(valor, out resultado);
            resultado2 = resultado;
        }
        catch
        {
            resultado2 = 0;
        }
        if (ultimocampo == true)
        {
            return resultado2.ToString().Insert(resultado2.ToString().Length, " ");
        }
        else
        {
            return resultado2.ToString().Insert(resultado2.ToString().Length, ", ");
        }
    }

    public static String SqlDecimalFormat(String valor, Boolean ultimocampo, String nomecampo)
    {
        return SqlDecimalFormat(valor, ultimocampo);
    }

    public static String SqlDecimalFormat(Decimal valor, Boolean ultimocampo)
    {
        Decimal resultado;
        SqlDecimal resultado2;
        try
        {
            Decimal.TryParse(valor.ToString(), out resultado);
            resultado2 = resultado;
        }
        catch
        {
            resultado2 = 0;
        }
        if (ultimocampo == true)
        {
            return resultado2.ToString().Insert(resultado2.ToString().Length, " ");
        }
        else
        {
            return resultado2.ToString().Insert(resultado2.ToString().Length, ", ");
        }
    }

    public static String SqlDecimalFormat(Decimal valor, Boolean ultimocampo, String nomecampo)
    {
        return SqlDecimalFormat(valor, ultimocampo);
    }

    public static String SqlFotoFormat(System.Drawing.Image foto, Boolean ultimocampo)
    {
        //string texto = ""; 
        ////The we make the cicles to read pixel by pixel and we make the comparation with the withe color.  
        //for (int i = 0; i < foto.Width; i++)
        //{
        //    for (int j = 0; j < foto.Height; j++)
        //    { 
        //        //When we add the value to the string we should invert the 
        //        //order because the images are reading from top to bottom  
        //        //and the textBox is write from left to right.  
        //        if (foto.GGetPixel(j, i) == "255" && foto.GetPixel(j,
        //        i).B.ToString() == "255" && img.GetPixel(j, i).G.ToString() ==
        //        "255" && img.GetPixel(j, i).R.ToString() == "255")
        //        { 
        //            texto = texto + "0"; 
        //        } 
        //        else
        //        { 
        //            texto = texto + "1"; 
        //        } 
        //    } 
        //    texto = texto + "\r\n"; // this is to make the enter between lines  
        //} 

        MemoryStream mstr = new MemoryStream();
        foto.Save(mstr, ImageFormat.Jpeg);
        byte[] arrImage = mstr.ToArray();

        ////MemoryStream ms = new MemoryStream();
        ////Byte[] myData;
        ////try
        ////{
        ////    foto.Save(ms, ImageFormat.Jpeg); //Salvando imagem no memoryStream 
        ////    myData = new Byte[ms.Length];
        ////    myData = ms.ToArray();
        ////}
        ////catch
        ////{
        ////    foto = Properties.Resources.Cancelar;
        ////    foto.Save(ms, ImageFormat.Jpeg); //Salvando imagem no memoryStream 
        ////    myData = new Byte[ms.Length];
        ////    myData = ms.ToArray();
        ////}                     
        ////UTF8Encoding.UTF8.GetBytes(arrImage);

        if (ultimocampo == true)
        {
            return Convert.ToBase64String(arrImage);
        }
        else
        {
            return Convert.ToBase64String(arrImage);
        }
    }

    public static String SqlFotoFormat(System.Drawing.Image valor, Boolean ultimocampo, String nomecampo)
    {
        return SqlFotoFormat(valor, ultimocampo);
    }
    //
    //

    public static Byte[] CodificarFoto(System.Drawing.Image foto)
    {
        MemoryStream ms = new MemoryStream();
        Byte[] myData;
        try
        {
            foto.Save(ms, ImageFormat.Jpeg); //Salvando imagem no memoryStream 
            myData = new Byte[ms.Length];
            myData = ms.ToArray();
        }
        catch
        {
            //foto = Properties.Resources.Cancelar;
            foto.Save(ms, ImageFormat.Jpeg); //Salvando imagem no memoryStream 
            myData = new Byte[ms.Length];
            myData = ms.ToArray();
        }
        return myData;
    }

    public static Byte[] CodificarFotoMonocromatico(System.Drawing.Image foto)
    {
        MemoryStream ms = new MemoryStream();
        Byte[] myData;
        try
        {
            foto.Save(ms, ImageFormat.Jpeg); //Salvando imagem no memoryStream 
            myData = new Byte[ms.Length];
            myData = ms.ToArray();

            for (Int32 x = 0; x < Int32.Parse(myData.Length.ToString()); x++)
            {
                // if (myData[x].ToString() != "0" && myData[x].ToString()!= "255")
                if (x == 0 || x == 1 || x == 0 || x == 2)
                {
                    myData[x] = Byte.Parse("0".ToString());
                }
            }
        }
        catch
        {
            //foto = Properties.Resources.Cancelar;
            foto.Save(ms, ImageFormat.Jpeg); //Salvando imagem no memoryStream 
            myData = new Byte[ms.Length];
            myData = ms.ToArray();
        }
        return myData;
    }

    /// <summary>
    /// Converte um []byte em foto
    /// </summary>
    /// <param name="foto">A foto no formato de byte</param>
    /// <returns>A imagem convertida</returns>
    public static System.Drawing.Image DecodificarFoto(Byte[] foto)
    {
        System.Drawing.Image imagem;
        try
        {
            MemoryStream ms = new MemoryStream(foto);
            imagem = System.Drawing.Image.FromStream(ms);
        }
        catch
        {
                //imagem = Properties.Resources.Cancelar;  = esta era a correta
                MemoryStream ms = new MemoryStream(foto);
                imagem = System.Drawing.Image.FromStream(ms);
            }
        return imagem;
    }

    /// <summary>
    /// Deve receber um Datatable.Rows[x][x]
    /// </summary>
    /// <param name="datacell"></param>
    /// <returns></returns>
    public static System.Drawing.Image DecodificarFotoDatatablerowcell(object datacell)
    {
        if (datacell == null)
        {
            return null;
        }

        Byte[] foto;
        try
        {
            foto = (byte[])datacell;
        }
        catch (Exception)
        {
            return null;
        }

        System.Drawing.Image imagem;
        try
        {
            MemoryStream ms = new MemoryStream(foto);
            imagem = System.Drawing.Image.FromStream(ms);
        }
        catch
        {
                //imagem = Properties.Resources.Cancelar; = correta
                MemoryStream ms = new MemoryStream(foto);
                imagem = System.Drawing.Image.FromStream(ms);

            }
            return imagem;
    }

    public static SqlBoolean SqlBooleanParse(String s)
    {
        SqlBoolean resultado;
        try
        {
            resultado = SqlBoolean.Parse(s);
        }
        catch
        {
            resultado = SqlBoolean.False;
        }
        return resultado;
    }

    public static String StringData(String s)
    {
        String resultado;
        try
        {
            resultado = s.Substring(6, 2) + "/" + s.Substring(4, 2) + "/" + s.Substring(0, 4);
        }
        catch
        {
            resultado = "";
        }
        return resultado;
    }

    /// <summary>
    /// Retorna o texto [dd/MM/yyyy] a partir de uma Data. Em caso de falha retorna ''.
    /// </summary>
    /// <param name="d">Data a ser utilizada no processo.</param>
    /// <returns></returns>
    public static String RData(DateTime d)
    {
        if (d == null)
            return "";

        return d.ToString("dd/MM/yyyy");
    }

    /// <summary>
    /// Retorna o texto [dd/MM/yyyy HH:mm] a partir de uma Data. Em caso de falha retorna ''.
    /// </summary>
    /// <param name="d">Data a ser utilizada no processo.</param>
    /// <returns></returns>
    public static String RDataHora(DateTime d)
    {
        if (d == null)
            return "";

        return d.ToString("dd/MM/yyyy HH:mm");
    }

    /// <summary>
    /// Retorna o texto [dd/MM/yyyy HH:mm:ss] a partir de uma Data. Em caso de falha retorna ''.
    /// </summary>
    /// <param name="d">Data a ser passada.</param>
    /// <returns></returns>
    public static String RDataHoraSegundo(DateTime d)
    {
        if (d == null)
            return "";

        return d.ToString("dd/MM/yyyy HH:mm:ss");
    }

    public static Byte ByteParse(String s)
    {
        Byte resultado;

        if (Byte.TryParse(s, out resultado) == true)
        {
            return resultado;
        }
        else
        {
            return 0;
        }
    }


}
}
