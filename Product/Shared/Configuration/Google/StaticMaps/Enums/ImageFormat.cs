// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageFormat.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    /// <summary>
    /// Images may be returned in several common web graphics formats: Gif, JPEG and PNG. The format parameter takes one of the following values:
    /// png8 or png (default) specifies the 8-bit PNG format.
    /// png32 specifies the 32-bit PNG format.
    /// gif specifies the Gif format.
    /// jpg specifies the JPEG compression format.
    /// jpg-baseline specifies a non-progressive JPEG compression format.
    /// jpg and jpg-baseline typically provide the smallest image size, though they do so through "lossy" compression which may degrade the image. gif, png8 and png32 provide lossless compression.
    /// Most JPEG images are progressive, meaning that they load a coarser image earlier and refine the image resolution as more data arrives. This allows images to be loaded quickly in webpages and is the most widespread use of JPEG currently. However, some uses of JPEG (especially printing) require non-progressive (baseline) images. In such cases, you may want to use the jpg-baseline format, which is non-progressive.
    /// </summary>
    public enum ImageFormat
    {
        /// <summary>
        /// (default) specifies the 8-bit PNG format.
        /// </summary>
        Png8,

        /// <summary>
        /// specifies the 32-bit PNG format.
        /// </summary>
        Png32,
        
        /// <summary>
        /// specifies the Gif format.
        /// </summary>
        Gif,
        
        /// <summary>
        /// specifies the JPEG compression format.
        /// </summary>
        Jpg,
        
        /// <summary>
        /// specifies a non-progressive JPEG compression format.
        /// </summary>
        JpgBaseline
    }
}
