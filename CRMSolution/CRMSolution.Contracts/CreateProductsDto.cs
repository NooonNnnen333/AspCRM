namespace CRMSolution.Presenters;

public record CreateProductsDto(
    Guid ProductId,
    string NameOfProduct);

public record UpdateProductsDto(
    string NameOfProduct);
