using System;
using System.Collections.Generic;

namespace SoLib.CognitiveService.ComputerVision
{
    public class AnalyzeImageResult
    {
        public List<Category> Categories { get; set; }
        public Adult Adult { get; set; }
        public List<Tag> Tags { get; set; }
        public Description Description { get; set; }
        public Guid RequestID { get; set; }
        public Metadata Metadata { get; set; }
        public List<Face> Faces { get; set; }
        public Color Color { get; set; }
        public Imagetype ImageType { get; set; }
    }

    public class Adult
    {
        public bool IsAdultContent { get; set; }
        public bool IsRacyContent { get; set; }
        public double AdultScore { get; set; }
        public double RacyScore { get; set; }
    }

    public class Description
    {
        public List<string> Tags { get; set; }
        public List<Caption> Captions { get; set; }
    }

    public class Caption
    {
        public string Text { get; set; }
        public double Confidence { get; set; }
    }

    public class Metadata
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Format { get; set; }
    }

    public class Color
    {
        public string DominantColorForeground { get; set; }
        public string DominantColorBackground { get; set; }
        public List<string> DominantColors { get; set; }
        public string AccentColor { get; set; }
        public bool IsBWImg { get; set; }
    }

    public class Imagetype
    {
        public ClipArtType ClipArtType { get; set; }
        public LineDrawingType LineDrawingType { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }
        public float Score { get; set; }
        public Detail Detail { get; set; }
    }

    public class Detail
    {
        public List<Celebrity> Celebrities { get; set; }
    }

    public class Celebrity
    {
        public string Name { get; set; }
        public FaceRectangle FaceRectangle { get; set; }
        public double Confidence { get; set; }
    }

    public class FaceRectangle
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class Tag
    {
        public string Name { get; set; }
        public float Confidence { get; set; }
    }

    public class Face
    {
        public int Age { get; set; }
        public string Gender { get; set; }
        public FaceRectangle FaceRectangle { get; set; }
    }

    public enum ClipArtType
    {
        NonClipArt = 0,
        Ambiguous = 1,
        NormalClipArt = 2,
        GoodClipArt = 3
    }

    public enum LineDrawingType
    {
        NonLineDrawing = 0,
        LineDrawing = 1
    }

    public class ErrorResult
    {
        public string Code { get; set; }

        public Guid RequestID { get; set; }

        public string Message { get; set; }
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
