// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WindowPosition.cs" company="Freiwillige Feuerwehr Krems/Donau">
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

    /// <summary>The window position.</summary>
    [GeneratedCode("xsd", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlType(AnonymousType = true)]
    [Serializable]
    public class WindowPosition
    {
        #region Fields

        /// <summary>The position x lock object</summary>
        private readonly object posXLockObject = new object();

        /// <summary>The position y lock object</summary>
        private readonly object posYLockObject = new object();

        /// <summary>The position x</summary>
        private int posX;

        /// <summary>The position y</summary>
        private int posY;

        #endregion

        #region Properties

        /// <summary>Gets or sets the position x.</summary>
        public int PosX
        {
            get
            {
                lock (this.posXLockObject)
                {
                    return this.posX;
                }
            }

            set
            {
                lock (this.posXLockObject)
                {
                    this.posX = value;
                }
            }
        }

        /// <summary>Gets or sets the position y.</summary>
        public int PosY
        {
            get
            {
                lock (this.posYLockObject)
                {
                    return this.posY;
                }
            }

            set
            {
                lock (this.posYLockObject)
                {
                    this.posY = value;
                }
            }
        }

        #endregion
    }
}