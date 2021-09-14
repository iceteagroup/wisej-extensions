///////////////////////////////////////////////////////////////////////////////
//
// (C) 2017 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.TourPanel
{
	/// <summary>
	/// Provides data for <see cref="E:Wisej.Web.Ext.TourPanel.TourPanelEventHandler" /> event.
	/// </summary>
	[ApiCategory("TourPanel")]
	public class TourPanelEventArgs : CancelEventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.TourPanel.TourPanelEventArgs" /> class. 
		/// </summary>
		/// <param name="step">The <see cref="T:Wisej.Web.Ext.TourPanel.TourStep" /> the event is occurring for.</param>
		/// <param name="index">The index of the <see cref="T:Wisej.Web.Ext.TourPanel.TourStep" /> the event is occurring for.</param>
		public TourPanelEventArgs(TourStep step, int index)
		{
			if (step == null)
				throw new ArgumentNullException(nameof(step));

			this.Step = step;
			this.StepIndex = index;
		}

		/// <summary>
		/// The <see cref="T:Wisej.Web.Ext.TourPanel.TourStep" /> the event is occurring for.
		/// </summary>
		public TourStep Step
		{
			get;
			private set;
		}

		/// <summary>
		/// The index of the <see cref="T:Wisej.Web.Ext.TourPanel.TourStep" /> the event is occurring for.
		/// </summary>
		public int StepIndex
		{
			get;
			private set;
		}
	}
}