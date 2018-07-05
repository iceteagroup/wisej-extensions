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

using System;
using System.ComponentModel;
using Wisej.Web;

namespace Wisej.Web.Ext.Charts
{
    /// <summary>
    /// Chart component.
    /// 
    /// Renders charts from the ChartJS library (http://www.chartjs.org/).
    /// </summary>
    public class Chart : Control
    {
        private int random()
        {
            return new Random().Next(0, 100);
        }

        protected override void OnWebRender(dynamic config)
        {
            base.OnWebRender((object)config);

            config.className = "wisej.web.ext.Chart";

            config.chartType = "Bar";
            config.appearance = "chart";

            config.data = new
            {
                labels = new[] { "January", "February", "March", "April", "May", "June", "July" },
                datasets = new[] {
			        new {
				        fillColor ="rgba(220,220,220,0.5)",
				        strokeColor = "rgba(220,220,220,0.8)",
				        highlightFill ="rgba(220,220,220,0.75)",
				        highlightStroke = "rgba(220,220,220,1)",
				        data = new[] {75,50,60,100,200,40,30,40,20,44,77,98}
			        },
			        new {
				        fillColor= "rgba(151,187,205,0.5)",
				        strokeColor = "rgba(151,187,205,0.8)",
				        highlightFill = "rgba(151,187,205,0.75)",
				        highlightStroke = "rgba(151,187,205,1)",
				        data = new[] {175,150,160,10,20,110,30,140,20,144,177,98}
                    }
                }
            };

        }
    }
}
