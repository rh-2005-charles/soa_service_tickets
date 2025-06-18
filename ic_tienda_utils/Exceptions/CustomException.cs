using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ic_tienda_utils.Exceptions
{
    // Define una clase CustomException que hereda de la clase base Exception.
    public class CustomException : Exception
    {
        // Propiedades para almacenar detalles adicionales sobre la excepción.
        ////    Código HTTP asociado con la excepción (por ejemplo, 404, 500).
        public int httpCode = 0;
        ////    Código de error específico (valor por defecto es 500).
        public int errorCode = 500;
        ////    Tipo de error (una cadena que describe el tipo de error).
        public string tipoError = "";
        ////    Mensaje de error (generalmente proporcionado por el usuario).
        public string message = "";

        // Constructor por defecto, llama al constructor base de Exception (sin mensaje).
        public CustomException() : base() { }

        // Constructor que recibe un mensaje y un código HTTP. Inicializa las propiedades con esos valores.
        public CustomException(string message, int httpCode)
        {
            this.httpCode = httpCode;
            this.message = message;
        }

        // Constructor que recibe mensaje, código HTTP, código de error y tipo de error.
        // Inicializa todas las propiedades y también pasa el mensaje al constructor base de Exception.
        public CustomException(string message, int httpCode, int errorCode, string tipoError) : base(message)
        {
            this.httpCode = httpCode;
            this.errorCode = errorCode;
            this.message = message;
            this.tipoError = tipoError;
        }

        // Constructor que recibe un mensaje, código HTTP, código de error, tipo de error y una excepción interna.
        // Llama al constructor base de Exception pasando el mensaje y la excepción interna.
        public CustomException(string message, int httpCode, int errorCode, string tipoError, Exception inner) : base(message, inner)
        {
            this.httpCode = httpCode;
            this.errorCode = errorCode;
            this.message = message;
            this.tipoError = tipoError;
        }
    }
}