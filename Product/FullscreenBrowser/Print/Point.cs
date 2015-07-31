// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Point.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.FullscreenBrowser.Print
{
    using System.Linq;

    /// <summary>The point.</summary>
    public class Point
    {
        /// <summary>Gets or sets the LNG.</summary>
        /// <value>The LNG.</value>
        public double Lng { get; set; }

        /// <summary>Gets or sets the LAT.</summary>
        /// <value>The LAT.</value>
        public double Lat { get; set; }

        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        public string Typ { get; set; }

        /// <summary>Gets or sets the text.</summary>
        /// <value>The text.</value>
        public string Txt { get; set; }

        /// <summary>Gets or sets the UID.</summary>
        /// <value>The UID.</value>
        public string Uid { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        public string Usr { get; set; }

        /// <summary>Gets or sets the distance in meters.</summary>
        /// <value>The distance in meters.</value>
        public double Dis { get; set; }

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        public string Adr { get; set; }

        /// <summary>Gets the short address.</summary>
        /// <value>The short address.</value>
        public string ShortAddress
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.Adr) ? string.Empty : this.Adr.Split(',').FirstOrDefault();
            }
        }

        /// <summary>Gets or sets the EID.</summary>
        /// <value>The EID.</value>
        public string Eid { get; set; }
    }
}