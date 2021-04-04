// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Disposition.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.FullscreenBrowser.Print
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>The disposition.</summary>
    public class Disposition
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is eigenalarmiert.</summary>
        /// <value>
        /// <c>true</c> if this instance is eigenalarmiert; otherwise, <c>false</c>.
        /// </value>
        public bool IsEigenalarmiert { get; set; }

        /// <summary>Gets or sets the dispo time.</summary>
        /// <value>The dispo time.</value>
        public DateTime DispoTime { get; set; }

        /// <summary>Gets or sets the aus time.</summary>
        /// <value>The aus time.</value>
        public DateTime AusTime { get; set; }

        /// <summary>Gets or sets the ein time.</summary>
        /// <value>The ein time.</value>
        public DateTime EinTime { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is background.</summary>
        /// <value>
        /// <c>true</c> if this instance is background; otherwise, <c>false</c>.
        /// </value>
        public bool IsBackground { get; set; }
    }
}