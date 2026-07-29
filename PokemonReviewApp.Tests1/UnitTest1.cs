using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using Moq;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Exceptions;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Services;
using Xunit;

namespace PokemonReviewApp.Tests1
{
    public class PokemonServiceTests
    {
        private readonly Mock<IPokemonRepository> _pokemonRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PokemonService _pokemonService;

        public PokemonServiceTests()
        {
            _pokemonRepositoryMock = new Mock<IPokemonRepository>();
            _mapperMock = new Mock<IMapper>();
            _pokemonService = new PokemonService(_pokemonRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetPokemon_ShouldReturnPokemonDto_WhenPokemonExists()
        {
            // Arrange
            int pokeId = 1;
            var pokemon = new Pokemon { Id = 1, Name = "Pikachu" };
            var pokemonDto = new PokemonDto { Id = 1, Name = "Pikachu" };

            _pokemonRepositoryMock.Setup(repo => repo.PokemonExists(pokeId)).Returns(true);
            _pokemonRepositoryMock.Setup(repo => repo.GetPokemon(pokeId)).Returns(pokemon);
            _mapperMock.Setup(m => m.Map<PokemonDto>(pokemon)).Returns(pokemonDto);

            // Act
            var result = _pokemonService.GetPokemon(pokeId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(pokeId);
            result.Name.Should().Be("Pikachu");
        }
    }
}