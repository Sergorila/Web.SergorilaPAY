using RabbitMQ.Client;
using System.Text;
using SergorilaPAY2._0.Views;

namespace SergorilaPAY2._0.RabbitMQ
{
    public static class SendOrderRabbitMQ
    {
        public static void SendOrderWhenCreated(OrderView order)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var chanel = connection.CreateModel())
                {
                    chanel.QueueDeclare(
                        queue: "first",
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var message = $"Был создан заказ : {DateTime.Now} {Environment.NewLine}" +
                                  $"ID: {order.Id}" +
                                  $"UserID: {order.UserId}";
                                  var body = Encoding.UTF8.GetBytes(message);

                    chanel.BasicPublish(
                        exchange: "",
                        routingKey: "first",
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }
    }
}