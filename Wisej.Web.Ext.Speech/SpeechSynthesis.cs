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
using Wisej.Web;

namespace Wisej.Web.Ext.Speech
{
	/// <summary>
	/// The SpeechSynthesis interface of the Web Speech API is the controller interface for the speech service; this can be used to retrieve 
	/// information about the synthesis voices available on the device, start and pause speech, and other commands besides.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(SpeechSynthesis))]
	[ProvideProperty("SpeechSynthesis", typeof(Control))]
	[Description("The SpeechSynthesis interface of the Web Speech API is the controller interface for the speech service.")]
	public class SpeechSynthesis : Wisej.Web.Component, IExtenderProvider
	{
		// collection of controls using the extender provider.
		private Dictionary<Control, Properties> utterances;

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.Speech"/> class.
		/// </summary>
		public SpeechSynthesis()
		{
			this.utterances = new Dictionary<Control, Properties>();
		}
		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the voice that will be used to speak the utterance.
		/// </summary>
		[DefaultValue("native")]
		[Description("Gets or sets the voice that will be used to speak the utterance.")]
		public string Voice
		{
			get { return this._voice; }
			set
			{
				if (String.IsNullOrEmpty(value))
					value = "native";

				if (this._voice != value)
				{
					this._voice = value;
					Update();
				}
			}
		}
		private string _voice = "native";

		/// <summary>
		/// Returns or sets the language of the utterance.
		/// </summary>
		[DefaultValue("")]
		[Description("Gets or sets the language of the utterance.")]
		public string Language
		{
			get { return this._language; }
			set
			{
				value = value ?? string.Empty;

				if (this._language != value)
				{
					this._language = value;
					Update();
				}
			}
		}
		private string _language = string.Empty;

		/// <summary>
		/// Returns or sets the volume that the utterance will be spoken at. The default is 1 (maximum).
		/// </summary>
		[DefaultValue(1f)]
		[Description("Gets or sets the volume that the utterance will be spoken at. The default is 1 (maximum).")]
		public float Volume
		{
			get { return this._volume; }
			set
			{
				if (value < 0 || value > 1)
					throw new ArgumentOutOfRangeException("Volume", SR.GetString("InvalidBoundArgument", "Volume", value, 0, 1));

				if (this._volume != value)
				{
					this._volume = value;
					Update();
				}
			}
		}
		private float _volume = 1;


		/// <summary>
		/// Returns and sets the speed at which the utterance will be spoken at.
		/// </summary>
		[DefaultValue(1f)]
		[Description("Gets and sets the speed at which the utterance will be spoken at.")]
		public float Rate
		{
			get { return this._rate; }
			set
			{
				if (value < 0 || value > 10)
					throw new ArgumentOutOfRangeException("Rate", SR.GetString("InvalidBoundArgument", "Rate", value, 0.1f, 10f));

				if (this._rate != value)
				{
					this._rate = value;
					Update();
				}
			}
		}
		private float _rate = 1;

		/// <summary>
		/// Returns and sets the pitch at which the utterance will be spoken at.
		/// </summary>
		[DefaultValue(1f)]
		[Description("Gets and sets the pitch at which the utterance will be spoken at.")]
		public float Pitch
		{
			get { return this._pitch; }
			set
			{
				if (value < 0 || value > 2)
					throw new ArgumentOutOfRangeException("Pitch", SR.GetString("InvalidBoundArgument", "Pitch", value, 0, 2));

				if (this._pitch != value)
				{
					this._pitch = value;
					Update();
				}
			}
		}
		private float _pitch = 1;

		/// <summary>
		/// Enables or disables the this <see cref="T:Wisej.Web.Ext.Speech.SpeechSynthesis" /> extender.
		/// </summary>
		[DefaultValue(true)]
		[Description("Enables or disables speech synthesis.")]
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
		private bool _enabled = true;

		#endregion

		#region Methods

		/// <summary>
		/// Puts the SpeechSynthesis object into a paused state.
		/// </summary>
		public void Pause()
		{
			Call("pause");
		}

