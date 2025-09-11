using Microsoft.Playwright;

namespace Wisej.Ext.PlayWright.Controls;

public class TreeView : Widget
{
	#region Constructor

	public TreeView(IElementHandle element) : base(element)
	{
	}

	public TreeView(string elementAsString) : base(elementAsString)
	{
	}

	#endregion

	#region Methods

	public async Task<Dictionary<string, TreeNode>> GetChildNodesAsync()
	{
		try
		{
			if (this._childNodes is null)
			{
				await CreateWidgetAsync(this);

				var nodeDomElements =
					await this.EvalUnrestrictedAsync<IJSHandle>("getTreeViewNodes", await this.ElementAsync);

				var props = await nodeDomElements.GetPropertiesAsync();
				this._childNodes = new Dictionary<string, TreeNode>();

				for (var i = 0; i < props.Count; i++)
				{
					var elementHandle = props.ElementAt(i).Value.AsElement();
					var node = new TreeNode(elementHandle);
					var label = await node.Element.InnerTextAsync();

					this._childNodes.Add(label, node);

				}
			}

			return this._childNodes;
		}
		catch (Exception e)
		{
			throw new Exception(e.Message);
		}
	}

	#endregion

	#region Properties

	public AsyncLazy<Dictionary<string, TreeNode>> ChildNodes => new(async () => await GetChildNodesAsync());

	private Dictionary<string, TreeNode> _childNodes;

	#endregion

	#region Methods

	public async Task NavigateToNodeAsync(params string[] args)
	{
		await CreateWidgetAsync(this);

		for (var i = 0; i < args.Length; i++)
		{

			var nodeLocator = this.Locator.Locator("div[name=label]").GetByText(args[i]).Locator("..");
			var elem = await nodeLocator.ElementHandleAsync();

			var node = new TreeNode(elem);

			await node.ScrollToViewAsync();
		}

	}

	public async Task<TreeNode> GetNodeByLabelAsync(string name)
	{
		await CreateWidgetAsync(this);

		var nodeLocator = this.Locator.Locator("div[name=label]").GetByText(name).Locator("..");
		var elem = await nodeLocator.ElementHandleAsync();

		var node = new TreeNode(elem);

		await node.ScrollToViewAsync();

		return node;
	}

	#endregion

}