Wisej.WebDriver.ListBox = {};

Wisej.WebDriver.ListBox.setListBoxSelectedItem = function () {
	var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	var itemView = widget.itemView;

	listView.setFocusedItem(arguments[1]);
};

Wisej.WebDriver.ListBox.clickFocusedListBoxItem = function () {
	var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	var itemView = widget.itemView;
	var focusedItem = itemView.getFocusedItem();
	widget.fireDataEvent("itemClick", focusedItem);
};

Wisej.WebDriver.ListBox.clickAndFocusListBoxItem = function () {
	var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	var itemView = widget.itemView;

	itemView.setFocusedItem(arguments[1]);
	widget.fireDataEvent("itemClick", arguments[1]);
};