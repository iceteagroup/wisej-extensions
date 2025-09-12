using System.ComponentModel;
using Wisej.Web.Ext.ChartJS4;

namespace Wisej.ChartJS.Samples
{
	public class OptionsFiller : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionsFiller()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.OptionsTooltips"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		public OptionsFiller(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// When you have multiple datasets, and you're using the fill option to create areas between lines
		/// the propagate setting affects what happens if the dataset you’re trying to fill to isn’t visible.
		/// If propogate is true, Chart.js keeps looking for the next dataset to fill in and it 
		/// propogates the fill to the next avalibale dataset.
		/// If propogate is false, the fill stops if the target dataset isn't visible or fillable. 
		/// It doesn't propogate- doesn't try to find a dataset to fill to.
		/// </summary>
		[Description("Decides whether a fill should be passed down to other datasets.")]
		[DefaultValue(true)]
		public bool Propagate
		{
			get;
			set;
		} = true;

		/// <summary>
		/// TODO: Julie
		/// </summary>
		[Description("The time at which the plugin should be called. The default is beforeDatasetsDraw.")]
		[DefaultValue("beforeDatasetsDraw")]
		public string DrawTime
		{
			get;
			set;
		} = "beforeDatasetsDraw";
	}
}
