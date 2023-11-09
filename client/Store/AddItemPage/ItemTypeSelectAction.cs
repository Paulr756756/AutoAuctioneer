namespace Client.Store.AddItem;

public class ItemTypeSelectAction {
    public Type? SelectedType { get; }

    public ItemTypeSelectAction(Type? selectedType) {
        SelectedType = selectedType;
    }
}

