using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace GIFT.QuestionBank.UI
{
    public class RightOrWrongPercentageToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                if (intValue < 0)
                {
                    return Brushes.MediumVioletRed;
                }
                else if (intValue == 0)
                {
                    return Brushes.LightSlateGray;
                }
                else if (intValue > 0)
                {
                    return Brushes.LightSeaGreen;
                }
            }

            return Brushes.DodgerBlue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
