Wisej.WebDriver.DataGridView = {};

Wisej.WebDriver.DataGridView.selectAndDoubleClickDataGridViewCell = function () {
	var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	var rowIndex = arguments[1];
	var colIndex = arguments[2];

	widget.setFocusedCell(rowIndex, colIndex);
	widget.fireDataEvent("gridCellDblTap", {
		row: rowIndex,
		col: colIndex
	});
};

Wisej.WebDriver.DataGridView.dataGridViewSetCellValue = function (text, colId, rowId, error, tooltip) {
	var dgv = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	dgv.setCellValue(colId, rowId, text, error, tooltip);
};

Wisej.WebDriver.DataGridView.dataGridViewgetCellValue = function (colId, rowId) {
	var dgv = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	dgv.setCellValue(colId, rowId);
};

Wisej.WebDriver.DataGridView.dataGridViewgetFocusedColumn = function () {
	var dgv = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	return dgv.getFocusedColumn();
};

Wisej.WebDriver.DataGridView.dataGridViewgetFocusedRow = function () {
	var dgv = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	return dgv.getFocusedRow();
};

Wisej.WebDriver.DataGridView.scrollDataGirdViewCellToView = function () {
	var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
	var rowIdx = arguments[1];
	var colIdx = arguments[2];
	widget.scrollCellIntoView(rowIdx, colIdx);
};