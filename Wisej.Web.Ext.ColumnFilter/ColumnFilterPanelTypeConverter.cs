///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// 
//
// ALL INFORMATION CONTAINED HEREIN IS, AND REMAINS
// THE PROPERTY OF ICE TEA GROUP LLC AND ITS SUPPLIERS, IF ANY.
// THE INTELLECTUAL PROPERTY AND TECHNICAL CONCEPTS CONTAINED
// HEREIN ARE PROPRIETARY TO ICE TEA GROUP LLC AND ITS SUPPLIERS
// AND MAY BE COVERED BY U.S. AND FOREIGN PATENTS, PATENT IN PROCESS, AND
// ARE PROTECTED BY TRADE SECRET OR COPYRIGHT LAW.
//
// DISSEMINATION OF THIS INFORMATION OR REPRODUCTION OF THIS MATERIAL
// IS STRICTLY FORBIDDEN UNLESS PRIOR WRITTEN PERMISSION IS OBTAINED
// FROM ICE TEA GROUP LLC.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;

namespace Wisej.Web.Ext.ColumnFilter
{

	/// <summary>
	/// Converter for the <see cref="ColumnFilter.FilterPanelType"/> property.
	/// Allows for the selection of the valid subclasses of <see cref="ColumnFilterPanel"/>.
	/// </summary>
	internal class ColumnFilterPanelTypeConverter : TypeConverter
	{
		private Type[] cachedTypes;

		/// <summary>
		/// Returns whether this converter can convert the specified <see cref="T:System.Type" /> of the source object using the given context.
		/// </summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">The <see cref="T:System.Type" /> of the source object.</param>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return
				sourceType == typeof(string)
				|| base.CanConvertFrom(context, sourceType);
		}

		/// <summary>
		/// Returns whether this converter can convert an object to the given destination type using the context.
		/// </summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to. </param>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return
				destinationType == typeof(Type)
				|| base.CanConvertTo(context, destinationType);
		}


		/// <summary>
		/// Converts the specified object to the native type of the converter.
		/// </summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture used to represent the font. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				if (this.cachedTypes != null)
				{
					string fullName = (string)value;
					foreach (Type t in this.cachedTypes)
					{
						if (t.FullName == fullName)
							return t;
					}
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			var panelType = typeof(ColumnFilterPanel);
			var host = (IDesignerHost)context.Container;
			var typeDiscovery = (ITypeDiscoveryService)host?.GetService(typeof(ITypeDiscoveryService));
			if (typeDiscovery != null)
			{
				var list = typeDiscovery.GetTypes(typeof(ColumnFilterPanel), false).OfType<Type>()
					.Where(t => t != panelType)
					.ToList();

				this.cachedTypes = list.ToArray();

				return new StandardValuesCollection(this.cachedTypes);
			}
			else
			{
				return new StandardValuesCollection(null);
			}
		}

	}
}