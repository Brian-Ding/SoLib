using System;
using System.Collections.Generic;

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

        /// <summary>
        /// 
        /// </summary>
        double Top { get; set; }

        /// <summary>
        /// 
        /// </summary>
        double Left { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //List<Guid> ChildrenIDs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int Level { get; set; }
    }
}
