This Solution contains all dependable project for shopBridge API as separate project so that in future it can be used for other project if needed which will be easy for doing new changes.OpenAPI Swagger is attached in project.

Run --> have to set ShopBridge.ServiceLayer as startup project and run with IISexpress swagger will open with help of the all action methods can be invoked and tested.

Projects 
1.) ShopBridge.ServiceLayer --> Actual API service layer which will contain controllers.
2.) ShopBridge.BusinessLayer --> All Business logic will reside here
3.) ShopBridge.BusinessObject --> All Business object will reside here
4.) ShopBridge.DataAccess--> All Database related changes will reside here
5.) ShopBridge.UnitTest--> UnitTest Coverage for all layers

Database :

1.) I have used sqLite with Dapper ORM which will is light weight and faster in terms of performance
2.) Database file shopBride.db 
IT is attached with solution and it will persist data even on refresh.

3.) table name --> item

CREATE TABLE "Item" (
	"Id"	INTEGER NOT NULL,
	"Name"	TEXT NOT NULL,
	"Price"	NUMERIC NOT NULL,
	"Description"	TEXT,
	"Quantity"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);

Model:

public class Item
    {
        public string Name { get; set; } --> mandatory
        public int Id { get; set; } 
        public string Description { get; set; }
        public decimal Price { get; set; } --> Mandatory
        public int Quantity { get; set; } --> Mandatory

}
