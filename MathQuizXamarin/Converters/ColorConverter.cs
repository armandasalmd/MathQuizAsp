using MathQuizXamarin.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MathQuizXamarin.Converters
{
    public class MessageTypeToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                MessageType type = (MessageType)value;
                switch (type)
                {
                    case MessageType.Primary:
                        return Color.BlueViolet;
                    case MessageType.Success:
                        return Color.Green;
                    case MessageType.Warning:
                        return Color.Orange;
                    case MessageType.Info:
                        return Color.DeepSkyBlue;
                    case MessageType.Error:
                        return Color.Red;
                    default:
                        return Color.Default;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return Color.Red;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
