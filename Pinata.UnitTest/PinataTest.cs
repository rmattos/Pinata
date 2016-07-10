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
            _sutPinata = new Pinata.Pinata(ConfigurationManager.ConnectionStrings["soclminer_mysql"].ToString(), Provider.Type.MySQL, "Sample/sqlData.json");
            _sutPinata.Feed();
            _sutPinata.Execute(CommandType.Delete);
        }

        public class Execute : PinataTest
        {
            public class Given_A_Insert_Command : IClassFixture<Execute>
            {
                private Pinata.Pinata _sutPinata = null;

                public Given_A_Insert_Command(Execute fixture)
                {
                    _sutPinata = fixture._sutPinata;
                }

                [Fact]
                public void When_Execute_Should_Return_True()
                {
                    _sutPinata.Execute(CommandType.Insert).Should().BeTrue();
                }
            }
        }
    }
}
