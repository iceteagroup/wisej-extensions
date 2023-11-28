using System;
using AventStack.ExtentReports;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Wisej.Ext.PlayWright;

namespace Wisej.Ext.Playwright.Test
{
	public class BaseTest
	{
		public IBrowser Browser;
		public IPage Page;

		public IPlaywright playwright;
		public WisejWebDriver Driver;
		public IBrowserContext context;

		[OneTimeSetUp]
		public void GlobalSetUp()
		{
			ExtentTestManager.CreateTest(GetType().Name, TestContext.CurrentContext.Test.FullName);
		}

		[OneTimeTearDown]
		public void GlobalTearDown()
		{
			ExtentService.Instance.AddSystemInfo("OS", Environment.OSVersion.ToString());
			ExtentService.Instance.AddSystemInfo("Browser Name", Browser.BrowserType.Name);
			ExtentService.Instance.AddSystemInfo("Browser Version", Browser.Version);

			ExtentService.Instance.Flush();
		}

		[TearDown]
		public async Task TearDown()
		{
			try
			{

				//Paths for screenshots
				var currentDirectory = Directory.GetCurrentDirectory();
				var rootDirectory = currentDirectory.Split("bin")[0];

				var reportDir = Path.Combine(rootDirectory, "reports");
				var screenshotDir = Path.Combine(reportDir, "Screenshots");

				//
				var status = TestContext.CurrentContext.Result.Outcome.Status;
				var errorMessage = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message) ? "" : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.Message);

				var stackTrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);

				var screen = Page.ScreenshotAsync(new PageScreenshotOptions() { Path = $"{screenshotDir}/{TestContext.CurrentContext.Test.Name}.png", FullPage = true }).Result;

				var mediaModel = MediaEntityBuilder.CreateScreenCaptureFromPath($"{screenshotDir}/{TestContext.CurrentContext.Test.Name}.png").Build();

				switch (status)
				{
					case NUnit.Framework.Interfaces.TestStatus.Failed:
						//ExtentTestManager.GetTest().CreateNode(TestContext.CurrentContext.Test.Name);


						ReportLog.Fail($"{TestContext.CurrentContext.Test.Name} Test Failed:", mediaModel);
						ReportLog.Fail(errorMessage);
						ReportLog.Fail(stackTrace);
						break;

					case NUnit.Framework.Interfaces.TestStatus.Passed:
						//ExtentTestManager.GetTest().CreateNode(TestContext.CurrentContext.Test.Name);
						ReportLog.Pass($"{TestContext.CurrentContext.Test.Name} Test Passed", mediaModel);
						break;

					case NUnit.Framework.Interfaces.TestStatus.Skipped:
						//ExtentTestManager.GetTest().CreateNode(TestContext.CurrentContext.Test.Name);

						ReportLog.Skip($"{TestContext.CurrentContext.Test.Name} Skipped");
						break;
				}

				await context.Tracing.StopAsync(new()
				{
					Path = "./trace.zip"
				});

				await context.CloseAsync();
			}
			catch (Exception e)
			{
				throw new Exception("Exception: " + e);
			}
		}

		[OneTimeSetUp]
		public async Task GlobalSetup() 
		{

		}

		[SetUp]
		public async Task Setup()
		{
			playwright = await Microsoft.Playwright.Playwright.CreateAsync();
			
			Browser = await playwright.Chromium.LaunchAsync(new()
			{
				Headless = false,
				SlowMo = 1200,
				TracesDir = "./trace",
			});


			
			context = await Browser.NewContextAsync(new()
			{
				RecordVideoDir = "./videos",
				RecordVideoSize = new RecordVideoSize() { Width = 1920, Height = 1080 }
			});

			Page = await context.NewPageAsync();
			
			await context.Tracing.StartAsync(new()
			{
				Screenshots = true,
				Snapshots = true,
				Sources = true,
				Name= TestContext.CurrentContext.Test.Name,
			});

			var pgto = new PageGotoOptions();
			pgto.WaitUntil = WaitUntilState.NetworkIdle;
			await Page.GotoAsync("http://localhost:5000", pgto);

			Page.Response += Page_Response;
			Page.Request += Page_Request;
			Driver = new WisejWebDriver(Browser, Page);

			await Driver.Init();
		}

		private void Context_Page(object? sender, IPage e)
		{
			
		}

		private void Page_Request(object? sender, IRequest e)
		{
			Console.WriteLine($"{e.Headers}");
		}

		private void Page_Response(object? sender, IResponse e)
		{
			Console.WriteLine($"{e.Headers}");
		}
	}
}

