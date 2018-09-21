# Task 1 - Basic local setup

In this task you will create docker images and run then using docker on your local system.

After completing this task you should have a good grasp of knowledge to build images and run them in conjuction in docker

## Step 1 - Building docker images

In order to run containers on docker you should create images of your applications
Build images of the order, order-listener, and process applications see https://docs.docker.com/engine/reference/commandline/build/ and https://docs.docker.com/engine/examples/dotnetcore/#prerequisites for more information.

## Step 2 - Creating a docker network

In order for docker containers to communicate with each other they should be on the same docker network
Create a docker network to run your containers on see https://docs.docker.com/network/

## Step 3 - Run containers for MongoDB and RabbitMQ on your docker network

MongoDB and RabbitMQ have images on the docker hub we can use to quickly spin up services

```
docker run --network=<network name> --name=mongodb -d mongo:4.1.1
docker run --network=<network name --name=rabbitmq -d rabbitmq:3.7.7
```

## Step 4 - Run containers for your application images on your docker network

The application images should run without any additionally setup as long as they are named correctly

## Step 5 - Post an order to the order api

```
{
	"emailAddress": "dev@docker.dev",
	"preferredLanguage": "en-GB",
	"product": "ice cream",
	"total": 1000.0
}
```

## Step 6 - Examine the logs of the docker containers to see if your order went through

If successful you should see something like

```
Looking for { orderid: 5ba4e6426134ce00018f10e4, status: "Open" }
Retrieving from MongoDB retrieved from 0ms
```