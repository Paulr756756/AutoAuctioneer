namespace Client.Store.AddItem;

public class ItemTypeSelectAction
{
    public ItemTypeSelectAction(Type? selectedType)
    {
        SelectedType = selectedType;
    }

    public Type? SelectedType { get; }
}