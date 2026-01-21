using CRMSolution.Domain.Products;
using CRMSolution.Presenters;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CRMSolution.Application;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;
    private readonly ILogger<Products> _logger;
    private readonly IValidator<CreateProductsDto> _validator;

    public ProductsService(IProductsRepository productsRepository, IValidator<CreateProductsDto> validator, ILogger<Products> logger)
    {
        _productsRepository = productsRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Guid> Create(CreateProductsDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var productId = Guid.NewGuid();
        var products = new Products
        {
            ProductId = productId,
            NameOfProduct = request.NameOfProduct
        };

        await _productsRepository.AddAsync(products, cancellationToken);
        _logger.LogInformation("Products created with {productId}", productId);

        return productId;
    }
}
