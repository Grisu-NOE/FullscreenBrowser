// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HttpServer.cs" company="Freiwillige Feuerwehr Krems/Donau">
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

namespace At.FF.Krems.FullscreenBrowser
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using At.FF.Krems.Interfaces;
    using At.FF.Krems.Utils.Bootstrapper;

    using log4net;

    /// <summary>The http server.</summary>
    public class HttpServer : IHttpServer
    {
        #region Fields

        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The listener</summary>
        private TcpListener listener;

        /// <summary>The initialize called.</summary>
        private bool initializeCalled;

        /// <summary>The listen task</summary>
        private Task listenTask;

        /// <summary>The cancellation token source</summary>
        private CancellationTokenSource cancellationTokenSource;

        #endregion Fields

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

        /// <summary>Start listening.</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        private void Listen(CancellationToken cancellationToken)
        {
            try
            {
                this.listener = new TcpListener(IPAddress.Loopback, Bootstrapper.GetInstance<IBrowserConfiguration>().Config.PrintSettings.PrintPort);
                this.listener.Start();
                while (!cancellationToken.IsCancellationRequested)
                {
                    var s = this.listener.AcceptTcpClient();
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }

                    var httpProcessor = Bootstrapper.TryGetInstance<IHttpProcessor>();
                    if (httpProcessor == null)
                    {
                        continue;
                    }

                    httpProcessor.Initialize(s);
                    var thread = new Thread(httpProcessor.Process); // TODO: Use Threadpool or Task instead?
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.IsBackground = true;
                    thread.Start();
                }
            }
            catch (Exception exception)
            {
                Logger.Warn(exception);
            }
            finally
            {
                if (this.listener != null)
                {
                    try
                    {
                        this.listener.Stop();
                    }
                    catch (Exception exception)
                    {
                        Logger.Warn(exception);
                    }
                }
            }
        }

        /// <summary>Initializes this instance.</summary>
        /// <param name="force">Force initialization, even if already initialized.</param>
        private void Initialize(bool force = false)
        {
            if (this.initializeCalled && !force)
            {
                return;
            }

            this.initializeCalled = true;
            this.cancellationTokenSource?.Cancel();

            this.cancellationTokenSource = new CancellationTokenSource();
            this.listener?.Stop();

            var token = this.cancellationTokenSource.Token;
            this.listenTask = Task.Factory.StartNew(
                    () => this.Listen(token),
                    token,
                    TaskCreationOptions.LongRunning,
                    TaskScheduler.Default);
        }

        #endregion Methods
    }
}