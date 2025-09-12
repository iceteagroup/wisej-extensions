using System.Diagnostics;
using AventStack.ExtentReports;
using Microsoft.Playwright;
using Wisej.Ext.PlayWright;
using Wisej.Ext.PlayWright.Controls;

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
			/*ExtentService.Instance.AddSystemInfo("OS", Environment.OSVersion.ToString());
			ExtentService.Instance.AddSystemInfo("Browser Name", Browser.BrowserType.Name);
			ExtentService.Instance.AddSystemInfo("Browser Version", Browser.Version);

			ExtentService.Instance.Flush();*/
		}

		[TearDown]
		public async Task TearDown()
		{
			try
			{

				/*//Paths for screenshots
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
						ExtentTestManager.GetTest().CreateNode(TestContext.CurrentContext.Test.Name);
						ReportLog.Fail($"{TestContext.CurrentContext.Test.Name} Test Failed:", mediaModel);
						ReportLog.Fail(errorMessage);
						ReportLog.Fail(stackTrace);
						break;

					case NUnit.Framework.Interfaces.TestStatus.Passed:
						ExtentTestManager.GetTest().CreateNode(TestContext.CurrentContext.Test.Name);
						ReportLog.Pass($"{TestContext.CurrentContext.Test.Name} Test Passed", mediaModel);
						break;

					case NUnit.Framework.Interfaces.TestStatus.Skipped:
						ExtentTestManager.GetTest().CreateNode(TestContext.CurrentContext.Test.Name);
						ReportLog.Skip($"{TestContext.CurrentContext.Test.Name} Skipped");
						break;
				}

				await context.Tracing.StopAsync(new()
				{
					Path = "./trace.zip"
				});*/

				await context.CloseAsync();
			}
			catch (Exception e)
			{
				throw new Exception("Exception: " + e);
			}
		}

		internal TreeView MasterTreeView;
		internal Dictionary<string, TreeNode> MasterChildNodes;

        [OneTimeSetUp]
		public async Task GlobalSetup() 
		{
			
        }

		[SetUp]
		public async Task Setup()
		{
			playwright = await Microsoft.Playwright.Playwright.CreateAsync();
			
			Browser = await playwright.Firefox.LaunchAsync(new()
			{
				Headless = true,
				SlowMo = 0,
				TracesDir = "./trace",

			});


			
			context = await Browser.NewContextAsync(new()
			{
				RecordVideoDir = $"./videos/{TestContext.CurrentContext.Test.Name}/{TestContext.CurrentContext.Test.Name}.WEMB",
				RecordVideoSize = new RecordVideoSize() { Width = 1920, Height = 1080 },
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
			await Page.GotoAsync("https://wisej-demobrowser.azurewebsites.net", pgto);

			Page.Response += Page_Response;
			Page.Request += Page_Request;
			Driver = new WisejWebDriver(Browser, Page);

			await Driver.InitAsync();
		}

		private void Context_Page(object? sender, IPage e)
		{
			
		}

		private void Page_Request(object? sender, IRequest e)
		{
		}

		private void Page_Response(object? sender, IResponse e)
		{
		}
	}
}

