namespace Store.Catalog.Shared.Core.ResponseModels;

public record ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsAvailable { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public int StockQuantity { get; set; }
    public DateTime RegistredAt { get; set; }
}