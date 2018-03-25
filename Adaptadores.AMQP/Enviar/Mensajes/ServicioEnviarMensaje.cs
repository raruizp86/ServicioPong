using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades.Mensajes;
using RabbitMQ.Client;
using Adaptadores.AMQP.Conexion;
using Dominio.Servicios.Logs;
using System.Threading;
using ServicioPong;
using Dominio.Entidades.Respuestas;

namespace Adaptadores.AMQP.Enviar.Mensajes
{
    public class ServicioEnviarMensaje : IServicioEnviarMensaje
    {

        private readonly IServicioConexion _servicioConexion;
        private readonly IServicioLog _servicioLog;
        private readonly RespuestaJson _respuesta;
        int enviadas= Globales.CantidadEnviadas;
        //private readonly IServicioMensajes servicioMensajes;
        public ServicioEnviarMensaje(IServicioConexion servicioConexion, IServicioLog servicioLog)
        {
            _servicioConexion = servicioConexion;
            _servicioLog = servicioLog;
            _respuesta = new RespuestaJson();
        }
        public bool PublicarMensaje(Mensaje mensaje,int recibidas)
        {
            int contador = 1;
            try
            {

                IConnection connection = _servicioConexion.Conectar();

                if (connection.IsOpen)
                {

                    using (var channel = connection.CreateModel())
                    {
                        Thread.Sleep(2000);
                        channel.ExchangeDeclare("Respuesta", ExchangeType.Direct, true, false, null);

                        channel.QueueDeclare("ColasRespuesta", true, false, false, null);

                        channel.QueueBind("ColasRespuesta", "Respuesta", "optional-routing-key");


                        var properties = channel.CreateBasicProperties();
                        properties.DeliveryMode = 1;
                        properties.ClearMessageId();
                        properties.ReplyTo = "Respuesta";
                        var encoding = new UTF8Encoding();
                        var hour = DateTime.Now.Hour;
                        var min = DateTime.Now.Minute;
                        var sec = DateTime.Now.Second;
                        var msg = string.Format(@"He recibido tu mensajes con el id: {3} desde Ping! Hora:{0} : {1} : {2}", hour, min, sec, mensaje.id);
                        var msgBytes = encoding.GetBytes(msg);

                        channel.BasicPublish("Respuesta", "optional-routing-key", false, properties, msgBytes);
                        enviadas++;
                        channel.Close();
                        _respuesta.respuestas(mensaje.id.ToString(),contador,recibidas);

                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                _servicioLog.RegistrarExcepcion("El mensaje con el id:no puedo ser enviado", ex);
                return false;
            }

        }


    }
}
