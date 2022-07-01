# QT-Shop
Highly scalable ecommerce application with Event Driven Archiecture, Microservices with Kafka and .Net Core.
The communications between service to service are asynchronous with the help of Kafka as a event broker. 
The project is demonstrating a few scenrios that can occur in a ecommerce application
# I. Product upsert
![Kafka Communication - Page 1](https://user-images.githubusercontent.com/23560729/176568776-9f751aa2-3989-46e6-84dc-676825d2c8dd.png)
Create or update product event will update its MongoDB database and also produce two type of events ***"ProductCreated"*** and ***"ProductUpdated"***, Inventory Service is actinng as a consumer for those events and handle the logic base on
those events in inventory service.
To ensure that we process all event, the Catalog Service (Producer) implement outbox pattern that is being illustrated with the diagram below:
![Outbox pattern (1)](https://user-images.githubusercontent.com/23560729/176568906-6e0c8a8c-846d-47c3-9fbd-da622f4d7058.png)
* **(1)** The Catalog service save event details in Outbox Message table in SQL database with ***ReadyToSent*** state.
* **(2)** Outbox service worker run every 3 second (Configurable in app setting) and look for events that are in ***ReadyToSent*** state
* **(3)** Publish ***ReadyToSent*** events to Kafka
* **(4)** Update the events that have been published to ***Sent*** state
# II. Quantity updated
![Kafka Communication - Page 1 (1)](https://user-images.githubusercontent.com/23560729/176569577-5f35182d-38ee-432f-80d8-10cdcf82289c.png)
 Inventory service owns the inventory system of QTShop, whenever the user or admin update inventory. Beside update it owns SQL database, it also produce 
 a ***"ProductQuantityUpdated"*** event for a particular product Id and Category service this time will act as a consumer to consumer and handle ***"ProductQuantityUpdated"*** event.
