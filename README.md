Piñata - In Progress
======

Piñata is a lightweight .NET library for managing database state during unit testing

###How to install via NugGet

To install Piñata, run the following command in the Package Manager Console

PM> Install-Package Pinata

###Usage

####Initiate Pinata

```C#
Pinata pinata new Pinata(ConfigurationManager.ConnectionStrings["conn"].ToString(), Provider.Type.MySQL, "Sample/sqlData.json");
```
####Load Data

```C#
pinata.Feed();
```
####Execute Command

```C#
pinata.Execute(CommandType.Insert);
```
