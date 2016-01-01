using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace At.FF.Krems.RPi2.Win10IoT.FullscreenBrowser
{
    using System.Diagnostics;
    using System.Net;
    using System.Text;

    using Windows.Networking;
    using Windows.Networking.Sockets;
    using Windows.Security.Cryptography.Certificates;
    using Windows.Storage;
    using Windows.Storage.Streams;
    using Windows.Web.Http;
    using Windows.Web.Http.Filters;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.Loaded += OnLoaded;
            this.InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            //var streamSocket = new StreamSocketListener();
            //streamSocket.Control.NoDelay = false;
            //streamSocket.ConnectionReceived += StreamSocketOnConnectionReceived;
            //await streamSocket.BindEndpointAsync(new HostName("localhost"), "121");

            MyWebView.NavigationCompleted += MyWebViewOnNavigationCompleted;
            MyWebView.NavigationFailed += MyWebViewOnNavigationFailed;
            MyWebView.PermissionRequested += MyWebViewOnPermissionRequested;
            MyWebView.UnsafeContentWarningDisplaying += MyWebViewOnUnsafeContentWarningDisplaying;
            MyWebView.UnsupportedUriSchemeIdentified += MyWebViewOnUnsupportedUriSchemeIdentified;
            MyWebView.UnviewableContentIdentified += MyWebViewOnUnviewableContentIdentified;
            MyWebView.ScriptNotify += MyWebViewOnScriptNotify;
            MyWebView.Settings.IsJavaScriptEnabled = true;

            var uriString = "https://infoscreen.florian10.info/ows/infoscreen/v3/";
#if DEBUG
            uriString += "?demo=1";
#endif
            MyWebView.Navigate(new Uri(uriString));
        }

        private void MyWebViewOnUnviewableContentIdentified(WebView sender, WebViewUnviewableContentIdentifiedEventArgs args)
        {
            Debug.WriteLine("Unviewable content identified! Uri: {0} MediaType: {1}", args.Uri, args.MediaType);
            Debugger.Break();
        }

        private void MyWebViewOnUnsupportedUriSchemeIdentified(WebView sender, WebViewUnsupportedUriSchemeIdentifiedEventArgs args)
        {
            Debug.WriteLine("Unsupported Uri scheme identified! Uri: {0}", args.Uri);
            Debugger.Break();
        }

        private void MyWebViewOnUnsafeContentWarningDisplaying(WebView sender, object args)
        {
            Debug.WriteLine("Unsafe content warning displaying! {0}", args.ToString());
            Debugger.Break();
        }

        private void MyWebViewOnPermissionRequested(WebView sender, WebViewPermissionRequestedEventArgs args)
        {
            Debug.WriteLine("Permission requested!");
            Debugger.Break();
        }

        private void MyWebViewOnNavigationFailed(object sender, WebViewNavigationFailedEventArgs webViewNavigationFailedEventArgs)
        {
            Debug.WriteLine("Navigation failed! Uri: {0} Web error status: {1}", webViewNavigationFailedEventArgs.Uri, webViewNavigationFailedEventArgs.WebErrorStatus);
            Debugger.Break();
        }

        //Mixed Content: The page at 'https://infoscreen.florian10.info/ows/infoscreen/v3/?demo=1' was loaded over HTTPS, but requested an insecure XMLHttpRequest endpoint 'http://localhost:121/?action=WASTL_DISPLAY_ACTIVATE'. This request has been blocked; the content must be served over HTTPS.

        //Mixed Content: The page at 'https://infoscreen.florian10.info/ows/infoscreen/v3/?demo=1' was loaded over HTTPS, but requested an insecure XMLHttpRequest endpoint 'http://localhost:121/?alarmstufe=T1&typ=T&meldebild=Fahrzeugbergung&melder=…m=2015-12-02+20%3A34&einsatznummer=KS+0815&action=WASTL_PRINTSERVICE_PRINT'. This request has been blocked; the content must be served over HTTPS.

        private void MyWebViewOnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            Debug.WriteLine("Navigation to {0} completed! Success: {1} WebErrorStatus: {2}", args.Uri, args.IsSuccess, args.WebErrorStatus);
        }

        //private async void StreamSocketOnConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        //{
        //    Debugger.Break();
        //    var streamSocket = args.Socket;

        //    var request = new StringBuilder();

        //    using (var input = streamSocket.InputStream)
        //    {
        //        const int BufferSize = 12000;
        //        var data = new byte[BufferSize];
        //        var buffer = data.AsBuffer();
        //        uint dataRead = BufferSize;
        //        while (dataRead == BufferSize)
        //        {
        //            await input.ReadAsync(buffer, BufferSize, InputStreamOptions.None);
        //            request.Append(Encoding.UTF8.GetString(data, 0, (int)buffer.Length));
        //            dataRead = buffer.Length;
        //        }

        //        var message = request.ToString();
        //    }
        //}

        private void MyWebViewOnScriptNotify(object sender, NotifyEventArgs notifyEventArgs)
        {
            Debug.WriteLine("Script notify! Uri: {0} Value: {1}", notifyEventArgs.CallingUri, notifyEventArgs.Value);
        }
    }
}
