using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


	public class ReportLog
	{
		public static ExtentTest Pass(string message)
		{
			return ExtentTestManager.GetTest().Pass(message);
		}

		public static ExtentTest Pass(string message, MediaEntityModelProvider mediaEntityBuilder)
		{
			return ExtentTestManager.GetTest().Pass(message, mediaEntityBuilder);
		}

		public static ExtentTest Fail(string message)
		{
			return ExtentTestManager.GetTest().Fail(message);
		}

		public static ExtentTest Fail(string message,MediaEntityModelProvider mediaEntityBuilder)
		{
			return ExtentTestManager.GetTest().Fail(message, mediaEntityBuilder);
		}

		public static ExtentTest Skip(string message)
		{
			return ExtentTestManager.GetTest().Skip(message);
		}
	}

