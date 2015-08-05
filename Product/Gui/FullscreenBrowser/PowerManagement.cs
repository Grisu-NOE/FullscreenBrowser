// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PowerManagement.cs" company="Freiwillige Feuerwehr Krems/Donau">
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

    using At.FF.Krems.Interfaces;
    using At.FF.Krems.Utils.Bootstrapper;

    /// <summary>The power management.</summary>
    public class PowerManagement : IPowerManagement
    {
        #region Constants

        /// <summary>The file share read.</summary>
        private const int FileShareRead = 0x00000001;

        /// <summary>The file share write.</summary>
        private const int FileShareWrite = 0x00000002;

        /// <summary>The open existing.</summary>
        private const int OpenExisting = 3;

        /// <summary>The privilege enabled.</summary>
        private const int SePrivilegeEnabled = 0x00000002;

        /// <summary>The token query.</summary>
        private const int TokenQuery = 0x00000008;

        /// <summary>The token adjust privileges.</summary>
        private const int TokenAdjustPrivileges = 0x00000020;

        /// <summary>The shutdown name.</summary>
        private const string SeShutdownName = "SeShutdownPrivilege";

        #endregion

        #region Fields

        /// <summary>The poll power status last poll.</summary>
        private readonly DateTime pollPowerStatusLastPoll = DateTime.MinValue;

        /// <summary>The power status.</summary>
        private NativeMethods.StructSystemPowerStatus powerStatus;

        /// <summary>The poll power status every sec.</summary>
        private int pollPowerStatusEverySec = 5;

        #endregion

        #region Enums

        /// <summary>The shutdown flags.</summary>
        [Flags]
        private enum ShutdownFlags : uint
        {
            /// <summary>The log off.</summary>
            LogOff = 0x00000000,

            /// <summary>The shutdown.</summary>
            Shutdown = 0x00000001,

            /// <summary>The restart.</summary>
            Restart = 0x00000002,

            /// <summary>The power off.</summary>
            PowerOff = 0x00000008,

            /// <summary>The force.</summary>
            Force = 0x00000004,

            /// <summary>The force if hung.</summary>
            ForceIfHung = 0x00000010
        }

        #endregion

        #region Properties

        /// <summary>Gets or sets the polling interval (in seconds) of battery information.</summary>
        public int BatteryUpdateEvery
        {
            get
            {
                // return update value
                return this.pollPowerStatusEverySec;
            }

            set
            {
                // set update value
                if (value > 0 && value < 43200)
                {
                    this.pollPowerStatusEverySec = value;
                }
                else
                {
                    this.pollPowerStatusEverySec = 5;
                }
            }
        }

        /// <summary>Gets percentage (0-100) of battery life remaining.</summary>
        public int BatteryLifePercent
        {
            get
            {
                // return BatteryLifePercent from struct
                this.PollPowerStatus();
                int ret = this.powerStatus.BatteryLifePercent;
                if (ret == 255)
                {
                    ret = 0; // if 255 (error), state as 0
                }

                return ret;
            }
        }

        /// <summary>Gets time in seconds of battery life remaining.</summary>
        public int BatteryRemainingSeconds
        {
            get
            {
                // return BatteryLifeTime from struct
                this.PollPowerStatus();
                var ret = this.powerStatus.BatteryLifeTime;
                if (ret == -1)
                {
                    ret = 0; // if -1 (error), state as 0               
                }

                return ret;
            }
        }

        /// <summary>Gets total time in seconds of battery life.</summary>
        public int BatteryTotalSeconds
        {
            get
            {
                // return BatteryFullLifeTime from struct
                this.PollPowerStatus();
                var ret = this.powerStatus.BatteryFullLifeTime;
                if (ret == -1)
                {
                    ret = 0; // if -1 (error), state as 0
                }

                return ret;
            }
        }

        /// <summary>Gets a value indicating whether the computer has a battery.</summary>
        public bool HasBattery
        {
            get
            {
                // return if system has battery (BatteryFlag < 128)
                this.PollPowerStatus();
                return this.powerStatus.BatteryFlag < 128;
            }
        }

        /// <summary>Gets 3 tier battery gauge, high, low and critical.</summary>
        public PowerBatteryLevel BatteryLevel
        {
            get
            {
                // read BatteryFlag from struct and set level flag
                this.PollPowerStatus();
                var ret = PowerBatteryLevel.BatteryHigh;
                switch (this.powerStatus.BatteryFlag)
                {
                    case 4:
                        ret = PowerBatteryLevel.BatteryCritical;
                        break;
                    case 2:
                        ret = PowerBatteryLevel.BatteryLow;
                        break;
                }

                return ret;
            }
        }

        /// <summary>Gets a value indicating whether the battery is charging.</summary>
        public bool BatteryCharging
        {
            get
            {
                // return if flag is charging
                this.PollPowerStatus();
                return this.powerStatus.BatteryFlag == 8;
            }
        }

        /// <summary>Gets a value indicating whether the computer is currently running off main power.</summary>
        public bool UsingAc
        {
            get
            {
                // return if ac flag from struct
                this.PollPowerStatus();
                return this.powerStatus.ACLineStatus == 1;
            }
        }

        /// <summary>Gets a value indicating whether system allows hibernation.</summary>
        public bool CanHibernate => NativeMethods.IsPwrHibernateAllowed();

        /// <summary>Gets a value indicating whether system allows the shutdown operation.</summary>
        public bool CanShutdown => NativeMethods.IsPwrShutdownAllowed();

        /// <summary>Gets a value indicating whether system allows suspend.</summary>
        public bool CanSuspend => NativeMethods.IsPwrSuspendAllowed();

        #endregion

        #region Methods

        #region Public

        /// <summary>Checks is a HDD is currently sleeping (wound down).</summary>
        /// <param name="deviceId">Integer value for volume, e.g. 0.</param>
        /// <returns>Boolean value stating if the HDD in question is sleeping.</returns>
        public bool DriveAsleep(int deviceId)
        {
            // call power state api and return if drive is asleep
            bool fOn, ret = true;
            var handle = GetDeviceHandle(deviceId);
            if (handle != IntPtr.Zero && NativeMethods.GetDevicePowerState(handle, out fOn))
            {
                ret = fOn;
            }

            NativeMethods.CloseHandle(handle);
            return !ret;
        }

        /// <summary>Set the power requirements for the current application, e.g. Hold display and system from suspend.</summary>
        /// <param name="threadReq">Flag stating suspend mode, or release.</param>
        public void SetPowerReq(PowerThreadRequirements threadReq)
        {
            if (Bootstrapper.GetInstance<IBrowserConfiguration>().Config.DisablePowerManagement)
            {
                return;
            }

            // Set application power requirments
            NativeMethods.SetThreadExecutionState(threadReq);
        }

        /// <summary>Lock the active workstation.</summary>
        public void LockWorkstation()
        {
            // call lock workstation
            if (!CheckEntryPoint("user32.dll", "LockWorkStation"))
            {
                throw new PlatformNotSupportedException("'LockWorkStation' method missing from 'user32.dll'!");
            }

            NativeMethods.LockWorkStation();
        }

        /// <summary>Log off the active user.</summary>
        /// <param name="force">Inform system to force operation.</param>
        public void LogOff(bool force = false)
        {
            // call log off
            CallShutdown(ShutdownFlags.LogOff, force);
        }

        /// <summary>Restart system.</summary>
        /// <param name="force">Inform system to force operation.</param>
        public void Restart(bool force = false)
        {
            // call restart
            CallShutdown(ShutdownFlags.Restart, force);
        }

        /// <summary>Shutdown system.</summary>
        /// <param name="force">Inform system to force operation.</param>
        public void Shutdown(bool force = false)
        {
            // call shutdown
            CallShutdown(ShutdownFlags.Shutdown, force);
        }

        /// <summary>Suspend system.</summary>
        /// <param name="force">Inform system to force operation.</param>
        public void Suspend(bool force = false)
        {
            // call suspend
            if (!CheckEntryPoint("powrprof.dll", "SetSuspendState"))
            {
                throw new PlatformNotSupportedException("'SetSuspendState' method missing from 'powrprof.dll'!");
            }

            NativeMethods.SetSuspendState(0, force ? 1 : 0, 0);
        }

        /// <summary>Hibernate system.</summary>
        /// <param name="force">Inform system to force operation.</param>
        public void Hibernate(bool force = false)
        {
            // call hibernate
            if (!CheckEntryPoint("powrprof.dll", "SetSuspendState"))
            {
                throw new PlatformNotSupportedException("'SetSuspendState' method missing from 'powrprof.dll'!");
            }

            NativeMethods.SetSuspendState(1, force ? 1 : 0, 0);
        }

        #endregion

        #region Private

        /// <summary>The check entry point.</summary>
        /// <param name="library">The library.</param>
        /// <param name="method">The method.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private static bool CheckEntryPoint(string library, string method)
        {
            var libPtr = NativeMethods.LoadLibrary(library);
            if (libPtr.Equals(IntPtr.Zero))
            {
                return false;
            }

            if (!NativeMethods.GetProcAddress(libPtr, method).Equals(IntPtr.Zero))
            {
                NativeMethods.FreeLibrary(libPtr);
                return true;
            }

            NativeMethods.FreeLibrary(libPtr);

            return false;
        }

        /// <summary>The call shutdown.</summary>
        /// <param name="shutdownFlag">The shutdown flag.</param>
        /// <param name="force">The force.</param>
        /// <exception cref="PlatformNotSupportedException">'GetCurrentProcess' method missing from 'kernel32'!</exception>
        private static void CallShutdown(ShutdownFlags shutdownFlag, bool force)
        {
            if (!CheckEntryPoint("kernel32.dll", "GetCurrentProcess"))
            {
                throw new PlatformNotSupportedException("'GetCurrentProcess' method missing from 'kernel32.dll'!");
            }

            // if force append to flag
            if (force)
            {
                shutdownFlag = shutdownFlag | ShutdownFlags.Force;
            }

            // open current process tokens
            var currentProcess = NativeMethods.GetCurrentProcess();
            var @null = IntPtr.Zero;

            // request privileges
            if (CheckEntryPoint("advapi32.dll", "OpenProcessToken"))
            {
                // open process token
                if (!NativeMethods.OpenProcessToken(currentProcess, TokenAdjustPrivileges | TokenQuery, ref @null))
                {
                    // failed...
                }

                // lookup privilege
                NativeMethods.LuidAtt luid;
                luid.Count = 1;
                luid.Luid = 0;
                luid.Attr = SePrivilegeEnabled;
                if (!NativeMethods.LookupPrivilegeValue(null, SeShutdownName, ref luid.Luid))
                {
                    // failed...
                }

                // adjust privileges and call shutdown
                if (NativeMethods.AdjustTokenPrivileges(@null, false, ref luid, 0, IntPtr.Zero, IntPtr.Zero))
                {
                    // failed...
                }
            }

            // assume pre 2000, and call exit windows anyway
            // call exitWindows api
            if (NativeMethods.ExitWindowsEx((int)shutdownFlag, 0))
            {
                // failed...
            }
        }

        /// <summary>The get device handle.</summary>
        /// <param name="deviceId">The device id.</param>
        /// <returns>The <see cref="IntPtr"/>.</returns>
        private static IntPtr GetDeviceHandle(int deviceId)
        {
            // drive to open, eg. \\\\.\\PhysicalDrive0
            var device = NativeMethods.CreateFile("\\\\.\\PhysicalDrive" + deviceId, 0, FileShareRead | FileShareWrite, IntPtr.Zero, OpenExisting, 0, IntPtr.Zero);
            return device.ToInt32() == -1 ? IntPtr.Zero : device;
        }

        /// <summary>The poll power status.</summary>
        private void PollPowerStatus()
        {
            if (DateTime.Now.Subtract(this.pollPowerStatusLastPoll).TotalSeconds < this.pollPowerStatusEverySec)
            {
                return;
            }

            this.powerStatus = new NativeMethods.StructSystemPowerStatus();
            NativeMethods.GetSystemPowerStatus(ref this.powerStatus);
        }

        #endregion

        #endregion
    }
}