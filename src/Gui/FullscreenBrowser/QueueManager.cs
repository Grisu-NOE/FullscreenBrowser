// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueueManager.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.FullscreenBrowser
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;
    using System.Threading;

    using At.FF.Krems.Utils.Logging;

    using log4net;

    /// <summary>The queue manager.</summary>
    public class QueueManager
    {
        #region Fields

        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The action queue</summary>
        private readonly BlockingCollection<Action> actionQueue = new BlockingCollection<Action>();

        /// <summary>The action worker thread</summary>
        private readonly Thread actionWorkerThread;

        #endregion

        /// <summary>Initializes a new instance of the <see cref="QueueManager" /> class.</summary>
        public QueueManager()
        {
            Logger.DebugFormat("Initialize {0}()", MethodBase.GetCurrentMethod().Name);
            this.actionWorkerThread = new Thread(
                () =>
                {
                    foreach (var action in this.actionQueue.GetConsumingEnumerable())
                    {
                        try
                        {
                            Logger.DebugFormat("Executing action \"{0}\" on \"{1}\"", action.Method, action.Target);
                            action();
                        }
                        catch (Exception exception)
                        {
                            Logger.Error("Exception occured in action\"" + action.Method + "\" on \"" + action.Target + "\"", exception);
                        }
                    }
                }) { Name = "Action Worker Thread", IsBackground = true };
            this.actionWorkerThread.SetApartmentState(ApartmentState.STA);

            Logger.Debug("Start actionWorkerThread");
            this.actionWorkerThread.Start();
        }

        /// <summary>Executes an action in the worker thread. If you call this method not on worker thread, no action will be executed.</summary>
        /// <param name="action">The action. Use it like delegate{...}.</param>
        /// <param name="executeIfCalledOnWorkerThread">If method gets called on worker thread and this flag is set to <c>true</c>, action will be called direct on worker thread.
        /// If <c>false</c> no execution! Default is <c>false</c>.</param>
        public void ExecuteAction(Action action, bool executeIfCalledOnWorkerThread = false)
        {
            if (action == null)
            {
                Logger.Warn("Action is NULL");
                return;
            }

            if (Thread.CurrentThread == this.actionWorkerThread)
            {
                if (executeIfCalledOnWorkerThread)
                {
                    Logger.TraceFormat("ExecuteAction({0}, true) -> ExecuteIfCalledOnWorkerThread is true => execute action direct on thread \"{1}\"!", action.Method, Thread.CurrentThread.Name);
                    action();
                    return;
                }

                Logger.TraceFormat("ExecuteAction({0}, false) -> ExecuteIfCalledOnWorkerThread is false => no execution of action on thread \"{1}\"!", action.Method, Thread.CurrentThread.Name);
            }

            Logger.TraceFormat("Adding action({0}) to action queue", action.Method);
            this.actionQueue.Add(action);
        }
    }
}