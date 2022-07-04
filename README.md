# QT-Shop
Highly scalable ecommerce application with Event Driven Archiecture, Microservices with Kafka and .Net Core.
The communications between service to service are asynchronous with the help of Kafka as a event broker. 
The project is demonstrating a few scenrios that can occur in a ecommerce application
# Product upsert
![Kafka Communication - Page 1](https://user-images.githubusercontent.com/23560729/176568776-9f751aa2-3989-46e6-84dc-676825d2c8dd.png)
Create or update product event will update its MongoDB database and also produce two type of events ***"ProductCreated"*** and ***"ProductUpdated"***, Inventory Service is actinng as a consumer for those events and handle the logic base on
those events in inventory service.
To ensure that we process all event, the Catalog Service (Producer) implement outbox pattern that is being illustrated with the diagram below:
![Outbox pattern (1)](https://user-images.githubusercontent.com/23560729/176568906-6e0c8a8c-846d-47c3-9fbd-da622f4d7058.png)
* **(1)** The Catalog service save event details in Outbox Message table in SQL database with ***ReadyToSent*** state.
* **(2)** Outbox service worker run every 3 second (Configurable in app setting) and look for events that are in ***ReadyToSent*** state
* **(3)** Publish ***ReadyToSent*** events to Kafka
* **(4)** Update the events that have been published to ***Sent*** state
# Quantity updated
![Kafka Communication - Page 1 (1)](https://user-images.githubusercontent.com/23560729/176569577-5f35182d-38ee-432f-80d8-10cdcf82289c.png)
 Inventory service owns the inventory system of QTShop, whenever the user or admin update inventory. Beside update it owns SQL database, it also produce 
 a ***"ProductQuantityUpdated"*** event for a particular product Id and Category service this time will act as a consumer to consumer and handle ***"ProductQuantityUpdated"*** event.
 # Product/Basket update
 Catalog service sometimes update a particular product, it might be pricing or product name. After successfully update the product within the service. Catalog service will publish a ***"ProductUpdated"*** event for Basket service to consumer. Basket service will look for all baskets that currently have the product and update the product accordingly.
 ![Blank diagram (1)](https://user-images.githubusercontent.com/23560729/177225458-936dc052-7c09-4845-bbc5-f5d1e1bc3f1e.png)
# Placing order
For order service, we break down the service into two separate projects as Order Command and Order Query for CQSR pattern. We also utilize Event Sourcing pattern for Order Command to record all of the events for order process. The records are being stored in Mongo DB under OrderEventCollection database. After creating order with *Pending* status. Order Command service publish an ***OrderPlaced*** events to all of the consumers that have interests in this event (Basket, Order Query, Invetory)
* Basket service will delete the basket after the order is placed.
* Order query service will update it owns SQL database for data consistency and available for query API.
* Inventory service will check if we have enough quantity to fullfil this order.
![Order Placed Event](https://user-images.githubusercontent.com/23560729/177225176-1baa3d07-710d-44f1-b81a-e47c0d35cb63.png)
