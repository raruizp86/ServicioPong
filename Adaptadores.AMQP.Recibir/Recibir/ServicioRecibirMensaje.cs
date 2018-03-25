using System;
using System.Text;
using Adaptadores.AMQP.Conexion;
using Dominio.Servicios.Logs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using Adaptadores.AMQP.Enviar.Mensajes;
using ServicioPong;
using Dominio.Entidades.Mensajes;

namespace Adaptadores.AMQP.Recibir.Recibir
{
    public class ServicioRecibirMensaje : IServicioRecibirMensaje
    {
        private readonly IServicioConexion _servicioConexion;
        private readonly IServicioLog _servicioLog;
        private readonly IServicioEnviarMensaje _servicioEnviarMensaje;
        int recibidas = Globales.CantidadRecibidas;
        public ServicioRecibirMensaje(IServicioConexion servicioConexion, IServicioLog servicioLog, IServicioEnviarMensaje servicioEnviarMensaje)
        {
            _servicioConexion = servicioConexion;
            _servicioLog = servicioLog;
            _servicioEnviarMensaje = servicioEnviarMensaje;
        }
        public void SuscribirMensaje()
        {
           
            string mensaje = string.Empty;
            try
            {
               

                IConnection connection = _servicioConexion.Conectar();
                using (var channel = connection.CreateModel())
                {
                    // This instructs the channel not to prefetch more than one message
                    channel.BasicQos(0, 1, false);

                    // Create a new, durable exchange
                    channel.ExchangeDeclare("Servicios", ExchangeType.Direct, true, false, null);
                    // Create a new, durable queue
                    channel.QueueDeclare("Colas", true, false, false, null);
                    // Bind the queue to the exchange
                    channel.QueueBind("Colas", "Servicios", "optional-routing-key");

                    using (var subscription = new Subscription(channel, "Colas", false))
                    {
                        Console.WriteLine("Waiting for messages...");
                        var encoding = new UTF8Encoding();
                        while (channel.IsOpen)
                        {
                            BasicDeliverEventArgs eventArgs;
                            var success = subscription.Next(2000, out eventArgs);
                            if (success == false) continue;
                            var msgBytes = eventArgs.Body;
                            var message = encoding.GetString(msgBytes);
                            mensaje = message;
                            Console.WriteLine(message);
                            Console.WriteLine("van esta cantidad de mensajes:"+ recibidas);
                            channel.BasicAck(eventArgs.DeliveryTag, false);
                            _servicioEnviarMensaje.PublicarMensaje(obtenerMensaje(mensaje), recibidas);
                            recibidas++;
                           

                        }
                        
                    }

                }

            }

            catch (Exception ex)
            {
                string mensanje = "No se pudo recibir mensajes";
                _servicioLog.RegistrarExcepcion(mensanje, ex);

            }
        }

        private Mensaje obtenerMensaje(string data)
        {
            var requestData = data.Split(' ');
            string id = requestData[1].Split(':')[1].ToString();
            string mensaje = requestData[10] + " : " + requestData[11];
            Mensaje objMensaje;


            objMensaje = new Mensaje()
            {
                id = int.Parse(id),
                Cuerpo = mensaje,
            };


            return objMensaje;
        }
}
}
