using System;

namespace SoLib.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public interface IData
    {
        /// <summary>
        /// 
        /// </summary>
        Guid ParentID { get; }

        /// <summary>
        /// 
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// 
        /// </summary>
        string Relation { get; }
    }
}
