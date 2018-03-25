using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioPong
{
    static class Globales
    {
        public static int CantidadRecibidas;
        public static int CantidadEnviadas;

        public static void Inicializar()
        {
            CantidadRecibidas = 1;
            CantidadEnviadas = 1;
        }
    }
}
