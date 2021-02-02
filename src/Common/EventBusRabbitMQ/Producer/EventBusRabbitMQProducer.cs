using EventBusRabbitMQ.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Producer
{
    public class EventBusRabbitMQProducer
    {
        private readonly IRabbitMQConnection _connection;

        public EventBusRabbitMQProducer(IRabbitMQConnection connection)
        {
            _connection = connection;
        }

        public void PublishBasketCheckout(string queueName, BasketCheckoutEvent publishModel)
        {
            using (var channel = _connection.CreateModel())
            {
                //Name
                //Durable(the queue will survive a broker restart)
                //Exclusive(used by only one connection and the queue will be deleted when that connection closes)
                //Auto - delete(queue that has had at least one consumer is deleted when last consumer unsubscribes)
                //Arguments(optional; used by plugins and broker - specific features such as message TTL, queue length limit, etc)

                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments:null);

                var message = JsonConvert.SerializeObject(publishModel);

                var body = Encoding.UTF8.GetBytes(message);


                //2 for "persistent", 1 for "transient".Some client libraries expose this property as a boolean or enum.

                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                channel.ConfirmSelect();
                channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: true, basicProperties: properties, body: body);
                channel.WaitForConfirmsOrDie();

                channel.BasicAcks += (sender, eventArgs) =>
                {
                    Console.WriteLine("Send RabbitMQ");
                };

                channel.ConfirmSelect();

            }
        }
    }
}
