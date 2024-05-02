
using System.Globalization;



namespace UltimateMatch.Resources.Utils
{
    public class Base64ToImageSourceConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string base64String)
            {
                try
                {
                    byte[] imageBytes = System.Convert.FromBase64String(base64String);
                    Stream stream = new MemoryStream(imageBytes);
                    return ImageSource.FromStream(() => stream);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al convertir la cadena base64 en imagen: {ex.Message}");
                    return null;
                }
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
