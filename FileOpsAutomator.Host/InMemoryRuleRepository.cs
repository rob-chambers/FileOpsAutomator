using FileOpsAutomator.Core;
using FileOpsAutomator.Core.Rules;
using FileOpsAutomator.Core.Filters;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FileOpsAutomator.Host
{
    internal class InMemoryRuleRepository : IRuleRepository
    {
        public Task<IEnumerable<Rule>> GetAllAsync()
        {
            var rules = new Collection<Rule>
            {
                new MoveRule
                {
                    SourceFolder = @"C:\Users\rob\Downloads",
                    DestinationFolder = @"C:\Users\rob\Downloads\FILE-AUTOMATE-TEST",
                    Filter = new EndsWithFilter("Spread betting Statement", ".pdf"),
                }
            };

            return Task.FromResult(rules.AsEnumerable());
        }
    }
}
