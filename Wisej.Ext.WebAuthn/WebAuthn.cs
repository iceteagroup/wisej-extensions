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

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Wisej.Web;
using System.ComponentModel;

namespace Wisej.Ext.WebAuthn
{
	/// <summary>
	/// Provides methods for creating and retrieving credentials from the client following
	/// the Web Authorization API standards.
	/// </summary>
	[ApiCategory("WebAuthn")]
	public static class WebAuthn
	{
		/// <summary>
		/// Checks whether the client device has a user-verifying platform authenticator available for use.
		/// </summary>
		/// <returns>True if the device has a user-verifying platform authenticator.</returns>
		public static async Task<bool> IsUserVerifyingPlatformAuthenticatorAvailableAsync()
		{
			return await CallAsync("isUserVerifyingPlatformAuthenticatorAvailable");
		}

		/// <summary>
		/// Creates new credentials for the client.
		/// </summary>
		/// <param name="challenge">Random string for validating the request.</param>
		/// <param name="rp">Relaying Party (rp), the organization responsible for registering and authenticating the user.</param>
		/// <param name="user">Information about the user currently registering.</param>
		/// <param name="publicKeyCredentialParameters">Array describing what public key types are acceptable to the server.</param>
		/// <param name="authenticatorSelection">"platform" (Windows Hello) vs "cross-platform" (Yubikey) required.</param>
		/// <param name="timeout">The time in milliseconds that the user has to respond to a prompt for registration.</param>
		/// <param name="attestation">allows servers to indicate how important the attestation data is to this registration event.</param>
		/// <returns>The client's credentials.</returns>
		public static async Task<CredentialsResponse> CreateAsync(
			string challenge,
			RelyingParty rp,
			PublicKeyCredentialUserEntity user,
			PublicKeyCredentialParameters[] publicKeyCredentialParameters,
			AuthenticatorSelectionCriteria authenticatorSelection,
			int timeout,
			AttestationConveyancePreference attestation)
		{
			var result = await CallAsync("create",
				challenge,
				rp,
				user,
				publicKeyCredentialParameters.Select((entry) => new { type = entry.Type, alg = (int)entry.Alg }),
				authenticatorSelection,
				timeout,
				attestation);

			if (result is string)
			{
				throw new Exception(result);
			}
			else
			{
				return new CredentialsResponse
				{
					AuthenticatorData = new AuthenticatorData
					{
						PublicKey = new PublicKey
						{
							CredentialID = result.authenticatorData.attestedCredentialData.credentialId,
							Data = result.authenticatorData.attestedCredentialData.publicKey
						}
					},
					ClientData = new ClientData
					{
						Challenge = FromBase64Url((string)result.clientData.challenge),
						Origin = result.clientData.origin,
						Type = result.clientData.type
					},
					SignatureCounter = unchecked((int)Convert.ToUInt32(result.authenticatorData.signCount ?? 0))
				};
			}
		}

		/// <summary>
		/// Gets the requested credentials from the client.
		/// </summary>
		/// <param name="challenge">Random string for validating the request.</param>
		/// <param name="allowCredentials">Which credential the server would like the user to authenticate with.</param>
		/// <param name="timeout">The time in milliseconds that the user has to respond to a prompt for registration.</param>
		/// <returns></returns>
		public static async Task<CredentialsResponse> GetAsync(
			string challenge,
			PublicKeyCredentialDescriptor[] allowCredentials,
			int timeout)
		{
			var result = await CallAsync("get", challenge, allowCredentials, timeout);

			if (result is string)
			{
				throw new Exception(result);
			}
			else
			{
				var authData = Convert.FromBase64String((string)result.authenticatorDataBase64);

				return new CredentialsResponse
				{
					AuthenticatorData = new AuthenticatorData
					{
						Base64 = result.authenticatorDataBase64,
						UserPresent = result.authenticatorData.flags.userPresentFlag,
						UserVerified = result.authenticatorData.flags.userVerifiedFlag,
						BackupEligibility = result.authenticatorData.flags.backupEligibilityFlag,
						RPIDHash = Convert.FromBase64String(result.authenticatorData.rpIdHash),
					},
					ClientData = new ClientData
					{
						Challenge = FromBase64Url((string)result.clientData.challenge),
						Origin = result.clientData.origin,
						Base64 = result.clientDataBase64,
						Type = result.clientData.type
					},
					UserHandle = result.userHandle,
					Signature = Convert.FromBase64String(result.signature),
					SignatureCounter = ReadSignatureCounter(authData),
				};
			}
		}

