using System;
using Microsoft.Data.Sqlite;
using Training.DotNetCore.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

namespace Training.DotNetCore.DA.Tests
{
    public class TrainingRepositoryTests
    {
        [Fact]
        public async Task Update_ShouldReturnNull_WhenEntityDoesNotExist()
        {
            using (var connection = new SqliteConnection("DataSource=:memory:"))
            {
                connection.Open();

                // Setup
                var options = new DbContextOptionsBuilder<DotNetCoreTrainingContext>()
                    .UseSqlite(connection)
                    .Options;
                
                using (var context = new DotNetCoreTrainingContext(options))
                {
                    await context.Database.EnsureCreatedAsync();
                }

                // Act
                Model.Training result;
                using (var context = new DotNetCoreTrainingContext(options))
                {
                    var repo = new TrainingRepository(context);
                    result = await repo.UpdateAsync(3, new Model.Training());
                }

                // Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task Update_ShouldUpdate_And_ReturnUpdatedEntity()
        {
            using (var connection = new SqliteConnection("DataSource=:memory:"))
            {
                connection.Open();

                // Setup
                var options = new DbContextOptionsBuilder<DotNetCoreTrainingContext>()
                    .UseSqlite(connection)
                    .Options;
                
                using (var context = new DotNetCoreTrainingContext(options))
                {
                    await context.Database.EnsureCreatedAsync();
                }
                var model = new Model.Training
                {
                    TrainerId = 1,
                    Name = "Test training 111",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today
                };

                // Act
                Model.Training result;
                using (var context = new DotNetCoreTrainingContext(options))
                {
                    var repo = new TrainingRepository(context);
                    result = await repo.UpdateAsync(2, model);
                }

                // Assert
                Assert.NotNull(result);
                Assert.Equal(model.Name, result.Name);
                Assert.Equal(model.StartDate, result.StartDate);
                Assert.Equal(model.EndDate, result.EndDate);
            }
        }
    }
}
