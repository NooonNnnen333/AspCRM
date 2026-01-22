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
    private readonly IValidator<UpdateProductsDto> _updateValidator;

    public ProductsService(
        IProductsRepository productsRepository,
        IValidator<CreateProductsDto> validator,
        IValidator<UpdateProductsDto> updateValidator,
        ILogger<Products> logger)
    {
        _productsRepository = productsRepository;
        _validator = validator;
        _updateValidator = updateValidator;
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

    public async Task<IReadOnlyList<Products>> GetAll(CancellationToken cancellationToken)
    {
        return await _productsRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Products> GetById(Guid productId, CancellationToken cancellationToken)
    {
        return await _productsRepository.GetByIdAsync(productId, cancellationToken);
    }

    public async Task<Guid> Update(Guid productId, UpdateProductsDto request, CancellationToken cancellationToken)
    {
        var validationResult = await _updateValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var products = new Products
        {
            ProductId = productId,
            NameOfProduct = request.NameOfProduct
        };

        await _productsRepository.SaveAsync(products, cancellationToken);
        _logger.LogInformation("Products updated with {productId}", productId);

        return productId;
    }

    public async Task<Guid> Delete(Guid productId, CancellationToken cancellationToken)
    {
        await _productsRepository.DeleteAsync(productId, cancellationToken);
        _logger.LogInformation("Products deleted with {productId}", productId);

        return productId;
    }
}
