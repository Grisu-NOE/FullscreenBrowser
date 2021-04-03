// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.FullscreenBrowser
{
    using System;
    using System.Windows;

    using At.FF.Krems.Interfaces;
    using At.FF.Krems.Utils.Bootstrapper;

    using Hardcodet.Wpf.TaskbarNotification;

    /// <summary>Interaction logic for App.XAML</summary>
    public partial class App : Application, IApp
    {
        /// <summary>The initialize called</summary>
        private bool initializeCalled;

        /// <summary>Gets the taskbar.</summary>
        /// <value>The taskbar.</value>
        public TaskbarIcon Taskbar { get; private set; }

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

        /// <summary>Raises the <see cref="E:System.Windows.Application.Startup" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrapper.GetInstance<IProcessStateLogging>().LogProcessReady();
        }

        /// <summary>Raises the <see cref="E:System.Windows.Application.Exit" /> event.</summary>
        /// <param name="e">An <see cref="T:System.Windows.ExitEventArgs" /> that contains the event data.</param>
        protected override void OnExit(ExitEventArgs e)
        {
            this.Taskbar.Dispose();  // The icon would clean up automatically, but this is cleaner
            base.OnExit(e);
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

            // create the taskbar icon (it's a resource declared in TaskbarIconResources.xaml
            var resourceDictionary = new ResourceDictionary
            {
                Source =
                    new Uri(
                    "/TaskbarIconResources.xaml",
                    UriKind.Relative)
            };

            Current.Resources.MergedDictionaries.Add(resourceDictionary);
            this.Taskbar = (TaskbarIcon)FindResource("TaskbarIcon");
        }
    }
}