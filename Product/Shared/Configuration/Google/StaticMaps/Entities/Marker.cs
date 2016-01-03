// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Marker.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.Configuration.Google.StaticMaps.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// The markers parameter defines a set of one or more markers at a set of locations. Each marker defined within a single markers declaration must exhibit the same visual style; if you wish to display markers with different styles, you will need to supply multiple markers parameters with separate style information.
    /// </summary>
    public class Marker
    {
        /// <summary>
        /// Marker's style
        /// </summary>
        public MarkerStyle Style { get; set; }

        public IList<ILocationString> Locations { get; set; }
    }
}
