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
	///              Specifies where the expanded area of the
	///              task dialog is to be displayed.
	///            
	///</summary>
	public enum TaskDialogExpanderPosition
	{

		/// <summary>
		/// 
		///              The expanded area is to be displayed immediately after the
		///              dialog's text.
		///            
		///</summary>
		AfterText,

		/// <summary>
		/// 
		///              The expanded area is to be displayed at the bottom of the dialog's
		///              footnote area.
		///            
		///</summary>
		AfterFootnote,
	}

}
