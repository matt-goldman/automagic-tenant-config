using System;
using System.Globalization;
using Xamarin.Forms;

namespace MedMan.Mobile.Converters
{
    public class NameToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((targetType == typeof(string)))
            {
                return GetConvertedValue((string)value);
            }
            else
            {
                return null;
            }
        }

        public string Convert(string value)
        {
            return GetConvertedValue(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string GetConvertedValue(string input)
        {
            switch(input)
            {
                case "Scripts":
                    return IconFont.Prescription;
                case "Patients":
                    return IconFont.AccountMultiple;
                case "Meds":
                    return IconFont.Pill;
                case "Admins":
                    return IconFont.Needle;
                case "Logout":
                    return IconFont.Logout;
                default:
                    return IconFont.Barcode;
            }
        }
    }
}
