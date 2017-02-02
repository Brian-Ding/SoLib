using SoLib.CognitiveService.Common;
using SoLib.CognitiveService.ComputerVision;

class Program
{
    static void Main(string[] args)
    {
        ComputerVisionManager manager = new ComputerVisionManager();
        IResult result = manager.AnalyzeImage("{subscription key}",
            "http://static.cnbetacdn.com/thumb/article/2017/0131/9a0adc9b1362043.jpg_600x600.jpg",
            //"https://imgsa.baidu.com/baike/c0%3Dbaike116%2C5%2C5%2C116%2C38/sign=9047a0f3af1ea8d39e2f7c56f6635b2b/c2fdfc039245d6887782a5ecadc27d1ed31b24f1.jpg",
            new VisualFeature[] {
                VisualFeature.Adult,
                VisualFeature.Categories,
                VisualFeature.Color,
                VisualFeature.Description,
                VisualFeature.Faces,
                VisualFeature.ImageType,
                VisualFeature.Tags },
            string.Empty, Language.en);
        
    }
}