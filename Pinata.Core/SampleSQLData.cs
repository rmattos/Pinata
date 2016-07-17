using System.Collections.Generic;

namespace Pinata.Common
{
    public class SampleSQLData : BaseSampleData
    {
        public string Table { get; set; }

        public IList<References> FK_References { get; set; }

        public DeletePriority DeletePriority { get; set; }
    }

    public class References
    {
        public string Table { get; set; }
    }

    public enum DeletePriority
    {
        None = 0,
        Low,
        Medium,
        High
    }
}
