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

    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<ITiffanyLampRepository> _tiffanyLampRepositoryMock;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _tiffanyLampRepositoryMock = new Mock<ITiffanyLampRepository>();
            _orderService = new OrderService(
                _orderRepositoryMock.Object,
                _tiffanyLampRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_WithValidData_ReturnsTrue()
        {
            var lampId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var lamp = new TiffanyLamp
            {
                Id = lampId,
                TiffanyLampName = "Test Lamp",
                Quantity = 10,
                Price = 100,
                Discount = 0
            };

            _tiffanyLampRepositoryMock
                .Setup(r => r.GetByIdAsync(lampId))
                .ReturnsAsync(lamp);

            _tiffanyLampRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<TiffanyLamp>()))
                .ReturnsAsync(true);

            _orderRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            var result = await _orderService.CreateAsync(lampId, userId, 3);

            result.Should().BeTrue();
            lamp.Quantity.Should().Be(7);
        }

        [Fact]
        public async Task CreateAsync_WithNullLamp_ReturnsFalse()
        {
            var lampId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _tiffanyLampRepositoryMock
                .Setup(r => r.GetByIdAsync(lampId))
                .ReturnsAsync((TiffanyLamp?)null);

            var result = await _orderService.CreateAsync(lampId, userId, 1);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task CreateAsync_WithInsufficientQuantity_ReturnsFalse()
        {
            var lampId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var lamp = new TiffanyLamp
            {
                Id = lampId,
                Quantity = 2,
                Price = 100,
                Discount = 0
            };

            _tiffanyLampRepositoryMock
                .Setup(r => r.GetByIdAsync(lampId))
                .ReturnsAsync(lamp);

            var result = await _orderService.CreateAsync(lampId, userId, 5);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task CreateAsync_WithEmptyUserId_ReturnsFalse()
        {
            var lampId = Guid.NewGuid();
            var lamp = new TiffanyLamp
            {
                Id = lampId,
                Quantity = 10,
                Price = 100,
                Discount = 0
            };

            _tiffanyLampRepositoryMock
                .Setup(r => r.GetByIdAsync(lampId))
                .ReturnsAsync(lamp);

            var result = await _orderService.CreateAsync(lampId, Guid.Empty, 1);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task CreateAsync_WithZeroQuantity_ReturnsFalse()
        {
            var lampId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var lamp = new TiffanyLamp
            {
                Id = lampId,
                Quantity = 10,
                Price = 100,
                Discount = 0
            };

            _tiffanyLampRepositoryMock
                .Setup(r => r.GetByIdAsync(lampId))
                .ReturnsAsync(lamp);

            var result = await _orderService.CreateAsync(lampId, userId, 0);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task CreateAsync_WithExactQuantity_ReturnsTrue()
        {
            var lampId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var lamp = new TiffanyLamp
            {
                Id = lampId,
                Quantity = 5,
                Price = 100,
                Discount = 0
            };

            _tiffanyLampRepositoryMock
                .Setup(r => r.GetByIdAsync(lampId))
                .ReturnsAsync(lamp);

            _tiffanyLampRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<TiffanyLamp>()))
                .ReturnsAsync(true);

            _orderRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            var result = await _orderService.CreateAsync(lampId, userId, 5);

            result.Should().BeTrue();
            lamp.Quantity.Should().Be(0);
        }

        [Fact]
        public async Task UserHasOrdersAsync_WhenUserHasOrders_ReturnsTrue()
        {
            var userId = Guid.NewGuid();
            var orders = new List<Order>
            {
                new Order { UserId = userId }
            };

            _orderRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(orders.AsAsyncQueryable());

            var result = await _orderService.UserHasOrdersAsync(userId);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task UserHasOrdersAsync_WhenUserHasNoOrders_ReturnsFalse()
        {
            var userId = Guid.NewGuid();

            _orderRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<Order>().AsAsyncQueryable());

            var result = await _orderService.UserHasOrdersAsync(userId);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task RemoveByIdAsync_WithNonExistentOrder_ReturnsFalse()
        {
            var orderId = Guid.NewGuid();

            _orderRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<Order>().AsAsyncQueryable());

            var result = await _orderService.RemoveByIdAsync(orderId);

            result.Should().BeFalse();
        }
    }
}