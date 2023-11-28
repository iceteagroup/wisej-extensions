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
	///              Contains constants for predefined icons of a task dialog.
	///            
	///</summary>
	public enum TaskDialogStandardIcon
	{

		/// <summary>
		/// 
		///              The task dialog does not display an icon.
		///            
		///</summary>
		None = 0,

		/// <summary>
		/// 
		///              The task dialog contains a symbol consisting of a lowercase letter "i" in a circle.
		///            
		///</summary>
		Information = 65533,

		/// <summary>
		/// 
		///              The task dialog contains an icon consisting of an exclamation point in a triangle with a yellow background.
		///            
		///</summary>
		Warning = 65535,

		/// <summary>
		/// 
		///              The task dialog contains an icon consisting of white "x" in a circle with a red background.
		///            
		///</summary>
		Error = 65534,

		/// <summary>
		/// 
		///              The task dialog contains an icon consisting of an user account control (UAC) shield.
		///            
		///</summary>
		Shield = 65532,

		/// <summary>
		/// 
		///              The task dialog contains an icon consisting of an user account control (UAC) shield and shows a blue bar around the icon.
		///            
		///</summary>
		ShieldBlueBar = 65531,

		/// <summary>
		/// 
		///              The task dialog contains an icon consisting of an user account control (UAC) shield and shows a gray bar around the icon.
		///            
		///</summary>
		ShieldGrayBar = 65527,

		/// <summary>
		/// 
		///              The task dialog contains an icon consisting of an exclamation point in a yellow shield and shows a yellow bar around the icon.
		///            
		///</summary>
		ShieldWarningYellowBar = 65530,

		/// <summary>
		/// 
		///              The task dialog contains an icon consisting of white "x" in a red shield and shows a red bar around the icon.
		///            
		///</summary>
		ShieldErrorRedBar = 65529,

		/// <summary>
		/// 
		///              The task dialog contains an icon consisting of white tick in a green shield and shows a green bar around the icon.
		///            
		///</summary>
		ShieldSuccessGreenBar = 65528,
	}

}
