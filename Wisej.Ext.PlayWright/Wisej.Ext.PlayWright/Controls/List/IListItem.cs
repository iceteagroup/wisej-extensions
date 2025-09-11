namespace Wisej.Ext.PlayWright.Controls.List;

public interface IListItem
{
        AsyncLazy<Label> LabelAsync { get; }
        AsyncLazy<string> TextAsync { get; }
        Widget Parent { get; set; }
        Task Select();
}