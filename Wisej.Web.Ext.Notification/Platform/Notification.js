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
 * wisej.web.ext.Notification
 *
 * Adds support for the Notification API: https://developer.mozilla.org/en-US/docs/Web/API/notification.
 *
 * The Notification interface of the Notifications API is used to configure and display desktop notifications to the user.
 */
qx.Class.define("wisej.web.ext.Notification", {

	extend: qx.core.Object,

	// All Wisej components must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejComponent],

	members: {

		/**
		 * Displays the desktop notification.
		 *
		 * @param options {Map} Defines the following options:
		 *
		 *			title {String} Defines a title for the notification, which will be shown at the top of the notification window when it is fired.
		 *			body {String} The body text of the notification, which will be displayed below the title.
		 *			icon {String} The URL of an icon to be displayed as part of the notification.
		 *			language {String} The notification's language, specified using a BCP 47 language tag.
		 *			showOnClick {Boolean} When true, it activates the browser when the user clicks the notification.
		 *
		 */
		show: function (options) {

			// supported?
			if (!("Notification" in window))
				return;

			// already granted? display.
			if (Notification.permission == "granted") {

				this.__show(options);
				return;
			}

			// not denied yet? ask and display.
			if (Notification.permission !== 'denied') {
				var me = this;
				Notification.requestPermission(function (permission) {
					if (permission === "granted") {

						me.__show(options);
					}
				});
			}
		},

		// Implementation of the show() method.
		__show: function(options)
		{

			var me = this;
			var title = options.title;
			var body = options.body;
			var icon = options.icon;
			var language = options.language;
			var showOnClick = options.showOnClick;

			var notification = new Notification(title, { body: body, icon: icon, lang: language });

			notification.onclick = function (e) {

				if (showOnClick)
					window.focus();

				me.fireDataEvent("click", title);
			};
		}
	}
});
