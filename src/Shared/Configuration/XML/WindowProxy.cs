// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WindowProxy.cs" company="Freiwillige Feuerwehr Krems/Donau">
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

    /// <summary>The proxy type.</summary>
    public enum ProxyType
    {
        /// <summary>Direct connection, no proxy.</summary>
        NoProxy = 0,

        /// <summary>Manual proxy configuration.</summary>
        Manual = 1,

        /// <summary>Proxy auto-configuration (PAC).</summary>
        AutoConfigurationPac = 2,

        /// <summary>Auto-detect proxy settings.</summary>
        AutoDetect = 4,

        /// <summary>Use system proxy settings.</summary>
        SystemProxy = 5
    }

    /// <summary>The window proxy.</summary>
    [GeneratedCode("xsd", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlType(AnonymousType = true)]
    [Serializable]
    public class WindowProxy
    {
        /// <summary>Gets or sets the type.</summary>
        public ProxyType Type { get; set; }

        /// <summary>Gets or sets the server.</summary>
        public string Server { get; set; }

        /// <summary>Gets or sets the port.</summary>
        public int Port { get; set; }

        /// <summary>Gets or sets the URL.</summary>
        public string Url { get; set; }
    }
}