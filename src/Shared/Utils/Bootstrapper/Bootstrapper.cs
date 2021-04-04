// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Bootstrapper.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.Utils.Bootstrapper
{
    using System.Diagnostics;

    using StructureMap;

    /// <summary>The BOOTSTRAPPER.</summary>
    public static class Bootstrapper
    {
        /// <summary>The initialize called</summary>
        private static bool initializeCalled;

        /// <summary>Gets the container.</summary>
        /// <value>The container.</value>
        public static IContainer Container { get; private set; }

        /// <summary>Initializes this instance.</summary>
        /// <param name="registry">The registry.</param>
        public static void Initialize(Registry registry)
        {
            if (initializeCalled)
            {
                return;
            }

            initializeCalled = true;
            Container = new Container(registry);
            Debug.WriteLine(WhatDoIHave());
            new StartupTasks().Run();
        }

        /// <summary>Gets the instance.</summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The instance of the type from the container.</returns>
        public static T GetInstance<T>()
        {
            return Container.GetInstance<T>();
        }

        /// <summary>Tries to get the instance.</summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The instance of the type from the container.</returns>
        public static T TryGetInstance<T>()
        {
            return Container.TryGetInstance<T>();
        }

        /// <summary>What do i have.</summary>
        /// <returns>The <see cref="string"/>.</returns>
        public static string WhatDoIHave()
        {
            return Container.WhatDoIHave();
        }
    }
}