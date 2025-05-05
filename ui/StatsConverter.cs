using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace ui;

public class StatsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Dictionary<string, int> stats && stats != null)
        {
            var buffs = new List<string>();
            foreach (var stat in stats)
            {
                buffs.Add($"{stat.Key} +{stat.Value}");
            }
            return buffs.Count > 0 ? string.Join(", ", buffs) : "No Buffs";
        }
        return "No Buffs";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}