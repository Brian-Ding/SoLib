using System.Collections.Generic;

namespace SoLib.Common
{
    public interface INode<T>
    {
        List<T> Children { get; set; }
        string Path { get; set; }
    }
}
