using FluentAssertions;
using Pinata.Command;
using Pinata.Data;
using System.Collections.Generic;
using System.Configuration;
using Xunit;

namespace Piñata.UnitTest
{
    public class PinataTest
    {
        protected Pinata.Pinata _sutPinata = null;

        public PinataTest()
        {
            _sutPinata = new Pinata.Pinata(ConfigurationManager.ConnectionStrings["soclminer_mysql"].ToString(), Provider.Type.MySQL);

        }

        public class Execute : PinataTest
        {
            public class Given_A_Insert_Command : IClassFixture<Execute>
            {
                private Pinata.Pinata _sutPinata = null;

                public Given_A_Insert_Command(Execute fixture)
                {
                    _sutPinata = fixture._sutPinata;
                    _sutPinata.Feed("Sample/sqlData.json");
                    _sutPinata.Execute(CommandType.Delete);
                }

                [Fact]
                public void When_Execute_Should_Return_True()
                {
                    _sutPinata.Execute(CommandType.Insert).Should().BeTrue();
                }
            }
        }

        public class ExecuteWithParameters : PinataTest
        {
            public class Given_A_Insert_Command : IClassFixture<ExecuteWithParameters>
            {
                private Pinata.Pinata _sutPinata = null;
                private IDictionary<string, string> parameters;

                public Given_A_Insert_Command(ExecuteWithParameters fixture)
                {
                    _sutPinata = fixture._sutPinata;
                    _sutPinata.Feed("Sample/sqlDataWithParameters.json");

                    parameters = new Dictionary<string, string>();
                    parameters.Add("movie_01", "The Color Purple");
                    parameters.Add("movie_02", "Lights Out");
                    parameters.Add("movie_03", "The Danish Girl");

                    parameters.Add("actor_01", "Danny Glover");
                    parameters.Add("actor_02", "Whoopi Goldberg");
                    parameters.Add("actor_03", "Eddie Redmayne");
                    parameters.Add("actor_04", "Teresa Palmer");

                    _sutPinata.Execute(CommandType.Delete, parameters);
                }

                [Fact]
                public void When_Execute_Should_Return_True()
                {
                    _sutPinata.Execute(CommandType.Insert, parameters).Should().BeTrue();
                }
            }
        }
    }
}