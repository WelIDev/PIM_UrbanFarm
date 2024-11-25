using System.Globalization;

namespace MobileApp.Converters
{
    public class TruncateConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var str = value.ToString();
            if (parameter != null && int.TryParse(parameter.ToString(), out int length))
            {
                if (str != null && str.Length > length)
                {
                    return str.Substring(0, length);
                }
            }
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
