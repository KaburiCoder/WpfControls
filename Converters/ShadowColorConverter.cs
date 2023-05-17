using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfControls.Converters
{
  public class ShadowColorConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      SolidColorBrush brush = (SolidColorBrush)value;
      return brush.Color;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
