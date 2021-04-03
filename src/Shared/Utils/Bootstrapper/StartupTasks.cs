// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StartupTasks.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.Utils.Bootstrapper
{
    using System.Collections.Generic;
    using System.Linq;

    using At.FF.Krems.Interfaces;

    /// <summary>The startup tasks.</summary>
    public class StartupTasks
    {
        /// <summary>The task list</summary>
        private IEnumerable<TaskExecutionParameters> taskList;

        /// <summary>Runs this instance.</summary>
        public void Run()
        {
            if (this.taskList == null)
            {
                this.taskList = this.AddExecutionParameters(this.GetTasks());
            }

            this.taskList.ToList().ForEach(Run);
        }

        /// <summary>Resets this instance.</summary>
        public void Reset()
        {
            if (this.taskList == null)
            {
                this.taskList = this.AddExecutionParameters(this.GetTasks());
            }

            this.taskList.ToList().ForEach(Reset);
        }

        /// <summary>Runs the specified task execution parameters.</summary>
        /// <param name="taskExecutionParameters">The task execution parameters.</param>
        private static void Run(TaskExecutionParameters taskExecutionParameters)
        {
            taskExecutionParameters.Task.Init();
        }

        /// <summary>Resets the specified task execution parameters.</summary>
        /// <param name="taskExecutionParameters">The task execution parameters.</param>
        private static void Reset(TaskExecutionParameters taskExecutionParameters)
        {
            taskExecutionParameters.Task.Reset();
        }

        /// <summary>Gets the tasks.</summary>
        /// <returns>The <see cref="List"/>.</returns>
        private List<IStartupTask> GetTasks()
        {
            var tasks = new List<IStartupTask>();
            if (Bootstrapper.Container != null)
            {
                tasks.AddRange(Bootstrapper.Container.GetAllInstances<IStartupTask>());
            }

            return tasks;
        }

        /// <summary>Adds the execution parameters.</summary>
        /// <param name="tasks">The tasks.</param>
        /// <returns>The <see cref="IEnumerable"/>.</returns>
        private IEnumerable<TaskExecutionParameters> AddExecutionParameters(List<IStartupTask> tasks)
        {
            var tasksWithParameters = new List<TaskExecutionParameters>();
            var i = 0;
            tasks.ForEach(t => tasksWithParameters.Add(new TaskExecutionParameters
                                                   {
                                                       Task = t,
                                                       Position = i++
                                                   }));
            return tasksWithParameters;
        }
    }
}