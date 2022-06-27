# QT-Shop
Ecommerce application with Event Driven Archiecture, Microservices with Kafka and .Net Core
The communications between service to service are asynchronous base with the help of Kafka as a event broker. 
The project is demonstrating a few scenrios that can occur in a ecommerce application
# I. Product upsert
![Blank diagram - Page 1 (3)](https://user-images.githubusercontent.com/23560729/176006527-f651e07f-5f87-4620-acbc-e224993e5765.png)
Create or update product event will update its MongoDB database and also produce two type of events ***"ProductCreated"*** and ***"ProductUpdated"***, Inventory Service is actinng as a consumer for those events and handle the logic base on
those events in inventory service.
# II. Quantity updated
![Blank diagram - Page 1 (2)](https://user-images.githubusercontent.com/23560729/176006550-100a0938-46dc-47f3-84f1-7d7af993ed19.png)
 Inventory service owns the inventory system of QTShop, whenever the user or admin update inventory. Beside update it owns SQL database, it also produce 
 a ***"ProductQuantityUpdated"*** event for a particular product Id and Category service this time will act as a consumer to consumer and handle ***"ProductQuantityUpdated"*** event.
