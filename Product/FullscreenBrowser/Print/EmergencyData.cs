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
namespace At.FF.Krems.FullscreenBrowser.Print
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Net;
    using System.Windows.Media.Imaging;

    /// <summary>The emergency data.</summary>
    public class EmergencyData
    {
        /// <summary>The map URL</summary>
        private string mapUrl;

        /// <summary>Gets or sets the einsatz identifier.</summary>
        /// <value>The einsatz identifier.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        // ReSharper disable once InconsistentNaming
        public string EinsatzID { get; set; }

        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>Gets or sets the alarmstufe.</summary>
        /// <value>The alarmstufe.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string Alarmstufe { get; set; }

        /// <summary>Gets or sets the meldebild.</summary>
        /// <value>The meldebild.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string Meldebild { get; set; }

        /// <summary>Gets or sets the nummer1.</summary>
        /// <value>The nummer1.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string Nummer1 { get; set; }

        /// <summary>Gets or sets the nummer2.</summary>
        /// <value>The nummer2.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string Nummer2 { get; set; }

        /// <summary>Gets or sets the nummer3.</summary>
        /// <value>The nummer3.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string Nummer3 { get; set; }

        /// <summary>Gets or sets the objekt.</summary>
        /// <value>The objekt.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string Objekt { get; set; }

        /// <summary>Gets or sets the PLZ.</summary>
        /// <value>The PLZ.</value>
        public string Plz { get; set; }

        /// <summary>Gets or sets the strasse.</summary>
        /// <value>The strasse.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string Strasse { get; set; }

        /// <summary>Gets or sets the ort.</summary>
        /// <value>The ort.</value>
        public string Ort { get; set; }

        /// <summary>Gets or sets the abschnitt.</summary>
        /// <value>The abschnitt.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string Abschnitt { get; set; }

        /// <summary>Gets or sets the bemerkung.</summary>
        /// <value>The bemerkung.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string Bemerkung { get; set; }

        /// <summary>Gets or sets the einsatz erzeugt.</summary>
        /// <value>The einsatz erzeugt.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public DateTime EinsatzErzeugt { get; set; }

        /// <summary>Gets or sets the melder.</summary>
        /// <value>The melder.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string Melder { get; set; }

        /// <summary>Gets or sets the melder telefon.</summary>
        /// <value>The melder telefon.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string MelderTelefon { get; set; }

        /// <summary>Gets or sets the einsatz nummer.</summary>
        /// <value>The einsatz nummer.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public int EinsatzNummer { get; set; }

        /// <summary>Gets or sets the dispositionen.</summary>
        /// <value>The dispositionen.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public List<Disposition> Dispositionen { get; set; }

        /// <summary>Gets or sets the map URL.</summary>
        /// <value>The map URL.</value>
        public string MapUrl
        {
            get
            {
                return this.mapUrl;
            }

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

        /// <summary>Gets the map image.</summary>
        /// <value>The map image.</value>
        public BitmapImage MapImage { get; private set; }

        /// <summary>Gets or sets the area.</summary>
        /// <value>The area.</value>
        public Area Area { get; set; }
    }
}