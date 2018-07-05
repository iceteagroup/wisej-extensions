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

/**
 * wisej.web.extender.speech.SpeechSynthesis
 *
 * This extender class adds Web Speech API features to Wisej widgets.
 */
qx.Class.define("wisej.web.extender.speech.SpeechSynthesis", {

	extend: qx.core.Object,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	construct: function () {

		this.base(arguments);

		this.initVoice();
		this.__speechByComponentId = {};
	},

	defer: function () {

		// try to preload the list of voices.
		var speech = window.speechSynthesis;
		if (speech && speech.getVoices)
			var voices = speech.getVoices();
	},

	properties: {

		/**
		 * Enabled property.
		 */
		enabled: { init: true, check: "Boolean" },

		/**
		 * Volume property.
		 *
		 * Gets and sets the volume that the utterance will be spoken at. The default is 1 (maximum).
		 */
		volume: { init: 1, check: "Number" },

		/**
		 * Pitch property.
		 *
		 * Gets and sets the pitch at which the utterance will be spoken at.
		 */
		pitch: { init: 1, check: "Number" },

		/**
		 * Lang property.
		 *
		 * Gets and sets the language of the utterance.
		 */
		lang: { init: "", check: "String" },

		/**
		 * Rate property.
		 *
		 * Gets and sets the speed at which the utterance will be spoken at.
		 */
		rate: { init: 1, check: "Number" },

		/**
		 * Voice property.
		 *
		 * Gets and sets the voice that will be used to speak the utterance.
		 */
		voice: { init: "native", check: "String", apply: "_applyVoice" },

		/**
		 * Utterances property.
		 *
		 * Collection of speech properties associated to other widgets.
		 */
		utterances: { init: null, nullable: true, check: "Array", apply: "_applyUtterances" },
	},

	members: {

		// speech property map, connects the component to the speech features.
		__speechByComponentId: null,

		// current voice object.
		__voice: null,

		/**
		 *  Adds an utterance to the utterance queue; it will be spoken when any other utterances queued before it have been spoken.
		 */
		speak: function (text) {

			if (!text)
				return;

			var speech = window.speechSynthesis;
			if (speech && speech.speak) {
				if (window.SpeechSynthesisUtterance) {

					var utterance = new SpeechSynthesisUtterance(text);

					utterance.rate = this.getRate();
					utterance.lang = this.getLang();
					utterance.pitch = this.getPitch();
					utterance.volume = this.getVolume();

					// if we have a voice but the list was not initialized, apply it again.
					if (this.getVoice() && !this.__voice)
						this._applyVoice(this.getVoice());

					// the voice should override the language setting.
					if (this.__voice != null) {
						utterance.lang = "";
						utterance.voice = this.__voice;
					}

					speech.speak(utterance);
				}
			}
		},

		/**
		 * Puts the SpeechSynthesis object into a paused state.
		 */
		pause: function () {

			var speech = window.speechSynthesis;
			if (speech && speech.pause)
				speech.pause();
		},

		/**
		 * Resumes speaking if the SpeechSynthesis object was already paused.
		 */
		resume: function () {

			var speech = window.speechSynthesis;
			if (speech && speech.resume)
				speech.resume();
		},

		/**
		 * Removes all utterances from the utterance queue.
		 */
		cancel: function () {

			var speech = window.speechSynthesis;
			if (speech && speech.cancel)
				speech.cancel();
		},

		_applyVoice: function (value, old) {

			this.__voice = null;

			// find the voice that matches the name.
			var speech = window.speechSynthesis;
			if (speech && speech.getVoices)
			{
				var voices = speech.getVoices();
				for (var i=0;i<voices.length; i++)
				{
					if (voices[i].name == value)
					{
						this.__voice = voices[i];
						break;
					}
				}
			}
		},

		_applyUtterances: function (value, old) {

			if (old != null && old.length > 0) {
				for (var i = 0; i < old.length; i++) {

					var id = old[i].id;

					// skip if the component is also in the new values.
					if (value != null) {
						var skip = false;
						for (var j = 0; j < value.length; j++) {
							if (value[j].id == id) {
								skip = true;
								break;
							}
						}
						if (skip)
							continue;
					}

					var comp = Wisej.Core.getComponent(id);
					delete this.__speechByComponentId[id];
					if (comp) {
						comp.removeListener("activate", this.__onComponentSpeak, this);
						comp.removeListener("deactivate", this.__onComponentSpeak, this);
					}
				}
			}

			if (value != null && value.length > 0) {
				for (var i = 0; i < value.length; i++) {

					var id = value[i].id;

					// make sure the handlers are attached only once.
					if (this.__speechByComponentId[id])
						continue;

					var comp = Wisej.Core.getComponent(id);
					if (comp) {
						this.__speechByComponentId[id] = value[i].speech;
						comp.addListener("activate", this.__onComponentSpeak, this);
						comp.addListener("deactivate", this.__onComponentSpeak, this);
					}
				}
			}
		},

		__onComponentSpeak: function (e) {

			e.stopPropagation();

			if (!this.isEnabled())
				return;

			var type = e.getType();
			var comp = wisej.utils.Widget.findWisejComponent(e.getTarget());
			if (!comp || !comp.isWisejComponent)
				return;

			// retrieve the speech properties.
			var speech = this.__speechByComponentId[comp.getId()];
			if (!speech)
				return;

			var text = null;
			var speak = false;
			var once = false;
			switch (speech.speakMode) {

				case "textOnEnter":
					speak = (type == "activate");
					text = speech.textToSpeak;
					break;

				case "textOnLeave":
					speak = (type == "deactivate");
					text = speech.textToSpeak;
					break;

				case "valueOnEnter":
					speak = (type == "activate");
					text = comp.getValue ? comp.getValue() : "";
					break;

				case "valueOnLeave":
					speak = (type == "deactivate");
					text = comp.getValue ? comp.getValue() : "";
					break;

				case "textOnEnterOnce":
					once = true;
					speak = (type == "activate");
					text = speech.textToSpeak;
					break;

				case "textOnLeaveOnce":
					once = true;
					speak = (type == "deactivate");
					text = speech.textToSpeak;
					break;

				case "valueOnEnterOnce":
					once = true;
					speak = (type == "activate");
					text = comp.getValue ? comp.getValue() : "";
					break;

				case "valueOnLeaveOnce":
					once = true;
					speak = (type == "deactivate");
					text = comp.getValue ? comp.getValue() : "";
					break;
			}

			if (speak && text) {

				this.speak(text);

				if (once) {
					delete this.__speechByComponentId[comp.getId()];
					comp.removeListener("activate", this.__onComponentSpeak, this);
					comp.removeListener("deactivate", this.__onComponentSpeak, this);
				}
			}
		},
	},

	destruct: function () {

		this.__speechByComponentId = null;
	},


});
