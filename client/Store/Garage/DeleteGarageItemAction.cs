using auc_client.Models.Entities;

namespace auc_client.Store.Garage;

public class DeleteGarageItemAction {
    public ItemEntity? Item { get;}
    public DeleteGarageItemAction(ItemEntity? item) {
        Item = item;
    }
}
