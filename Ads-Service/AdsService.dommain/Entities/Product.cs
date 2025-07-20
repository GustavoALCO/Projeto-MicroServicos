using System.ComponentModel.DataAnnotations;

namespace AdsService.Dommain.Entities;

public class Product
{
    [Key]
    public required Guid IdProduct { get; set; }

    public required Guid IdUser { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string State { get; set; }

    public required string City { get; set; }

    public required string ZipCode { get; set; }

    public required bool IsValid { get; set; }

    public List<Images> Images { get; set; }    
    
    public Category Category { get; set; }
}
