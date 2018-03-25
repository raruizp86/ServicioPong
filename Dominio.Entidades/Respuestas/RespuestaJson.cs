using Newtonsoft.Json;
using ServicioPong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.Respuestas
{
    public class RespuestaJson
    {
        public DateTime fecha { get; set; }
        public  string Id { get; set; }
        public int cantidaRecibidas { get; set; }
        public int CantidadEnviadas { get; set; }
        public  string respuestas(string Id,int recibidas,int enviadas)
        {
            RespuestaJson res = new RespuestaJson();
            res.cantidaRecibidas = recibidas;
            res.CantidadEnviadas = enviadas; 
            res.fecha = DateTime.Now;
            res.Id = Id;
            string json = JsonConvert.SerializeObject(res);
            return json;
        }
    }
}
