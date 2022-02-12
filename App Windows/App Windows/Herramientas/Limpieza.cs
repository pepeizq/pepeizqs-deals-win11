using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Net;

namespace Herramientas
{
    public static class Limpieza
    {
        public static string Limpiar(string texto)
        {
            if (texto != null)
            {
                texto = texto.Trim();

                if (texto.Contains("DLC"))
                {
                    int temp = texto.IndexOf("DLC");

                    if (temp == texto.Length - 3)
                    {
                        texto = texto.Remove(texto.Length - 3, 3);
                    }
                }

                texto = WebUtility.HtmlDecode(texto);

                List<string> listaCaracteres = new List<string>
                {
                    "(Mac)", "(Mac & Linux)", "(Linux)", "(Steam)", "(Epic)", "(GOG)", "Early Access", "Pre Order", "Pre-Purchase",
                    " ", "•", ">", "<", "¿", "?", "!", "¡", ":", "â", "Â", "¢",
                    ".", "_", "–", "-", ";", ",", "™", "®", "'", "’", "´", "`", "(", ")", "/", "|", "=", "+", Strings.ChrW(34).ToString(),
                    "@", "^", "[", "]", "ª", "«"
                };

                foreach (string caracter in listaCaracteres)
                {
                    texto = texto.Replace(caracter, null);
                }

                texto = texto.ToLower();
                texto = texto.Trim();
            }

            return texto;
        }
    }
}
