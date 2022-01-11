using System;

namespace EveStats.Service.Helpers.Extensions
{
    /// <summary>
    /// These extensions help format strings.
    /// </summary>
    public static class StringTools
    {
        /// <summary>
        /// converts first character to upper.
        /// </summary>
        /// <example>
        ///     For example:
        ///     <code>
        ///         string text = "word"
        ///         string newText = StringTools.FirstCharToUpper(text);
        ///     </code>
        ///     returns <c>text</c> as "Word".
        /// </example>
        /// <param name="input">any string, preferably one word or value.</param>
        /// <returns><paramref name="input"/> with the first character in upper case.</returns>
        /// <exception cref="ArgumentNullException">created if the received value is null.</exception>
        /// <exception cref="ArgumentException">created if the received value is an empty string.</exception>
        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty.", nameof(input)),
                _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
            };

        /* public static string Formatter(this string[] input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                _ => new StringFormatter(input).
            }*/
    }
}
