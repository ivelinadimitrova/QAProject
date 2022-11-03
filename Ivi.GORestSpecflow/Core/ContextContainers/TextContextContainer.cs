namespace Ivi.GORestSpecflow.Core.ContextContainers
{
    public class TextContextContainer
    {
        public HttpClient HttpClient { get; set; }
        public HttpResponseMessage Response { get; set; }
        public int Id { get; set; }
    }
}
