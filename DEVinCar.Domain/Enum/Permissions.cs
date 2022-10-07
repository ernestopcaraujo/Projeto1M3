using System.ComponentModel.DataAnnotations;

namespace DEVinCar.Domain.Enum
{
    public enum Permissions
    {
        [Display(Name= "Gerente")]
        Gerente,
        [Display(Name= "Vendedor")]
        Vendedor,
        [Display(Name= "Comprador")]
        Comprador
    }
}