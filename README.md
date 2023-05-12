# RabbitMqMailingSample

POC of mailing queue created with RabbitMq.

## Table of Contents
- [Configuration](#configuration)
- [Initial Setup](#initial-setup)
- [Usage](#usage)

## Configuration

Configuration files are located in <b>MailingConsumer/appsettings.json</b> and <b>MailingProducer/appsettings.json</b>.
<br>
### Configuration files - MailingConsumer/appsettings.json
```json
{
  "SmtpSettings": {
    "Server": "%smtp.yourdomain.com%",
    "Port": 587,
    "Username": "%youremail@yourdomain.com%",
    "Password": "%yourpassword%",
    "EnableSsl": true
  },
  "RabbitMQ": {
    "Hostname": "localhost",
    "Username": "guest",
    "Password": "guest",
    "VirtualHost": "/",
    "Port": "5672"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
### Configuration files - MailingProducer/appsettings.json
```json
{
  "RabbitMQ": {
    "Hostname": "localhost",
    "Username": "guest",
    "Password": "guest",
    "VirtualHost": "/",
    "Port": "5672"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## Initial setup
1. Run rabbit mq with parameters specified in configuration files.
If you use docker you can use command from scripts/run-rabbit-mq-by-docker.bat
```
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=guest -e RABBITMQ_DEFAULT_PASS=guest rabbitmq:3-management
```
2. Run both projects: <b>MailConsumer</b> and <b>MailProducer</b>

## Usage
Use /api/Mailing endpoint to send email to the queue.
<b>IMPORTANT<br>
If you want to send email via mail server, then provide parameter "sendMailType" with value of 1.<br>
Otherwise, the email will be only shown in MailingConsumer application console, and will not be send via actual mail server.
</b>
```json
{
  "from": "fromemail@fromdomain.com",
  "to": "toemail@todomain.com",
  "title": "Email title",
  "content": "Email content",
  "sendMailType": 0 //0 - //only show messege in console, do not send actual email, 1 - send via mail server
}
```
