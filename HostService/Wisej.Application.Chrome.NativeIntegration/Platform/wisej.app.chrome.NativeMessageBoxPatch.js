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

if (CefSharp !== undefined) {

	/**
	 * wisej.app.chrome.NativeMessageBoxPatch
	 * 
	 * Replaces the show and close methods of the wisej.web.MessageBox
	 * widget to redirect the call to the native message box exposed by
	 * the chromium embedded application.
	 */
	qx.Mixin.define("wisej.app.chrome.NativeMessageBoxPatch", {

		members: {

			show: function () {

				this.exclude();
				try {
					var ret = NativeMessageBox.show(
						this.getTitle(),
						this.getMessage(),
						this.translateButtons(),
						this.translateIcon(),
						this.translateDefButton()
					);

					this.close(this.translateReturnValue(ret));

				} catch (ex) {
					alert(ex);
				}
			},

			close: function (ret) {

				try {

					// terminate the modal state on the server.
					this.fireDataEvent("close", ret);

					// activate the owner window
					var owner = this.getOwner();
					if (owner instanceof qx.ui.core.Widget) {

						owner.activate();
						if (owner instanceof qx.ui.window.Window)
							owner.setActive(true);
					}

				} catch (ex) {

					// ignore.
				}

				this.destroy();
			},

			translateButtons: function () {

				var buttons = this.getButtons();

				if (buttons.abort && buttons.retry && buttons.ignore) return 2;
				if (buttons.retry && buttons.cancel) return 5;
				if (buttons.yes && buttons.no && buttons.cancel) return 3;
				if (buttons.yes && buttons.no) return 4;
				if (buttons.cancel) return 1;

				return 0;
			},

			translateIcon: function () {

				var image = this.getImage();

				switch (image) {

					case "error": return 16;
					case "question": return 32;
					case "warning": return 48;
					case "information": return 64;
					case "hand": return 16;
					case "stop": return 16;
					default: return 0;
				}
			},

			translateDefButton: function () {

				var value = 0;
				var buttons = this.getButtons();
				var defButton = this.getDefaultButton();
				for (var id in buttons) {

					if (id === defButton)
						return value;

					value += 256;

					if (value > 512)
						break;
				}

				return 0;
			},

			translateReturnValue: function (id) {

				switch (id) {
					case 1: return "ok";
					case 2: return "cancel";
					case 3: return "abort";
					case 4: return "retry";
					case 5: return "ignore";
					case 6: return "yes";
					case 7: return "no";
					default: return "none";
				}
			}
		}
	});

	qx.Class.patch(wisej.web.MessageBox, wisej.app.chrome.NativeMessageBoxPatch);

	CefSharp.BindObjectAsync("NativeMessageBox", "NativeMessageBox");
}
