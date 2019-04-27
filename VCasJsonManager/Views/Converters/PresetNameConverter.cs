//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.Globalization;
using System.Windows.Data;
using VCasJsonManager.Models.Settings;

namespace VCasJsonManager.Views.Converters
{
    /// <summary>
    /// プリセット名のコンバータ
    /// </summary>
    public class PresetNameConverter : IValueConverter
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
            if (value == null)
            {
                return DefaltName;
            }

            if (!(value is PresetInfo info))
            {
                throw new ArgumentException(nameof(value));
            }

            return info.Name;
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
            // 使用しない
            throw new NotSupportedException();
        }

        /// <summary>
        /// プリセット未選択時のデフォルト名
        /// </summary>
        public string DefaltName { get; set; }

    }
}
