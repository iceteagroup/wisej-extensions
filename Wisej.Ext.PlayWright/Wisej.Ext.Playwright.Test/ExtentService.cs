using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using SharpCompress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


	public class ExtentService
	{
		//public static ExtentReports? report;

		//public static ExtentReports GetExtent()
		//{
		//	if(report == null)
		//	{
		//	ExtentReports extent = new ExtentReports();
		//	var currentDirectory = Directory.GetCurrentDirectory();
		//	var rootDirectory = currentDirectory.Split("bin")[0];

		//	var reportDir = Path.Combine(rootDirectory, "reports");
		//	var reportPath = Path.Combine(reportDir, "index.html");

		//	var reporter = new ExtentHtmlReporter(reportPath);

		//	reporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
		//	reporter.Config.DocumentTitle = "Wisej.NET UI Testing Report";
		//	reporter.Config.ReportName = "Wisej.NET UI Testing Automation";

		//	extent.AttachReporter(reporter);

		//	}
		//	return report;
		//}

		private static readonly ExtentReports _lazy = new ExtentReports();

		public static ExtentReports Instance { get { return _lazy; } }

		static ExtentService()
		{

			var currentDirectory = Directory.GetCurrentDirectory();
			var rootDirectory = currentDirectory.Split("bin")[0];

			var reportDir = Path.Combine(rootDirectory, "reports");
			var reportPath = Path.Combine(reportDir, "index.html");
			if (!Directory.Exists(reportDir))
			{
				Directory.CreateDirectory(reportDir);
			}
			var reporter = new ExtentHtmlReporter(reportPath);

			reporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
			reporter.Config.DocumentTitle = "Wisej.NET UI Testing Report";
			reporter.Config.ReportName = "Wisej.NET UI Testing Automation";

			Instance.AttachReporter(reporter);
		}
	}
