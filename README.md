# Test_HelioOjeda
API REST with authentication and authorization

## Getting Started
First of all you need to run the application. When it's running, you will need to use an HTTP Client such as Postman to make the API operations.

First operation you have to make is the authentication, sending an HTTP POST request to "http://localhost:62567/api/Token" with the 
username and password in the body in JSON. In my SQL database there are two users to test:

ADMIN:    {"username": "Helio", "password": "helio"}
NO ADMIN: {"username": "Juan", "password": "juan"}

When you have done the correct authentication, you will receive a token. Copy it because you will need to pass it in the HTTP Authorization 
of the request (selecting "TYPE: Bearer Token") in order to make all operations. And then, if you are admin you will be able to make all the
CRUD operations (Users and Customers) but if you are not, you will only be able to access to Customer CRUD operations.

POST and PUT operations will need a JSON with a User/Customer format in the request body:

CUSTOMER: {"name":"Helio", "surname":"Ojeda", "photoUrl":"testingUrl"}
USER:     {"name":"Helio", "pasword":"helio", "isAdmin": true}

### Installing

· Visual Studio 2017

· Postman or similar

· MySQL Workbench or similar

If you want the API to work without changes you will need the folowwing database (user: root, password: root) with the following tables:

CREATE DATABASE test_theam;
USE test_theam;

DROP TABLE IF EXISTS users; 
CREATE TABLE users (id int(11) NOT NULL AUTO_INCREMENT, name varchar(45) NOT NULL, 
password varchar(45) NOT NULL, isAdmin tinyint(4) NOT NULL DEFAULT '0',
PRIMARY KEY (id) ) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

INSERT INTO users VALUES (1,'Helio','helio',1),(2,'Juan','juan',0);

DROP TABLE IF EXISTS customers; 
CREATE TABLE customers ( id int(11) NOT NULL AUTO_INCREMENT, name varchar(45) NOT NULL, surname varchar(45) NOT NULL, 
photoUrl varchar(45) DEFAULT NULL, createdBy varchar(45) DEFAULT NULL, modifiedBy varchar(45) DEFAULT NULL, 
PRIMARY KEY (id) ) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;

INSERT INTO customers VALUES (1,'Helio','Ojeda','www.prueba.com',NULL,NULL),(3,'Comprador','Modificado',NULL,NULL,'Helio'),
(6,'Juan','González','www.juangonzalez.com',NULL,NULL),(7,'Rodrigo','Torres',NULL,NULL,'Helio'),
(11,'Pedro','Rodríguez','www.pedrorod.es',NULL,NULL),(12,'Juan','Rodríguez','www.juanrd.es','Juan',NULL),
(13,'Alejandro','Reyes','www.alereyes.es','Helio',NULL);

## Built With
 · Visual Studio 2017 with ASP.NET Core 2.
 
 ## Authors
 · Helio Ojeda Reyes
