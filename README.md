# Csharp-Portfolio

ENGLISH VERSION --> Versão em Português abaixo


Projects using C#/.NET and SQL Server for real business needs.

####################### CUSTOMER ORDER VIEWER #######################
Our client needs a console application to be able to view customer order details. We will provide you with the following data
- Customers
- Items on sale
- Specific customer orders
Application needs to display customer order ID, customer full name and id along with the item(s) purchased including their description and price.

SQL -> Customer // Item // Customer Order
C# -> CONSOLE APPLICATION (Customer Order Id
                           Customer Full Name 
                           Customer Id
                           Item Id
                           Item Description
                           Item Price)

Will be created a view for a InnerJoin with the Customer and Item tables, in order to show everything in one table

CREATE VIEW [dbo].[CustomerOrderDetail] AS
SELECT
	t1.CustomerOrderId,
	t2.CustomerId,
	t3.ItemId,
	t2.FirstName,
	t2.LastName,
	t3.[Description], 
	t3.Price
	FROM
		dbo.CustomerOrder t1 -- It's a variable that holds your table in it
	INNER JOIN						 -- So you can reference your table just using this name
		dbo.Customer t2 ON t2.CustomerId = t1.CustomerId
	INNER JOIN
		dbo.Item t3 ON t3.ItemId = t1.ItemId;


We are gonna use this View to catch all the information and put in a List using C# console with .NET FrameWork, creating a class to be our model and another class for the commands
We'll create a list holding our model and use foreach statement to print each information on the console.
  
####################### CUSTOMER ORDER VIEWER 2.0 #######################
Client wants to be able to view Customers, Items and Customer Order Details from before in a console window
and wants to be able to know who created or updated new and existing customer orders. The client also asked for
a method to remove customer orders but still keep the data in the database as historical data

It's going to be used the Alter Table command to include the new information the client asked. 
