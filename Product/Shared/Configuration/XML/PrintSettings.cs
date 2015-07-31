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

    /// <summary>The map types for google maps API.</summary>
    public enum MapType
    {
        /// <summary>The roadmap is default</summary>
        Roadmap,

        /// <summary>The satellite</summary>
        Satellite,

        /// <summary>The terrain</summary>
        Terrain,

        /// <summary>The hybrid</summary>
        Hybrid
    }

    /// <summary>The print settings.</summary>
    [GeneratedCode("xsd", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlType(AnonymousType = true)]
    [Serializable]
    public class PrintSettings
    {
        /// <summary>The print URL</summary>
        private string printUrl = "https://infoscreen.florian10.info/ows/infoscreen/einsatz.ashx";

        /// <summary>The print port</summary>
        private int printPort = 121;

        /// <summary>The print enabled</summary>
        private bool printEnabled = true;

        /// <summary>The print on emergency</summary>
        private bool printOnEmergency = true;

        /// <summary>The number of pages per click</summary>
        private int numberOfPagesPerClick = 1;

        /// <summary>The number of pages on emergency</summary>
        private int numberOfPagesOnEmergency = 3;

        /// <summary>The map type</summary>
        private MapType mapType = MapType.Roadmap;

        /// <summary>Gets or sets the print URL.</summary>
        public string PrintUrl
        {
            get
            {
                return this.printUrl;
            }

            set
            {
                this.printUrl = value;
            }
        }

        /// <summary>Gets or sets the print port.</summary>
        public int PrintPort
        {
            get
            {
                return this.printPort;
            }

            set
            {
                this.printPort = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether print enabled.</summary>
        public bool PrintEnabled
        {
            get
            {
                return this.printEnabled;
            }

            set
            {
                this.printEnabled = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether [print on emergency].</summary>
        /// <value><c>true</c> if [print on emergency]; otherwise, <c>false</c>.</value>
        public bool PrintOnEmergency
        {
            get
            {
                return this.printOnEmergency;
            }

            set
            {
                this.printOnEmergency = value;
            }
        }

        /// <summary>Gets or sets the number of pages per click.</summary>
        /// <value>The number of pages per click.</value>
        public int NumberOfPagesPerClick
        {
            get
            {
                return this.numberOfPagesPerClick;
            }

            set
            {
                this.numberOfPagesPerClick = value;
            }
        }

        /// <summary>Gets or sets the number of pages on emergency.</summary>
        /// <value>The number of pages on emergency.</value>
        public int NumberOfPagesOnEmergency
        {
            get
            {
                return this.numberOfPagesOnEmergency;
            }

            set
            {
                this.numberOfPagesOnEmergency = value;
            }
        }

        /// <summary>Gets or sets the type of the map.</summary>
        /// <value>The type of the map.</value>
        public MapType MapType
        {
            get
            {
                return this.mapType;
            }
            
            set
            {
                this.mapType = value;
            }
        }
    }
}