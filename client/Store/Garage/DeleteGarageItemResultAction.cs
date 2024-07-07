using Client.Models.Entities;

namespace Client.Store.Garage;

public class DeleteGarageItemResultAction
{
    public DeleteGarageItemResultAction(ItemEntity? item, HttpResponseMessage? response)
    {
        Item = item;
        Response = response;
    }

    public ItemEntity? Item { get; }
    public HttpResponseMessage? Response { get; }
}