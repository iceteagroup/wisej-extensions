using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.JssorSlider
{
	public class ThumbnailOptions : OptionsBase
	{
		public ThumbnailOptions(JssorSlider slider) : base(slider)
		{
		}

		internal override string ClassName
		{
			get
			{
				return "$JssorThumbnailNavigator$";
			}
		}

		public bool Wrap
		{
			get { return base.GetState(STATE_WRAP); }
			set
			{
				if (this.Wrap != value)
				{
					base.SetState(STATE_WRAP, value);
					Update();
				}
			}
		}

		public int Columns
		{
			get { return this._columns; }
			set
			{
				if (this._columns != value)
				{
					this._columns = value;
					Update();
				}
			}
		}
		private int _columns = 1;

		public int Rows
		{
			get { return this._rows; }
			set
			{
				if (this._rows != value)
				{
					this._rows = value;
					Update();
				}
			}
		}
		private int _rows = 1;

		public int SpacingX
		{
			get { return this._spacingX; }
			set
			{
				if (this._spacingX != value)
				{
					this._spacingX = value;
					Update();
				}
			}
		}
		private int _spacingX = 0;

		public int SpacingY
		{
			get { return this._spacingY; }
			set
			{
				if (this._spacingY != value)
				{
					this._spacingY = value;
					Update();
				}
			}
		}
		private int _spacingY = 0;

		public Orientation Orientation
		{
			get { return this._orientation; }
			set
			{
				if (this._orientation != value)
				{
					this._orientation = value;
					Update();
				}
			}
		}
		private Orientation _orientation = Orientation.Horizontal;

		public bool Visible
		{
			get { return base.GetState(STATE_VISIBLE); }
			set
			{
				if (this.Visible != value)
				{
					base.SetState(STATE_VISIBLE, value);
					Update();
				}
			}
		}

		public bool ShowOnMouseOver
		{
			get { return base.GetState(STATE_SHOWONMOUSEOVER); }
			set
			{
				if (this.ShowOnMouseOver != value)
				{
					base.SetState(STATE_SHOWONMOUSEOVER, value);
					Update();
				}
			}
		}

		public bool AutoCenterVertically
		{
			get { return base.GetState(STATE_AUTOCENTERV); }
			set
			{
				if (this.AutoCenterVertically != value)
				{
					base.SetState(STATE_AUTOCENTERV, value);
					Update();
				}
			}
		}

		public bool AutoCenterHorizontally
		{
			get { return base.GetState(STATE_AUTOCENTERH); }
			set
			{
				if (this.AutoCenterHorizontally != value)
				{
					base.SetState(STATE_AUTOCENTERH, value);
					Update();
				}
			}
		}

	}
}
