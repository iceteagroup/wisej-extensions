Wisej.WebDriver = {};
Wisej.WebDriver.callMethod = function() {

    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    if (widget) {
        var name = arguments[1];
        var args = arguments[2];
        console.log("callMethod: ", name, " args: ", args);

        var func = widget[name];
        if (func == null)
            throw new Error("Unable to find method " + widget.name + "." + name);

        var retval = func.apply(widget, args);

        if (retval instanceof qx.ui.core.Widget)
            retval = retval.getContentElement().getDomElement();

        if (retval instanceof Array) {
            for (var i = 0; i < retval.length; i++) {
                if (retval[i] instanceof qx.ui.core.Widget)
                    retval[i] = retval[i].getContentElement().getDomElement();
            }
        }

        return retval;
    } else {
        throw new Error("Unable to find widget " + arguments[0]);
    }
    return null;
};


Wisej.WebDriver.getAllAlertBoxes = function() {

    var ret = [];
    var root = qx.core.Init.getApplication().getRoot();
    var children = root.getChildren();
    if (children && children.length > 0) {
        for (var i = 0; i < children.length; i++) {
            if (children[i] instanceof wisej.web.alertbox.DockingPane) {
                var items = children[i].getChildren();
                if (items && items.length > 0) {
                    for (var j = 0; j < items.length; j++) {
                        if (items[j] instanceof wisej.web.AlertBox)
                            ret.push(items[j].getContentElement().getDomElement());
                    }
                }
            }
        }
    }
    return ret;
};

Wisej.WebDriver.getAlertBoxCount = function() {

    var ret = [];
    var root = qx.core.Init.getApplication().getRoot();
    var children = root.getChildren();
    if (children && children.length > 0) {
        for (var i = 0; i < children.length; i++) {
            if (children[i] instanceof wisej.web.alertbox.DockingPane) {
                var items = children[i].getChildren();
                if (items && items.length > 0) {
                    for (var j = 0; j < items.length; j++) {
                        if (items[j] instanceof wisej.web.AlertBox)
                            ret.push(items[j]);
                    }
                }
            }
        }
    }

    return ret.length;
};

Wisej.WebDriver.getAllLogEvents = function() {
    var ret = [];
    Wisej.WebDriver.appender.getAllLogEvents().forEach(function(entry) {
        var jsonEntry = {
            clazz: entry.clazz ? entry.clazz.toString() : null,
            time: entry.time.toString(),
            level: entry.level,
            items: entry.items.map(function(item) {
                if (item.text instanceof Array) {
                    return item.text.map(function(obj) {
                        return obj.text;
                    }).join(" ");
                } else {
                    return item.text;
                }
            })
        };
        ret.push(JSON.stringify(jsonEntry));
    });
    return ret;
};

Wisej.WebDriver.getAllMessageBoxes = function() {

    var ret = [];
    var root = qx.core.Init.getApplication().getRoot();
    var windows = root.getWindows();
    var children = root.getChildren();
    if (children && children.length > 0) {
        for (var i = 0; i < children.length; i++) {
            if (children[i] instanceof wisej.web.Desktop) {
                windows = children[i].getWindows();
                break;
            }
        }
    }
    if (windows && windows.length > 0) {
        for (var i = 0; i < windows.length; i++) {
            if (windows[i] instanceof wisej.web.MessageBox)
                ret.push(windows[i].getContentElement().getDomElement());
        }

    }
    return ret.length;
};

Wisej.WebDriver.getCaughtErrors = function() {
    return Wisej.WebDriver.globalErrors.map(function(ex) {
        var exString = "";
        if (typeof ex.getSourceException == "function") {
            ex = ex.getSourceException();
        }
        if (qx.core.WindowError && ex instanceof qx.core.WindowError) {
            exString = ex.toString() + " in " + ex.getUri() + " line " + ex.getLineNumber();
        } else {
            exString = ex.name + ": " + ex.message;
        }
        if (ex.fileName) {
            exString += " in file " + ex.fileName;
        }
        if (ex.lineNumber) {
            exString += " line " + ex.lineNumber;
        }
        if (ex.columnNumber) {
            exString += " column " + ex.columnNumber;
        }
        var stack = ex.stack || ex.stacktrace;
        if (!stack && qx.dev.StackTrace && qx.dev.StackTrace.getStackTraceFromError) {
            stack = qx.dev.StackTrace.getStackTraceFromError(ex).join("\n");
        }
        var lines = stack.split("\n");
        if (lines[0] == exString) {
            stack = lines.slice(1).join("\n");
        }
        if (stack) {
            exString += "\n" + stack;
        }

        return exString;
    });
};

