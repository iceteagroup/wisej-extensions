using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.SmoothieChart
{
	/// <summary>
	/// Determines the interpolation used to draw the <see cref="T:Wisej.Web.Ext.SmoothieChart.TimeSeries"/> line.
	/// </summary>
	public enum InterpolationType
	{
		/// <summary>
		/// Connects the points on the line using the Bezier Curve interpolation.
		/// </summary>
		Bezier,

		/// <summary>
		/// Connects the points on the line using straight lines.
		/// </summary>
		Linear,

		/// <summary>
		/// Connects the points on the line using a quadratic stepped connector line.
		/// </summary>
		Step
	}
}
