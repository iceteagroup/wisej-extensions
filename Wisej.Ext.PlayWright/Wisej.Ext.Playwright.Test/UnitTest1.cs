using Wisej.Ext.PlayWright;
using Wisej.Ext.PlayWright.Controls;

namespace Wisej.Ext.Playwright.Test
{
	public class Tests : BaseTest
	{

		[Test]
		public async Task AlertBoxTest()
		{
			var element = await Page.QuerySelectorAsync("#Window1_button1");

			Widget button = new Widget(element);

			

			AlertBox alertBox = new AlertBox();

			await button.Element.ClickAsync();
			await button.Element.ClickAsync();

			var messages = await alertBox.Messages;

			//Assert.AreEqual(messages.Length, 3);

			Assert.AreEqual(2, await alertBox.Count);

			//var message = await alertBox.GetMessage(0);

			//Assert.AreEqual(message, "Hello");
		}

		[Test]
		public async Task ButtonTest()
		{
			var element = await Page.QuerySelectorAsync("#Window1_button1");

			Button button = new Button(element);

			Assert.IsFalse(button.IsSelected);
			Assert.IsFalse(button.IsClicked);

			await button.Select();
			Assert.IsTrue(button.IsSelected);

			await button.Click();
			Assert.IsTrue(button.IsClicked);
		}

		[Test]
		public async Task ComboBoxTest()
		{
			var element = await Page.QuerySelectorAsync("#Window1_comboBox1");

			ComboBox comboBox = new ComboBox(element);

			var text1 = await comboBox.Text;

			Assert.AreEqual("Test", text1);

			var selectedIndex1 = await comboBox.SelectedIndex;
			Assert.AreEqual(0, selectedIndex1);

			comboBox.SetSelectedIndex(1);
			var selectedIndex2 = await comboBox.SelectedIndex;
			Assert.AreEqual(1, selectedIndex2);

			var text2 = await comboBox.Text;
			Assert.AreEqual("Test2", text2);
		}
	}
}