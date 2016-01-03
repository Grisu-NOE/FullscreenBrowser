// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrintSettings.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.Configuration.XML
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using At.FF.Krems.Configuration.Google.StaticMaps.Enums;

    /// <summary>The print settings.</summary>
    [GeneratedCode("xsd", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlType(AnonymousType = true)]
    [Serializable]
    public class PrintSettings
    {
        /// <summary>Gets or sets the print URL.</summary>
        public string PrintUrl { get; set; } = "https://infoscreen.florian10.info/ows/infoscreen/einsatz.ashx";

        /// <summary>Gets or sets the print port.</summary>
        public int PrintPort { get; set; } = 121;

        /// <summary>Gets or sets a value indicating whether print enabled.</summary>
        public bool PrintEnabled { get; set; } = true;

        /// <summary>Gets or sets a value indicating whether [print on emergency].</summary>
        /// <value><c>true</c> if [print on emergency]; otherwise, <c>false</c>.</value>
        public bool PrintOnEmergency { get; set; } = true;

        /// <summary>Gets or sets the number of pages per click.</summary>
        /// <value>The number of pages per click.</value>
        public int NumberOfPagesPerClick { get; set; } = 1;

        /// <summary>Gets or sets the number of pages on emergency.</summary>
        /// <value>The number of pages on emergency.</value>
        public int NumberOfPagesOnEmergency { get; set; } = 3;

        /// <summary>Gets or sets the type of the map.</summary>
        /// <value>The type of the map.</value>
        public MapType MapType { get; set; } = MapType.Roadmap;

        /// <summary>Gets or sets the maximum number of hydrants.</summary>
        /// <value>The maximum number of hydrants.</value>
        public int MaxHydrants { get; set; } = 10;
    }
}
