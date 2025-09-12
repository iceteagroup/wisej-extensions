using System.Drawing;

namespace Wisej.Web.Ext.ChartJS4
{
	public class OptionsBorder : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionsBorder()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.OptionsBorder"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		public OptionsBorder(OptionsBase owner)
		{
			this.Owner = owner;
		}

		public Color Color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}
		private Color _color = Color.Empty;

		private bool ShouldSerializeColor()
		{
			return this._color != Color.Empty;
		}
	}
}
