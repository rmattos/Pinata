using System.Collections.Generic;

namespace Pinata.Common
{
    public class SampleSQLData : BaseSampleData
    {
        public string Table { get; set; }

        public IList<References> FK_References { get; set; }
    }

    public class References
    {
        public string Table { get; set; }
    }
}
