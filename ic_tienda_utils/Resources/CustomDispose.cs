using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ic_tienda_utils.Resources
{
    public class CustomDispose
    {
        // Método Dispose que suprime la finalización del objeto.
        public void Dispose()
        {
            // Evita que el recolector de basura llame al finalizador de este objeto.
            GC.SuppressFinalize(this);
        }
    }
}