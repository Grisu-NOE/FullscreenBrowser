﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapStyle.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using System.Drawing;

    using At.FF.Krems.Configuration.Google.StaticMaps.Enums;

    /// <summary>
    /// Styled maps allow you to customize the presentation of the standard Google map styles, changing the visual display of such elements as roads, parks, and built-up areas to reflect a different style than that used in the default map type.
    /// These components are known as features and a styled map allows you to select these features and apply visual styles to their display (including hiding them entirely).
    /// With these changes, the map can be made to emphasize particular components or complement content within the surrounding page.
    /// A customized "styled" map consists of one or more specified styles, each indicated through a style parameter within the Static Map request URL.
    /// Additional styles are specified by passing additional style parameters.
    /// A style consists of a selection(s) and a set of rules to apply to that selection.
    /// The rules indicate what visual modification to make to the selection.
    /// Mix of https://developers.google.com/maps/documentation/javascript/reference#MapTypeStyler and https://developers.google.com/maps/documentation/javascript/reference#MapTypeStyle
    /// </summary>
    public class MapStyle
    {
        /// <summary>Gets or sets the color of the feature. Valid values: An RGB hex string, i.e. '#ff0000'.</summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the gamma by raising the lightness to the given power. Valid values: Floating point numbers, [0.01, 10], with 1.0 representing no change.
        /// indicates the amount of gamma correction to apply to the element. Gammas modify the lightness of hues in a non-linear fashion, while not affecting white or black values. Gammas are typically used to modify the contrast of multiple elements. For example, you could modify the gamma to increase or decrease the contrast between the edges and interiors of elements. Low gamma values (less than 1) increase contrast, while high values (> 1) decrease contrast.
        /// </summary>
        public float? Gamma { get; set; }

        /// <summary>
        /// Gets or sets the hue of the feature to match the hue of the color supplied. Note that the saturation and lightness of the feature is conserved, which means that the feature will not match the color supplied exactly. Valid values: An RGB hex string, i.e. '#ff0000'.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public Color HUE { get; set; }

        /// <summary>Gets or sets a value indicating whether of <c>true</c> will invert the lightness of the feature while preserving the <see cref="HUE"/> and <see cref="Saturation"/>.</summary>
        public bool InvertLightness { get; set; }

        /// <summary>
        /// Gets or sets a floating point value between -100 and 100 indicates the percentage change in brightness of the element. Negative values increase darkness (where -100 specifies black) while positive values increase brightness (where +100 specifies white).
        /// </summary>
        public float? Lightness { get; set; }

        /// <summary>
        /// Gets or sets a floating point value between -100 and 100. Indicates the percentage change in intensity of the basic color to apply to the element.
        /// </summary>
        public float? Saturation { get; set; }

        /// <summary>
        /// Gets or sets the indicator whether and how the element appears on the map. visibility:simplified indicates that the map should simplify the presentation of those elements as it sees fit. (A simplified road structure may show fewer roads, for example.)
        /// </summary>
        public MapVisibility Visibility { get; set; }

        /// <summary>Gets or sets the weight of the feature, in pixels. Valid values: Integers greater than or equal to zero.</summary>
        public int Weight { get; set; }

        /// <summary>
        /// Gets or sets (optional) what features to select for this style modification. (See Map Features below.) If no feature argument is passed, all features will be selected.
        /// </summary>
        public MapFeature MapFeature { get; set; }

        /// <summary>
        /// Gets or sets (optional) what sub-set of the selected features to select. (See Map Elements below.) If no element argument is passed, all elements of the given feature will be selected.
        /// </summary>
        public MapElement MapElement { get; set; }
    }
}
