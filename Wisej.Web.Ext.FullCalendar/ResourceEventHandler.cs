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
using System.ComponentModel;

namespace Wisej.Web.Ext.FullCalendar
{
	/// <summary>
	/// Represents the method that will handle the <see cref="Wisej.Web.Ext.FullCalendar.FullCalendar.ResourceChanged" /> event.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="Wisej.Web.Ext.FullCalendar.ResourceEventArgs" /> that contains the event data. </param>
	public delegate void ResourceEventHandler(object sender, ResourceEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="Wisej.Web.Ext.FullCalendar.FullCalendar.ResourceChanged" /> event.
	/// </summary>
	[ApiCategory("FullCalendar")]
	public class ResourceEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of <see cref="Wisej.Web.Ext.FullCalendar.ResourceEventArgs"/>.
		/// </summary>
		/// <param name="resource">The <see cref="Wisej.Web.Ext.FullCalendar.FCResource"/> that has changed.</param>
		public ResourceEventArgs(FCResource resource)
		{
			this.Resource = resource;
		}

		/// <summary>
		/// Returns the <see cref="Wisej.Web.Ext.FullCalendar.FCResource"/> that has changed.
		/// </summary>
		public FCResource Resource
		{
			get;
			private set;
		}
	}
}
