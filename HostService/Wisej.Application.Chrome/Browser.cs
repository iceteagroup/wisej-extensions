///////////////////////////////////////////////////////////////////////////////
//
// (C) 2017 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using CefSharp;
using CefSharp.WinForms;
using System.Windows.Forms;

namespace Wisej.Application
{
	/// <summary>
	/// Chromium Web Browser.
	/// </summary>
	internal class Browser : ChromiumWebBrowser, IKeyboardHandler
	{
		public Browser(string url)
			:base(url)
		{
			this.KeyboardHandler = this;
		}

		#region IKeyboardHandler

		bool IKeyboardHandler.OnKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
		{
			return false;
		}

		bool IKeyboardHandler.OnPreKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
		{
			if (type == KeyType.RawKeyDown)
			{
				bool isShiftPressed = (modifiers & CefEventFlags.ShiftDown) == CefEventFlags.ShiftDown;
				bool isCtrlPressed = (modifiers & CefEventFlags.ControlDown) == CefEventFlags.ControlDown;
				bool isAltPressed = (modifiers & CefEventFlags.AltDown) == CefEventFlags.AltDown;

				Keys key = (Keys)windowsKeyCode;

				if (isCtrlPressed)
					key = key | Keys.Control;

				if (isShiftPressed)
					key = key | Keys.Shift;

				if (isAltPressed)
					key = key | Keys.Alt;

				OnPreviewKeyDown(new PreviewKeyDownEventArgs(key));
			}

			return false;
		}

		#endregion
	}
}
