using FileOpsAutomator.Core.Rules;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileOpsAutomator.Core
{
    public interface IRuleRepository
    {
        Task<IEnumerable<Rule>> GetAllAsync();
        void WriteRules(IEnumerable<Rule> rules);
    }
}