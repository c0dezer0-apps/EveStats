using System;
using EveStats.Service.Helpers.Extensions;

namespace EveStats.Service.Helpers
{
    /// <summary>\
    ///     Takes an array of strings, cleans them, and formats them.
    /// </summary>
    /// <remarks>
    ///     This is primarily used to create XML tag names for serializing the ESI scopes.
    /// </remarks>
    /// 
    public abstract class EsiXmlTagGenerator
    {
        internal abstract string ReturnMiddle(string scope);
        internal abstract string SnakeToCamelCase(string input);
    }

    public class StringFormatter : EsiXmlTagGenerator
    {
        protected string[] InitialArray { get; set; }
        protected string[] FormattedArray { get; set; }

        public StringFormatter Initialize(string[] arr)
        {
            InitialArray = arr;
            FormattedArray = System.Array.Empty<string>();

            return this;
        }

        /// <summary>
        ///     <para>Splits and returns the middle child.</para>
        ///     <para>Split turns dot-notated string into an array object which then returns the middle index casted as a string.</para>
        /// </summary>
        /// <param name="scope">
        ///     <para>Takes a string in dot notation</para>
        ///     <para>
        ///         <paramref name="scope"/> represents an ESI Scope. 
        ///         <example>
        ///             esi-locations.read_location.v1
        ///         </example>
        ///     </para>
        /// </param>
        /// <example> For example:
        ///     <code>
        ///         string DotNotated = "esi-locations.read_location.v1";
        ///         ReturnMiddle(DotNotated);
        ///     </code>
        ///     returns "read_location".
        /// </example>
        /// <returns>A casted value of the middle element.</returns>
        internal override string ReturnMiddle(string scope)
        {
            return (string) scope.Split('.').GetValue(1);
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
        /// <returns>string in CamelCase.</returns>
        internal override string SnakeToCamelCase(string camel_cased_word)
        {
            string[] _arr = camel_cased_word.Split("_");

            // UpperCase each index.
            for (var i = 0; i < _arr.Length; i++)
            {
                _arr[i] = StringTools.FirstCharToUpper(_arr[i]);
            }

            return String.Join("", _arr) ;
        }

        public string FormatString(string input)
        {

            ;
        }

    }
}
