// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MarkerStyle.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using At.FF.Krems.Configuration.Google.StaticMaps.Enums;

    public class MarkerStyle
    {
        /// <summary>
        /// (optional) specifies the size of marker from the set {tiny, mid, small}. If no size parameter is set, the marker will appear in its default (normal) size.
        /// </summary>
        public MarkerSize Size { get; set; }

        /// <summary>
        /// optional) specifies a 24-bit color (example: color=0xFFFFCC) or a predefined color from the set {black, brown, green, purple, yellow, blue, gray, orange, red, white}.
        /// Note that transparencies (specified using 32-bit hex color values) are not supported in markers, though they are supported for paths.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// (optional) specifies a single uppercase alphanumeric character from the set {A-Z, 0-9}. 
        /// (The requirement for uppercase characters is new to this version of the API.) 
        /// Note that default and mid sized markers are the only markers capable of displaying an alphanumeric-character parameter. 
        /// tiny and small markers are not capable of displaying an alphanumeric-character.
        /// </summary>
        public string Label { get; set; }
    }
}
