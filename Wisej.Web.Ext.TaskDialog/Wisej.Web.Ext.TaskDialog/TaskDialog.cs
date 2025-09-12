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
	/// 
	///              A task dialog allows to display information and get simple input from the user. It is similar
	///              to a <see cref="MessageBox" /> (in that it is formatted by the operating system) but provides
	///              a lot more features.
	///            
	///</summary>
	public partial class TaskDialog : Wisej.Web.Component, IWisejControl
	{

		#region Properties
		/// <summary>
		/// 
		///              Gets the window handle of the task dialog window, or <see cref="F:System.IntPtr.Zero" />
		///              if the dialog is currently not being shown.
		///            
		///</summary>
		public virtual System.IntPtr Handle
		{
			get
			{
				return this._handle;
			}
		}

		public Rectangle Bounds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public string Name => throw new NotImplementedException();

		public bool Visible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public bool Created => throw new NotImplementedException();

		public IWisejControl Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public Size Size { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public ICollection Children => throw new NotImplementedException();

		public int DesignerTimeout => throw new NotImplementedException();

		public string AppearanceKey => throw new NotImplementedException();

		public ClientTheme Theme => throw new NotImplementedException();

		public ClientPlatform Platform => throw new NotImplementedException();

		private System.IntPtr _handle;
		#endregion

		#region Methods
		/// <summary>
		/// 
		///              Shows the task dialog.
		///            
		///</summary>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="page" /> is <see langword="null" />.
		///            </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///            The specified <paramref name="page" /> contains an invalid configuration.
		///            </exception>
		/// <returns>
		///              The <see cref="TaskDialogButton" /> which was clicked by the user to close the dialog.
		///            </returns>
		/// <param name="page">
		///              The page instance that contains the contents which this task dialog will display.
		///            </param>
		/// <param name="startupLocation">
		///              Gets or sets the position of the task dialog when it is shown.
		///            </param>
		public TaskDialogButton ShowDialog(TaskDialogPage page, TaskDialogStartupLocation startupLocation)
		{
			// TODO: Implement
			return new TaskDialogButton();
		}

		/// <summary>
		/// 
		///              Shows the task dialog with the specified owner.
		///            
		///</summary>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="owner" /> is <see langword="null" />
		///              - or -
		///              <paramref name="page" /> is <see langword="null" />.
		///            </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///            The specified <paramref name="page" /> contains an invalid configuration.
		///            </exception>
		/// <returns>
		///              The <see cref="TaskDialogButton" /> which was clicked by the user to close the dialog.
		///            </returns>
		/// <param name="page">
		///              The page instance that contains the contents which this task dialog will display.
		///            </param>
		/// <param name="owner">The owner window, or <see langword="null" /> to show a modeless dialog.</param>
		/// <param name="startupLocation">
		///              Gets or sets the position of the task dialog when it is shown.
		///            </param>
		public TaskDialogButton ShowDialog(IWisejControl owner, TaskDialogPage page, TaskDialogStartupLocation startupLocation)
		{
			// TODO: Implement
			return new TaskDialogButton();
		}


		/// <summary>
		/// 
		///              Closes the shown task dialog with <see cref="P:System.Windows.Forms.TaskDialogButton.Cancel" /> as resulting button.
		///            
		///</summary>
		public void Close()
		{
			// TODO: Implement
		}

		public bool Focus()
		{
			throw new NotImplementedException();
		}

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
