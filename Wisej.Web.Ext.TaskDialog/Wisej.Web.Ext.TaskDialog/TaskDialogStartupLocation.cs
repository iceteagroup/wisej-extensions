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
	///            Specifies the position that a task dialog will be shown in when it is first opened.
	///            
	///</summary>
	public enum TaskDialogStartupLocation
	{

		/// <summary>
		/// 
		///              The startup location of the task dialog is the center of the screen.
		///            
		///</summary>
		CenterScreen,

		/// <summary>
		/// 
		///              The startup location of the task dialog is the center of the window that owns it,
		///              as specified by the <c>owner</c> parameter.
		///            
		///</summary>
		CenterOwner,
	}

}
