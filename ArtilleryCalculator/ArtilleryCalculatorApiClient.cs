using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ArtilleryCalculator
{
    class ArtilleryCalculatorApiClient
    {
        const string API_BASE_URL = "https://3kpt0s0jod.execute-api.us-east-1.amazonaws.com/dev";

        protected HttpClient Client { get; }
        protected JsonSerializer Serializer { get; } = new JsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };


        public ArtilleryCalculatorApiClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public async Task<LatestVersionInformation> GetLatestVersionInformation()
        {
            try
            {
                var currentVersion = this.GetType().Assembly.GetName().Version;
                var url = $"{API_BASE_URL}/latest-version?version={currentVersion}";
                var response = await Client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var contentStream = await response.Content.ReadAsStreamAsync();

                using (var streamReader = new StreamReader(contentStream))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    return Serializer.Deserialize<LatestVersionInformation>(jsonReader);
                }
            }
            catch
            {
#if DEBUG
                throw;
#else
                // Silently fail on purpose. I don't want this to ever prevent using the app
                // and we do not have error reporting set up so this makes some sense.
                return null;
#endif
            }
        }

        public class LatestVersionInformation
        {
            public bool Outdated { get; set; }
            public string Version { get; set; }
            public string Name { get; set; }
            public string Body { get; set; }
            public string Url { get; set; }
            public DateTime PublishedAt { get; set; }
        }
    }
}
