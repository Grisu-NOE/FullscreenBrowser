// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Area.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>The area.</summary>
    public class Area
    {
        /// <summary>The points</summary>
        private List<Point> points;

        /// <summary>Gets or sets the limit of the points.</summary>
        /// <value>The limit.</value>
        public int PointLimit { get; set; } = 10;

        /// <summary>Gets or sets the points.</summary>
        /// <value>The points.</value>
        public List<Point> Points
        {
            get { return this.points?.OrderBy(x => x.Dis).ToList(); }
            set { this.points = value; }
        }

        /// <summary>Gets the limited points.</summary>
        /// <value>The limited points.</value>
        public List<Point> LimitedPoints
        {
            get
            {
                var result = this.Points;
                return result?.Take(this.PointLimit).ToList();
            }
        }

        /// <summary>Gets or sets the LNG.</summary>
        /// <value>The LNG.</value>
        public double Lng { get; set; }

        /// <summary>Gets or sets the LAT.</summary>
        /// <value>The LAT.</value>
        public double Lat { get; set; }
    }
}