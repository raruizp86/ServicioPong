﻿using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptadores.AMQP.Conexion
{
    public interface IServicioConexion
    {
        IConnection Conectar();
    }
}
