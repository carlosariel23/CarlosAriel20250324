using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarlosAriel20250324.Models;

public partial class Warehouse
{
    public int Id { get; set; }

    [Display(Name = "Nombre del Almacén")]
    [Required(ErrorMessage = "El nombre del almacén es obligatorio.")]
    public string WarehouseName { get; set; } = null!;

    [Display(Name = "Notas")]
    public string? Notes { get; set; }
    [Display(Name = "Productos")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}