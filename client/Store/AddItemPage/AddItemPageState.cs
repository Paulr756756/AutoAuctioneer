using Fluxor;

namespace Client.Store.AddItem;

[FeatureState]
public class AddItemPageState
{
    //Required by fluxor for creating initial state
    private AddItemPageState()
    {
    }

    public AddItemPageState(Type? selectedType)
    {
        SelectedType = selectedType;
    }

    public Type? SelectedType { get; }
}