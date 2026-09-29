using Wisej.Web;

namespace Wisej.Ext.FuzzySort
{
	/// <summary>
	/// Provides extension methods that add fuzzy search to Wisej.NET controls.
	/// </summary>
	public static class FuzzySort
	{
		/// <summary>
		/// Enables fuzzy matching on the specified <see cref="ComboBox"/>.
		/// </summary>
		/// <param name="control">The <see cref="ComboBox"/> to enhance with fuzzy search.</param>
		/// <returns>The same <paramref name="control"/> instance, allowing calls to be chained.</returns>
		/// <remarks>
		/// This method sets <see cref="ComboBox.AutoCompleteMode"/> to <see cref="AutoCompleteMode.Filter"/>,
		/// pushes the change to the client, and then starts the client-side fuzzy search on the control.
		/// As the user types, the drop-down items are filtered and matched with the fuzzysort library, and
		/// the matching characters in each item are highlighted.
		/// <para>
		/// Call this method after the control has been created. Setting <see cref="ComboBox.AutoCompleteMode"/>
		/// to a different value afterwards turns off the filtering behavior.
		/// </para>
		/// </remarks>
		/// <example>
		/// The following example fills a <see cref="ComboBox"/> with items and enables fuzzy search on it:
		/// <code><![CDATA[
		/// using Wisej.Web;
		/// using Wisej.Ext.FuzzySort;
		///
		/// var comboBox = new ComboBox();
		/// comboBox.Items.AddRange(new object[] { "Apple", "Apricot", "Banana", "Blueberry", "Cherry" });
		/// comboBox.ApplyFuzzySort();
		///
		/// this.Controls.Add(comboBox);
		/// ]]></code>
		/// </example>
		public static ComboBox ApplyFuzzySort(this ComboBox control)
		{
			control.AutoCompleteMode = AutoCompleteMode.Filter;

			Application.Update(control);

			control.Eval("wisej.ext.FuzzySort.initialize(this);");

			return control;
		}
	}
}
