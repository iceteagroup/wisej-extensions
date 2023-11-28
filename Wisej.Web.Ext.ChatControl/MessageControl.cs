using System;
using Wisej.Web;

namespace Wisej.Web.Ext.ChatControl
{
	public partial class MessageControl : UserControl
	{
		public MessageControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Fires when the message's action is invoked.
		/// </summary>
		public event EventHandler<dynamic> ActionInvoke;

		public void SetMessageResult(dynamic data)
		{
			ActionInvoke?.Invoke(this, data);
		}
	}
}
