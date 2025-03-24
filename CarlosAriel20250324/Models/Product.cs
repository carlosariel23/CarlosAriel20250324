using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarlosAriel20250324.Models;

public partial class Product
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del Producto")]
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        public string ProductName { get; set; } = null!;

        [Display(Name = "Descripción")]
        public string? Description { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El precio es obligatorio.")]
        public decimal Price { get; set; }

        [Display(Name = "Precio de Compra")]
        public decimal PurchasePrice { get; set; }

        [Display(Name = "Almacén")]
        public int? WarehouseId { get; set; }

        [Display(Name = "Marca")]
        public int? BrandId { get; set; }

        [Display(Name = "Notas")]
        public string? Notes { get; set; }
        [Display(Name = "Marca")]
        public virtual Brand? Brand { get; set; }
        [Display(Name = "Almacén")]
        public virtual Warehouse? Warehouse { get; set; }
    }