/**
 * Initialize...
 */

// read options from server.
// var options = $options;

// create the document container element.
var id = widget.getId() + "_onlyoffice";
widget.container.innerHTML = "<div id=\"" + id + "\" style='width:100%;height:100%'></div>";

var config = {
	type: 'desktop',
	documentType: 'text',
	document: {
		fileType: 'docx',
		title: 'Test',
		url: 'Documents/Features Table.docx',
	},
	editorConfig: {
		callbackUrl: '',
	},
	events: {
		onReady: function () { widget.fireEvent("onEditorReady"); }
	}
};

widget.$editor = new DocsAPI.DocEditor(id, config);

