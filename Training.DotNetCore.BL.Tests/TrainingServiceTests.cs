using System;
using Xunit;
using Moq;
using AutoMapper;
using Training.DotNetCore.DA.Repositories;
using Training.DotNetCore.BL.Services;

namespace Training.DotNetCore.BL.Tests
{
    public class TrainingServiceTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<ITrainingRepository> _trainingRepositoryMock;

        public TrainingServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _trainingRepositoryMock = new Mock<ITrainingRepository>();
        }

        [Fact]
        public void Add_Null_ShouldThrowArgumentNullException()
        {
            // Setup
            var service = new TrainingService(_mapperMock.Object, _trainingRepositoryMock.Object);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>("training", () => service.AddAsync(null));
        }

        [Fact]
        public void Add_Training_ShouldReturnCreatedTraining()
        {
            // Setup
            var trainingBL = new BL.Model.Training();
            var trainingDA = new DA.Model.Training { Id = 1 };

            _mapperMock.Setup(m => m.Map<DA.Model.Training>(trainingBL))
                .Returns(new DA.Model.Training());
            _mapperMock.Setup(m => m.Map<BL.Model.Training>(trainingDA))
                .Returns(new BL.Model.Training { Id = trainingDA.Id });

            var service = new TrainingService(_mapperMock.Object, _trainingRepositoryMock.Object);

            // Act
            var result = service.AddAsync(trainingBL);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(trainingDA.Id, result.Id);
            _trainingRepositoryMock.Verify(t => t.AddAsync(It.IsAny<DA.Model.Training>()), Times.Once);
        }
    }
}
