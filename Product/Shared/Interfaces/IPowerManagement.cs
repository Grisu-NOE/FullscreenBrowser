// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPowerManagement.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.Interfaces
{
    using System;

    #region Enums

    /// <summary>The power battery level.</summary>
    [Flags]
    public enum PowerBatteryLevel
    {
        /// <summary>The battery high.</summary>
        BatteryHigh = 1,

        /// <summary>The battery low.</summary>
        BatteryLow = 2,

        /// <summary>The battery critical.</summary>
        BatteryCritical = 4,

        /// <summary>The battery charging.</summary>
        BatteryCharging = 8,

        /// <summary>The no system battery.</summary>
        NoSystemBattery = 128,

        /// <summary>The unknown.</summary>
        Unknown = 255
    }

    /// <summary>The power thread requirements.</summary>
    [Flags]
    public enum PowerThreadRequirements : uint
    {
        /// <summary>The release hold.</summary>
        ReleaseHold = 0x80000000,

        /// <summary>The hold system.</summary>
        HoldSystem = 0x00000001 | ReleaseHold,

        /// <summary>The hold display.</summary>
        HoldDisplay = 0x00000002 | ReleaseHold,

        /// <summary>The hold system and display.</summary>
        HoldSystemAndDisplay = HoldSystem | HoldDisplay | ReleaseHold,
    }

    /// <summary>The power management state of the monitor.</summary>
    public enum MonitorState
    {
        On = -1,
        Off = 2,
        Standby = 1
    }

    #endregion
    /// <summary>The PowerManagement interface.</summary>
    public interface IPowerManagement
    {
        #region Properties

        /// <summary>Gets or sets the polling interval (in seconds) of battery information.</summary>
        int BatteryUpdateEvery { get; set; }

        /// <summary>Gets percentage (0-100) of battery life remaining.</summary>
        int BatteryLifePercent { get; }

        /// <summary>Gets time in seconds of battery life remaining.</summary>
        int BatteryRemainingSeconds { get; }

        /// <summary>Gets total time in seconds of battery life.</summary>
        int BatteryTotalSeconds { get; }

        /// <summary>Gets a value indicating whether the computer has a battery.</summary>
        bool HasBattery { get; }

        /// <summary>Gets 3 tier battery gauge, high, low and critical.</summary>
        PowerBatteryLevel BatteryLevel { get; }

        /// <summary>Gets a value indicating whether the battery is charging.</summary>
        bool BatteryCharging { get; }

        /// <summary>Gets a value indicating whether the computer is currently running off main power.</summary>
        bool UsingAc { get; }

        /// <summary>Gets a value indicating whether system allows hibernation.</summary>
        bool CanHibernate { get; }

        /// <summary>Gets a value indicating whether system allows the shutdown operation.</summary>
        bool CanShutdown { get; }

        /// <summary>Gets a value indicating whether system allows suspend.</summary>
        bool CanSuspend { get; }

        #endregion

        #region Methods

        /// <summary>Checks is a HDD is currently sleeping (wound down).</summary>
        /// <param name="deviceId">Integer value for volume, e.g. 0.</param>
        /// <returns>Boolean value stating if the HDD in question is sleeping.</returns>
        bool DriveAsleep(int deviceId);

        /// <summary>Set the power requirements for the current application, e.g. Hold display and system from suspend.</summary>
        /// <param name="threadReq">Flag stating suspend mode, or release.</param>
        void SetPowerReq(PowerThreadRequirements threadReq);

        /// <summary>Lock the active workstation.</summary>
        void LockWorkstation();

        /// <summary>Log off the active user.</summary>
        /// <param name="force">Inform system to force operation.</param>
        void LogOff(bool force = false);

        /// <summary>Restart system.</summary>
        /// <param name="force">Inform system to force operation.</param>
        void Restart(bool force = false);

        /// <summary>Shutdown system.</summary>
        /// <param name="force">Inform system to force operation.</param>
        void Shutdown(bool force = false);

        /// <summary>Suspend system.</summary>
        /// <param name="force">Inform system to force operation.</param>
        void Suspend(bool force = false);

        /// <summary>Hibernate system.</summary>
        /// <param name="force">Inform system to force operation.</param>
        void Hibernate(bool force = false);

        /// <summary>
        /// Sets the power management state of the monitor (needed since Windows 8)
        /// </summary>
        /// <param name="state">The power management state of the monitor.</param>
        void SetMonitorState(MonitorState state);

        /// <summary>
        /// Sets the power management state of the monitor (needed since Windows 8)
        /// </summary>
        /// <param name="state">The power management state of the monitor.</param>
        /// <param name="windowHandle">A handle to the window whose window procedure will receive the message.
        /// If this parameter is <see cref="NativeMethods.HwndBroadcast"/>,
        /// the message is sent to all top-level windows in the system,
        /// including disabled or invisible unowned windows, overlapped windows,
        /// and pop-up windows; but the message is not sent to child windows.
        /// 
        /// Message sending is subject to UIPI. The thread of a process can send messages only to message queues of threads in processes of lesser or equal integrity level.</param>
        void SetMonitorState(MonitorState state, IntPtr windowHandle);

        #endregion
    }
}