Wisej.WebDriver.getChildControl = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    return widget.getChildControl(arguments[1]).getContentElement().getDomElement().outerHTML;
};

Wisej.WebDriver.getChildrenElements = function() {
    var childrenElements = [];
    var widget = Wisej.WebDriver.getWidgetByElement(arguments[0]);
    widget.getChildren().forEach(function(child) {
        if (child.getContentElement && child.getContentElement()) {
            var contentElement = child.getContentElement();
            if (contentElement.nodeType && contentElement.nodeType === 1) {
                childrenElements.push(contentElement);
            }
            if (contentElement.getDomElement && contentElement.getDomElement()) {
                childrenElements.push(contentElement.getDomElement());
            }
        }
    });
    return childrenElements;
};

Wisej.WebDriver.getChildren = function() {
    var childrenElements = [];
    var widget = Wisej.WebDriver.getWidgetByElement(arguments[0]);
    childrenElements =  widget.getChildren();
    return childrenElements;
};

Wisej.WebDriver.getClassName = function() {
    console.log("From JS: " + arguments[0]);

    if (typeof (arguments[0]) === 'string') {
        var markup = arguments[0];
        var parser = new DOMParser()
        var el = parser.parseFromString(markup, "text/xml");

        var widget = Wisej.WebDriver.getWidgetByElement(el);
    }

    if(!arguments[0].tagName){
        var widget = arguments[0];
    }
    if (typeof(arguments[0]) !== "string") {
        var widget = Wisej.WebDriver.getWidgetByElement(arguments[0]);
    }

   
    
    return widget.classname;
};

Wisej.WebDriver.getDomElementFromWidget = function(){
    var widget = arguments[0];
    console.log(widget);
    var domElement = document.createElement("div");
    
    if (widget.getDomElement() != null) {
        domElement = widget.getDomElement();
    }
    else {
        return wisget.getDomElement();
    }
    return domElement;
}

Wisej.WebDriver.getColumnCount = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    var model = widget.getTableModel();
    return model.getColumnCount();
};

Wisej.WebDriver.getContentElement = function() {
    if (!arguments[0].tagName) {
        var widget = arguments[0];
    }
    else if (arguments[0] != null) {
        var widget = Wisej.WebDriver.getWidgetByElement(arguments[0]);
    }
    else if (typeof (arguments[0]) === "string") {
        var markup = arguments[0];
        var parser = new DOMParser()
        var el = parser.parseFromString(markup, "text/xml");

        var widget = Wisej.WebDriver.getWidgetByElement(el);
    }
    var contentElement = widget.getContentElement();
    if (!contentElement) {
        throw new Error("Widget " + widget.toString() + " has no content element!");
    }

    /* contentElement is the DOM element in qx.ui.mobile.core.Widget */
    if (contentElement.nodeType && contentElement.nodeType === 1) {
        return contentElement;
    }
    console.log("WIDGET " + widget);
    console.log("CONTENT "+contentElement);
    return contentElement;
};

Wisej.WebDriver.getElementFromProperty = function() {
    var widget = Wisej.WebDriver.getWidgetByElement(arguments[0]);
    var propVal = widget.get(arguments[1]);

    try {
        var contentElement = propVal.getContentElement();
        if (contentElement.nodeType && contentElement.nodeType === 1) {
            return contentElement;
        }
        return contentElement.getDomElement();
    } catch (ex) {
        throw new Error("Couldn't get DOM element from widget " + propVal.toString() + ": " + ex.message);
    }
};

