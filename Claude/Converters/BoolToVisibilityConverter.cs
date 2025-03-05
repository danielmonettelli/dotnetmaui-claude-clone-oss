namespace Claude.Converters;

public class BoolToVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isTrue)
        {
            // Si IsUser es true, muestra el elemento; de lo contrario, ocúltalo
            return isTrue;
        }

        return true; // Valor por defecto
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
