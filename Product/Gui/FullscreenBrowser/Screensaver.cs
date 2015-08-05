// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScreenSaver.cs" company="Freiwillige Feuerwehr Krems/Donau">
//   Freiwillige Feuerwehr Krems/Donau
//   //     Austraße 33
//   //     A-3500 Krems/Donau
//   //     Austria
//   //     Tel.:   +43 (0)2732 85522
//   //     Fax.:   +43 (0)2732 85522 40
//   //     E-mail: office@feuerwehr-krems.at
//   //     This software is furnished under a license and may be
//   //     used  and copied only in accordance with the terms of
//   //     such  license  and  with  the  inclusion of the above
//   //     copyright  notice.  This software or any other copies
//   //     thereof   may  not  be  provided  or  otherwise  made
//   //     available  to  any  other  person.  No  title  to and
//   //     ownership of the software is hereby transferred.
//   //     The information in this software is subject to change
//   //     without  notice  and  should  not  be  construed as a
//   //     commitment by Freiwillige Feuerwehr Krems/Donau.
// </copyright>
// <summary>
//   The screen saver.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace At.FF.Krems.FullscreenBrowser
{
    using System;

    using Interfaces;

    /// <summary>The screen saver.</summary>
    public class Screensaver : IScreensaver
    {
        #region Constants

        /// <summary>The SPI get screensaver active.</summary>
        private const int SpiGetScreensaverActive = 16;

        /// <summary>The SPI set screensaver active.</summary>
        private const int SpiSetScreensaverActive = 17;

        /// <summary>The SPI get screensaver timeout.</summary>
        private const int SpiGetScreensaverTimeout = 14;

        /// <summary>The SPI set screensaver timeout.</summary>
        private const int SpiSetScreensaverTimeout = 15;

        /// <summary>The SPI get screensaver running.</summary>
        private const int SpiGetScreensaverRunning = 114;

        /// <summary>The SPIF send WIN INI change.</summary>
        private const int SpifSendWininiChange = 2;

        /// <summary>The desktop write objects.</summary>
        private const uint DesktopWriteObjects = 0x0080;

        /// <summary>The desktop read objects.</summary>
        private const uint DesktopReadObjects = 0x0001;

        /// <summary>The WM close.</summary>
        private const int WmClose = 16;

        #endregion

        #region Methods

        /// <summary>Get screensaver active.</summary>
        /// <returns>Returns <see cref="bool">true</see> if the screensaver is active (enabled, but not necessarily running).</returns>
        public bool GetScreensaverActive()
        {
            var isActive = false;
            NativeMethods.SystemParametersInfo(SpiGetScreensaverActive, 0, ref isActive, 0);
            return isActive;
        }

        /// <summary>Set screensaver active.</summary>
        /// <param name="active">Pass in TRUE to activate or FALSE to deactivate the screensaver.</param>
        public void SetScreensaverActive(bool active)
        {
            var nullVar = 0;
            NativeMethods.SystemParametersInfo(SpiSetScreensaverActive, active ? 1 : 0, ref nullVar, SpifSendWininiChange);
        }

        /// <summary>Get screensaver timeout.</summary>
        /// <returns>Returns the screensaver timeout setting, in seconds.</returns>
        public int GetScreensaverTimeout()
        {
            var value = 0;
            NativeMethods.SystemParametersInfo(SpiGetScreensaverTimeout, 0, ref value, 0);
            return value;
        }

        /// <summary>Set screensaver timeout.</summary>
        /// <param name="value">Pass in the number of seconds to set the screensaver timeout value.</param>
        public void SetScreensaverTimeout(int value)
        {
            var nullVar = 0;
            NativeMethods.SystemParametersInfo(SpiSetScreensaverTimeout, value, ref nullVar, SpifSendWininiChange);
        }

        /// <summary>Get screensaver running.</summary>
        /// <returns><see cref="bool">True</see> if the screensaver is actually running.</returns>
        public bool GetScreensaverRunning()
        {
            var isRunning = false;
            NativeMethods.SystemParametersInfo(SpiGetScreensaverRunning, 0, ref isRunning, 0);
            return isRunning;
        }

        /// <summary>Kill the screensaver.</summary>
        /// From Microsoft's Knowledge Base article #140723:
        /// http://support.microsoft.com/kb/140723
        /// "How to force a screen saver to close once started
        /// in Windows NT, Windows 2000, and Windows Server 2003"
        public void KillScreensaver()
        {
            var desktop = NativeMethods.OpenDesktop("Screen-saver", 0, false, DesktopReadObjects | DesktopWriteObjects);
            if (desktop != IntPtr.Zero)
            {
                NativeMethods.EnumDesktopWindows(desktop, this.KillScreensaverFunc, IntPtr.Zero);
                NativeMethods.CloseDesktop(desktop);
            }
            else
            {
                NativeMethods.PostMessage(NativeMethods.GetForegroundWindow(), WmClose, 0, 0);
            }
        }

        /// <summary>Kill screensaver.</summary>
        /// <param name="wnd">The window.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool KillScreensaverFunc(IntPtr wnd, IntPtr param)
        {
            if (NativeMethods.IsWindowVisible(wnd))
            {
                NativeMethods.PostMessage(wnd, WmClose, 0, 0);
            }

            return true;
        }

        #endregion
    }
}