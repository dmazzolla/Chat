# Chat
Chat - RabbitMQ - C# .NET Core - Console Application


Linha de comando para executar o RabbitMQ em container Docker:

docker run -d --hostname rabbitserver --name rabbitmq-server -p 15672:15672 -p 5672:5672 rabbitmq:3-management



Linha de comando para ativar o Management Plugin (caso ainda não ativo)

rabbitmq-plugins enable rabbitmq_management
