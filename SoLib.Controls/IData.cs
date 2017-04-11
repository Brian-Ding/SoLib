using System;
using System.Collections.Generic;
using Windows.UI.Xaml;

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
        double Top { get; set; }

        /// <summary>
        /// 
        /// </summary>
        double Left { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int Level { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object DataContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Relation { get; }
    }
}
