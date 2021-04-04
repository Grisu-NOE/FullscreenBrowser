// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Freiwillige Feuerwehr Krems/Donau">
//       Freiwillige Feuerwehr Krems/Donau
//       Austraße 33
//       A-3500 Krems/Donau
//       Austria
//
//       Tel.:   +43 (0)2732 85522
//       Fax.:   +43 (0)2732 85522 40
//       E-mail: office@feuerwehr-krems.at
//
//       This software is furnished under a license and may be
//       used  and copied only in accordance with the terms of
//       such  license  and  with  the  inclusion of the above
//       copyright  notice.  This software or any other copies
//       thereof   may  not  be  provided  or  otherwise  made
//       available  to  any  other  person.  No  title  to and
//       ownership of the software is hereby transferred.
//
//       The information in this software is subject to change
//       without  notice  and  should  not  be  construed as a
//       commitment by Freiwillige Feuerwehr Krems/Donau.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace At.FF.Krems.Interfaces
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    /// <summary>The native methods.</summary>
    public static class NativeMethods
    {
        #region Fields

        #region WindowManagement

        /// <summary>The SWP no size.</summary>
        public const uint SwpNosize = 0x0001;

        /// <summary>The SWP no move.</summary>
        public const uint SwpNomove = 0x0002;

        /// <summary>The topmost flags.</summary>
        public const uint TopmostFlags = SwpNomove | SwpNosize;

        /// <summary>The HWN d topmost.</summary>
        public static readonly IntPtr HwndTopmost = new IntPtr(-1);

        /// <summary>The HWN d no topmost.</summary>
        public static readonly IntPtr HwndNotopmost = new IntPtr(-2);

        /// <summary>The HWN d top.</summary>
        public static readonly IntPtr HwndTop = new IntPtr(0);

        /// <summary>The HWN d bottom.</summary>
        public static readonly IntPtr HwndBottom = new IntPtr(1);

        #endregion

        #endregion

        #region Delegates

        #region Screensaver

        /// <summary>The enumerated desktop windows process.</summary>
        /// <param name="hDesktop">The desktop.</param>
        /// <param name="lParam">The parameter.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public delegate bool EnumDesktopWindowsProc(IntPtr hDesktop, IntPtr lParam);

        #endregion

        #region WindowManagement

        /// <summary>The win event delegate.</summary>
        /// <param name="hWinEventHook">The h win event hook.</param>
        /// <param name="eventType">The event type.</param>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="idObject">The id object.</param>
        /// <param name="idChild">The id child.</param>
        /// <param name="dwEventThread">The DW event thread.</param>
        /// <param name="dwmsEventTime">The DWMS event time.</param>
        public delegate void WinEventDelegate(
            IntPtr hWinEventHook,
            uint eventType,
            IntPtr hwnd,
            int idObject,
            int idChild,
            uint dwEventThread,
            uint dwmsEventTime);

        #endregion

        #endregion

        #region Enums

        #region PowerManagement

        /// <summary>The execution state.</summary>
        [Flags]
        public enum ExecutionState : uint
        {
            /// <summary>The  system required.</summary>
            SystemRequired = 0x00000001,

            /// <summary>The display required.</summary>
            DisplayRequired = 0x00000002,

            /// <summary>The user present.</summary>
            UserPresent = 0x00000004,     // legacy flag should not be used

            /// <summary>The continuous.</summary>
            Continuous = 0x80000000,
        }

        #endregion

        #region CursorManagement

        /// <summary>The mouse event flags.</summary>
        [Flags]
        public enum MouseEventFlags
        {
            /// <summary>The left down</summary>
            LeftDown = 0x2,

            /// <summary>The left up</summary>
            LeftUp = 0x4,

            /// <summary>The middle down</summary>
            MiddleDown = 0x20,

            /// <summary>The middle up</summary>
            MiddleUp = 0x40,

            /// <summary>The move</summary>
            Move = 0x1,

            /// <summary>The absolute</summary>
            Absolute = 0x8000,

            /// <summary>The right down</summary>
            RightDown = 0x8,

            /// <summary>The right up</summary>
            RightUp = 0x10
        }

        #endregion

        #endregion

        #region Methods

        #region Screensaver

        /// <summary>The get foreground window.</summary>
        /// <returns>The <see cref="IntPtr"/>.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>The system parameters info.</summary>
        /// <param name="uAction">The action.</param>
        /// <param name="uParam">The u parameter.</param>
        /// <param name="lpvParam">The LPV parameter.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SystemParametersInfo(int uAction, int uParam, ref int lpvParam, int flags);

        /// <summary>The system parameters info.</summary>
        /// <param name="uAction">The action.</param>
        /// <param name="uParam">The u parameter.</param>
        /// <param name="lpvParam">The LPV parameter.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SystemParametersInfo(int uAction, int uParam, ref bool lpvParam, int flags);

        /// <summary>The post message.</summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="wMsg">The w message.</param>
        /// <param name="wParam">The W parameter.</param>
        /// <param name="lParam">The L parameter.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        /// <summary>The open desktop.</summary>
        /// <param name="hDesktop">The h desktop.</param>
        /// <param name="flags">The flags.</param>
        /// <param name="inherit">The inherit.</param>
        /// <param name="desiredAccess">The desired access.</param>
        /// <returns>The <see cref="IntPtr"/>.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenDesktop(string hDesktop, int flags, bool inherit, uint desiredAccess);

        /// <summary>The close desktop.</summary>
        /// <param name="hDesktop">The h desktop.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseDesktop(IntPtr hDesktop);

        /// <summary>The enumerated desktop windows.</summary>
        /// <param name="hDesktop">The h desktop.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDesktopWindowsProc callback, IntPtr lParam);

        /// <summary>The is window visible.</summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        #endregion

        #region PowerManagement

        /// <summary>The create file.</summary>
        /// <param name="filename">The filename.</param>
        /// <param name="desiredAccess">The desired access.</param>
        /// <param name="shareMode">The share mode.</param>
        /// <param name="attributes">The attributes.</param>
        /// <param name="creationDisposition">The creation disposition.</param>
        /// <param name="flagsAndAttributes">The flags and attributes.</param>
        /// <param name="templateFile">The template file.</param>
        /// <returns>The <see cref="IntPtr"/>.</returns>
        [DllImport("Kernel32.dll", EntryPoint = "CreateFileW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateFile(string filename, uint desiredAccess, uint shareMode, IntPtr attributes, uint creationDisposition, uint flagsAndAttributes, IntPtr templateFile);

        /// <summary>The close handle.</summary>
        /// <param name="handle">The handle.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DllImport("Kernel32.dll", EntryPoint = "CloseHandle", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int CloseHandle(IntPtr handle);

        /// <summary>The set thread execution state.</summary>
        /// <param name="state">The state.</param>
        /// <returns>The <see cref="ExecutionState"/>.</returns>
        [DllImport("Kernel32.dll", EntryPoint = "SetThreadExecutionState", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern ExecutionState SetThreadExecutionState(PowerThreadRequirements state);

        /// <summary>The get system power status.</summary>
        /// <param name="systemPowerStatus">The system power status.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DllImport("Kernel32.dll", EntryPoint = "GetSystemPowerStatus", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetSystemPowerStatus(ref StructSystemPowerStatus systemPowerStatus);

        /// <summary>The get device power state.</summary>
        /// <param name="hDevice">The h device.</param>
        /// <param name="fOn">The f on.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("Kernel32.dll", EntryPoint = "GetDevicePowerState", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetDevicePowerState(IntPtr hDevice, out bool fOn);

        /// <summary>The is power hibernate allowed.</summary>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("PowrProf.dll", EntryPoint = "IsPwrHibernateAllowed", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool IsPwrHibernateAllowed();

        /// <summary>The is power shutdown allowed.</summary>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("PowrProf.dll", EntryPoint = "IsPwrShutdownAllowed", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool IsPwrShutdownAllowed();

        /// <summary>The is power suspend allowed.</summary>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("PowrProf.dll", EntryPoint = "IsPwrSuspendAllowed", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool IsPwrSuspendAllowed();

        /// <summary>The get current process.</summary>
        /// <returns>The <see cref="IntPtr"/>.</returns>
        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GetCurrentProcess();

        /// <summary>The open process token.</summary>
        /// <param name="processHandle">The process handle.</param>
        /// <param name="desiredAccess">The desired access.</param>
        /// <param name="tokenHandle">The token handle.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool OpenProcessToken(IntPtr processHandle, int desiredAccess, ref IntPtr tokenHandle);

        /// <summary>The lookup privilege value.</summary>
        /// <param name="systemName">The system name.</param>
        /// <param name="name">The name.</param>
        /// <param name="luidHandle">The LUID handle.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LookupPrivilegeValue(string systemName, string name, ref long luidHandle);

        /// <summary>The adjust token privileges.</summary>
        /// <param name="tokenHandle">The token handle.</param>
        /// <param name="disableAllPrivileges">The disable all privileges.</param>
        /// <param name="newState">The new state.</param>
        /// <param name="bufferLength">The buffer length.</param>
        /// <param name="previousState">The previous state.</param>
        /// <param name="returnLength">The return length.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool AdjustTokenPrivileges(IntPtr tokenHandle, bool disableAllPrivileges, ref LuidAtt newState, int bufferLength, IntPtr previousState, IntPtr returnLength);

        /// <summary>The exit windows ex.</summary>
        /// <param name="flags">The flags.</param>
        /// <param name="reason">The reason.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool ExitWindowsEx(int flags, int reason);

        /// <summary>The lock work station.</summary>
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern void LockWorkStation();

        /// <summary>The load library.</summary>
        /// <param name="lpLibFileName">The lib file name.</param>
        /// <returns>The <see cref="IntPtr"/>.</returns>
        [DllImport("kernel32.dll", EntryPoint = "LoadLibraryA", CharSet = CharSet.Ansi)]
        public static extern IntPtr LoadLibrary(string lpLibFileName);

        /// <summary>The free library.</summary>
        /// <param name="hLibModule">The h lib module.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary", CharSet = CharSet.Ansi)]
        public static extern int FreeLibrary(IntPtr hLibModule);

        /// <summary>The get process address.</summary>
        /// <param name="hModule">The h module.</param>
        /// <param name="lpProcName">The process name.</param>
        /// <returns>The <see cref="IntPtr"/>.</returns>
        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress", CharSet = CharSet.Ansi)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        /// <summary>The set suspend state.</summary>
        /// <param name="hibernate">The hibernate.</param>
        /// <param name="forceCritical">The force critical.</param>
        /// <param name="disableWakeEvent">The disable wake event.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DllImport("powrprof.dll", EntryPoint = "SetSuspendState", CharSet = CharSet.Ansi)]
        public static extern int SetSuspendState(int hibernate, int forceCritical, int disableWakeEvent);

        #endregion

        #region WindowManagement

        /// <summary>Sets the window position.</summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="hWndInsertAfter">The h WND insert after.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="uFlags">The u flags.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        /// <summary>The set win event hook.</summary>
        /// <param name="eventMin">The event min.</param>
        /// <param name="eventMax">The event max.</param>
        /// <param name="hmodWinEventProc">The HMOD win event process.</param>
        /// <param name="lpfnWinEventProc">The LPFN win event process.</param>
        /// <param name="idProcess">The id process.</param>
        /// <param name="idThread">The id thread.</param>
        /// <param name="dwFlags">The DW flags.</param>
        /// <returns>The <see cref="IntPtr"/>.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetWinEventHook(
            uint eventMin,
            uint eventMax,
            IntPtr hmodWinEventProc,
            WinEventDelegate lpfnWinEventProc,
            uint idProcess,
            uint idThread,
            uint dwFlags);

        /// <summary>The unhook win event.</summary>
        /// <param name="hWinEventHook">The h win event hook.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DllImport("user32.dll")]
        public static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        #endregion

        #region CursorManagement

        /// <summary>Shows the cursor.</summary>
        /// <param name="bShow">If set to <c>true</c> [b show].</param>
        /// <returns>The <see cref="int" />.</returns>
        [DllImport("user32.dll")]
        public static extern int ShowCursor(bool bShow);

        /// <summary>Mouse_events the specified dw flags.</summary>
        /// <param name="dwFlags">The dw flags.</param>
        /// <param name="dx">The dx.</param>
        /// <param name="dy">The dy.</param>
        /// <param name="dwData">The dw data.</param>
        /// <param name="dwExtraInfo">The dw extra information.</param>
        // ReSharper disable once InconsistentNaming
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        #endregion

        #endregion

        #region Structs

        #region PowerManagement

        /// <summary>The struct system power status.</summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct StructSystemPowerStatus
        {
            /// <summary>The ac line status.</summary>
            public readonly byte ACLineStatus;

            /// <summary>The battery flag.</summary>
            public readonly byte BatteryFlag;

            /// <summary>The battery life percent.</summary>
            public readonly byte BatteryLifePercent;

            /// <summary>The reserved 1.</summary>
            public readonly byte Reserved1;

            /// <summary>The battery life time.</summary>
            public readonly int BatteryLifeTime;

            /// <summary>The battery full life time.</summary>
            public readonly int BatteryFullLifeTime;
        }

        /// <summary>The LUID attribute.</summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct LuidAtt
        {
            /// <summary>The count.</summary>
            public int Count;

            /// <summary>The LUID.</summary>
            public long Luid;

            /// <summary>The attribute.</summary>
            public int Attr;
        }

        #endregion

        #endregion
    }
}