		/// <summary>
		/// Adds an utterance to the utterance queue; it will be spoken when any other utterances queued before it have been spoken.
		/// </summary>
		/// <param name="text"></param>
		public void Speak(string text)
		{
			Call("speak", text);
		}

		/// <summary>
		/// Resumes speaking if the SpeechSynthesis object was already paused.
		/// </summary>
		public void Resume()
		{
			Call("resume");
		}

		/// <summary>
		/// Removes all utterances from the utterance queue.
		/// If an utterance is currently being spoken, speaking will stop immediately.
		/// </summary>
		public void Cancel()
		{
			Call("cancel");
		}

		/// <summary>
		/// Returns true if <see cref="T:Wisej.Web.Ext.Speech.SpeechSynthesis" /> can offer an extender property to the specified target component.
		/// </summary>
		/// <returns>true if the <see cref="T:Wisej.Web.Ext.Speech.SpeechSynthesis" /> class can offer one or more extender properties; otherwise, false.</returns>
		/// <param name="target">The target object to add an extender property to. </param>
		public bool CanExtend(object target)
		{
			return (target is Control);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.utterances.Clear();
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// SpeechSynthesis properties.
		/// </summary>
		/// <returns>A <see cref="T:Wisej.Web.Ext.Speech.SpeechSynthesis.Properties" /> instance with the SpeechSynthesis properties.</returns>
		/// <param name="control">The <see cref="T:Wisej.Web.Control" /> for which to retrieve the speech properties. </param>
		[DisplayName("SpeechSynthesis")]
		[Description("SpeechSynthesis properties")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Properties GetSpeechSynthesis(Control control)
		{
			return GetSpeechSynthesisProperties(control);
		}

		private bool ShouldSerializeSpeechSynthesis(Control control)
		{
			if (!HasSpeechSynthesisProperties(control))
				return false;

			Properties props = GetSpeechSynthesisProperties(control);
			return props.SpeakMode != SpeakMode.Never;
		}

		private void ResetSpeechSynthesis(Control control)
		{
			lock (this.utterances)
			{
				this.utterances.Remove(control);
				control.Disposed -= this.Control_Disposed;
			}

			Update(control);
		}

		/// <summary>
		/// Removes all speech extenders.
		/// </summary>
		public void RemoveAll()
		{
			lock (this.utterances)
			{
				this.utterances.Clear();
			}
		}

		/// <summary>
		/// Updates the component on the client.
		/// </summary>
		private void Update(Control control)
		{
			base.Update();
		}

		public void SetSpeechSynthesis(Control control, Properties speechSynthesis)
		{
			// don't do anything. this is here only to enable the property for writing.
		}

		/// <summary>
		/// Returns if the control has defined the speech synthesis properties.
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		private bool HasSpeechSynthesisProperties(Control control)
		{
			if (control == null)
				throw new ArgumentNullException("control");

			lock (this.utterances)
			{
				return this.utterances.ContainsKey(control);
			}
		}

		/// <summary>
		/// Returns of creates the speech synthesis properties associated with the control.
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		private Properties GetSpeechSynthesisProperties(Control control)
		{
			if (control == null)
				throw new ArgumentNullException("control");

			lock (this.utterances)
			{
				Properties props = null;
				if (!this.utterances.TryGetValue(control, out props))
				{
					props = new Properties(this, control);
					this.utterances.Add(control, props);

					control.Disposed -= this.Control_Disposed;
					control.Disposed += this.Control_Disposed;
				}
				return props;
			}
		}


		private void Control_Disposed(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			control.Disposed -= Control_Disposed;

			// remove the extender values associated with the disposed control.
			lock (this.utterances)
			{
				this.utterances.Remove(control);
			}
		}

		#endregion

		#region Speech Properties

		/// <summary>
		/// Determines how the <see cref="T:Wisej.Web.Ext.Speech.SpeechSynthesis" /> extender applies to the extended control.
		/// </summary>
		public enum SpeakMode
		{
			/// <summary>
			/// Speech is disabled for this control.
			/// </summary>
			[Description("Speech is disabled for this control.")]
			Never,

			/// <summary>
			/// Speaks the specified text when the control is activated.
			/// </summary>
			[Description("Speaks the specified text when the control is activated.")]
			TextOnEnter,

			/// <summary>
			/// Speaks the specified text when the control is deactivated.
			/// </summary>
			[Description("Speaks the specified text when the control is deactivated.")]
			TextOnLeave,

			/// <summary>
			/// Speaks the value of the control when it is activated.
			/// </summary>
			[Description("Speaks the value of the control when it is activated.")]
			ValueOnEnter,

			/// <summary>
			/// Speaks the value of the control when it is deactivated.
			/// </summary>
			[Description("Speaks the value of the control when it is deactivated.")]
			ValueOnLeave,

			/// <summary>
			/// Speaks the specified text when the control is activated the first time only.
			/// </summary>
			[Description("Speaks the specified text when the control is activated. the first time only")]
			TextOnEnterOnce,

			/// <summary>
			/// Speaks the specified text when the control is deactivated the first time only.
			/// </summary>
			[Description("Speaks the specified text when the control is deactivated the first time only.")]
			TextOnLeaveOnce,

			/// <summary>
			/// Speaks the value of the control when it is activated the first time only.
			/// </summary>
			[Description("Speaks the value of the control when it is activated the first time only.")]
			ValueOnEnterOnce,

			/// <summary>
			/// Speaks the value of the control when it is deactivated the first time only.
			/// </summary>
			[Description("Speaks the value of the control when it is deactivated the first time only.")]
			ValueOnLeaveOnce
		}

		/// <summary>
		/// Represents the set of speech properties added to the extended controls.
		/// </summary>
		[TypeConverter(typeof(Properties.ExpandableObjectConverter))]
		public class Properties
		{
			private SpeechSynthesis owner;
			private Control control;

			/// <summary>
			/// Creates a new instance of the rotation properties connected to the specified control.
			/// </summary>
			/// <param name="owner"></param>
			/// <param name="control"></param>
			internal Properties(SpeechSynthesis owner, Control control)
			{
				Debug.Assert(owner != null);
				Debug.Assert(control != null);

				this.owner = owner;
				this.control = control;
			}

			/// <summary>
			/// Determines how the <see cref="T:Wisej.Web.Ext.Speech.SpeechSynthesis" /> extender applies to the extended control.
			/// </summary>
			[DefaultValue(SpeakMode.Never)]
			[Description("Determines how the speak extender applies to the extended control.")]
			public SpeakMode SpeakMode
			{
				get { return this._speakMode; }
				set
				{
					if (this._speakMode != value)
					{
						this._speakMode = value;
						this.owner.Update(this.control);
					}
				}
			}
			private SpeakMode _speakMode = SpeakMode.Never;

			/// <summary>
			/// Returns or sets the text to speak.
			/// </summary>
			[Localizable(true)]
			[DefaultValue(null)]
			[Description("Gets or sets the text to speak.")]
			public string TextToSpeak
			{
				get { return this._textToSpeak; }
				set
				{
					if (value == string.Empty)
						value = null;

					if (this._textToSpeak != value)
					{
						this._textToSpeak = value;
						this.owner.Update(this.control);
					}
				}
			}
			private string _textToSpeak = null;

			internal object Render()
			{
				return new
				{
					textToSpeak = this.TextToSpeak,
					speakMode = this.SpeakMode
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
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.web.extender.speech.SpeechSynthesis";
			config.enabled = this.Enabled;
			config.volume = this.Volume;
			config.pitch = this.Pitch;
			config.lang = this.Language;
			config.rate = this.Rate;
			config.voice = this.Voice;

			lock (this.utterances)
			{
				config.utterances = this.utterances.Select(o => new
				{
					id = ((IWisejControl)o.Key).Id,
					speech = o.Value.Render()

				}).ToArray();
			}
		}

		#endregion

	}
}
