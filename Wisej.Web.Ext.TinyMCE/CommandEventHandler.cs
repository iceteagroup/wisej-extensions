///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// 
//
// ALL INFORMATION CONTAINED HEREIN IS, AND REMAINS
// THE PROPERTY OF ICE TEA GROUP LLC AND ITS SUPPLIERS, IF ANY.
// THE INTELLECTUAL PROPERTY AND TECHNICAL CONCEPTS CONTAINED
// HEREIN ARE PROPRIETARY TO ICE TEA GROUP LLC AND ITS SUPPLIERS
// AND MAY BE COVERED BY U.S. AND FOREIGN PATENTS, PATENT IN PROCESS, AND
// ARE PROTECTED BY TRADE SECRET OR COPYRIGHT LAW.
//
// DISSEMINATION OF THIS INFORMATION OR REPRODUCTION OF THIS MATERIAL
// IS STRICTLY FORBIDDEN UNLESS PRIOR WRITTEN PERMISSION IS OBTAINED
// FROM ICE TEA GROUP LLC.
//
///////////////////////////////////////////////////////////////////////////////

using System;


namespace Wisej.Web.Ext.TinyMCE
{
	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.TinyMCE.TinyMCE.Command"/> event of 
	/// a <see cref="T:Wisej.Web.Ext.TinyMCE.TinyMCE" /> control.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.TinyMCE.CommandEventArgs" /> that contains the event data. </param>
	public delegate void CommandEventHandler(object sender, CommandEventArgs e);


	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.TinyMCE.TinyMCE.Command" /> event.
	/// </summary>
	public class CommandEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes an instance of the <see cref="T:Wisej.Web.Ext.TinyMCE.CommandEventArgs" /> class.
		/// </summary>
		/// <param name="command">The event data from the client.</param>
		public CommandEventArgs(string command)
		{
			this.Command = command;
		}

		/// <summary>
		/// Returns the name of the command that was executed by the <see cref="T:Wisej.Web.Ext.TinyMCE.TinyMCE" /> control.
		/// </summary>
		public string Command
		{
			get;
			private set;
		}
	}
}
