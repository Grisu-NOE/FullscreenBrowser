// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapType.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.Configuration.Google.StaticMaps.Enums
{
    /// <summary>The map type</summary>
    public enum MapType
    {
        /// <summary>
        ///  (default) This map type displays a normal street map. If no map type value is specified, the Static Maps API serves roadmap tiles by default.
        /// </summary>
        Roadmap,

        /// <summary>This map type displays satellite images.</summary>
        Satellite,

        /// <summary>This map type displays maps with physical features such as terrain and vegetation.</summary>
        Terrain,
        
        /// <summary>This map type displays a transparent layer of major streets on satellite images.</summary>
        Hybrid
    }
}
