// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserConfigViewModel.cs" company="Freiwillige Feuerwehr Krems/Donau">
//     Freiwillige Feuerwehr Krems/Donau
//     Austraße 33
//     A-3500 Krems/Donau
//     Austria
// 
//     Tel.:   +43 (0)2732 85522
//     Fax.:   +43 (0)2732 85522 40
//     E-mail: office@feuerwehr-krems.at
// 
//     This software is furnished under a license and may be
//     used  and copied only in accordance with the terms of
//     such  license  and  with  the  inclusion of the above
//     copyright  notice.  This software or any other copies
//     thereof   may  not  be  provided  or  otherwise  made
//     available  to  any  other  person.  No  title  to and
//     ownership of the software is hereby transferred.
// 
//     The information in this software is subject to change
//     without  notice  and  should  not  be  construed as a
//     commitment by Freiwillige Feuerwehr Krems/Donau.
// 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace At.FF.Krems.Config_Gui
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using At.FF.Krems.Config_Gui.Annotations;
    using At.FF.Krems.Configuration.Google.StaticMaps.Enums;
    using At.FF.Krems.Configuration.XML;

    /// <summary>The browser config view model.</summary>
    public class BrowserConfigViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>The selected window.</summary>
        private Window selectedWindow;

        /// <summary>The selected cookie</summary>
        private Cookie selectedCookie;

        #endregion

        #region Ctor

        /// <summary>Initializes a new instance of the <see cref="BrowserConfigViewModel"/> class.</summary>
        public BrowserConfigViewModel()
        {
            this.Config = new BrowserConfig { PrintSettings = new PrintSettings(), ScreenRefresher = new ScreenRefresher { Duration = 30, Enabled = true, Height = 50, Interval = 60, RunAtStartup = true }, ClearCookiesAtStartup = false, Proxy = new WindowProxy { Type = ProxyType.NoProxy } };
            this.Windows = new ObservableCollection<Window>();
            this.Windows.CollectionChanged += (sender, args) =>
            {
                this.Config.Window = ((ObservableCollection<Window>)sender).ToArray();
            };
            this.Windows.Add(new Window { Autostart = true, OnTop = true, Name = "Infoscreen", Url = "https://infoscreen.florian10.info", ShowOnScreen = 1, ZoomLevel = 1f, Position = new WindowPosition { PosX = 0, PosY = 0 }, Dimensions = new WindowDimensions { Width = "max2", Height = "max" }, ReloadInSeconds = 86400 });
            this.Windows.Add(new Window { Autostart = true, OnTop = true, Name = "Infoscreen Ruhebildschirm", Url = "http://www.feuerwehr-krems.at/Warnung/teaser_noe_all.asp", IsAlternativeWindow = true, ShowOnScreen = 1, ZoomLevel = 1f, Position = new WindowPosition { PosX = 0, PosY = 0 }, Dimensions = new WindowDimensions { Width = "max2", Height = "max" } });
            this.SelectedWindow = this.Windows[0];
            this.Cookies = new ObservableCollection<Cookie>();
            this.Cookies.CollectionChanged += (sender, args) => this.Config.Cookie = ((ObservableCollection<Cookie>)sender).ToArray();
        }

        #endregion

        #region Events

        /// <summary>Occurs when a property value changes.</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>Gets the windows.</summary>
        public ObservableCollection<Window> Windows { get; }

        /// <summary>Gets or sets the selected window.</summary>
        public Window SelectedWindow
        {
            get
            {
                return this.selectedWindow;
            }

            set
            {
                if (this.selectedWindow == value)
                {
                    return;
                }

                this.selectedWindow = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets the config.</summary>
        public BrowserConfig Config { get; }

        /// <summary>Gets or sets a value indicating whether disable screensaver permanently.</summary>
        /// <value>
        /// <c>true</c> if disable screensaver permanently otherwise, <c>false</c>.
        /// </value>
        public bool DisableScreensaverPermanently
        {
            get
            {
                return this.Config.DisableScreensaverPermanently;
            }

            set
            {
                if (this.Config.DisableScreensaverPermanently == value)
                {
                    return;
                }

                this.Config.DisableScreensaverPermanently = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets a value indicating whether print enabled.</summary>
        /// <value><c>true</c> if print enabled otherwise, <c>false</c>.</value>
        public bool PrintEnabled
        {
            get
            {
                return this.Config.PrintSettings.PrintEnabled;
            }

            set
            {
                if (this.Config.PrintSettings.PrintEnabled == value)
                {
                    return;
                }

                this.Config.PrintSettings.PrintEnabled = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the print port.</summary>
        /// <value>The print port.</value>
        public int PrintPort
        {
            get
            {
                return this.Config.PrintSettings.PrintPort;
            }

            set
            {
                if (this.Config.PrintSettings.PrintPort == value)
                {
                    return;
                }

                this.Config.PrintSettings.PrintPort = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the print URL.</summary>
        /// <value>The print URL.</value>
        public string PrintUrl
        {
            get
            {
                return this.Config.PrintSettings.PrintUrl;
            }

            set
            {
                if (this.Config.PrintSettings.PrintUrl == value)
                {
                    return;
                }

                this.Config.PrintSettings.PrintUrl = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets a value indicating whether [print on emergency].</summary>
        /// <value><c>true</c> if [print on emergency]; otherwise, <c>false</c>.</value>
        public bool PrintOnEmergency
        {
            get
            {
                return this.Config.PrintSettings.PrintOnEmergency;
            }

            set
            {
                if (this.PrintOnEmergency.Equals(value))
                {
                    return;
                }

                this.Config.PrintSettings.PrintOnEmergency = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the number of pages on emergency.</summary>
        /// <value>The number of pages on emergency.</value>
        public int NumberOfPagesOnEmergency
        {
            get
            {
                return this.Config.PrintSettings.NumberOfPagesOnEmergency;
            }

            set
            {
                if (this.NumberOfPagesOnEmergency.Equals(value))
                {
                    return;
                }

                this.Config.PrintSettings.NumberOfPagesOnEmergency = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the number of pages per click.</summary>
        /// <value>The number of pages per click.</value>
        public int NumberOfPagesPerClick
        {
            get
            {
                return this.Config.PrintSettings.NumberOfPagesPerClick;
            }

            set
            {
                if (this.NumberOfPagesPerClick.Equals(value))
                {
                    return;
                }

                this.Config.PrintSettings.NumberOfPagesPerClick = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the maximum number of hydrants.</summary>
        /// <value>The maximum number of hydrants.</value>
        public int MaxHydrants
        {
            get
            {
                return this.Config.PrintSettings.MaxHydrants;
            }

            set
            {
                if (this.MaxHydrants.Equals(value))
                {
                    return;
                }

                this.Config.PrintSettings.MaxHydrants = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the runtime.</summary>
        /// <value>The runtime.</value>
        public string Runtime
        {
            get
            {
                return this.Config.Runtime;
            }

            set
            {
                if (this.Config.Runtime == value)
                {
                    return;
                }

                this.Config.Runtime = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the screen refresher duration.</summary>
        /// <value>The duration of the screen refresher.</value>
        public int ScreenRefresherDuration
        {
            get
            {
                return this.Config.ScreenRefresher.Duration;
            }

            set
            {
                if (this.Config.ScreenRefresher.Duration == value)
                {
                    return;
                }

                this.Config.ScreenRefresher.Duration = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets a value indicating whether screen refresher enabled.</summary>
        /// <value>
        /// <c>true</c> if screen refresher enabled otherwise, <c>false</c>.
        /// </value>
        public bool ScreenRefresherEnabled
        {
            get
            {
                return this.Config.ScreenRefresher.Enabled;
            }

            set
            {
                if (this.Config.ScreenRefresher.Enabled == value)
                {
                    return;
                }

                this.Config.ScreenRefresher.Enabled = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the screen refresher height.</summary>
        /// <value>The height of the screen refresher.</value>
        public int ScreenRefresherHeight
        {
            get
            {
                return this.Config.ScreenRefresher.Height;
            }

            set
            {
                if (this.Config.ScreenRefresher.Height == value)
                {
                    return;
                }

                this.Config.ScreenRefresher.Height = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the screen refresher interval.</summary>
        /// <value>The screen refresher interval.</value>
        public int ScreenRefresherInterval
        {
            get
            {
                return this.Config.ScreenRefresher.Interval;
            }

            set
            {
                if (this.Config.ScreenRefresher.Interval == value)
                {
                    return;
                }

                this.Config.ScreenRefresher.Interval = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets a value indicating whether screen refresher run at startup.</summary>
        /// <value>
        /// <c>true</c> if screen refresher run at startup otherwise, <c>false</c>.
        /// </value>
        public bool ScreenRefresherRunAtStartup
        {
            get
            {
                return this.Config.ScreenRefresher.RunAtStartup;
            }

            set
            {
                if (this.Config.ScreenRefresher.RunAtStartup == value)
                {
                    return;
                }

                this.Config.ScreenRefresher.RunAtStartup = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the type of the proxy.</summary>
        /// <value>The type of the proxy.</value>
        public ProxyType ProxyType
        {
            get
            {
                return this.Config.Proxy.Type;
            }

            set
            {
                if (this.Config.Proxy.Type == value)
                {
                    return;
                }

                this.Config.Proxy.Type = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the proxy server.</summary>
        /// <value>The proxy server.</value>
        public string ProxyServer
        {
            get
            {
                return this.Config.Proxy.Server;
            }

            set
            {
                if (this.Config.Proxy.Server == value)
                {
                    return;
                }

                this.Config.Proxy.Server = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the proxy port.</summary>
        /// <value>The proxy port.</value>
        public int ProxyPort
        {
            get
            {
                return this.Config.Proxy.Port;
            }

            set
            {
                if (this.Config.Proxy.Port == value)
                {
                    return;
                }

                this.Config.Proxy.Port = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the proxy URL.</summary>
        /// <value>The proxy URL.</value>
        public string ProxyUrl
        {
            get
            {
                return this.Config.Proxy.Url;
            }

            set
            {
                if (this.Config.Proxy.Url == value)
                {
                    return;
                }

                this.Config.Proxy.Url = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets the proxy type list.</summary>
        /// <value>The proxy type list.</value>
        public IEnumerable<ProxyType> ProxyTypeList => Enum.GetValues(typeof(ProxyType)).Cast<ProxyType>();

        /// <summary>Gets the map type list.</summary>
        /// <value>The map type list.</value>
        public IEnumerable<MapType> MapTypeList => Enum.GetValues(typeof(MapType)).Cast<MapType>();

        /// <summary>Gets or sets the type of the map.</summary>
        /// <value>The type of the map.</value>
        public MapType MapType
        {
            get
            {
                return this.Config.PrintSettings.MapType;
            }

            set
            {
                if (this.Config.PrintSettings.MapType == value)
                {
                    return;
                }

                this.Config.PrintSettings.MapType = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether clear cookies at startup.
        /// </summary>
        /// <value>
        /// <c>true</c> if clear cookies at startup otherwise, <c>false</c>.
        /// </value>
        public bool ClearCookiesAtStartup
        {
            get
            {
                return this.Config.ClearCookiesAtStartup;
            }

            set
            {
                if (this.Config.ClearCookiesAtStartup == value)
                {
                    return;
                }

                this.Config.ClearCookiesAtStartup = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets the cookies.</summary>
        /// <value>The cookies.</value>
        public ObservableCollection<Cookie> Cookies { get; }

        /// <summary>Gets or sets the selected cookie.</summary>
        /// <value>The selected cookie.</value>
        public Cookie SelectedCookie
        {
            get
            {
                return this.selectedCookie;
            }

            set
            {
                if (this.selectedCookie == value)
                {
                    return;
                }

                this.selectedCookie = value;
                this.OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        /// <summary>Called when [property changed].</summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}