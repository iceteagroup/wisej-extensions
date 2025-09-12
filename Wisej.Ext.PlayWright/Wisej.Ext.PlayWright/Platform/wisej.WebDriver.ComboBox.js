Wisej.WebDriver.ComboBox = {};


Wisej.WebDriver.ComboBox.getComboBoxSelectedText = function () {
	var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	if (widget.classname == "wisej.web.LabelWrapper") widget = widget.getEditor();
	widget.selectAllText();
	return widget.getTextSelection();
};

Wisej.WebDriver.ComboBox.openComboBox = function () {
	var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	if (widget.classname == "wisej.web.LabelWrapper") widget = widget.getEditor();
	widget.open();
};

Wisej.WebDriver.ComboBox.setComboBoxSelectedIndex = function () {
	var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	if (widget.classname == "wisej.web.LabelWrapper") widget = widget.getEditor();
	widget.setSelectedIndex(arguments[1]);
	widget.fireDataEvent("selectionChanged", arguments[1]);
};

Wisej.WebDriver.ComboBox.getComboBoxSelectedIndex = function () {
	var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	if (widget.classname == "wisej.web.LabelWrapper") widget = widget.getEditor();
	return widget.getSelectedIndex();
};