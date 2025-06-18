using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ic_tienda_utils.Utilities
{
    public class GenericValidator
    {
        /// <summary>
        /// Valida un objeto genérico utilizando un diccionario de reglas.
        /// </summary>
        /// <param name="entity">El objeto a validar.</param>
        /// <param name="rules">Diccionario de reglas de validación con el nombre de la propiedad y la regla asociada.</param>
        /// <returns>Un objeto ValidationResult con el resultado de la validación.</returns>
        public static ValidationResult Validate(
            object entity,
            Dictionary<string, ValidationRule> rules)
        {
            var validationResult = new ValidationResult();

            foreach (var rule in rules)
            {
                var propertyName = rule.Key;
                var validationRule = rule.Value;

                // Obtener información de la propiedad
                var propertyInfo = entity.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    validationResult.Errors.Add($"La propiedad '{propertyName}' no existe en '{entity.GetType().Name}'.");
                    continue;
                }

                // Obtener el valor de la propiedad
                var value = propertyInfo.GetValue(entity);

                // Aplicar la regla de validación
                if (!validationRule.Validate(value))
                {
                    validationResult.Errors.Add(validationRule.ErrorMessage);
                }
            }

            return validationResult;
        }
    }

    /// <summary>
    /// Representa una regla de validación con un mensaje de error.
    /// </summary>
    public class ValidationRule
    {
        public Func<object?, bool> Validate { get; }
        public string ErrorMessage { get; }

        public ValidationRule(Func<object?, bool> validate, string errorMessage)
        {
            Validate = validate;
            ErrorMessage = errorMessage;
        }
    }

    /// <summary>
    /// Representa el resultado de una validación.
    /// </summary>
    public class ValidationResult
    {
        public bool IsValid => !Errors.Any();
        public List<string> Errors { get; set; } = new();
    }
}