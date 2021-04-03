// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BrowserConfig.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Xml.Serialization;

    /// <summary>The browser config.</summary>
    [GeneratedCode("xsd", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlRoot(Namespace = "", IsNullable = false), XmlType(AnonymousType = true)]
    [Serializable]
    public class BrowserConfig
    {
        /// <summary>Gets or sets the window.</summary>
        [XmlElement("Window")]
        public Window[] Window { get; set; }

        /// <summary>Gets or sets the runtime.</summary>
        public string Runtime { get; set; }

        /// <summary>Gets or sets the display name.</summary>
        public string DisplayName { get; set; }

        /// <summary>Gets or sets the print url.</summary>
        public string PrintUrl { get; set; }

        /// <summary>Gets or sets a value indicating whether print enabled.</summary>
        public bool PrintEnabled { get; set; }
        
        /// <summary>Gets or sets the print port.</summary>
        public int PrintPort { get; set; }

        /// <summary>Gets or sets a value indicating whether disable screensaver permanently.</summary>
        public bool DisableScreensaverPermanently { get; set; }

        /// <summary>Gets or sets the screen refresher.</summary>
        [XmlElement("ScreenRefresher")]
        public ScreenRefresher ScreenRefresher { get; set; }

        /// <summary>Gets or sets the proxy.</summary>
        public WindowProxy Proxy { get; set; }

        /// <summary>Gets or sets a value indicating whether to clear cookies at startup.</summary>
        public bool ClearCookiesAtStartup { get; set; }

        /// <summary>Gets or sets the setting.</summary>
        [XmlElement("Cookie")]
        public Cookie[] Cookie
        {
            get
            {
                var result = new List<Cookie>();
                if (this.CookieField != null && this.CookieField.Any())
                {
                    foreach (var cookie in this.CookieField.Where(cookie => !result.Any(cookie1 => cookie1.Host != null && cookie1.Host.Equals(cookie.Host, StringComparison.OrdinalIgnoreCase) && cookie1.Name.Equals(cookie.Name))))
                    {
                        result.Add(cookie);
                    }
                }

                return result.Any() ? result.ToArray() : null;
            }

            set
            {
                this.CookieField = value;
            }
        }

        /// <summary>Gets or sets the cookie field.</summary>
        private Cookie[] CookieField { get; set; }
    }
}