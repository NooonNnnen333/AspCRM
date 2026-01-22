using CRMSolution.Domain.Client;
using CRMSolution.Presenters;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CRMSolution.Application;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly ILogger<Client> _logger;
    private readonly IValidator<CreateClientDto> _validator;
    private readonly IValidator<UpdateClientDto> _updateValidator;

    public ClientService(
        IClientRepository clientRepository,
        IValidator<CreateClientDto> validator,
        IValidator<UpdateClientDto> updateValidator,
        ILogger<Client> logger)
    {
        _clientRepository = clientRepository;
        _validator = validator;
        _updateValidator = updateValidator;
        _logger = logger;
    }

    public async Task<Guid> Create(CreateClientDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var clientId = Guid.NewGuid();
        var client = new Client
        {
            ClientId = clientId,
            Name = request.Name,
            Famaly = request.Famaly,
            Otchestvo = request.Otchestvo,
            Mail = request.Mail,
            NumberOfPhone = request.NumberOfPhone,
            NumbersOfReqest = request.NumbersOfReqest,
            Passport = request.Passport
        };

        await _clientRepository.AddAsync(client, cancellationToken);
        _logger.LogInformation("Client created with {clientId}", clientId);

        return clientId;
    }

    public async Task<IReadOnlyList<Client>> GetAll(CancellationToken cancellationToken)
    {
        return await _clientRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Client> GetById(Guid clientId, CancellationToken cancellationToken)
    {
        return await _clientRepository.GetByIdAsync(clientId, cancellationToken);
    }

    public async Task<Guid> Update(Guid clientId, UpdateClientDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _updateValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var client = new Client
        {
            ClientId = clientId,
            Name = request.Name,
            Famaly = request.Famaly,
            Otchestvo = request.Otchestvo,
            Mail = request.Mail,
            NumberOfPhone = request.NumberOfPhone,
            NumbersOfReqest = request.NumbersOfReqest,
            Passport = request.Passport
        };

        await _clientRepository.SaveAsync(client, cancellationToken);
        _logger.LogInformation("Client updated with {clientId}", clientId);

        return clientId;
    }

    public async Task<Guid> Delete(Guid clientId, CancellationToken cancellationToken)
    {
        await _clientRepository.DeleteAsync(clientId, cancellationToken);
        _logger.LogInformation("Client deleted with {clientId}", clientId);

        return clientId;
    }
}
