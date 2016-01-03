// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapElement.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    /// Additionally, some features on a map typically consist of different elements. A road, for example, consists of not only the graphical line (geometry) on the map, but the text denoting its name (labels) attached the map. Elements within features are selected by declaring an element argument
    /// https://developers.google.com/maps/documentation/javascript/reference#MapTypeStyleElementType
    /// </summary>
    public enum MapElement
    {
        /// <summary>Apply the rule to all elements of the specified feature.</summary>
        All,

        /// <summary>Apply the rule to the feature's geometry.</summary>
        Geometry,

        /// <summary>Apply the rule to the fill of the feature's geometry.</summary>
        GeometryFill,

        /// <summary>Apply the rule to the stroke of the feature's geometry.</summary>
        GeometryStroke,

        /// <summary>Apply the rule to the feature's labels.</summary>
        Labels,

        /// <summary>Apply the rule to icons within the feature's labels.</summary>
        LabelsIcon,

        /// <summary>Apply the rule to the text in the feature's label.</summary>
        LabelsText,

        /// <summary>Apply the rule to the fill of the text in the feature's labels.</summary>
        LabelsTextFill,

        /// <summary>Apply the rule to the stroke of the text in the feature's labels.</summary>
        LabelsTextStroke
    }
}
