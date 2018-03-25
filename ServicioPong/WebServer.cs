using Aplicacion.Servicios;
using Aplicacion.Servicios.Request;
using Aplicacion.Servicios.Response;
using SimpleInjector;
using System;
using System.Net;
using System.Net.Sockets;

namespace ServicioPong
{
    public class WebServer
    {
        private TcpListener listener;
        private int port;
        private string home;
        private static int MAX_SIZE = 1000;
        private readonly IServicioRequest _servicioRequest;
        public WebServer(IServicioRequest servicioRequest)
        {
            _servicioRequest = servicioRequest;
        }
        public WebServer(int port, string path)
        {
            this.port = port;
            this.home = path;
            listener = new TcpListener(IPAddress.Any, port);
            
        }
        public void Start()
        {
            listener.Start();
            Console.WriteLine(string.Format("Local server started at localhost:{0}", port));

            Console.CancelKeyPress += delegate
            {
                Console.WriteLine("Stopping server");
                StopServer();
            };
        }
        public void Listen()
        {
            
            try
            {
                while (true)
                {
                    Byte[] result = new Byte[MAX_SIZE];
                    string requestData;
                    TcpClient client = listener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    int size = stream.Read(result, 0, result.Length);
                    requestData = System.Text.Encoding.ASCII.GetString(result, 0, size);
                    Injection injection = new Injection();
                    var container = injection.Dependencias();
                    

                   
                    
                    var bl = container.GetInstance<ServicioRequest>();
                    bl.procesarRequest(requestData);
                    //Console.WriteLine("Received: {0}", requestData);container.Resolve<TasksAgentService>()
                    //_servicioRequest.procesarRequest();
                    /*equest request = resques.GetRequest(requestData);*/
                    //resques.ProcessRequest(request, stream);
                   
                    
                    client.Close();
                }
            }
            finally
            {
                listener.Stop();
            }
        }

      
        private void StopServer()
        {
            listener.Stop();
        }






    }
}