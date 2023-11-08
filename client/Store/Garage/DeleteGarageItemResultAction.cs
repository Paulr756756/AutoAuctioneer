using auc_client.Models.Entities;

namespace auc_client.Store.Garage;

public class DeleteGarageItemResultAction {
    public ItemEntity? Item { get; }
    public HttpResponseMessage? Response { get; }

    public DeleteGarageItemResultAction(ItemEntity? item, HttpResponseMessage? response) {
        Item = item;
        Response = response;
    }
}