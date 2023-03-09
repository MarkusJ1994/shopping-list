using System;
namespace shopping_list
{
	public interface IItemService
	{
        Task<List<Item>> GetItems();

        Task AddItem(AddItemDto item);

        Task RemoveItem(Guid id);

        Task UpdateItem(Guid id, AddItemDto item);
    }
}

