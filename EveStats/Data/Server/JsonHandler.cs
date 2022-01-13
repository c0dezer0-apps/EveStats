using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EveStats.Data.Server
{
    public static class JsonHandler
    {
        /// <summary>
        /// Returns JSON as an Object from the specified file.
        /// </summary>
        /// <example>
        ///     <code>
        ///         ParseJson(C:/text.json);
        ///     </code>
        /// </example>
        /// <param name="filePath">The path to the JSON file.</param>
        /// <value>
        ///     <para><c>r</c> reads file to the end.</para>
        ///     <para><c>json</c> contains the contents of the file.</para>
        ///     <para><c>arr</c> converts the contents into a dotnet object.</para>
        /// </value>
        public static dynamic ParseJson(string filePath)
        {
            using StreamReader r = new StreamReader($"../Data/Resources/{filePath}");
            string json = r.ReadToEnd();
            dynamic jsonO = JsonConvert.DeserializeObject(json);

            return jsonO;
        }

        /// <summary>
        /// Writes changes to a JSON file.
        /// </summary>
        /// <param name="json">The modified object.</param>
        /// <param name="filePath">The url to the JSON file.</param>
        /// <value><c>w</c> opens file for writing.</value>
        public static void StoreJson(dynamic json, string filePath)
        {
            string pathToApp = AppContext.BaseDirectory;

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Include;

            using StreamWriter sw = new ($"../Data/Resources/{filePath}");
            using JsonWriter writer = new JsonTextWriter(sw);
            serializer.Serialize(writer, json);
        }
    }
}
