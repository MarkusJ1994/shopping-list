using Npgsql;
using System.Data.Common;

namespace shopping_list;

public class ItemService : IItemService
{
    public ItemService()
    {
    }

    public Item ItemMapper(NpgsqlDataReader reader)
    {
        return new Item(reader.GetGuid(0), reader.GetString(1), reader.GetBoolean(2));
    }

    public Task<List<Item>> GetItems()
    {
        return DbDriver.Read<Item>("SELECT * FROM items", ItemMapper);
    }

    public Task AddItem(AddItemDto item)
    {
        try
        {
            return DbDriver.Write("INSERT INTO items (id, name, in_storage) VALUES (@id, @name, @in_storage)", new NpgsqlParameter("@id", Guid.NewGuid()), new NpgsqlParameter("@name", item.Name), new NpgsqlParameter("@in_storage", item.InStorage));
        }
        catch (DbException)
        {
            throw new HttpStatusException(System.Net.HttpStatusCode.InternalServerError, "could not persist item");
        }
    }

    public Task RemoveItem(Guid id)
    {
        try
        {
            return DbDriver.Write("DELETE FROM items WHERE id = @id", new NpgsqlParameter("@id", id));
        }
        catch (DbException)
        {
            throw new HttpStatusException(System.Net.HttpStatusCode.InternalServerError, "could not delete item");
        }
    }

    public Task UpdateItem(Guid id, AddItemDto item)
    {
        try
        {
            return DbDriver.Write("UPDATE items SET name = @name, in_storage = @in_storage WHERE id = @id", new NpgsqlParameter("@name", item.Name), new NpgsqlParameter("@in_storage", item.InStorage), new NpgsqlParameter("@id", id));
        }
        catch (DbException)
        {
            throw new HttpStatusException(System.Net.HttpStatusCode.InternalServerError, "could not update item");
        }
    }

}


