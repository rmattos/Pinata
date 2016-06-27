Piñata - In Progress
======

Piñata is a simple and lightweight .NET library for managing database state during unit testing

###How to install via NugGet

To install Piñata, run the following command in the Package Manager Console

PM> Install-Package Pinata

###Database server supports:

* MySQL
* MongoDB 
* Microsoft SQL Server - *coming soon*
* SQLLite - *coming soon*
* PostgreSQL  - *coming soon*
* Oracle - *coming soon*

###Basic Usage

```csharp
Pinata pinata = new Pinata(ConfigurationManager.ConnectionStrings["conn"].ToString(), Provider.Type.MySQL, "Sample/sqlData.json");

pinata.Feed();

pinata.Execute(CommandType.Insert);
```
