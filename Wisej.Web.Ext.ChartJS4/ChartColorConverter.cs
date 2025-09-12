using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace Wisej.Web.Ext.ChartJS4
{
	internal class ChartColorConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(Color) || base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string str)
			{
				if (string.IsNullOrEmpty(str))
					return string.Empty;

				return str;
			}

			if (value is Color color)
				return color;

			if (value is null)
				return null;

			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}


