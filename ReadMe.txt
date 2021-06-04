This Solution contains all dependable project for shopBridge API

1.)Database :

I have used Sqllite and dapper ORM for database 

table name --> item

CREATE TABLE "Item" (
	"Id"	INTEGER NOT NULL,
	"Name"	TEXT NOT NULL,
	"Price"	NUMERIC NOT NULL,
	"Description"	TEXT,
	"Quantity"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);


1.) Service layer --> ShopBridge 