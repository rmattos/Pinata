using System.Configuration;
using FluentAssertions;
using Pinata.Command;
using Pinata.Data;
using Xunit;

namespace Piñata.UnitTest
{
    public class PinataMongoTest
    {
        protected Pinata.Pinata _sutPinata = null;

        public PinataMongoTest()
        {
            _sutPinata = new Pinata.Pinata(ConfigurationManager.ConnectionStrings["soclminer_mongodb"].ToString(), Provider.Type.MongoDB, "Sample/mongoData.json");
            _sutPinata.Feed();
            _sutPinata.Execute(CommandType.Delete);
        }

        public class Execute : PinataMongoTest
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
                    _sutPinata.Feed();
                    _sutPinata.Execute(CommandType.Insert).Should().BeTrue();
                }
            }
        }
    }
}
