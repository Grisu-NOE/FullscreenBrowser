// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessStateLogging.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using System;
    using System.Reflection;

    using At.FF.Krems.Interfaces;
    using At.FF.Krems.Utils.Extensions;

    using log4net;

    /// <summary>The process state logging.</summary>
    public class ProcessStateLogging : IProcessStateLogging
    {
        #region Fields

        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Public methods

        /// <summary>Log process starting.</summary>
        public void LogProcessStart()
        {
            Logger.NoticeFormat("Process {0} (Version: {1})(OSVersion: {2}) starting now ...", AppDomain.CurrentDomain.FriendlyName, ReflectionExtensions.GetEntryAssembly().GetName().Version, Environment.OSVersion);
        }

        /// <summary>Log process start communication.</summary>
        public void LogProcessStartComm()
        {
            Logger.NoticeFormat("Process {0} (Version: {1})(OSVersion: {2}) starting communication...", AppDomain.CurrentDomain.FriendlyName, ReflectionExtensions.GetEntryAssembly().GetName().Version, Environment.OSVersion);
        }

        /// <summary>Log process ready.</summary>
        public void LogProcessReady()
        {
            Logger.NoticeFormat("Process {0} (Version: {1})(OSVersion: {2}) is ready.", AppDomain.CurrentDomain.FriendlyName, ReflectionExtensions.GetEntryAssembly().GetName().Version, Environment.OSVersion);
        }

        /// <summary>Log process stopping.</summary>
        public void LogProcessStop()
        {
            Logger.NoticeFormat("Process {0} (Version: {1})(OSVersion: {2}) stopping now ...", AppDomain.CurrentDomain.FriendlyName, ReflectionExtensions.GetEntryAssembly().GetName().Version, Environment.OSVersion);
        }

        /// <summary>Log process exited.</summary>
        public void LogProcessExited()
        {
            Logger.NoticeFormat("Process {0} (Version: {1})(OSVersion: {2}) exited.", AppDomain.CurrentDomain.FriendlyName, ReflectionExtensions.GetEntryAssembly().GetName().Version, Environment.OSVersion);
        }

        #endregion
    }
}