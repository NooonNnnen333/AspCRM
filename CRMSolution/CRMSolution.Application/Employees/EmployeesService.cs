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

    public EmployeesService(IEmployeesRepository employeesRepository, IValidator<CreateEmployeesDto> validator, ILogger<Employees> logger)
    {
        _employeesRepository = employeesRepository;
        _validator = validator;
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
}
