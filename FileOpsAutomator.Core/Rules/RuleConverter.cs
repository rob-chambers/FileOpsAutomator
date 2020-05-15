using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace FileOpsAutomator.Core.Rules
{
    internal class RuleConverter : JsonConverter
    {
        private static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() 
        {
            ContractResolver = new ConcreteClassConverter<Rule>()
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Rule);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            var type = jo[nameof(Rule.Type)].Value<int>();
            var json = jo.ToString();
            if (Enum.IsDefined(typeof(RuleType), type))
            {
                switch ((RuleType)type)
                {
                    case RuleType.MoveRule:                        
                        return JsonConvert.DeserializeObject<MoveRule>(json, SpecifiedSubclassConversion);
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