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
using Wisej.Core;

namespace Wisej.Web.Ext.TaskDialog
{
	/// <summary>
	/// 
	///              Represents a progress bar control of a task dialog.
	///            
	///</summary>
	public class TaskDialogProgressBar : TaskDialogControl
	{

		#region Constructors
		/// <summary>
		/// 
		///              Initializes a new instance of the <see cref="TaskDialogProgressBar" /> class.
		///            
		///</summary>
		public TaskDialogProgressBar()
		{
			// TODO: Implement
		}

		/// <summary>
		/// 
		///              Initializes a new instance of the <see cref="TaskDialogProgressBar" /> class
		///              using the given <paramref name="state" />.
		///            
		///</summary>
		/// <param name="state">The state of the progress bar.</param>
		public TaskDialogProgressBar(TaskDialogProgressBarState state)
		{
			this._state = state;
			// TODO: Implement
		}
		#endregion

		#region Properties
		/// <summary>
		/// 
		///              Gets or sets the state of the progress bar.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set on a progress bar instance that is currently bound to a task dialog, but the value
		///              to be set is <see cref="F:System.Windows.Forms.TaskDialogProgressBarState.None" />.
		///              - or -
		///              The property is set on a progress bar instance that is currently bound to a task dialog, but it's not visible as its initial
		///              <see cref="P:System.Windows.Forms.TaskDialogProgressBar.State" /> property value was <see cref="F:System.Windows.Forms.TaskDialogProgressBarState.None" />.
		///              - or -
		///              The property is set on a progress bar instance that is currently bound to a task dialog, but the dialog
		///              has just started navigating to a different page.
		///            </exception>
		public TaskDialogProgressBarState State
		{
			get
			{
				return this._state;
			}
			set
			{
				if ((this._state != value))
				{
					this._state = value;
				}
			}
		}

		private TaskDialogProgressBarState _state;

		/// <summary>
		/// 
		///              Gets or sets the minimum value of the range of the control.
		///            
		///</summary>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///              The value is less than 0 or greater than <see cref="F:System.UInt16.MaxValue" />.
		///            </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set on a progress bar instance that is currently bound to a task dialog, but it's not visible as its initial
		///              <see cref="P:System.Windows.Forms.TaskDialogProgressBar.State" /> property value was <see cref="F:System.Windows.Forms.TaskDialogProgressBarState.None" />.
		///              - or -
		///              The property is set on a progress bar instance that is currently bound to a task dialog, but the dialog
		///              has just started navigating to a different page.
		///            </exception>
		public int Minimum
		{
			get
			{
				return this._minimum;
			}
			set
			{
				if ((this._minimum != value))
				{
					this._minimum = value;
				}
			}
		}

		private int _minimum;

		/// <summary>
		/// 
		///              Gets or sets the maximum value of the range of the control.
		///            
		///</summary>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///              The value is less than 0 or greater than <see cref="F:System.UInt16.MaxValue" />.
		///            </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set on a progress bar instance that is currently bound to a task dialog, but it's not visible as its initial
		///              <see cref="P:System.Windows.Forms.TaskDialogProgressBar.State" /> property value was <see cref="F:System.Windows.Forms.TaskDialogProgressBarState.None" />.
		///              - or -
		///              The property is set on a progress bar instance that is currently bound to a task dialog, but the dialog
		///              has just started navigating to a different page.
		///            </exception>
		public int Maximum
		{
			get
			{
				return this._maximum;
			}
			set
			{
				if ((this._maximum != value))
				{
					this._maximum = value;
				}
			}
		}

		private int _maximum;

		/// <summary>
		/// 
		///              Gets or sets the current position of the progress bar.
		///            
		///</summary>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///              The value is less than 0 or greater than <see cref="F:System.UInt16.MaxValue" />.
		///            </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set on a progress bar instance that is currently bound to a task dialog, but it's not visible as its initial
		///              <see cref="P:System.Windows.Forms.TaskDialogProgressBar.State" /> property value was <see cref="F:System.Windows.Forms.TaskDialogProgressBarState.None" />.
		///              - or -
		///              The property is set on a progress bar instance that is currently bound to a task dialog, but the dialog
		///              has just started navigating to a different page.
		///            </exception>
		public int Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if ((this._value != value))
				{
					this._value = value;
				}
			}
		}

		private int _value;

		/// <summary>
		/// 
		///              Gets or sets the speed of the marquee display of a progress bar.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set on a progress bar instance that is currently bound to a task dialog, but it's not visible as its initial
		///              <see cref="P:System.Windows.Forms.TaskDialogProgressBar.State" /> property value was <see cref="F:System.Windows.Forms.TaskDialogProgressBarState.None" />.
		///              - or -
		///              The property is set on a progress bar instance that is currently bound to a task dialog, but the dialog
		///              has just started navigating to a different page.
		///            </exception>
		public int MarqueeSpeed
		{
			get
			{
				return this._marqueeSpeed;
			}
			set
			{
				if ((this._marqueeSpeed != value))
				{
					this._marqueeSpeed = value;
				}
			}
		}

		private int _marqueeSpeed;
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
