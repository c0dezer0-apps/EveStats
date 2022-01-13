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
    public class Formatter
    {
        private bool initialized = false;

        protected string[] ArrayOfInputs { get; set; }
        protected string[] ArrayOfOutputs { get; set; }
        protected string Input { get; set; }
        protected string Output { get; set; }
        protected bool Initialized
        {
            get { return initialized; }
            set { initialized = value; }
        }

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
            Initialized = true;
        }

        /// <param name="input">Takes a single string.</param>
        internal void Initialize(string input)
        {
            ArrayOfInputs = Array.Empty<string>();
            ArrayOfOutputs = Array.Empty<string>();
            Input = input;
            Output = string.Empty;
            Initialized = true;
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
        /// Only need so methods can be chained. Will create overloads as needed.
        /// </summary>
        public Formatter Split(string input, char separator)
        {
            if (!Initialized)
                Initialize(input);

            ArrayOfOutputs = input.Split(separator);

            return this;
        }

        public Formatter ReturnElementAtIndex(int pos)
        {
            if (!Initialized)
                throw new ArgumentException("You can't format an empty string!");

            if (ArrayOfInputs.Length < 1)
                Output = (string)StringValidator(Input).Split('.').GetValue(pos);
            else
            {
                for (int i = 0; i < ArrayOfInputs.Length; i++)
                {
                    ArrayOfOutputs[i] = (string)StringValidator(ArrayOfInputs[i]).Split('.').GetValue(pos);
                }
            }

            return this;
        }

        public Formatter ReturnElementAtIndex(string match)
        {
            if (!Initialized)
                throw new ArgumentException("You can't format an empty string!");
            

            return this;
        }

        /// <summary>
        ///     <para>Returns the specified index.</para>
        ///     <para>Takes a string in dot notation, splits at the dot, and returns the specified index that matches the supplied string.</para>
        /// </summary>
        /// <param name="input">
        ///     <para>Takes a string in dot notation</para>
        ///     <para>
        ///         <paramref name="input"/> represents an ESI Scope. 
        ///         <example>
        ///             esi-locations.read_location.v1
        ///         </example>
        ///     </para>
        /// </param>
        /// <param name="match">Takes a string.</param>
        /// <example>
        ///     <code>
        ///         private string _esiCharLocation = "esi-locations.read_location.v1";
        ///         ReturnElementAtIndex(_esiCharLocation, "read_location");
        ///     </code>
        ///     returns <c>"read_location"</c>.
        /// </example>
        /// <value><c>arr</c> is the split dot-notated string.</value>
        /// <value><c>Output</c> sets the class property.</value>
        /// <returns>The FormatterBase instance with the changes.</returns>
        public Formatter ReturnElementAtIndex(string input, string match)
        {
            if (!Initialized)
                Initialize(input);

            string[] arr = StringValidator(input).Split('.');
            Output = Array.Find(arr, element => element == match);

            return this;
        }

        /// <summary>
        ///     An overload of the previous method that uses index positon instead of a match.
        /// </summary>
        /// <param name="input">
        /// <para>Takes a string in dot notation</para>
        ///     <para>
        ///         <paramref name="input"/> represents an ESI Scope. 
        ///         <example>
        ///             esi-locations.read_location.v1
        ///         </example>
        ///     </para>
        /// </param>
        /// <param name="pos">Takes an integer.</param>
        /// <value><c>Output</c> sets the class property.</value>
        /// <returns>The FormatterBase instance with the supplied changes.</returns>
        public Formatter ReturnElementAtIndex(string input, int pos)
        {
            if(!Initialized)
                Initialize(input);

            Output = (string)StringValidator(input).Split('.').GetValue(pos);

            return this;
        }

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
        public Formatter SnakeToCamelCase(string camel_cased_word)
        {
            if (!Initialized)
                Initialize(camel_cased_word);

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
        public Formatter SnakeToCamelCase(string[] arr_of_camel_cased_words)
        {
            if (!Initialized)
                Initialize(arr_of_camel_cased_words);

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
