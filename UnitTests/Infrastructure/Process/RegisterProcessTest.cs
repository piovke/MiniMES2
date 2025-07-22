using Application.DTOs;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.Infrastructure.Process;

public class RegisterProcessTest
{
    private ProcessRepository _repository;
    private ProcessService _service;

    private MiniProductionDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<MiniProductionDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        return new MiniProductionDbContext(options);
    }
    
    public RegisterProcessTest()
    {
        _repository = new ProcessRepository(CreateContext());
        _service = new ProcessService(_repository);
    }
    
    [Fact]
    public async Task SuccessCreatingProcess()
    {
        var context = CreateContext();
        context.Orders.Add(new Order
        {
            Id = 1,
            MachineId = 1,
            ProductId = 1,
            Code = "jeden"
        });
        context.Parameters.Add(new Parameter
        {
            Id = 1,
            Name = "temperatura",
            Unit = "stopnie celcjusza"
        });
        await context.SaveChangesAsync();
        _repository = new ProcessRepository(context);
        _service = new ProcessService(_repository);
        
        var dto = new CreateProcessDto
        {
            SerialNumber = 1,
            OrderId = 1,
            Status = "OK",
            DateTime = DateTime.MaxValue
        };
        
        var result = await _service.Create(dto);
        Assert.True(result.Success);
        Assert.Equal("added", result.Message);
    }

    [Fact]
    public async Task FailCreatingProcessNonExistingOrder()
    {
        var context = CreateContext();
        context.Parameters.Add(new Parameter()
        {
            Id = 1,
            Name = "temperatura",
            Unit = "stopnie celcjusza"
        });
        await context.SaveChangesAsync();
        _repository = new ProcessRepository(context);
        _service = new ProcessService(_repository);
        
        var dto = new CreateProcessDto
        {
            SerialNumber = 1,
            OrderId = 1,
            Status = "OK",
            DateTime = DateTime.MaxValue
        };
        
        var result = await _service.Create(dto);
        Assert.False(result.Success);
        Assert.Equal("order doesnt exist", result.Message);
    }

    [Fact]
    public async Task FailCreatingProcessNonExistingParameter()
    {
        var context = CreateContext();
        context.Parameters.Add(new Parameter()
        {
            Id = 1,
            Name = "temperatura",
            Unit = "stopnie celcjusza"
        });
        await context.SaveChangesAsync();
        _repository = new ProcessRepository(context);
        _service = new ProcessService(_repository);
        
        var dto = new CreateProcessDto
        {
            SerialNumber = 1,
            OrderId = 1,
            Status = "OK",
            DateTime = DateTime.Now.AddDays(-1)
        };
        
        var result = await _service.Create(dto);
        Assert.False(result.Success);
        Assert.Equal("Time should be from the future", result.Message);
    }
}