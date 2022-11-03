using System.Text;
using Ivi.GORestSpecflow.Core.Config;
using Ivi.GORestSpecflow.Core.ContextContainers;
using Ivi.GORestSpecflow.Core.Helpers;
using Newtonsoft.Json;

namespace Ivi.GORestSpecflow.StepDefinitions
{
    [Binding]
    public class UserSteps
    {
        private readonly BaseConfig _baseConfig;
        private GoRestRequestUser _user;
        private TextContextContainer _context;

        public UserSteps(BaseConfig baseConfig, TextContextContainer context)
        {
            _baseConfig = baseConfig;
            _context = context;
        }

        //GET all users
        [Given(@"I want to prepare a request")]
        public void GivenIWantToPrepareARequest()
        {

        }

        [When(@"I get all users from the (.*) endpoint")]
        public void WhenIGetAllUsersFromTheEndpoint(string endpoint)
        {
            _context.Response = _context.HttpClient.GetAsync($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}").Result;
        }

        //Create a new user
        [Given(@"I have the following user data (.*), (.*), (.*), (.*)")]
        public void GivenIHaveTheFollowingData(string name, string email, string gender, string status)
        {
            _user = new GoRestRequestUser()
            {
                Name = name,
                Email = email,
                Gender = gender,
                Status = status
            };
        }

        [When(@"I send a request to the (.*) endpoint")]
        public void WhenISendARequestToTheUsersEndpoint(string endpoint)
        {
            var request = JsonConvert.SerializeObject(_user);
            var requestBody = new StringContent(request, Encoding.UTF8, "application/json");

            var msgBody = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}"),
                Content = requestBody
            };

            _context.Response = _context.HttpClient.SendAsync(msgBody).Result;
        }

        //Update an existing user
        [Given(@"I have a created user already")]
        public void GivenIHaveACreatedUserAlready()
        {
             var response = CreateUser();
            var userToUpdate = JsonConvert.DeserializeObject<GoRestUser>(response.Content.ReadAsStringAsync().Result);
            _context.Id = userToUpdate.Id;
        }

        [When(@"I send update request to the (.*) endpoint")]
        public void WhenISendUpdateRequestToTheUsersEndpoint(string endpoint)
        {
            var updateUser = new GoRestRequestUser
            {
                Name = "UpdJen",
                Email = "update@d.me",
                Gender = "female",
                Status = "active"
            };

            var requestBody = new StringContent(JsonConvert.SerializeObject(updateUser), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}/{_context.Id}"),
                Content = requestBody
            };

            _context.Response = _context.HttpClient.SendAsync(request).Result;
        }

        private HttpResponseMessage CreateUser()
        {
            var user = new GoRestRequestUser
            {
                Name = "Janna",
                Email = "ra@dom.com",
                Gender = "female",
                Status = "active"
            };

            var requestBody = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}users"),
                Content = requestBody
            };

            return _context.HttpClient.SendAsync(request).Result;
        }
    }
}