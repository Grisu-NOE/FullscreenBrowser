// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWindowManager.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using System.Collections.Generic;
    using System.Net;

    /// <summary>The WindowManager interface.</summary>
    public interface IWindowManager : IStartupTask
    {
        /// <summary>Activates the WASTL display.</summary>
        void ActivateWastlDisplay();

        /// <summary>Gets the document cookies.</summary>
        /// <param name="hostFilter">The host filter.</param>
        /// <returns>The <see cref="string"/>.</returns>
        List<Cookie> GetDocumentCookies(string hostFilter = null);

        /// <summary>Clears the browser cache.</summary>
        void ClearBrowserCache();
    }
}