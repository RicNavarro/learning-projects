using Microsoft.EntityFrameworkCore;
using OrderFlow.Api.Data;

namespace OrderFlow.Tests.Helpers;

public static class DbContextFactory
{
    public static AppDbContext Create()
    {
        var options =
            new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        return new AppDbContext(options);
    }
}