# Chat - RabbitMQ - C# .NET Core - Console Application


## 🚀 Começando

Um serviço está disponível temporariamente para não haver necessidade de qualquer instalação do RabbitMQ:

```
mazzolla.eastus2.cloudapp.azure.com:5672
```


Caso opte em executar o RabbitMQ localmente em container Docker:

```
docker run -d --hostname rabbitserver --name rabbitmq-server -p 15672:15672 -p 5672:5672
```


Também será necessário ativar o Management Plugin:

```
rabbitmq-plugins enable rabbitmq_management
```
