using Fluxor;

namespace Client.Store.AddItem;

public static class AddItemReducer
{
    [ReducerMethod]
    public static AddItemPageState ReduceDropDownChange(AddItemPageState state, ItemTypeSelectAction action)
    {
        return action.SelectedType switch
        {
            null => state,
            _ => new AddItemPageState(action.SelectedType)
        };
    }
}