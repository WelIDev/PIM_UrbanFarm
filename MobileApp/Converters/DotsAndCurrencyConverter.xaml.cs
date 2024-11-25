using System.Globalization;
using MobileApp.Models;

namespace MobileApp.Converters
{
    public class DotsAndCurrencyConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            if (value is ProdutoModel produto)
            {
                var totalProduto = produto.Preco * produto.Quantidade;
                var formattedValue = totalProduto.ToString("C", culture);

                int maxLength = parameter != null &&
                                int.TryParse(parameter.ToString(), out int totalLength)
                    ? totalLength
                    : 25; // Default maximum length if no parameter is provided

                var dots = new string('.', maxLength - formattedValue.Length).Replace(".", " .");

                return $"{dots} {formattedValue}".PadLeft(maxLength, ' ');
            }

            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DotsAndCurrencyConverterDouble : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            if (value is double valorTotal)
            {
                var formattedValue = valorTotal.ToString("C", culture);

                int maxLength = parameter != null &&
                                int.TryParse(parameter.ToString(), out int totalLength)
                    ? totalLength
                    : 25; // Default maximum length if no parameter is provided

                var dots = new string('.', maxLength - formattedValue.Length).Replace(".", " .");

                return $"{dots} {formattedValue}".PadLeft(maxLength, ' ');
            }

            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
