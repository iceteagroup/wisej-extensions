///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using Wisej.Base;

namespace Wisej.Ext.ARIA
{
	/// <summary>
	/// Represents the set of ARIA properties associated to a control.
	/// </summary>
	public class WebARIA
	{
		bool Hidden { get; set; }

		bool Required { get; set; }

		bool ReadOnly { get; set; }

		TriState Selected { get; set; }

		TriState Expanded { get; set; }

		ControlBase LabeledBy { get; set; }

		ControlBase DescribedBy { get; set; }

		int ValueNow { get; set; }

		int ValueMin { get; set; }

		int ValueMax { get; set; }

		string Label { get; set; }

		string ValueText { get; set; }

		Invalid Invalid { get; set; }
	}

	/// <summary>
	/// The aria-invalid attribute is used to indicate that the value entered into an input field does not conform to the format expected by the application.
	/// This may include formats such as email addresses or telephone numbers. aria-invalid can also be used to indicate that a required field has not been filled in.
	/// The attribute should be programmatically set as a result of a validation process.
	/// </summary>
	public enum Invalid
	{
		/// <summary>
		/// Default. No errors detected.
		/// </summary>
		@false,

		/// <summary>
		/// The value has failed validation.
		/// </summary>
		@true,

		/// <summary>
		/// A grammatical error has been detected.
		/// </summary>
		grammar,

		/// <summary>
		/// A spelling error has been detected.
		/// </summary>
		spelling,
	}

	/// <summary>
	/// Represents a tristate value for a boolean ARIA property.
	/// </summary>
	public enum TriState
	{
		/// <summary>
		/// Default. The element state is unknown.
		/// </summary>
		undefined = -1,

		/// <summary>
		/// False.
		/// </summary>
		@false = 0,

		/// <summary>
		/// True.
		/// </summary>
		@true = 1
	}

}
