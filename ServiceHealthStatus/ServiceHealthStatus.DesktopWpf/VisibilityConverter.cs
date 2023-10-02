using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace ServiceHealthStatus.DesktopWpf.View.Shared
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value.Equals(parameter) ? Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
