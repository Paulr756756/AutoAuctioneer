using Fluxor;

namespace Client.Store.AddItem;

[FeatureState]
public class AddItemPageState {
    public Type? SelectedType { get; }
    //Required by fluxor for creating initial state
    private AddItemPageState() { }
    public AddItemPageState(Type? selectedType) {
        SelectedType = selectedType;
    }
}