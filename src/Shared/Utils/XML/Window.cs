// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Window.cs" company="Freiwillige Feuerwehr Krems/Donau">
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

namespace At.FF.Krems.Utils.XML
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    /// <summary>The window.</summary>
    [GeneratedCode("xsd", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlRoot(Namespace = "", IsNullable = false), XmlType(AnonymousType = true)]
    [Serializable]
    public class Window
    {
        /// <summary>Gets or sets a value indicating whether auto start.</summary>
        public bool Autostart { get; set; }

        /// <summary>Gets or sets the URL.</summary>
        public string Url { get; set; }

        /// <summary>Gets or sets a value indicating whether is alternative window.</summary>
        public bool IsAlternativeWindow { get; set; }

        /// <summary>Gets or sets the show on screen.</summary>
        public int ShowOnScreen { get; set; }

        /// <summary>Gets or sets the position.</summary>
        public WindowPosition Position { get; set; }

        /// <summary>Gets or sets the dimensions.</summary>
        public WindowDimensions Dimensions { get; set; }

        /// <summary>Gets or sets the restart in seconds.</summary>
        public int RestartInSeconds { get; set; }

        /// <summary>Gets or sets the reload in seconds.</summary>
        public int ReloadInSeconds { get; set; }

        /// <summary>Gets or sets the name.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets a value indicating whether on top.</summary>
        public bool OnTop { get; set; }

        /// <summary>Gets or sets the zoom level.</summary>
        public float ZoomLevel { get; set; }
    }
}