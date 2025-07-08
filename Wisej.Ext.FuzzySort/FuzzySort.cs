using Wisej.Web;

namespace Wisej.Ext.FuzzySort
{
	public static class FuzzySort
	{
		public static ComboBox ApplyFuzzySort(this ComboBox control)
		{
			control.AutoCompleteMode = AutoCompleteMode.Filter;

			Application.Update(control);

			control.Eval("wisej.ext.FuzzySort.initialize(this);");

			return control;
		}
	}
}
