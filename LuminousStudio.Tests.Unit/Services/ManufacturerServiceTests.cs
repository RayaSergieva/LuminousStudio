namespace LuminousStudio.Tests.Unit.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FluentAssertions;
    using Moq;
    using Xunit;

    using LuminousStudio.Data.Models;
    using LuminousStudio.Data.Repository.Interfaces;
    using LuminousStudio.Services.Core.Services;

    public class ManufacturerServiceTests
    {
        private readonly Mock<IManufacturerRepository> _manufacturerRepositoryMock;
        private readonly ManufacturerService _manufacturerService;

        public ManufacturerServiceTests()
        {
            _manufacturerRepositoryMock = new Mock<IManufacturerRepository>();
            _manufacturerService = new ManufacturerService(
                _manufacturerRepositoryMock.Object);
        }

        [Fact]
        public async Task GetManufacturersAsync_ReturnsAllManufacturers()
        {
            var manufacturers = new List<Manufacturer>
            {
                new Manufacturer { Id = Guid.NewGuid(), ManufacturerName = "Louis Comfort Tiffany" },
                new Manufacturer { Id = Guid.NewGuid(), ManufacturerName = "Clara Driscoll" }
            };

            _manufacturerRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(manufacturers);

            var result = await _manufacturerService.GetManufacturersAsync();

            result.Should().HaveCount(2);
            result.Should().Contain(m => m.ManufacturerName == "Louis Comfort Tiffany");
        }

        [Fact]
        public async Task GetManufacturerByIdAsync_WithValidId_ReturnsManufacturer()
        {
            var manufacturerId = Guid.NewGuid();
            var manufacturer = new Manufacturer
            {
                Id = manufacturerId,
                ManufacturerName = "Louis Comfort Tiffany"
            };

            _manufacturerRepositoryMock
                .Setup(r => r.GetByIdAsync(manufacturerId))
                .ReturnsAsync(manufacturer);

            var result = await _manufacturerService.GetManufacturerByIdAsync(manufacturerId);

            result.Should().NotBeNull();
            result!.ManufacturerName.Should().Be("Louis Comfort Tiffany");
        }

        [Fact]
        public async Task GetManufacturerByIdAsync_WithInvalidId_ReturnsNull()
        {
            var manufacturerId = Guid.NewGuid();

            _manufacturerRepositoryMock
                .Setup(r => r.GetByIdAsync(manufacturerId))
                .ReturnsAsync((Manufacturer?)null);

            var result = await _manufacturerService.GetManufacturerByIdAsync(manufacturerId);

            result.Should().BeNull();
        }
    }
}