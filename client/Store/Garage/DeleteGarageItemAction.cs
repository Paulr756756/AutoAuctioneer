using Client.Models.Entities;

namespace Client.Store.Garage;

public class DeleteGarageItemAction {
    public ItemEntity? Item { get;}
    public DeleteGarageItemAction(ItemEntity? item) {
        Item = item;
    }
}
