using System;
using Npgsql;

namespace shopping_list;


public class DbDriver
{

    private static string Host = "localhost";
    private static string User = "postgres";
    private static string DBname = "postgres";
    private static string Password = "password";
    private static string Port = "5432";

    private static string connectionString = $"Host={Host};Username={User};Password={Password};Database={DBname};Port={Port};";

    private static NpgsqlDataSource? dataSource;


    public DbDriver()
    {
    }

    public static NpgsqlDataSource GetDataSource()
    {
        if (dataSource == null)
        {
            dataSource = NpgsqlDataSource.Create(connectionString);
        }
        return dataSource;
    }

    public static async Task<int> Insert(string sql, params NpgsqlParameter[] parameters)
    {
        var dataSource = DbDriver.GetDataSource();
        await using (var command = dataSource.CreateCommand(sql))
        {
            command.Parameters.AddRange(parameters);

            return await command.ExecuteNonQueryAsync();
        }
    }

    public delegate T Mapper<T>(NpgsqlDataReader reader);

    public static async Task<List<T>> Select<T>(string sql, Mapper<T> mapper)
    {
        var resultSet = new List<T>();

        var dataSource = DbDriver.GetDataSource();
        await using (var command = dataSource.CreateCommand(sql))
        {
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Console.WriteLine(reader.GetGuid(0));
                Console.WriteLine(reader.GetString(1));
                Console.WriteLine(reader.GetBoolean(2));

                resultSet.Add(mapper(reader));                
            }
        }
        return resultSet;
    }



}
