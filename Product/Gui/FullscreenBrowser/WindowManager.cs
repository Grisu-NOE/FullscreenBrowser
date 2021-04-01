// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WindowManager.cs" company="Freiwillige Feuerwehr Krems/Donau">
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

namespace At.FF.Krems.FullscreenBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Windows.Controls;

    using Gecko;
    using Gecko.Cache;

    using Interfaces;

    using Properties;

    using Utils.Bootstrapper;

    using Cookie = System.Net.Cookie;

    /// <summary>The window manager.</summary>
    public class WindowManager : IWindowManager
    {
        #region Fields

        /// <summary>The gecko browsers</summary>
        private readonly List<IGeckoBrowser> geckoBrowsers = new List<IGeckoBrowser>();

        /// <summary>The window timer</summary>
        private readonly Timer windowTimer = new Timer { AutoReset = false };

        /// <summary>The window timeout</summary>
        private int windowTimeout;

        /// <summary>The disable screensaver permanently.</summary>
        private bool disableScreensaverPermanently;

        /// <summary>The initialize called</summary>
        private bool initializeCalled;

        #endregion

        #region Startup Tasks

        /// <summary>Initializes this instance.</summary>
        public void Init()
        {
            this.Initialize();
        }

        /// <summary>Resets this instance.</summary>
        public void Reset()
        {
            this.Initialize(true);
        }

        #endregion

        #region Methods

        /// <summary>Activates the WASTL display.</summary>
        public void ActivateWastlDisplay()
        {
            this.windowTimer.Interval = this.windowTimeout;
            Parallel.ForEach(this.geckoBrowsers, browser => browser.CloseBrowser(true));
            this.geckoBrowsers.ForEach(browser => browser.StartOrResetBrowser(true, true, true));
            Bootstrapper.GetInstance<IScreenRefresher>().HideScreenRefresher();
            if (!this.windowTimer.Enabled)
            {
                this.windowTimer.Start();
            }

            if (!this.disableScreensaverPermanently)
            {
                Bootstrapper.GetInstance<IPowerManagement>().SetPowerReq(PowerThreadRequirements.HoldSystemAndDisplay);
            }

            var screensaver = Bootstrapper.GetInstance<IScreensaver>();
            if (!screensaver.GetScreensaverActive())
            {
                return;
            }

            if (screensaver.GetScreensaverRunning())
            {
                screensaver.KillScreensaver();
            }
            else
            {
                screensaver.SetScreensaverActive(true);
            }
        }

        /// <summary>Gets the document cookies.</summary>
        /// <param name="hostFilter">The host filter.</param>
        /// <returns>The <see cref="string" />.</returns>
        public List<Cookie> GetDocumentCookies(string hostFilter = null)
        {
            var data = new List<Cookie>();
            foreach (var geckoBrowser in this.geckoBrowsers)
            {
                data.AddRange(
                    string.IsNullOrWhiteSpace(hostFilter)
                        ? geckoBrowser.DocumentCookies
                        : geckoBrowser.DocumentCookies.Where(
                            x => x.Domain.Equals(hostFilter, StringComparison.OrdinalIgnoreCase)));
            }

            return data;
        }

        /// <summary>Clears the browser cache.</summary>
        public void ClearBrowserCache()
        {
            var historyManager = Xpcom.GetService<nsIBrowserHistory>(Contracts.NavHistoryService);
            historyManager = Xpcom.QueryInterface<nsIBrowserHistory>(historyManager);
            historyManager.RemovePagesByTimeframe(0, DateTimeOffset.Now.ToUnixTimeMilliseconds()); // Remove all pages, defined in milliseconds from UNIX epoch, aBeginTime <= time-frame <= aEndTime

            ImageCache.ClearCache(true);
            ImageCache.ClearCache(false);
            CacheService.Clear(new CacheStoragePolicy());
        }

        /// <summary>Initializes this instance.</summary>
        /// <param name="force">Force initialization, even if already initialized.</param>
        private void Initialize(bool force = false)
        {
            if (this.initializeCalled && !force)
            {
                return;
            }

            this.disableScreensaverPermanently = Bootstrapper.GetInstance<IBrowserConfiguration>().Config.DisableScreensaverPermanently;
            if (this.disableScreensaverPermanently)
            {
                Bootstrapper.GetInstance<IPowerManagement>().SetPowerReq(PowerThreadRequirements.HoldSystemAndDisplay);
            }

            if (!this.initializeCalled)
            {
                this.windowTimer.Elapsed += (sender, args) =>
                {
                    foreach (var geckoBrowser in this.geckoBrowsers)
                    {
                        geckoBrowser.StartOrResetBrowser();
                    }

                    Bootstrapper.GetInstance<IPrintController>().ClearPrintedMissions();
                    if (!this.disableScreensaverPermanently)
                    {
                        Bootstrapper.GetInstance<IPowerManagement>().SetPowerReq(PowerThreadRequirements.HoldSystem);
                    }
                };
            }

            this.initializeCalled = true;

            var taskbar = Bootstrapper.GetInstance<IApp>().Taskbar;
            var configuration = Bootstrapper.GetInstance<IBrowserConfiguration>().Config;

            this.windowTimeout = configuration.ShowClosedWindowsInMinutes * 60 * 1000;
            this.windowTimer.Interval = this.windowTimeout;

            foreach (var window in configuration.Window)
            {
                var geckoBrowser = Bootstrapper.GetInstance<IGeckoBrowser>();
                geckoBrowser.Setup(window);
                var menuItem = new MenuItem { Header = window.Name };
                var startOrReload = new MenuItem
                {
                    Header = Resources.screenStart_DE_AT,
                    Command = new DelegateCommand
                    {
                        CommandAction =
                            () =>
                            {
                                foreach (var browser in this.geckoBrowsers)
                                {
                                    var isOwnBrowser = browser == geckoBrowser;
                                    browser.StartOrResetBrowser(isOwnBrowser, false, false, !isOwnBrowser);
                                }
                            }
                    }
                };
                menuItem.Items.Add(startOrReload);
                var close = new MenuItem { Header = Resources.screenClose_DE_AT, IsEnabled = false, Command = new DelegateCommand { CommandAction = () => geckoBrowser.CloseBrowser() } };
                menuItem.Items.Add(close);
                menuItem.Items.Add(new Separator());
                var zoomItem = new MenuItem { Header = Resources.Zoom_DE_AT };
                zoomItem.Items.Add(
                    new MenuItem
                        {
                            Header = Resources.ZoomIn_DE_AT,
                            Command = new DelegateCommand { CommandAction = geckoBrowser.ZoomIn }
                        });
                zoomItem.Items.Add(
                    new MenuItem
                        {
                            Header = Resources.ZoomOut_DE_AT,
                            Command = new DelegateCommand { CommandAction = geckoBrowser.ZoomOut }
                        });
                zoomItem.Items.Add(
                    new MenuItem
                        {
                            Header = Resources.ResetZoom_DE_AT,
                            Command = new DelegateCommand { CommandAction = geckoBrowser.ResetZoom }
                        });
                menuItem.Items.Add(zoomItem);
                taskbar.ContextMenu.Items.Insert(0, menuItem);
                geckoBrowser.VisibleChanged += (sender, args) =>
                    {
                        var browser = (IGeckoBrowser)sender;
                        if (browser == null)
                        {
                            return;
                        }

                        if (browser.Visible)
                        {
                            close.IsEnabled = true;
                            zoomItem.IsEnabled = true;
                            startOrReload.Header = Resources.screenReload_DE_AT;
                        }
                        else
                        {
                            close.IsEnabled = false;
                            zoomItem.IsEnabled = false;
                            startOrReload.Header = Resources.screenStart_DE_AT;
                            this.windowTimer.Interval = this.windowTimeout;
                            if (!this.windowTimer.Enabled)
                            {
                                this.windowTimer.Start();
                            }
                        }
                    };
                this.geckoBrowsers.Add(geckoBrowser);
                geckoBrowser.StartOrResetBrowser();
                if (!this.disableScreensaverPermanently)
                {
                    Bootstrapper.GetInstance<IPowerManagement>().SetPowerReq(PowerThreadRequirements.HoldSystem);
                }
            }
        }

        #endregion
    }
}