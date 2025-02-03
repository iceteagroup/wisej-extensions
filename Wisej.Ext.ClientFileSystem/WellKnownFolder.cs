///////////////////////////////////////////////////////////////////////////////
//
// (C) 2025 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Ext.ClientFileSystem
{
	/// <summary>
	/// Indicates the starting directory for <see cref="ClientFileSystem.ShowDirectoryPicker"/> and 
	/// <see cref="ClientFileSystem.ShowSaveFilePicker"/>
	/// </summary>
	public enum WellKnownFolder
	{
		/// <summary>
		/// No initial folder.
		/// </summary>
		None,

		/// <summary>
		/// User's desktop folder.
		/// </summary>
		Desktop,

		/// <summary>
		/// Users documents folder.
		/// </summary>
		Documents,

		/// <summary>
		/// User's downloads folder.
		/// </summary>
		Downloads,

		/// <summary>
		/// User's music folder.
		/// </summary>
		Music,

		/// <summary>
		/// User's pictures folder.
		/// </summary>
		Pictures,

		/// <summary>
		/// User's videos folder.
		/// </summary>
		Videos
	}
}