		/// <summary>
		/// Validates the given attestation against the provided public key.
		/// </summary>
		/// <param name="publicKey">The public key generated during registration.</param>
		/// <param name="authenticatorDataBase64">"platform" (Windows Hello) vs "cross-platform" (Yubikey) required.</param>
		/// <param name="clientDataBase64">Client data base64 encoded.</param>
		/// <param name="signature">Authentication signature.</param>
		/// <returns>The success of the validation.</returns>
		/// <exception cref="Exception"></exception>
		public static bool Validate(
			PublicKey publicKey,
			string authenticatorDataBase64,
			string clientDataBase64,
			byte[] signature)
		{
			switch (publicKey.Algorithm)
			{
				case COSEAlgorithmIdentifier.ES256:
					return ValidateES256Signature(publicKey, clientDataBase64, authenticatorDataBase64, signature);

				case COSEAlgorithmIdentifier.EdDSA:
					return ValidateEdDSA(publicKey, clientDataBase64, authenticatorDataBase64, signature);

				case COSEAlgorithmIdentifier.RS256:
					return ValidateRS256(publicKey, clientDataBase64, authenticatorDataBase64, signature);

				default:
					return false;
			}
		}

		/// <summary>
		/// Validates an ES256 signature.
		/// </summary>
		/// <param name="publicKey">The public key generated during registration.</param>
		/// <param name="clientDataBase64">Client data base64 encoded.</param>
		/// <param name="authenticatorDataBase64">"platform" (Windows Hello) vs "cross-platform" (Yubikey) required.</param>
		/// <param name="signature">Authentication signature.</param>
		/// <returns></returns>
		private static bool ValidateES256Signature(
			PublicKey publicKey,
			string clientDataBase64,
			string authenticatorDataBase64,
			byte[] signature)
		{
			// parse public key data from client.
			var x = publicKey.Data["-2"];
			var y = publicKey.Data["-3"];

			var hasher = SHA256.Create();

			// let hash be the result of computing a hash over the cData using SHA-256.
			var clientDataBytes = Convert.FromBase64String(clientDataBase64);
			var hash = hasher.ComputeHash(clientDataBytes);

			// using the credential public key looked up in step 3, verify that sig is a valid signature over the binary concatenation of aData and hash.
			byte[] authenticatorData = Convert.FromBase64String(authenticatorDataBase64);
			var signatureBase = new byte[authenticatorData.Length + hash.Length];
			authenticatorData.CopyTo(signatureBase, 0);
			hash.CopyTo(signatureBase, authenticatorData.Length);

			var ecDsa = ECDsa.Create(new ECParameters
			{
				Curve = ECCurve.NamedCurves.nistP256,
				Q = new ECPoint
				{
					X = Convert.FromBase64String(x),
					Y = Convert.FromBase64String(y),
				}
			});

			return ecDsa.VerifyData(signatureBase, DeserializeSignature(signature), HashAlgorithmName.SHA256);
		}

		/// <summary>
		/// TODO: Validates an EdDSA signature.
		/// </summary>
		/// <param name="publicKey">The public key generated during registration.</param>
		/// <param name="clientDataBase64">Client data base64 encoded.</param>
		/// <param name="authenticatorDataBase64">"platform" (Windows Hello) vs "cross-platform" (Yubikey) required.</param>
		/// <param name="signature">Authentication signature.</param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		private static bool ValidateEdDSA(
			PublicKey publicKey,
			string clientDataBase64,
			string authenticatorDataBase64,
			byte[] signature)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Validates an RS256 signature.
		/// </summary>
		/// <param name="publicKey">The public key data received from the client upon registration.</param>
		/// <param name="clientDataBase64">Client data base64 encoded.</param>
		/// <param name="authenticatorDataBase64">"platform" (Windows Hello) vs "cross-platform" (Yubikey) required.</param>
		/// <param name="signature">Authentication signature.</param>
		/// <returns></returns>
		private static bool ValidateRS256(
			PublicKey publicKey,
			string clientDataBase64,
			string authenticatorDataBase64,
			byte[] signature)
		{
			// parse public key data from client.
			var keyType = publicKey.Data["1"];
			var modulus = publicKey.Data["-1"];
			var exponent = publicKey.Data["-2"];

			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.ImportParameters(new RSAParameters
			{
				Modulus = Convert.FromBase64String(modulus),
				Exponent = Convert.FromBase64String(exponent)
			});

			RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);

