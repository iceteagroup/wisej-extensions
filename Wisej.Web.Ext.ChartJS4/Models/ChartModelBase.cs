///////////////////////////////////////////////////////////////////////////////
//
// (C) 2021 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Wisej.Web.Ext.ChartJS4.Models
{
	/// <summary>
	/// Base class for chart models that need a back-reference to the owning chart.
	/// </summary>
	public abstract class ChartModelBase
	{
		private ChartJS4? _chart;

		/// <summary>
		/// Gets or sets the owning chart instance.
		/// </summary>
		[Browsable(false)]
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public ChartJS4? Chart
		{
			get => _chart;
			set
			{
				if (ReferenceEquals(_chart, value))
					return;

				_chart = value;
				OnChartChanged();
			}
		}

		/// <summary>
		/// Triggers a chart refresh.
		/// </summary>
		protected void Update()
		{
			Chart?.Update();
		}

		/// <summary>
		/// Assigns the field value and triggers chart updates when changed.
		/// </summary>
		protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(field, value))
				return false;

			if (field is ChartModelBase oldChild && oldChild.Chart == Chart)
				oldChild.Chart = null;

			field = value;

			if (value is ChartModelBase newChild)
				newChild.Chart = Chart;

			Update();
			return true;
		}

		/// <summary>
		/// Called when the <see cref="Chart"/> reference changes.
		/// </summary>
		protected virtual void OnChartChanged()
		{
		}
	}
}
