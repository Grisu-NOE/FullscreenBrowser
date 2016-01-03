// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrintController.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Printing;
    using System.Reflection;
    using System.Windows.Documents;
    using System.Windows.Markup;

    using At.FF.Krems.Interfaces;
    using At.FF.Krems.Utils.Bootstrapper;
    using At.FF.Krems.Utils.Extensions;

    using log4net;

    using Newtonsoft.Json;

    /// <summary>The print controller.</summary>
    public class PrintController : IPrintController
    {
        #region Fields

        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The automatic printed missions lock object</summary>
        private readonly object printedMissionsLockObject = new object();

        /// <summary>The automatic printed missions</summary>
        private readonly List<EmergencyData> printedMissions = new List<EmergencyData>();

        /// <summary>The queue manager.</summary>
        private readonly QueueManager queueManager = new QueueManager();

        /// <summary>The configuration</summary>
        private Configuration.XML.PrintSettings config;

        /// <summary>The initialize called</summary>
        private bool initializeCalled;

        #endregion

        #region Startup Tasks

        /// <summary>Initializes this instance.</summary>
        public void Init()
        {
            this.Initialize();
        }

        /// <summary>Resets this instance.</summary>
        public void Reset()
        {
            this.Initialize(true);
        }

        #endregion

        #region Methods

        #region IPrintController

        /// <summary>Print rescue data.</summary>
        /// <param name="onFirstEmergency">if set to <c>true</c> only print on first emergency.</param>
        public void Print(bool onFirstEmergency = false)
        {
            if (!this.config.PrintEnabled)
            {
                return;
            }

            var automaticPrint = false;
            if (onFirstEmergency)
            {
                if (!this.config.PrintOnEmergency)
                {
                    return;
                }

                automaticPrint = true;
            }

            this.queueManager.ExecuteAction(() => this.InternalPrint(automaticPrint));
        }

        /// <summary>Clear the printed missions.</summary>
        public void ClearPrintedMissions()
        {
            lock (this.printedMissionsLockObject)
            {
                Logger.Debug("Clearing cached emergencies");
                this.printedMissions.Clear();
            }
        }

        #endregion

        /// <summary>Initializes this instance.</summary>
        /// <param name="force">Force initialization, even if already initialized.</param>
        private void Initialize(bool force = false)
        {
            if (this.initializeCalled && !force)
            {
                return;
            }

            this.initializeCalled = true;
            this.config = Bootstrapper.GetInstance<IBrowserConfiguration>().Config.PrintSettings;
        }

        /// <summary>The internal print.</summary>
        /// <param name="automaticPrint">if set to <c>true</c> [automatic print].</param>
        private void InternalPrint(bool automaticPrint)
        {
            var data = this.GetData(this.config.PrintUrl);
            var printTemplateViewModel = JsonConvert.DeserializeObject<PrintTemplateViewModel>(data);
            if (printTemplateViewModel?.EinsatzData == null)
            {
                return;
            }

            PrintQueue printQueue = null;
            try
            {
                printQueue = LocalPrintServer.GetDefaultPrintQueue();
                if (printQueue == null)
                {
                    // No printer exist, return null PrintTicket
                    // ReSharper disable once InconsistentlySynchronizedField
                    Logger.Info("No printer exists");
                    return;
                }

                // Get default PrintTicket from printer
                var printTicket = printQueue.DefaultPrintTicket;
                printTicket.CopyCount = automaticPrint
                                            ? this.config.NumberOfPagesOnEmergency
                                            : this.config.NumberOfPagesPerClick;

                var printCapabilites = printQueue.GetPrintCapabilities();

                // Modify PrintTicket
                if (printCapabilites.PageMediaSizeCapability.Any(x => x.PageMediaSizeName.Equals(PageMediaSizeName.ISOA4)))
                {
                    printTicket.PageMediaSize = printCapabilites.PageMediaSizeCapability.First(x => x.PageMediaSizeName.Equals(PageMediaSizeName.ISOA4));
                }

                if (printCapabilites.PageOrientationCapability.Contains(PageOrientation.Portrait))
                {
                    printTicket.PageOrientation = PageOrientation.Portrait;
                }

                if (printCapabilites.PageBorderlessCapability.Contains(PageBorderless.Borderless))
                {
                    printTicket.PageBorderless = PageBorderless.Borderless;
                }

                if (printCapabilites.CollationCapability.Contains(Collation.Collated))
                {
                    printTicket.Collation = Collation.Collated;
                }

                var doc = new FixedDocument();
                foreach (var emergencyData in printTemplateViewModel.EinsatzData)
                {
                    var item = emergencyData;
                    lock (this.printedMissionsLockObject)
                    {
                        if (automaticPrint && this.printedMissions.Any(x => x.EinsatzID.Equals(item.EinsatzID)))
                        {
                            continue;
                        }

                        var tempItem = this.printedMissions.FirstOrDefault(x => x.EinsatzID.Equals(item.EinsatzID));
                        if (tempItem != null
                            && tempItem.PublicInstancePropertiesEqual(
                                item, "Dispositionen", "MapUrl", "MapImage", "Area"))
                        {
                            tempItem.Dispositionen = item.Dispositionen; // Always update dispositions because of missing deep value comparison
                            item = tempItem;
                            Logger.DebugFormat(
                                "Using cached version of emergency ID '{0}'",
                                item.EinsatzID);
                        }
                        else
                        {
                            if (tempItem != null)
                            {
                                Logger.DebugFormat(
                                    "Removing emergency ID '{0}' from cache",
                                    tempItem.EinsatzID);
                                this.printedMissions.Remove(tempItem);
                            }

                            Logger.DebugFormat("Adding emergency ID '{0}' to cache", item.EinsatzID);
                            this.printedMissions.Add(item);
                        }
                    }

                    // ReSharper disable once InconsistentlySynchronizedField
                    Logger.DebugFormat(
                        "Printing '{0}', # of copies={1}",
                        item.EinsatzID,
                        printTicket.CopyCount);

                    if (string.IsNullOrWhiteSpace(item.MapUrl))
                    {
                        // Google Static Maps Developer Guide: https://developers.google.com/maps/documentation/static-maps/intro
                        var mapData =
                            this.GetData(
                                $"https://secure.florian10.info/ows/infoscreen/geo/staticmap.ashx?address={item.Strasse}{(string.IsNullOrWhiteSpace(item.Nummer1) ? string.Empty : "%20" + item.Nummer1)},%20{item.Plz}%20{item.Ort}");
                        if (!string.IsNullOrWhiteSpace(mapData))
                        {
                            var mapUrl =
                                mapData.Remove(mapData.Length - 2, 2)
                                    .Remove(0, 8)
                                    .Replace("&size=800x400&", "&size=640x640&scale=2&")
                                    .Replace("2.png", ".png")
                                    .Replace("&markers=icon:http://", "&markers=scale:2|icon:http://");
                            item.MapUrl = $"{mapUrl}&maptype={this.config.MapType.ToString().ToLowerInvariant()}";
                            var latLng =
                                mapData.Split('&')
                                    .First(x => x.StartsWith("center="))
                                    .Split(',')
                                    .Select(x => x.Replace("center=", string.Empty))
                                    .ToList();

                            if (latLng.Count() >= 2)
                            {
                                var areaJson =
                                    this.GetData(
                                        $"https://secure.florian10.info/ows/infoscreen/geo/umkreis.ashx?lat={latLng[0]}&lng={latLng[1]}");
                                item.Area = JsonConvert.DeserializeObject<Area>(areaJson);
                                item.Area.PointLimit = this.config.MaxHydrants;
                            }
                        }
                    }

                    var fixedPage = new FixedPage();
                    var template = new PrintTemplate { DataContext = item };
                    if (printCapabilites.PageImageableArea != null)
                    {
                        template.Width = printCapabilites.PageImageableArea.ExtentWidth;
                        template.Height = printCapabilites.PageImageableArea.ExtentHeight;
                        FixedPage.SetLeft(template, printCapabilites.PageImageableArea.OriginWidth);
                        FixedPage.SetTop(template, printCapabilites.PageImageableArea.OriginHeight);
                    }

                    fixedPage.Children.Add(template);
                    var pageContent = new PageContent();
                    ((IAddChild)pageContent).AddChild(fixedPage);
                    doc.Pages.Add(pageContent);
                }

                if (doc.Pages.Any())
                {
                    // ReSharper disable once InconsistentlySynchronizedField
                    Logger.Debug("Printing now");
                    PrintQueue.CreateXpsDocumentWriter(printQueue).Write(doc, printTicket);
                }
            }
            finally
            {
                printQueue?.Dispose();
            }
        }

        /// <summary>Gets the data.</summary>
        /// <param name="url">The URL.</param>
        /// <returns>The <see cref="string" />.</returns>
        private string GetData(string url)
        {
            var cookies = new CookieContainer();

            foreach (var cookie in Bootstrapper.GetInstance<IWindowManager>().GetDocumentCookies("infoscreen.florian10.info"))
            {
                cookies.Add(cookie);
            }

            string returnData;

            // Print uri for demo "https://infoscreen.florian10.info/ows/infoscreen/demo.ashx?demo=1";
            // Print uri for emergency "https://infoscreen.florian10.info/ows/infoscreen/einsatz.ashx";
            // Need to retrieve cookies first
            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = "GET";
            request.CookieContainer = cookies;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response == null)
                {
                    return string.Empty;
                }

                using (var stream = response.GetResponseStream())
                {
                    if (stream == null)
                    {
                        return string.Empty;
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        returnData = reader.ReadToEnd();
                    }
                }
            }

            return returnData;
        }

        #endregion
    }
}