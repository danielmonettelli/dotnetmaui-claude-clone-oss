namespace Claude.Services
{
    public class AnthropicServiceFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AnthropicServiceFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IAnthropicService CreateService(bool useSimulation)
        {
            if (useSimulation || string.IsNullOrEmpty(ApiConstants.ANTHROPIC_API_KEY) || ApiConstants.ANTHROPIC_API_KEY == "your_api_key_here")
            {
                return new SimulatedAnthropicService();
            }
            else
            {
                HttpClient httpClient = _httpClientFactory.CreateClient();
                return new AnthropicService(httpClient);
            }
        }
    }
}