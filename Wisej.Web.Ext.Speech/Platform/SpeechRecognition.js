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
 * wisej.web.extender.speech.SpeechRecognition
 *
 * This extender class adds Web Speech API features to Wisej widgets.
 */
qx.Class.define("wisej.web.extender.speech.SpeechRecognition", {

	extend: qx.core.Object,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	construct: function () {

		this.base(arguments);

		this.__speechByComponentId = {};

		// initialize the recognition object.
		var speech = window.speechRecognition || window.webkitSpeechRecognition || window.mozSpeechRecognition;
		if (speech) {
			this.__recognition = new speech();

			this.__recognition.onend = this.__onEnd.bind(this);
			this.__recognition.onerror = this.__onError.bind(this);
			this.__recognition.onstart = this.__onStart.bind(this);
			this.__recognition.onresult = this.__onResult.bind(this);
			this.__recognition.onnomatch = this.__onNoMatch.bind(this);
			this.__recognition.onspeechend = this.__onSpeechEnd.bind(this);
			this.__recognition.onspeechstart = this.__onSpeechStart.bind(this);
		}
	},

	properties: {

		/**
		 * Enabled property.
		 */
		enabled: { init: true, check: "Boolean", apply: "_applyEnabled" },

		/**
		 * InterimResults property.
		 *
		 * Controls whether interim results should be returned (true) or not (false.)
		 */
		interimResults: { init: false, check: "Boolean", apply: "_applyProperty" },

		/**
		 * MaxAlternatives property.
		 *
		 * Gets and sets the maximum number of alternatives provided per each speech recognition result.
		 */
		maxAlternatives: { init: 1, check: "PositiveInteger", apply: "_applyProperty" },

		/**
		 * Lang property.
		 *
		 * Gets and sets the language of the current SpeechRecognition.
		 */
		lang: { init: null, check: "String", apply: "_applyProperty" },

		/**
		 * Grammars property.
		 *
		 * Gets and sets the list of grammar definitions for this instance of the SpeechRecognition object.
		 * See https://developer.mozilla.org/en-US/docs/Web/API/SpeechGrammar and https://www.w3.org/TR/jsgf/ for the grammar syntax.
		 * 
		 */
		grammars: { init: null, check: "Array", apply: "_applyGrammars" },

		/**
		 * Continuous property.
		 *
		 * Controls whether continuous results are returned for each recognition, or only a single result.
		 */
		continuous: { init: false, check: "Boolean", apply: "_applyProperty" },
	},

	members: {

		// current recognition object.
		__recognition: null,

		/**
		 *  Starts the speech recognition service listening to incoming audio.
		 */
		start: function () {

			if (this.__recognition && !this.__started) {
				try {
					this.__recognition.start();
				} catch (ex) { }
			}

		},

		/**
		 * Stops the speech recognition service from listening to incoming audio, and attempts to return a result using the audio captured so far.
		 */
		stop: function () {

			if (this.__recognition && this.__started) {
				try {
					this.__recognition.stop();
				}
				catch (ex) { }
			}
		},

		/**
		 * Stops the speech recognition service from listening to incoming audio, and doesn't attempt to return a result.
		 */
		abort: function () {

			if (this.__recognition && this.__started) {
				try {
					this.__recognition.abort();
				}
				catch (ex) { }
			}
		},

		_applyEnabled: function (value, old) {

			value
				? this.start()
				: this.stop();
		},

		_applyProperty: function (value, old, name) {

			if (this.__recognition)
				this.__recognition[name] = value;
		},

		_applyGrammars: function (value, old) {

			if (this.__recognition) {

				try {
					if (value && value.length > 0) {

						// create the list.
						var grammarList = window.speechGrammarList || window.webkitSpeechGrammarList || window.mozSpeechGrammarList;
						if (grammarList) {
							var list = new grammarList();
							for (var i = 0; i < value.length; i++) {
								list.addFromString(value[i], 1);
							}
							this.__recognition.grammars = list;
						}
					}
					else {
						this.__recognition.grammars = null;
					}
				} catch (ex) { }
			}
		},

		// event handlers.

		__onResult: function (e) {

			// convert to an array.
			var results = [];
			for (var i = e.resultIndex; i < e.results.length; ++i) {

				var res = e.results[i];
				var length = res.length;
				for (var j = 0; j < length; j++) {

					results.push({
						isFinal: res.isFinal,
						confidence: res[j].confidence,
						transcript: res[j].transcript
					});
				}
			}

			this.fireDataEvent("result", results);
		},

		__onError: function (e) {
			this.fireDataEvent("error", e.error);
		},

		__onNoMatch: function (e) {
			this.fireEvent("nomatch");
		},

		__onSpeechStart: function (e) {
			this.fireEvent("speechstart");
		},

		__onSpeechEnd: function (e) {
			this.fireEvent("speechend");
		},

		__onStart: function (e) {
			this.__started = true;
		},

		__onEnd: function (e) {

			this.__started = false;

			if (this.isEnabled()) {
				var me = this;
				setTimeout(function () {
					me.start();
				}, 10);
			}
		},
	},

});
