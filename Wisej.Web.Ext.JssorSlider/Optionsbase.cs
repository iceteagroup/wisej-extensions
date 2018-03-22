using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.JssorSlider
{
	public abstract class OptionsBase
	{
		protected const int STATE_WRAP = 0x0001;
		protected const int STATE_VISIBLE = 0x0002;
		protected const int STATE_AUTOCENTER = 0x0004;
		protected const int STATE_AUTOCENTERV = 0x0008;
		protected const int STATE_AUTOCENTERH = 0x0010;
		protected const int STATE_SHOWONMOUSEOVER = 0x0020;

		private int state = 0;
		private JssorSlider slider;

		public OptionsBase(JssorSlider slider)
		{
			this.slider = slider;
		}

		protected internal bool GetState(int state)
		{
			return (state & state) == state;
		}

		protected internal void SetState(int state, bool on)
		{
			if (on)
				state |= state;
			else
				state &= ~state;
		}

		internal bool IsDirty
		{
			get;
			set;
		}

		protected internal void Update()
		{
		}

		internal virtual string ClassName
		{
			get { return string.Empty; }
		}


	}
}
