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
	///            Specifies identifiers to indicate the return value of a task dialog.
	///            
	///</summary>
	public enum TaskDialogResult
	{

		/// <summary>
		/// 
		///             No button was selected.
		///            
		///</summary>
		None,

		/// <summary>
		/// 
		///             The <c>OK</c> button was selected.
		///            
		///</summary>
		OK,

		/// <summary>
		/// 
		///             The <c>Cancel</c> button was selected.
		///            
		///</summary>
		Cancel,

		/// <summary>
		/// 
		///             The <c>Abort</c> button was selected.
		///            
		///</summary>
		Abort,

		/// <summary>
		/// 
		///             The <c>Retry</c> button was selected.
		///            
		///</summary>
		Retry,

		/// <summary>
		/// 
		///             The <c>Ignore</c> button was selected.
		///            
		///</summary>
		Ignore,

		/// <summary>
		/// 
		///             The <c>Yes</c> button was selected.
		///            
		///</summary>
		Yes,

		/// <summary>
		/// 
		///             The <c>No</c> button was selected.
		///            
		///</summary>
		No,

		/// <summary>
		/// 
		///             The <c>Close</c> button was selected.
		///            
		///</summary>
		Close,

		/// <summary>
		/// 
		///             The <c>Help</c> button was selected.
		///            
		///</summary>
		Help,

		/// <summary>
		/// 
		///             The <c>Try Again</c> button was selected.
		///            
		///</summary>
		TryAgain,

		/// <summary>
		/// 
		///             The <c>Continue</c> button was selected.
		///            
		///</summary>
		Continue,
	}

}
