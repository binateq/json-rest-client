using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Binateq.JsonRestClient
{
    /// <summary>
    /// Describes settings of <see cref="JsonRestClient"/>.
    /// </summary>
    public class JsonRestClientSettings
    {
        /// <summary>
        /// Method used to serialize objects to JSON.
        /// </summary>
        public Func<object, string> Serialize { get; set; }

        /// <summary>
        /// Method used to deserialized objects from JSON.
        /// </summary>
        public Func<string, Type, object> Deserialize { get; set; }

        /// <summary>
        /// Indicates short array serialization.
        /// </summary>
        /// <remarks>
        /// If <c>true</c>, then arrays are formatted as <c>id=1,2,3,4</c>.
        /// If <c>false</c>, then arrays are formatted as <c>id=1&id=2&id=3&id=4</c>.
        /// </remarks>
        public bool IsShortArraySerialization { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="JsonRestClientSettings"/> type.
        /// </summary>
        /// <param name="settings">JSON serializer settings.</param>
        public JsonRestClientSettings(JsonSerializerSettings settings)
        {
            Serialize = (value) => JsonConvert.SerializeObject(value, settings);
            Deserialize = (value, type) => JsonConvert.DeserializeObject(value, type, settings);
            IsShortArraySerialization = false;
        }

        /// <summary>
        /// Initializes new instance of <see cref="JsonRestClientSettings"/> type.
        /// </summary>
        public JsonRestClientSettings()
            : this(new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            })
        { }
    }
}
