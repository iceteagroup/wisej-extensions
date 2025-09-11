///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// Author: Gianluca Pivato
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
 * wisej.web.ext.Chart
 */
qx.Class.define("wisej.web.ext.Chart", {

    extend: wisej.web.Control,

    construct: function () {

        this.base(arguments);

        this.addListenerOnce("appear", this.__createChart);
    },

    properties: {

        /**
         * ChartType property.
         *
         * Determines the type of the chart to display.
         * Supported values are: "Bar", "Doughnut", "Line", "PolarArea", "Radar".
         */
        chartType: { init: null, check: ["Bar", "Doughnut", "Line", "PolarArea", "Radar"], apply: "_applyChartType" },

        /**
         * Data property.
         *
         * Data map to display in the chart.
         */
        data: { init: { labels: [] }, check: "Object", apply: "_applyData" },

        /**
         * Options property.
         *
         * Chart options map.
         */
        options: { init: {responsive: false}, check: "Object", apply: "_applyOptions" },

    },

    members: {

        // overridden
        _createContentElement: function () {
            return new qx.html.Canvas;
        },

        /**
         * _applyData
         *
         * Changes the data of the underlying chart.
         */
        _applyData: function (value, old) {

        },

        /**
         * _applyOptions
         *
         * Changes the options of the underlying chart.
         */
        _applyOptions: function (value, old) {

        },

        /**
         * _applyChartType
         *
         * Applies the ChartType properties.
         * This method creates the chart object, if not already created.
         */
        _applyChartType: function (value, old) {

            if (this.$$chart != null) {
                this.$$chart.destroy();
                delete this.$$chart;

                this.__createChart();
            }
        },

        /**
         * __createChart
         *
         * Creates the chart element.
         */
        __createChart: function () {

            if (this.$$chart != null)
                return this.$$chart;

            var type = this.getChartType();
            if (type == null)
                return;

            var data = this.getData();
            var options = this.getOptions();
            var ctx = this.getContentElement().getContext2d();

            if (this.designMode) {
                options.animation = false;
            }

            var chart = new Chart(ctx)[type](data, options);

            this.$$chart = chart;
            return this.$$chart;
        }

    },

});