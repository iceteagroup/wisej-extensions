//#Minify=Off
///////////////////////////////////////////////////////////////////////////////
//
// (C) 2022 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
 * wisej.ext.WebAuthn
 * 
 * Provides a wrapper for Web Authentication API.
 */
qx.Class.define("wisej.ext.WebAuthn", {

	type: "singleton",
	extend: qx.core.Object,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	construct: function () {

		this.base(arguments);

		// cbor.js is required to decode authenticator-related data.
		wisej.utils.Loader.load([
			{
				id: "cbor.js",
				url: "resource.wx/Wisej.Ext.WebAuthn.JavaScript.cbor.js"
			}]);
	},

	members: {

		/**
		 * Detects whether a platform-authenticator is available for the user.
		 **/
		isUserVerifyingPlatformAuthenticatorAvailable: function () {
			return (async function () {
				return await PublicKeyCredential.isUserVerifyingPlatformAuthenticatorAvailable();
			})();
        },

		/**
		 * Creates a new Credential instance based on the provided options, or null if no Credential object can be created.
		 * @param {any} challenge Random string for validating the request.
		 * @param {any} rp Relaying Party (rp), the organization responsible for registering and authenticating the user.
		 * @param {any} user Information about the user currently registering.
		 * @param {any} pubKeyCredParams Array describing what public key types are acceptable to the server.
		 * @param {any} authenticatorSelection "platform" (Windows Hello) vs "cross-platform" (Yubikey) required.
		 * @param {any} timeout The time in milliseconds that the user has to respond to a prompt for registration.
		 * @param {any} attestation allows servers to indicate how important the attestation data is to this registration event.
		 */
		create: function (challenge, rp, user, pubKeyCredParams, authenticatorSelection, timeout, attestation) {
			var me = this;
			return (async function () {
				var configuration = {
					publicKey: {
						challenge: me._formatChallenge(challenge),
						rp: rp,
						user: {
							id: Uint8Array.from(user.id, c => c.charCodeAt(0)),
							displayName: user.displayName,
							name: user.name
						},
						pubKeyCredParams: pubKeyCredParams,
						authenticatorSelection: authenticatorSelection,
						timeout: timeout,
						attestation: attestation
					}
				};

				try {
					var credentials = await navigator.credentials.create(configuration);
					return me._processResponse(credentials);
				} catch (ex) {
					return ex.message;
				}
			})();
		},

		/**
		 * Gets an assertion that verifies the user has a private key.
		 * @param {any} challenge Random string for validating the request.
		 * @param {any} allowCredentials Which credentials the server would like the user to authenticate with.
		 * @param {any} timeout The time in milliseconds that the user has to respond to a prompt for registration.
		 */
		get: function (challenge, allowCredentials, timeout) {
			var me = this;
			return (async function () {

				var assertion = await navigator.credentials.get({
					publicKey: {
						challenge: me._formatChallenge(challenge),
						allowCredentials: [{
							type: allowCredentials.type,
							transports: allowCredentials.transports ?? [],
							id: me._base64ToUint8Array(allowCredentials.id)
						}],
						timeout: timeout
					}
				});

				return me._processResponse(assertion);
			})();
		},

		/**
		 * Converts the given challenge into a Uint8Array for use with the authenticator.
		 * @param {any} challenge The request challenge.
		 */
		_formatChallenge: function (challenge) {
			var byteCharacters = atob(challenge);
			var byteNumbers = new Array(byteCharacters.length);
			for (var i = 0; i < byteCharacters.length; i++) {
				byteNumbers[i] = byteCharacters.charCodeAt(i);
			}
			return new Uint8Array(byteNumbers);
		},

		/**
		 * Converts the given data into a response format usable by the server.
		 * @param {any} data The credential response.
		 */
		_processResponse: function (data) {

			// parse client data.
			var decoder = new TextDecoder("utf-8");

			var clientDataJSON = data.response.clientDataJSON;
			var clientDataBase64 = this._arrayBufferToBase64(clientDataJSON);
			var clientDataText = decoder.decode(clientDataJSON);
			var clientData = JSON.parse(clientDataText);
			var type = clientData.type;

			var authenticatorAttachment = data.authenticatorAttachment;
			var authenticatorDataBase64;
			var authenticatorData;
			var attestationObject;
			var signature;
			var userHandle;

			switch (type) {
				case "webauthn.create":

					attestationObject = CBOR.decode(data.response.attestationObject);
					authenticatorData = this._parseAuthenticatorData(attestationObject.authData);
					break;

				case "webauthn.get":

					var authData = data.response.authenticatorData;
					authenticatorDataBase64 = this._arrayBufferToBase64(authData);
					authenticatorData = this._parseAuthenticatorData(authData);
					
					signature = this._arrayBufferToBase64(data.response.signature);

					if (data.response.userHandle != null)
						userHandle = decoder.decode(data.response.userHandle);

					break;
			}

			return {
				signature,
				userHandle,
				clientData,
				clientDataBase64,
				authenticatorData,
				authenticatorAttachment,
				authenticatorDataBase64
			};
		},

		/**
		 * Converts an ArrayBuffer to a base64 encoded value.
		 * @param {any} arrayBuffer
		 */
		_arrayBufferToBase64: function (arrayBuffer) {
			return btoa(String.fromCharCode.apply(null, new Uint8Array(arrayBuffer)));
		},

		/**
		 * Converts base64 encoded data to a Uint8Array.
		 * @param {any} base64
		 */
		_base64ToUint8Array: function (base64) {
			return new Uint8Array(atob(base64).split("").map(function (c) { return c.charCodeAt(0); }))
		},

		/**
		 * Parses data from the given authenticator data into a human-readable format.
		 * @param {any} authData Authenticator data.
		 */
		_parseAuthenticatorData: function (authData) {

			var rpIdHash = this._arrayBufferToBase64(authData.slice(0, 32));

			// parse the flags.
			var flagsBuffer = new Uint8Array(authData.slice(32, 33))[0];

			var userPresentFlag = this._isBitOn(flagsBuffer, 0);
			var userVerifiedFlag = this._isBitOn(flagsBuffer, 1);
			var backupEligibilityFlag = this._isBitOn(flagsBuffer, 3);
			var backupStateFlag = this._isBitOn(flagsBuffer, 4);
			var attestedCredentialDataFlag = this._isBitOn(flagsBuffer, 6);
			var extensionDataFlag = this._isBitOn(flagsBuffer, 7);

			// parse the signature counter.
			var signCount;
			var signCountArray = authData.slice(34, 37);
			if (signCountArray.buffer != null) {
				var signCountDataView = new DataView(signCountArray.buffer, 0)

				signCount = signCountDataView.getInt16(0);
            }

			// parse attestedCredentialData, if available.
			var attestedCredentialData = attestedCredentialDataFlag ? this._parseAttestedCredentialData(authData) : null;

			// TODO: parse extension data, if available.

			// see: https://w3c.github.io/webauthn/#sctn-authenticator-data.
			return {
				rpIdHash,
				signCount,
				flags: {
					userPresentFlag,
					backupStateFlag,
					userVerifiedFlag,
					extensionDataFlag,
					backupEligibilityFlag,
					attestedCredentialDataFlag,
				},
				attestedCredentialData
			};
		},

		/**
		 * Parses public key data if it's included.
		 * @param {any} authData The authenticator data.
		 * 
		 * @returns The public key data including public key and credential id.
		 */
		_parseAttestedCredentialData: function (authData) {

			var publicKey = {};

			var dataView = new DataView(new ArrayBuffer(2));

			// first parse the credential id.
			var idLenBytes = authData.slice(53, 55);

			idLenBytes.forEach((value, index) => dataView.setUint8(index, value));

			var credentialIdLength = dataView.getUint16();
			credentialId = this._arrayBufferToBase64(authData.slice(55, 55 + credentialIdLength));

			// use the credential id length to calculate where the public key starts.
			var publicKeyBytes = authData.slice(55 + credentialIdLength);
			publicKey = CBOR.decode(publicKeyBytes.buffer);

			var keys = Object.keys(publicKey);
			for (var i = 0; i < keys.length; i++) {
				var key = keys[i];
				var value = publicKey[key];
				if (value instanceof Uint8Array) {
					publicKey[key] = this._arrayBufferToBase64(value);
				}
			}

			return { publicKey, credentialId };
		},

		/**
		 * Checks whether a particular bit is set.
		 * @param {any} number
		 * @param {any} index
		 */
		_isBitOn: function (number, index) {
			return Boolean(number & (1 << index));
		}
	}
});
