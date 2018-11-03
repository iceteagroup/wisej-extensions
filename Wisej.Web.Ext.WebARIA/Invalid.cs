///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.WebARIA
{
	/// <summary>
	/// The aria-invalid attribute is used to indicate that the value entered into an input field does not conform to the format expected by the application.
	/// This may include formats such as email addresses or telephone numbers. aria-invalid can also be used to indicate that a required field has not been filled in.
	/// The attribute should be programmatically set as a result of a validation process.
	/// </summary>
	public enum Invalid
	{
		/// <summary>
		/// Not specified.
		/// </summary>
		NotSet,

		/// <summary>
		/// Default. No errors detected.
		/// </summary>
		False,

		/// <summary>
		/// The value has failed validation.
		/// </summary>
		True,

		/// <summary>
		/// A grammatical error has been detected.
		/// </summary>
		Grammar,

		/// <summary>
		/// A spelling error has been detected.
		/// </summary>
		Spelling,
	}
}