Wisej.WebDriver.getElementsFromProperty = function() {
    var getDomElement = function(widget) {
        if (widget.getContentElement && widget.getContentElement()) {
            var contentElement = widget.getContentElement();
            if (contentElement.nodeType && contentElement.nodeType === 1) {
                return contentElement;
            }
            if (contentElement.getDomElement && contentElement.getDomElement()) {
                return contentElement.getDomElement();
            }
        }
        return null;
    };

    var widgets = [];
    var widget = Wisej.WebDriver.getWidgetByElement(arguments[0]);
    var value = widget.get(arguments[1]);
    var isDataArray = value instanceof qx.data.Array;

    for (var i = 0, l = value.length; i < l; i++) {
        var item = isDataArray ? value.getItem(i) : value[i];
        var result = getDomElement(item);
        if (result) {
            widgets.push(result);
        }
    }
    return widgets;
};

Wisej.WebDriver.getFirstVisibleTableRow = function() {
    var scroller = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    return scroller.getTablePane().getFirstVisibleRow();
};

Wisej.WebDriver.getInheritance = function() {
    var widget = Wisej.WebDriver.getWidgetByElement(arguments[0]);
    var hierarchy = [];
    var clazz = widget.constructor;
    while (clazz && clazz.classname) {
        hierarchy.push(clazz.classname);
        clazz = clazz.superclass;
    }
    return hierarchy;
};

Wisej.WebDriver.getInterfaces = function() {
    var widget = Wisej.WebDriver.getWidgetByElement(arguments[0]);
    var iFaces = [];
    var clazz = widget.constructor;
    qx.Class.getInterfaces(clazz).forEach(function(item, i) {
        var match = /\[Interface (.*?)\]/.exec(item.toString());
        if (match && match.length > 1) {
            iFaces.push(match[1]);
        }
    });
    return iFaces;
};

Wisej.WebDriver.getItemFromSelectables = function() {
    var widget = Wisej.WebDriver.getWidgetByElement(arguments[0]);
    if (typeof arguments[1] == "number") {
        /* var scrollBar = widget.getChildControl("scrollbar-" + arguments[2]).getContentElement().getDomElement(); */
        var scrollBar = widget.getChildControl("scrollbar-y");
        if (scrollBar !== null) {
            var singleStep = scrollBar.get("singleStep");
            scrollBar["scrollTo"](arguments[1] * singleStep);
        }
    }
    var selectables = widget.getSelectables();
    for (var i = 0; i < selectables.length; i++) {
        if ((typeof arguments[1] == "number" && i === arguments[1]) ||
            (typeof arguments[1] == "string" && selectables[i].getLabel().match(new RegExp(arguments[1])))) {
            var contentElement = selectables[i].getContentElement();
            if (contentElement.nodeType && contentElement.nodeType === 1) {
                return contentElement;
            }
            return contentElement.getDomElement();
        }
    }
    return null;
};

Wisej.WebDriver.getLayoutParent = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    var contentElement = widget.getContentElement();
    if (contentElement.nodeType && contentElement.nodeType === 1) {
        return contentElement;
    }
    return contentElement.getDomElement();
};

Wisej.WebDriver.getObjectHash = function() {
    var widget = Wisej.WebDriver.getWidgetByElement(arguments[0]);
    return widget.toHashCode();
};

Wisej.WebDriver.getPropertyValue = function() {
    var widget = Wisej.WebDriver.getWidgetByElement(arguments[0]);
    var result = widget.get(arguments[1]);
    if (qx.data && qx.data.Array && result instanceof qx.data.Array) {
        result = result.toArray();
    }
    if (result instanceof Array) {
        result = result.map(function(item) {
            return item instanceof qx.core.Object ? item.toString() : item;
        });
    }
    if (result instanceof qx.core.Object) {
        result = result.toString();
    }
    return result;
};

Wisej.WebDriver.getPropertyValueAsJson = function() {
    var json = window.JSON || qx.lang.Json;
    var widget = Wisej.WebDriver.getWidgetByElement(arguments[0]);
    var val = widget.get(arguments[1]);
    return json.stringify(val);
};

Wisej.WebDriver.getRowCount = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    var model = widget.getTableModel();
    return model.getRowCount();
};

Wisej.WebDriver.getScrollMax = function() {
    var widget = Wisej.WebDriver.getWidgetByElement(arguments[0]);
    var methodName = "getScrollMax" + arguments[1].toUpperCase();
    return widget[methodName]();
};

Wisej.WebDriver.getTableRowHeight = function() {
    var scroller = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    return scroller.getTable().getRowHeight();
};

