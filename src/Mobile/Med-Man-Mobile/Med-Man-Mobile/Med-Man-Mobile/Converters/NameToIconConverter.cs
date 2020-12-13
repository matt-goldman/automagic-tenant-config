using System;
using System.Globalization;
using Xamarin.Forms;

namespace MedManMobile.Converters
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

    static class IconFont
    {
        public const string Prescription = "\U000f0706";
        public const string MedicalBag = "\U000f06ef";
        public const string Needle = "\U000f0391";
        public const string Pill = "\U000f0402";
        public const string AccountMultiple = "\U000f000e";
        public const string Logout = "\U000f0343";
        public const string Barcode = "\U000f0071";
    }
}
