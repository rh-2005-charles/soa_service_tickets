using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ic_tienda_utils.Validations
{
    public static class DataValidator
    {
        public static void ValidateUniqueName(bool exists, string fieldName)
        {
            if (exists)
            {
                throw new ArgumentException($"El {fieldName} ya está registrado. Intente con otro nombre.");
            }
        }

        public static void ValidateRequired(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"El campo {fieldName} es requerido.");
            }
        }

        public static void ValidateRequiredImage(IFormFile image, string fieldName)
        {
            if (image == null || image.Length == 0)
            {
                throw new ArgumentException($"Debe proporcionar una imagen para {fieldName}.");
            }
        }
    }
}