using System.ComponentModel;


namespace Wisej.Web.Ext.ChartJS4
{
	/// <summary>
	/// Represents the interaction options for the ChartJS4 control.
	/// </summary>
	public class OptionsInteraction : OptionsBase
	{
		/// <summary>
		/// Gets or sets a value indicating whether the interaction mode should intersect items.
		/// </summary>
		/// <value>
		/// <c>true</c> if the interaction mode should intersect items; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(true)]
		public bool Intersect
		{
			get
			{
				return this._intersect;
			}
			set
			{
				if (this._intersect != value)
				{
					this._intersect = value;
					Update();
				}
			}
		}
		private bool _intersect = true;

		/// <summary>
		/// Gets or sets the interaction mode.
		/// </summary>
		/// <value>
		/// The interaction mode.
		/// </value>
		[DefaultValue(InteractionMode.Nearest)]
		public InteractionMode Mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				if (this._mode != value)
				{
					this._mode = value;
					Update();
				}
			}
		}
		private InteractionMode _mode = InteractionMode.Nearest;

		/// <summary>
		/// Gets or sets the axis used for interaction.
		/// </summary>
		/// <value>
		/// The axis used for interaction.
		/// </value>
		[DefaultValue("x")]
		public string Axis
		{
			get
			{
				return this._axis;
			}
			set
			{
				if (this._axis != value)
				{
					this._axis = value;
					Update();
				}
			}
		}
		private string _axis = "x";

		/// <summary>
		/// Gets or sets a value indicating whether to include invisible points that are outside of the chart area when evaluating interactions.
		/// </summary>
		/// <value>
		/// <c>true</c> if invisible points should be included; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(false)]
		public bool IncludeVisible
		{
			get
			{
				return this._includeVisible;
			}
			set
			{
				if (this._includeVisible != value)
				{
					this._includeVisible = value;
					Update();
				}
			}
		}
		private bool _includeVisible = false;
	}

	/// <summary>
	/// Specifies the interaction modes for the ChartJS4 control.
	/// </summary>
	public enum InteractionMode
	{
		/// <summary>
		/// Interaction mode that focuses on a single point.
		/// </summary>
		Point,

		/// <summary>
		/// Interaction mode that focuses on the nearest item.
		/// </summary>
		Nearest,

		/// <summary>
		/// Interaction mode that focuses on the index of the item.
		/// </summary>
		Index,

		/// <summary>
		/// Interaction mode that focuses on the dataset.
		/// </summary>
		Dataset,

		/// <summary>
		/// Interaction mode that focuses on the x-axis.
		/// </summary>
		X,

		/// <summary>
		/// Interaction mode that focuses on the y-axis.
		/// </summary>
		Y
	}
}