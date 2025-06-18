using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ic_tienda_utils.Utilities
{
    public class ValidationRules
    {
        // Validación para que el valor no sea nulo o vacío
        public static ValidationRule IsRequired = new ValidationRule(
            value => value is string str && !string.IsNullOrWhiteSpace(str),
            "El campo es obligatorio"
        );

        // Validación para un número positivo
        public static ValidationRule IsPositiveNumber = new ValidationRule(
            value => value is int i && i > 0,
            "El número debe ser mayor que cero"
        );

        // Validación para un número en rango
        public static ValidationRule IsRangeNumber = new ValidationRule(
            value => value is decimal i && i > 0 && i <= 100,
            "El número debe ser mayor que cero menor que cien"
        );



    }
    // public class ValidationRuleas
    // {
    //     public Func<object?, bool> Rule { get; set; }  // Función de validación
    //     public string ErrorMessage { get; set; }  // Mensaje de error si la validación falla
    // }

}