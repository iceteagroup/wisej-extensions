using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Wisej.Ext.PlayWright.Controls
{
    public class MessageBox : Widget
    {

        #region Constructors
        public MessageBox(string elementAsString) : base(elementAsString)
        {
        }

        public MessageBox(IElementHandle element) : base(element)
        {
        }

        public MessageBox(ILocator locator) : base(locator)
        {
        }

        #endregion

        #region Properties

        public static AsyncLazy<MessageBox[]> MessageBoxesAsync => new AsyncLazy<MessageBox[]>(async () => await GetMessageBoxesAsync());

        public AsyncLazy<string> MessageTextAsync => new AsyncLazy<string>(async () => await GetMessageTextAsync());

        public AsyncLazy<string> TitleTextAsync => new AsyncLazy<string>(async () => await GetTitleTextAsync());

        #endregion

        #region Methods

        public static async Task<MessageBox[]> GetMessageBoxesAsync()
        {
            var driverInstance = WisejWebDriver.Instance;
            var messageBoxes = await driverInstance.Page.Locator("div[name=MessageBox]").AllAsync();
            var messageBoxList = new List<MessageBox>();

            foreach (var messageBox in messageBoxes)
            {
                //var isVisible = (await messageBox.IsVisibleAsync());
                
                //if(isVisible)
                messageBoxList.Add(new MessageBox(messageBox));
            }

            return messageBoxList.ToArray();
        }

        public async Task<string> GetMessageTextAsync()
        {
            //var pane = await this.Locator.GetDomElementByNameAsync("pane");
			var messageContent = await Locator.GetDomElementByNameAsync("message");
            var x = await messageContent.ElementHandleAsync(new()
            {
                Timeout = 500
            });

            return await x.InnerTextAsync();
        }

        public async Task<string> GetTitleTextAsync()
        {
            //var pane = await this.Locator.GetDomElementByNameAsync("captionBar");
			var messageContent = await Locator.GetDomElementByNameAsync("title");
            return await messageContent.InnerTextAsync();
        }


        public async Task<Button> GetButtonAsync(string buttonName)
        {
            //Get button pane DOM element
            var pane = await this.Locator.GetDomElementByNameAsync("pane");
            var buttonPane = await pane.GetDomElementByNameAsync("buttonsPane");

            var button = await buttonPane.GetDomElementByNameAsync(buttonName);

            return new Button(button);
        }
        #endregion
    }
}
