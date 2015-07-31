// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScreenRefresher.cs" company="Freiwillige Feuerwehr Krems/Donau">
//     Freiwillige Feuerwehr Krems/Donau
//     Austraße 33
//     A-3500 Krems/Donau
//     Austria
// 
//     Tel.:   +43 (0)2732 85522
//     Fax.:   +43 (0)2732 85522 40
//     E-mail: office@feuerwehr-krems.at
// 
//     This software is furnished under a license and may be
//     used  and copied only in accordance with the terms of
//     such  license  and  with  the  inclusion of the above
//     copyright  notice.  This software or any other copies
//     thereof   may  not  be  provided  or  otherwise  made
//     available  to  any  other  person.  No  title  to and
//     ownership of the software is hereby transferred.
// 
//     The information in this software is subject to change
//     without  notice  and  should  not  be  construed as a
//     commitment by Freiwillige Feuerwehr Krems/Donau.
// 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace At.FF.Krems.Utils.XML
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    /// <summary>The screen refresher.</summary>
    [GeneratedCode("xsd", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlType(AnonymousType = true)]
    [Serializable]
    public class ScreenRefresher
    {
        /// <summary>The duration field.</summary>
        private int durationField;

        /// <summary>The interval field.</summary>
        private int intervalField;

        /// <summary>The height field.</summary>
        private int heightField;

        /// <summary>Gets or sets a value indicating whether enabled.</summary>
        public bool Enabled { get; set; }

        /// <summary>Gets or sets a value indicating whether run at startup.</summary>
        public bool RunAtStartup { get; set; }

        /// <summary>Gets or sets the duration.</summary>
        public int Duration
        {
            get
            {
                return this.durationField <= 0 ? 10 : this.durationField;
            }

            set
            {
                this.durationField = value;
            }
        }

        /// <summary>Gets or sets the interval.</summary>
        public int Interval
        {
            get
            {
                return this.intervalField <= 0 ? 60 : this.intervalField;
            }

            set
            {
                this.intervalField = value;
            }
        }

        /// <summary>Gets or sets the height.</summary>
        public int Height
        {
            get
            {
                return this.heightField <= 0 ? 50 : this.heightField;
            }

            set
            {
                this.heightField = value;
            }
        }
    }
}