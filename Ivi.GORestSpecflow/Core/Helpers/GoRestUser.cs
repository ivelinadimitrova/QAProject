using Newtonsoft.Json;

namespace Ivi.GORestSpecflow.Core.Helpers
{
    public class GoRestUser
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public string Gender { get; set; }
        [JsonProperty]
        public string Email { get; set; }
        [JsonProperty]
        public string Status { get; set; }
    }

    public class GoRestRequestUser
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
