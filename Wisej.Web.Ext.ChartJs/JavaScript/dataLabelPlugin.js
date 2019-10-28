// Define a plugin to provide data labels
Chart.plugins.register({
	id: 'dataLabel',

	afterDatasetsDraw: function (chart) {
		var ctx = chart.ctx;

		chart.data.datasets.forEach(function (dataset, i) {

			var meta = chart.getDatasetMeta(i);

			// Skip hidden sets
			if (meta.hidden)
				return;

			if (chart.config.options.dataLabel !== undefined && chart.config.options.dataLabel.display) {
			
				meta.data.forEach(function (element, index) {

					// Skip hidden sets
					if (meta.data[index].hidden)
						return;

					// Draw the text in black, with the specified font
					ctx.fillStyle = 'rgb(0, 0, 0)';

					var fontSize = chart.config.options.dataLabel.fontSize;
					var fontStyle = chart.config.options.dataLabel.fontStyle;
					var fontFamily = chart.config.options.dataLabel.fontFamily;
					
					ctx.font = Chart.helpers.fontString(fontSize, fontStyle, fontFamily);

					// Use the pre-formatted string or simply convert the data
					var dataString = "";
					if (dataset.formatted && dataset.formatted[index] != null)
						dataString = dataset.formatted[index];
					else
						dataString = dataset.data[index].toString();

					// Make sure alignment settings are correct
					ctx.textAlign = 'center';
					ctx.textBaseline = 'middle';

					var padding = 5;
					var position = element.tooltipPosition();
					ctx.fillText(dataString, position.x, position.y - (fontSize / 2) - padding);
				});
			}
		});
	}
});