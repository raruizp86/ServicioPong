using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Json;

using ServicioPong;
using Newtonsoft.Json;
using Dominio.Entidades.Respuestas;

namespace Aplicacion.Servicios.Response
{
    public class ServicioResponse : IServicioResponse
    {

        public string ObtenerRespuesta()
        {
                     
            Respuesta respuesta = new Respuesta();
            respuesta.cantidaRecibidas = Globales.CantidadRecibidas;
            respuesta.CantidadEnviadas = Globales.CantidadEnviadas;
            

            string json = JsonConvert.SerializeObject(respuesta);
            return json;

        }
    }
}
