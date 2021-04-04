// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WindowDimensions.cs" company="Freiwillige Feuerwehr Krems/Donau">
//       Freiwillige Feuerwehr Krems/Donau
//       Austraße 33
//       A-3500 Krems/Donau
//       Austria
// 
//       Tel.:   +43 (0)2732 85522
//       Fax.:   +43 (0)2732 85522 40
//       E-mail: office@feuerwehr-krems.at
// 
//       This software is furnished under a license and may be
//       used  and copied only in accordance with the terms of
//       such  license  and  with  the  inclusion of the above
//       copyright  notice.  This software or any other copies
//       thereof   may  not  be  provided  or  otherwise  made
//       available  to  any  other  person.  No  title  to and
//       ownership of the software is hereby transferred.
// 
//       The information in this software is subject to change
//       without  notice  and  should  not  be  construed as a
//       commitment by Freiwillige Feuerwehr Krems/Donau.
// 
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace At.FF.Krems.Configuration.XML
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    /// <summary>The window dimensions.</summary>
    [GeneratedCode("xsd", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlType(AnonymousType = true)]
    [Serializable]
    public class WindowDimensions
    {
        #region Fields

        /// <summary>The width lock object</summary>
        private readonly object widthLockObject = new object();

        /// <summary>The height lock object</summary>
        private readonly object heightLockObject = new object();

        /// <summary>The width</summary>
        private string width;

        /// <summary>The height</summary>
        private string height;

        #endregion

        #region Properties

        /// <summary>Gets or sets the width.</summary>
        public string Width
        {
            get
            {
                lock (this.widthLockObject)
                {
                    return string.IsNullOrWhiteSpace(this.width) ? (this.width = "max2") : this.width;
                }
            }

            set
            {
                lock (this.widthLockObject)
                {
                    this.width = value;
                }
            }
        }

        /// <summary>Gets or sets the height.</summary>
        public string Height
        {
            get
            {
                lock (this.heightLockObject)
                {
                    return string.IsNullOrWhiteSpace(this.height) ? (this.height = "max") : this.height;
                }
            }

            set
            {
                lock (this.heightLockObject)
                {
                    this.height = value;
                }
            }
        }

        #endregion
    }
}