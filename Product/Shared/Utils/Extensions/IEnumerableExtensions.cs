// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEnumerableExtensions.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    // ReSharper disable once InconsistentNaming

    /// <summary>The IEnumerable extensions.</summary>
    public static class IEnumerableExtensions
    {
        /// <summary>Determines whether the specified IEnumerable has items.</summary>
        /// <param name="target">The target.</param>
        /// <returns><see cref="bool">False</see> if null or empty.</returns>
        public static bool HasItems(this IEnumerable target)
        {
            return target != null && target.OfType<object>().Any();
        }

        /// <summary>Determines whether the specified generic IEnumerable has items.</summary>
        /// <typeparam name="T">The element type.</typeparam>
        /// <param name="target">The target.</param>
        /// <returns><see cref="bool">False</see> if null or empty.</returns>
        public static bool HasItems<T>(this IEnumerable<T> target)
        {
            return target != null && target.Any();
        }
    }
}