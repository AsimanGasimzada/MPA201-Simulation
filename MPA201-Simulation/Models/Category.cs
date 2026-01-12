using MPA201_Simulation.Models.Common;

namespace MPA201_Simulation.Models;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = [];
}