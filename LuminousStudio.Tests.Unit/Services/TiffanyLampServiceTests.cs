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
    using LuminousStudio.Tests.Unit.Helpers;

    public class TiffanyLampServiceTests
    {
        private readonly Mock<ITiffanyLampRepository> _tiffanyLampRepositoryMock;
        private readonly TiffanyLampService _tiffanyLampService;

        public TiffanyLampServiceTests()
        {
            _tiffanyLampRepositoryMock = new Mock<ITiffanyLampRepository>();
            _tiffanyLampService = new TiffanyLampService(
                _tiffanyLampRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_WithValidData_ReturnsTrue()
        {
            _tiffanyLampRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<TiffanyLamp>()))
                .Returns(Task.CompletedTask);

            var result = await _tiffanyLampService.CreateAsync(
                "Test Lamp", Guid.NewGuid(), Guid.NewGuid(),
                "http://test.com/image.jpg", 10, 100, 0);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task RemoveByIdAsync_WithNonExistentLamp_ReturnsFalse()
        {
            _tiffanyLampRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<TiffanyLamp>().AsAsyncQueryable());

            var result = await _tiffanyLampService.RemoveByIdAsync(Guid.NewGuid());

            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateAsync_WithNonExistentLamp_ReturnsFalse()
        {
            _tiffanyLampRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<TiffanyLamp>().AsAsyncQueryable());

            var result = await _tiffanyLampService.UpdateAsync(
                Guid.NewGuid(), "Updated", Guid.NewGuid(), Guid.NewGuid(),
                "http://test.com/image.jpg", 10, 100, 0);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetTiffanyLampsAsync_ReturnsAllLamps()
        {
            var lamps = new List<TiffanyLamp>
            {
                new TiffanyLamp
                {
                    Id = Guid.NewGuid(),
                    TiffanyLampName = "Lamp 1",
                    Manufacturer = new Manufacturer { ManufacturerName = "Test" },
                    LampStyle = new LampStyle { LampStyleName = "Table" }
                },
                new TiffanyLamp
                {
                    Id = Guid.NewGuid(),
                    TiffanyLampName = "Lamp 2",
                    Manufacturer = new Manufacturer { ManufacturerName = "Test" },
                    LampStyle = new LampStyle { LampStyleName = "Floor" }
                }
            };

            _tiffanyLampRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(lamps.AsAsyncQueryable());

            var result = await _tiffanyLampService.GetTiffanyLampsAsync();

            result.Should().HaveCount(2);
        }
    }
}