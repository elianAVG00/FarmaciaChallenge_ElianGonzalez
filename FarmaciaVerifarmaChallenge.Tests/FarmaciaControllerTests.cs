using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using FarmaciaVerifarmaChallenge.Application.Interfaces;
using FarmaciaVerifarmaChallenge.Domain.Entities;
using FarmaciaVerifarmaChallenge.Presentation.Controllers;
using Microsoft.Extensions.Logging;
using FarmaciaVerifarmaChallenge.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

public class FarmaciaControllerTests
{
    private readonly Mock<IFarmaciaService> _mockFarmaciaService;
    private readonly Mock<ILogger<FarmaciaController>> _mockLogger;
    private readonly FarmaciaController _controller;

    public FarmaciaControllerTests()
    {
        _mockFarmaciaService = new Mock<IFarmaciaService>();
        _mockLogger = new Mock<ILogger<FarmaciaController>>();
        _controller = new FarmaciaController(_mockFarmaciaService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task AddFarmacia_ShouldReturnOkResult_WhenAddFarmacia()
    {
        // Arrange
        var farmacia = new FarmaciaDto { Nombre = "FarmaciaVerifarma", Direccion = "San Martin 123" };
        _mockFarmaciaService.Setup(s => s.AddFarmacia(farmacia));

        // Act
        var result = await _controller.AddFarmacia(farmacia);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task AddFarmacia_ShouldReturnBadRequest_WhenFarmaciaIsNull()
    {
        // Arrange
        FarmaciaDto farmacia = null;

        // Act
        _mockFarmaciaService.Setup(s => s.AddFarmacia(farmacia)).ThrowsAsync(new ArgumentNullException());
        var result = await _controller.AddFarmacia(farmacia);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task GetFarmaciaPorCercania_ShouldReturnFarmacias_WhenCalledWithValidCoordinates()
    {
        // Arrange
        var latitud = 34;
        var longitud = -58;
        var farmacia = new FarmaciaDto { Nombre = "Farmacia Verifarma", Direccion = "SanMArtin 123", Latitud = 23, Longitud = -40 };

        _mockFarmaciaService.Setup(s => s.GetFarmaciaPorCercania(latitud, longitud)).ReturnsAsync(farmacia);

        // Act
        var actionResult = await _controller.GetFarmaciaPorCercania(latitud, longitud);

        // Assert
        var result = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnValue = Assert.IsType<FarmaciaDto>(result.Value);
        Assert.Equal("Farmacia Verifarma", returnValue.Nombre);
    }
}
