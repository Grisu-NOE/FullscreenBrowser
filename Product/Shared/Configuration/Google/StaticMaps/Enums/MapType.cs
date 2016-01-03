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
    public enum MapType
    {
        /// <summary>
        ///  (default) specifies a standard roadmap image, as is normally shown on the Google Maps website. If no maptype value is specified, the Static Maps API serves roadmap tiles by default.
        /// </summary>
        Roadmap,
        /// <summary>
        /// specifies a satellite image.
        /// </summary>
        Satellite,
        /// <summary>
        ///  specifies a physical relief map image, showing terrain and vegetation.
        /// </summary>
        Terrain,
        /// <summary>
        /// specifies a hybrid of the satellite and roadmap image, showing a transparent layer of major streets and place names on the satellite image.
        /// </summary>
        Hybrid
    }
}
