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

    public ClientService(IClientRepository clientRepository, IValidator<CreateClientDto> validator, ILogger<Client> logger)
    {
        _clientRepository = clientRepository;
        _validator = validator;
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
}
