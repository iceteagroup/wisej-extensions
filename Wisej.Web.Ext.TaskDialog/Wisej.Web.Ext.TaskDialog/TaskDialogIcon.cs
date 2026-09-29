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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wisej.Core;

namespace Wisej.Web.Ext.TaskDialog
{
	/// <summary>
	/// Represents an icon that can be shown in the main area of a task dialog
	/// (by setting the <see cref="TaskDialogPage.Icon" /> property) or in the
	/// footnote of a task dialog (by setting the <see cref="TaskDialogFootnote.Icon" /> property).
	/// </summary>
	/// <example>
	/// Creating icons from an image and from an icon resource:
	/// <code><![CDATA[
	/// page.Icon = new TaskDialogIcon(new Bitmap(Application.MapPath("Images/warning.png")));
	/// page.Footnote = new TaskDialogFootnote("Contact support if the problem persists.")
	/// {
	///     Icon = new TaskDialogIcon(SystemIcons.Information)
	/// };
	/// ]]></code>
	/// </example>
	public class TaskDialogIcon : Wisej.Web.PictureBox
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskDialogIcon" /> class from a
		/// <see cref="T:System.Drawing.Bitmap" /> instance.
		/// </summary>
		/// <param name="image">The <see cref="T:System.Drawing.Bitmap" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="image" /> is <see langword="null" />.</exception>
		public TaskDialogIcon(Bitmap image)
		{
			this._image = image;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskDialogIcon" /> class from an
		/// <see cref="T:System.Drawing.Icon" /> instance.
		/// </summary>
		/// <param name="icon">The <see cref="T:System.Drawing.Icon" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="icon" /> is <see langword="null" />.</exception>
		public TaskDialogIcon(Icon icon)
		{
			this._icon = icon;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskDialogIcon" /> class from an
		/// icon handle.
		/// </summary>
		/// <param name="iconHandle">A handle to an instance of an icon, or <see cref="F:System.IntPtr.Zero" /> to not show an icon.</param>
		public TaskDialogIcon(System.IntPtr iconHandle)
		{
			this._iconHandle = iconHandle;
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the icon handle (<c>HICON</c>) that is represented by this
		/// <see cref="TaskDialogIcon" /> instance.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">
		/// This <see cref="TaskDialogIcon" /> instance was not created using a
		/// constructor that takes an icon or icon handle.
		/// </exception>
		/// <remarks>
		/// The value is <see cref="F:System.IntPtr.Zero"/> unless the instance was created using the
		/// constructor that takes an icon handle.
		/// </remarks>
		public System.IntPtr IconHandle
		{
			get
			{
				return this._iconHandle;
			}
		}

		private System.IntPtr _iconHandle;
		private Bitmap _image;
		private Icon _icon;

		#endregion

		#region Methods

		/// <summary>
		/// Releases all resources used by this <see cref="TaskDialogIcon" />.
		/// </summary>
		/// <remarks>
		/// This method is not implemented yet in this version and has no effect.
		/// </remarks>
		/// <example>
		/// Releasing the icon after the dialog is closed:
		/// <code><![CDATA[
		/// var icon = new TaskDialogIcon(new Bitmap(Application.MapPath("Images/logo.png")));
		/// page.Icon = icon;
		/// new TaskDialog().ShowDialog(page, TaskDialogStartupLocation.CenterScreen);
		/// icon.Dispose();
		/// ]]></code>
		/// </example>
		public virtual void Dispose()
		{
			// TODO: Implement
		}

		#endregion

	}

}
