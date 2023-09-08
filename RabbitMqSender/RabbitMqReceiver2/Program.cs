using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


ConnectionFactory factory = new();

// Create a connection to RabbitMQ, we shouldn't be putting this in the code - add this to secrets.  But this is just a demo, so relax. ;-)

factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit Mambo #5 receiver 2";

IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();

// the z makes it cool.  Don't judge me.
string exchangeName = "exchangerz";
string routingKey = "route keyz";
string queueName = "queue namez";

channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct, durable: false, autoDelete: false, arguments: null);
channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey, arguments: null);

channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    //zomg, this server is twice as slow as the other one.  It takes 10 seconds to process a message!!!

    // Yes, we can run 2 instances of receiver 1.  But this is as rabbitmq demo, not a docker demo. 

    Task.Delay(TimeSpan.FromSeconds(10)).Wait();
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Received message: {message}");

    //this is where we would save to a db or whatever. 
    //but we need to tell rabbit that we got the message.

    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
};

// make it so we can cancel the message.
string consumerTag = channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);


//this is just so we can see the messages.
Console.ReadLine();

channel.BasicCancel(consumerTag);
channel.Close();
connection.Close(); 
