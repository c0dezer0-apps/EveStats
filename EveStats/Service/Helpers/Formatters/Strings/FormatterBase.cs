using System;
using EveStats.Service.Helpers.Extensions;

namespace EveStats.Service.Helpers.Formatters.Strings
{
    /// <summary>
    ///     For formatting strings in various cases.
    /// </summary>
    /// <value><c>ArrayOfInputs</c> is an array of strings to format.</value>
    /// <value><c>SingleInput</c> is a single <c>string</c> to format.</value>
    /// <value><c>ArrayOfOutputs</c> is the altered <c>ArrayOfInputs</c>.</value>
    /// <value><c>Output</c> is the altered <c>SingleInput</c>.</value>
    public class FormatterBase
    {
        protected string[] ArrayOfInputs { get; set; }
        protected string[] ArrayOfOutputs { get; set; }
        protected string Input { get; set; }
        protected string Output { get; set; }

        /// <summary>
        /// Initializes the default values with supplied parameters. 
        /// </summary>
        /// <param name="arrOfInputs">Takes an array of strings.</param>
        internal void Initialize(string[] arrOfInputs)
        {
            ArrayOfInputs = arrOfInputs;
            ArrayOfOutputs = Array.Empty<string>();
            Input = string.Empty;
            Output = string.Empty;
        }

        /// <param name="input">Takes a single string.</param>
        internal void Initialize(string input)
        {
            ArrayOfInputs = Array.Empty<string>();
            ArrayOfOutputs = Array.Empty<string>();
            Input = input;
            Output = string.Empty;
        }

        /// <summary>
        /// Makes sure a supplied string is neither null nor empty.
        /// </summary>
        /// <example>
        ///     <para>
        ///         If <paramref name="input"/> is correctly supplied:
        ///         <code>
        ///             string input = "Hello";
        ///             StringValidator(input);
        ///         </code>
        ///         will return <c>true</c>.
        ///     </para>
        ///     <para>
        ///         If <paramref name="input"/> is empty or null.
        ///         <code>
        ///             string input = "" // or null;
        ///             StringValidator(input);
        ///         </code>
        ///         will result in an exception being thrown.
        ///     </para>
        /// </example>
        /// <param name="input">Represents a string.</param>
        /// <returns>The supplied string.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        internal static string StringValidator(string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty.", nameof(input)),
                _ => input
            };

        /// <summary>
        /// Converts snake_case strings to CamelCase.
        /// </summary>
        /// <example>
        ///     For example:
        ///     <code>
        ///         SnakeToCamelCase("camel_case");
        ///     </code>
        ///     returns "CamelCase"
        /// </example>
        /// <param name="camel_cased_word">camel_case strings. (no spaces)</param>
        /// <value>Prop <c>_arr</c> is <c>camel_cased_word</c> split into an array of separate strings.</value>
        /// <value>Prop <c>_camelCasedWord</c> is the final result.</value>
        /// <value>Prop <c>Output</c> is a class Property representing the result.</value>
        /// <returns>The modified instance.</returns>
        public FormatterBase SnakeToCamelCase(string camel_cased_word)
        {
            string[] arr = camel_cased_word.Split('_');
            string camelCasedWord;

            // UpperCase each index.
            for (var i = 0; i < arr.Length; i++)
                arr[i] = StringTools.FirstCharToUpper(arr[i]);
            

            camelCasedWord = String.Join("", arr);
            Output = camelCasedWord;

            return this;
        }

        /// <summary>
        ///     Overload that takes an array instead of a single word.
        /// </summary>
        /// <param name="arr_of_camel_cased_words"><c>Array</c> of camel_cased strings.</param>
        /// <value><c>index</c> is an incrementor for array index.</value>
        /// <value><c>camelCasedWord</c> is the result to be added to <c>ArrayOfOutputs</c>.</value>
        /// <value><c>arr</c> is the camel_cased string split at the underscore into two elements.</value>
        /// <value><c>ArrayOfOutputs</c> is a class property.</value>
        /// <returns>The instance.</returns>
        /// <example>
        ///     <code>
        ///         private string[] _arrOfCamelCase = ["test_one", "test_two"];
        ///         FormatterBase t = new(_arrOfCamelCase);
        ///         t.SnakeToCamelCase(_arrOfCamelCase);
        ///         
        ///         Console.Out.WriteLine(t.Result);
        ///     </code>
        ///         
        /// </example>
        public FormatterBase SnakeToCamelCase(string[] arr_of_camel_cased_words)
        {
            int index = 0;
            string camelCasedWord;
            string[] arr = System.Array.Empty<string>();
            ArrayOfOutputs = new string[arr_of_camel_cased_words.Length];

            foreach (var str in arr_of_camel_cased_words)
            {
                arr = arr_of_camel_cased_words[index].Split('_');

                for (var i = 0; i < arr.Length; i++)
                    arr[i] = StringTools.FirstCharToUpper(arr[i]);

                camelCasedWord = String.Join("", arr);
                ArrayOfOutputs[index] = camelCasedWord;
                index++;
            }

            return this;
        }

    }
}
