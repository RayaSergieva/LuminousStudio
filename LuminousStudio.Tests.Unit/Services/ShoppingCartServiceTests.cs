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

    public class ShoppingCartServiceTests
    {
        private readonly Mock<IShoppingCartRepository> _cartRepositoryMock;
        private readonly Mock<ITiffanyLampRepository> _lampRepositoryMock;
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartServiceTests()
        {
            _cartRepositoryMock = new Mock<IShoppingCartRepository>();
            _lampRepositoryMock = new Mock<ITiffanyLampRepository>();
            _shoppingCartService = new ShoppingCartService(
                _cartRepositoryMock.Object,
                _lampRepositoryMock.Object);
        }

        [Fact]
        public async Task AddOrUpdateItemAsync_WithNullLamp_DoesNotAddToCart()
        {
            var lampId = Guid.NewGuid();

            _lampRepositoryMock
                .Setup(r => r.GetByIdAsync(lampId))
                .ReturnsAsync((TiffanyLamp?)null);

            await _shoppingCartService.AddOrUpdateItemAsync(lampId, Guid.NewGuid());

            _cartRepositoryMock.Verify(
                r => r.AddAsync(It.IsAny<ShoppingCart>()), Times.Never);
        }



        [Fact]
        public async Task AddOrUpdateItemAsync_WithNewItem_AddsToCart()
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

            _lampRepositoryMock
                .Setup(r => r.GetByIdAsync(lampId))
                .ReturnsAsync(lamp);

            _cartRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<ShoppingCart>().AsAsyncQueryable());

            _cartRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<ShoppingCart>()))
                .Returns(Task.CompletedTask);

            _lampRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<TiffanyLamp>()))
                .ReturnsAsync(true);

            await _shoppingCartService.AddOrUpdateItemAsync(lampId, userId);

            _cartRepositoryMock.Verify(
                r => r.AddAsync(It.IsAny<ShoppingCart>()), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_WithNonExistentItem_DoesNotCallDelete()
        {
            var cartId = Guid.NewGuid();

            _cartRepositoryMock
                .Setup(r => r.GetByIdAsync(cartId))
                .ReturnsAsync((ShoppingCart?)null);

            await _shoppingCartService.RemoveAsync(cartId);

            _cartRepositoryMock.Verify(
                r => r.DeleteAsync(It.IsAny<ShoppingCart>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryUpdate()
        {
            var cart = new ShoppingCart { Id = Guid.NewGuid(), Count = 2 };

            _cartRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<ShoppingCart>()))
                .ReturnsAsync(true);

            await _shoppingCartService.UpdateAsync(cart);

            _cartRepositoryMock.Verify(r => r.UpdateAsync(cart), Times.Once);
        }
    }
}