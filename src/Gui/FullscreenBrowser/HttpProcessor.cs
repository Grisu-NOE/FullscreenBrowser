// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HttpProcessor.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Security.Permissions;
    using System.Threading;

    using At.FF.Krems.Interfaces;
    using At.FF.Krems.Utils.Bootstrapper;

    using log4net;

    /// <summary>The http processor.</summary>
    public class HttpProcessor : IHttpProcessor
    {
        #region Fields

        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The TCP socket.</summary>
        private TcpClient socket;

        /// <summary>The input stream.</summary>
        private Stream inputStreamField;

        #endregion Fields

        #region Properties

        /// <summary>Gets the http URL.</summary>
        public string HttpUrl
        {
            get;
            private set;
        }

        /// <summary>Gets the output stream.</summary>
        public StreamWriter OutputStream
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        /// <summary>Initializes the specified TCP client.</summary>
        /// <param name="tcpClient">The TCP client.</param>
        public void Initialize(TcpClient tcpClient)
        {
            this.socket = tcpClient;
        }

        /// <summary>The process.</summary>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public void Process()
        {
            using (this.inputStreamField = new BufferedStream(this.socket.GetStream()))
            {
                this.OutputStream = new StreamWriter(new BufferedStream(this.socket.GetStream()));
                try
                {
                    this.ParseRequest();
                    this.HandleGetRequest();
                }
                catch (Exception exception)
                {
                    this.WriteFailure();
                    Logger.Warn(exception);
                }

                try
                {
                    this.OutputStream.Flush();
                }
                catch (Exception exception)
                {
                    Logger.Warn(exception);
                }

                this.inputStreamField = null;
                this.OutputStream = null;
                this.socket.Close();
            }
        }

        /// <summary>The write failure.</summary>
        public void WriteFailure()
        {
            this.OutputStream.WriteLine("HTTP/1.0 500");
            this.OutputStream.WriteLine("Connection: close");
            this.OutputStream.WriteLine(string.Empty);
        }

        /// <summary>The write success.</summary>
        public void WriteSuccess()
        {
            this.OutputStream.WriteLine("HTTP/1.0 200 OK");
            this.OutputStream.WriteLine("Content-Type: application/json");
            this.OutputStream.WriteLine("Connection: close");
            this.OutputStream.WriteLine(string.Empty);
        }

        /// <summary>The stream read line.</summary>
        /// <param name="inputStream">The input stream.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string StreamReadLine(Stream inputStream)
        {
            var text = string.Empty;
            while (true)
            {
                var num = inputStream.ReadByte();
                if (num == 10)
                {
                    break;
                }

                if (num == 13)
                {
                    continue;
                }

                if (num == -1)
                {
                    Thread.Sleep(1);
                }
                else
                {
                    text += Convert.ToChar(num);
                }
            }

            return text;
        }

        /// <summary>The log.</summary>
        /// <param name="message">The message.</param>
        /// <param name="eventLogEntryType">The event log entry type.</param>
        private static void Log(string message, EventLogEntryType eventLogEntryType)
        {
            if (!EventLog.SourceExists("WASTL"))
            {
                EventLog.CreateEventSource("WASTL", "WASTL");
            }

            EventLog.WriteEntry("WASTL", message, eventLogEntryType);
        }

        /// <summary>The handle get request.</summary>
        private void HandleGetRequest()
        {
            Debug.WriteLine("request: {0}", this.HttpUrl);
            var httpUrl = Uri.UnescapeDataString(this.HttpUrl);
            try
            {
                var str = string.Empty;
                var split = httpUrl.Split('&');
                if (split.Any())
                {
                    split[0] = split[0].Remove(0, 2);
                    var dict = new Dictionary<string, string>();
                    foreach (var item in split)
                    {
                        var splittedItem = item.Replace('+', ' ').Split('=');
                        if (!splittedItem.Any())
                        {
                            continue;
                        }

                        dict.Add(splittedItem[0], splittedItem.Length > 1 ? splittedItem[1] : string.Empty);
                    }

                    if (dict.TryGetValue("action", out var action))
                    {
                        switch (action.ToUpper())
                        {
                            case "WASTL_DISPLAY_ACTIVATE":
                                Bootstrapper.GetInstance<IWindowManager>().ActivateWastlDisplay();
                                Bootstrapper.GetInstance<IPrintController>().Print(true);
                                break;

                            case "WASTL_LOG":
                                Log("Log: " + httpUrl, EventLogEntryType.Information);
                                break;
                            case "WASTL_PRINTSERVICE_PRINT":
                                Bootstrapper.GetInstance<IPrintController>().Print();
                                break;
                        }
                    }
                }

                this.WriteSuccess();
                this.OutputStream.WriteLine("{status:\"OK\",message:\"" + str + "\"}");
            }
            catch (Exception exception)
            {
                Logger.Warn(exception);
                this.WriteFailure();
                this.OutputStream.WriteLine("{status:\"Error\",message:\"" + exception.Message + "\"}");
            }
        }

        /// <summary>The parse request.</summary>
        private void ParseRequest()
        {
            var text = StreamReadLine(this.inputStreamField);
            this.HttpUrl = text.Split(' ')[1];
        }

        #endregion Methods
    }
}