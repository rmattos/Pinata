using System.Configuration;
using FluentAssertions;
using Pinata.Command;
using Pinata.Data;
using Xunit;

namespace Piñata.UnitTest
{
    public class PinataTest
    {
        protected Pinata.Pinata _sutPinata = null;

        public PinataTest()
        {
            _sutPinata = new Pinata.Pinata(ConfigurationManager.ConnectionStrings["soclminer_db_write"].ToString(), Provider.Type.MySQL, "Sample/data.json");
        }

        public class Load : PinataTest
        {
            [Fact]
            public void Given_A_Insert_When_Execute_Command_Should_Return_True()
            {
                _sutPinata.Feed();
                _sutPinata.Execute(CommandType.Insert).Should().BeTrue();
            }
        }
    }
}
