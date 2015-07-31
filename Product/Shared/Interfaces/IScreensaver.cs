// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IScreensaver.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    /// <summary>The Screensaver interface.</summary>
    public interface IScreensaver
    {
        /// <summary>Get screensaver active.</summary>
        /// <returns>Returns <see cref="bool">true</see> if the screensaver is active (enabled, but not necessarily running).</returns>
        bool GetScreensaverActive();

        /// <summary>Set screensaver active.</summary>
        /// <param name="active">Pass in TRUE to activate or FALSE to deactivate the screensaver.</param>
        void SetScreensaverActive(bool active);

        /// <summary>Get screensaver timeout.</summary>
        /// <returns>Returns the screensaver timeout setting, in seconds.</returns>
        int GetScreensaverTimeout();

        /// <summary>Set screensaver timeout.</summary>
        /// <param name="value">Pass in the number of seconds to set the screensaver timeout value.</param>
        void SetScreensaverTimeout(int value);

        /// <summary>Get screensaver running.</summary>
        /// <returns><see cref="bool">True</see> if the screensaver is actually running.</returns>
        bool GetScreensaverRunning();

        /// <summary>Kill the screensaver.</summary>
        /// From Microsoft's Knowledge Base article #140723:
        /// http://support.microsoft.com/kb/140723
        /// "How to force a screen saver to close once started
        /// in Windows NT, Windows 2000, and Windows Server 2003"
        void KillScreensaver();
    }
}