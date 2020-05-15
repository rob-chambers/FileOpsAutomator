using FileOpsAutomator.Core.Rules;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace FileOpsAutomator.Core
{
    public class JsonRuleRepository : IRuleRepository
    {
        private readonly static string FileName = @"rules.json";

        public Task<IEnumerable<Rule>> GetAllAsync()
        {
            var contents = File.ReadAllText(FileName);
            var rules = JsonConvert.DeserializeObject(contents, typeof(IEnumerable<Rule>));

            return Task.FromResult((IEnumerable<Rule>)rules);
        }

        public void WriteRules(IEnumerable<Rule> rules)
        {
            var contents = JsonConvert.SerializeObject(rules, Formatting.Indented);
            File.WriteAllText(FileName, contents);
        }
    }
}
