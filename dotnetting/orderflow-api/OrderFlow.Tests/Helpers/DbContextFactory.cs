using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Api.Data;

namespace OrderFlow.Tests.Helpers;

public static class DbContextFactory
{
    public static AppDbContext Create()
    {
        var connection = new SqliteConnection("DataSource=:memory:");

        connection.Open();

        var options =
            new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;

        var context = new AppDbContext(options);

        context.Database.EnsureCreated();

        return context;
    }
}