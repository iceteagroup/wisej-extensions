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


/**
 * wisej.web.ribbonBar.TabView
 *
 * Represents the inner tab container in a wisej.web.RibbonBar control.
 */
qx.Class.define("wisej.web.ribbonBar.TabView", {

	extend: qx.ui.tabview.TabView,

	// All Wisej controls must include this mixin
	// to provide services to the Wisej core.
	include: [wisej.mixin.MWisejControl],

	construct: function () {

		this.base(arguments);

		// the tab control needs the controls in the
		// order they are declared to display the tabs in 
		// the correct sequence.
		this.setReverseControls(false);

		// RightToLeft support.
		this.addListener("changeRtl", this._onRtlChange, this);
	},

	properties: {

	},

	members: {

		/**
		 * Returns or changes the current page.
		 */
		getSelectedIndex: function () {

			var page = this.getSelection()[0];
			return this.indexOf(page);
		},
		setSelectedIndex: function (value) {

			if (value == -1) {
				this.resetSelection();
			}
			else {

				var index = value;
				var pages = this.getChildren();

				if (this.getContentElement().getDomElement() == null) {
					this.addListenerOnce("appear", function (e) {
						this.setSelectedIndex(index);
					}, this);
				}
				else {
					if (index < pages.length) {
						this.setSelection([pages[index]]);
					}
				}
			}
		},

		// Listens to "changeRtl" to mirror the widgets in the tabview bar.
		_onRtlChange: function (e) {

			if (e.getData() === e.getOldData())
				return;

			var rtl = e.getData();
			if (rtl != null) {
				this.getChildControl("bar")._mirrorChildren(rtl);
			}
		},

		/**
		 * getTabRects
		 *
		 * Returns an array containing the bounds for all the visible tabs.
		 */
		getTabRects: function () {

			// add the bounds of the tab-buttons container.
			var appButton = this.getLayoutParent().getAppButton();
			var barBounds = this.getChildControl("bar").getBounds();

			// shift the tab buttons by the width of the app button.
			if (appButton && appButton.getBounds() && barBounds) {
				barBounds.left += appButton.getBounds().width;
			}

			var rects = [];
			var pages = this.getChildren();
			for (var i = 0; i < pages.length; i++) {

				var bounds = pages[i].getButton().getBounds();
				if (!bounds || !barBounds) {
					rects.push(null);
				}
				else {
					rects.push({
						top: bounds.top += barBounds.top,
						left: bounds.left += barBounds.left,
						width: bounds.width,
						height: bounds.height
					});
				}
			}

			return rects;
		},
	}

});
