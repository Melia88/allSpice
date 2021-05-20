using System.ComponentModel.DataAnnotations;

namespace allSpace.Models
{
  public class Recipe
  {
    public int Id { get; set; }

    public string CreatorId { get; set; }

    public string Type { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
  }
}
// NOTE aparently type is a reserved keyword so now things in edit dont like us