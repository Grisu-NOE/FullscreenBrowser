// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScreenRefresher.xaml.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.FullscreenBrowser
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    using At.FF.Krems.Interfaces;
    using At.FF.Krems.Utils.Bootstrapper;

    using Timer = System.Timers.Timer;

    /// <summary>Interaction logic for ScreenRefresher.XAML</summary>
    public partial class ScreenRefresher : IScreenRefresher
    {
        #region Fields

        /// <summary>The cycle colors.</summary>
        private readonly Color[] cycleColors =
            {
                Colors.White,
                Colors.Black,
                Colors.Lime,
                Colors.Red,
                Colors.Blue
            };

        /// <summary>The timer</summary>
        private readonly Timer timer = new Timer { AutoReset = false };

        /// <summary>The initialize called</summary>
        private bool initializeCalled;

        /// <summary>The configuration</summary>
        private Configuration.XML.ScreenRefresher config;

        /// <summary>The event hook</summary>
        private IEventHook eventHook;

        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="ScreenRefresher"/> class.</summary>
        public ScreenRefresher()
        {
            this.InitializeComponent();
        }

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

        /// <summary>Hides the screen refresher.</summary>
        public void HideScreenRefresher()
        {
            if (!this.IsVisible)
            {
                return;
            }

            if (!this.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(new Action(this.HideScreenRefresher));
                return;
            }

            this.Dispatcher.BeginInvoke(new Action(this.eventHook.Stop));
            this.Hide();
            this.StartTimer();
        }

        /// <summary>Shows the screen refresher.</summary>
        public void ShowScreenRefresher()
        {
            Task.Factory.StartNew(
                () =>
                {
                    if (!this.IsVisible)
                    {
                        this.Dispatcher.Invoke(this.Show);
                    }

                    const string ThreadName = "ScreenRefresherAnimationThread";
                    if (string.IsNullOrWhiteSpace(Thread.CurrentThread.Name) || Thread.CurrentThread.Name != ThreadName)
                    {
                        Thread.CurrentThread.Name = ThreadName;
                    }

                    this.Dispatcher.BeginInvoke(new Action(this.eventHook.Start));
                    this.StartAnimation(this.cycleColors.First());
                },
                CancellationToken.None,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }

        /// <summary>Raises the <see cref="E:Closing" /> event.</summary>
        /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.HideScreenRefresher();
            base.OnClosing(e);
        }

        /// <summary>Initializes this instance.</summary>
        /// <param name="force">Force initialization, even if already initialized.</param>
        private void Initialize(bool force = false)
        {
            if ((this.initializeCalled && !force) || !Bootstrapper.GetInstance<IBrowserConfiguration>().Config.ScreenRefresher.Enabled)
            {
                return;
            }

            if (!this.initializeCalled)
            {
                this.timer.Elapsed += (sender, args) => this.ShowScreenRefresher();
                this.eventHook = Bootstrapper.GetInstance<IEventHook>();
                this.eventHook.Setup(WinEvents.EventSystemForeground);
                this.eventHook.EventFired +=
                    (hook, type, hwnd, idObject, child, thread, time) =>
                        this.Dispatcher.BeginInvoke(
                        new Action(() =>
                            NativeMethods.SetWindowPos(
                            new WindowInteropHelper(this).Handle,
                            NativeMethods.HwndTopmost,
                            0,
                            0,
                            0,
                            0,
                            NativeMethods.TopmostFlags)));
            }

            this.initializeCalled = true;
            this.config = Bootstrapper.GetInstance<IBrowserConfiguration>().Config.ScreenRefresher;
            Canvas.SetTop(this.RectangleAnimation, this.config.Height * -1);
            this.RectangleAnimation.Height = this.config.Height;

            if (this.config.RunAtStartup)
            {
                this.ShowScreenRefresher();
            }
            else
            {
                this.StartTimer();
            }
        }

        /// <summary>Starts the timer.</summary>
        private void StartTimer()
        {
            this.timer.Interval = this.config.Interval * 60 * 1000;
            if (!this.timer.Enabled)
            {
                this.timer.Start();
            }
        }

        /// <summary>Starts the animation.</summary>
        /// <param name="color">The color.</param>
        private void StartAnimation(Color color)
        {
            if (!this.IsVisible)
            {
                return;
            }

            if (!this.CheckAccess())
            {
                this.Dispatcher.Invoke(() => this.StartAnimation(color));
                return;
            }

            this.WindowAnimation.Width = SystemParameters.VirtualScreenWidth;
            this.WindowAnimation.Height = SystemParameters.VirtualScreenHeight;
            this.RectangleAnimation.Fill = new SolidColorBrush(color);
            var currentPositionY = Canvas.GetTop(this.RectangleAnimation);
            var moveAnimY = new DoubleAnimation(
                currentPositionY,
                currentPositionY > 0 ? this.RectangleAnimation.Height * -1 : this.WindowAnimation.Height,
                new Duration(TimeSpan.FromSeconds(this.config.Duration)));
            if (this.cycleColors.Last() != ((SolidColorBrush)this.RectangleAnimation.Fill).Color)
            {
                moveAnimY.Completed += (sender, args) => this.StartAnimation(this.cycleColors.SkipWhile(col => col != ((SolidColorBrush)this.RectangleAnimation.Fill).Color).Skip(1).First());
            }
            else
            {
                moveAnimY.Completed += (sender, args) => this.HideScreenRefresher();
            }

            this.RectangleAnimation.BeginAnimation(Canvas.TopProperty, moveAnimY);
        }

        #endregion
    }
}