using Core.DomainObjects.Entities;
using Core.DomainObjects.Interfaces;

namespace Store.Catalog.Domain.Entities;

public class Product : Entity, IAggregateRoot
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsAvailable { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public int StockQuantity { get; set; }
    public DateTime RegistredAt { get; set; } = DateTime.Now;

}