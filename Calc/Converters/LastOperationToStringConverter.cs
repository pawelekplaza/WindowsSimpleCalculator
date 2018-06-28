using Calc.Helpers;
using Calc.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Calc.Converters
{
    public class LastOperationToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ((Operation)value).GetSign();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
