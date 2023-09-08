first run a rabbit mq server. https://www.rabbitmq.com/download.html

docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management

for now use guest/guest for user and pass.

click on the docker container to test.