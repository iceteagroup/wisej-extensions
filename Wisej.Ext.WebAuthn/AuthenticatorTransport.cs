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

using System.ComponentModel;

namespace Wisej.Ext.WebAuthn
{
	/// <summary>
	/// Available transport types.
	/// </summary>	
	[ApiCategory("WebAuthn")]
	public enum AuthenticatorTransport
    {
        /// <summary>
        /// Indicates the respective authenticator can be contacted over removable USB.
        /// </summary>
        Usb,

        /// <summary>
        /// Indicates the respective authenticator can be contacted over Bluetooth Smart (Bluetooth Low Energy / BLE).
        /// </summary>
        Ble,

        /// <summary>
        /// Indicates the respective authenticator can be contacted over Near Field Communication (NFC).
        /// </summary>
        Nfc,

        /// <summary>
        /// Indicates the respective authenticator is contacted using a client device-specific 
        /// transport, i.e., it is a platform authenticator. 
        /// </summary>
        Internal
    }
}
