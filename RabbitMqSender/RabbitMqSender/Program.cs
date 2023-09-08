using RabbitMQ.Client;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

//first run a rabbit mq server. https://www.rabbitmq.com/download.html
//docker run -it --rm --name rabbitmq -p 5672:5672 - p 15672:15672 rabbitmq: 3.12 - management
//for now use guest / guest for user and pass.
//click on the docker container to test.

// don't forget to set the startup projects to multiple projects and select both the sender and receivers.


ConnectionFactory factory = new();

// Create a connection to RabbitMQ, we shouldn't be putting this in the code - add this to secrets.  But this is just a demo, so relax. ;-)

factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit Mambo #5";

IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();

// the z makes it cool.  Don't judge me.
string exchangeName = "exchangerz";
string routingKey = "route keyz";
string queueName = "queue namez";

channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct, durable: false, autoDelete: false, arguments: null);
channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey, arguments: null);


//ok, lets now send the messages:

for (int count = 0; count < 100; count++)
{
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes($"Who's the mother flipper?  I'm the mother flipper!  How many messages?  {count}");
    channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: messageBodyBytes);

}

    channel.Close();
    connection.Close();
