namespace CRMSolution.Domain.Products;

public class Products
{
    public required Guid ProductId { get; set; }

    public string NameOfProduct { get; set; } = string.Empty;
}
