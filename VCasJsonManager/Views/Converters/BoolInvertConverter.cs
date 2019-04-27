//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.Globalization;
using System.Windows.Data;

namespace VCasJsonManager.Views.Converters
{
    /// <summary>
    /// boolの値を反転するコンバーター
    /// </summary>
    public class BoolInvertConverter : IValueConverter
    {
        /// <summary>
        /// Convert
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((value as bool?) ?? true);
        }

        /// <summary>
        /// ConvertBack
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((value as bool?) ?? true);
        }
    }
}