Wisej.WebDriver.getTableScrollerMaximum = function() {
    var scroller = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    var model = scroller.getTable().getTableModel();
    var rowCount = model.getRowCount();
    var rowHeight = scroller.getTable().getRowHeight();
    var scrollSize = rowCount * rowHeight;
    var paneSize = scroller.getPaneClipper().getInnerSize();

    if (paneSize.height < scrollSize) {
        return Math.max(0, scrollSize - paneSize.height);
    } else {
        return 0;
    }
};

Wisej.WebDriver.getTableSelectedRanges = function() {
    var json = window.JSON || qx.lang.Json;
    var table = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    var ranges = table.getSelectionModel().getSelectedRanges();
    return json.stringify(ranges);
};

Wisej.WebDriver.getTreeViewNodes = function() {

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

Wisej.WebDriver.getVisibleTableRowCount = function() {
    var scroller = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    return scroller.getTablePane().getVisibleRowCount();
};

//Wisej.WebDriver.getWidgetByElement = function() {
//    var widget = null;
//    if (!qx.ui) {
//        return widget;
//    }
//    if (qx.ui.core && qx.ui.core.Widget) {
//        widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
//    }

//    if (!widget) {
//        throw new Error("Could not find a widget for this DOM element!");
//    }

//    return widget;
//};

Wisej.WebDriver.getWidgetByElement = function () {
    var widget = null;
    if (!qx.ui) {
        return widget;
    }
    if (typeof qx.ui.core !== 'undefined' && typeof qx.ui.core.Widget !== 'undefined') {
        widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    }
    if (!widget && arguments[0].id && typeof qx.ui.mobile !== 'undefined' && typeof qx.ui.mobile.core !== 'undefined' && typeof qx.ui.mobile.core.Widget !== 'undefined') {
        widget = qx.ui.mobile.core.Widget.getWidgetById(arguments[0].id);
    }

    if (!widget && typeof arguments[0] === "string") {
        var markup = arguments[0];
        var parser = new DOMParser()
        var el = parser.parseFromString(markup, "text/xml");

        widget = Wisej.WebDriver.getWidgetByElement(el);
    }

    if (!widget) {
        //throw new Error("Could not find a widget for this DOM element!");
        widget = null;
    }
    
    return widget;
};

Wisej.WebDriver.hasChildControl = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    return widget.hasChildControl(arguments[1]);
};

Wisej.WebDriver.isApplicationReady = function() {
    return (typeof qx !== "undefined" &&
        typeof qx.core !== "undefined" &&
        typeof qx.core.Init !== "undefined" &&
        !!qx.core.Init.getApplication());
};

Wisej.WebDriver.isDisposed = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    return !widget || widget.isDisposed();
};

Wisej.WebDriver.registerGlobalErrorHandler = function() {
    Wisej.WebDriver.globalErrors = [];
    var errorHandler = function(ex) {
        Wisej.WebDriver.globalErrors.push(ex);
    };
    qx.event.GlobalError.setErrorHandler(errorHandler);
};

Wisej.WebDriver.registerLogAppender = function() {
    Wisej.WebDriver.appender = new qx.log.appender.RingBuffer(500);
    qx.log.Logger.register(Wisej.WebDriver.appender);
};

Wisej.WebDriver.scrollTo = function() {
    var methodName = "scrollTo";
    if (arguments[2]) {
        methodName += arguments[2].toUpperCase();
    }
    var widget = Wisej.WebDriver.getWidgetByElement(arguments[0]);
    console.log(widget);
    widget[methodName](arguments[1]);
};

Wisej.WebDriver.selectTableRow = function() {
    var rowIdx = arguments[1];
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    widget.getSelectionModel().setSelectionInterval(rowIdx, rowIdx);
};

Wisej.WebDriver.setPropertyValue = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    widget.set(arguments[1], arguments[2]);
};

Wisej.WebDriver.setTreeNodeOpened = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    var rowIdx = arguments[1];
    var opened = arguments[2];
    var dm = widget.getDataModel();
    var node = dm.getNodeFromRow(rowIdx);
    widget.nodeSetOpened(node, opened);
};

Wisej.WebDriver.scrollChildNodeToView = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    console.log("Argument 0 =" + arguments[0]);
    console.log(widget);
    TreeView.main.scrollChildIntoView(widget);
    console.log(TreeView.main);
};