			rsaDeformatter.SetHashAlgorithm("SHA256");

			// get client data json hash.
			var hasher = SHA256.Create();
			var clientData = Convert.FromBase64String(clientDataBase64);

			var clientDataHash = hasher.ComputeHash(clientData);

			// combine authenticator data with client data hash.
			var authenticatorData = Convert.FromBase64String(authenticatorDataBase64);
			var signatureBase = new byte[authenticatorData.Length + clientDataHash.Length];

			authenticatorData.CopyTo(signatureBase, 0);
			clientDataHash.CopyTo(signatureBase, authenticatorData.Length);

			return rsaDeformatter.VerifySignature(hasher.ComputeHash(signatureBase), signature);
		}

		/// <summary>
		/// Converts a DER ECDSA signature to ASN.1 DER format.
		/// </summary>
		/// <param name="signature">The DER ECDSA-encoded signature</param>
		/// <returns>The ASN.1 encoded signature.</returns>
		/// <remarks>
		/// ASN.1 DER format is required to use nist.P256 validation for a signature.
		/// </remarks>
		private static byte[] DeserializeSignature(byte[] signature)
		{
			using (var ms = new MemoryStream(signature))
			{
				var header = ms.ReadByte(); // marker
				var b1 = ms.ReadByte(); // length of remaining bytes

				var markerR = ms.ReadByte(); // marker
				var b2 = ms.ReadByte(); // length of vr
				var vr = new byte[b2]; // signed big-endian encoding of r
				ms.Read(vr, 0, vr.Length);
				vr = RemoveAnyNegativeFlag(vr); // r

				var markerS = ms.ReadByte(); // marker 
				var b3 = ms.ReadByte(); // length of vs
				var vs = new byte[b3]; // signed big-endian encoding of s
				ms.Read(vs, 0, vs.Length);
				vs = RemoveAnyNegativeFlag(vs); // s

				var parsedSignature = new byte[vr.Length + vs.Length];
				vr.CopyTo(parsedSignature, 0);
				vs.CopyTo(parsedSignature, vr.Length);

				return parsedSignature;
			}
		}

		private static byte[] RemoveAnyNegativeFlag(byte[] input)
		{
			if (input[0] != 0) return input;

			var output = new byte[input.Length - 1];
			Array.Copy(input, 1, output, 0, output.Length);
			return output;
		}

		/// <summary>
		/// Decodes a base64url value (RFC 4648 section 5), with or without padding.
		/// </summary>
		/// <remarks>
		/// The browser returns clientData.challenge base64url-encoded without padding,
		/// which <see cref="Convert.FromBase64String(string)"/> cannot decode directly.
		/// Standard base64 input is also accepted.
		/// </remarks>
		/// <param name="value">The base64url-encoded value.</param>
		/// <returns>The decoded bytes.</returns>
		private static byte[] FromBase64Url(string value)
		{
			if (value == null)
				return null;

			var s = value.Replace('-', '+').Replace('_', '/');
			switch (s.Length % 4)
			{
				case 2: s += "=="; break;
				case 3: s += "="; break;
			}
			return Convert.FromBase64String(s);
		}

		/// <summary>
		/// Reads the unsigned 32-bit big-endian signature counter (bytes 33-36) from the authenticator data.
		/// </summary>
		/// <remarks>
		/// See: <see href="https://w3c.github.io/webauthn/#sctn-authenticator-data"/>.
		/// Values above <see cref="int.MaxValue"/> wrap because <see cref="CredentialsResponse.SignatureCounter"/> is an <see cref="int"/>.
		/// </remarks>
		/// <param name="authData">The raw authenticator data.</param>
		/// <returns>The signature counter, or 0 if the data is too short.</returns>
		private static int ReadSignatureCounter(byte[] authData)
		{
			if (authData == null || authData.Length < 37)
				return 0;

			return unchecked((int)(
				(uint)authData[33] << 24 |
				(uint)authData[34] << 16 |
				(uint)authData[35] << 8 |
				authData[36]));
		}

		#region Wisej Implementation

		private static Task<dynamic> CallAsync(string method, params object[] args)
		{
			return Application.CallAsync($"wisej.ext.WebAuthn.{method}", args);
		}

		#endregion

	}
}