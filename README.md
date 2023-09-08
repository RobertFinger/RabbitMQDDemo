# RabbitMQDDemo
This is a short explanation of a message broker in c# 7,  for some friends who are learning to code.

first run a rabbit mq server. https://www.rabbitmq.com/download.html
docker run -it --rm --name rabbitmq -p 5672:5672 - p 15672:15672 rabbitmq: 3.12 - management
for now use guest / guest for user and pass.
click on the docker container to test.

Don't forget to set the startup projects to multiple projects and select both the sender and receivers.

Yes, we can run 2 instances of receiver 1.  But this is as rabbitmq demo, not a docker demo. 

