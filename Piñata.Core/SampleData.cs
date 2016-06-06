using System.Collections.Generic;

namespace Pinata.Core
{
    public class SampleData
    {
        public string Table { get; set; }

        public IList<string> Keys { get; set; }

        public IList<Schema> Schema { get; set; }

        public IList<object> Rows { get; set; }

        public IList<References> FK_References { get; set; }
    }

    public class References
    {
        public string Table { get; set; }
    }

    public class Schema
    {
        public string Column { get; set; }

        public string Type { get; set; }
    }
}
