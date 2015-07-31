// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalEnvironment.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>The global environment.</summary>
    public static class GlobalEnvironment
    {
        /// <summary>The emergency lock object.</summary>
        private static readonly object EmergencyLockObject = new object();

        /// <summary>The emergency active field.</summary>
        private static bool emergencyActiveField;

        /// <summary>The main thread managed id.</summary>
        private static int mainThreadManagedId = -1;

        /// <summary>The emergency state changed.</summary>
        public static event EventHandler EmergencyStateChanged;

        /// <summary>Gets or sets a value indicating whether emergency active.</summary>
        public static bool EmergencyActive
        {
            get
            {
                lock (EmergencyLockObject)
                {
                    return emergencyActiveField;
                }
            }

            set
            {
                lock (EmergencyLockObject)
                {
                    if (emergencyActiveField == value)
                    {
                        return;
                    }

                    emergencyActiveField = value;
                    if (EmergencyStateChanged != null)
                    {
                        Task.Factory.StartNew(() => EmergencyStateChanged(null, null));
                    }
                }
            }
        }

        /// <summary>Gets or sets the main thread managed id.</summary>
        public static int MainThreadManagedId
        {
            get
            {
                return mainThreadManagedId;
            }

            set
            {
                if (mainThreadManagedId != -1)
                {
                    return;
                }

                mainThreadManagedId = value;
            }
        }

        /// <summary>Gets a value indicating whether is main thread.</summary>
        public static bool IsMainThread
        {
            get
            {
                return Thread.CurrentThread.ManagedThreadId == MainThreadManagedId;
            }
        }
    }
}