using System;
using Npgsql;
using System.Data.Common;

namespace shopping_list;

public class ItemService : IItemService
{
    public ItemService()
    {
    }

    public async void AddItem(Item item)
    {
        try {
            await DbDriver.insert("INSERT INTO items (id, name, status) VALUES (@id, @name, @status)", new NpgsqlParameter("@id", Guid.NewGuid()), new NpgsqlParameter("@name", item.Name), new NpgsqlParameter("@status", item.Status));

        } catch (DbException dbe)
        {
            Console.WriteLine(dbe);
        }
    }
}


