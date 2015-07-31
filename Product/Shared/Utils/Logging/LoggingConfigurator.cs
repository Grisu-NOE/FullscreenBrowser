// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingConfigurator.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.Utils.Logging
{
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    using At.FF.Krems.Interfaces;
    using At.FF.Krems.Utils.Extensions;

    using log4net;
    using log4net.Config;

    using Bootstrapper = At.FF.Krems.Utils.Bootstrapper.Bootstrapper;

    /// <summary>The logging configurator.</summary>
    public class LoggingConfigurator : ILoggingConfigurator
    {
        #region Fields

        /// <summary>Source code backup XML.</summary>
        private const string SourceCodeBackUpXml = @"<?xml version='1.0' encoding='utf-8'?>
    <log4net update='Overwrite'>
        <appender name='FILE_TRACE' type='log4net.Appender.RollingFileAppender'>
            <!-- ROLLING TRACE APPENDER -->
            <file type='log4net.Util.PatternString' value='%property{PATHNAME}\%property{FILENAME_TRACE}'/>
            <rollingStyle value='Size'/>
            <staticLogFileName value='false'/>
            <AppendToFile value='true'/>
            <CountDirection value='-1'/>
            <maximumFileSize value='20MB'/>
            <PreserveLogFileNameExtension value='true'/>
            <maxSizeRollBackups value='7'/>
            <layout type='log4net.Layout.PatternLayout'>
                <conversionPattern value='%d %-7level %m - %c [%t] %n'/>
            </layout>
        </appender>
        <appender name='FILE_DEBUG' type='log4net.Appender.RollingFileAppender'>
            <!-- ROLLING DEBUG APPENDER -->
            <file type='log4net.Util.PatternString' value='%property{PATHNAME}\%property{FILENAME_DEBUG}'/>
            <rollingStyle value='Size'/>
            <staticLogFileName value='false'/>
            <AppendToFile value='true'/>
            <CountDirection value='-1'/>
            <maximumFileSize value='10MB'/>
            <maxSizeRollBackups value='7'/>
            <PreserveLogFileNameExtension value='true'/>
            <threshold value='DEBUG'/>
            <layout type='log4net.Layout.PatternLayout'>
                <conversionPattern value='%d %-7level %m - %c [%t] %n'/>
            </layout>
        </appender>
        <appender name='FILE_NOTICE' type='log4net.Appender.RollingFileAppender'>
            <!-- ROLLING NOTICE APPENDER -->
            <file type='log4net.Util.PatternString' value='%property{PATHNAME}\%property{FILENAME_NOTICE}'/>
            <rollingStyle value='Size'/>
            <staticLogFileName value='false'/>
            <AppendToFile value='true'/>
            <CountDirection value='-1'/>
            <maximumFileSize value='5MB'/>
            <maxSizeRollBackups value='7'/>
            <PreserveLogFileNameExtension value='true'/>
            <threshold value='NOTICE'/>
            <layout type='log4net.Layout.PatternLayout'>
                <conversionPattern value='%d %-7level %m - %c [%t] %n'/>
            </layout>
        </appender>
        <appender name='CONSOLE_TRACE' type='log4net.Appender.ColoredConsoleAppender'>
            <!-- CONSOLE TRACE APPENDER -->
            <layout type='log4net.Layout.PatternLayout'>
                <conversionPattern value='%d %-7level %m - %c [%t] %n'/>
            </layout>
            <mapping>
                <level value='Notice'/>
                <foreColor value='White'/>
                <backColor value='Blue'/>
            </mapping>
            <mapping>
                <level value='Warn'/>
                <foreColor value='Red, HighIntensity'/>
            </mapping>
            <mapping>
                <level value='Error'/>
                <foreColor value='White'/>
                <backColor value='Red'/>
            </mapping>
            <mapping>
                <level value='Fatal'/>
                <foreColor value='White'/>
                <backColor value='Red, HighIntensity'/>
            </mapping>
            <mapping>
                <level value='Info'/>
                <foreColor value='Yellow, HighIntensity'/>
            </mapping>
        </appender>
        <appender name='CONSOLE_DEBUG' type='log4net.Appender.ColoredConsoleAppender'>
            <!-- CONSOLE DEBUG APPENDER -->
            <layout type='log4net.Layout.PatternLayout'>
                <conversionPattern value='%d %-7level %m - %c [%t] %n'/>
            </layout>
            <threshold value='DEBUG'/>
            <mapping>
                <level value='Notice'/>
                <foreColor value='White'/>
                <backColor value='Blue'/>
            </mapping>
            <mapping>
                <level value='Warn'/>
                <foreColor value='Red, HighIntensity'/>
            </mapping>
            <mapping>
                <level value='Error'/>
                <foreColor value='White'/>
                <backColor value='Red'/>
            </mapping>
            <mapping>
                <level value='Fatal'/>
                <foreColor value='White'/>
                <backColor value='Red, HighIntensity'/>
            </mapping>
            <mapping>
                <level value='Info'/>
                <foreColor value='Yellow, HighIntensity'/>
            </mapping>
        </appender>
        <appender name='CONSOLE_INFO' type='log4net.Appender.ColoredConsoleAppender'>
            <!-- CONSOLE INFO APPENDER -->
            <layout type='log4net.Layout.PatternLayout'>
                <conversionPattern value='%d %-7level %m - %c [%t] %n'/>
            </layout>
            <threshold value='INFO'/>
            <mapping>
                <level value='Notice'/>
                <foreColor value='White'/>
                <backColor value='Blue'/>
            </mapping>
            <mapping>
                <level value='Warn'/>
                <foreColor value='Red, HighIntensity'/>
            </mapping>
            <mapping>
                <level value='Error'/>
                <foreColor value='White'/>
                <backColor value='Red'/>
            </mapping>
            <mapping>
                <level value='Fatal'/>
                <foreColor value='White'/>
                <backColor value='Red, HighIntensity'/>
            </mapping>
            <mapping>
                <level value='Info'/>
                <foreColor value='Yellow, HighIntensity'/>
            </mapping>
        </appender>
        <!-- Setup the root category, add the appenders and set the default level -->
        <root>
            <level value='DEBUG'/>
            <appender-ref ref='FILE_DEBUG'/>
            <appender-ref ref='FILE_NOTICE'/>
            <appender-ref ref='CONSOLE_INFO'/>
        </root>
        <!-- To enable TRACE => change root tag
       <root> 
        <level value='ALL' /> 
        <appender-ref ref='FILE_TRACE' /> 
        <appender-ref ref='FILE_WARN' /> 
        <appender-ref ref='CONSOLE_TRACE' /> 
      </root> -->
    </log4net>";

        /// <summary>The logger for this class.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The initialize called</summary>
        private bool initializeCalled;

        #endregion

        #region Properties

        /// <summary>Gets the logging directory.</summary>
        public string LoggingDirectory { get; private set; }

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

        /// <summary>Initializes this instance.</summary>
        /// <param name="force">Force initialization, even if already initialized.</param>
        private void Initialize(bool force = false)
        {
            if (this.initializeCalled && !force)
            {
                return;
            }

            this.initializeCalled = true;
            this.LoggingDirectory = Path.Combine(new FileInfo(ReflectionExtensions.GetEntryAssembly().Location).DirectoryName, "Logging");
            Directory.CreateDirectory(this.LoggingDirectory);
            var configFile = new FileInfo(Path.Combine(this.LoggingDirectory, "Log4NetConfig_" + ReflectionExtensions.GetEntryAssembly().GetName().Name + ".xml"));
            if (!configFile.Exists)
            {
                var backUpConfigFile = new FileInfo(Path.Combine(this.LoggingDirectory, "Log4NetDefaultConfig.xml"));
                if (!backUpConfigFile.Exists)
                {
                    Logger.Warn("Process -> " + Process.GetCurrentProcess().ProcessName + " -> File for log4net configuration (Log4NetDefaultConfig.xml) was not found. File will be created with backup configuration from SourceCode.");
                    backUpConfigFile.Create().Close();
                    File.WriteAllLines(backUpConfigFile.FullName, new[] { SourceCodeBackUpXml.Replace("'", "\"") });
                }

                backUpConfigFile.CopyTo(Path.Combine(this.LoggingDirectory, "Log4NetConfig_" + ReflectionExtensions.GetEntryAssembly().GetName().Name + ".xml"), true);
            }

            GlobalContext.Properties["PATHNAME"] = "Logging";
            GlobalContext.Properties["FILENAME_TRACE"] = string.Format("{0}_TRACE.log", ReflectionExtensions.GetEntryAssembly().GetName().Name);
            GlobalContext.Properties["FILENAME_DEBUG"] = string.Format("{0}_DEBUG.log", ReflectionExtensions.GetEntryAssembly().GetName().Name);
            GlobalContext.Properties["FILENAME_NOTICE"] = string.Format("{0}.log", ReflectionExtensions.GetEntryAssembly().GetName().Name);

            XmlConfigurator.ConfigureAndWatch(configFile);

            // Log Process Start now !
            Bootstrapper.GetInstance<IProcessStateLogging>().LogProcessStart();
        }

        #endregion
    }
}