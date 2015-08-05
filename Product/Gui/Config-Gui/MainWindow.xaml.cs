// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Xml;
    using System.Xml.Serialization;

    using Configuration.XML;
    using Utils;
    using Utils.Extensions;

    using KellermanSoftware.CompareNetObjects;

    using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

    /// <summary>Interaction logic for MainWindow.XAML</summary>
    public partial class MainWindow
    {
        #region Fields

        /// <summary>The browser config default.</summary>
        private BrowserConfig browserConfigDefault;

        #endregion

        #region Ctor

        /// <summary>Initializes a new instance of the <see cref="MainWindow"/> class.</summary>
        public MainWindow()
        {
            this.InitializeComponent();
            if (File.Exists(Constants.XmlFile))
            {
                var xmlSerializer = new XmlSerializer(typeof(BrowserConfig));
                using (var xmlReader = XmlReader.Create(Constants.XmlFile))
                {
                    var browserConfig = (BrowserConfig)xmlSerializer.Deserialize(xmlReader);
                    if (browserConfig != null)
                    {
                        this.SetViewModelProperties(browserConfig);
                    }
                }

                this.SetBrowserConfigDefault();
            }
            else
            {
                this.SaveOnClick(this, null);
            }

            this.KeyDown += this.OnKeyDown;
        }

        #endregion

        #region Properties

        /// <summary>Gets the view model.</summary>
        /// <value>The view model.</value>
        private BrowserConfigViewModel ViewModel => (BrowserConfigViewModel)this.DataContext;

        /// <summary>Gets the height of the expander.</summary>
        /// <value>The height of the expander.</value>
        private double ExpanderHeight => (double)this.Resources["ExpanderHeight"];

        /// <summary>Gets the width of the expander.</summary>
        /// <value>The width of the expander.</value>
        private double ExpanderWidth => (double)this.Resources["ExpanderWidth"];

        #endregion

        #region Methods

        /// <summary>The get data from list box.</summary>
        /// <param name="source">The source.</param>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="object"/>.</returns>
        private static object GetDataFromListBox(ListBox source, Point point)
        {
            var element = source.InputHitTest(point) as UIElement;
            if (element == null)
            {
                return null;
            }

            var data = DependencyProperty.UnsetValue;
            while (data == DependencyProperty.UnsetValue)
            {
                if (element == null)
                {
                    continue;
                }

                data = source.ItemContainerGenerator.ItemFromContainer(element);

                if (data == DependencyProperty.UnsetValue)
                {
                    element = VisualTreeHelper.GetParent(element) as UIElement;
                }

                if (source.Equals(element))
                {
                    return null;
                }
            }

            return data != DependencyProperty.UnsetValue ? data : null;
        }

        /// <summary>The set browser config default.</summary>
        private void SetBrowserConfigDefault()
        {
            this.browserConfigDefault = ((BrowserConfigViewModel)this.DataContext).Config.Clone();
        }

        /// <summary>Sets the view model properties.</summary>
        /// <param name="config">The configuration.</param>
        private void SetViewModelProperties(BrowserConfig config)
        {
            this.ViewModel.DisableScreensaverPermanently = config.DisableScreensaverPermanently;

            // Print settings
            this.ViewModel.PrintUrl = config.PrintSettings.PrintUrl;
            this.ViewModel.PrintPort = config.PrintSettings.PrintPort;
            this.ViewModel.PrintEnabled = config.PrintSettings.PrintEnabled;
            this.ViewModel.PrintOnEmergency = config.PrintSettings.PrintOnEmergency;
            this.ViewModel.NumberOfPagesPerClick = config.PrintSettings.NumberOfPagesPerClick;
            this.ViewModel.NumberOfPagesOnEmergency = config.PrintSettings.NumberOfPagesOnEmergency;
            this.ViewModel.MapType = config.PrintSettings.MapType;
            this.ViewModel.MaxHydrants = config.PrintSettings.MaxHydrants;

            this.ViewModel.Runtime = config.Runtime;
            this.ViewModel.ScreenRefresherDuration = config.ScreenRefresher.Duration;
            this.ViewModel.ScreenRefresherEnabled = config.ScreenRefresher.Enabled;
            this.ViewModel.ScreenRefresherHeight = config.ScreenRefresher.Height;
            this.ViewModel.ScreenRefresherInterval = config.ScreenRefresher.Interval;
            this.ViewModel.ScreenRefresherRunAtStartup = config.ScreenRefresher.RunAtStartup;
            this.ViewModel.ProxyPort = config.Proxy.Port;
            this.ViewModel.ProxyServer = config.Proxy.Server;
            this.ViewModel.ProxyType = config.Proxy.Type;
            this.ViewModel.ProxyUrl = config.Proxy.Url;
            this.ViewModel.Cookies.Clear();
            if (config.Cookie != null && config.Cookie.Any())
            {
                foreach (var cookie in config.Cookie)
                {
                    this.ViewModel.Cookies.Add(cookie);
                }

                this.ViewModel.SelectedCookie = this.ViewModel.Cookies.First();
            }

            this.ViewModel.Windows.Clear();
            if (config.Window != null && config.Window.Any())
            {
                foreach (var window in config.Window)
                {
                    this.ViewModel.Windows.Add(window);
                }

                this.ViewModel.SelectedWindow = this.ViewModel.Windows.First();
            }
        }

        /// <summary>The cookie expander on expanded.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void CookieExpanderOnExpanded(object sender, RoutedEventArgs e)
        {
            this.Height += this.ExpanderHeight;
        }

        /// <summary>The cookie expander on collapsed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void CookieExpanderOnCollapsed(object sender, RoutedEventArgs e)
        {
            this.Height -= this.ExpanderHeight;
        }

        /// <summary>Generals the expander on expanded.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void GeneralExpanderOnExpanded(object sender, RoutedEventArgs e)
        {
            this.Width += this.ExpanderWidth;
        }

        /// <summary>Generals the expander on collapsed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void GeneralExpanderOnCollapsed(object sender, RoutedEventArgs e)
        {
            this.Width -= this.ExpanderWidth;
        }

        /// <summary>The add on click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void AddOnClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Windows.Add(new Configuration.XML.Window
                                       {
                                           Name = Properties.Resources.EnterName_DE_AT, 
                                           Autostart = true, 
                                           OnTop = true, 
                                           ShowOnScreen = 1, 
                                           Position = new WindowPosition { PosX = 0, PosY = 0 }, 
                                           Dimensions = new WindowDimensions { Height = "max", Width = "max2" }, 
                                           IsAlternativeWindow = false, 
                                           ReloadInSeconds = 0, 
                                           ZoomLevel = 1f
                                       });
            this.ViewModel.SelectedWindow = this.ViewModel.Windows.Last();
        }

        /// <summary>The remove on click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void RemoveOnClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Windows.Remove(this.ViewModel.SelectedWindow);
            this.ViewModel.SelectedWindow = this.ViewModel.Windows.First();
        }

        /// <summary>Resets the on click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ResetOnClick(object sender, RoutedEventArgs e)
        {
            this.SetViewModelProperties(this.browserConfigDefault);
        }

        /// <summary>The save on click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SaveOnClick(object sender, RoutedEventArgs e)
        {
            this.SetBrowserConfigDefault();

            var serializer = new XmlSerializer(this.ViewModel.Config.GetType());
            using (var writer = File.Create(Constants.XmlFile))
            {
                serializer.Serialize(writer, this.ViewModel.Config);
            }
        }

        /// <summary>The element list on preview mouse move.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void ElementListOnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed || !(sender is ListBox))
            {
                return;
            }

            var listBox = (ListBox)sender;
            var data = GetDataFromListBox(listBox, e.GetPosition(listBox));

            if (data != null)
            {
                DragDrop.DoDragDrop(listBox, data, DragDropEffects.Move);
            }
        }

        /// <summary>The element list on drop.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        private void ElementListOnDrop(object sender, DragEventArgs e)
        {
            var droppedData = e.Data.GetData(typeof(Configuration.XML.Window)) as Configuration.XML.Window;
            var listBox = sender as ListBox;
            if (listBox == null)
            {
                return;
            }

            var target = GetDataFromListBox(listBox, e.GetPosition(listBox)) as Configuration.XML.Window;

            var removedIdx = this.ViewModel.Windows.IndexOf(droppedData);
            var targetIdx = target == null ? this.ViewModel.Windows.Count - 1 : this.ViewModel.Windows.IndexOf(target);

            if (removedIdx < targetIdx)
            {
                this.ViewModel.Windows.Insert(targetIdx + 1, droppedData);
                this.ViewModel.Windows.RemoveAt(removedIdx);
            }
            else
            {
                var remIdx = removedIdx + 1;
                if (this.ViewModel.Windows.Count + 1 <= remIdx)
                {
                    return;
                }

                this.ViewModel.Windows.Insert(targetIdx, droppedData);
                this.ViewModel.Windows.RemoveAt(remIdx);
            }

            this.ViewModel.SelectedWindow = this.ViewModel.Windows[targetIdx];
        }

        /// <summary>The move up on click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MoveUpOnClick(object sender, RoutedEventArgs e)
        {
            var oldIndex = this.ViewModel.Windows.IndexOf(this.ViewModel.SelectedWindow);
            this.ViewModel.Windows.Insert(oldIndex - 1, this.ViewModel.SelectedWindow);
            this.ViewModel.Windows.RemoveAt(oldIndex + 1);
            this.ViewModel.SelectedWindow = this.ViewModel.Windows[oldIndex - 1];
        }

        /// <summary>The move down on click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MoveDownOnClick(object sender, RoutedEventArgs e)
        {
            var oldIndex = this.ViewModel.Windows.IndexOf(this.ViewModel.SelectedWindow);
            this.ViewModel.Windows.Insert(oldIndex + 2, this.ViewModel.SelectedWindow);
            this.ViewModel.Windows.RemoveAt(oldIndex);
            this.ViewModel.SelectedWindow = this.ViewModel.Windows[oldIndex + 1];
        }

        /// <summary>Adds the cookie on click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void AddCookieOnClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Cookies.Add(new Cookie { Name = Properties.Resources.EnterName_DE_AT });
            this.ViewModel.SelectedCookie = this.ViewModel.Cookies.Last();
        }

        /// <summary>Removes the cookie on click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RemoveCookieOnClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Cookies.Remove(this.ViewModel.SelectedCookie);
            this.ViewModel.SelectedCookie = (this.ViewModel.Cookies != null && this.ViewModel.Cookies.Any()) ? this.ViewModel.Cookies.First() : null;
        }

        /// <summary>The main window on closing.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        private void MainWindowOnClosing(object sender, CancelEventArgs e)
        {
            var compareObjects = new CompareLogic();
            if (compareObjects.Compare(this.ViewModel.Config, this.browserConfigDefault).AreEqual)
            {
                return;
            }

            var result = MessageBox.Show(Properties.Resources.SaveContent_DE_AT, Properties.Resources.Save_DE_AT + "?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, this.FindResource("MessageBoxStyle") as Style);
            switch (result)
            {
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    return;
                case MessageBoxResult.Yes:
                    this.SaveOnClick(sender, null);
                    break;
            }
        }

        /// <summary>The on key down.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="keyEventArgs">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control || keyEventArgs.Key != Key.S)
            {
                return;
            }

            this.SaveOnClick(this, null);
        }

        /// <summary>The list box on preview mouse left button down.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void ListBoxOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == null || listBox.SelectedItems.Count != 1)
            {
                return;
            }

            var container = listBox.ItemContainerGenerator.ContainerFromItem(listBox.SelectedItems[0]) as UIElement;
            if (container == null)
            {
                return;
            }

            var pos = e.GetPosition(container);
            var result = VisualTreeHelper.HitTest(container, pos);
            if (result != null)
            {
                e.Handled = true;
            }
        }

        #endregion
    }
}