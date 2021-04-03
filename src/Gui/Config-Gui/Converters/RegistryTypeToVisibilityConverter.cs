// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryTypeToVisibilityConverter.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.Config_Gui.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    using At.FF.Krems.Configuration.XML;

    /// <summary>
    /// Converts the <see cref="BrowserRegistryType"/> to <see cref="Visibility"/>
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    public class RegistryTypeToVisibilityConverter : IValueConverter
    {
        /// <summary>Converts a value.</summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BrowserRegistryType currentRegistryType;
            if (!Enum.TryParse(value.ToString(), true, out currentRegistryType))
            {
                return Visibility.Collapsed;
            }

            BrowserRegistryType desiredRegistryType;
            if (!Enum.TryParse(parameter.ToString(), true, out desiredRegistryType))
            {
                return Visibility.Collapsed;
            }

            return currentRegistryType == desiredRegistryType ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>Converts a value.</summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <exception cref="System.NotImplementedException"><see cref="Visibility"/> to <see cref="BrowserRegistryType"/> is not supported</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}