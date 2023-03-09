using System;
namespace shopping_list
{
	public interface IItemService
	{
        Task<List<Item>> GetItems();

        Task AddItem(Item item);

        Task RemoveItem(Guid id);

        Task UpdateItem(Guid id, Item item);
    }
}