Wisej.WebDriver.getName = function(){
    if(!arguments[0].tagName){
        var widget = arguments[0];
        return widget.getName();
    }

    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    return widget.getName();
}

Wisej.WebDriver.setTreeView = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    TreeView.main = widget;
    console.log(TreeView.main);
};

Wisej.WebDriver.scrollSubNodeToView = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    console.log("Argument 0 =" + arguments[0]);
    console.log(widget);
    var parent = widget.getParent();
    parent.scrollChildIntoView(widget);
};


Wisej.WebDriver.isEnabled = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    return widget.isEnabled();
};

Wisej.WebDriver.setEnabled = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    return widget.setEnabled(arguments[1]);
};

Wisej.WebDriver.isReadOnly = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    return widget.isReadOnly();
};

Wisej.WebDriver.setReadOnly = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    return widget.setReadOnly(arguments[1]);
};

Wisej.WebDriver.getComboBoxSelectedText = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    if (widget.classname == "wisej.web.LabelWrapper") widget = widget.getEditor();
    widget.selectAllText();
    return widget.getTextSelection();
};

Wisej.WebDriver.getVisibleColumnCount = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);

    if (widget.classname == "wisej.web.DataGrid") {
        var count = widget.getVisibleColumnCount();
        if (widget.getRowHeadersVisible()) {
            count--;
            return count;
        }
        return count;
    }

    return widget.getVisibleColumnCount();
};

Wisej.WebDriver.getVisibleRowCount = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);

    if (widget.classname == "wisej.web.DataGrid") {
        var count = widget.getVisibleRowCount();

        return count;
    }

    return widget.getVisibleRowCount();
};

Wisej.WebDriver.openComboBox = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    if (widget.classname == "wisej.web.LabelWrapper") widget = widget.getEditor();
    widget.open();
};

Wisej.WebDriver.setComboBoxSelectedIndex = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    if (widget.classname == "wisej.web.LabelWrapper") widget = widget.getEditor();
    widget.setSelectedIndex(arguments[1]);
    widget.fireDataEvent("selectionChanged", arguments[1]);
};

Wisej.WebDriver.getComboBoxSelectedIndex = function () {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    if (widget.classname == "wisej.web.LabelWrapper") widget = widget.getEditor();
    return widget.getSelectedIndex();
};

Wisej.WebDriver.getValue = function () {
    if (!arguments[0].tagName) {
        return arguments[0].getValue();
    }
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    if (widget.classname == "wisej.web.LabelWrapper") widget = widget.getEditor();
    
    return widget.getValue();
};

Wisej.WebDriver.setValue = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    if (widget.classname == "wisej.web.LabelWrapper") widget = widget.getEditor();
    return widget.setValue(arguments[1]);
};

Wisej.WebDriver.scrollDataGirdViewCellToView = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    var rowIdx = arguments[1];
    var colIdx = arguments[2];
    widget.scrollCellIntoView(rowIdx, colIdx);
};

Wisej.WebDriver.fireDataEvent = function(widget, type, data) {
    return widget.fireDataEvent(type, data);
};

Wisej.WebDriver.getListViewSelectedItemText = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    var itemView = widget.itemView;
    var focusedItem = itemView.getFocusedItem();
    var label = lv.itemView.getItemWidgets()[focusedItem].getLabel();

    return label;
};

Wisej.WebDriver.setListBoxSelectedItem = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    var itemView = widget.itemView;

    listView.setFocusedItem(arguments[1]);
};

Wisej.WebDriver.selectAndDoubleClickDataGridViewCell = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    var rowIndex = arguments[1];
    var colIndex = arguments[2];

    widget.setFocusedCell(rowIndex, colIndex);
    widget.fireDataEvent("gridCellDblTap", { row: rowIndex, col: colIndex });
};

Wisej.WebDriver.clickFocusedListBoxItem = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    var itemView = widget.itemView;
    var focusedItem = itemView.getFocusedItem();
    widget.fireDataEvent("itemClick", focusedItem);
};

Wisej.WebDriver.clickAndFocusListBoxItem = function() {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    var itemView = widget.itemView;

    itemView.setFocusedItem(arguments[1]);
    widget.fireDataEvent("itemClick", arguments[1]);
};

console.log("LOADED");