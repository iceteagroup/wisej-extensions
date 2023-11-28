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

namespace Wisej.Web.Ext.TaskDialog
{
	/// <summary>
	/// 
	///              Contains constants that specify the state of a task dialog progress bar.
	///            
	///</summary>
	public enum TaskDialogProgressBarState
	{

		/// <summary>
		/// 
		///              Shows a regular progress bar.
		///            
		///</summary>
		Normal,

		/// <summary>
		/// 
		///              Shows a paused (yellow) progress bar.
		///            
		///</summary>
		Paused,

		/// <summary>
		/// 
		///              Shows an error (red) progress bar.
		///            
		///</summary>
		Error,

		/// <summary>
		/// 
		///              Shows a marquee progress bar.
		///            
		///</summary>
		Marquee,

		/// <summary>
		/// 
		///              Shows a marquee progress bar where the marquee animation is paused.
		///            
		///</summary>
		MarqueePaused,

		/// <summary>
		/// 
		///              The progress bar will not be displayed.
		///            
		///</summary>
		None,
	}

}
