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

####Create a JSON file with your data

```json
[
  {
    "Table": "TestPinata_Actor",
    "Keys": [ "Id" ],
    "Relationship": "None",
    "Schema": [
        {
            "Column": "Id",
            "Type": "guid"
        },
        {
            "Column": "Name",
            "Type": "string"
        }
    ],
    "Rows": [
        {
            "Id": "b3e6447b-38e3-11e6-8aa3-0003ff500b9d",
            "Name": "Chris Evans"
        },
        {
            "Id": "dee91372-38e3-11e6-8aa3-0003ff500b9d",
            "Name": "Robert Downey Jr."
        },
        {
            "Id": "e4b94626-38e3-11e6-8aa3-0003ff500b9d",
            "Name": "Scarlett Johansson"
        },
        {
            "Id": "1cb9da05-38e5-11e6-8aa3-0003ff500b9d",
            "Name": "Ryan Reynolds"
        }
    ],
    "FK_References": [ ]
  }
]

```
####Create a new instance of Pinata and execute an insert command

```csharp
Pinata pinata = new Pinata(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString(), Provider.Type.MySQL, "data.json");

pinata.Feed();

pinata.Execute(CommandType.Insert);
```
