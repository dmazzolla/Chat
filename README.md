# Chat
Chat - RabbitMQ - C# .NET Core - Console Application

Linha de comando para executar o RabbitMQ em container Docker:
docker run -d --hostname rabbitserver --name rabbitmq-server -p 15672:15672 -p 5672:5672 rabbitmq:3-management


Caso ainda não tenha ativado o Management Plugin, segue linha de comando:
rabbitmq-plugins enable rabbitmq_management
