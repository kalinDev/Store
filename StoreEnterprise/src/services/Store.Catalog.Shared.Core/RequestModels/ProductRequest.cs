﻿namespace Store.Catalog.Shared.Core.RequestModels;

public record ProductRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsAvailable { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public int StockQuantity { get; set; }
}