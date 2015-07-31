// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGeckoBrowser.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using System.Collections.Generic;

    using At.FF.Krems.Configuration.XML;

    /// <summary>The GeckoBrowser interface.</summary>
    public interface IGeckoBrowser : IStartupTask
    {
        /// <summary>Occurs when [visible changed].</summary>
        event EventHandler VisibleChanged;

        /// <summary>Gets a value indicating whether this <see cref="IGeckoBrowser"/> is visible.</summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        bool Visible { get; }

        /// <summary>Gets the document cookies.</summary>
        /// <value>The document cookies.</value>
        List<System.Net.Cookie> DocumentCookies { get; }

        /// <summary>Setups the specified setting.</summary>
        /// <param name="setting">The setting.</param>
        void Setup(Window setting);

        /// <summary>Starts or resets the browser.</summary>
        /// <param name="ignoreAutoStart">if set to <c>true</c> ignores flag of automatic start.</param>
        /// <param name="disableReload">if set to <c>true</c> disable reload.</param>
        /// <param name="onlyIfIsEmergencyWindow">if set to <c>true</c> [only if is emergency window].</param>
        /// <param name="ignoreVisibility">if set to <c>true</c> [ignore visibility].</param>
        void StartOrResetBrowser(bool ignoreAutoStart = false, bool disableReload = false, bool onlyIfIsEmergencyWindow = false, bool ignoreVisibility = false);

        /// <summary>Closes the browser.</summary>
        /// <param name="onlyIfIsAlternativeWindow">if set to <c>true</c> [only if is alternative window].</param>
        void CloseBrowser(bool onlyIfIsAlternativeWindow = false);

        /// <summary>The zoom in.</summary>
        void ZoomIn();

        /// <summary>The zoom out.</summary>
        void ZoomOut();

        /// <summary>Resets the zoom.</summary>
        void ResetZoom();
    }
}