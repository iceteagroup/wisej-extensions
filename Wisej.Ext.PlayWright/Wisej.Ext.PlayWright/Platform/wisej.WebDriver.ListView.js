Wisej.WebDriver.ListView = {};

Wisej.WebDriver.ListView.getListViewSelectedItemText = function () {
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    var itemView = widget.itemView;
    var focusedItem = itemView.getFocusedItem();
    var label = lv.itemView.getItemWidgets()[focusedItem].getLabel();

    return label;
};

Wisej.WebDriver.ListView.getListViewSelectedRanges = function(){
    var widget = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
    var itemView = widget.itemView;
    
    var selectedIndices = itemView.getSelectedRanges();
    var arrayTransform = [];
    
    if(selectedIndices.length > 0){
        selectedIndices.forEach(o=>{
            arrayTransform.push(o.minIndex);
            arrayTransform.push(o.maxIndex);
        })
    }
    
    return arrayTransform;
}