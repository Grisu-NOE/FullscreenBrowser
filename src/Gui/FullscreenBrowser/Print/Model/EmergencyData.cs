// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmergencyData.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.FullscreenBrowser.Print.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Windows.Media.Imaging;

    /// <summary>The emergency data.</summary>
    public class EmergencyData
    {
        private string mapUrl;

        public string EinsatzID { get; set; }
        public int Status { get; set; }
        public string Alarmstufe { get; set; }
        public string Meldebild { get; set; }

        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public string Accuracy { get; set; }

        public string Nummer1 { get; set; }
        public string Nummer2 { get; set; }
        public string Nummer3 { get; set; }
        public string Plz { get; set; }
        public string Strasse { get; set; }
        public string Ort { get; set; }
        public string Abschnitt { get; set; }
        public string Objekt { get; set; }
        public string ObjektId { get; set; }
        public string Bemerkung { get; set; }
        public DateTime? EinsatzErzeugt { get; set; }
        public string Melder { get; set; }
        public string MelderTelefon { get; set; }
        public long EinsatzNummer { get; set; }
        public List<Disposition> Dispositionen { get; set; } = new List<Disposition>();
        public List<ResponseRegulation> Ausrueckordnung { get; set; } = new List<ResponseRegulation>();
        public RSVP Rsvp { get; set; }

        public string MapUrl
        {
            get => this.mapUrl;

            set
            {
                if (value == null || this.mapUrl == value)
                {
                    return;
                }

                this.mapUrl = value;
                using (var webClient = new WebClient())
                {
                    var buffer = webClient.DownloadData(value);
                    using (var stream = new MemoryStream(buffer))
                    {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = stream;
                        image.EndInit();
                        this.MapImage = image;
                    }
                }
            }
        }

        public BitmapImage MapImage { get; private set; }

        public Area Area { get; set; }
    }
}