using Microsoft.Extensions.Logging;
using EduLink.Api.Middlewares;
using Moq;
using Microsoft.AspNetCore.Http;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Entities;
using FluentAssertions;

namespace EduLink.Api.Tests.Middlewares;

public class GlobalErrorHandlerMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_WhenNoExceptionThrown_ShouldNotModifyResponse()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<GlobalErrorHandlerMiddleware>>();
        var middleware = new GlobalErrorHandlerMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();
        var nextDelegateMock = new Mock<RequestDelegate>();

        //act
        await middleware.InvokeAsync(context, nextDelegateMock.Object);

        //assert
        nextDelegateMock.Verify(next => next(context), Times.Once);

    }


    [Fact]
    public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetStatusCodeTo404()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<GlobalErrorHandlerMiddleware>>();
        var middleware = new GlobalErrorHandlerMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();
        var notFoundException = new NotFoundException(nameof(Academy), "1");

        //act
        await middleware.InvokeAsync(context, _ => throw notFoundException);

        //assert
        context.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);

    }


    [Fact]
    public async Task InvokeAsync_WhenNotForbidExeptionThrown_ShouldSetStatusCodeTo403()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<GlobalErrorHandlerMiddleware>>();
        var middleware = new GlobalErrorHandlerMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();
        var forbidExeption = new ForbidExeption();

        //act
        await middleware.InvokeAsync(context, _ => throw forbidExeption);

        //assert
        context.Response.StatusCode.Should().Be(StatusCodes.Status403Forbidden);

    }

    [Fact]
    public async Task InvokeAsync_WhenGenericExeptionThrown_ShouldSetStatusCodeTo500()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<GlobalErrorHandlerMiddleware>>();
        var middleware = new GlobalErrorHandlerMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();
        var exeption = new Exception();

        //act
        await middleware.InvokeAsync(context, _ => throw exeption);

        //assert
        context.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError  );

    }


}