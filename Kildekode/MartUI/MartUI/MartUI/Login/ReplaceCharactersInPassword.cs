using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MartUI.Login
{
    // https://stackoverflow.com/questions/18971198/can-you-replace-characters-in-a-textbox-as-you-type

    // Idk about this
    public class ReplaceCharactersInPassword : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var passwordRaw = value as string;

            if (passwordRaw == null)
                return value;

            return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
