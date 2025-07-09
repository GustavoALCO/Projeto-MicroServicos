using System.ComponentModel.DataAnnotations;

namespace AdsService.Dommain.Entities;

public class Product
{
    [Key]
    public required Guid IdProduct { get; set; }

    public required Guid IdUser { get; set; }

    [MaxLength(100)]
    public required string Title { get; set; }

    [MaxLength(200)]
    [MinLength(20)]
    public required string Description { get; set; }

    [MaxLength (2)]
    [MinLength(2)]
    public required string State { get; set; }

    [MinLength(3)]
    public required string City { get; set; }

    public List<Images> Images { get; set; }    

    // Mostra todos as entidades de categorias
    public Category Category { get; set; }

}
