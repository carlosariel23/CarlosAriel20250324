using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarlosAriel20250324.Models;

public partial class User
{
    public int Id { get; set; }

    [Display(Name = "Nombre de Usuario")]
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    public string Username { get; set; } = null!;

    [Display(Name = "Correo Electrónico")]
    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]

    public string Email { get; set; } = null!;

    [Display(Name = "Contraseña")]
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string Password { get; set; } = null!;

    [Display(Name = "Rol")]
    [Required(ErrorMessage = "El rol es obligatorio.")]
    public string Role { get; set; } = null!;

    [Display(Name = "Notas")]
    public string? Notes { get; set; }
}