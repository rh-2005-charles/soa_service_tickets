using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ic_tienda_utils.Utilities
{
    public class UtilDatabase
    {
        // Método para obtener el nombre de la base de datos según la institución proporcionada.
        public static string GetDatabaseName(string institucion)
        {
            // Variable para almacenar el nombre de la base de datos.
            string dataBaseName = "";

            // Selecciona el nombre de la base de datos basado en el valor de 'institucion'.
            switch (institucion)
            {
                // Si la institución es "1", retorna "sales".
                case "1": dataBaseName = "sales"; break;

                // Por defecto, retorna "CicloudDemo".
                default:
                    dataBaseName = "CicloudDemo"; break;
            }

            // Retorna el nombre de la base de datos.
            return dataBaseName;
        }
    }
}