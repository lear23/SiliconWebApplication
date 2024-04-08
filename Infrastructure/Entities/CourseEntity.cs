using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class CourseEntity
{

    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public string? ImageName { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Discount { get; set; }

    public int Hours { get; set; }

    public bool IsBestSeller { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public string? LikesNumbers { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public string? LikesProcent { get; set; }

}