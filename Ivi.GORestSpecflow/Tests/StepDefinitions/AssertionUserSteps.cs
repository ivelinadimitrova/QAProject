using Ivi.GORestSpecflow.Core.Config;
using Ivi.GORestSpecflow.Core.ContextContainers;
using Ivi.GORestSpecflow.Core.Helpers;
using Newtonsoft.Json;

namespace Ivi.GORestSpecflow.Tests.StepDefinitions
{
    [Binding]
    public class AssertionUserSteps
    {
        private readonly BaseConfig _baseConfig;
        private GoRestRequestUser _user;
        private TextContextContainer _context;

        public AssertionUserSteps(BaseConfig baseConfig, TextContextContainer context)
        {
            _baseConfig = baseConfig;
            _context = context;
        }

        //GET all users
        [Then(@"The response status code should be (.*)")]
        public void ThenTheResponseStatudCodeShouldBe(string statusCode)
        {
            _context.Response.StatusCode.ToString().Should().Be(statusCode);
        }

        [Then(@"the response should contain a list of users")]
        public void ThenTheResponseShouldContainAListOfUsers()
        {
            var content = _context.Response.Content.ReadAsStringAsync().Result;
            var expectedResponse = JsonConvert.DeserializeObject<List<GoRestUser>>(content);

            expectedResponse.Should().NotBeEmpty();
        }

        //Create a new user
        [Then(@"The user should be created successfully")]
        public void ThenTheUserShouldBeCreatedSuccessfully()
        {
            if (_context.Response.IsSuccessStatusCode == true)
            {
                var actualResponse = JsonConvert.DeserializeObject<GoRestUser>(_context.Response.Content.ReadAsStringAsync().Result);

                actualResponse.Id.Should().NotBe(null);
            }
            else if (_context.Response.IsSuccessStatusCode == false)
            {
                var errorMess = new ErrorMessage()
                {
                    Field = "name",
                    Message = "can't be blank"
                };

                var actualResponse = JsonConvert.DeserializeObject<List<ErrorMessage>>(_context.Response.Content.ReadAsStringAsync().Result);

                actualResponse[0].Field.Should().Contain(errorMess.Field);
                actualResponse[0].Message.Should().Contain(errorMess.Message);
            }
        }

        //Update an existing user
        [Then(@"The user should be updated successfully")]
        public void ThenTheUserShouldBeUpdatedSuccessfully()
        {
            var content = _context.Response.Content.ReadAsStringAsync().Result;
            var expectedResponse = JsonConvert.DeserializeObject<GoRestUser>(content);

            expectedResponse.Id.Should().Be(_context.Id);
        }
    }
}
