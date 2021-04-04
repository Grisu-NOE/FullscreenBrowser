// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PathStyle.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using System.Runtime.InteropServices;

    /// <summary>The path style</summary>
    public class PathStyle
    {
        /// <summary>
        /// Gets or sets (optional) the thickness of the path in pixels. If no weight parameter is set, the path will appear in its default thickness (5 pixels).
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Gets or sets (optional) a color either as a 24-bit (example: color=0xFFFFCC) or 32-bit hexadecimal value (example: color=0xFFFFCCFF), or from the set {black, brown, green, purple, yellow, blue, gray, orange, red, white}.
        /// When a 32-bit hex value is specified, the last two characters specify the 8-bit alpha transparency value. This value varies between 00 (completely transparent) and FF (completely opaque). Note that transparencies are supported in paths, though they are not supported for markers.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets (optional) that both the path marks off a polygonal area and specifies the fill color to use as an overlay within that area. The set of locations following need not be a "closed" loop; the Static Map server will automatically join the first and last points. Note, however, that any stroke on the exterior of the filled area will not be closed unless you specifically provide the same beginning and end location.
        /// </summary>
        public string FillColor { get; set; }
    }
}
