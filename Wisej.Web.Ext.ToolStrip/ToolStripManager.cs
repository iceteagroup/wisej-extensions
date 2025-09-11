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

namespace Wisej.Web.Ext.ToolStrip
{	
	/// <summary>
	/// Controls <see cref="ToolStrip" /> rendering and rafting, and the merging of <see cref="MenuStrip" />, <see cref="ToolStripDropDownMenu" />, and <see cref="ToolStripMenuItem" /> objects. This class cannot be inherited.
	///</summary>
	public class ToolStripManager
	{
		#region Methods

		/// <summary>
		/// Finds the specified <see cref="ToolStrip" /> or a type derived from <see cref="ToolStrip" />.
		///</summary>
		/// <returns>The <see cref="ToolStrip" /> or one of its derived types as specified by the <paramref name="toolStripName" /> parameter, or null if the <see cref="ToolStrip" /> is not found.</returns>
		/// <param name="toolStripName">A string specifying the name of the <see cref="ToolStrip" /> or derived <see cref="ToolStrip" /> type to find.</param>
		public ToolStrip FindToolStrip(string toolStripName)
		{
			// TODO: Implement
			return new ToolStrip();
		}

		/// <summary>
		/// Loads settings for the given <see cref="Form" /> using the full name of the <see cref="Form" /> as the settings key.
		///</summary>
		/// <exception cref="System.ArgumentNullException">The <paramref name="targetForm" /> parameter is null.</exception>
		/// <param name="targetForm">The <see cref="Form" /> whose name is also the settings key.</param>
		public void LoadSettings(Form targetForm)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Loads settings for the specified <see cref="Form" /> using the specified settings key.
		///</summary>
		/// <exception cref="System.ArgumentNullException">The <paramref name="targetForm" /> parameter is null.</exception>
		/// <exception cref="System.ArgumentNullException">The <paramref name="key" /> parameter is null or empty.</exception>
		/// <param name="targetForm">The <see cref="Form" /> for which to load settings.</param>
		/// <param name="key">A <see cref="System.String" /> representing the settings key for this <see cref="Form" />.</param>
		public void LoadSettings(Form targetForm, string key)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Saves settings for the given <see cref="Form" /> using the full name of the <see cref="Form" /> as the settings key.
		///</summary>
		/// <exception cref="System.ArgumentNullException">The <paramref name="sourceForm" /> parameter is null.</exception>
		/// <param name="sourceForm">The <see cref="Form" /> whose name is also the settings key.</param>
		public void SaveSettings(Form sourceForm)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Saves settings for the specified <see cref="Form" /> using the specified settings key.
		///</summary>
		/// <exception cref="System.ArgumentNullException">The <paramref name="sourceForm" /> parameter is null.</exception>
		/// <exception cref="System.ArgumentNullException">The <paramref name="key" /> parameter is null or empty.</exception>
		/// <param name="sourceForm">The <see cref="Form" /> for which to save settings.</param>
		/// <param name="key">A <see cref="System.String" /> representing the settings key for this <see cref="Form" />.</param>
		public void SaveSettings(Form sourceForm, string key)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Retrieves a value indicating whether a defined shortcut key is valid.
		///</summary>
		/// <returns>true if the shortcut key is valid; otherwise, false. </returns>
		/// <param name="shortcut">The shortcut key to test for validity.</param>
		public bool IsValidShortcut(Keys shortcut)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Retrieves a value indicating whether the specified shortcut key is used by any of the <see cref="ToolStrip" /> controls of a form.
		///</summary>
		/// <returns>true if the shortcut key is used by any <see cref="ToolStrip" /> on the form; otherwise, false. </returns>
		/// <param name="shortcut">The shortcut key for which to search.</param>
		public bool IsShortcutDefined(Keys shortcut)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Combines two <see cref="ToolStrip" /> objects of different types.
		///</summary>
		/// <returns>true if the merge is successful; otherwise, false.</returns>
		/// <param name="sourceToolStrip">The <see cref="ToolStrip" /> to be combined with the <see cref="ToolStrip" /> referred to by the <paramref name="targetToolStrip" /> parameter.</param>
		/// <param name="targetToolStrip">The <see cref="ToolStrip" /> that receives the <see cref="ToolStrip" /> referred to by the <paramref name="sourceToolStrip" /> parameter.</param>
		public bool Merge(ToolStrip sourceToolStrip, ToolStrip targetToolStrip)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Combines two <see cref="ToolStrip" /> objects of the same type.
		///</summary>
		/// <exception cref="System.ArgumentNullException">The <paramref name="sourceToolStrip" /> or <paramref name="targetName" /> parameter is null.</exception>
		/// <exception cref="System.ArgumentException">The <paramref name="sourceToolStrip" /> or <paramref name="targetName" /> parameters refer to the same <see cref="ToolStrip" />.</exception>
		/// <returns>true if the merge is successful; otherwise, false. </returns>
		/// <param name="sourceToolStrip">The <see cref="ToolStrip" /> to be combined with the <see cref="ToolStrip" /> referred to by the <paramref name="targetName" /> parameter.</param>
		/// <param name="targetName">The name of the <see cref="ToolStrip" /> that receives the <see cref="ToolStrip" /> referred to by the <paramref name="sourceToolStrip" /> parameter.</param>
		public bool Merge(ToolStrip sourceToolStrip, string targetName)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Undoes a merging of two <see cref="ToolStrip" /> objects, returning the specified <see cref="ToolStrip" /> to its state before the merge and nullifying all previous merge operations.
		///</summary>
		/// <returns>true if the undoing of the merge is successful; otherwise, false. </returns>
		/// <param name="targetToolStrip">The <see cref="ToolStripItem" /> for which to undo a merge operation.</param>
		public bool RevertMerge(ToolStrip targetToolStrip)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Undoes a merging of two <see cref="ToolStrip" /> objects, returning both <see cref="ToolStrip" /> controls to their state before the merge and nullifying all previous merge operations.
		///</summary>
		/// <exception cref="System.ArgumentNullException">The <paramref name="sourceToolStrip" /> is null.</exception>
		/// <returns>true if the undoing of the merge is successful; otherwise, false.</returns>
		/// <param name="targetToolStrip">The name of the <see cref="ToolStripItem" /> for which to undo a merge operation.</param>
		/// <param name="sourceToolStrip">The <see cref="ToolStrip" /> that was merged with the <paramref name="targetToolStrip" />.</param>
		public bool RevertMerge(ToolStrip targetToolStrip, ToolStrip sourceToolStrip)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Undoes a merging of two <see cref="ToolStrip" /> objects, returning the <see cref="ToolStrip" /> with the specified name to its state before the merge and nullifying all previous merge operations.
		///</summary>
		/// <returns>true if the undoing of the merge is successful; otherwise, false. </returns>
		/// <param name="targetName">The name of the <see cref="ToolStripItem" /> for which to undo a merge operation.</param>
		public bool RevertMerge(string targetName)
		{
			// TODO: Implement
			return false;
		}

		#endregion
	}
}
