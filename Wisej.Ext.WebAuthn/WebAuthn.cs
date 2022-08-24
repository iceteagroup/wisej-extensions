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
using System.Text;
using System.Threading.Tasks;
using Wisej.Web;

namespace Wisej.Ext.WebAuthn
{
	/// <summary>
	/// Provides methods for creating and retrieving credentials from the client following
	/// the Web Authorization API standards.
	/// </summary>
	public static class WebAuthn
	{
		/// <summary>
		/// Checks whether the client device has a user-verifying platform authenticator available for use.
		/// </summary>
		/// <returns>True if the device has a user-verifying platform authenticator.</returns>
		public static async Task<bool> IsUserVerifyingPlatformAuthenticatorAvailableAsync()
        {
			return await Instance.CallAsync("isUserVerifyingPlatformAuthenticatorAvailable");
        }

        /// <summary>
        /// Creates new credentials for the client.
        /// </summary>
        /// <param name="challenge"></param>
        /// <param name="rp"></param>
        /// <param name="user"></param>
        /// <param name="publicKeyCredentialParameters"></param>
        /// <param name="authenticatorSelection"></param>
        /// <param name="timeout"></param>
        /// <param name="attestation"></param>
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
            var result = await Instance.CallAsync("create", 
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
						Challenge = Convert.FromBase64String(result.clientData.challenge),
						Origin = result.clientData.origin,
						Type = result.clientData.type
					}
				};
			}
		}

        /// <summary>
        /// Gets the requested credentials from the client.
        /// </summary>
        /// <param name="challenge"></param>
        /// <param name="allowCredentials"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static async Task<CredentialsResponse> GetAsync(
			string challenge, 
			PublicKeyCredentialDescriptor allowCredentials, 
			int timeout)
        {
			var result = await Instance.CallAsync("get", challenge, allowCredentials, timeout);

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
						Base64 = result.authenticatorDataBase64,
						UserPresent = result.authenticatorData.flags.userPresentFlag,
						UserVerified = result.authenticatorData.flags.userVerifiedFlag,
						BackupEligibility = result.authenticatorData.flags.backupEligibilityFlag,
						RPIDHash = Convert.FromBase64String(result.authenticatorData.rpIdHash),
					},
					ClientData = new ClientData
					{
						Challenge = Convert.FromBase64String(result.clientData.challenge),
						Origin = result.clientData.origin,
						Base64 = result.clientDataBase64,
						Type = result.clientData.type
					},
					UserHandle = result.userHandle,
					Signature = Convert.FromBase64String(result.signature),
				};
			}
		}

        /// <summary>
        /// Validates the given attestation against the provided public key.
        /// </summary>
        /// <param name="publicKey">The public key generated during registration.</param>
        /// <param name="authenticatorDataBase64"></param>
        /// <param name="clientDataBase64"></param>
        /// <param name="signature"></param>
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
        /// Valides an ES256 signature.
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="clientDataBase64"></param>
        /// <param name="authenticatorDataBase64"></param>
        /// <param name="signature"></param>
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
        /// TODO: Valide an EdDSA signature.
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="clientDataBase64"></param>
        /// <param name="authenticatorDataBase64"></param>
        /// <param name="signature"></param>
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
        /// <param name="clientDataBase64"></param>
        /// <param name="authenticatorDataBase64"></param>
        /// <param name="signature"></param>
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

		#region Wisej Implementation

		private const string INSTANCE_KEY = "Wisej.Ext.WebAuthn";

		private static WebAuthnComponent Instance
		{
			get
			{
				var instance = Application.Session[INSTANCE_KEY];
				if (instance == null)
				{
					instance = new WebAuthnComponent();
					Application.Session[INSTANCE_KEY] = instance;
				}
				return instance;
			}
		}

		// Connection to the client component.
		private class WebAuthnComponent : Component
		{
			protected override void OnWebRender(dynamic config)
			{
				base.OnWebRender((object)config);

				config.className = "wisej.ext.WebAuthn";
			}

			protected override void OnWebEvent(Core.WisejEventArgs e)
			{
				switch (e.Type)
				{

					default:
						base.OnWebEvent(e);
						break;
				}
			}
		}

        #endregion

    }
}