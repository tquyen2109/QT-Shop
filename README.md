# QT-Shop
Ecommerce application with Event Driven Archiecture, Microservices with Kafka and .Net Core
The communications between service to service are asynchronous base with the help of Kafka as a event broker. 
The project is demonstrating a few scenrios that can occur in a ecommerce application
# I. Product upsert
![Blank diagram - Page 1](https://user-images.githubusercontent.com/23560729/176003990-1e89aeb8-9f44-49f0-8d28-2e6e43ee3f13.png)
Create or update product event will update its MongoDB database and also produce two type of events ***"ProductCreated"*** and ***"ProductUpdated"***, Inventory Service is actinng as a consumer for those events and handle the logic base on
those events in inventory service.
# II. Quantity updated
![Blank diagram - Page 1 (1)](https://user-images.githubusercontent.com/23560729/176005099-81ff51b8-8497-4eac-8d82-3fdff39c18fe.png)
 Inventory service owns the inventory system of QTShop, whenever the user or admin update inventory. Beside update it owns SQL database, it also produce 
 a ***"ProductQuantityUpdated"*** event for a particular product Id and Category service this time will act as a consumer to consumer and handle ***"ProductQuantityUpdated"*** event.
