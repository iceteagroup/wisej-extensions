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
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.Speech
{
	/// <summary>
	/// Represents a component that uses the SpeechRecognition interface of the Web Speech API to capture the browser's
	/// audio stream and convert it to text. It also extends <see cref="TextBoxBase"/> controls to enable dictation.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Speech recognition runs in the browser and requires a browser that supports the Web Speech API
	/// (i.e. Chrome, Edge, Safari), a secure (HTTPS) connection and the user's permission to use the microphone.
	/// When the browser doesn't support it, the component does nothing.
	/// </para>
	/// <para>
	/// Set <see cref="Enabled"/> to true to listen continuously (the recognition service is restarted automatically
	/// every time it ends), or call <see cref="Start"/> to listen for a single session. Recognized text is returned by
	/// the <see cref="Result"/> event and, for the extended text controls that have the focus, assigned to their
	/// <see cref="Control.Text"/> property.
	/// </para>
	/// </remarks>
	/// <example>
	/// Enabling dictation in a text box and handling the results:
	/// <code><![CDATA[
	/// var speech = new SpeechRecognition { Language = "en-US" };
	/// speech.GetSpeechRecognition(this.textBoxNotes).Enabled = true;
	/// speech.GetSpeechRecognition(this.textBoxNotes).RecognitionMode = SpeechRecognition.RecognitionMode.WhenFocused;
	/// 
	/// speech.Result += (s, e) =>
	/// {
	///     foreach (var result in e.Results)
	///         Application.Session.LastTranscript = result.Transcript;
	/// };
	/// speech.Error += (s, e) => AlertBox.Show(e.Error, MessageBoxIcon.Error);
	/// 
	/// speech.Enabled = true;
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(SpeechRecognition))]
	[ProvideProperty("SpeechRecognition", typeof(Control))]
	[Description("The SpeechRecognition interface of Web Speech API allows JavaScript to have access to a browser's audio stream and convert it to text.")]
	[ApiCategory("Speech")]
	public class SpeechRecognition : Wisej.Web.Component, IExtenderProvider
	{
		// collection of controls using the extender provider.
		private Dictionary<Control, Properties> listeners;

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognition"/> component.
		/// </summary>
		public SpeechRecognition()
		{
			this.listeners = new Dictionary<Control, Properties>();
		}
		#endregion

		#region Events

		/// <summary>
		/// Fired when the speech recognition service returns a result — a word or phrase has been positively recognized and this has been communicated back to the app (when the result event fires.)
		/// </summary>
		public event SpeechRecognitionEventHandler Result
		{
			add { base.AddHandler(nameof(Result), value); }
			remove { base.RemoveHandler(nameof(Result), value); }
		}

		/// <summary>
		/// Occurs when the speech recognition service returns a final result with no significant recognition (when the nomatch event fires.)
		/// </summary>
		public event EventHandler NoMatch
		{
			add { base.AddHandler(nameof(NoMatch), value); }
			remove { base.RemoveHandler(nameof(NoMatch), value); }
		}

		/// <summary>
		/// Occurs when a speech recognition error is detected.
		/// </summary>
		public event SpeechRecognitionEventHandler Error
		{
			add { base.AddHandler(nameof(Error), value); }
			remove { base.RemoveHandler(nameof(Error), value); }
		}

		/// <summary>
		/// Occurs when sound recognised by the speech recognition service as speech has been detected.
		/// </summary>
		public event EventHandler SpeechStart
		{
			add { base.AddHandler(nameof(SpeechStart), value); }
			remove { base.RemoveHandler(nameof(SpeechStart), value); }
		}

		/// <summary>
		/// Occurs when speech recognised by the speech recognition service has stopped being detected (when the speechend event fires.)
		/// </summary>
		public event EventHandler SpeechEnd
		{
			add { base.AddHandler(nameof(SpeechEnd), value); }
			remove { base.RemoveHandler(nameof(SpeechEnd), value); }
		}

		/// <summary>
		/// Fires the Result event.
		/// </summary>
		/// <param name="e">A <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognitionEventArgs" /> that contains the event data. </param>
		protected virtual void OnResult(SpeechRecognitionEventArgs e)
		{
			ProcessListeners(e);

			((SpeechRecognitionEventHandler)base.Events[nameof(Result)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the Error event.
		/// </summary>
		/// <param name="e">A <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognitionEventArgs" /> that contains the event data. </param>
		protected virtual void OnError(SpeechRecognitionEventArgs e)
		{
			((SpeechRecognitionEventHandler)base.Events[nameof(Error)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the NoMatch event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnNoMatch(EventArgs e)
		{
			((EventHandler)base.Events[nameof(NoMatch)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the SpeechStart event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSpeechStart(EventArgs e)
		{
			((EventHandler)base.Events[nameof(SpeechStart)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires the SpeechEnd event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSpeechEnd(EventArgs e)
		{
			((EventHandler)base.Events[nameof(SpeechEnd)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets whether continuous results are returned for each recognition, or only a single result.
		/// </summary>
		/// <remarks>
		/// When false (default), the recognition session ends after the first final result. When true, the browser keeps
		/// listening and fires a <see cref="Result"/> event for each recognized phrase until <see cref="Stop"/> is called.
		/// The value is used when the recognition session starts.
		/// </remarks>
		[DefaultValue(false)]
		[Description("Controls whether continuous results are returned for each recognition, or only a single result.")]
		public bool Continuous
		{
			get { return this._continuous; }
			set
			{
				if (this._continuous != value)
				{
					this._continuous = value;
					Update();
				}
			}
		}
		private bool _continuous = false;

		/// <summary>
		/// Returns or sets the language of the current SpeechRecognition.
		/// If not specified, this defaults to the HTML lang attribute value, or the user agent's language setting if that isn't set either.
		/// </summary>
		/// <remarks>
		/// The value is a BCP 47 language tag, i.e. "en-US", "de-DE", "it-IT". An empty string is converted to null.
		/// </remarks>
		/// <example>
		/// Recognizing the language of the current session culture:
		/// <code><![CDATA[
		/// this.speechRecognition1.Language = Application.CurrentCulture.Name;
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Gets and sets the language of the current SpeechRecognition.")]
		public string Language
		{
			get { return this._language; }
			set
			{
				if (value == string.Empty)
					value = null;

				if (this._language != value)
				{
					this._language = value;
					Update();
				}
			}
		}
		private string _language = null;

		/// <summary>
		/// Returns or sets the maximum number of alternatives provided per each speech recognition result.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">The value is less than 0 or greater than 10.</exception>
		/// <remarks>
		/// The default is 1. Each alternative is returned as a separate <see cref="SpeechRecognitionResult"/> in
		/// <see cref="SpeechRecognitionEventArgs.Results"/>, with its own <see cref="SpeechRecognitionResult.Confidence"/>.
		/// </remarks>
		[DefaultValue(1)]
		[Description("Gets and sets the volume that the utterance will be spoken at. The default is 1 (maximum).")]
		public int MaxAlternatives
		{
			get { return this._maxAlternatives; }
			set
			{
				if (value < 0 || value > 10)
					throw new ArgumentOutOfRangeException("MaxAlternatives", SR.GetString("InvalidBoundArgument", "MaxAlternatives", value, 1, 10));

				if (this._maxAlternatives != value)
				{
					this._maxAlternatives = value;
					Update();
				}
			}
		}
		private int _maxAlternatives = 1;

		/// <summary>
		/// Returns or sets whether interim results should be returned (true) or not (false.)
		/// Interim results are results that are not yet final (e.g. the <see cref="SpeechRecognitionResult.IsFinal"/> property is false.)
		/// </summary>
		/// <remarks>
		/// Interim results are delivered only to the <see cref="Result"/> event handlers; the extended text controls
		/// are updated only with final results.
		/// </remarks>
		[DefaultValue(false)]
		[Description("Controls whether interim results should be returned (true) or not (false.)")]
		public bool InterimResults
		{
			get { return this._interimResults; }
			set
			{
				if (this._interimResults != value)
				{
					this._interimResults = value;
					Update();
				}
			}
		}
		private bool _interimResults = false;

		/// <summary>
		/// Returns or sets whether the <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognition" /> component is listening.
		/// </summary>
		/// <remarks>
		/// Setting this property to true starts the recognition service and restarts it automatically every time it ends,
		/// until the property is set back to false (which calls <see cref="Stop"/> on the client). The default is false.
		/// The browser asks the user for permission to use the microphone the first time.
		/// </remarks>
		[DefaultValue(false)]
		[Description("Enables or disables the speech recognition.")]
		public bool Enabled
		{
			get { return this._enabled; }
			set
			{
				if (this._enabled != value)
				{
					this._enabled = value;
					Update();
				}
			}
		}
		private bool _enabled = false;

		/// <summary>
		/// Returns or sets a collection of grammar definitions - using the JSpeech Grammar Format (JSGF) <see href="https://www.w3.org/TR/jsgf/"/>.
		/// </summary>
		/// <remarks>
		/// Each string is a complete JSGF grammar, added to the browser's grammar list with weight 1. Grammars are only hints
		/// and many browsers ignore them.
		/// </remarks>
		/// <example>
		/// Limiting the recognition to a list of colors:
		/// <code><![CDATA[
		/// this.speechRecognition1.Grammars = new[]
		/// {
		///     "#JSGF V1.0; grammar colors; public <color> = red | green | blue | yellow ;"
		/// };
		/// ]]></code>
		/// </example>
		[Localizable(true)]
		[Description("Gets and sets a collection of grammar definitions - using the JSpeech Grammar Format (JSGF) <see href=\"https://www.w3.org/TR/jsgf/.\"/>")]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string[] Grammars
		{
			get { return this._grammars; }
			set
			{
				if (this._grammars != value)
				{
					this._grammars = value;
					Update();
				}
			}
		}
		private string[] _grammars = null;

		private bool ShouldSerializeGrammars()
		{
			return this._grammars != null && this._grammars.Length > 0;
		}

		private void ResetGrammars()
		{
			this.Grammars = null;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Starts the speech recognition service listening to incoming audio.
		/// </summary>
		/// <remarks>
		/// The call is ignored if the service is already listening. Unless <see cref="Enabled"/> is true, the session ends
		/// after the first result (or after the silence timeout) when <see cref="Continuous"/> is false.
		/// </remarks>
		/// <example>
		/// Listening while a button is pressed:
		/// <code><![CDATA[
		/// private void buttonMic_MouseDown(object sender, MouseEventArgs e)
		/// {
		///     this.speechRecognition1.Start();
		/// }
		/// 
		/// private void buttonMic_MouseUp(object sender, MouseEventArgs e)
		/// {
		///     this.speechRecognition1.Stop();
		/// }
		/// ]]></code>
		/// </example>
		public void Start()
		{
			Call("start");
		}

		/// <summary>
		/// Stops the speech recognition service from listening to incoming audio, and attempts to return a result using the audio captured so far.
		/// </summary>
		/// <remarks>
		/// If <see cref="Enabled"/> is true, the service is restarted automatically; set <see cref="Enabled"/> to false to stop listening.
		/// </remarks>
		/// <example>
		/// Stopping and processing the audio captured so far:
		/// <code><![CDATA[
		/// private void buttonDone_Click(object sender, EventArgs e)
		/// {
		///     this.speechRecognition1.Stop();
		/// }
		/// ]]></code>
		/// </example>
		public void Stop()
		{
			Call("stop");
		}

		/// <summary>
		/// Stops the speech recognition service from listening to incoming audio, and doesn't attempt to return a result.
		/// </summary>
		/// <remarks>
		/// If <see cref="Enabled"/> is true, the service is restarted automatically; set <see cref="Enabled"/> to false to stop listening.
		/// </remarks>
		/// <example>
		/// Discarding the current recognition:
		/// <code><![CDATA[
		/// private void buttonCancel_Click(object sender, EventArgs e)
		/// {
		///     this.speechRecognition1.Abort();
		/// }
		/// ]]></code>
		/// </example>
		public void Abort()
		{
			Call("abort");
		}

		/// <summary>
		/// Returns true if <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognition" /> can offer an extender property to the specified target component.
		/// </summary>
		/// <param name="target">The target object to add an extender property to. </param>
		/// <returns>true if the <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognition" /> class can offer one or more extender properties; otherwise, false.</returns>
		/// <remarks>
		/// Only controls derived from <see cref="TextBoxBase"/> can be extended.
		/// </remarks>
		/// <example>
		/// Enabling dictation in all the text boxes of a form:
		/// <code><![CDATA[
		/// foreach (Control control in this.Controls)
		/// {
		///     if (this.speechRecognition1.CanExtend(control))
		///         this.speechRecognition1.GetSpeechRecognition(control).Enabled = true;
		/// }
		/// ]]></code>
		/// </example>
		public bool CanExtend(object target)
		{
			return (target is TextBoxBase);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Clear();
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Returns the speech recognition properties associated with the specified control.
		/// </summary>
		/// <param name="control">The <see cref="T:Wisej.Web.Control" /> for which to retrieve the speech properties. </param>
		/// <returns>A <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognition.Properties" /> instance with the SpeechRecognition properties.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="control"/> is null.</exception>
		/// <remarks>
		/// The properties are created the first time they are requested for a control and are removed automatically
		/// when the control is disposed.
		/// </remarks>
		/// <example>
		/// Filling a text box with the next phrase spoken while it has the focus:
		/// <code><![CDATA[
		/// var props = this.speechRecognition1.GetSpeechRecognition(this.textBoxSearch);
		/// props.RecognitionMode = SpeechRecognition.RecognitionMode.WhenFocusedOnce;
		/// props.Enabled = true;
		/// 
		/// this.speechRecognition1.Enabled = true;
		/// ]]></code>
		/// </example>
		[DisplayName("SpeechRecognition")]
		[Description("SpeechRecognition properties")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Properties GetSpeechRecognition(Control control)
		{
			return GetSpeechRecognitionProperties(control);
		}

		private bool ShouldSerializeSpeechRecognition(Control control)
		{
			if (!HasSpeechRecognitionProperties(control))
				return false;

			Properties props = GetSpeechRecognitionProperties(control);
			return props.Enabled || props.RecognitionMode != RecognitionMode.WhenFocusedOnce;
		}

		private void ResetSpeechRecognition(Control control)
		{
			lock (this.listeners)
			{
				this.listeners.Remove(control);
				control.Disposed -= this.Control_Disposed;
			}

			Update(control);
		}

		/// <summary>
		/// Removes all speech extenders.
		/// </summary>
		/// <remarks>
		/// After this call the recognized text is no longer assigned to any control; the <see cref="Result"/> event is still fired.
		/// </remarks>
		/// <example>
		/// Disabling dictation in all controls:
		/// <code><![CDATA[
		/// this.speechRecognition1.Clear();
		/// ]]></code>
		/// </example>
		public void Clear()
		{
			lock (this.listeners)
			{
				this.listeners.ToList().ForEach((o) =>
				{
					o.Key.Disposed -= this.Control_Disposed;
				});

				this.listeners.Clear();

				Update();
			}
		}

		/// <summary>
		/// Updates the component on the client.
		/// </summary>
		private void Update(Control control)
		{
			base.Update();
		}

		/// <summary>
		/// Assigns the speech recognition properties to the control.
		/// </summary>
		/// <param name="control">The control that receives the recognized text.</param>
		/// <param name="properties">An instance of <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognition.Properties"/> defining the speech listeners.</param>
		/// <exception cref="ArgumentNullException"><paramref name="control"/> or <paramref name="properties"/> is null.</exception>
		/// <example>
		/// Assigning a new set of properties to a text box:
		/// <code><![CDATA[
		/// this.speechRecognition1.SetSpeechRecognition(this.textBoxNotes, new SpeechRecognition.Properties
		/// {
		///     Enabled = true,
		///     RecognitionMode = SpeechRecognition.RecognitionMode.WhenFocused
		/// });
		/// ]]></code>
		/// </example>
		public void SetSpeechRecognition(Control control, Properties properties)
		{
			if (control == null)
				throw new ArgumentNullException("control");
			if (properties == null)
				throw new ArgumentNullException("properties");

			lock (this.listeners)
			{
				properties.Owner = this;
				properties.Control = control;
				this.listeners[control] = properties;
			}
			Update(control);
		}

		/// <summary>
		/// Returns if the control has defined the speech recognition properties.
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		private bool HasSpeechRecognitionProperties(Control control)
		{
			if (control == null)
				throw new ArgumentNullException("control");

			lock (this.listeners)
			{
				return this.listeners.ContainsKey(control);
			}
		}

		/// <summary>
		/// Creates or retrieves the speech recognition properties associated with the control.
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		private Properties GetSpeechRecognitionProperties(Control control)
		{
			if (control == null)
				throw new ArgumentNullException("control");

			lock (this.listeners)
			{
				Properties props = null;
				if (!this.listeners.TryGetValue(control, out props))
				{
					props = new Properties(this, control);
					this.listeners.Add(control, props);

					control.Disposed -= this.Control_Disposed;
					control.Disposed += this.Control_Disposed;
				}
				return props;
			}
		}


		private void Control_Disposed(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			control.Disposed -= this.Control_Disposed;

			// remove the extender values associated with the disposed control.
			lock (this.listeners)
				this.listeners.Remove(control);
		}

		/// <summary>
		/// Assigns the speech result to one the speech enabled controls.
		/// </summary>
		/// <param name="e"></param>
		private void ProcessListeners(SpeechRecognitionEventArgs e)
		{
			lock (this.listeners)
			{
				foreach (var l in this.listeners)
				{
					if (l.Value.ProcessResults(e.Results))
						return;
				}
			}
		}

		#endregion

		#region Recognition Properties

		/// <summary>
		/// Determines how the <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognition" /> extender applies to the extended control.
		/// </summary>
		public enum RecognitionMode
		{
			/// <summary>
			/// Speech recognition is activated every time the control is focused.
			/// </summary>
			[Description("Speech recognition is activated every time the control is focused.")]
			WhenFocused,

			/// <summary>
			/// Speech recognition is activated when the control is focused and disabled after the first final recognition.
			/// </summary>
			[Description("Speech recognition is activated when the control is focused and disabled after the first final recognition.")]
			WhenFocusedOnce,
		}

		/// <summary>
		/// Represents the set of speech properties added to the extended controls.
		/// </summary>
		[TypeConverter(typeof(Properties.ExpandableObjectConverter))]
		public class Properties
		{
			private SpeechRecognition owner;
			private Control control;

			/// <summary>
			/// Creates a new instance of the speech recognition properties.
			/// </summary>
			/// <remarks>
			/// The new instance is not connected to any control until it's assigned using <see cref="SetSpeechRecognition"/>.
			/// </remarks>
			public Properties()
			{

			}

			/// <summary>
			/// Creates a new instance of the rotation properties connected to the specified control.
			/// </summary>
			/// <param name="owner"></param>
			/// <param name="control"></param>
			internal Properties(SpeechRecognition owner, Control control)
			{
				Debug.Assert(owner != null);
				Debug.Assert(control != null);

				this.owner = owner;
				this.control = control;
			}

			internal SpeechRecognition Owner
			{
				get { return this.owner; }
				set { this.owner = value; }
			}

			internal Control Control
			{
				get { return this.control; }
				set { this.control = value; }
			}

			/// <summary>
			/// Returns or sets how the <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognition" /> extender applies to the extended control.
			/// </summary>
			/// <remarks>
			/// The recognized text is assigned only to the extended control that has the focus. With
			/// <see cref="SpeechRecognition.RecognitionMode.WhenFocusedOnce"/> (default), <see cref="Enabled"/> is reset to false
			/// after the first final result is assigned to the control; with <see cref="SpeechRecognition.RecognitionMode.WhenFocused"/>
			/// every final result replaces the text of the control while it has the focus.
			/// </remarks>
			[DefaultValue(RecognitionMode.WhenFocusedOnce)]
			[Description("Determines how the speak extender applies to the extended control.")]
			public RecognitionMode RecognitionMode
			{
				get { return this._reconMode; }
				set
				{
					if (this._reconMode != value)
					{
						this._reconMode = value;
						Update();
					}
				}
			}
			private RecognitionMode _reconMode = RecognitionMode.WhenFocusedOnce;

			/// <summary>
			/// Returns or sets whether the recognized text is assigned to the control.
			/// </summary>
			/// <remarks>
			/// When enabled and the control has the focus, the final result with the highest confidence replaces the text
			/// of the control and the caret is moved to the end. The component itself must be listening: set
			/// <see cref="SpeechRecognition.Enabled"/> to true or call <see cref="SpeechRecognition.Start"/>.
			/// </remarks>
			[DefaultValue(false)]
			[Description("Enables or disables speech recognition on the control.")]
			public bool Enabled
			{
				get { return this._enabled; }
				set
				{
					if (this._enabled != value)
					{
						this._enabled = value;
						Update();
					}
				}
			}
			private bool _enabled = false;

			/// <summary>
			/// Process the speech recognition results in relation to the
			/// speech enabled control.
			/// </summary>
			/// <param name="results"></param>
			/// <returns>True if the result was applied to the control.</returns>
			internal bool ProcessResults(SpeechRecognitionResult[] results)
			{
				if (this.Enabled)
				{
					var textBox = this.control as TextBoxBase;
					if (textBox != null)
					{
						if (textBox.Focused)
						{
							SpeechRecognitionResult bestResult = null;
							foreach (var r in results)
							{
								if (r.IsFinal && (bestResult == null || r.Confidence > bestResult.Confidence))
									bestResult = r;
							}
							if (bestResult != null)
							{
								textBox.Text = bestResult.Transcript;
								textBox.SelectionStart = textBox.Text.Length;

								// disable for the next time around.
								if (this.RecognitionMode == RecognitionMode.WhenFocusedOnce)
									this.Enabled = false;

								return true;
							}
						}
					}
				}

				return false;
			}

			private void Update()
			{
				this.owner?.Update(this.control);
			}

			internal object Render()
			{
				return new
				{
					enabled = this.Enabled,
					reconMode = this.RecognitionMode
				};
			}

			internal class ExpandableObjectConverter : System.ComponentModel.ExpandableObjectConverter
			{
				public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
				{
					if (destinationType == typeof(string))
						return "(...)";

					return base.ConvertTo(context, culture, value, destinationType);
				}
			}

		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{

				case "result":
					OnResult(new SpeechRecognitionEventArgs(e.Parameters.Results, null));
					break;

				case "speechend":
					OnSpeechEnd(EventArgs.Empty);
					break;

				case "speechstart":
					OnSpeechStart(EventArgs.Empty);
					break;

				case "nomatch":
					OnNoMatch(EventArgs.Empty);
					break;

				case "error":
					OnError(new SpeechRecognitionEventArgs(null, e.Parameters.Message));
					break;

				default:
					base.OnWebEvent(e);
					break;
			}
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.web.extender.speech.SpeechRecognition";
			config.lang = this.Language;
			config.enabled = this.Enabled;
			config.grammars = this.Grammars;
			config.continuous = this.Continuous;
			config.interimResults = this.InterimResults;
			config.maxAlternatives = this.MaxAlternatives;

			lock (this.listeners)
			{
				WiredEvents events = new WiredEvents();
				if (base.Events[nameof(Result)] != null || (this.listeners != null && this.listeners.Count > 0))
					events.Add("result(Results),");
				if (base.Events[nameof(SpeechStart)] != null)
					events.Add("speechstart");
				if (base.Events[nameof(SpeechEnd)] != null)
					events.Add("speechend");
				if (base.Events[nameof(NoMatch)] != null)
					events.Add("nomatch");
				if (base.Events[nameof(Error)] != null)
					events.Add("error(Message)");

				config.wiredEvents = (events.Count > 0) ? events : null;
			}
		}

		#endregion

	}
}
