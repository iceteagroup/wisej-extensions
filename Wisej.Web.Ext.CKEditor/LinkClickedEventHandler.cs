///////////////////////////////////////////////////////////////////////////////
//
// (C) 2021 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.CKEditor
{
	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.CKEditor.CKEditor.LinkClicked"/> event of 
	/// a <see cref="T:Wisej.Web.Ext.CKEditor.CKEditor" /> control.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.CKEditor.LinkClickedEventArgs" /> that contains the event data.</param>
	public delegate void LinkClickedEventHandler(object sender, LinkClickedEventArgs e);


	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.CKEditor.CKEditor.LinkClicked" /> event.
	/// </summary>
	public class LinkClickedEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes an instance of the <see cref="T:Wisej.Web.Ext.CKEditor.LinkClickedEventArgs" /> class.
		/// </summary>
		/// <param name="link">The link clicked in the editor.</param>
		public LinkClickedEventArgs(string link)
		{
			this.Link = link;
		}

		/// <summary>
		/// Returns the link clicked on the <see cref="T:Wisej.Web.Ext.CKEditor.CKEditor" /> control.
		/// </summary>
		public string Link
		{
			get;
			private set;
		}
	}
}
