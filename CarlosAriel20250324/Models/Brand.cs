using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarlosAriel20250324.Models;

public partial class Brand
{
    public int Id { get; set; }  

    [Display(Name = "Nombre de la Marca")]
    [Required(ErrorMessage = "El nombre de la marca es obligatorio.")]
    public string BrandName { get; set; } = null!;

    [Display(Name = "País")]
    public string? Country { get; set; }  

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
