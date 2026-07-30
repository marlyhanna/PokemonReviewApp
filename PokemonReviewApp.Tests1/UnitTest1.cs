using AutoMapper;
using Moq;
using PokemonReviewApp.Interface;
using PokemonReviewApp.Services;
using Xunit;

namespace PokemonReviewApp.Tests
{
    public class UnitTest1
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PokemonService _pokemonService;

        public UnitTest1()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();

            // Instantiating PokemonService with mocked IUnitOfWork and IMapper
            _pokemonService = new PokemonService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void Test1()
        {
            // Arrange

            // Act

            // Assert
            Assert.True(true);
        }
    }
}