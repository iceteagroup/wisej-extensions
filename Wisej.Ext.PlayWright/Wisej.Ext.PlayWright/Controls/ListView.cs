using Microsoft.Playwright;

namespace Wisej.Ext.PlayWright.Controls.List;

public class ListView : Widget
{
    #region Constructors

    internal ListView()
    {
    }

    public ListView(IElementHandle element) : base(element)
    {
    }

    public ListView(ILocator locator) : base(locator)
    {
    }

    #endregion

    #region Properties

    

    #endregion

    #region Methods

    public async Task<int[]> GetSelectedIndices()
    {
        await CreateWidgetAsync(this);
        
        var result = await this.EvalAsync<int[]>("getListViewSelectedRanges", await this.ElementAsync);

        return result;
    }

    #endregion
}