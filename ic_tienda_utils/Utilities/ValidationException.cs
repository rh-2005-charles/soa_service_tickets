using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ic_tienda_utils.Utilities
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; set; }

        public ValidationException(List<string> errors)
            : base(string.Join(", ", errors)) // Concatenamos los errores en el mensaje de la excepción
        {
            Errors = errors;
        }
    }
}