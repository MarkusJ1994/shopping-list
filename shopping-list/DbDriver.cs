using System;
using Npgsql;

namespace shopping_list;


public class DbDriver
{

    private static string Host = Environment.GetEnvironmentVariable("DBHOST") != null ? Environment.GetEnvironmentVariable("DBHOST")! : "localhost";
    private static string User = Environment.GetEnvironmentVariable("DBUSER") != null ? Environment.GetEnvironmentVariable("DBUSER")! : "postgres";
    private static string DBname = Environment.GetEnvironmentVariable("DBNAME") != null ? Environment.GetEnvironmentVariable("DBNAME")! : "postgres";
    private static string Password = Environment.GetEnvironmentVariable("DBPASSWORD") != null ? Environment.GetEnvironmentVariable("DBPASSWORD")! : "password";
    private static string Port = Environment.GetEnvironmentVariable("DBPORT") != null ? Environment.GetEnvironmentVariable("DBPORT")! : "5432";

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

    public static async Task<int> Write(string sql, params NpgsqlParameter[] parameters)
    {
        var dataSource = DbDriver.GetDataSource();
        await using (var command = dataSource.CreateCommand(sql))
        {
            command.Parameters.AddRange(parameters);

            return await command.ExecuteNonQueryAsync();
        }
    }

    public delegate T Mapper<T>(NpgsqlDataReader reader);

    public static async Task<List<T>> Read<T>(string sql, Mapper<T> mapper)
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
