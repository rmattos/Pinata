using FluentAssertions;
using Pinata.Command;
using Pinata.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using Xunit;

namespace Piñata.UnitTest
{
    public class PinataMongoTest
    {
        protected Pinata.Pinata _sutPinata = null;

        public PinataMongoTest()
        {
            _sutPinata = new Pinata.Pinata(ConfigurationManager.ConnectionStrings["soclminer_mongodb"].ToString(), Provider.Type.MongoDB);
        }

        public class Execute : PinataMongoTest
        {
            public class Given_A_Insert_Command : IClassFixture<Execute>
            {
                private Pinata.Pinata _sutPinata = null;

                public Given_A_Insert_Command(Execute fixture)
                {
                    _sutPinata = fixture._sutPinata;
                    _sutPinata.Feed("Sample/mongoData.json");
                    _sutPinata.Execute(CommandType.Delete);
                }

                [Fact]
                public void When_Execute_Should_Return_True()
                {
                    _sutPinata.Execute(CommandType.Insert).Should().BeTrue();
                }
            }
        }

        public class ExecuteWithParameters : PinataMongoTest
        {
            public class Given_A_Insert_Command : IClassFixture<ExecuteWithParameters>
            {
                private Pinata.Pinata _sutPinata = null;

                private IDictionary<string, string> parameters;

                public Given_A_Insert_Command(ExecuteWithParameters fixture)
                {
                    _sutPinata = fixture._sutPinata;
                    _sutPinata.Feed("Sample/mongoDataWithParameters.json");

                    parameters = new Dictionary<string, string>();

                    parameters.Add("user_id_01", "57cf3ac41c61d08a18c832c2");
                    parameters.Add("user_id_02", "57cf3ad71c61d08a18c832c3");

                    parameters.Add("user_01_email", "second_user@anotheremail.com");
                    parameters.Add("user_02_email", "first_user@email.com");

                    parameters.Add("user_01_age", "36");
                    parameters.Add("user_02_age", "66");

                    parameters.Add("user_01_birthday", new DateTime(1980, 09, 01).ToString());
                    parameters.Add("user_02_birthday", new DateTime(1950, 01, 03).ToString());

                    parameters.Add("user_01_interest", "[ \" sports\" ]");
                    parameters.Add("user_02_interest", "[ \" sports\", \"anime\", \"series\" ]");

                    parameters.Add("user_01_address", "{ \"Street\" : \"Viaduto do Cha, 15\", \"District\" : \"Centro\", \"City\" : \"São Paulo\", \"State\" : \"SP\" }");
                    parameters.Add("user_02_address", "{ \"Street\" : \"Av. Brasil, 2971\", \"District\" : \"Compensa\", \"City\" : \"Manaus\", \"State\" : \"AM\" }");

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
