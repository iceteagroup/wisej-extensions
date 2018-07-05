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

using System;
using System.Drawing;
using Wisej.Core;

namespace Wisej.Web.Ext.Speech
{
	/// <summary>
	/// Represents an entry in the list of speech recognition results.
	///</summary>
	public class SpeechRecognitionResult
	{
		/// <summary>
		/// Returns a numeric estimate of how confident the speech recognition system is that the recognition is correct.
		/// </summary>
		public double Confidence { get; internal set; }

		/// <summary>
		/// Returns if this result is final (true) or not (false) — if so, then this is the final time this result will be returned; if not, then this result is an interim result, and may be updated later on.
		/// </summary>
		public bool IsFinal { get; internal set; }

		/// <summary>
		/// Returns a string containing the transcript of the recognised word.
		/// </summary>
		public string Transcript { get; internal set; }
	}
}
