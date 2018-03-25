using Dominio.Servicios.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios.Request
{
    public class ServicioRequest : IServicioRequest
    {
        private readonly IServicioMensajes _servicioMensajes;
        public ServicioRequest(IServicioMensajes servicioMensajes)
        {
            _servicioMensajes = servicioMensajes;
        }
        public void procesarRequest(string requestData)
        {
            string data = string.Empty;
            _servicioMensajes.RecibirMensaje();
            //var list = requestData.Split(' ');
            //_servicioMensajes.EnviarMensaje(list[0],list[1]);
        }
    }
}
