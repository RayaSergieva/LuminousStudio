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

    public class LampStyleServiceTests
    {
        private readonly Mock<ILampStyleRepository> _lampStyleRepositoryMock;
        private readonly LampStyleService _lampStyleService;

        public LampStyleServiceTests()
        {
            _lampStyleRepositoryMock = new Mock<ILampStyleRepository>();
            _lampStyleService = new LampStyleService(
                _lampStyleRepositoryMock.Object);
        }

        [Fact]
        public async Task GetLampStylesAsync_ReturnsAllStyles()
        {
            var styles = new List<LampStyle>
            {
                new LampStyle { Id = Guid.NewGuid(), LampStyleName = "Table Lamp" },
                new LampStyle { Id = Guid.NewGuid(), LampStyleName = "Floor Lamp" },
                new LampStyle { Id = Guid.NewGuid(), LampStyleName = "Chandelier" }
            };

            _lampStyleRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(styles);

            var result = await _lampStyleService.GetLampStylesAsync();

            result.Should().HaveCount(3);
            result.Should().Contain(s => s.LampStyleName == "Table Lamp");
        }

        [Fact]
        public async Task GetLampStyleByIdAsync_WithValidId_ReturnsStyle()
        {
            var styleId = Guid.NewGuid();
            var style = new LampStyle { Id = styleId, LampStyleName = "Table Lamp" };

            _lampStyleRepositoryMock
                .Setup(r => r.GetByIdAsync(styleId))
                .ReturnsAsync(style);

            var result = await _lampStyleService.GetLampStyleByIdAsync(styleId);

            result.Should().NotBeNull();
            result!.LampStyleName.Should().Be("Table Lamp");
        }

        [Fact]
        public async Task GetLampStyleByIdAsync_WithInvalidId_ReturnsNull()
        {
            var styleId = Guid.NewGuid();

            _lampStyleRepositoryMock
                .Setup(r => r.GetByIdAsync(styleId))
                .ReturnsAsync((LampStyle?)null);

            var result = await _lampStyleService.GetLampStyleByIdAsync(styleId);

            result.Should().BeNull();
        }
    }
}