Wisej.WebDriver.TreeView = {};

Wisej.WebDriver.TreeView.getTreeViewNodes = function () {

	var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	if (widget) {
		var rootNodes = widget.getChildren();
		if (rootNodes && rootNodes.length > 0) {
			var rootNode = rootNodes[0];
			var nodes = rootNode.getItems(false, false, true);
			for (var i = 0; i < nodes.length; i++) {
				if (nodes[i] instanceof qx.ui.core.Widget)
					nodes[i] = nodes[i].getContentElement().getDomElement();
			}

			return nodes;
		} else {
			return [];
		}
	}
	return null;
};