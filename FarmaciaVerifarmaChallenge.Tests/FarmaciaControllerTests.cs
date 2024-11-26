using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using FarmaciaVerifarmaChallenge.Application.Interfaces;
using FarmaciaVerifarmaChallenge.Domain.Entities;
using FarmaciaVerifarmaChallenge.API.Controllers;
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
        var farmacia = new FarmaciaDto { Nombre = "FarmaciaVerifarma", Direccion = "SanMArtin 123" };
        _mockFarmaciaService.Setup(s => s.AddFarmacia(It.IsAny<FarmaciaDto>()));

        // Act
        var result = await _controller.AddFarmacia(farmacia);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<Farmacia>(actionResult.Value);
        Assert.Equal("Farmacia Central", returnValue.Nombre);
    }

    [Fact]
    public async Task AddFarmacia_ShouldReturnBadRequest_WhenFarmaciaIsNull()
    {
        // Arrange
        FarmaciaDto farmacia = null;

        // Act
        var result = await _controller.AddFarmacia(farmacia);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task GetFarmaciaPorCercania_ShouldReturnFarmacias_WhenCalledWithValidCoordinates()
    {
        // Arrange
        var latitud = 34;
        var longitud = -58;
        var farmacias = new List<Farmacia>
        {
            new Farmacia { Id = 1, Nombre = "Farmacia A", Latitud = 65, Longitud = 58 },
            new Farmacia { Id = 2, Nombre = "Farmacia B", Latitud = 34, Longitud = 58 }
        };

        _mockFarmaciaService.Setup(s => s.GetFarmaciaPorCercania(latitud, longitud));

        // Act
        var result = await _controller.GetFarmaciaPorCercania(latitud, longitud);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Farmacia>>(actionResult.Value);
        Assert.Equal(2, returnValue.Count);
    }
}
