using Fluxor;
using System.Reflection.Metadata.Ecma335;

namespace Client.Store.AddItem;

public static class AddItemReducer {
    [ReducerMethod]
    public static AddItemPageState ReduceDropDownChange(AddItemPageState state, ItemTypeSelectAction action) =>
        action.SelectedType switch {
            null => state,
            _ => new AddItemPageState(action.SelectedType),
        };

}