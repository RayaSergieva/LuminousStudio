namespace LuminousStudio.Tests.Unit.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FluentAssertions;
    using Moq;
    using Xunit;

    using Microsoft.AspNetCore.Identity;

    using LuminousStudio.Data.Models;
    using LuminousStudio.Data.Repository.Interfaces;
    using LuminousStudio.Services.Core.Services;
    using LuminousStudio.Services.Common;
    using LuminousStudio.Tests.Unit.Helpers;

    public class StatisticServiceTests
    {
        private readonly Mock<ITiffanyLampRepository> _tiffanyLampRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly StatisticService _statisticService;

        public StatisticServiceTests()
        {
            _tiffanyLampRepositoryMock = new Mock<ITiffanyLampRepository>();
            _orderRepositoryMock = new Mock<IOrderRepository>();

            var store = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);

            _statisticService = new StatisticService(
                _tiffanyLampRepositoryMock.Object,
                _orderRepositoryMock.Object,
                _userManagerMock.Object);
        }

        [Fact]
        public async Task CountTiffanyLampsAsync_ReturnsCorrectCount()
        {
            _tiffanyLampRepositoryMock
                .Setup(r => r.CountAsync())
                .ReturnsAsync(5);

            var result = await _statisticService.CountTiffanyLampsAsync();

            result.Should().Be(5);
        }

        [Fact]
        public async Task CountTiffanyLampsAsync_WhenNoLamps_ReturnsZero()
        {
            _tiffanyLampRepositoryMock
                .Setup(r => r.CountAsync())
                .ReturnsAsync(0);

            var result = await _statisticService.CountTiffanyLampsAsync();

            result.Should().Be(0);
        }

        [Fact]
        public async Task CountOrdersAsync_ReturnsCorrectCount()
        {
            _orderRepositoryMock
                .Setup(r => r.CountAsync())
                .ReturnsAsync(10);

            var result = await _statisticService.CountOrdersAsync();

            result.Should().Be(10);
        }

        [Fact]
        public async Task CountOrdersAsync_WhenNoOrders_ReturnsZero()
        {
            _orderRepositoryMock
                .Setup(r => r.CountAsync())
                .ReturnsAsync(0);

            var result = await _statisticService.CountOrdersAsync();

            result.Should().Be(0);
        }

        [Fact]
        public async Task SumOrdersAsync_ReturnsCorrectSum()
        {
            var orders = new List<Order>
            {
                new Order { Quantity = 2, Price = 100, Discount = 0 },
                new Order { Quantity = 1, Price = 200, Discount = 10 }
            };

            _orderRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(orders.AsAsyncQueryable());

            var result = await _statisticService.SumOrdersAsync();

            result.Should().Be(380);
        }

        [Fact]
        public async Task SumOrdersAsync_WhenNoOrders_ReturnsZero()
        {
            _orderRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<Order>().AsAsyncQueryable());

            var result = await _statisticService.SumOrdersAsync();

            result.Should().Be(0);
        }

        [Fact]
        public async Task CountClientsAsync_ReturnsOnlyNonAdminUsers()
        {
            var adminUser = new ApplicationUser { Id = Guid.NewGuid(), UserName = "admin" };
            var clientUser1 = new ApplicationUser { Id = Guid.NewGuid(), UserName = "client1" };
            var clientUser2 = new ApplicationUser { Id = Guid.NewGuid(), UserName = "client2" };

            var allUsers = new List<ApplicationUser>
            {
                adminUser, clientUser1, clientUser2
            }.AsAsyncQueryable();

            _userManagerMock
                .Setup(u => u.GetUsersInRoleAsync(ApplicationRoles.Administrator))
                .ReturnsAsync(new List<ApplicationUser> { adminUser });

            _userManagerMock
                .Setup(u => u.Users)
                .Returns(allUsers);

            var result = await _statisticService.CountClientsAsync();

            result.Should().Be(2);
        }

        [Fact]
        public async Task CountClientsAsync_WhenNoClients_ReturnsZero()
        {
            var adminUser = new ApplicationUser { Id = Guid.NewGuid(), UserName = "admin" };

            var allUsers = new List<ApplicationUser>
            {
                adminUser
            }.AsAsyncQueryable();

            _userManagerMock
                .Setup(u => u.GetUsersInRoleAsync(ApplicationRoles.Administrator))
                .ReturnsAsync(new List<ApplicationUser> { adminUser });

            _userManagerMock
                .Setup(u => u.Users)
                .Returns(allUsers);

            var result = await _statisticService.CountClientsAsync();

            result.Should().Be(0);
        }
    }
}