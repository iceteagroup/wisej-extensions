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
	/// Controls the merging of <see cref="ToolStrip" />, <see cref="ToolStripDropDownMenu" /> and <see cref="ToolStripMenuItem" /> objects,
	/// and the loading and saving of the <see cref="ToolStrip"/> settings of a <see cref="Form"/>.
	/// </summary>
	/// <remarks>
	/// Unlike the WinForms <c>ToolStripManager</c>, the methods of this class are instance methods: create an instance
	/// of <see cref="ToolStripManager"/> to call them.
	/// </remarks>
	public class ToolStripManager
	{
		#region Methods

		/// <summary>
		/// Finds the specified <see cref="ToolStrip" /> or a type derived from <see cref="ToolStrip" />.
		/// </summary>
		/// <param name="toolStripName">A string specifying the name of the <see cref="ToolStrip" /> or derived <see cref="ToolStrip" /> type to find.</param>
		/// <returns>The <see cref="ToolStrip" /> or one of its derived types as specified by the <paramref name="toolStripName" /> parameter, or null if the <see cref="ToolStrip" /> is not found.</returns>
		/// <example>
		/// Finding a tool strip by name:
		/// <code><![CDATA[
		/// var manager = new ToolStripManager();
		/// var toolStrip = manager.FindToolStrip("mainToolStrip");
		/// if (toolStrip != null)
		///     toolStrip.Visible = true;
		/// ]]></code>
		/// </example>
		public ToolStrip FindToolStrip(string toolStripName)
		{
			// TODO: Implement
			return new ToolStrip();
		}

		/// <summary>
		/// Loads settings for the given <see cref="Form" /> using the full name of the <see cref="Form" /> as the settings key.
		/// </summary>
		/// <param name="targetForm">The <see cref="Form" /> whose name is also the settings key.</param>
		/// <exception cref="System.ArgumentNullException">The <paramref name="targetForm" /> parameter is null.</exception>
		/// <remarks>
		/// The settings restore the size, location, visibility and order of the <see cref="ToolStrip"/> controls on the form
		/// that were previously saved with <see cref="SaveSettings(Form)"/>.
		/// </remarks>
		/// <example>
		/// Restoring the tool strip layout when the form is loaded:
		/// <code><![CDATA[
		/// private void MainForm_Load(object sender, EventArgs e)
		/// {
		///     new ToolStripManager().LoadSettings(this);
		/// }
		/// ]]></code>
		/// </example>
		public void LoadSettings(Form targetForm)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Loads settings for the specified <see cref="Form" /> using the specified settings key.
		/// </summary>
		/// <param name="targetForm">The <see cref="Form" /> for which to load settings.</param>
		/// <param name="key">A <see cref="System.String" /> representing the settings key for this <see cref="Form" />.</param>
		/// <exception cref="System.ArgumentNullException">The <paramref name="targetForm" /> parameter is null.</exception>
		/// <exception cref="System.ArgumentNullException">The <paramref name="key" /> parameter is null or empty.</exception>
		/// <remarks>
		/// Use the same <paramref name="key"/> that was passed to <see cref="SaveSettings(Form, string)"/>.
		/// </remarks>
		/// <example>
		/// Restoring a per-user tool strip layout:
		/// <code><![CDATA[
		/// private void MainForm_Load(object sender, EventArgs e)
		/// {
		///     new ToolStripManager().LoadSettings(this, "MainForm.Layout");
		/// }
		/// ]]></code>
		/// </example>
		public void LoadSettings(Form targetForm, string key)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Saves settings for the given <see cref="Form" /> using the full name of the <see cref="Form" /> as the settings key.
		/// </summary>
		/// <param name="sourceForm">The <see cref="Form" /> whose name is also the settings key.</param>
		/// <exception cref="System.ArgumentNullException">The <paramref name="sourceForm" /> parameter is null.</exception>
		/// <example>
		/// Saving the tool strip layout when the form is closed:
		/// <code><![CDATA[
		/// private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		/// {
		///     new ToolStripManager().SaveSettings(this);
		/// }
		/// ]]></code>
		/// </example>
		public void SaveSettings(Form sourceForm)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Saves settings for the specified <see cref="Form" /> using the specified settings key.
		/// </summary>
		/// <param name="sourceForm">The <see cref="Form" /> for which to save settings.</param>
		/// <param name="key">A <see cref="System.String" /> representing the settings key for this <see cref="Form" />.</param>
		/// <exception cref="System.ArgumentNullException">The <paramref name="sourceForm" /> parameter is null.</exception>
		/// <exception cref="System.ArgumentNullException">The <paramref name="key" /> parameter is null or empty.</exception>
		/// <example>
		/// Saving a per-user tool strip layout:
		/// <code><![CDATA[
		/// private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		/// {
		///     new ToolStripManager().SaveSettings(this, "MainForm.Layout");
		/// }
		/// ]]></code>
		/// </example>
		public void SaveSettings(Form sourceForm, string key)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Returns a value indicating whether a defined shortcut key is valid.
		/// </summary>
		/// <param name="shortcut">The shortcut key to test for validity.</param>
		/// <returns>true if the shortcut key is valid; otherwise, false.</returns>
		/// <remarks>
		/// A valid shortcut is a key combined with at least one modifier (for example <c>Ctrl+S</c>), or a function key such as F1.
		/// </remarks>
		/// <example>
		/// Validating a shortcut before assigning it to a menu item:
		/// <code><![CDATA[
		/// var shortcut = Keys.Control | Keys.S;
		/// if (new ToolStripManager().IsValidShortcut(shortcut))
		///     this.menuItemSave.ShortcutKeys = shortcut;
		/// ]]></code>
		/// </example>
		public bool IsValidShortcut(Keys shortcut)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Returns a value indicating whether the specified shortcut key is used by any of the <see cref="ToolStrip" /> controls of a form.
		/// </summary>
		/// <param name="shortcut">The shortcut key for which to search.</param>
		/// <returns>true if the shortcut key is used by any <see cref="ToolStrip" /> on the form; otherwise, false.</returns>
		/// <example>
		/// Avoiding duplicate shortcuts:
		/// <code><![CDATA[
		/// var shortcut = Keys.Control | Keys.P;
		/// if (!new ToolStripManager().IsShortcutDefined(shortcut))
		///     this.menuItemPrint.ShortcutKeys = shortcut;
		/// ]]></code>
		/// </example>
		public bool IsShortcutDefined(Keys shortcut)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Combines two <see cref="ToolStrip" /> objects of different types.
		/// </summary>
		/// <param name="sourceToolStrip">The <see cref="ToolStrip" /> to be combined with the <see cref="ToolStrip" /> referred to by the <paramref name="targetToolStrip" /> parameter.</param>
		/// <param name="targetToolStrip">The <see cref="ToolStrip" /> that receives the <see cref="ToolStrip" /> referred to by the <paramref name="sourceToolStrip" /> parameter.</param>
		/// <returns>true if the merge is successful; otherwise, false.</returns>
		/// <remarks>
		/// Both tool strips must have <see cref="ToolStrip.AllowMerge"/> set to true. Each item of the source is merged according
		/// to its <see cref="ToolStripItem.MergeAction"/> and <see cref="ToolStripItem.MergeIndex"/>. Use
		/// <see cref="RevertMerge(ToolStrip, ToolStrip)"/> to undo the merge.
		/// </remarks>
		/// <example>
		/// Merging the items of a child view into the main tool strip:
		/// <code><![CDATA[
		/// this.buttonExport.MergeAction = MergeAction.Insert;
		/// this.buttonExport.MergeIndex = 2;
		///
		/// var manager = new ToolStripManager();
		/// if (!manager.Merge(this.childToolStrip, this.mainToolStrip))
		///     AlertBox.Show("Merge failed.");
		/// ]]></code>
		/// </example>
		public bool Merge(ToolStrip sourceToolStrip, ToolStrip targetToolStrip)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Combines two <see cref="ToolStrip" /> objects of the same type.
		/// </summary>
		/// <param name="sourceToolStrip">The <see cref="ToolStrip" /> to be combined with the <see cref="ToolStrip" /> referred to by the <paramref name="targetName" /> parameter.</param>
		/// <param name="targetName">The name of the <see cref="ToolStrip" /> that receives the <see cref="ToolStrip" /> referred to by the <paramref name="sourceToolStrip" /> parameter.</param>
		/// <returns>true if the merge is successful; otherwise, false.</returns>
		/// <exception cref="System.ArgumentNullException">The <paramref name="sourceToolStrip" /> or <paramref name="targetName" /> parameter is null.</exception>
		/// <exception cref="System.ArgumentException">The <paramref name="sourceToolStrip" /> or <paramref name="targetName" /> parameters refer to the same <see cref="ToolStrip" />.</exception>
		/// <remarks>
		/// The target is located by name as with <see cref="FindToolStrip(string)"/>.
		/// </remarks>
		/// <example>
		/// Merging into a tool strip identified by name:
		/// <code><![CDATA[
		/// new ToolStripManager().Merge(this.childToolStrip, "mainToolStrip");
		/// ]]></code>
		/// </example>
		public bool Merge(ToolStrip sourceToolStrip, string targetName)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Undoes a merging of two <see cref="ToolStrip" /> objects, returning the specified <see cref="ToolStrip" /> to its state before the merge and nullifying all previous merge operations.
		/// </summary>
		/// <param name="targetToolStrip">The <see cref="ToolStrip" /> for which to undo a merge operation.</param>
		/// <returns>true if the undoing of the merge is successful; otherwise, false.</returns>
		/// <example>
		/// Restoring the main tool strip when the child view is closed:
		/// <code><![CDATA[
		/// new ToolStripManager().RevertMerge(this.mainToolStrip);
		/// ]]></code>
		/// </example>
		public bool RevertMerge(ToolStrip targetToolStrip)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Undoes a merging of two <see cref="ToolStrip" /> objects, returning both <see cref="ToolStrip" /> controls to their state before the merge and nullifying all previous merge operations.
		/// </summary>
		/// <param name="targetToolStrip">The <see cref="ToolStrip" /> for which to undo a merge operation.</param>
		/// <param name="sourceToolStrip">The <see cref="ToolStrip" /> that was merged with the <paramref name="targetToolStrip" />.</param>
		/// <returns>true if the undoing of the merge is successful; otherwise, false.</returns>
		/// <exception cref="System.ArgumentNullException">The <paramref name="sourceToolStrip" /> is null.</exception>
		/// <example>
		/// Undoing the merge of a specific child tool strip:
		/// <code><![CDATA[
		/// new ToolStripManager().RevertMerge(this.mainToolStrip, this.childToolStrip);
		/// ]]></code>
		/// </example>
		public bool RevertMerge(ToolStrip targetToolStrip, ToolStrip sourceToolStrip)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Undoes a merging of two <see cref="ToolStrip" /> objects, returning the <see cref="ToolStrip" /> with the specified name to its state before the merge and nullifying all previous merge operations.
		/// </summary>
		/// <param name="targetName">The name of the <see cref="ToolStrip" /> for which to undo a merge operation.</param>
		/// <returns>true if the undoing of the merge is successful; otherwise, false.</returns>
		/// <example>
		/// Restoring a tool strip identified by name:
		/// <code><![CDATA[
		/// new ToolStripManager().RevertMerge("mainToolStrip");
		/// ]]></code>
		/// </example>
		public bool RevertMerge(string targetName)
		{
			// TODO: Implement
			return false;
		}

		#endregion
	}
}
