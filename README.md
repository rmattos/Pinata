![alt text](https://img.shields.io/appveyor/ci/gruntjs/grunt/master.svg "build status") ![alt text](https://img.shields.io/badge/nuget-v0.10.0--beta-blue.svg "version")


Piñata
======

Piñata is a simple and lightweight .NET library for managing database state during unit testing

###How to install via NugGet

To install Piñata, run the following command in the Package Manager Console

PM> Install-Package Pinata -Pre

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
####Loading data into database

```csharp
Pinata pinata = new Pinata(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString(), Provider.Type.MySQL, "data.json");

pinata.Feed();

pinata.Execute(CommandType.Insert);
```

How to use
======

###JSON structure to SQL database

```json
[
  {
    "Table": "MyTable",
    "Keys": [ "key1", "key2" ],
    "Relationship": "None",
    "Schema": [ 
		{
            "Column": "id",
            "Type": "int"
        },
        {
            "Column": "name",
            "Type": "string"
        }
	],
    "Rows": [
		{
			"id": "1",
			"name": "Chris"
		},
		{
			"id": "2",
			"name": "Robert"
		}
	],
    "FK_References": [
		{ "Table": "table_reference_1" }
	]
  }
]

```

Field | Type | Description | Observation
--- | --- | --- | ---
Table | string | name of table inside database |
Keys | array | primary keys of table | used as reference to execute delete operations
Relationship | string | type of relationship with other tables | possible values : "None", "OneToOne", "OneToMany", "ManyToMany"
Schema | array | type of data value to each column | types: int, long, short, byte, bool, string, char, guid, double, decimal, float, datetime, array, document, objectid
Rows | array | data to insert on database |
FK_References | array | foreign key tables |

###JSON structure to NoSQL database

```json
[
    {
        "Collection": "MyCollection",
        "Keys": [ "_id" ],
        "Schema": [
            {
                "Column": "_id",
                "Type": "objectId"
            },
            {
                "Column": "Email",
                "Type": "string"
            },
            {
                "Column": "Age",
                "Type": "int"
            }
        ],
        "Rows": [
            {
                "_id": "577aedd825e7695ec2a81145",
                "Email": "test1@socialminer.com",
                "Age": "28"
            },
            {
                "_id": "577aedfc25e7695ec2a81147",
                "Email": "test2@socialminer.com",
                "Age": "31",
            }
        ]
    }
]

```
Field | Type | Description | Observation
--- | --- | --- | ---
Collection | string | name of collection inside database |
Keys | array | unique identifier for collection | used as reference to execute delete operations
Schema | array | type of data value to each column | types: int, long, short, byte, bool, string, char, guid, double, decimal, float, datetime, array, document, objectid
Rows | array | data to insert on database |

###Create a new Pinata object

```csharp
Pinata pinata = new Pinata(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString(), Provider.Type.MySQL, "data.json");
```
####Parameters

Parameter | Type | Description
--- | --- | ---
ConnectionString | string
Provider | enum | values: Provider.Type.MySQL , Provider.Type.MongoDB
SamplePath | array | array of json files 

###Load data into Pinata

```csharp
pinata.Feed();
```
or

```csharp
pinata.Feed("data1.json", "data2.json");
```

####Parameters

Parameter | Type | Description
--- | --- | ---
SamplePath | array | array of json files, you can load json files direct from Feed method

###Execute a command into database

```csharp
pinata.Execute(CommandType.Insert);
```
####Parameters

Parameter | Type | Description
--- | --- | ---
CommandType | enum | type of command to execute. values: CommandType.Insert, CommandType.Delete

###Supported data types

* Int 
* Long 
* Short
* Byte
* Bool
* String
* Char
* Guid
* Double
* Decimal
* Float
* DateTime
* Array
* Document
* ObjectId
