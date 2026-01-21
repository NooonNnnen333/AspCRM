using CRMSolution.Domain.CompanyesClients;
using CRMSolution.Presenters;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CRMSolution.Application;

public class CompanyesClientsService : ICompanyesClientsService
{
    private readonly ICompanyesClientsRepository _companyesClientsRepository;
    private readonly ILogger<CompanyesClients> _logger;
    private readonly IValidator<CreateCompanyesClientsDto> _validator;

    public CompanyesClientsService(
        ICompanyesClientsRepository companyesClientsRepository,
        IValidator<CreateCompanyesClientsDto> validator,
        ILogger<CompanyesClients> logger)
    {
        _companyesClientsRepository = companyesClientsRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Guid> Create(CreateCompanyesClientsDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var companyesClientsId = Guid.NewGuid();
        var companyesClients = new CompanyesClients
        {
            CompanyesClientsId = companyesClientsId,
            Mail = request.Mail,
            NumberOfPhone = request.NumberOfPhone,
            Inn = request.Inn
        };

        await _companyesClientsRepository.AddAsync(companyesClients, cancellationToken);
        _logger.LogInformation("CompanyesClients created with {companyesClientsId}", companyesClientsId);

        return companyesClientsId;
    }
}
