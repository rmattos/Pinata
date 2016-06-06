using System.Collections.Generic;
using FluentAssertions;
using Pinata.Core;
using Pinata.Data;
using Xunit;

namespace Piñata.UnitTest
{
    public class PinataTest
    {
        protected Pinata.Pinata _sutPinata = null;

        public PinataTest()
        {
            _sutPinata = new Pinata.Pinata("", Provider.MySQL, "Sample/data.json");
        }

        public class Load : PinataTest
        {
            [Fact]
            public void Given_a_Valid_Sample_Path_Should_Return_A_List_Of_Sample_Data()
            {
                _sutPinata.Load().Should().BeOfType<List<SampleData>>();
            }
        }
    }
}
