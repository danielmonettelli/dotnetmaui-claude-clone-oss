namespace Claude.Converters;

public class BoolToStyleConverter : IValueConverter
{
    // Propiedad opcional para almacenar el estilo cuando IsUser=false (AIBubble)
    public Style? FalseStyle { get; set; }

    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isTrue)
        {
            // Si es true, usamos el estilo pasado como parámetro (UserBubble)
            if (isTrue && parameter is Style trueStyle)
            {
                return trueStyle;
            }
            // Si es false, usamos el FalseStyle (AIBubble) si está establecido
            else if (!isTrue && FalseStyle != null)
            {
                return FalseStyle;
            }
        }

        // Si no se cumplen las condiciones anteriores, devolvemos null
        // para que se use el FallbackValue en el binding
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}