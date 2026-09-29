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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.TaskDialog
{
	/// <summary>
	/// Represents a task dialog that displays information and gets simple input from the user.
	/// It is similar to a <see cref="MessageBox"/> but provides a lot more features.
	/// </summary>
	/// <remarks>
	/// The content of the dialog (heading, text, icon, buttons, radio buttons, verification checkbox,
	/// expander, footnote and progress bar) is defined by a <see cref="TaskDialogPage"/> passed to
	/// <see cref="ShowDialog(TaskDialogPage, TaskDialogStartupLocation)"/>. While the dialog is shown,
	/// the content can be replaced by calling <see cref="TaskDialogPage.Navigate"/>.
	/// </remarks>
	/// <example>
	/// Asking the user to save the changes to a document:
	/// <code><![CDATA[
	/// var page = new TaskDialogPage
	/// {
	///     Caption = "My Application",
	///     Heading = "Do you want to save the changes to Report.docx?",
	///     Text = "Your changes will be lost if you don't save them."
	/// };
	/// var save = page.Buttons.Add("Save", true, true);
	/// page.Buttons.Add("Don't Save", true, true);
	///
	/// var taskDialog = new TaskDialog();
	/// if (taskDialog.ShowDialog(page, TaskDialogStartupLocation.CenterScreen) == save)
	/// {
	///     SaveDocument();
	/// }
	/// ]]></code>
	/// </example>
	public partial class TaskDialog : Wisej.Web.Component, IWisejControl
	{

		#region Properties
		/// <summary>
		/// Returns the window handle of the task dialog window, or <see cref="F:System.IntPtr.Zero" />
		/// if the dialog is currently not being shown.
		/// </summary>
		public virtual System.IntPtr Handle
		{
			get
			{
				return this._handle;
			}
		}

		/// <summary>
		/// Returns or sets the bounds of the task dialog.
		/// </summary>
		/// <remarks>
		/// This member is part of the <see cref="IWisejControl"/> implementation and is not implemented
		/// in this version: accessing it throws a <see cref="T:System.NotImplementedException"/>.
		/// </remarks>
		public Rectangle Bounds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		/// <summary>
		/// Returns the name of the task dialog.
		/// </summary>
		/// <remarks>
		/// This member is part of the <see cref="IWisejControl"/> implementation and is not implemented
		/// in this version: accessing it throws a <see cref="T:System.NotImplementedException"/>.
		/// </remarks>
		public string Name => throw new NotImplementedException();

		/// <summary>
		/// Returns or sets a value indicating whether the task dialog is visible.
		/// </summary>
		/// <remarks>
		/// This member is part of the <see cref="IWisejControl"/> implementation and is not implemented
		/// in this version: accessing it throws a <see cref="T:System.NotImplementedException"/>.
		/// </remarks>
		public bool Visible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		/// <summary>
		/// Returns a value indicating whether the task dialog has been created.
		/// </summary>
		/// <remarks>
		/// This member is part of the <see cref="IWisejControl"/> implementation and is not implemented
		/// in this version: accessing it throws a <see cref="T:System.NotImplementedException"/>.
		/// </remarks>
		public bool Created => throw new NotImplementedException();

		/// <summary>
		/// Returns or sets the parent of the task dialog.
		/// </summary>
		/// <remarks>
		/// This member is part of the <see cref="IWisejControl"/> implementation and is not implemented
		/// in this version: accessing it throws a <see cref="T:System.NotImplementedException"/>.
		/// </remarks>
		public IWisejControl Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		/// <summary>
		/// Returns or sets the size of the task dialog.
		/// </summary>
		/// <remarks>
		/// This member is part of the <see cref="IWisejControl"/> implementation and is not implemented
		/// in this version: accessing it throws a <see cref="T:System.NotImplementedException"/>.
		/// </remarks>
		public Size Size { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		/// <summary>
		/// Returns the collection of child components of the task dialog.
		/// </summary>
		/// <remarks>
		/// This member is part of the <see cref="IWisejControl"/> implementation and is not implemented
		/// in this version: accessing it throws a <see cref="T:System.NotImplementedException"/>.
		/// </remarks>
		public ICollection Children => throw new NotImplementedException();

		/// <summary>
		/// Returns the timeout used by the designer when rendering the task dialog.
		/// </summary>
		/// <remarks>
		/// This member is part of the <see cref="IWisejControl"/> implementation and is not implemented
		/// in this version: accessing it throws a <see cref="T:System.NotImplementedException"/>.
		/// </remarks>
		public int DesignerTimeout => throw new NotImplementedException();

		/// <summary>
		/// Returns the theme appearance key of the task dialog.
		/// </summary>
		/// <remarks>
		/// This member is part of the <see cref="IWisejControl"/> implementation and is not implemented
		/// in this version: accessing it throws a <see cref="T:System.NotImplementedException"/>.
		/// </remarks>
		public string AppearanceKey => throw new NotImplementedException();

		/// <summary>
		/// Returns the theme used by the task dialog.
		/// </summary>
		/// <remarks>
		/// This member is part of the <see cref="IWisejControl"/> implementation and is not implemented
		/// in this version: accessing it throws a <see cref="T:System.NotImplementedException"/>.
		/// </remarks>
		public ClientTheme Theme => throw new NotImplementedException();

		/// <summary>
		/// Returns the platform information of the client displaying the task dialog.
		/// </summary>
		/// <remarks>
		/// This member is part of the <see cref="IWisejControl"/> implementation and is not implemented
		/// in this version: accessing it throws a <see cref="T:System.NotImplementedException"/>.
		/// </remarks>
		public ClientPlatform Platform => throw new NotImplementedException();

		private System.IntPtr _handle;
		#endregion

		#region Methods
		/// <summary>
		/// Shows the task dialog.
		/// </summary>
		/// <param name="page">
		/// The page instance that contains the contents which this task dialog will display.
		/// </param>
		/// <param name="startupLocation">
		/// The position of the task dialog when it is shown.
		/// </param>
		/// <returns>
		/// The <see cref="TaskDialogButton" /> which was clicked by the user to close the dialog.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="page" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		/// The specified <paramref name="page" /> contains an invalid configuration.
		/// </exception>
		/// <remarks>
		/// This method is not implemented yet in this version: it doesn't show the dialog and returns a new
		/// <see cref="TaskDialogButton"/> instance.
		/// </remarks>
		/// <example>
		/// Showing a confirmation dialog in the center of the screen:
		/// <code><![CDATA[
		/// var page = new TaskDialogPage
		/// {
		///     Heading = "Delete the selected customer?",
		///     Text = "This operation cannot be undone."
		/// };
		/// var delete = page.Buttons.Add("Delete", true, true);
		/// page.Buttons.Add("Keep", true, true);
		///
		/// var result = new TaskDialog().ShowDialog(page, TaskDialogStartupLocation.CenterScreen);
		/// if (result == delete)
		/// {
		///     DeleteCustomer(this.customerId);
		/// }
		/// ]]></code>
		/// </example>
		public TaskDialogButton ShowDialog(TaskDialogPage page, TaskDialogStartupLocation startupLocation)
		{
			// TODO: Implement
			return new TaskDialogButton();
		}

		/// <summary>
		/// Shows the task dialog with the specified owner.
		/// </summary>
		/// <param name="owner">The owner window, or <see langword="null" /> to show a modeless dialog.</param>
		/// <param name="page">
		/// The page instance that contains the contents which this task dialog will display.
		/// </param>
		/// <param name="startupLocation">
		/// The position of the task dialog when it is shown. Use <see cref="TaskDialogStartupLocation.CenterOwner"/>
		/// to center the dialog on <paramref name="owner"/>.
		/// </param>
		/// <returns>
		/// The <see cref="TaskDialogButton" /> which was clicked by the user to close the dialog.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="owner" /> is <see langword="null" />
		/// - or -
		/// <paramref name="page" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="T:System.InvalidOperationException">
		/// The specified <paramref name="page" /> contains an invalid configuration.
		/// </exception>
		/// <remarks>
		/// This method is not implemented yet in this version: it doesn't show the dialog and returns a new
		/// <see cref="TaskDialogButton"/> instance.
		/// </remarks>
		/// <example>
		/// Showing a task dialog centered on the current form:
		/// <code><![CDATA[
		/// private void buttonExport_Click(object sender, EventArgs e)
		/// {
		///     var page = new TaskDialogPage
		///     {
		///         Heading = "Export completed",
		///         Text = "1,250 records have been exported."
		///     };
		///     page.Buttons.Add("Close", true, true);
		///
		///     new TaskDialog().ShowDialog(this, page, TaskDialogStartupLocation.CenterOwner);
		/// }
		/// ]]></code>
		/// </example>
		public TaskDialogButton ShowDialog(IWisejControl owner, TaskDialogPage page, TaskDialogStartupLocation startupLocation)
		{
			// TODO: Implement
			return new TaskDialogButton();
		}


		/// <summary>
		/// Closes the shown task dialog with <see cref="TaskDialogButton.Cancel" /> as resulting button.
		/// </summary>
		/// <remarks>
		/// This method is not implemented yet in this version and has no effect.
		/// </remarks>
		/// <example>
		/// Closing the dialog when a long running operation completes:
		/// <code><![CDATA[
		/// private void OnImportCompleted()
		/// {
		///     this.taskDialog1.Close();
		/// }
		/// ]]></code>
		/// </example>
		public void Close()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Sets the input focus to the task dialog.
		/// </summary>
		/// <returns><see langword="true"/> if the input focus request was successful; otherwise, <see langword="false"/>.</returns>
		/// <remarks>
		/// This member is part of the <see cref="IWisejControl"/> implementation and is not implemented
		/// in this version: calling it throws a <see cref="T:System.NotImplementedException"/>.
		/// </remarks>
		/// <example>
		/// Moving the focus to the task dialog:
		/// <code><![CDATA[
		/// this.taskDialog1.Focus();
		/// ]]></code>
		/// </example>
		public bool Focus()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Sets the design metrics received from the designer.
		/// </summary>
		/// <param name="metrics">The dynamic object containing the design metrics.</param>
		/// <remarks>
		/// This member is part of the <see cref="IWisejControl"/> implementation and is not implemented
		/// in this version: calling it throws a <see cref="T:System.NotImplementedException"/>.
		/// </remarks>
		/// <example>
		/// This method is used by the designer infrastructure and is not meant to be called from application code:
		/// <code><![CDATA[
		/// dynamic metrics = new Wisej.Core.DynamicObject();
		/// this.taskDialog1.SetDesignMetrics(metrics);
		/// ]]></code>
		/// </example>
		public void SetDesignMetrics(dynamic metrics)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Wisej Implementation

		protected override void OnWebEvent(WisejEventArgs e)
		{
			base.OnWebEvent(e);
		}

		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender(config);
		}

		protected override void OnWebUpdate(dynamic config)
		{
			base.OnWebUpdate(config);
		}

		#endregion

	}

}
