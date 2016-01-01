// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeckoBrowser.cs" company="Freiwillige Feuerwehr Krems/Donau">
//      Freiwillige Feuerwehr Krems/Donau
//      Austraße 33
//      A-3500 Krems/Donau
//      Austria
//      Tel.:   +43 (0)2732 85522
//      Fax.:   +43 (0)2732 85522 40
//      E-mail: office@feuerwehr-krems.at
// 
//      This software is furnished under a license and may be
//      used  and copied only in accordance with the terms of
//      such  license  and  with  the  inclusion of the above
//      copyright  notice.  This software or any other copies
//      thereof   may  not  be  provided  or  otherwise  made
//      available  to  any  other  person.  No  title  to and
//      ownership of the software is hereby transferred.
// 
//      The information in this software is subject to change
//      without  notice  and  should  not  be  construed as a
//      commitment by Freiwillige Feuerwehr Krems/Donau.
// 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace At.FF.Krems.FullscreenBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using At.FF.Krems.Configuration.XML;
    using At.FF.Krems.FullscreenBrowser.Properties;
    using At.FF.Krems.Interfaces;
    using At.FF.Krems.Utils.Bootstrapper;

    using Gecko;

    using log4net;

    using Cookie = System.Net.Cookie;
    using Screen = System.Windows.Forms.Screen;
    using Timer = System.Timers.Timer;

    /// <summary>The gecko browser.</summary>
    public partial class GeckoBrowser : Form, IGeckoBrowser
    {
        #region Fields

        /// <summary>The zoom step</summary>
        private const float ZoomStep = 0.1f;

        /// <summary>The logger</summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The DOM content loaded lock object</summary>
        private readonly object domContentLoadedLockObject = new object();

        /// <summary>The reload lock object</summary>
        private readonly object reloadLockObject = new object();

        /// <summary>The initialize called</summary>
        private bool initializeCalled;

        /// <summary>The configuration</summary>
        private Window config;

        /// <summary>The browser</summary>
        private GeckoWebBrowser browser;

        /// <summary>The DOM content loaded</summary>
        private bool domContentLoaded;

        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="GeckoBrowser"/> class.</summary>
        public GeckoBrowser()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Delegates

        /// <summary>The string delegate.</summary>
        /// <param name="url">The url.</param>
        private delegate void StringDelegate(string url);

        #endregion

        #region Properties

        /// <summary>Gets the document cookie.</summary>
        /// <value>The document cookie.</value>
        public List<Cookie> DocumentCookies { get; } = new List<Cookie>();

        /// <summary>Gets or sets a value indicating whether [DOM content loaded].</summary>
        /// <value><c>true</c> if [DOM content loaded]; otherwise, <c>false</c>.</value>
        private bool DomContentLoaded
        {
            get
            {
                lock (this.domContentLoadedLockObject)
                {
                    return this.domContentLoaded;
                }
            }

            set
            {
                lock (this.domContentLoadedLockObject)
                {
                    this.domContentLoaded = value;
                }
            }
        }

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

        #region IGeckoBrowser

        /// <summary>Setups the specified setting.</summary>
        /// <param name="setting">The setting.</param>
        public void Setup(Window setting)
        {
            try
            {
                this.config = setting;
                this.Text = this.config.Name;
                this.TopMost = this.config.OnTop;
                this.Icon = Resources.TrayIcon;

                this.browser = new GeckoWebBrowser
                {
                    Parent = this,
                    Dock = DockStyle.Fill,
                    Location = new Point(0, 0),
                    Name = "browser",
                    Size = new Size(104, 100),
                    TabIndex = 0
                };
                this.ClientSize = new Size(104, 100);
                this.ControlBox = false;
                this.Controls.Add(this.browser);
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Name = "browser";
                this.SizeGripStyle = SizeGripStyle.Hide;
                this.StartPosition = FormStartPosition.Manual;
                this.ResumeLayout(false);
#if DEBUG
                this.FormBorderStyle = FormBorderStyle.Fixed3D;
#else
                this.FormBorderStyle = FormBorderStyle.None;
#endif

                this.browser.HandleCreated += this.GeckoBrowserHandleCreated;

                var showOnScreen = this.config.ShowOnScreen;
                showOnScreen = (showOnScreen >= Screen.AllScreens.Length) ? 0 : showOnScreen;
                var screen = Screen.AllScreens[showOnScreen];
                var left = screen.Bounds.Left;
                var top = screen.Bounds.Top;
                var posX = this.config.Position.PosX;
                var posY = this.config.Position.PosY;
                this.Location = new Point(left + posX, top + posY);
                var width = this.config.Dimensions.Width;
                int num5;
                if (width == "max" || width == "max2")
                {
                    num5 = (width == "max") ? screen.Bounds.Width : (screen.Bounds.Width * 2);
                    num5 -= posX;
                }
                else
                {
                    num5 = Convert.ToInt32(width);
                }

                var height = this.config.Dimensions.Height;
                int num6;
                if (height == "max" || height == "max2")
                {
                    num6 = (height == "max") ? screen.Bounds.Height : (screen.Bounds.Height * 2);
                    num6 -= posY;
                }
                else
                {
                    num6 = Convert.ToInt32(height);
                }

                this.ClientSize = new Size(num5, num6);
                this.browser.DOMContentLoaded += this.BrowserOnDomContentLoaded;
                this.browser.DomMouseScroll += this.BrowserOnDomMouseScroll;
                this.browser.DomKeyDown += this.BrowserOnDomKeyDown;

                if (setting.ReloadInSeconds > 0)
                {
                    // This timer works as auto reload of the webpage.
                    var reloadTimer = new Timer { Interval = setting.ReloadInSeconds * 1000, AutoReset = true };
                    this.browser.Disposed += (sender, args) =>
                    {
                        if (reloadTimer == null)
                        {
                            return;
                        }

                        reloadTimer.Stop();
                        reloadTimer.Dispose();
                        reloadTimer = null;
                    };

                    reloadTimer.Elapsed += (sender, args) =>
                    {
                        if (this.browser.Disposing || !this.DomContentLoaded)
                        {
                            return;
                        }

                        lock (this.reloadLockObject)
                        {
                            this.DocumentCookies.Clear();
                            this.browser.Reload();
                        }
                    };
                    reloadTimer.Start();
                }
            }
            catch (Exception exception)
            {
                Logger.Warn(exception);
            }
        }

        /// <summary>Starts or resets the browser.</summary>
        /// <param name="ignoreAutoStart">if set to <c>true</c> ignores flag of automatic start.</param>
        /// <param name="disableReload">if set to <c>true</c> disable reload.</param>
        /// <param name="onlyIfIsEmergencyWindow">if set to <c>true</c> [only if is emergency window].</param>
        /// <param name="ignoreVisibility">if set to <c>true</c> [ignore visibility].</param>
        public void StartOrResetBrowser(bool ignoreAutoStart = false, bool disableReload = false, bool onlyIfIsEmergencyWindow = false, bool ignoreVisibility = false)
        {
            if (onlyIfIsEmergencyWindow && this.config.IsAlternativeWindow)
            {
                return;
            }

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => this.StartOrResetBrowser(ignoreAutoStart, disableReload, onlyIfIsEmergencyWindow)));
                return;
            }

            if (!ignoreVisibility && !this.Visible && (ignoreAutoStart || this.config.Autostart))
            {
                this.Show();
            }

            if (!disableReload && this.DomContentLoaded)
            {
                lock (this.reloadLockObject)
                {
                    this.DocumentCookies.Clear();
                    this.browser.Reload();
                }
            }

            this.BringToFront();
        }

        /// <summary>Closes the browser.</summary>
        /// <param name="onlyIfIsAlternativeWindow">if set to <c>true</c> [only if is alternative window].</param>
        public void CloseBrowser(bool onlyIfIsAlternativeWindow = false)
        {
            if (!this.Visible || (onlyIfIsAlternativeWindow && !this.config.IsAlternativeWindow))
            {
                return;
            }

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => this.CloseBrowser(onlyIfIsAlternativeWindow)));
                return;
            }

            this.Hide();
        }

        /// <summary>The zoom in.</summary>
        public void ZoomIn()
        {
            this.Zoom(ZoomStep);
        }

        /// <summary>The zoom out.</summary>
        public void ZoomOut()
        {
            this.Zoom(ZoomStep * -1);
        }

        /// <summary>Resets the zoom.</summary>
        public void ResetZoom()
        {
            this.Zoom(1f, false);
        }

        #endregion

        /// <summary>Raises the <see cref="E:FormClosing" /> event.</summary>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    break;
            }

            base.OnFormClosing(e);
        }

        /// <summary>Initializes this instance.</summary>
        /// <param name="force">Force initialization, even if already initialized.</param>
        private void Initialize(bool force = false)
        {
            if (this.initializeCalled && !force)
            {
                return;
            }

            this.initializeCalled = true;
            var browserConfig = Bootstrapper.GetInstance<IBrowserConfiguration>().Config;
            if (!Xpcom.IsInitialized)
            {
                PromptFactory.PromptServiceCreator = () => new FilteredPromptService();
                Xpcom.Initialize(Path.GetFullPath(browserConfig.Runtime));
            }

            if (browserConfig.ClearCookiesAtStartup)
            {
                var cookieManager = Xpcom.GetService<nsICookieManager>("@mozilla.org/cookiemanager;1");
                cookieManager?.RemoveAll();
            }

            if (browserConfig.Cookie != null && browserConfig.Cookie.Any())
            {
                var cookieManager2 = Xpcom.GetService<nsICookieManager2>("@mozilla.org/cookiemanager;1");
                if (cookieManager2 != null)
                {
                    foreach (var cookie in browserConfig.Cookie.Where(cookie => cookie != null && (cookie.IsSession || cookie.ExpirationDate > DateTime.Now)))
                    {
                        cookieManager2.Add(
                            new nsAUTF8String(cookie.Host),
                            new nsAUTF8String(cookie.Path),
                            new nsACString(cookie.Name),
                            new nsACString(cookie.Value),
                            cookie.IsSecure,
                            cookie.IsHttpOnly,
                            cookie.IsSession,
                            (long)cookie.ExpirationDate.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
                    }
                }
            }

            if (browserConfig.Proxy != null)
            {
                GeckoPreferences.User["network.proxy.no_proxies_on"] = "localhost, 127.0.0.1";
                var num = browserConfig.Proxy.Type;
                GeckoPreferences.User["network.proxy.type"] = (int)num;
                switch (num)
                {
                    case ProxyType.Manual:
                        GeckoPreferences.User["network.proxy.http"] = browserConfig.Proxy.Server;
                        GeckoPreferences.User["network.proxy.http_port"] = browserConfig.Proxy.Port;
                        GeckoPreferences.User["network.proxy.ftp"] = browserConfig.Proxy.Server;
                        GeckoPreferences.User["network.proxy.ftp_port"] = browserConfig.Proxy.Port;
                        GeckoPreferences.User["network.proxy.ssl"] = browserConfig.Proxy.Server;
                        GeckoPreferences.User["network.proxy.ssl_port"] = browserConfig.Proxy.Port;
                        GeckoPreferences.User["network.proxy.socks"] = browserConfig.Proxy.Server;
                        GeckoPreferences.User["network.proxy.socks_port"] = browserConfig.Proxy.Port;
                        break;
                    case ProxyType.AutoConfigurationPac:
                        GeckoPreferences.User["network.proxy.autoconfig_url"] = browserConfig.Proxy.Url;
                        break;
                }
            }
        }

        /// <summary>Zoom to level.</summary>
        /// <param name="zoom">The zoom.</param>
        /// <param name="increment">The increment.</param>
        private void Zoom(float zoom, bool increment = true)
        {
            if (this.browser.InvokeRequired)
            {
                this.browser.Invoke(new Action(() => this.Zoom(zoom, increment)));
                return;
            }

            var documentViewer = this.browser.GetMarkupDocumentViewer();
            if (documentViewer == null)
            {
                return;
            }

            var currentZoomLevel = zoom;
            if (increment)
            {
                currentZoomLevel = documentViewer.GetTextZoomAttribute();
                currentZoomLevel += zoom;
            }

            documentViewer.SetTextZoomAttribute(currentZoomLevel);
            this.config.ZoomLevel = currentZoomLevel;
        }

        /// <summary>The gecko browser handle created.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void GeckoBrowserHandleCreated(object sender, EventArgs e)
        {
            this.NavigateToUrl(this.config.Url);
        }

        /// <summary>Performs actions on browser when DOM content loaded.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="domEventArgs">The dom event args.</param>
        private void BrowserOnDomContentLoaded(object sender, DomEventArgs domEventArgs)
        {
            if (!this.config.IsAlternativeWindow)
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(3000);
                    var posX = this.config.Position.PosX;
                    var posY = this.config.Position.PosY;
                    Cursor.Position = new Point(posX, posY);
                    Cursor.Position = new Point(posX + 50, posY + 50);
                });
            }

            this.Zoom(this.config.ZoomLevel, false);
            var document = ((GeckoWebBrowser)sender).Document;
            const char Seperator = '=';
            var splitIndex = document.Cookie.IndexOf(Seperator);
            if (document.Cookie.Contains(Seperator))
            {
                var name = document.Cookie.Substring(0, splitIndex);
                var value = document.Cookie.Substring(splitIndex + 1, document.Cookie.Length - name.Length - 1);
                this.DocumentCookies.Add(new Cookie(name, value, "/", document.Domain));
            }

            this.DomContentLoaded = true;
        }

        /// <summary>The async navigate thread.</summary>
        /// <param name="url">The url.</param>
        private void NavigateToUrl(string url)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (this.InvokeRequired || this.browser.InvokeRequired)
            {
                this.BeginInvoke(new StringDelegate(this.NavigateToUrl), url);
                return;
            }

            this.browser.Navigate(url);
        }

        /// <summary>Browsers the on DOM mouse scroll.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="domMouseEventArgs">The <see cref="DomMouseEventArgs"/> instance containing the event data.</param>
        private void BrowserOnDomMouseScroll(object sender, DomMouseEventArgs domMouseEventArgs)
        {
            // See https://developer.mozilla.org/zh-CN/docs/DOM/DOM_event_reference/DOMMouseScroll
            if (!domMouseEventArgs.CtrlKey)
            {
                return;
            }

            this.config.ZoomLevel = this.browser.GetMarkupDocumentViewer().GetTextZoomAttribute();
        }

        /// <summary>The browser on dom key down.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="domKeyEventArgs">The <see cref="DomKeyEventArgs"/> instance containing the event data.</param>
        private void BrowserOnDomKeyDown(object sender, DomKeyEventArgs domKeyEventArgs)
        {
            try
            {
                switch (domKeyEventArgs.KeyCode)
                {
                    case (uint)Keys.F5:
                        lock (this.reloadLockObject)
                        {
                            this.DocumentCookies.Clear();
                            this.browser.Reload();
                        }

                        return;
                    case (uint)Keys.F4:
                        if (!domKeyEventArgs.AltKey)
                        {
                            return;
                        }

                        this.CloseBrowser();
                        return;
                    case (uint)Keys.F1:
                        {
                            var stringBuilder = new StringBuilder();
                            var num = 0;
                            var allScreens = Screen.AllScreens;
                            foreach (var screen in allScreens)
                            {
                                stringBuilder.AppendLine($"Screen #{num++} ({screen.DeviceName})");
                                stringBuilder.AppendLine("Primary display: " + screen.Primary);
                                stringBuilder.AppendLine($"Display resolution: {screen.Bounds.Width} x {screen.Bounds.Height}");
                                stringBuilder.AppendLine($"Position: x={screen.Bounds.Left}, y={screen.Bounds.Top}");
                                stringBuilder.AppendLine(string.Empty);
                            }

                            MessageBox.Show(stringBuilder.ToString(), Resources.screenInfo_DE_AT, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }

                        return;
                    case (uint)Keys.S:
                        if (!domKeyEventArgs.CtrlKey)
                        {
                            return;
                        }

                        Bootstrapper.GetInstance<IBrowserConfiguration>().Save();
                        return;
                    case (uint)Keys.BrowserFavorites:
                    case (uint)Keys.Add:
                        if (!domKeyEventArgs.CtrlKey)
                        {
                            return;
                        }

                        this.ZoomIn();
                        break;
                    case (uint)Keys.VolumeMute:
                    case (uint)Keys.Subtract:
                        if (!domKeyEventArgs.CtrlKey)
                        {
                            return;
                        }

                        this.ZoomOut();
                        break;
                    case (uint)Keys.NumPad0:
                    case (uint)Keys.D0:
                        if (!domKeyEventArgs.CtrlKey)
                        {
                            return;
                        }

                        this.ResetZoom();
                        break;
                }
            }
            catch (Exception exception)
            {
                Logger.Warn(exception);
            }
        }

        #endregion
    }
}