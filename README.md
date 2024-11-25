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

In order to make the connection with the DataBase easier, we'll use the Dapper library this time. So we can jump the connection and reading steps, and just send our Query straight.

We'll create the Customer, Customer Order Detail and Item Models to create a possibility of a ShowAll() function that will bring all the tables for the console.
For the tables of customers and items, we'll need only the GetList() command in order to access the data, but for the CustomerOrderDetail table, we'll create the commands using the
Stored Procedures(SPROCs) created on the SQL Server, in order to delete, update or create rows. 

The common structure for the files will be used. Being a Models folder for the models classes and a Repository folder for the command classes.


  
####################### COURSE REPORT EMAILER #######################
The client will be providing the following information:
	Courses
 	Students
  	Enrollments
The client would like an application which puts together all of the information in an Excel WorkBook in one WorkSheet and send an email to
support@someCompany.com
Subject of the email should be Enrollment Details Report
Name of the generated file should be EnrollmentDetailsReport.xlsx
Name of the worksheet should be Report Sheet
The client would like the following fields to be shown
	EnrollmentId
 	FirstName of student
  	LastName of student
   	CourseCode of the course being taken
    	Description of the course being taken


####################### COURSE MANAGER #######################
The client would like a graphical user interface to be able to manage existing enrollments and create new ones
The form needs to: 
	- Display all existing enrollments
 	- Display the student and course attached to the enrollment when one 	is selected
  	- Display error messages and other statuses as they happen
   	- Let user edit existing enrollment's student and/or course
    	- Let user add new enrollment from existing students and courses
     	- Once enrollment is edited or added, we need to know which user 	performed the action and when



####################### DIGIMON CRUD #######################
In this project I will review the basics by creating a users's table, a digimon's table (digimon is like a pet, which each use can have only one), a view linking the two tables with join command, stored procedures for calling the view and each table, so I can use it with C# to create a console view. I will also create stored procedures for upsert and soft delete (instead of deleting, I will just turn active = 0), and a User Defined Type for use in the insert code in C#. This is a simple API just for practicing CRUD development.
I will be using Dapper packet for access the SPROCs and UDDTs and Microsoft.Data.SqlClient for connecting to the database.
