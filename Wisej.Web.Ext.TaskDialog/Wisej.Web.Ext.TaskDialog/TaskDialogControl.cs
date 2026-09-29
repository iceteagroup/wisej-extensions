///////////////////////////////////////////////////////////////////////////////
//
// (C) 2023 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wisej.Web.Ext.TaskDialog
{
	/// <summary>
	/// Represents the base class of the controls that can be displayed in a <see cref="TaskDialogPage"/>.
	/// </summary>
	public abstract class TaskDialogControl : Wisej.Web.Control
	{

		#region Properties
		/// <summary>
		/// Returns or sets the object that contains data about the control.
		/// </summary>
		/// <example>
		/// Associating an application object with a task dialog control:
		/// <code><![CDATA[
		/// var footnote = new TaskDialogFootnote("Last synchronized 5 minutes ago.");
		/// footnote.Tag = this.currentAccount;
		/// ]]></code>
		/// </example>
		public object Tag
		{
			get
			{
				return this._tag;
			}
			set
			{
				if ((this._tag != value))
				{
					this._tag = value;
				}
			}
		}

		private object _tag;

		/// <summary>
		/// Returns or sets the <see cref="TaskDialogPage" /> instance which this control
		/// is currently bound to.
		/// </summary>
		/// <remarks>
		/// The value is <see langword="null"/> when the control is not part of a page that is currently
		/// displayed in a <see cref="TaskDialog"/>.
		/// </remarks>
		public TaskDialogPage BoundPage
		{
			get
			{
				return this._boundPage;
			}
			set
			{
				if ((this._boundPage != value))
				{
					this._boundPage = value;
				}
			}
		}

		private TaskDialogPage _boundPage;
		#endregion

	}

}
