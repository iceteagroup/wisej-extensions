using System;

namespace Wisej.Web.Ext.ChatControl
{
	public class Message
	{
		/// <summary>
		/// Gets a unique identifier for this message.
		/// </summary>
		public Guid Id 
		{ 
			get
			{
				if (this._id == null)
					this._id = Guid.NewGuid();

				return this._id;
			}
		}
		private Guid _id;

		public User User { get; set; }

		public DateTime? DateTime { get; set; }

		public MessageControl Control { get; set; }
	}
}
