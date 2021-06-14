// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Markup;

    using At.FF.Krems.FullscreenBrowser.CustomBootstrapper;
    using At.FF.Krems.Interfaces;
    using At.FF.Krems.Utils.Bootstrapper;

    using log4net;

    /// <summary>The program.</summary>
    public static class Program
    {
        #region Fields

        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        /// <summary>The main.</summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [STAThread]
        public static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            int exitcode;

            try
            {
                // Ensure the current culture passed into bindings is the OS culture.
                // By default, WPF uses en-US as the culture, regardless of the system settings.
                FrameworkElement.LanguageProperty.OverrideMetadata(
                    typeof(FrameworkElement),
                      new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
                var currentDirectory = new FileInfo(typeof(Program).Assembly.Location).DirectoryName;
                if (currentDirectory != null)
                {
                    Environment.CurrentDirectory = currentDirectory;
                }

                // Register and initialize
                Bootstrapper.Initialize(new StructureMapRegistry());

                // Run
                exitcode = Bootstrapper.GetInstance<App>().Run();
            }
            catch (Exception exception)
            {
                Logger.Info("In case of `System.DllNotFoundException`, try to install `Visual C++ Redistributable for Visual Studio 2015` from https://www.microsoft.com/de-at/download/details.aspx?id=48145");
                Logger.Error(exception);
                Logger.Debug(Bootstrapper.WhatDoIHave());
                exitcode = -1;
            }

            var log = Bootstrapper.TryGetInstance<IProcessStateLogging>();
            log?.LogProcessExited();

            return exitcode;
        }

        /// <summary>
        /// Logs unhandled exceptions (including exception that corrupt process state including StackOverflowExceptions, ExecutionEngineExceptions, ...)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="unhandledExceptionEventArgs">Unhandled Exception Event Args</param>
        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            Logger.FatalFormat("Unhandled Exception occured:\n{0}", unhandledExceptionEventArgs.ExceptionObject);
        }
    }
}