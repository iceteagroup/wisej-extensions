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
 * wisej.web.ribbonBar.RibbonGroupLayout
 *
 * Layout engine for the wisej.web.ribbonBar.RibbonGroup control.
 *
 * The children are laid out vertically as in a vertical-flow layout wrapping to a
 * new column when the layout property <code>break</code> is set to true.
 *
 * The children's height is stretched vertically according to the value of the 
 * <code>flex</code> layout property and the <code>allowGrowY</code> widget property.
 *
 * *Item Properties*
 *
 * <ul>
 * <li><strong>flex</strong> <em>(Integer)</em>: The flexibility of a layout item determines how the container
 *   distributes remaining empty space among its children. If items are made
 *   flexible, they can grow or shrink accordingly. Their relative flex values
 *   determine how the items are being resized, i.e. the larger the flex ratio
 *   of two items, the larger the resizing of the first item compared to the
 *   second.
 *
 *   If there is only one flex item in a layout container, its actual flex
 *   value is not relevant. To disallow items to become flexible, set the
 *   flex value to zero.
 * </li>
 * <li><strong>columnBreak</strong> <em>(String)</em>: Indicates that the following
 *   widgets should be located in a new vertical column.
 * </li>
 * <li><strong>newColumn</strong> <em>(String)</em>: Indicates that this widget
 *   should be located in a new vertical column.
 * </li>
 * </ul>*/
qx.Class.define("wisej.web.ribbonBar.RibbonGroupLayout", {

	extend: qx.ui.layout.Abstract,

	/**
	 * @param spacingX {Integer?0} The horizontal spacing between columns.
	 *     Sets {@link #spacingX}.
	 * @param spacingY {Integer?0} The vertical spacing between widgets.
	 *     Sets {@link #spacingY}.
	 */
	construct: function (spacingX, spacingY) {

		this.base(arguments);

		this.__rowData = [];
		this.__colData = [];

		if (spacingX) {
			this.setSpacingX(spacingX);
		}

		if (spacingY) {
			this.setSpacingY(spacingY);
		}
	},

	properties: {

		/**
		 * The horizontal spacing between columns.
		 */
		spacingX: { check: "Integer", init: 0, apply: "_applyLayoutChange" },

		/**
		 * The vertical spacing between widgets.
		 */
		spacingY: { check: "Integer", init: 0, apply: "_applyLayoutChange" },
	},

	members: {

		/** @type {Array} 2D array of cell data */
		__rowData: null,
		__colData: null,

		// overridden
		verifyLayoutProperty: qx.core.Environment.select("qx.debug",
		{
			"true": function (item, name, value) {
				var layoutProperties = {
					"break": 1,
					"flex": 1
				}
				this.assert(layoutProperties[name] == 1, "The property '" + name + "' is not supported by the RibbonGroupLayout layout!");
			},

			"false": null
		}),

		// overridden
		renderLayout: function (availWidth, availHeight, padding) {

			var marginTop = 0;
			var marginLeft = 0;
			var marginRight = 0;
			var marginBottom = 0;
			var spacingX = this.getSpacingX();
			var spacingY = this.getSpacingY();
			var hint, left, top, width, height;
			var colData = this.__buildColumns();
			var column, columnFlex, columnFlexHeight, columnWidth, totalHeight;

			left = padding.left;
			for (var i = 0; i < colData.length; i++) {

				column = colData[i];
				totalHeight = availHeight;
				columnFlex = this.__computeColumnFlex(column);
				columnWidth = this.__computeColumnWidth(column);
				columnFlexHeight = columnFlex > 0 ? this.__computeColumnFlexHeight(column, availHeight) : 0;

				top = padding.top;
				width = columnWidth;

				for (var j = 0; j < column.length; j++) {

					var child = column[j];
					hint = child.getSizeHint();
					height = hint.height;
					marginTop = child.getMarginTop() | 0;
					marginLeft = child.getMarginLeft() | 0;
					marginRight = child.getMarginRight() | 0;
					marginBottom = child.getMarginBottom() | 0;

					if (columnFlex > 0) {
						if (child.getAllowGrowY()) {
							var flex = child.getLayoutProperties().flex | 0;
							if (flex > 0) {
								height = Math.max(hint.minHeight, columnFlexHeight / columnFlex * flex);
							}
						}
					}

					totalHeight = Math.max(0, totalHeight - height - spacingY);

					child.renderLayout(
						left + marginLeft,
						top + marginTop,
						width - marginLeft - marginRight,
						height);

					top += height + spacingY + marginTop + marginBottom;
				}

				left += width + spacingX;
			}
		},

		// overridden
		_computeSizeHint: function () {

			var width = 0, height = 0;
			var colData = this.__buildColumns();
			var column, columnWidth, columnHeight;
			var spacingX = this.getSpacingX();

			for (var i = 0; i < colData.length; i++) {

				column = colData[i];
				columnWidth = this.__computeColumnWidth(column);
				columnHeight = this.__computeColumnHeight(column);

				width += columnWidth + spacingX;
				height = Math.max(height, columnHeight);
			}

			width -= spacingX;

			var hint = {
				minWidth: width,
				width: width,
				minHeight: height,
				height: height
			};

			return hint;
		},

		__computeColumnFlex: function (widgets) {
			var totalFlex = 0;
			for (var i = 0; i < widgets.length; i++) {
				var widget = widgets[i];
				if (widget.getAllowGrowY())
					totalFlex += widget.getLayoutProperties().flex | 0;
			}
			return totalFlex;
		},

		__computeColumnFlexHeight: function (widgets, availHeight) {

			var spacingY = this.getSpacingY();
			for (var i = 0; i < widgets.length; i++) {
				var widget = widgets[i];
				var hint = widget.getSizeHint();
				if (!widget.getAllowGrowY() || !widget.getLayoutProperties().flex) {
					availHeight = availHeight - hint.height;
				}
				availHeight -= spacingY;
			}

			if (widgets.length > 0)
				availHeight += spacingY;

			return availHeight;
		},

		__computeColumnWidth: function (widgets) {
			var columnWidth = 0;
			for (var i = 0; i < widgets.length; i++) {
				var hint = widgets[i].getSizeHint();
				var margin = widgets[i].getMarginLeft() + widgets[i].getMarginRight();
				columnWidth = Math.max(columnWidth, hint.width + margin);
			}
			return columnWidth;
		},

		__computeColumnHeight: function (widgets) {
			var columnHeight = 0;
			var spacingY = this.getSpacingY();
			for (var i = 0; i < widgets.length; i++) {
				var hint = widgets[i].getSizeHint();
				columnHeight += hint.height + spacingY;
				columnHeight += widgets[i].getMarginTop() + widgets[i].getMarginBottom();
			}
			columnHeight -= spacingY;
			return columnHeight;
		},

		// Collects and organizes all the child widgets into columns.
		__buildColumns: function () {

			var colData = [];
			var column = null;
			var children = this._getLayoutChildren();

			for (var i = 0; i < children.length; i++) {

				var child = children[i];
				var props = child.getLayoutProperties();

				// this child starts a new column?
				if (column == null || props.newColumn === true) {
					column = [];
					colData.push(column);
				}

				column.push(child);

				// next child starts a new column?
				if (props.columnBreak === true && (i + 1) < children.length) {
					column = [];
					colData.push(column);
				}

			}

			return colData;
		},
	}

});