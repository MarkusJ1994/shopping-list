using System;
using Npgsql;
using System.Data.Common;

namespace shopping_list;

public class ItemService : IItemService
{
    public ItemService()
    {
    }

    public Task AddItem(Item item)
    {
        try
        {
            return DbDriver.Insert("INSERT INTO items (id, name, in_storage) VALUES (@id, @name, @in_storage)", new NpgsqlParameter("@id", Guid.NewGuid()), new NpgsqlParameter("@name", item.Name), new NpgsqlParameter("@in_storage", item.InStorage));
        }
        catch (DbException dbe)
        {
            throw new HttpStatusException(System.Net.HttpStatusCode.InternalServerError, "could not persist item");
        }
    }

    public Task<List<Item>> GetItems()
    {       
        return DbDriver.Select<Item>("SELECT * FROM items", ItemMapper);
    }

    public Task RemoveItem(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateItem(Guid id, Item item)
    {
        throw new NotImplementedException();
    }

    public Item ItemMapper(NpgsqlDataReader reader)
    {
        return new Item(reader.GetGuid(0), reader.GetString(1), reader.GetBoolean(2));
    }
}


