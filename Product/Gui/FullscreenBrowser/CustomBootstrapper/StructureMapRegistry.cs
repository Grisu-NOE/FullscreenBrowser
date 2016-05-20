// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="StructureMapRegistry.cs" company="Freiwillige Feuerwehr Krems/Donau">
//       Freiwillige Feuerwehr Krems/Donau
//       Austraße 33
//       A-3500 Krems/Donau
//       Austria
//
//       Tel.:   +43 (0)2732 85522
//       Fax.:   +43 (0)2732 85522 40
//       E-mail: office@feuerwehr-krems.at
//
//       This software is furnished under a license and may be
//       used  and copied only in accordance with the terms of
//       such  license  and  with  the  inclusion of the above
//       copyright  notice.  This software or any other copies
//       thereof   may  not  be  provided  or  otherwise  made
//       available  to  any  other  person.  No  title  to and
//       ownership of the software is hereby transferred.
//
//       The information in this software is subject to change
//       without  notice  and  should  not  be  construed as a
//       commitment by Freiwillige Feuerwehr Krems/Donau.
//
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace At.FF.Krems.FullscreenBrowser.CustomBootstrapper
{
    using Interfaces;
    using Print;
    using StructureMap;
    using StructureMap.Pipeline;
    using Utils;
    using Utils.Logging;

    /// <summary>The structure map registry.</summary>
    public class StructureMapRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="StructureMapRegistry"/> class.</summary>
        public StructureMapRegistry()
        {
            // Pay attention to the order of the startup tasks!
            this.ForSingletonOf<IProcessStateLogging>().Use<ProcessStateLogging>();
            this.ForSingletonOf<ILoggingConfigurator>().Use<LoggingConfigurator>();
            this.Forward<ILoggingConfigurator, IStartupTask>();
            this.ForSingletonOf<IBrowserConfiguration>().Use<BrowserConfiguration>();
            this.Forward<IBrowserConfiguration, IStartupTask>();
            this.For<IGeckoBrowser>().LifecycleIs(new UniquePerRequestLifecycle()).Use<GeckoBrowser>();
            this.Forward<IGeckoBrowser, IStartupTask>();
            this.ForSingletonOf<App>().Use<App>();
            this.Forward<App, IApp>();
            this.Forward<App, IStartupTask>();
            this.ForSingletonOf<IScreensaver>().Use<Screensaver>();
            this.ForSingletonOf<IPowerManagement>().Use<PowerManagement>();
            this.For<IEventHook>().LifecycleIs(new UniquePerRequestLifecycle()).Use<EventHook>();
            this.ForSingletonOf<IScreenRefresher>().Use<ScreenRefresher>();
            this.Forward<IScreenRefresher, IStartupTask>();
            this.ForSingletonOf<IPrintController>().Use<PrintController>();
            this.Forward<IPrintController, IStartupTask>();
            this.ForSingletonOf<IWindowManager>().Use<WindowManager>();
            this.Forward<IWindowManager, IStartupTask>();
            this.For<IHttpProcessor>().LifecycleIs(new UniquePerRequestLifecycle()).Use<HttpProcessor>();
            this.ForSingletonOf<IHttpServer>().Use<HttpServer>();
            this.Forward<IHttpServer, IStartupTask>();
        }
    }
}
