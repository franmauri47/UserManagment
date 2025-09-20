using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.ContextExtensions;

internal static class InitialSeed
{
    internal static ModelBuilder AddInitialSeed(this ModelBuilder modelBuilder)
    {
        var seedCreationDate = new DateTime(2025, 9, 20);

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Name = "John Doe",
                Email = "johnDoe@gmail.com",
                CreationDate = seedCreationDate,
                ModifiedDate = null
            },
            new User
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "jSmith@gmail.com",
                CreationDate = seedCreationDate,
                ModifiedDate = null
            },
            new User
            {
                Id = 3,
                Name = "Alice Johnson",
                Email = "aliceJ@gmail.com",
                CreationDate = seedCreationDate,
                ModifiedDate = null
            });

        modelBuilder.Entity<Domicile>().HasData(
            new Domicile
            {
                Id = 1,
                UserId = 1,
                Street = "Main St",
                DirectionNumber = "123",
                Province = "Province A",
                City = "City X",
                CreationDate = seedCreationDate,
                ModifiedDate = null
            },
            new Domicile
            {
                Id = 2,
                UserId = 2,
                Street = "Second St",
                DirectionNumber = "456",
                Province = "Province B",
                City = "City Y",
                CreationDate = seedCreationDate,
                ModifiedDate = null
            },
            new Domicile
            {
                Id = 3,
                UserId = 3,
                Street = "Third St",
                DirectionNumber = "789",
                Province = "Province C",
                City = "City Z",
                CreationDate = seedCreationDate,
                ModifiedDate = null
            });

        return modelBuilder;
    }
}
