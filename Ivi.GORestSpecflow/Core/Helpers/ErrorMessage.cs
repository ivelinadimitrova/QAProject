using Newtonsoft.Json;

namespace Ivi.GORestSpecflow.Core.Helpers
{
    public class ErrorMessage
    {
        [JsonProperty("field")]
        public string Field { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
