using System;
using System.Text.RegularExpressions;

namespace Chat.Common
{
    public static class StringExtensions
    {
        /// <summary>
        /// Verifica se a string está vazia, nula ou se apenas contém espaços
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrWhiteSpace(this string input)
        {
            return string.IsNullOrEmpty(input) || input.Trim() == string.Empty;
        }

        /// <summary>
        /// Verifica se a string está vazia, nula ou se apenas contém espaços
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmptyOrWhiteSpace(this string input)
        {
            return !IsNullOrEmptyOrWhiteSpace(input);
        }

        /// <summary>
        /// Verifica se a string não começa com o padrão informado
        /// </summary>
        /// <param name="input">String a ser verificada</param>
        /// <param name="pattern">padrão procurado no início da string</param>
        /// <returns></returns>
        public static bool DoesNotStartWith(this string input, string pattern)
        {
            return string.IsNullOrEmpty(pattern) ||
                   input.IsNullOrEmptyOrWhiteSpace() ||
                   !input.StartsWith(pattern, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Garante que a string comece com a sentença informada, adicionando-a se necessário
        /// </summary>
        /// <param name="input">String a ser verificada</param>
        /// <param name="pattern">Início de string desejado</param>
        /// <param name="pattern2">Início opcional também aceito. Nulo para nao considerar essa opção</param>
        /// <returns></returns>
        public static string EnsureStartsWith(this string input, string pattern, string pattern2 = null)
        {
            if (string.IsNullOrEmpty(pattern))
                return input;

            if (input.IsNullOrEmptyOrWhiteSpace())
                return pattern;

            if (input.DoesNotStartWith(pattern))
            {
                if (pattern2.IsNullOrEmptyOrWhiteSpace() || input.DoesNotStartWith(pattern2))
                    input = pattern + input;
            }

            return input;
        }

        /// <summary>
        /// Garante que a string termine com a sentença informada, adicionando-a se necessário
        /// </summary>
        /// <param name="input">String a ser verificada</param>
        /// <param name="pattern">Início de string desejado</param>
        /// <returns></returns>
        public static string EnsureEndsWith(this string input, string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
                return input;

            if (input.IsNullOrEmptyOrWhiteSpace())
                return pattern;

            if (!input.EndsWith(pattern))
                input = input + pattern;

            return input;
        }

        /// <summary>
        /// Garante que a string contenha apenas caracteres alfanuméricos
        /// </summary>
        /// <param name="input">String a ser tratada</param>
        /// <returns></returns>        
        public static string EnsureAlphaNumeric(this string input)
        {
            if (input.IsNullOrEmptyOrWhiteSpace())
                return String.Empty;

            Regex rgx = new Regex("[^a-zA-Z0-9]");
            return rgx.Replace(input.Trim(), "");
        }

    }
}
