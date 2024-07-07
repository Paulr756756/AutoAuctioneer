using Client.Models.Entities;

namespace Client.Store.Garage;

public class DeleteGarageItemAction
{
    public DeleteGarageItemAction(ItemEntity? item)
    {
        Item = item;
    }

    public ItemEntity? Item { get; }
}