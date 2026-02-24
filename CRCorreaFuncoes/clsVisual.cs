using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRCorreaFuncoes
{
    public static class clsVisual
    {
        /// <summary>
        /// Armazena o 'Nome do Controle' e sua respectiva 'BackColor' antes de ser modificada.
        /// </summary>
        private static Hashtable hteCor = new Hashtable();

        /// <summary>
        /// Muda a cor de fundo do controle selecionado. Suporte apenas para os controles: 'TextBox', 'ComboBox', 'CheckBox', 'RadioButton'.
        /// </summary>
        /// <param name="sender">Controle que ativou o evento.</param>
        public static void ControlEnter(object sender)
        {
            if (sender is ToolStripTextBox &&
                !hteCor.Contains(((ToolStripTextBox)sender).Name))
            {
                hteCor.Add(((ToolStripTextBox)sender).Name, ((ToolStripTextBox)sender).BackColor);

                ((ToolStripTextBox)sender).BackColor = Color.LightSteelBlue;
                ((ToolStripTextBox)sender).Select();
            }
            else if (!hteCor.Contains(((Control)sender).Name))
            {
                hteCor.Add(((Control)sender).Name, ((Control)sender).BackColor);

                ((Control)sender).BackColor = Color.LightSteelBlue;
                ((Control)sender).Select();

                if (sender is TextBox)
                {
                    ((TextBox)sender).SelectAll();
                }
            }
        }
        /// <summary>
        /// Retorna a cor de fundo antes do Evento 'ControlEnter' ter sido acionado no controle. Suporte apenas para os controles: 'TextBox', 'ComboBox', 'CheckBox', 'RadioButton'.
        /// </summary>
        /// <param name="sender">Controle que ativou o evento.</param>
        public static void ControlLeave(object sender)
        {
            if (sender is ToolStripTextBox)
            {
                if (hteCor.Contains(((ToolStripTextBox)sender).Name) == true)
                {
                    ((ToolStripTextBox)sender).BackColor = (Color)hteCor[((ToolStripTextBox)sender).Name];
                    hteCor.Remove(((ToolStripTextBox)sender).Name);
                }
            }
            else
            {
                if (hteCor.Contains(((Control)sender).Name) == true)
                {
                    ((Control)sender).BackColor = (Color)hteCor[((Control)sender).Name];
                    hteCor.Remove(((Control)sender).Name);
                }
            }
        }

        /// <summary>
        /// Pressionando 'Enter' ou 'Tecla para Baixo' seleciona o próximo controle. 'Tecla para Cima' seleciona o controle anterior.
        /// </summary>
        /// <param name="sender">Controle que ativou o Evento.</param>
        /// <param name="e">Controle de Evento.</param>
        public static void ControlKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox)
            {
                if (((TextBox)sender).ReadOnly == true)
                {
                    return;
                }
            }

            if (sender is ComboBox ||
                sender is ListBox)
            {
                if (e.KeyCode == Keys.Enter) SendKeys.Send("{Tab}");
            }
            else
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down) SendKeys.Send("{Tab}");
                else if (e.KeyCode == Keys.Up) SendKeys.Send("+{Tab}");
            }
        }

        /// <summary>
        /// Máscara para campo de CNPJ.
        /// Pressionando 'Enter' ou 'Tecla para Baixo' seleciona o próximo controle. 'Tecla para Cima' seleciona o controle anterior.
        /// </summary>
        /// <param name="t">Controle que ativou o Evento.</param>
        /// <param name="e">Controle de Evento.</param>
        public static void ControlKeyDownCnpj(TextBox t, KeyEventArgs e)
        {
            if (t.ReadOnly == true)
            {
                return;
            }

            Int32 posicao = 0;
            String letra = ((Char)e.KeyCode).ToString();

            if (e.KeyCode == Keys.NumPad0) { letra = "0"; }
            else if (e.KeyCode == Keys.NumPad1) { letra = "1"; }
            else if (e.KeyCode == Keys.NumPad2) { letra = "2"; }
            else if (e.KeyCode == Keys.NumPad3) { letra = "3"; }
            else if (e.KeyCode == Keys.NumPad4) { letra = "4"; }
            else if (e.KeyCode == Keys.NumPad5) { letra = "5"; }
            else if (e.KeyCode == Keys.NumPad6) { letra = "6"; }
            else if (e.KeyCode == Keys.NumPad7) { letra = "7"; }
            else if (e.KeyCode == Keys.NumPad8) { letra = "8"; }
            else if (e.KeyCode == Keys.NumPad9) { letra = "9"; }

            if (letra == "0" ||
                letra == "1" ||
                letra == "2" ||
                letra == "3" ||
                letra == "4" ||
                letra == "5" ||
                letra == "6" ||
                letra == "7" ||
                letra == "8" ||
                letra == "9")
            {
                posicao = t.SelectionStart;
                t.Text = t.Text.Remove(t.SelectionStart, t.SelectionLength);
            }
            else
            {
                posicao = t.SelectionStart;
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SendKeys.Send("+{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (t.SelectionStart + 1 <= t.Text.Length)
                {
                    if (t.Text[t.SelectionStart] == '.' ||
                        t.Text[t.SelectionStart] == '/' ||
                        t.Text[t.SelectionStart] == '-')
                    {
                        if (t.SelectionStart + 2 <= t.Text.Length)
                        {
                            t.SelectionStart += 1;
                        }
                        else
                        {
                            e.SuppressKeyPress = true;
                            return;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (t.SelectionStart - 1 >= 0 &&
                    t.SelectionStart - 1 <= t.Text.Length)
                {
                    if (t.SelectionStart - 2 >= 0 &&
                        t.SelectionStart - 2 <= t.Text.Length)
                    {
                        if (t.Text[t.SelectionStart - 2] == '.' ||
                            t.Text[t.SelectionStart - 2] == '/' ||
                            t.Text[t.SelectionStart - 2] == '-')
                        {
                            t.SelectionStart -= 1;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (t.SelectionStart == 0)
                {
                    return;
                }

                if (t.SelectionStart - 2 >= 0 &&
                    t.SelectionStart - 2 <= t.Text.Length)
                {
                    if (t.Text[t.SelectionStart - 2] == '.' ||
                        t.Text[t.SelectionStart - 2] == '/' ||
                        t.Text[t.SelectionStart - 2] == '-')
                    {
                        t.Text = t.Text.Remove(t.SelectionStart - 2, 2);
                        RefazerMaskCnpj(t, posicao, false);
                        t.SelectionStart = posicao - 2;

                        e.SuppressKeyPress = true;
                        return;
                    }
                }
                t.Text = t.Text.Remove(t.SelectionStart - 1, 1);
                RefazerMaskCnpj(t, posicao, false);
                posicao = posicao - 1;
                t.SelectionStart = posicao;

                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (t.SelectionStart == t.Text.Length)
                {
                    return;
                }

                if (t.Text[t.SelectionStart] == '.' ||
                    t.Text[t.SelectionStart] == '/' ||
                    t.Text[t.SelectionStart] == '-')
                {
                    t.Text = t.Text.Remove(t.SelectionStart + 1, 1);
                }
                else
                {
                    t.Text = t.Text.Remove(t.SelectionStart, 1);
                }

                RefazerMaskCnpj(t, posicao, false);
                t.SelectionStart = posicao;
                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Home ||
                     e.KeyCode == Keys.End)
            {
                return;
            }
            else if (t.Text.Length >= 18)
            {
                e.SuppressKeyPress = true;
                return;
            }
            else if (letra == "0" ||
                    letra == "1" ||
                        letra == "2" ||
                    letra == "3" ||
                    letra == "4" ||
                    letra == "5" ||
                    letra == "6" ||
                    letra == "7" ||
                    letra == "8" ||
                    letra == "9")
            {
                t.Text = t.Text.Insert(posicao, letra);
                if (posicao + 1 == t.Text.Length) posicao += RefazerMaskCnpj(t, posicao, true);
                else RefazerMaskCnpj(t, posicao, true);
                t.SelectionStart = posicao + 1;

                if (t.Text[t.SelectionStart - 1] == '.' ||
                    t.Text[t.SelectionStart - 1] == '/' ||
                    t.Text[t.SelectionStart - 1] == '-')
                {
                    t.SelectionStart++;
                }

                e.SuppressKeyPress = true;
            }
            else
            {
                e.SuppressKeyPress = true;
                return;
            }
        }

        public static Int32 RefazerMaskCnpj(TextBox t, Int32 posicao, Boolean maiorigual)
        {
            Int32 retorno = 0;
            t.Text = t.Text.Replace(".", "").Replace("/", "").Replace("-", "");

            for (Int32 x = 0; x < t.Text.Length; x++)
            {
                if (x + 2 > t.Text.Length) break;

                if (x == 1)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, ".");
                }
                else if (x == 5)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, ".");
                }
                else if (x == 9)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, "/");
                }
                else if (x == 14)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, "-");
                }
            }

            return retorno;
        }

        /// <summary>
        /// Máscara para campo de CPF.
        /// Pressionando 'Enter' ou 'Tecla para Baixo' seleciona o próximo controle. 'Tecla para Cima' seleciona o controle anterior.
        /// </summary>
        /// <param name="t">Controle que ativou o Evento.</param>
        /// <param name="e">Controle de Evento.</param>
        public static void ControlKeyDownCpf(TextBox t, KeyEventArgs e)
        {
            if (t.ReadOnly == true)
            {
                return;
            }

            Int32 posicao = 0;
            String letra = ((Char)e.KeyCode).ToString();

            if (e.KeyCode == Keys.NumPad0) { letra = "0"; }
            else if (e.KeyCode == Keys.NumPad1) { letra = "1"; }
            else if (e.KeyCode == Keys.NumPad2) { letra = "2"; }
            else if (e.KeyCode == Keys.NumPad3) { letra = "3"; }
            else if (e.KeyCode == Keys.NumPad4) { letra = "4"; }
            else if (e.KeyCode == Keys.NumPad5) { letra = "5"; }
            else if (e.KeyCode == Keys.NumPad6) { letra = "6"; }
            else if (e.KeyCode == Keys.NumPad7) { letra = "7"; }
            else if (e.KeyCode == Keys.NumPad8) { letra = "8"; }
            else if (e.KeyCode == Keys.NumPad9) { letra = "9"; }

            if (letra == "0" ||
                letra == "1" ||
                letra == "2" ||
                letra == "3" ||
                letra == "4" ||
                letra == "5" ||
                letra == "6" ||
                letra == "7" ||
                letra == "8" ||
                letra == "9")
            {
                posicao = t.SelectionStart;
                t.Text = t.Text.Remove(t.SelectionStart, t.SelectionLength);
            }
            else
            {
                posicao = t.SelectionStart;
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SendKeys.Send("+{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (t.SelectionStart + 1 <= t.Text.Length)
                {
                    if (t.Text[t.SelectionStart] == '.' ||
                        t.Text[t.SelectionStart] == '-')
                    {
                        if (t.SelectionStart + 2 <= t.Text.Length)
                        {
                            t.SelectionStart += 1;
                        }
                        else
                        {
                            e.SuppressKeyPress = true;
                            return;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (t.SelectionStart - 1 >= 0 &&
                    t.SelectionStart - 1 <= t.Text.Length)
                {
                    if (t.SelectionStart - 2 >= 0 &&
                        t.SelectionStart - 2 <= t.Text.Length)
                    {
                        if (t.Text[t.SelectionStart - 2] == '.' ||
                            t.Text[t.SelectionStart - 2] == '-')
                        {
                            t.SelectionStart -= 1;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (t.SelectionStart == 0)
                {
                    return;
                }

                if (t.SelectionStart - 2 >= 0 &&
                    t.SelectionStart - 2 <= t.Text.Length)
                {
                    if (t.Text[t.SelectionStart - 2] == '.' ||
                        t.Text[t.SelectionStart - 2] == '-')
                    {
                        t.Text = t.Text.Remove(t.SelectionStart - 2, 2);
                        RefazerMaskCpf(t, posicao, false);
                        t.SelectionStart = posicao - 2;

                        e.SuppressKeyPress = true;
                        return;
                    }
                }
                t.Text = t.Text.Remove(t.SelectionStart - 1, 1);
                RefazerMaskCpf(t, posicao, false);
                posicao = posicao - 1;
                t.SelectionStart = posicao;

                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (t.SelectionStart == t.Text.Length)
                {
                    return;
                }

                if (t.Text[t.SelectionStart] == '.' ||
                    t.Text[t.SelectionStart] == '-')
                {
                    t.Text = t.Text.Remove(t.SelectionStart + 1, 1);
                }
                else
                {
                    t.Text = t.Text.Remove(t.SelectionStart, 1);
                }

                RefazerMaskCpf(t, posicao, false);
                t.SelectionStart = posicao;
                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Home ||
                     e.KeyCode == Keys.End)
            {
                return;
            }
            else if (t.Text.Length >= 14)
            {
                e.SuppressKeyPress = true;
                return;
            }
            else if (letra == "0" ||
                    letra == "1" ||
                    letra == "2" ||
                    letra == "3" ||
                    letra == "4" ||
                    letra == "5" ||
                    letra == "6" ||
                    letra == "7" ||
                    letra == "8" ||
                    letra == "9")
            {
                t.Text = t.Text.Insert(posicao, letra);
                if (posicao + 1 == t.Text.Length) posicao += RefazerMaskCpf(t, posicao, true);
                else RefazerMaskCpf(t, posicao, true);
                t.SelectionStart = posicao + 1;

                if (t.Text[t.SelectionStart - 1] == '.' ||
                    t.Text[t.SelectionStart - 1] == '-')
                {
                    t.SelectionStart++;
                }

                e.SuppressKeyPress = true;
            }
            else
            {
                e.SuppressKeyPress = true;
                return;
            }
        }

        private static Int32 RefazerMaskCpf(TextBox t, Int32 posicao, Boolean maiorigual)
        {
            Int32 retorno = 0;
            t.Text = t.Text.Replace(".", "").Replace("-", "");

            for (Int32 x = 0; x < t.Text.Length; x++)
            {
                if (x + 2 > t.Text.Length) break;

                if (x == 2)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, ".");
                }
                else if (x == 6)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, ".");
                }
                else if (x == 10)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, "-");
                }
            }

            return retorno;
        }

        /// <summary>
        /// Máscara para campo de RG.
        /// Pressionando 'Enter' ou 'Tecla para Baixo' seleciona o próximo controle. 'Tecla para Cima' seleciona o controle anterior.
        /// </summary>
        /// <param name="t">Controle que ativou o Evento.</param>
        /// <param name="e">Controle de Evento.</param>
        public static void ControlKeyDownRg(TextBox t, KeyEventArgs e)
        {
            if (t.ReadOnly == true)
            {
                return;
            }

            Int32 posicao = 0;
            posicao = t.SelectionStart;

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SendKeys.Send("+{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (t.SelectionStart + 1 <= t.Text.Length)
                {
                    if (t.Text[t.SelectionStart] == '.' ||
                        t.Text[t.SelectionStart] == '-')
                    {
                        if (t.SelectionStart + 2 <= t.Text.Length)
                        {
                            t.SelectionStart += 1;
                        }
                        else
                        {
                            e.SuppressKeyPress = true;
                            return;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (t.SelectionStart - 1 >= 0 &&
                    t.SelectionStart - 1 <= t.Text.Length)
                {
                    if (t.SelectionStart - 2 >= 0 &&
                        t.SelectionStart - 2 <= t.Text.Length)
                    {
                        if (t.Text[t.SelectionStart - 2] == '.' ||
                            t.Text[t.SelectionStart - 2] == '-')
                        {
                            t.SelectionStart -= 1;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (t.SelectionStart == 0)
                {
                    return;
                }

                if (t.SelectionStart - 2 >= 0 &&
                    t.SelectionStart - 2 <= t.Text.Length)
                {
                    if (t.Text[t.SelectionStart - 2] == '.' ||
                        t.Text[t.SelectionStart - 2] == '-')
                    {
                        t.Text = t.Text.Remove(t.SelectionStart - 2, 2);
                        RefazerMaskRg(t, posicao, false);
                        t.SelectionStart = posicao - 2;

                        e.SuppressKeyPress = true;
                        return;
                    }
                }
                t.Text = t.Text.Remove(t.SelectionStart - 1, 1);
                RefazerMaskRg(t, posicao, false);
                posicao = posicao - 1;
                t.SelectionStart = posicao;

                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (t.SelectionStart == t.Text.Length)
                {
                    return;
                }

                if (t.Text[t.SelectionStart] == '.' ||
                    t.Text[t.SelectionStart] == '-')
                {
                    t.Text = t.Text.Remove(t.SelectionStart + 1, 1);
                }
                else
                {
                    t.Text = t.Text.Remove(t.SelectionStart, 1);
                }

                RefazerMaskRg(t, posicao, false);
                t.SelectionStart = posicao;
                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Home ||
                     e.KeyCode == Keys.End)
            {
                return;
            }
            else if (t.Text.Length >= 12)
            {
                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.NumPad0 ||
                     e.KeyCode == Keys.NumPad1 ||
                     e.KeyCode == Keys.NumPad2 ||
                     e.KeyCode == Keys.NumPad3 ||
                     e.KeyCode == Keys.NumPad4 ||
                     e.KeyCode == Keys.NumPad5 ||
                     e.KeyCode == Keys.NumPad6 ||
                     e.KeyCode == Keys.NumPad7 ||
                     e.KeyCode == Keys.NumPad8 ||
                     e.KeyCode == Keys.NumPad9 ||
                     e.KeyCode == Keys.D0 ||
                     e.KeyCode == Keys.D1 ||
                     e.KeyCode == Keys.D2 ||
                     e.KeyCode == Keys.D3 ||
                     e.KeyCode == Keys.D4 ||
                     e.KeyCode == Keys.D5 ||
                     e.KeyCode == Keys.D6 ||
                     e.KeyCode == Keys.D7 ||
                     e.KeyCode == Keys.D8 ||
                     e.KeyCode == Keys.D9 ||
                     e.KeyCode == Keys.X)
            {
                if (t.Text.Length != 10 && e.KeyCode == Keys.X)
                {
                    e.SuppressKeyPress = true;
                    return;
                }

                t.Text = t.Text.Insert(posicao, ((Char)e.KeyCode).ToString());
                if (posicao + 1 == t.Text.Length) posicao += RefazerMaskRg(t, posicao, true);
                else RefazerMaskRg(t, posicao, true);
                t.SelectionStart = posicao + 1;

                if (t.Text[t.SelectionStart - 1] == '.' ||
                    t.Text[t.SelectionStart - 1] == '-')
                {
                    t.SelectionStart++;
                }

                e.SuppressKeyPress = true;
            }
            else
            {
                e.SuppressKeyPress = true;
                return;
            }
        }

        private static Int32 RefazerMaskRg(TextBox t, Int32 posicao, Boolean maiorigual)
        {
            Int32 retorno = 0;
            t.Text = t.Text.Replace(".", "").Replace("-", "");

            for (Int32 x = 0; x < t.Text.Length; x++)
            {
                if (x + 2 > t.Text.Length) break;

                if (x == 1)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, ".");
                }
                else if (x == 5)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, ".");
                }
                else if (x == 9)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, "-");
                }
            }

            return retorno;
        }

        /// <summary>
        /// Máscara para campo de Data - Hora - Segundo [dd/MM/yyyy HH:mm:ss].
        /// Pressionando 'Enter' ou 'Tecla para Baixo' seleciona o próximo controle. 'Tecla para Cima' seleciona o controle anterior.
        /// </summary>
        /// <param name="t">Controle que ativou o Evento.</param>
        /// <param name="e">Controle de Evento.</param>
        public static void ControlKeyDownDataHoraSegundo(TextBox t, KeyEventArgs e)
        {
            if (t.ReadOnly == true)
            {
                return;
            }

            Int32 posicao = 0;
            posicao = t.SelectionStart;

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SendKeys.Send("+{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (t.SelectionStart + 1 <= t.Text.Length)
                {
                    if (t.Text[t.SelectionStart] == '/' ||
                        t.Text[t.SelectionStart] == ':' ||
                        t.Text[t.SelectionStart] == ' ')
                    {
                        if (t.SelectionStart + 2 <= t.Text.Length)
                        {
                            t.SelectionStart += 1;
                        }
                        else
                        {
                            e.SuppressKeyPress = true;
                            return;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (t.SelectionStart - 1 >= 0 &&
                    t.SelectionStart - 1 <= t.Text.Length)
                {
                    if (t.SelectionStart - 2 >= 0 &&
                        t.SelectionStart - 2 <= t.Text.Length)
                    {
                        if (t.Text[t.SelectionStart - 2] == '/' ||
                            t.Text[t.SelectionStart - 2] == ':' ||
                            t.Text[t.SelectionStart - 2] == ' ')
                        {
                            t.SelectionStart -= 1;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (t.SelectionStart == 0)
                {
                    return;
                }

                if (t.SelectionStart - 2 >= 0 &&
                    t.SelectionStart - 2 <= t.Text.Length)
                {
                    if (t.Text[t.SelectionStart - 2] == '/' ||
                        t.Text[t.SelectionStart - 2] == ':' ||
                        t.Text[t.SelectionStart - 2] == ' ')
                    {
                        t.Text = t.Text.Remove(t.SelectionStart - 2, 2);
                        RefazerMaskDataHoraSegundo(t, posicao, false);
                        t.SelectionStart = posicao - 2;

                        e.SuppressKeyPress = true;
                        return;
                    }
                }
                t.Text = t.Text.Remove(t.SelectionStart - 1, 1);
                RefazerMaskDataHoraSegundo(t, posicao, false);
                posicao = posicao - 1;
                t.SelectionStart = posicao;

                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (t.SelectionStart == t.Text.Length)
                {
                    return;
                }

                if (t.Text[t.SelectionStart] == '/' ||
                    t.Text[t.SelectionStart] == ':' ||
                    t.Text[t.SelectionStart] == ' ')
                {
                    t.Text = t.Text.Remove(t.SelectionStart + 1, 1);
                }
                else
                {
                    t.Text = t.Text.Remove(t.SelectionStart, 1);
                }

                RefazerMaskDataHoraSegundo(t, posicao, false);
                t.SelectionStart = posicao;
                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Home ||
                     e.KeyCode == Keys.End)
            {
                return;
            }
            else if (t.Text.Length >= 19)
            {
                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.NumPad0 ||
                     e.KeyCode == Keys.NumPad1 ||
                     e.KeyCode == Keys.NumPad2 ||
                     e.KeyCode == Keys.NumPad3 ||
                     e.KeyCode == Keys.NumPad4 ||
                     e.KeyCode == Keys.NumPad5 ||
                     e.KeyCode == Keys.NumPad6 ||
                     e.KeyCode == Keys.NumPad7 ||
                     e.KeyCode == Keys.NumPad8 ||
                     e.KeyCode == Keys.NumPad9 ||
                     e.KeyCode == Keys.D0 ||
                     e.KeyCode == Keys.D1 ||
                     e.KeyCode == Keys.D2 ||
                     e.KeyCode == Keys.D3 ||
                     e.KeyCode == Keys.D4 ||
                     e.KeyCode == Keys.D5 ||
                     e.KeyCode == Keys.D6 ||
                     e.KeyCode == Keys.D7 ||
                     e.KeyCode == Keys.D8 ||
                     e.KeyCode == Keys.D9)
            {
                t.Text = t.Text.Insert(posicao, ((Char)e.KeyCode).ToString());
                if (posicao + 1 == t.Text.Length) posicao += RefazerMaskDataHoraSegundo(t, posicao, true);
                else RefazerMaskDataHoraSegundo(t, posicao, true);
                t.SelectionStart = posicao + 1;

                if (t.Text[t.SelectionStart - 1] == '/' ||
                    t.Text[t.SelectionStart - 1] == ':' ||
                    t.Text[t.SelectionStart - 1] == ' ')
                {
                    t.SelectionStart++;
                }

                e.SuppressKeyPress = true;
            }
            else
            {
                e.SuppressKeyPress = true;
                return;
            }
        }

        private static Int32 RefazerMaskDataHoraSegundo(TextBox t, Int32 posicao, Boolean maiorigual)
        {
            Int32 retorno = 0;
            t.Text = t.Text.Replace("/", "").Replace(":", "").Replace(" ", "");

            for (Int32 x = 0; x < t.Text.Length; x++)
            {
                if (x + 2 > t.Text.Length) break;

                if (x == 1 || x == 4)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, "/");
                }
                else if (x == 9)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, " ");
                }
                else if (x == 12 || x == 15)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, ":");
                }
            }

            return retorno;
        }

        /// <summary>
        /// Máscara para campo de Data - Hora [dd/MM/yyyy HH:mm].
        /// Pressionando 'Enter' ou 'Tecla para Baixo' seleciona o próximo controle. 'Tecla para Cima' seleciona o controle anterior.
        /// </summary>
        /// <param name="t">Controle que ativou o Evento.</param>
        /// <param name="e">Controle de Evento.</param>
        public static void ControlKeyDownDataHora(TextBox t, KeyEventArgs e)
        {
            if (t.ReadOnly == true)
            {
                return;
            }

            Int32 posicao = 0;
            String letra = ((Char)e.KeyCode).ToString();

            if (e.KeyCode == Keys.NumPad0) { letra = "0"; }
            else if (e.KeyCode == Keys.NumPad1) { letra = "1"; }
            else if (e.KeyCode == Keys.NumPad2) { letra = "2"; }
            else if (e.KeyCode == Keys.NumPad3) { letra = "3"; }
            else if (e.KeyCode == Keys.NumPad4) { letra = "4"; }
            else if (e.KeyCode == Keys.NumPad5) { letra = "5"; }
            else if (e.KeyCode == Keys.NumPad6) { letra = "6"; }
            else if (e.KeyCode == Keys.NumPad7) { letra = "7"; }
            else if (e.KeyCode == Keys.NumPad8) { letra = "8"; }
            else if (e.KeyCode == Keys.NumPad9) { letra = "9"; }

            if (letra == "0" ||
                letra == "1" ||
                letra == "2" ||
                letra == "3" ||
                letra == "4" ||
                letra == "5" ||
                letra == "6" ||
                letra == "7" ||
                letra == "8" ||
                letra == "9")
            {
                posicao = t.SelectionStart;
                t.Text = t.Text.Remove(t.SelectionStart, t.SelectionLength);
            }
            else
            {
                posicao = t.SelectionStart;
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SendKeys.Send("+{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (t.SelectionStart + 1 <= t.Text.Length)
                {
                    if (t.Text[t.SelectionStart] == '/' ||
                        t.Text[t.SelectionStart] == ':' ||
                        t.Text[t.SelectionStart] == ' ')
                    {
                        if (t.SelectionStart + 2 <= t.Text.Length)
                        {
                            t.SelectionStart += 1;
                        }
                        else
                        {
                            e.SuppressKeyPress = true;
                            return;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (t.SelectionStart - 1 >= 0 &&
                    t.SelectionStart - 1 <= t.Text.Length)
                {
                    if (t.SelectionStart - 2 >= 0 &&
                        t.SelectionStart - 2 <= t.Text.Length)
                    {
                        if (t.Text[t.SelectionStart - 2] == '/' ||
                            t.Text[t.SelectionStart - 2] == ':' ||
                            t.Text[t.SelectionStart - 2] == ' ')
                        {
                            t.SelectionStart -= 1;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (t.SelectionStart == 0)
                {
                    return;
                }

                if (t.SelectionStart - 2 >= 0 &&
                    t.SelectionStart - 2 <= t.Text.Length)
                {
                    if (t.Text[t.SelectionStart - 2] == '/' ||
                        t.Text[t.SelectionStart - 2] == ':' ||
                        t.Text[t.SelectionStart - 2] == ' ')
                    {
                        t.Text = t.Text.Remove(t.SelectionStart - 2, 2);
                        RefazerMaskDataHora(t, posicao, false);
                        t.SelectionStart = posicao - 2;

                        e.SuppressKeyPress = true;
                        return;
                    }
                }
                t.Text = t.Text.Remove(t.SelectionStart - 1, 1);
                RefazerMaskDataHora(t, posicao, false);
                posicao = posicao - 1;
                t.SelectionStart = posicao;

                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (t.SelectionStart == t.Text.Length)
                {
                    return;
                }

                if (t.Text[t.SelectionStart] == '/' ||
                    t.Text[t.SelectionStart] == ':' ||
                    t.Text[t.SelectionStart] == ' ')
                {
                    t.Text = t.Text.Remove(t.SelectionStart + 1, 1);
                }
                else
                {
                    t.Text = t.Text.Remove(t.SelectionStart, 1);
                }

                RefazerMaskDataHora(t, posicao, false);
                t.SelectionStart = posicao;
                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Home ||
                     e.KeyCode == Keys.End)
            {
                return;
            }
            else if (t.Text.Length >= 16)
            {
                e.SuppressKeyPress = true;
                return;
            }
            else if (letra == "0" ||
                    letra == "1" ||
                    letra == "2" ||
                    letra == "3" ||
                    letra == "4" ||
                    letra == "5" ||
                    letra == "6" ||
                    letra == "7" ||
                    letra == "8" ||
                    letra == "9")
            {
                t.Text = t.Text.Insert(posicao, letra);
                if (posicao + 1 == t.Text.Length) posicao += RefazerMaskDataHora(t, posicao, true);
                else RefazerMaskDataHora(t, posicao, true);
                t.SelectionStart = posicao + 1;

                if (t.Text[t.SelectionStart - 1] == '/' ||
                    t.Text[t.SelectionStart - 1] == ':' ||
                    t.Text[t.SelectionStart - 1] == ' ')
                {
                    t.SelectionStart++;
                }

                e.SuppressKeyPress = true;
            }
            else
            {
                e.SuppressKeyPress = true;
                return;
            }
        }

        private static Int32 RefazerMaskDataHora(TextBox t, Int32 posicao, Boolean maiorigual)
        {
            Int32 retorno = 0;
            t.Text = t.Text.Replace("/", "").Replace(":", "").Replace(" ", "");

            for (Int32 x = 0; x < t.Text.Length; x++)
            {
                if (x + 2 > t.Text.Length) break;

                if (x == 1 || x == 4)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, "/");
                }
                else if (x == 9)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, " ");
                }
                else if (x == 12)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, ":");
                }
            }

            return retorno;
        }

        /// <summary>
        /// Máscara para campo de Data [dd/MM/yyyy].
        /// Pressionando 'Enter' ou 'Tecla para Baixo' seleciona o próximo controle. 'Tecla para Cima' seleciona o controle anterior.
        /// </summary>
        /// <param name="t">Controle que ativou o Evento.</param>
        /// <param name="e">Controle de Evento.</param>
        public static void ControlKeyDownData(TextBox t, KeyEventArgs e)
        {
            if (t.ReadOnly == true)
            {
                return;
            }

            Int32 posicao = 0;
            String letra = ((Char)e.KeyCode).ToString();

            if (e.KeyCode == Keys.NumPad0) { letra = "0"; }
            else if (e.KeyCode == Keys.NumPad1) { letra = "1"; }
            else if (e.KeyCode == Keys.NumPad2) { letra = "2"; }
            else if (e.KeyCode == Keys.NumPad3) { letra = "3"; }
            else if (e.KeyCode == Keys.NumPad4) { letra = "4"; }
            else if (e.KeyCode == Keys.NumPad5) { letra = "5"; }
            else if (e.KeyCode == Keys.NumPad6) { letra = "6"; }
            else if (e.KeyCode == Keys.NumPad7) { letra = "7"; }
            else if (e.KeyCode == Keys.NumPad8) { letra = "8"; }
            else if (e.KeyCode == Keys.NumPad9) { letra = "9"; }

            if (letra == "0" ||
                letra == "1" ||
                letra == "2" ||
                letra == "3" ||
                letra == "4" ||
                letra == "5" ||
                letra == "6" ||
                letra == "7" ||
                letra == "8" ||
                letra == "9")
            {
                posicao = t.SelectionStart;
                t.Text = t.Text.Remove(t.SelectionStart, t.SelectionLength);
            }
            else
            {
                posicao = t.SelectionStart;
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SendKeys.Send("+{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (t.SelectionStart + 1 <= t.Text.Length)
                {
                    if (t.Text[t.SelectionStart] == '/')
                    {
                        if (t.SelectionStart + 2 <= t.Text.Length)
                        {
                            t.SelectionStart += 1;
                        }
                        else
                        {
                            e.SuppressKeyPress = true;
                            return;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (t.SelectionStart - 1 >= 0 &&
                    t.SelectionStart - 1 <= t.Text.Length)
                {
                    if (t.SelectionStart - 2 >= 0 &&
                        t.SelectionStart - 2 <= t.Text.Length)
                    {
                        if (t.Text[t.SelectionStart - 2] == '/')
                        {
                            t.SelectionStart -= 1;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (t.SelectionStart == 0)
                {
                    return;
                }

                if (t.SelectionStart - 2 >= 0 &&
                    t.SelectionStart - 2 <= t.Text.Length)
                {
                    if (t.Text[t.SelectionStart - 2] == '/')
                    {
                        t.Text = t.Text.Remove(t.SelectionStart - 2, 2);
                        RefazerMaskData(t, posicao, false);
                        t.SelectionStart = posicao - 2;

                        e.SuppressKeyPress = true;
                        return;
                    }
                }
                t.Text = t.Text.Remove(t.SelectionStart - 1, 1);
                RefazerMaskData(t, posicao, false);
                posicao = posicao - 1;
                t.SelectionStart = posicao;

                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (t.SelectionStart == t.Text.Length)
                {
                    return;
                }

                if (t.Text[t.SelectionStart] == '/')
                {
                    t.Text = t.Text.Remove(t.SelectionStart + 1, 1);
                }
                else
                {
                    t.Text = t.Text.Remove(t.SelectionStart, 1);
                }

                RefazerMaskData(t, posicao, false);
                t.SelectionStart = posicao;
                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Home ||
                     e.KeyCode == Keys.End)
            {
                return;
            }
            else if (t.Text.Length >= 10)
            {
                e.SuppressKeyPress = true;
                return;
            }
            else if (letra == "0" ||
                    letra == "1" ||
                    letra == "2" ||
                    letra == "3" ||
                    letra == "4" ||
                    letra == "5" ||
                    letra == "6" ||
                    letra == "7" ||
                    letra == "8" ||
                    letra == "9")
            {
                t.Text = t.Text.Insert(posicao, letra);
                if (posicao + 1 == t.Text.Length) posicao += RefazerMaskData(t, posicao, true);
                else RefazerMaskData(t, posicao, true);
                t.SelectionStart = posicao + 1;

                if (t.Text[t.SelectionStart - 1] == '/')
                {
                    t.SelectionStart++;
                }

                e.SuppressKeyPress = true;
            }
            else
            {
                e.SuppressKeyPress = true;
                return;
            }
        }

        public static void ControlKeyDownHora(TextBox t, KeyEventArgs e)
        {
            if (t.ReadOnly == true)
            {
                return;
            }

            Int32 posicao = 0;
            String letra = ((Char)e.KeyCode).ToString();

            if (e.KeyCode == Keys.NumPad0) { letra = "0"; }
            else if (e.KeyCode == Keys.NumPad1) { letra = "1"; }
            else if (e.KeyCode == Keys.NumPad2) { letra = "2"; }
            else if (e.KeyCode == Keys.NumPad3) { letra = "3"; }
            else if (e.KeyCode == Keys.NumPad4) { letra = "4"; }
            else if (e.KeyCode == Keys.NumPad5) { letra = "5"; }
            else if (e.KeyCode == Keys.NumPad6) { letra = "6"; }
            else if (e.KeyCode == Keys.NumPad7) { letra = "7"; }
            else if (e.KeyCode == Keys.NumPad8) { letra = "8"; }
            else if (e.KeyCode == Keys.NumPad9) { letra = "9"; }

            if (letra == "0" ||
                letra == "1" ||
                letra == "2" ||
                letra == "3" ||
                letra == "4" ||
                letra == "5" ||
                letra == "6" ||
                letra == "7" ||
                letra == "8" ||
                letra == "9")
            {
                posicao = t.SelectionStart;
                t.Text = t.Text.Remove(t.SelectionStart, t.SelectionLength);
            }
            else
            {
                posicao = t.SelectionStart;
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SendKeys.Send("+{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (t.SelectionStart + 1 <= t.Text.Length)
                {
                    if (t.Text[t.SelectionStart] == ':')
                    {
                        if (t.SelectionStart + 2 <= t.Text.Length)
                        {
                            t.SelectionStart += 1;
                        }
                        else
                        {
                            e.SuppressKeyPress = true;
                            return;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (t.SelectionStart - 1 >= 0 &&
                    t.SelectionStart - 1 <= t.Text.Length)
                {
                    if (t.SelectionStart - 2 >= 0 &&
                        t.SelectionStart - 2 <= t.Text.Length)
                    {
                        if (t.Text[t.SelectionStart - 2] == ':')
                        {
                            t.SelectionStart -= 1;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (t.SelectionStart == 0)
                {
                    return;
                }

                if (t.SelectionStart - 2 >= 0 &&
                    t.SelectionStart - 2 <= t.Text.Length)
                {
                    if (t.Text[t.SelectionStart - 2] == ':')
                    {
                        t.Text = t.Text.Remove(t.SelectionStart - 2, 2);
                        RefazerMaskHora(t, posicao, false);
                        t.SelectionStart = posicao - 2;

                        e.SuppressKeyPress = true;
                        return;
                    }
                }
                t.Text = t.Text.Remove(t.SelectionStart - 1, 1);
                RefazerMaskHora(t, posicao, false);
                posicao = posicao - 1;
                t.SelectionStart = posicao;

                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (t.SelectionStart == t.Text.Length)
                {
                    return;
                }

                if (t.Text[t.SelectionStart] == ':')
                {
                    t.Text = t.Text.Remove(t.SelectionStart + 1, 1);
                }
                else
                {
                    t.Text = t.Text.Remove(t.SelectionStart, 1);
                }

                RefazerMaskHora(t, posicao, false);
                t.SelectionStart = posicao;
                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Home ||
                     e.KeyCode == Keys.End)
            {
                return;
            }
            else if (t.Text.Length >= 5)
            {
                e.SuppressKeyPress = true;
                return;
            }
            else if (letra == "0" ||
                    letra == "1" ||
                    letra == "2" ||
                    letra == "3" ||
                    letra == "4" ||
                    letra == "5" ||
                    letra == "6" ||
                    letra == "7" ||
                    letra == "8" ||
                    letra == "9")
            {
                if (posicao == 0)
                {
                    if (letra != "0" &&
                        letra != "1" &&
                        letra != "2")
                    {
                        e.SuppressKeyPress = true;
                        return;
                    }
                }
                else if (posicao == 1)
                {
                    if (t.Text.Substring(0, 1) == "2")
                    {
                        if (letra != "0" &&
                            letra != "1" &&
                            letra != "2" &&
                            letra != "3")
                        {
                            e.SuppressKeyPress = true;
                            return;
                        }
                    }
                }
                else if (posicao == 2 || posicao == 3)
                {
                    if (letra == "6" ||
                        letra == "7" ||
                        letra == "8" ||
                        letra == "9")
                    {
                        e.SuppressKeyPress = true;
                        return;
                    }
                }

                t.Text = t.Text.Insert(posicao, letra);
                if (posicao + 1 == t.Text.Length) posicao += RefazerMaskHora(t, posicao, true);
                else RefazerMaskHora(t, posicao, true);
                t.SelectionStart = posicao + 1;

                if (t.Text[t.SelectionStart - 1] == ':')
                {
                    t.SelectionStart++;
                }

                e.SuppressKeyPress = true;
            }
            else
            {
                e.SuppressKeyPress = true;
                return;
            }
        }

        private static Int32 RefazerMaskData(TextBox t, Int32 posicao, Boolean maiorigual)
        {
            Int32 retorno = 0;
            t.Text = t.Text.Replace("/", "");

            for (Int32 x = 0; x < t.Text.Length; x++)
            {
                if (x + 2 > t.Text.Length) break;

                if (x == 1 || x == 4)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, "/");
                }
            }

            return retorno;
        }

        private static Int32 RefazerMaskHora(TextBox t, Int32 posicao, Boolean maiorigual)
        {
            Int32 retorno = 0;
            t.Text = t.Text.Replace(":", "");

            for (Int32 x = 0; x < t.Text.Length; x++)
            {
                if (x + 2 > t.Text.Length) break;

                if (x == 1)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, ":");
                }
            }

            return retorno;
        }

        /// <summary>
        /// Máscara para campo de Numérico.
        /// Pressionando 'Enter' ou 'Tecla para Baixo' seleciona o próximo controle. 'Tecla para Cima' seleciona o controle anterior.
        /// </summary>
        /// <param name="t">Controle que ativou o Evento.</param>
        /// <param name="e">Controle de Evento.</param>
        public static void ControlKeyDownNumero(TextBox t, KeyEventArgs e)
        {
            if (t.ReadOnly == true)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SendKeys.Send("+{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Right ||
                     e.KeyCode == Keys.Left ||
                     e.KeyCode == Keys.Back ||
                     e.KeyCode == Keys.Delete ||
                     e.KeyCode == Keys.Home ||
                     e.KeyCode == Keys.End ||
                     e.KeyCode == Keys.NumPad0 ||
                     e.KeyCode == Keys.NumPad1 ||
                     e.KeyCode == Keys.NumPad2 ||
                     e.KeyCode == Keys.NumPad3 ||
                     e.KeyCode == Keys.NumPad4 ||
                     e.KeyCode == Keys.NumPad5 ||
                     e.KeyCode == Keys.NumPad6 ||
                     e.KeyCode == Keys.NumPad7 ||
                     e.KeyCode == Keys.NumPad8 ||
                     e.KeyCode == Keys.NumPad9 ||
                     e.KeyCode == Keys.D0 ||
                     e.KeyCode == Keys.D1 ||
                     e.KeyCode == Keys.D2 ||
                     e.KeyCode == Keys.D3 ||
                     e.KeyCode == Keys.D4 ||
                     e.KeyCode == Keys.D5 ||
                     e.KeyCode == Keys.D6 ||
                     e.KeyCode == Keys.D7 ||
                     e.KeyCode == Keys.D8 ||
                     e.KeyCode == Keys.D9)
            {
                return;
            }
            else
            {
                e.SuppressKeyPress = true;
                return;
            }
        }

        /// <summary>
        /// Máscara para campo de CEP.
        /// Pressionando 'Enter' ou 'Tecla para Baixo' seleciona o próximo controle. 'Tecla para Cima' seleciona o controle anterior.
        /// </summary>
        /// <param name="t">Controle que ativou o Evento.</param>
        /// <param name="e">Controle de Evento.</param>
        public static void ControlKeyDownCep(TextBox t, KeyEventArgs e)
        {
            if (t.ReadOnly == true)
            {
                return;
            }

            Int32 posicao = 0;
            String letra = ((Char)e.KeyCode).ToString();

            if (e.KeyCode == Keys.NumPad0) { letra = "0"; }
            else if (e.KeyCode == Keys.NumPad1) { letra = "1"; }
            else if (e.KeyCode == Keys.NumPad2) { letra = "2"; }
            else if (e.KeyCode == Keys.NumPad3) { letra = "3"; }
            else if (e.KeyCode == Keys.NumPad4) { letra = "4"; }
            else if (e.KeyCode == Keys.NumPad5) { letra = "5"; }
            else if (e.KeyCode == Keys.NumPad6) { letra = "6"; }
            else if (e.KeyCode == Keys.NumPad7) { letra = "7"; }
            else if (e.KeyCode == Keys.NumPad8) { letra = "8"; }
            else if (e.KeyCode == Keys.NumPad9) { letra = "9"; }

            if (letra == "0" ||
                letra == "1" ||
                letra == "2" ||
                letra == "3" ||
                letra == "4" ||
                letra == "5" ||
                letra == "6" ||
                letra == "7" ||
                letra == "8" ||
                letra == "9")
            {
                posicao = t.SelectionStart;
                t.Text = t.Text.Remove(t.SelectionStart, t.SelectionLength);
            }
            else
            {
                posicao = t.SelectionStart;
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SendKeys.Send("+{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (t.SelectionStart + 1 <= t.Text.Length)
                {
                    if (t.Text[t.SelectionStart] == '-')
                    {
                        if (t.SelectionStart + 2 <= t.Text.Length)
                        {
                            t.SelectionStart += 1;
                        }
                        else
                        {
                            e.SuppressKeyPress = true;
                            return;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (t.SelectionStart - 1 >= 0 &&
                    t.SelectionStart - 1 <= t.Text.Length)
                {
                    if (t.SelectionStart - 2 >= 0 &&
                        t.SelectionStart - 2 <= t.Text.Length)
                    {
                        if (t.Text[t.SelectionStart - 2] == '/')
                        {
                            t.SelectionStart -= 1;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (t.SelectionStart == 0)
                {
                    return;
                }

                t.Text = t.Text.Remove(t.SelectionStart - 1, 1);
                RefazerMaskCep(t, posicao, false);
                posicao = posicao - 1;
                t.SelectionStart = posicao;

                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (t.SelectionStart == t.Text.Length)
                {
                    return;
                }

                if (t.Text[t.SelectionStart] == '-')
                {
                    t.Text = t.Text.Remove(t.SelectionStart + 1, 1);
                }
                else
                {
                    t.Text = t.Text.Remove(t.SelectionStart, 1);
                }

                RefazerMaskCep(t, posicao, false);
                t.SelectionStart = posicao;
                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Home ||
                     e.KeyCode == Keys.End)
            {
                return;
            }
            else if (t.Text.Length >= 9)
            {
                e.SuppressKeyPress = true;
                return;
            }
            else if (letra == "0" ||
                    letra == "1" ||
                    letra == "2" ||
                    letra == "3" ||
                    letra == "4" ||
                    letra == "5" ||
                    letra == "6" ||
                    letra == "7" ||
                    letra == "8" ||
                    letra == "9")
            {
                t.Text = t.Text.Insert(posicao, letra);
                if (posicao + 1 == t.Text.Length) posicao += RefazerMaskCep(t, posicao, true);
                else RefazerMaskCep(t, posicao, true);
                t.SelectionStart = posicao + 1;

                if (t.Text[t.SelectionStart - 1] == '-')
                {
                    t.SelectionStart++;
                }

                e.SuppressKeyPress = true;
            }
            else
            {
                e.SuppressKeyPress = true;
                return;
            }
        }

        private static Int32 RefazerMaskCep(TextBox t, Int32 posicao, Boolean maiorigual)
        {
            Int32 retorno = 0;
            t.Text = t.Text.Replace("-", "");

            for (Int32 x = 0; x < t.Text.Length; x++)
            {
                if (x + 2 > t.Text.Length) break;

                if (x == 4)
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x + 1, "-");
                }
            }

            return retorno;
        }

        /// <summary>
        /// Máscara para campo de IE.
        /// Pressionando 'Enter' ou 'Tecla para Baixo' seleciona o próximo controle. 'Tecla para Cima' seleciona o controle anterior.
        /// </summary>
        /// <param name="t">Controle que ativou o Evento.</param>
        /// <param name="e">Controle de Evento.</param>
        public static void ControlKeyDownIE(TextBox t, KeyEventArgs e, String UF)
        {
            if (t.ReadOnly == true)
            {
                return;
            }

            //'AC' = '**.***.***/***-**';
            //'AL' = '*********';
            //'AP' = '*********';
            //'AM' = '**.***.***-*';
            //'BA' = '******-**';
            //'CE' = '********-*';
            //'DF' = '***********-**';
            //'ES' = '*********';
            //'GO' = '**.***.***-*';
            //'MA' = '*********';
            //'muito' = '**********-*';
            //'MS' = '*********';
            //'MG' = '***.***.***/****';
            //'pra' = '**-******-*';
            //'PB' = '********-*';
            //'PR' = '********-**';
            //'PE' = '**.*.***.*******-*';
            //'PI' = '*********';
            //'RJ' = '**.***.**-*';
            //'RN' = '**.***.***-*';
            //'RS' = '***/*******';
            //'RO' = '***.*****-*';
            //'RR' = '********-*';
            //'SC' = '***.***.***';
            //'SP' = '***.***.***.***';
            //'SE' = '*********-*';
            //'TO' = '***********';

            Hashtable hsMascaras = new Hashtable();
            hsMascaras.Add("AC", "00.000.000/000-00");
            hsMascaras.Add("AL", "000000000");
            hsMascaras.Add("AP", "000000000");
            hsMascaras.Add("AM", "00.000.000-0");
            hsMascaras.Add("BA", "000000-00");
            hsMascaras.Add("CE", "00000000-0");
            hsMascaras.Add("DF", "00000000000-00");
            hsMascaras.Add("ES", "000000000");
            hsMascaras.Add("GO", "00.000.000-0");
            hsMascaras.Add("MA", "000000000");
            hsMascaras.Add("MS", "000000000");
            hsMascaras.Add("MG", "000.000.000/0000");
            hsMascaras.Add("PB", "00000000-0");
            hsMascaras.Add("PR", "00000000-00");
            hsMascaras.Add("PE", "00.0.000.0000000-0");
            hsMascaras.Add("PI", "000000000");
            hsMascaras.Add("RJ", "00.000.00-0");
            hsMascaras.Add("RN", "00.000.000-0");
            hsMascaras.Add("RS", "000/0000000");
            hsMascaras.Add("RO", "000.00000-0");
            hsMascaras.Add("RR", "00000000-0");
            hsMascaras.Add("SC", "000.000.000");
            hsMascaras.Add("SP", "000.000.000.000");
            hsMascaras.Add("SE", "000000000-0");
            hsMascaras.Add("TO", "00000000000");
            //hsMascaras.Add("DEFAULT", "000.000.000.000");

            ArrayList colecaoPontos = new ArrayList();
            colecaoPontos.Add(".");
            colecaoPontos.Add("-");
            colecaoPontos.Add("/");

            Int32 posicao = 0;
            String letra = ((Char)e.KeyCode).ToString();

            if (e.KeyCode == Keys.NumPad0) { letra = "0"; }
            else if (e.KeyCode == Keys.NumPad1) { letra = "1"; }
            else if (e.KeyCode == Keys.NumPad2) { letra = "2"; }
            else if (e.KeyCode == Keys.NumPad3) { letra = "3"; }
            else if (e.KeyCode == Keys.NumPad4) { letra = "4"; }
            else if (e.KeyCode == Keys.NumPad5) { letra = "5"; }
            else if (e.KeyCode == Keys.NumPad6) { letra = "6"; }
            else if (e.KeyCode == Keys.NumPad7) { letra = "7"; }
            else if (e.KeyCode == Keys.NumPad8) { letra = "8"; }
            else if (e.KeyCode == Keys.NumPad9) { letra = "9"; }
            else if (e.KeyCode == Keys.I)
            {
                t.Text = "ISENTO";
                e.SuppressKeyPress = true;
                return;
            }

            if (letra == "0" ||
                letra == "1" ||
                letra == "2" ||
                letra == "3" ||
                letra == "4" ||
                letra == "5" ||
                letra == "6" ||
                letra == "7" ||
                letra == "8" ||
                letra == "9")
            {
                posicao = t.SelectionStart;
                t.Text = t.Text.Remove(t.SelectionStart, t.SelectionLength);
            }
            else
            {
                posicao = t.SelectionStart;
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                SendKeys.Send("+{Tab}");
                return;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (t.SelectionStart + 1 <= t.Text.Length)
                {
                    if (colecaoPontos.Contains(t.Text[t.SelectionStart]) == true)
                    {
                        if (t.SelectionStart + 2 <= t.Text.Length)
                        {
                            t.SelectionStart += 1;
                        }
                        else
                        {
                            e.SuppressKeyPress = true;
                            return;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (t.SelectionStart - 1 >= 0 &&
                    t.SelectionStart - 1 <= t.Text.Length)
                {
                    if (t.SelectionStart - 2 >= 0 &&
                        t.SelectionStart - 2 <= t.Text.Length)
                    {
                        if (colecaoPontos.Contains(t.Text[t.SelectionStart - 2]) == true)
                        {
                            t.SelectionStart -= 1;
                        }
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (t.Text == "ISENTO")
                {
                    t.Text = "";
                    return;
                }

                if (t.SelectionStart == 0)
                {
                    return;
                }

                if (t.SelectionStart - 2 >= 0 &&
                    t.SelectionStart - 2 <= t.Text.Length)
                {
                    if (colecaoPontos.Contains(t.Text[t.SelectionStart - 2]) == true)
                    {
                        t.Text = t.Text.Remove(t.SelectionStart - 2, 2);
                        RefazerMaskIE(t, posicao, false, UF);
                        t.SelectionStart = posicao - 2;

                        e.SuppressKeyPress = true;
                        return;
                    }
                }
                t.Text = t.Text.Remove(t.SelectionStart - 1, 1);
                RefazerMaskIE(t, posicao, false, UF);
                posicao = posicao - 1;
                t.SelectionStart = posicao;

                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (t.SelectionStart == t.Text.Length)
                {
                    return;
                }

                if (t.Text == "ISENTO")
                {
                    t.Text = "";
                    return;
                }

                if (colecaoPontos.Contains(t.Text[t.SelectionStart]) == true)
                {
                    t.Text = t.Text.Remove(t.SelectionStart + 1, 1);
                }
                else
                {
                    t.Text = t.Text.Remove(t.SelectionStart, 1);
                }

                RefazerMaskIE(t, posicao, false, UF);
                t.SelectionStart = posicao;
                e.SuppressKeyPress = true;
                return;
            }
            else if (e.KeyCode == Keys.Home ||
                     e.KeyCode == Keys.End)
            {
                return;
            }
            else if (t.Text.Length >= hsMascaras[UF].ToString().Length)
            {
                e.SuppressKeyPress = true;
                return;
            }
            else if (letra == "0" ||
                    letra == "1" ||
                    letra == "2" ||
                    letra == "3" ||
                    letra == "4" ||
                    letra == "5" ||
                    letra == "6" ||
                    letra == "7" ||
                    letra == "8" ||
                    letra == "9")
            {
                t.Text = t.Text.Insert(posicao, letra);
                if (posicao + 1 == t.Text.Length) posicao += RefazerMaskIE(t, posicao, true, UF);
                else RefazerMaskIE(t, posicao, true, UF);
                t.SelectionStart = posicao + 1;

                if (colecaoPontos.Contains(t.Text[t.SelectionStart - 1]) == true)
                {
                    t.SelectionStart++;
                }

                e.SuppressKeyPress = true;
            }
            else
            {
                e.SuppressKeyPress = true;
                return;
            }
        }

        public static Int32 RefazerMaskIE(TextBox t, Int32 posicao, Boolean maiorigual, String UF)
        {
            Int32 retorno = 0;
            t.Text = t.Text.Replace(".", "").Replace("/", "").Replace("-", "");

            if (t.Text.Length > 0 && t.Text.ToUpper().Substring(0, 1) == "I")
            {
                return retorno;
            }

            Hashtable hsMascaras = new Hashtable();
            hsMascaras.Add("AC", "00.000.000/000-00");
            hsMascaras.Add("AL", "000000000");
            hsMascaras.Add("AP", "000000000");
            hsMascaras.Add("AM", "00.000.000-0");
            hsMascaras.Add("BA", "000000-00");
            hsMascaras.Add("CE", "00000000-0");
            hsMascaras.Add("DF", "00000000000-00");
            hsMascaras.Add("ES", "000000000");
            hsMascaras.Add("GO", "00.000.000-0");
            hsMascaras.Add("MA", "000000000");
            hsMascaras.Add("MS", "000000000");
            hsMascaras.Add("MG", "000.000.000/0000");
            hsMascaras.Add("PB", "00000000-0");
            hsMascaras.Add("PR", "00000000-00");
            hsMascaras.Add("PE", "00.0.000.0000000-0");
            hsMascaras.Add("PI", "000000000");
            hsMascaras.Add("RJ", "00.000.00-0");
            hsMascaras.Add("RN", "00.000.000-0");
            hsMascaras.Add("RS", "000/0000000");
            hsMascaras.Add("RO", "000.00000-0");
            hsMascaras.Add("RR", "00000000-0");
            hsMascaras.Add("SC", "000.000.000");
            hsMascaras.Add("SP", "000.000.000.000");
            hsMascaras.Add("SE", "000000000-0");
            hsMascaras.Add("TO", "00000000000");
            hsMascaras.Add("MT", "000000000");
            //hsMascaras.Add("DEFAULT", "000.000.000.000");

            for (Int32 x = 0; x < t.Text.Length; x++)
            {
                if (x + 1 > t.Text.Length || x + 1 > hsMascaras[UF].ToString().Length)
                {
                    break;
                }

                if (hsMascaras[UF].ToString().Substring(x, 1) != "0")
                {
                    if (posicao >= x) retorno++;
                    t.Text = t.Text.Insert(x, hsMascaras[UF].ToString().Substring(x, 1));
                }
            }

            if (t.Text.Length > hsMascaras[UF].ToString().Length)
            {
                t.Text = t.Text.Substring(0, hsMascaras[UF].ToString().Length);
            }

            return retorno;
        }

        /// <summary>
        /// Converte uma String para Data e depois este de volta para String.
        /// </summary>
        /// <param name="s">String a ser processada.</param>
        /// <returns>Retorna uma Data no formato de String. Caso falhe retorna ''.</returns>
        public static String ExibeData(String s)
        {
            DateTime d;
            try { d = clsParser.SqlDateTimeParse(s).Value; } catch { return ""; }

            return d.ToString();
        }

        /// <summary>
        /// Converte um SqlDateTime para String.
        /// </summary>
        /// <param name="d">SqlDateTime a ser processado.</param>
        /// <returns>Retorna uma Data no formato de String. Caso falhe retorna ''.</returns>
        public static String ExibeData(SqlDateTime d)
        {
            if (d.IsNull)
                return "";

            return d.Value.ToString();
        }

        /// <summary>
        /// Converte uma String para Data e depois este de volta para String.
        /// </summary>
        /// <param name="s">String a ser processada.</param>
        /// <param name="formato">Formato da Data de retorno.</param>
        /// <returns>Retorna a Data em formato de String com o formato de retorno desejado. Caso falhe retorna ''.</returns>
        public static String ExibeData(String s, String formato)
        {
            DateTime d;

            try { d = clsParser.SqlDateTimeParse(s).Value; } catch { return ""; }

            return d.ToString(formato);
        }

        /// <summary>
        /// Converte um SqlDateTime para String.
        /// </summary>
        /// <param name="d">SqlDateTime a ser processado.</param>
        /// <param name="formato">Formato da Data de retorno.</param>
        /// <returns>Retorna a Data em formato de String com o formato desejado. Caso falhe retorna ''.</returns>
        public static String ExibeData(SqlDateTime d, String formato)
        {
            if (d.IsNull)
                return "";

            return d.Value.ToString(formato);
        }

        public static byte[] ImageToByteArray(Image img, System.Drawing.Imaging.ImageFormat imgF)
        {
            byte[] res;

            try
            {
                MemoryStream msm = new MemoryStream();
                img.Save(msm, imgF);
                res = msm.ToArray();

                return res;
            }
            catch
            {
                throw new Exception("Erro ao converter a imagem para array de bytes, favor verificar");
            }
        }

        public static Image ByteArrayToImage(byte[] b)
        {
            Image res;

            try
            {
                using (MemoryStream msm = new MemoryStream(b, 0, b.Length))
                {
                    Image newImage;

                    msm.Write(b, 0, b.Length);

                    newImage = Image.FromStream(msm, true);

                    res = (Image)newImage.Clone();
                    return res;
                }
            }
            catch
            {
                throw new Exception("Erro ao converter array de bytes para imagem, favor verificar");
            }
        }

        public static Int32 SelecionarIndex(String valor, Int32 nLetras, ComboBox c)
        {
            String s = "";
            Int32 index = 0;
            Int32 resultado = 0;

            foreach (Object obj in c.Items)
            {
                s = obj.ToString();

                if (nLetras == 0)
                {
                    if (s == valor)
                    {
                        resultado = index;
                        break;
                    }
                }
                else
                {
                    if (s.Substring(0, nLetras) == valor)
                    {
                        resultado = index;
                        break;
                    }
                }

                index++;
            }

            return resultado;
        }

        public static Int32 SelecionarIndex(String valor, Int32 nLetras, ListBox c)
        {
            String s = "";
            Int32 index = 0;
            Int32 resultado = 0;

            foreach (Object obj in c.Items)
            {
                s = obj.ToString();

                if (nLetras == 0)
                {
                    if (s == valor)
                    {
                        resultado = index;
                        break;
                    }
                }
                else
                {
                    if (s.Substring(0, nLetras) == valor)
                    {
                        resultado = index;
                        break;
                    }
                }

                index++;
            }

            return resultado;
        }

        /// <summary>
        /// Arredonda determinado valor Double sempre para cima.
        /// </summary>
        /// <param name="value">Valor a ser arredondado.</param>
        /// <param name="cs">Qtde de casas decimais desejada.</param>
        /// <returns>Retorna o Valor Double arredondado.</returns>
        public static double RoundUp(Double value, Int32 cs)
        {
            Double result = Math.Abs(value) - Math.Truncate(value);
            if (result != 0 && result < 0.500)
            {
                return Math.Round(Convert.ToDouble(value + 0.500), cs);
            }
            else if (Math.Abs(value) == 0.5)
            {
                value = value + 0.5;
            }

            return Math.Round(Convert.ToDouble(value), cs);
        }

        /// <summary>
        /// Arredonda determinado valor Decimal sempre para cima.
        /// </summary>
        /// <param name="value">Valor a ser arredondado.</param>
        /// <param name="cs">Qtde de casas decimais desejada.</param>
        /// <returns>Retorna o Valor Double arredondado.</returns>
        public static Decimal RoundUp(Decimal value, Int32 cs)
        {
            Decimal result = Math.Abs(value) - Math.Truncate(value);
            if (result != 0 && result < (Decimal)0.5)
            {
                return Math.Round(Convert.ToDecimal(value + (Decimal)0.500), cs);
            }
            else if (Math.Abs(value) == (Decimal)0.5)
            {
                value = value + (Decimal)0.5;
            }

            return Math.Round(Convert.ToDecimal(value), cs);
        }

        /// <summary>
        /// Arredonda determinado valor Double sempre para baixo.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cs"></param>
        /// <returns></returns>
        public static double RoundDown(Double value, Int32 cs)
        {
            if ((Math.Abs(value) - Math.Truncate(value)) > 0.5)
            {
                return Math.Round(Convert.ToDouble(value - 0.5), cs);
            }
            else if (Math.Abs(value) == 0.5)
            {
                value = value - 0.5;
            }

            return Math.Round(Convert.ToDouble(value), cs);
        }

        /// <summary>
        /// Arredonda determinado valor Decimal sempre para baixo.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cs"></param>
        /// <returns></returns>
        public static Decimal RoundDown(Decimal value, Int32 cs)
        {
            if ((Math.Abs(value) - Math.Truncate(value)) > (Decimal)0.500)
            {
                return Math.Round(Convert.ToDecimal(value - (Decimal)0.500), cs);
            }
            else if (Math.Abs(value) == (Decimal)0.5)
            {
                value = value - (Decimal)0.5;
            }

            return Math.Round(Convert.ToDecimal(value), cs);
        }

        /// <summary>
        /// Trunca determinado Double para o número de dígitos depois da vírgula desejado.
        /// </summary>
        /// <param name="value">Valor a ser truncado.</param>
        /// <param name="digits">Número de dígitos depois da vírgula que deve prevalecer.</param>
        /// <returns></returns>
        public static double Truncar(double value, int digits)
        {
            if (digits <= 0)
            {
                return Math.Truncate(value);
            }

            var numeroCalculado = int.Parse('1' + new String('0', digits));

            value *= numeroCalculado;

            value = Math.Truncate(value);

            value /= numeroCalculado;

            return value;
        }

        /// <summary>
        /// Trunca determinado Decimal para o número de dígitos depois da vírgula desejado.
        /// </summary>
        /// <param name="value">Valor a ser truncado.</param>
        /// <param name="digits">Número de dígitos depois da vírgula que deve prevalecer.</param>
        /// <returns></returns>
        public static decimal Truncar(decimal value, int digits)
        {
            if (digits <= 0)
            {
                return Math.Truncate(value);
            }

            var numeroCalculado = int.Parse('1' + new String('0', digits));

            value *= numeroCalculado;

            value = Math.Truncate(value);

            value /= numeroCalculado;

            return value;
        }

        public static void AutoCompletar(String conexao,
                                        String tabela,
                                        String campo,
                                        String where,
                                        TextBox t)
        {
            t.AutoCompleteCustomSource.Clear();

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            String sql;

            sql = "SELECT " + campo + " FROM " + tabela;

            t.AccessibleDescription = conexao + "|" + sql + "|" + campo;

            if ((where.Trim() == String.Empty) == false)
                sql += " WHERE " + where;
            sql += " ORDER BY " + campo;

            scn = new SqlConnection(conexao);
            scd = new SqlCommand(sql, scn);

            scn.Open();
            sdr = scd.ExecuteReader();

            while (sdr.Read())
                t.AutoCompleteCustomSource.Add(sdr[0].ToString());
            scn.Close();

            t.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            t.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public static void AutoCompletar(String conexao,
                                        String query,
                                        TextBox t)
        {
            t.AutoCompleteCustomSource.Clear();

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(conexao);
            scd = new SqlCommand(query, scn);

            scn.Open();
            sdr = scd.ExecuteReader();
            while (sdr.Read())
                t.AutoCompleteCustomSource.Add(sdr[0].ToString());
            scn.Close();

            t.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            t.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public static Int32 GetDiffDays(DateTime startDate, DateTime endDate)
        {
            Int32 days = 0;
            Int32 daysCount = 0;

            days = startDate.Subtract(endDate).Days;

            if (days < 0)
            {
                days = days * -1;
            }

            for (int i = 1; i <= days; i++)
            {
                startDate = startDate.AddDays(1);

                if (startDate.DayOfWeek == DayOfWeek.Sunday ||
                    startDate.DayOfWeek == DayOfWeek.Saturday ||
                    IsHoliday(startDate) == true)
                {
                    daysCount++;
                }
            }

            return daysCount;
        }

        public static Boolean IsHoliday(DateTime d)
        {
            Boolean result = false;

            String query;

            SqlConnection scn;
            SqlCommand scd;
            SqlDataReader sdr;

            scn = new SqlConnection(clsInfo.conexaosqldados);

            query = "select " +
                        "count(*) " +
                    "from " +
                        "feriados " +
                    "where " +
                        "day(@data) = day(data) and " +
                        "month(@data) = month(data) and " +
                        "year(@data) = year(data) ";

            scd = new SqlCommand(query, scn);
            scd.Parameters.Add("@data", System.Data.SqlDbType.DateTime).Value = d;

            scn.Open();
            sdr = scd.ExecuteReader();

            if (sdr.Read() == true)
            {
                if (clsParser.Int32Parse(sdr[0].ToString()) > 0)
                {
                    result = true;
                }
            }

            scn.Close();

            return result;
        }

        /// <summary>
        /// Formata o valor do campo númerico de acordo com o número de casas decimais definido na propriedades 'Tag'.
        /// </summary>
        /// <param name="_ctrl"></param>
        public static void FormatarCampoNumerico(object _ctrl)
        {
            if (_ctrl is TextBox)
            {
                if (((TextBox)_ctrl).Tag != null && ((TextBox)_ctrl).Tag.ToString() != "")
                {
                    if (((TextBox)_ctrl).Tag.ToString().Substring(0, 1) == "N")
                    {
                        try
                        {
                            Int32 i = Convert.ToInt32(Math.Ceiling(Decimal.Parse(((TextBox)_ctrl).Text)));
                        }
                        catch
                        {
                            ((TextBox)_ctrl).Text = "0";
                        }
                        //
                        if (((TextBox)_ctrl).Tag.ToString() == "N6")
                        {
                            ((TextBox)_ctrl).Text = Decimal.Parse(((TextBox)_ctrl).Text).ToString("N6");
                        }
                        if (((TextBox)_ctrl).Tag.ToString() == "N5")
                        {
                            ((TextBox)_ctrl).Text = Decimal.Parse(((TextBox)_ctrl).Text).ToString("N5");
                        }
                        if (((TextBox)_ctrl).Tag.ToString() == "N4")
                        {
                            ((TextBox)_ctrl).Text = Decimal.Parse(((TextBox)_ctrl).Text).ToString("N4");
                        }
                        else if (((TextBox)_ctrl).Tag.ToString() == "N3")
                        {
                            ((TextBox)_ctrl).Text = Decimal.Parse(((TextBox)_ctrl).Text).ToString("N3");
                        }
                        else if (((TextBox)_ctrl).Tag.ToString() == "N2")
                        {
                            ((TextBox)_ctrl).Text = Decimal.Parse(((TextBox)_ctrl).Text).ToString("N2");
                        }
                        else if (((TextBox)_ctrl).Tag.ToString() == "N0")
                        {
                            ((TextBox)_ctrl).Text = Decimal.Parse(((TextBox)_ctrl).Text).ToString("N0");
                        }
                        else if (((TextBox)_ctrl).Tag.ToString() == "N")
                        {
                            try
                            {
                                ((TextBox)_ctrl).Text = Int32.Parse(((TextBox)_ctrl).Text.Split(',').GetValue(0).ToString()).ToString();
                            }
                            catch
                            {
                                ((TextBox)_ctrl).Text = Int32.Parse(((TextBox)_ctrl).Text.Split('.').GetValue(0).ToString()).ToString();
                            }
                        }
                    }
                }
            }
        }

        public static Object Pesquisa(String query, List<clsSqlFactoryValue> chave_valor, String cnn)
        {
            Object result = null;

            SqlConnection scn = new SqlConnection(cnn);
            SqlCommand scd = new SqlCommand(query, scn);
            SqlDataReader sdr;

            foreach (clsSqlFactoryValue sfv in chave_valor)
            {
                scd.Parameters.AddWithValue("@" + sfv.Nome, sfv.Valor);
            }

            scn.Open();
            sdr = scd.ExecuteReader();

            if (sdr.Read() == true)
            {
                result = sdr[0];
            }

            scn.Close();

            return result;
        }

        /// <summary>
        /// Deve ser eliminado futuramente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="_aceitarnegativo"></param>
        public static void ControlKeyPressNumero(TextBox sender, KeyPressEventArgs e, Boolean _aceitarnegativo)
        {
            if (_aceitarnegativo == true)
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != (Char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != (Char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Valida o numero da placa do carro
        /// </summary>
        /// <param name="placa"></param>
        /// <returns></returns>
        public static Boolean ValidaPlaca(String placa)
        {
            if (placa.ToUpper() == "XXX9999" || placa == "XXX999" || placa == "XX9999" || placa == "XXXX999")
            {
                return false;
            }

            Regex regex = new Regex(@"^[a-zA-Z]{3}\-\d{4}$");

            if (regex.IsMatch(placa))
            {
                return true;
            }

            return false;
        }

        public static Boolean ValidaCPF(String s)
        {
            String cpf = s.Replace(".", "");

            cpf = cpf.Replace("-", "");
            if (cpf.Length != 11)
            {
                return false;
            }

            Boolean igual = true;
            for (int i = 1; i < 11 && igual; i++)
            {
                if (cpf[i] != cpf[0])
                {
                    igual = false;
                }
            }

            if (igual || cpf == "12345678909")
            {
                return false;
            }

            Int32[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
            {
                numeros[i] = int.Parse(
                cpf[i].ToString());
            }

            Int32 soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += (10 - i) * numeros[i];
            }

            Int32 resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                {
                    return false;
                }
            }
            else if (numeros[9] != 11 - resultado)
            {
                return false;
            }

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += (11 - i) * numeros[i];
            }

            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                {
                    return false;
                }
            }
            else
            {
                if (numeros[10] != 11 - resultado)
                {
                    return false;
                }
            }
            return true;
        }

        public static Boolean ValidaCnpj(String s)
        {

            String cnpj = s.Replace(".", "");
            cnpj = cnpj.Replace("/", "");
            cnpj = cnpj.Replace("-", "");

            Int32[] digitos, soma, resultado;
            Int32 nrDig;
            String ftmt;
            Boolean[] cnpjOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];

            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            cnpjOk = new bool[2];
            cnpjOk[0] = false;
            cnpjOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(cnpj.Substring(nrDig, 1));

                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig + 1, 1)));

                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        cnpjOk[nrDig] = (digitos[12 + nrDig] == 0);
                    else
                        cnpjOk[nrDig] = (digitos[12 + nrDig] == (11 - resultado[nrDig]));
                }
                return (cnpjOk[0] && cnpjOk[1]);
            }
            catch
            {
                return false;
            }
        }

        public static String RemoveAcentos(String str)
        {
            if (str != null)
            {
                string C_acentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇçØ!#$%¨&*@€‚ƒ„…†‡ˆ‰Š‹ŒŽ‘’“”•–—˜™š›œžŸ ¡¢£¤¥¦§¨©«¬­®åæðñ÷ø¿¸´ªº²³¯¿†¬­‡×Ø|";
                string S_acentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc" + "".PadRight(77, ' ');

                for (int i = 0; i < C_acentos.Length; i++)
                {
                    str = str.Replace(C_acentos[i].ToString(), S_acentos[i].ToString()).Trim();
                }
            }
            else
            {
                str = "";
            }
            return str;
        }

        public static void FillComboBox(ComboBox c,
                                    String query,
                                    String conexao)
        {
            c.AutoCompleteCustomSource.Clear();

            SqlConnection scn = new SqlConnection(conexao);
            SqlCommand scd = new SqlCommand(query, scn);
            SqlDataReader sdr;

            scn.Open();
            sdr = scd.ExecuteReader();
            while (sdr.Read())
            {
                c.Items.Add(sdr[0].ToString());
            }
            scn.Close();
        }

        public static string CamposVisual(String tipo, String campo)
        {
            if (tipo == "CGC")
            {
                campo = campo.Replace(".", "").Replace("-", "").Replace("/", "");
                if (campo.Length > 2)
                {
                    campo = campo.Insert(2, ".");
                }

                if (campo.Length > 6)
                {
                    campo = campo.Insert(6, ".");
                }

                if (campo.Length > 10)
                {
                    campo = campo.Insert(10, "/");
                }

                if (campo.Length > 15)
                {
                    campo = campo.Insert(15, "-");
                }
            }

            if (tipo == "CPF" && campo.Length == 11)
            {
                campo = (campo.Substring(0, 3) + "." + campo.Substring(3, 3) + "." + campo.Substring(6, 3) + "-" + campo.Substring(9, 2)).ToString();
            }
            if (tipo == "IE" && campo.Length == 12)
            {
                campo = (campo.Substring(0, 3) + "." + campo.Substring(3, 3) + "." + campo.Substring(6, 3) + "." + campo.Substring(9, 3)).ToString();
            }
            if (tipo == "CEP" && campo.Length == 8)
            {
                campo = (campo.Substring(0, 5) + "-" + campo.Substring(5, 3)).ToString();
            }

            return campo;
        }

        public static void ControlKeyDownTelefone(TextBox sender, KeyEventArgs e)
        {
            Int32 keycode;
            Char keycodevalor;

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{tab}");
            }
            else if (e.KeyCode == Keys.Up)
            {
                SendKeys.Send("+{tab}");
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                sender.SelectionStart = sender.Text.Length;
            }
            else if ((e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) &&
                        (Char)e.KeyCode != Char.Parse(".") && (Char)e.KeyCode != Char.Parse("'"))
            {
                if (sender.Text.Length - 1 > 4 && sender.Text.Length - 1 < 10)
                {
                    sender.Text = sender.Text.Replace("-", "");
                    sender.Text = sender.Text.Insert(4, "-");
                    sender.SelectionStart = sender.Text.Length;
                }
                else if (sender.Text.Length - 1 == 10)
                {
                    sender.Text = sender.Text.Replace("-", "");
                    sender.Text = sender.Text.Insert(5, "-");
                    sender.SelectionStart = sender.Text.Length + 1;
                }
                return;
            }
            else if ((e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) &&
                        (Char)e.KeyCode == Char.Parse(".") && (Char)e.KeyCode != Char.Parse("'"))
            {
                if (sender.Text.Length - 1 > 4 && sender.Text.Length - 1 < 10)
                {
                    sender.Text = sender.Text.Replace("-", "");
                    sender.Text = sender.Text.Insert(4, "-");
                    if (sender.Text.Length == 10)
                    {
                        sender.Text = sender.Text.Replace("-", "");
                        sender.Text = sender.Text.Insert(5, "-");
                    }
                    sender.SelectionStart = sender.Text.Length;
                }
                else if (sender.Text.Length - 1 == 10)
                {
                    sender.Text = sender.Text.Replace("-", "");
                    sender.Text = sender.Text.Insert(5, "-");
                    sender.SelectionStart = sender.Text.Length + 1;
                }
                return;
            }
            //else if (sender.Text.Length > 3 && sender.Text.Length < 9 )
            //{
            //    sender.Text = sender.Text.Replace("-", "");
            //    sender.Text = sender.Text.Insert(4, "-");
            //    sender.SelectionStart = sender.Text.Length;
            //}
            //else if (sender.Text.Length == 9)
            //{
            //    sender.Text = sender.Text.Replace("-", "");
            //    sender.Text = sender.Text.Insert(5, "-");
            //    sender.SelectionStart = sender.Text.Length + 1;
            //}

            if (e.KeyCode == Keys.NumPad0 ||
                e.KeyCode == Keys.NumPad1 ||
                e.KeyCode == Keys.NumPad2 ||
                e.KeyCode == Keys.NumPad3 ||
                e.KeyCode == Keys.NumPad4 ||
                e.KeyCode == Keys.NumPad5 ||
                e.KeyCode == Keys.NumPad6 ||
                e.KeyCode == Keys.NumPad7 ||
                e.KeyCode == Keys.NumPad8 ||
                e.KeyCode == Keys.NumPad9)
            {
                keycode = e.KeyValue - 48;
            }
            else
            {
                keycode = e.KeyValue;
            }
            keycodevalor = (Char)keycode;

            if (keycodevalor == '0' ||
                keycodevalor == '1' ||
                keycodevalor == '2' ||
                keycodevalor == '3' ||
                keycodevalor == '4' ||
                keycodevalor == '5' ||
                keycodevalor == '6' ||
                keycodevalor == '7' ||
                keycodevalor == '8' ||
                keycodevalor == '9')
            {
                // Está OK
                // Passa para o campo a devida letra
                if (sender.Text.Length > 3 && sender.Text.Length < 9)
                {
                    sender.Text = sender.Text.Replace("-", "");
                    sender.Text = sender.Text.Insert(4, "-");
                    sender.SelectionStart = sender.Text.Length;
                }
                else if (sender.Text.Length == 9)
                {
                    sender.Text = sender.Text.Replace("-", "");
                    sender.Text = sender.Text.Insert(5, "-");
                    sender.SelectionStart = sender.Text.Length + 1;
                }

                if (sender.Text.Length == 10)
                {
                    e.SuppressKeyPress = true;
                }
            }
            else
            {
                e.SuppressKeyPress = true;
            }
        }

        public static void ControlKeyDownNumeroPonto(TextBox sender, KeyEventArgs e)
        {
            Int32 keycode;
            Char keycodevalor;

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{tab}");
            }
            else if (e.KeyCode == Keys.Up)
            {
                SendKeys.Send("+{tab}");
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                sender.SelectionStart = sender.Text.Length;
            }
            else if ((e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) &&
                        (Char)e.KeyCode != Char.Parse("'"))
            {
                return;
            }

            if (e.KeyCode == Keys.NumPad0 ||
                e.KeyCode == Keys.NumPad1 ||
                e.KeyCode == Keys.NumPad2 ||
                e.KeyCode == Keys.NumPad3 ||
                e.KeyCode == Keys.NumPad4 ||
                e.KeyCode == Keys.NumPad5 ||
                e.KeyCode == Keys.NumPad6 ||
                e.KeyCode == Keys.NumPad7 ||
                e.KeyCode == Keys.NumPad8 ||
                e.KeyCode == Keys.NumPad9)
            {
                keycode = e.KeyValue - 48;
            }
            else
            {
                keycode = e.KeyValue;
            }
            keycodevalor = (Char)keycode;

            if (keycodevalor == '0' ||
                keycodevalor == '1' ||
                keycodevalor == '2' ||
                keycodevalor == '3' ||
                keycodevalor == '4' ||
                keycodevalor == '5' ||
                keycodevalor == '6' ||
                keycodevalor == '7' ||
                keycodevalor == '8' ||
                keycodevalor == '9')
            {
                // Está OK
                // Passa para o campo a devida letra
            }
            else
            {
                if ((Char)e.KeyCode != '.' && (Char)e.KeyCode != ',')
                {
                    e.SuppressKeyPress = true;
                }
            }
        }

 /*       public static String[] BuscaCep(String ctexto)
        {
            //Variáveis de controle de resultado 
            //resultado resultado_txt 
            //1 sucesso. cep encontrado 
            //-1 cep não encontrado 
            //-2 formato de cep inválido 
            //-3 limite de buscas de ip por minuto excedido 
            //-4 ip banido. contate o administrador 
            //-5 chave banida. contate o administrador 
            //-6 entre 0 e 6 horas da madrugada todas as buscas são limitadas a 5 buscas por minuto 
            //-7 chave inválida. cadastre-se para continuar utilizando o servico 
            try
            {
                String[] _valores;
                DataRow drEmpresaCep;
                String data = "";
                String url;
                WebClient wc = new WebClient();
                String regiao = "";

                drEmpresaCep = clsSelectInsertUpdateBLL.SelectCollectionFields(clsInfo.conexaosqldados,
                            "PROXYSITE, PROXYPORTA, PROXYLOGIN, PROXYSENHA, CHAVEBUSCACEP", "EMPRESAS ",
                            "ID = " + clsInfo.zempresaid.ToString(), "PROXYSITE");

                if (String.IsNullOrEmpty(drEmpresaCep["CHAVEBUSCACEP"].ToString()))
                {
                    return null;
                }

                if (drEmpresaCep != null)
                {
                    url = @"http://www.buscarcep.com.br/?cep=" + ctexto.Trim().Replace("-", "") + "&formato=string&chave=" + drEmpresaCep["CHAVEBUSCACEP"].ToString();

                    if (drEmpresaCep["PROXYSITE"].ToString().Length > 0 &&
                        drEmpresaCep["PROXYPORTA"].ToString().Length > 0 &&
                        drEmpresaCep["PROXYLOGIN"].ToString().Length > 0 &&
                        drEmpresaCep["PROXYSENHA"].ToString().Length > 0)
                    {
                        WebProxy p = null;
                        ICredentials cred;
                        cred = new NetworkCredential(drEmpresaCep["PROXYLOGIN"].ToString(), drEmpresaCep["PROXYSENHA"].ToString());
                        p = new WebProxy(drEmpresaCep["PROXYSITE"].ToString().ToLower() + ":" + drEmpresaCep["PROXYPORTA"].ToString(), true, null, cred);
                        wc.Proxy = p;
                        WebRequest.DefaultWebProxy = p;
                        data = wc.DownloadString(url);
                    }
                    else
                    {
                        data = wc.DownloadString(url);
                    }

                    String x1;

                    x1 = data.Replace("cep=", "").Replace("uf=", "").Replace("cidade=", "").Replace("bairro=", "").
                    Replace("tipo_logradouro=", "").Replace("logradouro=", "").Replace("resultado=", "");

                    if (x1.Split('&').GetValue(1).ToString() == "-1" || ctexto == "")
                    {
                        MessageBox.Show("Cep não encontrado !", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    else if (x1.Split('&').GetValue(1).ToString() == "-2")
                    {
                        MessageBox.Show("Codigo de erro -2" + Environment.NewLine + "Formato de CEP inválido, por favor corrija !", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    else if (x1.Split('&').GetValue(1).ToString() == "-3")
                    {
                        MessageBox.Show("Codigo de erro -3" + Environment.NewLine + "limite de buscas de ip por minuto excedido, por favor aguarde 1 minuto e tente novamente!", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    else if (x1.Split('&').GetValue(1).ToString() == "-4")
                    {
                        MessageBox.Show("Codigo de erro -4" + Environment.NewLine + "IP banido. contate o administrador", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    else if (x1.Split('&').GetValue(1).ToString() == "-5")
                    {
                        MessageBox.Show("Codigo de erro -5" + Environment.NewLine + "Chave banida. contate o administrador", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    else if (x1.Split('&').GetValue(1).ToString() == "-6")
                    {
                        MessageBox.Show("Codigo de erro -6" + Environment.NewLine + "Entre 0 e 6 horas da madrugada todas as buscas são limitadas a 5 buscas por minuto", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    else if (x1.Split('&').GetValue(1).ToString() == "-7")
                    {
                        MessageBox.Show("Codigo de erro -7" + Environment.NewLine + "Chave inválida. cadastre-se para continuar utilizando o servico!"
                            + Environment.NewLine + "http://www.buscarcep.com.br/?cep="
                            + Environment.NewLine + "ou Verifique a chave na Configuração da Empresa Credenciada a Utilizar", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    else
                    {

                        // uf,cidade,bairro,tipoend,endereço,IBGE
                        _valores = new String[7] {x1.Split('&').GetValue(2).ToString().ToUpper(),
                                    x1.Split('&').GetValue(3).ToString().ToUpper(),
                                    x1.Split('&').GetValue(4).ToString().ToUpper(),
                                    x1.Split('&').GetValue(5).ToString().ToUpper(),
                                    x1.Split('&').GetValue(6).ToString().ToUpper(),
                                    x1.Substring((x1.Length - 7), 7),
                                    ""
                                   };
                        regiao = Procedure.PesquisaoPrimeiro(clsInfo.conexaosqldados, "select REGIAO from ESTADOS where ESTADO = '" + _valores[0].ToUpper() + "'", "0");
                        _valores[6] = regiao;

                        return _valores;
                    }
                }
                else
                {
                    MessageBox.Show("Empresa sem Cadastro!", "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aplisoft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;
            }
        }
 */
        public static Boolean ValidarInscricaoEstadual(string pUF, string pInscr)
        {
            bool retorno = false;
            string strBase;
            string strBase2;
            string strOrigem;
            string strDigito1;
            string strDigito2;
            int intPos;
            int intValor;
            int intSoma = 0;
            int intResto;
            int intNumero;
            int intPeso = 0;
            strBase = "";
            strBase2 = "";
            strOrigem = "";

            if ((pInscr.Trim().ToUpper() == "ISENTO"))
                return true;
            for (intPos = 1; intPos <= pInscr.Trim().Length; intPos++)
            {
                if ((("0123456789P".IndexOf(pInscr.Substring((intPos - 1), 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0))
                    strOrigem = (strOrigem + pInscr.Substring((intPos - 1), 1));
            }
            switch (pUF.ToUpper())
            {
                case "AC":
                    // #region
                    strBase = (strOrigem.Trim() + "00000000000").Substring(0, 11);
                    if (strBase.Substring(0, 2) == "01")
                    {
                        intSoma = 0;
                        intPeso = 4;
                        for (intPos = 1; (intPos <= 11); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            if (intPeso == 1) intPeso = 9;
                            intSoma += intValor * intPeso;
                            intPeso--;
                        }
                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        intSoma = 0;
                        strBase = (strOrigem.Trim() + "000000000000").Substring(0, 12);
                        intPeso = 5;
                        for (intPos = 1; (intPos <= 12); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            if (intPeso == 1) intPeso = 9;
                            intSoma += intValor * intPeso;
                            intPeso--;
                        }
                        intResto = (intSoma % 11);
                        strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 12) + strDigito2);
                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    // #endregion
                    break;
                case "AL":
                    // #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    if ((strBase.Substring(0, 2) == "24"))
                    {
                        //24000004-8
                        //98765432
                        intSoma = 0;
                        intPeso = 9;
                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                            intSoma += intValor * intPeso;
                            intPeso--;
                        }
                        intSoma = (intSoma * 10);
                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto == 10) ? "0" : Convert.ToString(intResto)).Substring((((intResto == 10) ? "0" : Convert.ToString(intResto)).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);
                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    // #endregion
                    break;
                case "AM":
                    // #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;
                    intPeso = 9;
                    for (intPos = 1; (intPos <= 8); intPos++)
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                        intSoma += intValor * intPeso;
                        intPeso--;
                    }
                    intResto = (intSoma % 11);
                    if (intSoma < 11)
                        strDigito1 = (11 - intSoma).ToString();
                    else
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));

                    strBase2 = (strBase.Substring(0, 8) + strDigito1);
                    if ((strBase2 == strOrigem))
                        retorno = true;
                    // #endregion
                    break;
                case "AP":
                    // #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intPeso = 9;
                    if ((strBase.Substring(0, 2) == "03"))
                    {
                        strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                        intSoma = 0;
                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intSoma += intValor * intPeso;
                            intPeso--;
                        }
                        intResto = (intSoma % 11);
                        intValor = (11 - intResto);

                        strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);
                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    // #endregion
                    break;
                case "BA":
                    // #region
                    if (strOrigem.Length == 8)
                        strBase = (strOrigem.Trim() + "00000000").Substring(0, 8);
                    else if (strOrigem.Length == 9)
                        strBase = (strOrigem.Trim() + "00000000").Substring(0, 9);
                    if ((("0123458".IndexOf(strBase.Substring(0, 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0) && strBase.Length == 8)
                    {
                        // #region
                        intSoma = 0;
                        for (intPos = 1; (intPos <= 6); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            if (intPos == 1) intPeso = 7;
                            intSoma += intValor * intPeso;
                            intPeso--;
                        }
                        intResto = (intSoma % 10);
                        strDigito2 = ((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Length - 1));
                        strBase2 = strBase.Substring(0, 7) + strDigito2;
                        if (strBase2 == strOrigem)
                            retorno = true;
                        if (retorno)
                        {
                            intSoma = 0;
                            intPeso = 0;
                            for (intPos = 1; (intPos <= 7); intPos++)
                            {
                                intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                                if (intPos == 7)
                                    intValor = int.Parse(strBase.Substring((intPos), 1));
                                if (intPos == 1) intPeso = 8;
                                intSoma += intValor * intPeso;
                                intPeso--;
                            }
                            intResto = (intSoma % 10);
                            strDigito1 = ((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Length - 1));
                            strBase2 = (strBase.Substring(0, 6) + strDigito1 + strDigito2);
                            if ((strBase2 == strOrigem))
                                retorno = true;
                        }
                        // #endregion
                    }
                    else if ((("679".IndexOf(strBase.Substring(0, 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0) && strBase.Length == 8)
                    {
                        // #region
                        intSoma = 0;
                        for (intPos = 1; (intPos <= 6); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            if (intPos == 1) intPeso = 7;
                            intSoma += intValor * intPeso;
                            intPeso--;
                        }
                        intResto = (intSoma % 11);
                        strDigito2 = ((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = strBase.Substring(0, 7) + strDigito2;
                        if (strBase2 == strOrigem)
                            retorno = true;
                        if (retorno)
                        {
                            intSoma = 0;
                            intPeso = 0;
                            for (intPos = 1; (intPos <= 7); intPos++)
                            {
                                intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                                if (intPos == 7)
                                    intValor = int.Parse(strBase.Substring((intPos), 1));
                                if (intPos == 1) intPeso = 8;
                                intSoma += intValor * intPeso;
                                intPeso--;
                            }
                            intResto = (intSoma % 11);
                            strDigito1 = ((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                            strBase2 = (strBase.Substring(0, 6) + strDigito1 + strDigito2);
                            if ((strBase2 == strOrigem))
                                retorno = true;
                        }
                        // #endregion
                    }
                    else if ((("0123458".IndexOf(strBase.Substring(1, 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0) && strBase.Length == 9)
                    {
                        // #region
                        /* Segundo digito */
                        //1000003
                        //8765432
                        intSoma = 0;

                        for (intPos = 1; (intPos <= 7); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            if (intPos == 1) intPeso = 8;
                            intSoma += intValor * intPeso;
                            intPeso--;
                        }
                        intResto = (intSoma % 10);
                        strDigito2 = ((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Length - 1));
                        strBase2 = strBase.Substring(0, 8) + strDigito2;
                        if (strBase2 == strOrigem)
                            retorno = true;
                        if (retorno)
                        {
                            //1000003 6
                            //9876543 2
                            intSoma = 0;
                            intPeso = 0;

                            for (intPos = 1; (intPos <= 8); intPos++)
                            {
                                intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                                if (intPos == 8)
                                    intValor = int.Parse(strBase.Substring((intPos), 1));
                                if (intPos == 1) intPeso = 9;
                                intSoma += intValor * intPeso;
                                intPeso--;
                            }
                            intResto = (intSoma % 10);
                            strDigito1 = ((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                            strBase2 = (strBase.Substring(0, 7) + strDigito1 + strDigito2);
                            if ((strBase2 == strOrigem))
                                retorno = true;
                        }
                        // #endregion
                    }
                    // #endregion
                    break;
                case "CE":
                    // #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;
                    for (intPos = 1; (intPos <= 8); intPos++)
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                    }
                    intResto = (intSoma % 11);
                    intValor = (11 - intResto);
                    if ((intValor > 9))
                        intValor = 0;
                    strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);
                    if ((strBase2 == strOrigem))
                        retorno = true;
                    // #endregion
                    break;
                case "DF":
                    // #region
                    strBase = (strOrigem.Trim() + "0000000000000").Substring(0, 13);
                    if ((strBase.Substring(0, 3) == "073"))
                    {
                        intSoma = 0;
                        intPeso = 2;
                        for (intPos = 11; (intPos >= 1); intPos = (intPos + -1))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);
                            if ((intPeso > 9))
                                intPeso = 2;
                        }
                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 11) + strDigito1);
                        intSoma = 0;
                        intPeso = 2;
                        for (intPos = 12; (intPos >= 1); intPos = (intPos + -1))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);
                            if ((intPeso > 9))
                                intPeso = 2;
                        }
                        intResto = (intSoma % 11);
                        strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 12) + strDigito2);
                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    // #endregion
                    break;
                case "ES":
                    // #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;
                    for (intPos = 1; (intPos <= 8); intPos++)
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                    }
                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);
                    if ((strBase2 == strOrigem))
                        retorno = true;
                    // #endregion
                    break;
                case "GO":
                    // #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    if ((("10,11,15".IndexOf(strBase.Substring(0, 2), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0))
                    {
                        intSoma = 0;
                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                        }
                        intResto = (intSoma % 11);
                        if ((intResto == 0))
                            strDigito1 = "0";
                        else if ((intResto == 1))
                        {
                            intNumero = int.Parse(strBase.Substring(0, 8));
                            strDigito1 = (((intNumero >= 10103105) && (intNumero <= 10119997)) ? "1" : "0").Substring(((((intNumero >= 10103105) && (intNumero <= 10119997)) ? "1" : "0").Length - 1));
                        }
                        else
                            strDigito1 = Convert.ToString((11 - intResto)).Substring((Convert.ToString((11 - intResto)).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);
                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    // #endregion
                    break;
                case "MA":
                    // #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    if ((strBase.Substring(0, 2) == "12"))
                    {
                        intSoma = 0;
                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                        }
                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);
                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    // #endregion
                    break;
                case "MT":
                    // #region
                    strBase = (strOrigem.Trim() + "0000000000").Substring(0, 10);
                    intSoma = 0;
                    intPeso = 2;
                    for (intPos = 10; intPos >= 1; intPos = (intPos + -1))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * intPeso);
                        intSoma = (intSoma + intValor);
                        intPeso = (intPeso + 1);
                        if ((intPeso > 9))
                            intPeso = 2;
                    }
                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 10) + strDigito1);
                    if ((strBase2 == strOrigem))
                        retorno = true;
                    // #endregion
                    break;
                case "MS":
                    // #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    if ((strBase.Substring(0, 2) == "28"))
                    {
                        intSoma = 0;
                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                        }
                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);
                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    // #endregion
                    break;
                case "MG":
                    // #region
                    strBase = (strOrigem.Trim() + "0000000000000").Substring(0, 13);
                    strBase2 = (strBase.Substring(0, 3) + ("0" + strBase.Substring(3, 8)));
                    intNumero = 2;
                    string strSoma = "";
                    for (intPos = 1; (intPos <= 12); intPos++)
                    {
                        intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                        intNumero = ((intNumero == 2) ? 1 : 2);
                        intValor = (intValor * intNumero);

                        intSoma = (intSoma + intValor);
                        strSoma += intValor.ToString();
                    }
                    intSoma = 0;
                    //Soma -se os algarismos, não o produto
                    for (int i = 0; i < strSoma.Length; i++)
                    {
                        intSoma += int.Parse(strSoma.Substring(i, 1));
                    }
                    intValor = int.Parse(strBase.Substring(8, 2));
                    strDigito1 = (intValor - intSoma).ToString();
                    strBase2 = (strBase.Substring(0, 11) + strDigito1);
                    if ((strBase2 == strOrigem.Substring(0, 12)))
                        retorno = true;
                    if (retorno)
                    {
                        intSoma = 0;
                        intPeso = 3;
                        for (intPos = 1; (intPos <= 12); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            if (intPeso < 2)
                                intPeso = 11;

                            intSoma += (intValor * intPeso);
                            intPeso--;
                        }
                        intResto = (intSoma % 11);
                        intValor = 11 - intResto;
                        strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 12) + strDigito2);
                        if (strBase2 == strOrigem)
                            retorno = true;
                    }

                    // #endregion
                    break;
                case "PA":
                    // #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    if ((strBase.Substring(0, 2) == "15"))
                    {
                        intSoma = 0;
                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                        }
                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);
                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    // #endregion
                    break;
                case "PB":
                    // #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;
                    for (intPos = 1; (intPos <= 8); intPos++)
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                    }
                    intResto = (intSoma % 11);
                    intValor = (11 - intResto);
                    if ((intValor > 9))
                        intValor = 0;
                    strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);
                    if ((strBase2 == strOrigem))
                        retorno = true;
                    // #endregion
                    break;
                case "PE":
                    // #region
                    strBase = (strOrigem.Trim() + "00000000000000").Substring(0, 14);
                    intSoma = 0;
                    intPeso = 2;
                    for (intPos = 7; (intPos >= 1); intPos = (intPos + -1))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * intPeso);
                        intSoma = (intSoma + intValor);
                        intPeso = (intPeso + 1);
                        if ((intPeso > 9))
                            intPeso = 2;
                    }
                    intResto = (intSoma % 11);
                    intValor = (11 - intResto);
                    if ((intValor > 9))
                        intValor = (intValor - 10);
                    strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                    strBase2 = (strBase.Substring(0, 7) + strDigito1);
                    if ((strBase2 == strOrigem.Substring(0, 8)))
                        retorno = true;
                    if (retorno)
                    {
                        intSoma = 0;
                        intPeso = 2;
                        for (intPos = 8; (intPos >= 1); intPos = (intPos + -1))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);
                            if ((intPeso > 9))
                                intPeso = 2;
                        }
                        intResto = (intSoma % 11);
                        intValor = (11 - intResto);
                        if ((intValor > 9))
                            intValor = (intValor - 10);
                        strDigito2 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito2);
                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    // #endregion
                    break;
                case "PI":
                    // #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;
                    for (intPos = 1; (intPos <= 8); intPos++)
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                    }
                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);
                    if ((strBase2 == strOrigem))
                        retorno = true;
                    // #endregion
                    break;
                case "PR":
                    // #region
                    strBase = (strOrigem.Trim() + "0000000000").Substring(0, 10);
                    intSoma = 0;
                    intPeso = 2;
                    for (intPos = 8; (intPos >= 1); intPos = (intPos + -1))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * intPeso);
                        intSoma = (intSoma + intValor);
                        intPeso = (intPeso + 1);
                        if ((intPeso > 7))
                            intPeso = 2;
                    }
                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);
                    intSoma = 0;
                    intPeso = 2;
                    for (intPos = 9; (intPos >= 1); intPos = (intPos + -1))
                    {
                        intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                        intValor = (intValor * intPeso);
                        intSoma = (intSoma + intValor);
                        intPeso = (intPeso + 1);
                        if ((intPeso > 7))
                            intPeso = 2;
                    }
                    intResto = (intSoma % 11);
                    strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase2 + strDigito2);
                    if ((strBase2 == strOrigem))
                        retorno = true;
                    break;
                case "RJ":
                    // #region
                    strBase = (strOrigem.Trim() + "00000000").Substring(0, 8);
                    intSoma = 0;
                    intPeso = 2;
                    for (intPos = 7; (intPos >= 1); intPos = (intPos + -1))
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * intPeso);
                        intSoma = (intSoma + intValor);
                        intPeso = (intPeso + 1);
                        if ((intPeso > 7))
                            intPeso = 2;
                    }
                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 7) + strDigito1);
                    if ((strBase2 == strOrigem))
                        retorno = true;
                    // #endregion
                    break;
                case "RN": //Verficar com 10 digitos
                    // #region
                    if (strOrigem.Length == 9)
                        strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    else if (strOrigem.Length == 10)
                        strBase = (strOrigem.Trim() + "000000000").Substring(0, 10);
                    if ((strBase.Substring(0, 2) == "20") && strBase.Length == 9)
                    {
                        intSoma = 0;
                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                        }
                        intSoma = (intSoma * 10);
                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto > 9) ? "0" : Convert.ToString(intResto)).Substring((((intResto > 9) ? "0" : Convert.ToString(intResto)).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);
                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    else if (strBase.Length == 10)
                    {
                        intSoma = 0;
                        for (intPos = 1; (intPos <= 9); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * (11 - intPos));
                            intSoma = (intSoma + intValor);
                        }
                        intSoma = (intSoma * 10);
                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto > 10) ? "0" : Convert.ToString(intResto)).Substring((((intResto > 10) ? "0" : Convert.ToString(intResto)).Length - 1));
                        strBase2 = (strBase.Substring(0, 9) + strDigito1);
                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    // #endregion
                    break;
                case "RO":
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    strBase2 = strBase.Substring(3, 5);
                    intSoma = 0;
                    for (intPos = 1; (intPos <= 5); intPos++)
                    {
                        intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                        intValor = (intValor * (7 - intPos));
                        intSoma = (intSoma + intValor);
                    }
                    intResto = (intSoma % 11);
                    intValor = (11 - intResto);
                    if ((intValor > 9))
                        intValor = (intValor - 10);
                    strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);
                    if ((strBase2 == strOrigem))
                        retorno = true;
                    break;
                case "RR":
                    // #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    if ((strBase.Substring(0, 2) == "24"))
                    {
                        intSoma = 0;
                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = intValor * intPos;
                            intSoma += intValor;
                        }
                        intResto = (intSoma % 9);
                        strDigito1 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + strDigito1);
                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    // #endregion
                    break;
                case "RS":
                    // #region
                    strBase = (strOrigem.Trim() + "0000000000").Substring(0, 10);
                    intNumero = int.Parse(strBase.Substring(0, 3));
                    if (((intNumero > 0) && (intNumero < 468)))
                    {
                        intSoma = 0;
                        intPeso = 2;
                        for (intPos = 9; (intPos >= 1); intPos = (intPos + -1))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);
                            if ((intPeso > 9))
                                intPeso = 2;
                        }
                        intResto = (intSoma % 11);
                        intValor = (11 - intResto);
                        if ((intValor > 9))
                            intValor = 0;
                        strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                        strBase2 = (strBase.Substring(0, 9) + strDigito1);
                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    // #endregion
                    break;
                case "SC":
                    // #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;
                    for (intPos = 1; (intPos <= 8); intPos++)
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                    }
                    intResto = (intSoma % 11);
                    strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);
                    if ((strBase2 == strOrigem))
                        retorno = true;
                    // #endregion
                    break;
                case "SE":
                    // #region
                    strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                    intSoma = 0;
                    for (intPos = 1; (intPos <= 8); intPos++)
                    {
                        intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                        intValor = (intValor * (10 - intPos));
                        intSoma = (intSoma + intValor);
                    }
                    intResto = (intSoma % 11);
                    intValor = (11 - intResto);
                    if ((intValor > 9))
                        intValor = 0;
                    strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                    strBase2 = (strBase.Substring(0, 8) + strDigito1);
                    if ((strBase2 == strOrigem))
                        retorno = true;
                    // #endregion
                    break;
                case "SP":
                    // #region
                    if ((strOrigem.Substring(0, 1) == "P"))
                    {
                        strBase = (strOrigem.Trim() + "0000000000000").Substring(0, 13);
                        strBase2 = strBase.Substring(1, 8);
                        intSoma = 0;
                        intPeso = 1;
                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);
                            if ((intPeso == 2))
                                intPeso = 3;
                            if ((intPeso == 9))
                                intPeso = 10;
                        }
                        intResto = (intSoma % 11);
                        strDigito1 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                        strBase2 = (strBase.Substring(0, 9) + (strDigito1 + strBase.Substring(10, 3)));
                    }
                    else
                    {
                        strBase = (strOrigem.Trim() + "000000000000").Substring(0, 12);
                        intSoma = 0;
                        intPeso = 1;
                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);
                            if ((intPeso == 2))
                                intPeso = 3;
                            if ((intPeso == 9))
                                intPeso = 10;
                        }
                        intResto = (intSoma % 11);
                        strDigito1 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                        strBase2 = (strBase.Substring(0, 8) + (strDigito1 + strBase.Substring(9, 2)));
                        intSoma = 0;
                        intPeso = 2;
                        for (intPos = 11; (intPos >= 1); intPos = (intPos + -1))
                        {
                            intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                            intValor = (intValor * intPeso);
                            intSoma = (intSoma + intValor);
                            intPeso = (intPeso + 1);
                            if ((intPeso > 10))
                                intPeso = 2;
                        }
                        intResto = (intSoma % 11);
                        strDigito2 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                        strBase2 = (strBase2 + strDigito2);
                    }
                    if ((strBase2 == strOrigem))
                        retorno = true;
                    // #endregion
                    break;
                case "TO":
                    // #region
                    strBase = (strOrigem.Trim() + "00000000000").Substring(0, 11);
                    if ((("01,02,03,99".IndexOf(strBase.Substring(2, 2), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0))
                    {
                        strBase2 = (strBase.Substring(0, 2) + strBase.Substring(4, 6));
                        intSoma = 0;
                        for (intPos = 1; (intPos <= 8); intPos++)
                        {
                            intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                            intValor = (intValor * (10 - intPos));
                            intSoma = (intSoma + intValor);
                        }
                        intResto = (intSoma % 11);
                        strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                        strBase2 = (strBase.Substring(0, 10) + strDigito1);
                        if ((strBase2 == strOrigem))
                            retorno = true;
                    }
                    // #endregion
                    break;
            }
            return retorno;
        }

        /// <summary>
        /// Retorna o tamanho de uma imagem em bytes.
        /// </summary>
        /// <param name="image">Imagem que terá o seu tamanho calculado.</param>
        /// <returns></returns>
        public static int TamanhoImagem(Image image)
        {
            if (image == null)
            {
                return 0;
            }

            var imageConverter = new ImageConverter();

            var imageBytes = (byte[])imageConverter.ConvertTo(image, typeof(byte[]));

            return imageBytes.Length;
        }

        public static Image RedimensionarImagem(Image imagemRedimensionar, Size size)
        {
            int sourceWidth = imagemRedimensionar.Width;
            int sourceHeight = imagemRedimensionar.Height;

            float nPercentW = (size.Width / (float)sourceWidth);
            float nPercentH = (size.Height / (float)sourceHeight);

            float nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var src = imagemRedimensionar;

            using (var dst = new Bitmap(destWidth, destHeight, imagemRedimensionar.PixelFormat))
            {
                dst.SetResolution(imagemRedimensionar.HorizontalResolution, imagemRedimensionar.VerticalResolution);

                using (var g = Graphics.FromImage(dst))
                {
                    var mime = GetMimeType(imagemRedimensionar);
                    ImageFormat format;
                    if (mime == "image/gif" || mime == "image/png")
                    {
                        //convert all gif to png, better resize quality
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(src, 0, 0, dst.Width, dst.Height);
                        format = ImageFormat.Png;
                    }
                    else
                    {
                        //jpeg
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        format = ImageFormat.Jpeg;
                    }

                    g.DrawImage(src, 0, 0, dst.Width, dst.Height);

                    var m = new MemoryStream();
                    dst.Save(m, format);

                    var img = Image.FromStream(m);

                    return img;
                }
            }
        }

        public static string GetMimeType(Image image)
        {
            var content = string.Empty;

            if (image.RawFormat.Guid.Equals(System.Drawing.Imaging.ImageFormat.Bmp.Guid))
            {
                content = "image/x-ms-bmp";
            }
            else if (image.RawFormat.Guid.Equals(System.Drawing.Imaging.ImageFormat.Jpeg.Guid))
            {
                content = "image/jpeg";
            }
            else if (image.RawFormat.Guid.Equals(System.Drawing.Imaging.ImageFormat.Gif.Guid))
            {
                content = "image/gif";
            }
            else if (image.RawFormat.Guid.Equals(System.Drawing.Imaging.ImageFormat.Png.Guid))
            {
                content = "image/png";
            }
            else
            {
                content = "application/octet-stream";
            }

            return content;
        }
    }
}
