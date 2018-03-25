using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServicioPong
{
    public class WebServer
    {
        private TcpListener listener;
        private int port;
        private string home;
        private static int MAX_SIZE = 1000;
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
            Request resques = new Request();
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
                    //Console.WriteLine("Received: {0}", requestData);

                    Request request = resques.GetRequest(requestData);
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
