// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageSize.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    /// <summary>
    /// Images may be retrieved in sizes up to 640 by 640 pixels.
    /// The size parameter takes a string with two values separated by the x character.
    /// 640x640 is the largest image size allowed.
    /// Note that the center parameter, combined with the size parameter implicitly defines the coverage area of the map image.
    /// </summary>
    public struct ImageSize
    {
        /// <summary>The width</summary>
        public readonly int Width;

        /// <summary>The height</summary>
        public readonly int Height;

        /// <summary>Initializes a new instance of the <see cref="ImageSize"/> struct.</summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public ImageSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
