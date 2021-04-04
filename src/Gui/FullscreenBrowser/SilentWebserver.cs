// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SilentWebserver.cs" company="Freiwillige Feuerwehr Krems/Donau">
//   Freiwillige Feuerwehr Krems/Donau
//   //       Austraße 33
//   //       A-3500 Krems/Donau
//   //       Austria
//   //       Tel.:   +43 (0)2732 85522
//   //       Fax.:   +43 (0)2732 85522 40
//   //       E-mail: office@feuerwehr-krems.at
//   //       This software is furnished under a license and may be
//   //       used  and copied only in accordance with the terms of
//   //       such  license  and  with  the  inclusion of the above
//   //       copyright  notice.  This software or any other copies
//   //       thereof   may  not  be  provided  or  otherwise  made
//   //       available  to  any  other  person.  No  title  to and
//   //       ownership of the software is hereby transferred.
//   //       The information in this software is subject to change
//   //       without  notice  and  should  not  be  construed as a
//   //       commitment by Freiwillige Feuerwehr Krems/Donau.
// </copyright>
// <summary>
//   The silent webserver.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace At.FF.Krems.FullscreenBrowser
{
    using System;
    using System.Reflection;

    using Interfaces;
    using log4net;
    using Microsoft.Win32;
    using Utils.Bootstrapper;

    /// <summary>The silent webserver.</summary>
    public class SilentWebserver
    {
        #region Fields
        
        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>Singleton instance.</summary>
        private static SilentWebserver singletonInstance;

        /// <summary>The initialize called.</summary>
        private bool initializeCalled;

        #endregion

        #region Constructors

        /// <summary>Prevents a default instance of the <see cref="SilentWebserver"/> class from being created.</summary>
        private SilentWebserver()
        {
            this.initializeCalled = false;
        }

        #endregion Constructors

        /// <summary>Gets the Singleton instance.</summary>
        public static SilentWebserver Instance => singletonInstance ?? (singletonInstance = new SilentWebserver());

        /// <summary>Initializes the silent webserver.</summary>
        /// <param name="url">The url.</param>
        /// <param name="port">The port.</param>
        public void Initialize(string url, int port)
        {
            if (this.initializeCalled)
            {
                return;
            }

            this.initializeCalled = true;
            try
            {
                using (var currentUser = Registry.CurrentUser)
                {
                    using (var registryKey = currentUser.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\PageSetup", true))
                    {
                        if (registryKey != null)
                        {
                            registryKey.SetValue("footer", string.Empty);
                            registryKey.SetValue("header", string.Empty);
                            registryKey.SetValue("margin_bottom", "0.590550");
                            registryKey.SetValue("margin_left", "0.393700");
                            registryKey.SetValue("margin_right", "0.393700");
                            registryKey.SetValue("margin_top", "0.393700");
                            registryKey.SetValue("Print_Background", "yes");
                            registryKey.SetValue("Shrink_To_Fit", "yes");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Warn(exception);
            }

            var httpServer = Bootstrapper.GetInstance<IHttpServer>();
            httpServer.Init();
        }
    }
}