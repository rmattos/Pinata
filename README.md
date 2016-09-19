![alt text](https://img.shields.io/appveyor/ci/gruntjs/grunt/master.svg "build status") ![alt text](https://img.shields.io/badge/nuget-v0.13.0-blue.svg "version")

Piñata
======

Piñata is a simple and lightweight .NET library for managing database state during integration testing.


## Compatibility

.Net Framework 4.5 or higher

### Database server supports

* MySQL (5.0 or higher)
* MongoDB (2.6 or higher)
* Microsoft SQL Server - *coming soon*
* SQLLite - *coming soon*
* PostgreSQL  - *coming soon*
* Oracle - *coming soon*



## Getting Started

### How to install via NugGet

To install Piñata, run the following command in the Package Manager Console

PM> Install-Package Pinata

### How it Works

Given a JSON file having tables schema and data you want to handle, Piñata organize a dataset and try to execute insert and delete command into a database.
Actually it works with MySQL and MongoDB.


### Data Schema

JSON file should provide an array describing table's schema including primary and foreign keys, and the rows you want to handle. The following sample shows a JSON data for MySQL database:

```json
[
  {
    "Table": "TestPinata_Actor",
    "Keys": [ "Id" ],
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
        },
        {
            "Id": "1cb9da05-38e5-11e6-8aa3-0003ff500b9e",
            "Name": "dynamic_param"
        }

    ],
    "FK_References": [ ]
  }
]

```


Field           | Type      | Description                       | Observation
---             | ---       | ---                               | ---
Table           | string    | name of table inside database     |
Keys            | array     | primary keys of table             | used as reference to execute delete operations
Schema          | array     | type of data value to each column | types: int, long, short, byte, bool, string, char, guid, double, decimal, float, datetime, array, document, objectid
Rows            | array     | data to insert on database        |
FK_References   | array     | foreign key tables                |

Piñata allows you to work with MongoDB. The structure is similar to MySQL except that you don't need foreign keys, as the following sample:

```json
[
    {
        "Collection": "TestPinata_Movie",
        "Keys": [ "_id" ],
        "Schema": [
            {
                "Column": "_id",
                "Type": "objectId"
            },
            {
                "Column": "Name",
                "Type": "string"
            }
        ],
        "Rows": [
            {
                "_id": "577aedd825e7695ec2a81145",
                "Name": "Capitan America - Civil War"
            },
            {
                "_id": "577aedfc25e7695ec2a81147",
                "Name": "Avengers"
            }            
        ]
    }
]

```


Field       | Type      | Description                           | Observation
---         | ---       | ---                                   | ---
collection  | string    | name of collection inside database    |
Keys        | array     | unique identifier for collection      | used as reference to execute delete operations
Schema      | array     | type of data value to each column     | types: int, long, short, byte, bool, string, char, guid, double, decimal, float, datetime, array, document, objectid
Rows        | array     | data to insert on database            |


### Hands On

After create your data schema and install Piñata into your project, you can start an instance. 
Work with MySQL requires a ProviderType, otherwise you just need provide your database address.
The next step is having Piñata read the data schema file by the Feed method.

 ```csharp

Pinata pinata = new Pinata(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString(), Provider.Type.MySQL);

pinata.Feed("sample/data.json");

```

Is recommended to execute a delete command before the insert to ensure data coherence.

```csharp

pinata.Execute(CommandType.Delete);

pinata.Execute(CommandType.Insert);

```

Another way to load data is provide a JSON struct passing variables instead values.
You'll keep the same data schema but the Execute command must have a dictionary with the
corresponding parameters.


```csharp

private IDictionary<string, string> parameters = new Dictionary<string, string>();

parameters.Add("dynamic_param", "Lights Out");

pinata.Execute(CommandType.Insert, parameters);

```


## Documentation

### Pinata Class

#### new Pinata(options)

```csharp

Pinata pinata = new Pinata(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString(), Provider.Type.MySQL);

```

or

```csharp

Pinata pinata = new Pinata(ConfigurationManager.ConnectionStrings["myConnectionString"].ToString(), Provider.Type.MySQL, "data1.json", "data2.json");

```

Parameter           | Type      | optional  | Description
---                 | ---       | ---       | ---
ConnectionString    | string    | false
Provider            | enum      | false     | values: Provider.Type.MySQL , Provider.Type.MongoDB
SamplePath          | array     | true      | array of json files, this is optional you can load json files direct from feed method as well.


#### Feed(options)

```csharp

pinata.Feed();

```

or

```csharp

pinata.Feed("data1.json");

```

Parameter           | Type      | optional  | Description
---                 | ---       | ---       | ---
SamplePath          | array     | true      | array of json files, this is optional you can load json files direct from Piñata constructor as well.


#### Execute(options)

```csharp

pinata.Execute(CommandType.Insert);

```

or

```csharp

pinata.Execute(CommandType.Insert, Parameters);

```

Parameter           | Type                          | optional  | Description
---                 | ---                           | ---       | ---
CommandType         | enum                          | false     | type of command to execute. values: CommandType.Insert, CommandType.Delete.
Parameters          | dictionary<string></string>   | true      | dictionary having variable name and value


### Supported data types at schema

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


## Examples

### JSON example of tables with relationship to SQL database

```json
[
    {
        "Table": "TestPinata_Movie",
        "Keys": [ "Id" ],
        "Schema": [
            {
                "Column": "Id",
                "Type": "guid"
            },
            {
                "Column": "Name",
                "Type": "string"
            },
            {
                "Column": "CategoryId",
                "Type": "int"
            }
        ],
        "Rows": [
            {
                "Id": "adef64eb-38e4-11e6-8aa3-0003ff500b9d",
                "Name": "Capitan America - Civil War",
                "CategoryId": "1"
            },
            {
                "Id": "b6210be7-38e4-11e6-8aa3-0003ff500b9d",
                "Name": "Avangers",
                "CategoryId": "4"
            },
            {
                "Id": "ddbb311a-38e4-11e6-8aa3-0003ff500b9d",
                "Name": "Deadpool",
                "CategoryId": "1"
            }
        ],
        "FK_References": [
            { "Table": "TestPinata_Category" }
        ]
    },
    {
        "Table": "TestPinata_MovieActor",
        "Keys": [ "MovieId", "ActorId" ],
        "Schema": [
            {
                "Column": "MovieId",
                "Type": "guid"
            },
            {
                "Column": "ActorId",
                "Type": "guid"
            }
        ],
        "Rows": [
            {
                "MovieId": "adef64eb-38e4-11e6-8aa3-0003ff500b9d",
                "ActorId": "b3e6447b-38e3-11e6-8aa3-0003ff500b9d"
            },
            {
                "MovieId": "adef64eb-38e4-11e6-8aa3-0003ff500b9d",
                "ActorId": "dee91372-38e3-11e6-8aa3-0003ff500b9d"
            },
            {
                "MovieId": "adef64eb-38e4-11e6-8aa3-0003ff500b9d",
                "ActorId": "e4b94626-38e3-11e6-8aa3-0003ff500b9d"
            },
            {
                "MovieId": "ddbb311a-38e4-11e6-8aa3-0003ff500b9d",
                "ActorId": "1cb9da05-38e5-11e6-8aa3-0003ff500b9d"
            }
        ],
        "FK_References": [
            { "Table": "TestPinata_Movie" },
            { "Table": "TestPinata_Actor" }
        ]
    },
    {
        "Table": "TestPinata_Actor",
        "Keys": [ "Id" ],
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
    },
    {
        "Table": "TestPinata_Category",
        "Keys": [ "Id" ],
        "Schema": [
            {
                "Column": "Id",
                "Type": "int"
            },
            {
                "Column": "Name",
                "Type": "string"
            }
        ],
        "Rows": [
            {
                "Id": "1",
                "Name": "Action"
            },
            {
                "Id": "2",
                "Name": "Sci-Fi"
            },
            {
                "Id": "3",
                "Name": "Thriller"
            },
            {
                "Id": "4",
                "Name": "Adventure"
            }
        ],
        "FK_References": [ ]
    }
]
 
```
### JSON example of collection with complex documents to MongoDB

```json
[
    {
        "Collection": "TestPinata_User",
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
            },
            {
                "Column": "Birthday",
                "Type": "datetime"
            },
            {
                "Column": "Interest",
                "Type": "array"
            },
            {
                "Column": "Address",
                "Type": "document"
            }
        ],
        "Rows": [
            {
                "_id": "577aedd825e7695ec2a81145",
                "Email": "teste@soclminer.com.br",
                "Age": "28",
                "Birthday": "1990-01-25T10:30:00.000Z",
                "Interest": [ "sports" ],
                "Address": {
                    "Street": "R. Maria Carolina, 1001",
                    "District": "Jardim Paulistano",
                    "City": "São Paulo",
                    "State": "SP"
                }
            },
            {
                "_id": "577aedfc25e7695ec2a81147",
                "Email": "teste2@soclminer.com.br",
                "Age": "31",
                "Birthday": "1985-03-15T00:00:00.000Z",
                "Interest": [ "series", "anime", "sports" ],
                "Address": {
                    "Street": "R. Maria Carolina, 1001",
                    "District": "Jardim Paulistano",
                    "City": "São Paulo",
                    "State": "SP"
                }
            }
        ]
    }
]
    
```


## OSS Libraries used

* [Jil](https://github.com/kevin-montrose/Jil)
* [Dapper] (https://github.com/StackExchange/dapper-dot-net)
* [Mongo C# Driver] (https://github.com/mongodb/mongo-csharp-driver)
* [MySQL .NET Driver] (https://github.com/mysql/mysql-connector-net)


## License

This software is licensed in [MIT License (MIT)](https://github.com/rmattos/Pinata/blob/master/LICENSE)
