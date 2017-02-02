using System;

namespace SoLib.CognitiveService.Common
{
    public class ErrorResult : IResult
    {
        public string Code { get; set; }

        public Guid RequestID { get; set; }

        public string Message { get; set; }
    }
}
