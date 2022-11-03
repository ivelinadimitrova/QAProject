using System.Text;
using Ivi.GORestSpecflow.Core.Config;
using Ivi.GORestSpecflow.Core.ContextContainers;
using Ivi.GORestSpecflow.Core.Helpers;
using Newtonsoft.Json;
using TechTalk.SpecFlow.Infrastructure;

namespace Ivi.GORestSpecflow.Core.Support
{
    namespace GoRestTests.Support
    {
        [Binding]
        public sealed class Hooks
        {
            private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
            private TextContextContainer _textContextContainer;
            private BaseConfig _baseConfig;

            public Hooks(ISpecFlowOutputHelper specFlowOutputHelper, TextContextContainer textContextContainer, BaseConfig baseConfig)
            {
                _specFlowOutputHelper = specFlowOutputHelper;
                _textContextContainer = textContextContainer;
                _baseConfig = baseConfig;
            }

            [BeforeScenario]
            public void TearUp()
            {
                _textContextContainer.HttpClient = new HttpClient();
            }

            [BeforeScenario("Authenticate")]
            public void Authenticate()
            {
                _textContextContainer.HttpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClientConfig.Token);
            }

            //[AfterScenario]
            //public void TearDown()
            //{
                //var request = JsonConvert.SerializeObject(_user.Id.Equals(_textContextContainer.Id));
                //var requestBody = new StringContent(request, Encoding.UTF8, "application/json");

                //var msgBodyId = new HttpRequestMessage
                //{
                //    Method = HttpMethod.Delete,
                //    RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}{_textContextContainer.Id}"),
                //    Content = requestBody
                //};

                //_textContextContainer.Response = _textContextContainer.HttpClient.SendAsync(msgBodyId).Result;
            //}
        }
    }
}
