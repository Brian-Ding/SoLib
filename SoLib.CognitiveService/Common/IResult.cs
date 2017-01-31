using System;

namespace SoLib.CognitiveService.Common
{
    public interface IResult
    {
        Guid RequestID { get; set; }
    }
}
