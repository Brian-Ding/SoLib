using Newtonsoft.Json;
using SoLib.CognitiveService.Common;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// SoLib.CognitiveService.ComputerVision
/// </summary>
namespace SoLib.CognitiveService.ComputerVision
{
    /// <summary>
    /// ComputerVisionManager
    /// </summary>
    public class ComputerVisionManager
    {
        /// <summary>
        /// This operation extracts a rich set of visual features based on the image content.
        /// </summary>
        /// <param name="subscriptionKey">Subscription key which provides access to this API.</param>
        /// <param name="imageUrl">Image url.</param>
        /// <param name="visualFeatures">Indicating what visual feature types to return.</param>
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
        /// This operation extracts a rich set of visual features based on the image content.
        /// </summary>
        /// <param name="subscriptionKey">Subscription key which provides access to this API.</param>
        /// <param name="imageBuffer">Image binary data.</param>
        /// <param name="visualFeatures">Indicating what visual feature types to return.</param>
        /// <param name="details">A string indicating which domain-specific details to return. Can be empty or "Celebrities".</param>
        /// <param name="language">A string indicating which language to return. Part of visual features are english only.</param>
        /// <returns></returns>
        public IResult AnalyzeImage(string subscriptionKey, byte[] imageBuffer, VisualFeature[] visualFeatures = null, string details = null, Language language = Language.en)
        {
            HttpResponseMessage responseMessage = BuildClient(subscriptionKey).PostAsync(BuildUri(visualFeatures, details, language), BuildContent(imageBuffer)).Result;
            string json = responseMessage.Content.ReadAsStringAsync().Result;

            return GetResult(responseMessage.StatusCode, json);
        }

        /// <summary>
        /// This operation extracts a rich set of visual features based on the image content.
        /// </summary>
        /// <param name="subscriptionKey">Subscription key which provides access to this API.</param>
        /// <param name="imageStream">Image binary data.</param>
        /// <param name="visualFeatures">Indicating what visual feature types to return.</param>
        /// <param name="details">A string indicating which domain-specific details to return. Can be empty or "Celebrities".</param>
        /// <param name="language">A string indicating which language to return. Part of visual features are english only.</param>
        /// <returns></returns>
        public IResult AnalyzeImage(string subscriptionKey, Stream imageStream, VisualFeature[] visualFeatures = null, string details = null, Language language = Language.en)
        {
            HttpResponseMessage responseMessage = BuildClient(subscriptionKey).PostAsync(BuildUri(visualFeatures, details, language), BuildContent(imageStream)).Result;
            string json = responseMessage.Content.ReadAsStringAsync().Result;

            return GetResult(responseMessage.StatusCode, json);
        }

        /// <summary>
        /// This async operation extracts a rich set of visual features based on the image content.
        /// </summary>
        /// <param name="subscriptionKey">Subscription key which provides access to this API.</param>
        /// <param name="imageUrl">Image url.</param>
        /// <param name="visualFeatures">Indicating what visual feature types to return.</param>
        /// <param name="details">A string indicating which domain-specific details to return. Can be empty or "Celebrities".</param>
        /// <param name="language">A string indicating which language to return. Part of visual features are english only.</param>
        /// <returns></returns>
        public async Task<IResult> AnalyzeImageAsync(string subscriptionKey, string imageUrl, VisualFeature[] visualFeatures = null, string details = null, Language language = Language.en)
        {
            HttpResponseMessage responseMessage = await BuildClient(subscriptionKey).PostAsync(BuildUri(visualFeatures, details, language), BuildContent(imageUrl));
            string json = await responseMessage.Content.ReadAsStringAsync();

            return GetResult(responseMessage.StatusCode, json);
        }

        /// <summary>
        /// This async operation extracts a rich set of visual features based on the image content.
        /// </summary>
        /// <param name="subscriptionKey">Subscription key which provides access to this API.</param>
        /// <param name="imageBuffer">Image binary data.</param>
        /// <param name="visualFeatures">Indicating what visual feature types to return.</param>
        /// <param name="details">A string indicating which domain-specific details to return. Can be empty or "Celebrities".</param>
        /// <param name="language">A string indicating which language to return. Part of visual features are english only.</param>
        /// <returns></returns>
        public async Task<IResult> AnalyzeImageAsync(string subscriptionKey, byte[] imageBuffer, VisualFeature[] visualFeatures = null, string details = null, Language language = Language.en)
        {
            HttpResponseMessage responseMessage = await BuildClient(subscriptionKey).PostAsync(BuildUri(visualFeatures, details, language), BuildContent(imageBuffer));
            string json = await responseMessage.Content.ReadAsStringAsync();

            return GetResult(responseMessage.StatusCode, json);
        }

        /// <summary>
        /// This async operation extracts a rich set of visual features based on the image content.
        /// </summary>
        /// <param name="subscriptionKey">Subscription key which provides access to this API.</param>
        /// <param name="imageStream">Image binary data.</param>
        /// <param name="visualFeatures">Indicating what visual feature types to return.</param>
        /// <param name="details">A string indicating which domain-specific details to return. Can be empty or "Celebrities".</param>
        /// <param name="language">A string indicating which language to return. Part of visual features are english only.</param>
        /// <returns></returns>
        public async Task<IResult> AnalyzeImageAsync(string subscriptionKey, Stream imageStream, VisualFeature[] visualFeatures = null, string details = null, Language language = Language.en)
        {
            HttpResponseMessage responseMessage = await BuildClient(subscriptionKey).PostAsync(BuildUri(visualFeatures, details, language), BuildContent(imageStream));
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

        private HttpContent BuildContent(Stream imageStream)
        {
            byte[] buffer = new byte[imageStream.Length];
            imageStream.Read(buffer, 0, buffer.Length);
            ByteArrayContent content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return content;
        }

        private HttpContent BuildContent(byte[] imageBuffer)
        {
            ByteArrayContent content = new ByteArrayContent(imageBuffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

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

    /// <summary>
    /// VisualFeature
    /// </summary>
    public enum VisualFeature
    {
        /// <summary>
        /// Categories
        /// </summary>
        Categories,

        /// <summary>
        /// Tags
        /// </summary>
        Tags,

        /// <summary>
        /// Description
        /// </summary>
        Description,

        /// <summary>
        /// Faces
        /// </summary>
        Faces,

        /// <summary>
        /// ImageType
        /// </summary>
        ImageType,

        /// <summary>
        /// Color
        /// </summary>
        Color,

        /// <summary>
        /// Adult
        /// </summary>
        Adult
    }

    /// <summary>
    /// Language
    /// </summary>
    public enum Language
    {
        /// <summary>
        /// English
        /// </summary>
        en,

        /// <summary>
        /// 中文
        /// </summary>
        zh
    }
}