using System;
using System.Collections.Generic;
using Dominio.Entidades.Mensajes;
using Adaptadores.AMQP.Recibir.Recibir;

namespace Dominio.Servicios.Mensajes
{
    public class ServicioMensajes : IServicioMensajes
    {
     
        public readonly IServicioRecibirMensaje _servicioRecibirMensaje;
        public ServicioMensajes( IServicioRecibirMensaje servicioRecibirMensaje)
        {
           
            _servicioRecibirMensaje = servicioRecibirMensaje;
        }
        public bool EnviarMensaje(string id, string mensaje)
        {
            Mensaje obj = new Mensaje();
            List<Mensaje> Respuesta = new List<Mensaje>();
            Mensaje MensajeEnviar = MensajeEnviadoFormato(id, mensaje);
          
            return true;
        }


        public Mensaje MensajeEnviadoFormato(string id, string mensaje)
        {
            Mensaje objMensaje;


            objMensaje = new Mensaje()
            {
                id = int.Parse(id),
                Cuerpo = mensaje,
            };


            return objMensaje;
        }

        //public string RecibirMensaje(string id, string mensaje)
        //{
        //    Mensaje obj = new Mensaje();
        //    List<Mensaje> Respuesta = new List<Mensaje>();
        //    Mensaje MensajeEnviar = MensajeEnviadoFormato(id, mensaje);
        //    int respuesta = _servicioRecibirMensaje.SuscribirMensaje();
        //    return respuesta.ToString();
        //}

        public void RecibirMensaje()
        {
            int contador = 1;
            string fecha ="Mesanje recibido a las"+ DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
            Mensaje MensajeEnviar = MensajeEnviadoFormato(contador.ToString(), fecha);
            _servicioRecibirMensaje.SuscribirMensaje();
            contador++;
        }
    }
}
