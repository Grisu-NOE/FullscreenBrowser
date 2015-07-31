// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Cookie.cs" company="Freiwillige Feuerwehr Krems/Donau">
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

    /// <summary>The cookie.</summary>
    [GeneratedCode("xsd", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlType(AnonymousType = true)]
    [Serializable]
    public class Cookie
    {
        /// <summary>Gets or sets the host or domain for which the cookie is set.</summary>
        [XmlAttribute]
        public string Host { get; set; }

        /// <summary>Gets or sets the path within the domain for which the cookie is valid.</summary>
        public string Path { get; set; }

        /// <summary>Gets or sets the cookie name.</summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>Gets or sets the cookie data.</summary>
        public string Value { get; set; }

        /// <summary>Gets or sets a value indicating whether the cookie should only be sent over a secure connection.</summary>
        public bool IsSecure { get; set; }

        /// <summary>Gets or sets a value indicating whether the cookie should only be sent to, and can only be modified by, an HTTP connection.</summary>
        public bool IsHttpOnly { get; set; }

        /// <summary>Gets or sets a value indicating whether the cookie should exist for the current session only.</summary>
        public bool IsSession { get; set; }

        /// <summary>Gets or sets the expiration in seconds since the epoch (1.1.1970 0:0:0.0). Only relevant if <see cref="IsSession"/>is <see cref="bool">false</see>.</summary>
        public DateTime ExpirationDate { get; set; }
    }
}