using EduLink.Application.Academies.Dtos;
using EduLink.Domain.Entities;
using EduLink.Domain.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace EduLink.Api.Tests.Controllers;

public class AcademisControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IAcademiesRepository> _academiesRepositoryMock = new();
    public AcademisControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(Service =>
            {
                Service.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                Service.Replace(ServiceDescriptor.Scoped(typeof(IAcademiesRepository),
                                                 _ => _academiesRepositoryMock.Object));
            });
        });
    }



    [Fact]
    public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
    {
        //arrange
        var id = 12345;

        _academiesRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync((Academy?)null);


        var client = _factory.CreateClient();

        //act
        var response = await client.GetAsync($"/api/academies/{id}");

        //assert

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetById_ForExistingId_ShouldReturn200Ok()
    {
        //arrange
        var id = 99;

        var academy = new Academy
        {
            Id = id,
            Name = "Test Academy",
            Description = "Test Description",
        };

        _academiesRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync(academy);


        var client = _factory.CreateClient();

        //act
        var response = await client.GetAsync($"/api/academies/{id}");
        var academyDto = await response.Content.ReadFromJsonAsync<AcademyDto>();

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        academyDto.Should().NotBeNull();
        academyDto.Name.Should().Be("Test Academy");
        academyDto.Description.Should().Be("Test Description");


    }

    [Fact]
    public async Task GetAll_ForValidRequest_ShouldReturns200Ok()
    {
        // Arrange
        var client = _factory.CreateClient();
        // Act
        var result = await client.GetAsync("/api/academies?pageNumber=1&pageSize=10");
        // Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAll_ForINvalidRequest_ShouldReturns400BadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        // Act
        var result = await client.GetAsync("/api/academies");
        // Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }



}