using Adaptadores.AMQP.Conexion;
using Adaptadores.AMQP.Enviar.Mensajes;
using Adaptadores.AMQP.Recibir.Recibir;
using Aplicacion.Servicios;
using Aplicacion.Servicios.Request;
using Aplicacion.Servicios.Response;
using Dominio.Servicios.Logs;
using Dominio.Servicios.Mensajes;
using Infraestructura.Framework.Logs;
using SimpleInjector;

namespace ServicioPong
{
    public class Injection
    {
        public Container Dependencias() {
        var container = new Container();
            var lifestyle = Lifestyle.Singleton;
            container.Register<IServicioMensajes, ServicioMensajes>();
            container.Register<IServicioEnviarMensaje, ServicioEnviarMensaje>();
            container.Register<IServicioConexion, ServicioConexion>();
            container.Register<IServicioLog, ServicioLog>();
            container.Register<IServicioLogs, ServicioLogs>();
            container.Register<IServicioRecibirMensaje, ServicioRecibirMensaje>();
            container.Register<IServicioRequest, ServicioRequest>();
            container.Register<IServicioResponse, ServicioResponse>();
            return container;

        }


    }
}
