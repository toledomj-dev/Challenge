using Challenge.WebApi.Data;
using Challenge.WebApi.Models;
using Challenge.WebApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Test
{
    public class ChallengeTest
    {
        private DateOnly FechaActual = DateOnly.FromDateTime(DateTime.Now);

        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddUser()
        {
            // Arrange
            var context = GetDbContext();
            var repo = new UserRepository(context);
            var user = new User { Nombre = "Juan", Apellido = "Perez", DNI = "123456" };

            // Act
            var result = await repo.AddAsync(user);

            // Assert
            Assert.Equal(1, await context.Users.CountAsync());
            Assert.Equal("Juan", result.Nombre);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var context = GetDbContext();
            context.Users.AddRange(
                new User { Nombre = "Usuario 1", Apellido = "Test", DNI = "111", FechaDeNacimiento = FechaActual },
                new User { Nombre = "Usuario 2", Apellido = "Test", DNI = "222", FechaDeNacimiento = FechaActual }
            );
            await context.SaveChangesAsync();
            var repo = new UserRepository(context);

            // Act
            var result = await repo.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var context = GetDbContext();
            var user = new User { Nombre = "Maria", Apellido = "Gomez", DNI = "456", FechaDeNacimiento = FechaActual };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            var repo = new UserRepository(context);

            // Act
            var result = await repo.GetByIdAsync(user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Maria", result?.Nombre);
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyExistingUser()
        {
            // Arrange
            var context = GetDbContext();
            var user = new User { Nombre = "Original", Apellido = "User", DNI = "000", FechaDeNacimiento = FechaActual };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            user.Nombre = "Modificado";
            var repo = new UserRepository(context);

            // Act
            await repo.UpdateAsync(user);
            var updated = await context.Users.FindAsync(user.Id);

            // Assert
            Assert.Equal("Modificado", updated?.Nombre);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveUser()
        {
            // Arrange
            var context = GetDbContext();
            var user = new User { Nombre = "Eliminar", Apellido = "User", DNI = "999", FechaDeNacimiento = FechaActual };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            var repo = new UserRepository(context);

            // Act
            await repo.DeleteAsync(user.Id);

            // Assert
            Assert.Equal(0, await context.Users.CountAsync());
        }
    }
}
