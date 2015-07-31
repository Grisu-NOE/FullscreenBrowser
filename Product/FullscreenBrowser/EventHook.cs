// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventHook.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    using At.FF.Krems.Interfaces;

    using log4net;

    /// <summary>The event hook.</summary>
    public class EventHook : IEventHook
    {
        #region Fields

        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The process delegate.</summary>
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly NativeMethods.WinEventDelegate procDelegate;

        /// <summary>The active lock object</summary>
        private readonly object activeLockObject = new object();

        /// <summary>The win event hook.</summary>
        private IntPtr winEventHook;

        /// <summary>The active.</summary>
        private bool active;

        /// <summary>The event minimum</summary>
        private WinEvents eventMinimum;

        /// <summary>The event maximum</summary>
        private WinEvents eventMaximum;

        #endregion

        #region Ctor

        /// <summary>Initializes a new instance of the <see cref="EventHook"/> class.</summary>
        public EventHook()
        {
            this.procDelegate = this.WinEventProc;
        }

        #endregion

        #region Dtor

        /// <summary>Finalizes an instance of the <see cref="EventHook"/> class. </summary>
        ~EventHook()
        {
            this.Stop();
        }

        #endregion

        #region Events

        /// <summary>The event fired.</summary>
        public event NativeMethods.WinEventDelegate EventFired;

        #endregion

        #region Methods

        /// <summary>The setup.</summary>
        /// <param name="winEvent">The win event.</param>
        public void Setup(WinEvents winEvent)
        {
            this.Setup(winEvent, winEvent);
        }

        /// <summary>Setups the specified event minimum.</summary>
        /// <param name="eventMin">The event minimum.</param>
        /// <param name="eventMax">The event maximum.</param>
        public void Setup(WinEvents eventMin, WinEvents eventMax)
        {
            lock (this.activeLockObject)
            {
                if (this.active)
                {
                    return;
                }

                this.eventMinimum = eventMin;
                this.eventMaximum = eventMax;
            }
        }

        /// <summary>Starts this instance.</summary>
        public void Start()
        {
            lock (this.activeLockObject)
            {
                if (this.active)
                {
                    return;
                }

                this.active = true;
                this.winEventHook = NativeMethods.SetWinEventHook(
                    (uint)this.eventMinimum,
                    (uint)this.eventMaximum,
                    IntPtr.Zero,
                    this.procDelegate,
                    0,
                    0,
                    (uint)WinEvents.WinEventOutOfContext);
            }
        }

        /// <summary>Stops this instance.</summary>
        public void Stop()
        {
            lock (this.activeLockObject)
            {
                if (!this.active)
                {
                    return;
                }

                this.active = false;
                NativeMethods.UnhookWinEvent(this.winEventHook);
                this.winEventHook = IntPtr.Zero;
            }
        }

        /// <summary>The win event process.</summary>
        /// <param name="hWinEventHook">The h win event hook.</param>
        /// <param name="eventType">The event type.</param>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="idObject">The id object.</param>
        /// <param name="idChild">The id child.</param>
        /// <param name="dwEventThread">The DW event thread.</param>
        /// <param name="dwmsEventTime">The DWMS event time.</param>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        private void WinEventProc(
            IntPtr hWinEventHook,
            uint eventType,
            IntPtr hwnd,
            int idObject,
            int idChild,
            uint dwEventThread,
            uint dwmsEventTime)
        {
            Logger.DebugFormat("Foreground changed to {0:x8}", hwnd.ToInt32());
            if (this.EventFired != null)
            {
                this.EventFired(hWinEventHook, eventType, hwnd, idObject, idChild, dwEventThread, dwmsEventTime);
            }
        }

        #endregion
    }
}