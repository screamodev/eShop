using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using Order.Host.Configurations;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Models.Requests;
using Order.Host.Models.Responses;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services;
using Order.Host.Services.Interfaces;
using Xunit;

namespace Order.UnitTests.Services
{
    public class OrderServiceTest
    {
        private readonly IOrderService _orderService;
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IOptions<OrderConfig>> _settings;
        private readonly Mock<HttpClient> _httpClient;
        private readonly Mock<ILogger<OrderService>> _logger;

        public OrderServiceTest()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _mapper = new Mock<IMapper>();
            _settings = new Mock<IOptions<OrderConfig>>();
            _httpClient = new Mock<HttpClient>();
            _logger = new Mock<ILogger<OrderService>>();

            var dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var dbContextTransaction = new Mock<IDbContextTransaction>();
            dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
                .ReturnsAsync(dbContextTransaction.Object);

            _orderService = new OrderService(
                dbContextWrapper.Object,
                _logger.Object,
                _orderRepository.Object,
                _mapper.Object,
                _settings.Object,
                _httpClient.Object
            );
        }

        [Fact]
        public async Task GetAllByUserIdAsync_Success()
        {
            // Arrange
            var userId = "1";
            var orderEntities = new List<OrderEntity>
            {
                new OrderEntity { Id = 1, TotalAmount = 100.0m }
            };

            var orderDtos = new List<OrderDto>
            {
                new OrderDto { Id = 1, TotalAmount = 100.0m }
            };

            _orderRepository.Setup(repo => repo.GetAllByUserIdAsync(It.IsAny<int>())).ReturnsAsync(orderEntities);
            _mapper.Setup(m => m.Map<List<OrderDto>>(orderEntities)).Returns(orderDtos);

            // Act
            var result = await _orderService.GetAllByUserIdAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.First().TotalAmount.Should().Be(100.0m);
        }
    }
}
