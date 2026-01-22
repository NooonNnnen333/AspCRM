using CRMSolution.Domain.Emploees;
using CRMSolution.Presenters;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CRMSolution.Application;

public class EmployeesService : IEmployeesService
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly ILogger<Employees> _logger;
    private readonly IValidator<CreateEmployeesDto> _validator;
    private readonly IValidator<UpdateEmployeesDto> _updateValidator;

    public EmployeesService(
        IEmployeesRepository employeesRepository,
        IValidator<CreateEmployeesDto> validator,
        IValidator<UpdateEmployeesDto> updateValidator,
        ILogger<Employees> logger)
    {
        _employeesRepository = employeesRepository;
        _validator = validator;
        _updateValidator = updateValidator;
        _logger = logger;
    }

    public async Task<Guid> Create(CreateEmployeesDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var employeesId = Guid.NewGuid();
        var employees = new Employees
        {
            EmployeesId = employeesId,
            Name = request.Name,
            Famaly = request.Famaly,
            Otchestvo = request.Otchestvo,
            Mail = request.Mail,
            Role = request.Role,
            NumberOfPhone = request.NumberOfPhone
        };

        await _employeesRepository.AddAsync(employees, cancellationToken);
        _logger.LogInformation("Employees created with {employeesId}", employeesId);

        return employeesId;
    }

    public async Task<IReadOnlyList<Employees>> GetAll(CancellationToken cancellationToken)
    {
        return await _employeesRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Employees> GetById(Guid employeesId, CancellationToken cancellationToken)
    {
        return await _employeesRepository.GetByIdAsync(employeesId, cancellationToken);
    }

    public async Task<Guid> Update(Guid employeesId, UpdateEmployeesDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _updateValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var employees = new Employees
        {
            EmployeesId = employeesId,
            Name = request.Name,
            Famaly = request.Famaly,
            Otchestvo = request.Otchestvo,
            Mail = request.Mail,
            Role = request.Role,
            NumberOfPhone = request.NumberOfPhone
        };

        await _employeesRepository.SaveAsync(employees, cancellationToken);
        _logger.LogInformation("Employees updated with {employeesId}", employeesId);

        return employeesId;
    }

    public async Task<Guid> Delete(Guid employeesId, CancellationToken cancellationToken)
    {
        await _employeesRepository.DeleteAsync(employeesId, cancellationToken);
        _logger.LogInformation("Employees deleted with {employeesId}", employeesId);

        return employeesId;
    }
}
