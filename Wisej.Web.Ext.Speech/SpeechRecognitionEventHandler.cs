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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Wisej.Core;

namespace Wisej.Web.Ext.Speech
{
	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.Speech.SpeechRecognition.Result"/> event.
	///</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognitionEventArgs" /> that contains the event data. </param>
	public delegate void SpeechRecognitionEventHandler(object sender, SpeechRecognitionEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.Speech.SpeechRecognition.Result" /> and
	/// <see cref="E:Wisej.Web.Ext.Speech.SpeechRecognition.Error" /> events.
	///</summary>
	[ApiCategory("Speech")]
	public class SpeechRecognitionEventArgs : EventArgs
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognitionEventArgs" /> class.
		///</summary>
		/// <param name="results">The speech recognition results received from the client: an array of dynamic objects with the
		/// <c>isFinal</c>, <c>confidence</c> and <c>transcript</c> fields, or null.</param>
		/// <param name="error">The speech recognition error message, or null.</param>
		public SpeechRecognitionEventArgs(dynamic results, string error)
		{
			dynamic[] array = results as dynamic[];
			if (array != null)
			{
				List<SpeechRecognitionResult> list = new List<SpeechRecognitionResult>();
				foreach (dynamic result in array)
				{
					list.Add(new SpeechRecognitionResult() {

						IsFinal = result.isFinal ?? false,
						Confidence = result.confidence ?? 0d,
						Transcript = result.transcript ?? string.Empty,
					});
				}
				this.Results = list.ToArray();
			}
			else
			{
				this.Results = new SpeechRecognitionResult[0];
			}

			this.Error = error;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the list of speech recognition results.
		/// </summary>
		/// <remarks>
		/// The array contains all the alternatives (up to <see cref="SpeechRecognition.MaxAlternatives"/>) of the results received,
		/// including interim results when <see cref="SpeechRecognition.InterimResults"/> is true. It's empty for the
		/// <see cref="E:Wisej.Web.Ext.Speech.SpeechRecognition.Error"/> event.
		/// </remarks>
		/// <example>
		/// Using the final result with the highest confidence:
		/// <code><![CDATA[
		/// private void speechRecognition1_Result(object sender, SpeechRecognitionEventArgs e)
		/// {
		///     var best = e.Results
		///         .Where(r => r.IsFinal)
		///         .OrderByDescending(r => r.Confidence)
		///         .FirstOrDefault();
		/// 
		///     if (best != null)
		///         this.labelCommand.Text = best.Transcript;
		/// }
		/// ]]></code>
		/// </example>
		public SpeechRecognitionResult[] Results { get; private set; }

		/// <summary>
		/// Returns the error message from the speech recognition object.
		///</summary>
		/// <remarks>
		/// The value is the error code reported by the browser, i.e. "no-speech", "aborted", "audio-capture", "network",
		/// "not-allowed" or "language-not-supported". It's null for the <see cref="E:Wisej.Web.Ext.Speech.SpeechRecognition.Result"/> event.
		/// </remarks>
		public string Error { get; private set; }

		#endregion

	}
}
