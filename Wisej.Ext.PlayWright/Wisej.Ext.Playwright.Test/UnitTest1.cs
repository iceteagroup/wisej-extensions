using Wisej.Ext.PlayWright;
using Wisej.Ext.PlayWright.Controls;
using Wisej.Ext.PlayWright.Controls.List;

namespace Wisej.Ext.Playwright.Test
{
	public class Tests : BaseTest
	{

		[Test]
		public async Task TestTreeNode()
		{
			var treeView = new TreeView("#MainView_panelComponents_treeViewComponents");

			var nodes = await treeView.GetChildNodesAsync();
			
			Console.WriteLine(nodes.Count);
		}

		[Test]
		public async Task DataGridViewTest()
		{
			var treeView = new TreeView("#MainView_panelComponents_treeViewComponents");
			await treeView.NavigateToNodeAsync("Grids", "DataGridView");

			DataGridView dgv = new DataGridView("#MainView_panelMain_panelDemoInfo_panelDemoHost_panelDemo_Features_dataGridView1");

			(await dgv.ElementAsync).FocusAsync();
			(await dgv.ElementAsync).ClickAsync();

			var Value = await dgv.FocusedCellValueAsync;

			Console.WriteLine($"{await dgv.FocusedColumnAsync}");
			Console.WriteLine($"{await dgv.FocusedRowAsync}");
			Console.WriteLine($"{Value}");
		}

		[Test]
		public async Task CheckedListBoxTest()
		{
			var treeView = new TreeView("#MainView_panelComponents_treeViewComponents");

			await treeView.NavigateToNodeAsync("Editors", "CheckedListBox");

			var checkedListBox = new CheckedListBox("#MainView_panelMain_panelDemoInfo_panelDemoHost_panelDemo_Features_flowLayoutPanel1_checkedListBox2");

			await checkedListBox.ScrollIntoViewAsync();

			await checkedListBox.CheckMultipleItemsAsync("Aberdeen", "Augusta", "Bel Air");

			CheckedListBoxItem item = (CheckedListBoxItem) await checkedListBox.GetListItemByLabelAsync("Aberdeen");

            Assert.IsTrue((bool) await item.ValueAsync);

			CheckedListBoxItem item2 = (CheckedListBoxItem)await checkedListBox.GetListItemByLabelAsync("Bel Air");

			Assert.IsTrue((bool)await item2.ValueAsync);
        }

		[Test]
		public async Task LocatorTest()
		{
			var treeView = new TreeView("#MainView_panelComponents_treeViewComponents");

			await treeView.NavigateToNodeAsync("Editors", "CheckedListBox");

		}

		[Test]
		public async Task AlertBoxTest()
		{
			var treeView = new TreeView("#MainView_panelComponents_treeViewComponents");

			await treeView.NavigateToNodeAsync("Notifications", "AlertBox");

			Button button = new Button("#MainView_panelMain_panelDemoInfo_panelDemoHost_panelDemo_Features_flowLayoutPanel1_panel2_buttonDefault");

			await button.ClickAsync();

			var alertBoxCount = await new AlertBox().GetAlertBoxesAsync();

			Assert.AreEqual(1, alertBoxCount.Length);

			var message = await new AlertBox().GetMessageAsync(0);

			Assert.AreEqual("Hello, World!", message);
		}

		[Test]
		public async Task MessageBoxTest()
		{
			var treeView = new TreeView("#MainView_panelComponents_treeViewComponents");

			await treeView.NavigateToNodeAsync("Notifications", "MessageBox");

			Button button = new Button("#MainView_panelMain_panelDemoInfo_panelDemoHost_panelDemo_Features_flowLayoutPanel1_panel1_buttonDefault");

			await button.ClickAsync();

			var messageBoxes = await MessageBox.GetMessageBoxesAsync();

			Assert.AreEqual(1, messageBoxes.Length);

			var messageBox2 = messageBoxes[0];

			var message2 = await messageBox2.MessageTextAsync;
			var title2 = await messageBox2.TitleTextAsync;

			Assert.AreEqual("Hello, World", message2);
			Assert.AreEqual("MessageBox", title2);

			var buttonOk = await messageBox2.GetButtonAsync("ok");

			await buttonOk.ClickAsync();

			await Task.Delay(5000);

			messageBoxes = await MessageBox.MessageBoxesAsync;
			Assert.AreEqual(0, messageBoxes.Length);
		}
    }
}