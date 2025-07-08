//# sourceURL=wisej.ext.FuzzySort.js

qx.Class.define("wisej.ext.FuzzySort", {

	statics: {

		/**
		 * Initializes the ComboBoxHelper for a given ComboBox.
		 * Sets up listeners and caches the original items.
		 * @param {qx.ui.form.ComboBox} comboBox - The ComboBox instance
		 */
		initialize(comboBox) {

			var dropDownList = comboBox.getDropDownList();

			// shows or hides the list items that match the text.
			// should be used with the "filter" auto complete mode.
			/**
			 * Default implementation of list the items filter.
			 * An application may replace this method with a custom function.
			 *
			 * @param {String} label Label of the item being filtered.
			 * @param {String} text Text typed by the user.
			 * @returns {Boolean} true if the item show be visible, false to hide it.
			 */
			comboBox.onFilterListItem = function (label, text) {

				if (text && label) {

					// match anywhere in the label. case insensitive.
					if (label.toLowerCase().indexOf(text.toLowerCase()) !== -1)
						return true; // show.
				}
				else {
					// show the text is empty.
					return true;
				}

				// hide.
				return false;
			}

			dropDownList.findItemByLabelFuzzy = function (search, startIndex) {

				if (!search) {
					return null;
				}

				// lowercase search term for case-insensitive matching
				search = search.toLowerCase();

				// get all items in the dropdown list
				const items = this.getChildren();
				const itemLabels = items.map(item => item.isEnabled() && this._getItemText(item)).filter(Boolean);

				// perform fuzzy search using fuzzysort
				const results = fuzzysort.go(search, itemLabels);

				// Highlight matches
				for (let i = 0; i < items.length; i++) {
					const label = this._getItemText(items[i]);
					const match = results.find(result => result.target === label);

					if (match) {
						const highlighted = match.highlight('<span style="color: red;">', '</span>');
						items[i].setRich(true);
						items[i].setLabel(highlighted); // update label with highlighted match
					} else {
						items[i].setLabel(label); // restore original label if no match
					}
				}

				if (results.length === 0) {
					return null; // no matches
				}

				const bestMatchIndex = results[0].indexes[0];
				const bestMatch = items[bestMatchIndex];

				return bestMatch;
			}
		}
	},
});
