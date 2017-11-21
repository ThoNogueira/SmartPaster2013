using System;
using System.Windows.Forms; //clipboard
using EnvDTE;
using EnvDTE80;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace SmartPaster2013
{
    /// <summary>
    /// Class responsible for doing the pasting/manipulating of clipdata.
    /// </summary>
    internal sealed class SmartPaster
    {
        /// <summary>
        ///  Convient property to retrieve the clipboard text from the clipboard
        /// </summary>
        private static string ClipboardText
        {
            get
            {
                IDataObject iData = Clipboard.GetDataObject();
                if (iData == null) return string.Empty;
                //is it Unicode? Then we use that
                if (iData.GetDataPresent(DataFormats.UnicodeText))
                    return Convert.ToString(iData.GetData(DataFormats.UnicodeText));
                //otherwise ANSI
                if (iData.GetDataPresent(DataFormats.Text))
                    return Convert.ToString(iData.GetData(DataFormats.Text));
                return string.Empty;
            }
        }


        /// <summary>
        /// Inserts text at current cursor location in the application
        /// </summary>
        /// <param name="application">application with activewindow</param>
        /// <param name="text">text to insert</param>
        private static void Paste(DTE2 application, string text)
        {
            //get the text document
            var txt = (TextDocument)application.ActiveDocument.Object("TextDocument");

            //get an edit point
            EditPoint ep = txt.Selection.ActivePoint.CreateEditPoint();

            //get a start point
            EditPoint sp = txt.Selection.ActivePoint.CreateEditPoint();

            //open the undo context
            bool isOpen = application.UndoContext.IsOpen;
            if (!isOpen)
                application.UndoContext.Open("SmartPaster");

            //clear the selection
            if (!txt.Selection.IsEmpty)
                txt.Selection.Delete();

            //insert the text
            //ep.Insert(Indent(text, ep.LineCharOffset))
            ep.Insert(text);

            //smart format
            sp.SmartFormat(ep);

            //close the context
            if (!isOpen)
                application.UndoContext.Close();
        }

        private static bool IsXml(DTE2 application)
        {
            var caption = application.ActiveWindow.Caption;
            foreach (var ext in new[] { ".xml", ".xsd", ".config", ".xaml" })
            {
                if (caption.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
        private static bool IsVb(DTE2 application)
        {
            return application.ActiveWindow.Caption.EndsWith(".vb", StringComparison.OrdinalIgnoreCase);
        }
        private static bool IsCs(DTE2 application)
        {
            return application.ActiveWindow.Caption.EndsWith(".cs", StringComparison.OrdinalIgnoreCase);
        }
        private static bool IsCxx(DTE2 application)
        {
            return application.ActiveDocument.Language == "C/C++";
        }

        #region Aux

        /// <summary>
        /// Reescreve o texto em notação camelo minúscula (Lower Camel Case).
        /// </summary>
        /// <param name="texto">Texto a ser reescrito.</param>
        /// <returns>Texto reescrito.</returns>
        public string TranformaEmNotacaoCameloMinuscula(string texto)
        {
            //Remove todos os espaços do texto
            string textoModificado = texto.Replace(" ", "");

            //Retorna o Texto na notação Lower Camel Case
            return string.Format("{0}{1}", textoModificado.First().ToString().ToLower(), textoModificado.Substring(1));
        }

        /// <summary>
        /// Reescreve o texto em notação camelo maiúscula (Upper Camel Case).
        /// </summary>
        /// <param name="texto">Texto a ser reescrito.</param>
        /// <returns>Texto reescrito.</returns>
        public string TranformaEmNotacaoCameloMaiuscula(string texto)
        {
            texto = Regex.Replace(texto, "[ +]", " ");

            string[] textosSeparados = texto.Split(' ');

            string textoFinal = string.Empty;

            foreach (string textoSeparado in textosSeparados)
            {
                if (!string.IsNullOrEmpty(textoSeparado.Trim()))
                {
                    //Retorna o Texto na notação Upper Camel Case
                    textoFinal = string.Format("{0}{1}{2}", textoFinal, textoSeparado.Trim().FirstOrDefault().ToString().ToUpper(), textoSeparado.Trim().Substring(1));
                }
            }

            //Retorna o texto final
            return textoFinal;
        }

        /// <summary>
        /// Reescreve o texto adicionando espaço em branco antes de letras Maiúsculas.
        /// </summary>
        /// <param name="texto">Texto a ser reescrito.</param>
        /// <returns>Texto reescrito.</returns>
        public string SeparaPorMaiuscula(string texto)
        {
            //Remove espaços nas extremidades do texto
            texto = texto.Trim();

            //Para cada letra do texto
            for (int indice = 1; indice < texto.Length; indice++)
            {
                //Se for uma letra maiúscula
                // E se possuir letra anterior
                if (char.IsUpper(texto[indice]))
                {
                    //Se a letra anterior a corrente for maiúscula
                    if (char.IsUpper(texto[indice - 1]))
                    {
                        //Se não for a ultima letra
                        // E a letra posterior a corrente for minúscula
                        if (indice + 1 < texto.Length && char.IsLower(texto[indice + 1]))
                        {
                            //Insere um espaço antes da letra
                            texto = texto.Insert(indice, " ");
                            //Incrementa o contador pois adicionou-se uma nova letra
                            indice++;
                        }
                    }
                    //Se a letra anterior a corrente NÃO for maiúscula
                    else
                    {
                        //Insere um espaço antes da letra
                        texto = texto.Insert(indice, " ");
                        //Incrementa o contador pois adicionou-se uma nova letra
                        indice++;
                    }
                }
            }

            //Retorna texto reescrito
            return texto.Trim();
        }

        /// <summary>
        /// Resgata o texto adequado para ser o nome da entidade nos comentários.
        /// </summary>
        /// <param name="texto">Nome da classe (definido no modelo).</param>
        /// <returns>Nome da entidade para ser colocada em cometários.</returns>
        public string ResgataNomeParaComentario(string texto)
        {
            //Remove acentos
            texto = this.SeparaPorMaiuscula(texto);
            //Torna as iniciais das preposições minúsculas
            texto = this.TornaMinusculaAInicialDasPreposicoes(texto);

            //Retorna o texto modificado
            return texto;
        }

        /// <summary>
        /// Transforma em letra minúscula as iniciais das preposições.
        /// </summary>
        /// <param name="texto">Texto a ser reescrito.</param>
        /// <returns>Texto reescrito.</returns>
        public string TornaMinusculaAInicialDasPreposicoes(string texto)
        {
            //substitui as letras maiúsculas das preposições por minúsculas
            texto = texto.Replace(" Da ", " da ");
            texto = texto.Replace(" De ", " de ");
            texto = texto.Replace(" Do ", " do ");
            texto = texto.Replace(" Na ", " na ");
            texto = texto.Replace(" No ", " no ");

            //Retorna texto reescrito
            return texto.Trim();
        }

        /// <summary>
        /// Remove acentos
        /// </summary>
        /// <param name="texto">Texto sobre o qual se deseja remover os acentos.</param>
        /// <returns>Texto sem acentos.</returns>
        public string RemoverAcentos(string texto)
        {
            //Substitui as letras com acentos

            // 'ç' e 'Ç'
            texto = Regex.Replace(texto, "[ç]", "c");
            texto = Regex.Replace(texto, "[Ç]", "C");
            // 'ñ' e 'Ñ'
            texto = Regex.Replace(texto, "[ñ]", "n");
            texto = Regex.Replace(texto, "[Ñ]", "N");
            // 'á', 'à', 'ä', 'â', 'ã', 'Á', 'À', 'Ä', 'Â' e 'Ã'
            texto = Regex.Replace(texto, "[áàäâã]", "a");
            texto = Regex.Replace(texto, "[ÁÀÄÂÃ]", "A");
            // 'é', 'è', 'ë', 'ê', 'É', 'È', 'Ë' e 'Ê'
            texto = Regex.Replace(texto, "[éèëê]", "e");
            texto = Regex.Replace(texto, "[ÉÈËÊ]", "E");
            // 'í', 'ì', 'ï', 'î', 'Í', 'Ì', 'Ï' e 'Î'
            texto = Regex.Replace(texto, "[íìïî]", "i");
            texto = Regex.Replace(texto, "[ÍÌÏÎ]", "I");
            // 'ó', 'ò', 'ö', 'ô', 'õ', 'Ó', 'Ò', 'Ö', 'Ô' e 'Õ'
            texto = Regex.Replace(texto, "[óòöôõ]", "o");
            texto = Regex.Replace(texto, "[ÓÒÖÔÕ]", "O");
            // 'ú', 'ù', 'ü', 'û', 'Ú', 'Ù', 'Ü' e 'Û'
            texto = Regex.Replace(texto, "[úùüû]", "u");
            texto = Regex.Replace(texto, "[ÚÙÜÛ]", "U");

            //Retorna o texto modificado
            return texto;
        }

        #endregion

        #region "Paste As ..."

        /// <summary>
        /// Public method to paste and format clipboard text as string the cursor
        /// location for the configured or active window's langage .
        /// </summary>
        /// <param name="application">application to insert</param>
        public void PasteAsString(DTE2 application)
        {
            string text;
            if (IsVb(application))
                text = SmartFormatter.LiterallyInVb(ClipboardText);
            else if (IsCs(application))
                text = SmartFormatter.LiterallyInCs(ClipboardText);
            else if (IsCxx(application))
                text = SmartFormatter.LiterallyInCxx(ClipboardText);
            else
                text = ClipboardText;
            Paste(application, text);
        }

        public void PasteAsBytes(DTE2 application)
        {
            if (IsCxx(application) || IsCs(application))
            {
                var sb = new StringBuilder();
                var count = 0;
                string text = ClipboardText;
                foreach (var ch in ClipboardText)
                {
                    sb.AppendFormat("0x{0:x2}, ", (int)ch);
                    if (++count == 16)
                    {
                        count = 0;
                        sb.AppendLine();
                    }
                }
                Paste(application, sb.ToString());
            }
        }

        /// <summary>
        /// Pastes as verbatim string.
        /// </summary>
        /// <param name="application">The application.</param>
        public void PasteAsVerbatimString(DTE2 application)
        {
            if (IsVb(application))
            {
                //vb14 has verbatim strings, otherwise do the CData trick
                int version;
                var appVersion = application.Version;
                var p = appVersion.IndexOf('.'); //12.0 in VS2013, but MSDN says dp is optional
                if (p > 0) appVersion = appVersion.Substring(0, p);

                int.TryParse(appVersion, out version);

                Paste(application,
                    version < 14
                        ? SmartFormatter.CDataizeInVb(ClipboardText)
                        : SmartFormatter.StringinizeInVb(ClipboardText));
                return;
            }
            //c#
            Paste(application, SmartFormatter.StringinizeInCs(ClipboardText));
        }

        /// <summary>
        /// Public method to paste and format clipboard text as comment the cursor 
        /// location for the configured or active window's langage .
        /// </summary>
        /// <param name="application">application to insert</param>
        public void PasteAsComment(DTE2 application)
        {
            string text;
            if (IsVb(application))
            {
                text = SmartFormatter.CommentizeInVb(ClipboardText);
            }
            else if (IsXml(application))
            {
                text = SmartFormatter.CommentizeInXml(ClipboardText);
            }
            else
            {
                text = SmartFormatter.CommentizeInCs(ClipboardText);
            }

            text = ResgataNomeParaComentario(text);
            Paste(application, text);
        }

        /// <summary>
        /// Public method to paste format clipboard text into a specified region
        /// </summary>
        /// <param name="application">application to insert</param>
        public void PasteAsRegion(DTE2 application)
        {
            //get the region name
            const string region = "myRegion";

            //it's so simple, we really don't need a function
            string csRegionized = "#region " + region + Environment.NewLine + SmartFormatter.LimparTexto(ClipboardText) + Environment.NewLine + "#endregion";

            //and paste
            Paste(application, csRegionized);
        }

        /// <summary>
        /// Public method to paste and format clipboard text as stringbuilder the cursor 
        /// location for the configured or active window's langage .
        /// </summary>
        /// <param name="application">application to insert</param>
        public void PasteAsStringBuilder(DTE2 application)
        {
            const string stringbuilder = "sb";
            Paste(application, IsVb(application) ?
                SmartFormatter.StringbuilderizeInVb(SmartFormatter.LimparTexto(ClipboardText), stringbuilder) :
                SmartFormatter.StringbuilderizeInCs(SmartFormatter.LimparTexto(ClipboardText), stringbuilder));
        }

        public void PasteWithReplace(DTE2 application)
        {
            using (var replaceForm = new ReplaceForm())
            {
                if (replaceForm.ShowDialog() == DialogResult.OK)
                {
                    if (replaceForm.Regex)
                    {
                        var src = replaceForm.TextToReplace;
                        var dst = replaceForm.ReplaceText;
                        var text = Regex.Replace(ClipboardText, src, dst);
                        Paste(application, text);
                    }
                    else
                    {
                        var src = replaceForm.TextToReplace;
                        var dst = replaceForm.ReplaceText;
                        var text = ClipboardText.Replace(src, dst);
                        Paste(application, text);
                    }
                }
            }
        }

        public void PasteAsUpperCamelCase(DTE2 application)
        {
            Paste(application, TranformaEmNotacaoCameloMaiuscula(SmartFormatter.LimparTexto(RemoverAcentos( ClipboardText))));
        }

        public void PasteAsLowerCamelCase(DTE2 application)
        {
            Paste(application, TranformaEmNotacaoCameloMinuscula(SmartFormatter.LimparTexto(RemoverAcentos(ClipboardText))));
        }

        #endregion
    }
}
