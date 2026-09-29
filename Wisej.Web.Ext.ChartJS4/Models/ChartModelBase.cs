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
	/// <remarks>
	/// Derived models (options and data sets) use the back-reference to refresh the owning <see cref="ChartJS4"/>
	/// control when one of their properties changes. When a property is assigned a child <see cref="ChartModelBase"/>
	/// instance, the child is automatically bound to the same chart.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var dataSet = new LineDataSet { Label = "Temperature" };
	/// chart.DataSets.Add(dataSet);
	/// // dataSet.Chart == chart: changing a property now refreshes the chart.
	/// dataSet.BorderWidth = 3;
	/// ]]></code>
	/// </example>
	public abstract class ChartModelBase
	{
		private ChartJS4? _chart;

		/// <summary>
		/// Returns or sets the owning chart instance.
		/// </summary>
		/// <value>The <see cref="ChartJS4"/> control that owns this model, or <c>null</c> if the model is not attached to a chart.</value>
		/// <remarks>
		/// The value is set automatically when the model is assigned to <see cref="ChartJS4.ChartOptions"/>, added to
		/// <see cref="ChartJS4.DataSets"/>, or assigned to a property of another attached model. It is excluded from
		/// JSON serialization and hidden from the property grid.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var owner = chart.ChartOptions.Chart; // == chart
		/// var dataSet = new BarDataSet();
		/// chart.DataSets.Add(dataSet);          // dataSet.Chart == chart
		/// ]]></code>
		/// </example>
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
