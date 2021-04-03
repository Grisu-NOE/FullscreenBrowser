// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskbarIconViewModel.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Input;

    using Interfaces;
    using Utils.Bootstrapper;

    using Application = System.Windows.Application;

    /// <summary>
    /// Provides bindable properties and commands for the TaskbarIcon. In this sample, the
    /// view model is assigned to the TaskbarIcon in XAML. Alternatively, the startup routing
    /// in App.xaml.cs could have created this view model, and assigned it to the TaskbarIcon.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class TaskbarIconViewModel
    {
        /// <summary>Gets the logging directory.</summary>
        /// <value>The open logging directory command.</value>
        public ICommand OpenLoggingDirectoryCommand => new DelegateCommand
                                                           {
                                                               CommandAction = () => Process.Start(Bootstrapper.GetInstance<ILoggingConfigurator>().LoggingDirectory)
                                                           };

        /// <summary>Gets the clear browser cache command.</summary>
        /// <value>The clear browser cache command.</value>
        public ICommand ClearBrowserCacheCommand => new DelegateCommand
                                                        {
                                                            CommandAction = Bootstrapper.GetInstance<IWindowManager>().ClearBrowserCache
                                                        };

        /// <summary>Gets the save configuration command.</summary>
        /// <value>The save configuration command.</value>
        public ICommand SaveConfigurationCommand => new DelegateCommand
                                                        {
                                                            CommandAction = Bootstrapper.GetInstance<IBrowserConfiguration>().Save
                                                        };

        /// <summary>Gets the shut down for the application.</summary>
        /// <value>The exit application command.</value>
        public ICommand ExitApplicationCommand => new DelegateCommand
                                                      {
                                                          CommandAction = Application.Current.Shutdown
                                                      };
    }
}