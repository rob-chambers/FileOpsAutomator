using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace FileOpsAutomator.Core.Filters
{
    internal class FilterConverter : JsonConverter
    {
        private static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings()
        {
            ContractResolver = new ConcreteClassConverter<Filter>()
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Filter);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            var type = jo[nameof(Filter.Type)].Value<int>();
            var json = jo.ToString();
            if (Enum.IsDefined(typeof(FilterType), type))
            {
                switch ((FilterType)type)
                {
                    case FilterType.StartsWith:                        
                        return JsonConvert.DeserializeObject<StartsWithFilter>(json, SpecifiedSubclassConversion);

                    case FilterType.EndsWith:
                        return JsonConvert.DeserializeObject<EndsWithFilter>(json, SpecifiedSubclassConversion);

                    case FilterType.ExactMatch:
                        return JsonConvert.DeserializeObject<ExactMatchFilter>(json, SpecifiedSubclassConversion);
                }
            }

            return null;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // won't be called because CanWrite returns false
        }
    }
}