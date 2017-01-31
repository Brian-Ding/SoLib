using Newtonsoft.Json;
using SoLib.CognitiveService.Common;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SoLib.CognitiveService.ComputerVision
{
    public class Manager
    {
        /// <summary>
        /// This operation extracts a rich set of visual features based on the image content
        /// </summary>
        /// <param name="subscriptionKey">Subscription key which provides access to this API.</param>
        /// <param name="imageUrl">Image url.</param>
        /// <param name="visualFeatures">Indicating what visual feature types to return</param>
        /// <param name="details">A string indicating which domain-specific details to return. Can be empty or "Celebrities".</param>
        /// <param name="language">A string indicating which language to return. Part of visual features are english only.</param>
        /// <returns></returns>
        public IResult AnalyzeImage(string subscriptionKey, string imageUrl, VisualFeature[] visualFeatures = null, string details = null, Language language = Language.en)
        {
            HttpResponseMessage responseMessage = BuildClient(subscriptionKey).PostAsync(BuildUri(visualFeatures, details, language), BuildContent(imageUrl)).Result;
            string json = responseMessage.Content.ReadAsStringAsync().Result;

            return GetResult(responseMessage.StatusCode, json);
        }

        /// <summary>
        /// This operation extracts a rich set of visual features based on the image content
        /// </summary>
        /// <param name="subscriptionKey">Subscription key which provides access to this API.</param>
        /// <param name="imageUrl">Image url.</param>
        /// <param name="visualFeatures">Indicating what visual feature types to return</param>
        /// <param name="details">A string indicating which domain-specific details to return. Can be empty or "Celebrities".</param>
        /// <param name="language">A string indicating which language to return. Part of visual features are english only.</param>
        /// <returns></returns>
        public async Task<IResult> AnalyzeImageAsync(string subscriptionKey, string imageUrl, VisualFeature[] visualFeatures = null, string details = null, Language language = Language.en)
        {
            HttpResponseMessage responseMessage = await BuildClient(subscriptionKey).PostAsync(BuildUri(visualFeatures, details, language), BuildContent(imageUrl));
            string json = await responseMessage.Content.ReadAsStringAsync();

            return GetResult(responseMessage.StatusCode, json);
        }

        private string BuildUri(VisualFeature[] visualFeatures = null, string details = null, Language language = Language.en)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if (visualFeatures != null && visualFeatures.Length > 0)
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

            return uri;
        }

        private HttpContent BuildContent(string imageUrl)
        {
            StringContent content = new StringContent($"{{\"url\":\"{imageUrl}\"}}");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return content;
        }

        private HttpClient BuildClient(string subscriptionKey)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            return httpClient;
        }

        private IResult GetResult(HttpStatusCode statusCode, string json)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<AnalyzeImageResult>(json);
            }
            else
            {
                return JsonConvert.DeserializeObject<ErrorResult>(json);
            }
        }
    }

    public enum VisualFeature
    {
        Categories,
        Tags,
        Description,
        Faces,
        ImageType,
        Color,
        Adult
    }

    public enum Language
    {
        en,
        zh
    }
}