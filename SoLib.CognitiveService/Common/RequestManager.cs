using System.Collections.Generic;
using System.Net.Http;

namespace SoLib.CognitiveService.Common
{
    public class RequestManager
    {
        public string SendRequest(string subscriptionKey, string imageUrl, VisualFeature[] visualFeatures, string details, Language language = Language.en)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if (visualFeatures.Length > 0)
            {
                string features = string.Empty;
                foreach (var feature in visualFeatures)
                {
                    features += feature.ToString() + ",";
                }
                features = features.TrimEnd(',');

                parameters.Add("visualFeatures", features);
            }
            if (!string.IsNullOrEmpty(details))
            {
                parameters.Add("details", details);
            }
            parameters.Add("language", language.ToString());

            string uri = "https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze?";
            foreach (var pair in parameters)
            {
                uri += $"{pair.Key}={pair.Value}&";
            }
            uri = uri.TrimEnd('&');

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            StringContent content = new StringContent($"{{\"url\":\"{imageUrl}\"}}");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            //Task<HttpResponseMessage> responseMessageTask = httpClient.PostAsync(uri, content);
            string result = httpClient.PostAsync(uri, content).Result.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}
