// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaxMax2Int.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.Utils.WPF.ValidationRules
{
    using System.Globalization;
    using System.Windows.Controls;

    /// <summary>The max, max2 or integer validation rule.</summary>
    public class MaxMax2Int : ValidationRule
    {
        /// <summary>
        /// When overridden in a derived class, performs validation checks on a value.
        /// </summary>
        /// <param name="value">The value from the binding target to check.</param>
        /// <param name="cultureInfo">The culture to use in this rule.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.Controls.ValidationResult" /> object.
        /// </returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "String is null or empty.");
            }

            var valueString = ((string)value).ToLowerInvariant();
            int integer;
            var match = valueString == "max" || valueString == "max2" || int.TryParse(valueString, out integer);
            return new ValidationResult(match, match ? null : "Only max, max2 or integer allowed.");
        }
    }
}