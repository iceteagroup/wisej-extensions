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
	/// Provides data for the <see cref="E:Wisej.Web.Ext.Speech.SpeechRecognition.Result" /> events.
	///</summary>
	public class SpeechRecognitionEventArgs : EventArgs
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognitionEventArgs" /> class.
		///</summary>
		/// <param name="results">The speech recognition results.</param>
		/// <param name="error">The speech recognition error message.</param>
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
		public SpeechRecognitionResult[] Results { get; private set; }

		/// <summary>
		/// Returns the error message from the speech recognition object.
		///</summary>
		public string Error { get; private set; }

		#endregion

	}
}
