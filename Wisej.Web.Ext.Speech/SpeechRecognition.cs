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
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.Speech
{
	/// <summary>
	/// The SpeechRecognition interface of Web Speech API allows JavaScript to have access to a browser's audio stream and convert it to text.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(SpeechRecognition))]
	[ProvideProperty("SpeechRecognition", typeof(Control))]
	[Description("The SpeechRecognition interface of Web Speech API allows JavaScript to have access to a browser's audio stream and convert it to text.")]
	public class SpeechRecognition : Wisej.Web.Component, IExtenderProvider
	{
		// collection of controls using the extender provider.
		private Dictionary<Control, Properties> listeners;

		#region Constructors
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

			((SpeechRecognitionEventHandler)base.Events[nameof(Result)])?.Invoke(this,e);			
		}

		/// <summary>
		/// Fires the Error event.
		/// </summary>
		/// <param name="e">A <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognitionEventArgs" /> that contains the event data. </param>
		protected virtual void OnError(SpeechRecognitionEventArgs e)
		{
			((SpeechRecognitionEventHandler)base.Events[nameof(Error)])?.Invoke(this,e);			
		}

		/// <summary>
		/// Fires the NoMatch event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnNoMatch(EventArgs e)
		{
			((EventHandler)base.Events[nameof(NoMatch)])?.Invoke(this,e);			
		}

		/// <summary>
		/// Fires the SpeechStart event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSpeechStart(EventArgs e)
		{
			((EventHandler)base.Events[nameof(SpeechStart)])?.Invoke(this,e);			
		}

		/// <summary>
		/// Fires the SpeechEnd event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSpeechEnd(EventArgs e)
		{
			((EventHandler)base.Events[nameof(SpeechEnd)])?.Invoke(this,e);			
		}

		#endregion

		#region Properties

		/// <summary>
		/// Controls whether continuous results are returned for each recognition, or only a single result.
		/// </summary>
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
		/// Returns and sets the language of the current SpeechRecognition. 
		/// If not specified, this defaults to the HTML lang attribute value, or the user agent's language setting if that isn't set either.
		/// </summary>
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
		/// Returns and sets the maximum number of alternatives provided per each speech recognition result.
		/// </summary>
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
		/// Controls whether interim results should be returned (true) or not (false.)
		/// Interim results are results that are not yet final (e.g. the SpeechRecognitionResult.isFinal property is false.)
		/// </summary>
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
		/// Enables or disables the this <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognition" /> extender.
		/// </summary>
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
		/// Returns and sets a collection of grammar definitions - using the JSpeech Grammar Format (JSGF) https://www.w3.org/TR/jsgf/.
		/// </summary>
		[Localizable(true)]
		[Description("Gets and sets a collection of grammar definitions - using the JSpeech Grammar Format (JSGF) https://www.w3.org/TR/jsgf/.")]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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
		public void Start()
		{
			Call("start");
		}

		/// <summary>
		/// Stops the speech recognition service from listening to incoming audio, and attempts to return a result using the audio captured so far.
		/// </summary>
		public void Stop()
		{
			Call("stop");
		}

		/// <summary>
		/// Stops the speech recognition service from listening to incoming audio, and doesn't attempt to return a result.
		/// </summary>
		public void Abort()
		{
			Call("abort");
		}

		/// <summary>
		/// Returns true if <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognition" /> can offer an extender property to the specified target component.
		/// </summary>
		/// <returns>true if the <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognition" /> class can offer one or more extender properties; otherwise, false.</returns>
		/// <param name="target">The target object to add an extender property to. </param>
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
		/// SpeechRecognition properties.
		/// </summary>
		/// <returns>A <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognition.Properties" /> instance with the SpeechRecognition properties.</returns>
		/// <param name="control">The <see cref="T:Wisej.Web.Control" /> for which to retrieve the speech properties. </param>
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
		public void Clear()
		{
			lock (this.listeners)
			{
				this.listeners.ToList().ForEach((o) => {
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
		/// <param name="control">The control to rotate.</param>
		/// <param name="properties">An instance of <see cref="T:Wisej.Web.SpeechRecognition.Properties"/> defining the speech listeners.</param>
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
			/// Determines how the <see cref="T:Wisej.Web.Ext.Speech.SpeechRecognition" /> extender applies to the extended control.
			/// </summary>
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
			/// Enables or disables speech recognition on the control.
			/// </summary>
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
					TextBoxBase textBox = this.control as TextBoxBase;
					if (textBox != null)
						if (this.control.Focused)
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

								// disable for the next time around.
								if (this.RecognitionMode == RecognitionMode.WhenFocusedOnce)
									this.Enabled = false;

								return true;
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
