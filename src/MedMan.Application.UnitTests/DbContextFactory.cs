using IdentityServer4.EntityFramework.Options;
using MedMan.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace MedMan.Application.UnitTests
{
    public static class DbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var operationalStoreOptions = Options.Create(
                new OperationalStoreOptions
                {
                    DeviceFlowCodes =
                    new TableConfiguration("DeviceCodes"),
                    PersistedGrants =
                    new TableConfiguration("PersistedGrants")
                });

            var context = new ApplicationDbContext(options);//, operationalStoreOptions);

            //ApplicationDbContextSeeder.Seed(context);

            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
