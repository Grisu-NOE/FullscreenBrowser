// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringStartsWithParamToBoolConverter.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.Utils.WPF.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>The string starts with parameter to boolean converter.</summary>
    [ValueConversion(typeof(string), typeof(bool), ParameterType = typeof(string))]
    public class StringStartsWithParamToBoolConverter : IValueConverter
    {
        /// <summary>Converts the specified value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object" />.</returns>
        /// <exception cref="System.InvalidOperationException">The target must be a boolean</exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return false;
            }

            return ((string)value).StartsWith((string)parameter);
        }

        /// <summary>The convert back.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object"/>.</returns>
        /// <exception cref="System.NotSupportedException">Cannot convert back</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
}