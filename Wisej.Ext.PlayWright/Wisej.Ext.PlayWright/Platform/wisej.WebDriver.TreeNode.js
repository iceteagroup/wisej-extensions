Wisej.WebDriver.TreeNode = {};

Wisej.WebDriver.TreeNode.getTreeNodeChildren = function () {
	var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);

	if (widget) {
		var rootNodes = widget.getChildren();
		if (rootNodes && rootNodes.length > 0) {
			var nodes = [];
			for (var i = 0; i < rootNodes.length; i++) {
				if (rootNodes[i] instanceof qx.ui.core.Widget)
					nodes[i] = (rootNodes[i].getContentElement().getDomElement());
			}

			return nodes;
		}
		return [];
	}
	return null;
}

Wisej.WebDriver.TreeNode.scrollChildNodeToView = function () {
	var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	TreeView.main.scrollChildIntoView(widget);
};

Wisej.WebDriver.TreeNode.scrollSubNodeToView = function () {
	var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	var parent = widget.getParent();
	parent.scrollChildIntoView(widget);
};

Wisej.WebDriver.TreeNode.getTreeNodeLabel = function () {
	var treeNode = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	return treeNode.getLabel();
};

Wisej.WebDriver.TreeNode.setTreeNodeOpened = function () {
	var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	var opened = arguments[1];
	return widget.setOpen(opened);
};

Wisej.WebDriver.TreeNode.setTreeNodeOpenedByRowIndex = function () {
	var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	var rowIdx = arguments[1];
	var opened = arguments[2];
	var dm = widget.getDataModel();
	var node = dm.getNodeFromRow(rowIdx);
	widget.nodeSetOpened(node, opened);
};