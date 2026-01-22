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
    private readonly IValidator<UpdateCompanyesClientsDto> _updateValidator;

    public CompanyesClientsService(
        ICompanyesClientsRepository companyesClientsRepository,
        IValidator<CreateCompanyesClientsDto> validator,
        IValidator<UpdateCompanyesClientsDto> updateValidator,
        ILogger<CompanyesClients> logger)
    {
        _companyesClientsRepository = companyesClientsRepository;
        _validator = validator;
        _updateValidator = updateValidator;
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

    public async Task<IReadOnlyList<CompanyesClients>> GetAll(CancellationToken cancellationToken)
    {
        return await _companyesClientsRepository.GetAllAsync(cancellationToken);
    }

    public async Task<CompanyesClients> GetById(Guid companyesClientsId, CancellationToken cancellationToken)
    {
        return await _companyesClientsRepository.GetByIdAsync(companyesClientsId, cancellationToken);
    }

    public async Task<Guid> Update(Guid companyesClientsId, UpdateCompanyesClientsDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _updateValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var companyesClients = new CompanyesClients
        {
            CompanyesClientsId = companyesClientsId,
            Mail = request.Mail,
            NumberOfPhone = request.NumberOfPhone,
            Inn = request.Inn
        };

        await _companyesClientsRepository.SaveAsync(companyesClients, cancellationToken);
        _logger.LogInformation("CompanyesClients updated with {companyesClientsId}", companyesClientsId);

        return companyesClientsId;
    }

    public async Task<Guid> Delete(Guid companyesClientsId, CancellationToken cancellationToken)
    {
        await _companyesClientsRepository.DeleteAsync(companyesClientsId, cancellationToken);
        _logger.LogInformation("CompanyesClients deleted with {companyesClientsId}", companyesClientsId);

        return companyesClientsId;
    }
}
