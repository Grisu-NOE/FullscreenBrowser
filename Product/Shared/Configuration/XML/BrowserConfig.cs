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
namespace At.FF.Krems.Configuration.XML
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
        #region Fields

        /// <summary>The show closed windows in minutes lock object</summary>
        private readonly object showClosedWindowsInMinutesLockObject = new object();
        
        /// <summary>Gets or sets the cookie field.</summary>
        private Cookie[] cookieField;
        
        /// <summary>The show closed windows in minutes</summary>
        private int showClosedWindowsInMinutes;

        #endregion

        #region Properties

        /// <summary>Gets or sets the window.</summary>
        [XmlElement]
        public Window[] Window { get; set; }

        /// <summary>Gets or sets the runtime.</summary>
        public string Runtime { get; set; } = "xulrunner_33.0.3";

        /// <summary>Gets or sets a value indicating whether disable screensaver permanently.</summary>
        public bool DisableScreensaverPermanently { get; set; }

        /// <summary>Gets or sets a value indicating whether to disable power management.</summary>
        /// <value>
        /// <c>true</c> if power management is disabled otherwise, <c>false</c>.
        /// </value>
        public bool DisablePowerManagement { get; set; }

        /// <summary>Gets or sets the print settings.</summary>
        /// <value>The print settings.</value>
        public PrintSettings PrintSettings { get; set; } = new PrintSettings();

        /// <summary>Gets or sets the screen refresher.</summary>
        public ScreenRefresher ScreenRefresher { get; set; }

        /// <summary>Gets or sets the proxy.</summary>
        public WindowProxy Proxy { get; set; }

        /// <summary>Gets or sets a value indicating whether to clear cookies at startup.</summary>
        public bool ClearCookiesAtStartup { get; set; }

        /// <summary>Gets or sets the setting.</summary>
        [XmlElement]
        public Cookie[] Cookie
        {
            get
            {
                var result = new List<Cookie>();
                if (this.cookieField != null && this.cookieField.Any())
                {
                    foreach (
                        var cookie in
                            this.cookieField.Where(
                                cookie =>
                                !result.Any(
                                    cookie1 =>
                                    cookie1.Host != null
                                    && cookie1.Host.Equals(cookie.Host, StringComparison.OrdinalIgnoreCase)
                                    && cookie1.Name.Equals(cookie.Name))))
                    {
                        result.Add(cookie);
                    }
                }

                return result.Any() ? result.ToArray() : null;
            }

            set
            {
                this.cookieField = value;
            }
        }

        /// <summary>Gets or sets the show closed windows in minutes.</summary>
        /// <value>The show closed windows in minutes.</value>
        public int ShowClosedWindowsInMinutes
        {
            get
            {
                lock (this.showClosedWindowsInMinutesLockObject)
                {
                    return this.showClosedWindowsInMinutes > 0 ? this.showClosedWindowsInMinutes : 3;
                }
            }

            set
            {
                lock (this.showClosedWindowsInMinutesLockObject)
                {
                    this.showClosedWindowsInMinutes = value;
                }
            }
        }

        #endregion
    }
}