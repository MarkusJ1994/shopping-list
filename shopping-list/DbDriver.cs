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

    public static NpgsqlDataSource getDataSource()
    {
        if (dataSource == null)
        {
            dataSource = NpgsqlDataSource.Create(connectionString);
        }
        return dataSource;
    }

    public static async Task<int> insert(string sql, params NpgsqlParameter[] parameters)
    {
        var dataSource = DbDriver.getDataSource();
        await using (var command = dataSource.CreateCommand(sql))
        {
            command.Parameters.AddRange(parameters);

            return await command.ExecuteNonQueryAsync();
        }
    }

}
