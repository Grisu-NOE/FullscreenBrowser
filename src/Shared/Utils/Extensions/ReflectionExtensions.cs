// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionExtensions.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.Utils.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    using log4net;

    /// <summary>The reflection helpers.</summary>
    public static class ReflectionExtensions
    {
        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>Gets the entry assembly.
        /// In unit tests Assembly.GenEntryAssembly() always returns null, therefore
        /// we try to get a reasonable entry assembly from the StackTrace.</summary>
        /// <param name="startsWith">The starts with.</param>
        /// <returns>The entry <see cref="Assembly"/>.</returns>
        public static Assembly GetEntryAssembly(string startsWith = null)
        {
            var result = Assembly.GetEntryAssembly();

            if (result == null)
            {
                var search = (startsWith ?? "AT.").ToUpper();

                // expression to filter for methods only in an assembly that starts with "AT"
                Expression<Func<MethodBase, bool>> filter = m =>
                        m.DeclaringType != null &&
                        m.DeclaringType.Assembly != null &&
                        m.DeclaringType.Assembly.GetName() != null &&
                        m.DeclaringType.Assembly.GetName().Name != null &&
                        m.DeclaringType.Assembly.GetName().Name.ToUpper().StartsWith(search);

                var assemblies = GetAssembliesFromStackTrace(filter).ToList();
                result = Assembly.GetEntryAssembly() ?? (assemblies.HasItems() ? assemblies.LastOrDefault() : null);
            }

            return result;
        }

        /// <summary>Gets the assembly from stack trace.</summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The <see><cref>IEnumerable</cref></see>.</returns>
        public static IEnumerable<Assembly> GetAssembliesFromStackTrace(Expression<Func<MethodBase, bool>> expression)
        {
            try
            {
                var frames = new StackTrace().GetFrames();
                if (frames != null)
                {
                    return frames.Select(f => f.GetMethod())        // get mehtod from frame
                            .Where(m => expression.Compile()(m))    // apply filter
                            .Select(m => m.DeclaringType)
                            .Select(t => t.Assembly);
                }
            }
            catch (Exception exception)
            {
                Logger.Warn(exception);
                Trace.WriteLine(exception.Message);
            }

            return null;
        }

        /// <summary>The public instance properties equal.</summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="self">The self.</param>
        /// <param name="to">The to.</param>
        /// <param name="ignore">The ignore.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool PublicInstancePropertiesEqual<T>(this T self, T to, params string[] ignore) where T : class
        {
            if (self == null || to == null)
            {
                return self == to;
            }

            var type = typeof(T);
            var ignoreList = new List<string>(ignore);
            var unequalProperties =
                from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where !ignoreList.Contains(pi.Name)
                let selfValue = type.GetProperty(pi.Name).GetValue(self, null)
                let toValue = type.GetProperty(pi.Name).GetValue(to, null)
                where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
                select selfValue;
            return !unequalProperties.Any();
        }
    }
}