using System;
using System.Drawing;
using Wisej.Base;

namespace Wisej.Web.Ext.ChatControl.Messages
{
	public class ClientAutoSizeLabel : Label
	{
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);

			Resize();
		}

		public override Size MaximumSize 
		{ 
			get => base.MaximumSize;
			set
			{
				base.MaximumSize = value;

				Resize();
			}
		}

		private void Resize()
		{
			// if the text actually contains HTML, perform client-side auto-sizing.
			if (this.Text.Contains("<"))
			{
				// first measure the string if it is allowed to grow horizontally freely.
				TextUtils.MeasureText(this.Text, true, this.Font, 0, (s) =>
				{
					if (s.Width < this.MaximumSize.Width)
					{
						this.Size = s;
					}
					else
					{
						// measure again by applying the maximum width:
						TextUtils.MeasureText(this.Text, true, this.Font, this.MaximumSize.Width, (r) =>
						{
							this.Size = r;
						});
					}
	
				});
			}
			else
			{
				this.Size = this.PreferredSize;
			}
		}
	}
}
