using Application.DTOs;
using Domain.Models;

namespace Application.Services;

public interface IProcessService
{
    Task<List<ProcessDto>> List();
    Task<Process?> GetById(int id);
    Task<ProcessDto?> Details(int id);
    Task<bool> Create(CreateProcessDto process);
    Task<bool> Update(int id, CreateProcessDto process);
    Task<bool> Delete(int id);
}