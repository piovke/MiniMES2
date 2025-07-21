using Application.DTOs;
using Domain.Models;

namespace Application.Services;

public interface IProcessParametersService
{
    Task<List<ProcessParameterDto>> List();
    Task<ProcessParameters?> GetById(int id);
    Task<ProcessParameterDto?> Details(int id);
    Task<bool> Create(CreateProcessParameterDto order);
    Task<bool> Update(int id, CreateProcessParameterDto order);
    Task<bool> Delete(int id);
}