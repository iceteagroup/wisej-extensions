using System.ComponentModel;

namespace Wisej.Web.Ext.ChartJS4
{
	public class OptionsLayout : OptionsBase
	{
		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.OptionsLayout"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		public OptionsLayout(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Apply automatic padding so visible elements are completely drawn.
		/// </summary>
		[DefaultValue(true)]
		public bool AutoPadding
		{
			get;
			set;
		}

		/// <summary>
		/// The padding to add inside the chart.
		/// </summary>
		[DefaultValue(0)]
		public Padding Padding
		{
			get;
			set;
		} = new Padding(0);
	}
}
