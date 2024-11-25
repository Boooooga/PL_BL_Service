using System;
using System.Text;
using RabbitMQ.Client;

namespace PL_BL_Service
{
    public class RabbitMqClientService
    {
        private readonly IModel _channel;
        private readonly string _requestQueue;
        private readonly string _responseQueue;

        public RabbitMqClientService(IConfiguration configuration)
        {
            var factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQ:HostName"],
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"]
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _requestQueue = configuration["RabbitMQ:RequestQueue"];
            _responseQueue = configuration["RabbitMQ:ResponseQueue"];

            // Убедитесь, что очереди существуют
            _channel.QueueDeclare(_requestQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueDeclare(_responseQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: _requestQueue, basicProperties: null, body: body);
        }

        public string ReceiveMessage()
        {
            var result = _channel.BasicGet(queue: _responseQueue, autoAck: true);
            return result != null ? Encoding.UTF8.GetString(result.Body.ToArray()) : null;
        }
    }
}
