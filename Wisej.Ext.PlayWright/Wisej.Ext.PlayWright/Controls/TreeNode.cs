using Microsoft.Playwright;

namespace Wisej.Ext.PlayWright.Controls;

public class TreeNode : Widget
{

	#region Constructors

	public TreeNode()
	{
	}

	public TreeNode(IElementHandle element) : base(element)
	{
	}

	public TreeNode(string elementAsString) : base(elementAsString)
	{
	}

	#endregion

	#region Parameters

	/// <summary>
	/// Returns the parent TreeView parent of the child TreeNode.
	/// </summary>
	public TreeView? Parent { get; set; }

	public AsyncLazy<string> LabelAsync => new(async () => await GetLabelAsync());

	public AsyncLazy<Dictionary<string, TreeNode>> ChildNodesAsync => new(async () => await GetChildNodesAsync());

	private Dictionary<string, TreeNode> _childNodes;

	#endregion

	#region Methods

	public async Task ScrollToViewAsync()
	{
		await CreateWidgetAsync(this);

		await JsExecuter.EvalAsync<object>("TreeNode","scrollSubNodeToView", await this.ElementAsync);
		await this.Element.ClickAsync();
	}

	/// <summary>
	/// Finds a selectable child widget by index and returns it
	/// </summary>
	/// <param name="index">The index of the item.</param>
	/// <returns>The found item.</returns>
	public virtual async Task<Widget> GetSelectableItemAsync(int index)
	{
		await CreateWidgetAsync(this);

		var contentElement = await this.ContentElementAsync;

		var element = await this.EvalAsync<IElementHandle>("getItemFromSelectables", this.ContentElementAsync, index);

		var widget = new Widget(element);

		return widget;
	}

	/// <summary>
	/// Finds a selectable child widget by index and selects it
	/// </summary>
	/// <param name="index">The index of the item.</param>
	public virtual async Task SelectItemAsync(int index)
	{
		await CreateWidgetAsync(this);

		var element = await GetSelectableItemAsync(index);
		await element.Element.ClickAsync();
	}

	public async Task<Dictionary<string, TreeNode>> GetChildNodesAsync()
	{
		try
		{
			if (this._childNodes is null)
			{
				await CreateWidgetAsync(this);

				var nodeDomElements =
					await this.EvalUnrestrictedAsync<IJSHandle>("getTreeNodeChildren", await this.ElementAsync);

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

	private async Task<string> GetLabelAsync()
	{
		await CreateWidgetAsync(this);

		return await this.EvalAsync<string>("getTreeNodeLabel", await this.ElementAsync);
	}

	private async Task OpenNodeAsync(int rowIndex)
	{
		await CreateWidgetAsync(this);

		await this.CallAsync("setTreeNodeOpened", await this.ElementAsync, rowIndex, true);
	}

	private async Task CloseNodeAsync(int rowIndex)
	{
		await CreateWidgetAsync(this);

		await this.CallAsync("setTreeNodeOpened", await this.ElementAsync, rowIndex, false);
	}

	public async Task OpenNodeAsync()
	{
		await CreateWidgetAsync(this);

		await this.CallAsync("setTreeNodeOpened", await this.ElementAsync, true);
	}

	public async Task CloseNodeAsync()
	{
		await CreateWidgetAsync(this);

		await this.CallAsync("setTreeNodeOpened", await this.ElementAsync, false);
	}

	#endregion

}