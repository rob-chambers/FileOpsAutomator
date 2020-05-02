using System.Collections.Generic;

namespace FileOpsAutomator.Core
{
    public class KnownOperations
    {
        public KnownOperations()
        {
            //Operations.Add(new Operation
            //{

            //})
        }

        public List<Operation> Operations { get; set; }
    }

    public class Operation
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
