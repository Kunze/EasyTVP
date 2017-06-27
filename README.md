# EasyTVP
Easy Table Valued Parameter

**1 - Create tables and type:**
```sql
CREATE TABLE One (
	Id INT PRIMARY KEY IDENTITY (1, 1),
	Name VARCHAR(100)
)

CREATE TABLE Many (
	Id INT PRIMARY KEY IDENTITY (1, 1),
	Name varchar(100),
	Quantity int,
	OneId int,
	Data date,
	Status int,
	LongStatus bigint
)

CREATE TYPE ManyType AS TABLE(
	Name varchar(100) NULL,
	Quantity int NULL,
	Data date NULL,
	Status int NULL,
	LongStatus bigint
)
```

**2 - Create procedure:**
```sql
CREATE PROCEDURE AddOneAndMany
	@Name VARCHAR(100)
	,@Many ManyType READONLY
AS
BEGIN
	DECLARE @Temp TABLE (Id INT)

	INSERT INTO One (Name) OUTPUT inserted.Id INTO @Temp VALUES (@Name)

	INSERT INTO Many (OneId, Name, Quantity, data, status, longStatus)
	SELECT (SELECT Id FROM @Temp), Name, Quantity, Data, status, longStatus FROM @Many
END
```

**3 - Run:**
```c#
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace Example
{
	internal enum Status
	{
		Closed = 0,
		Open = 1
	}

	internal enum LongStatus : long
	{
		Closed = long.MaxValue - 1,
		Open = long.MaxValue
	}

	class Many
	{
		public string Name { get; set; } = "A name";
		public int Quantity { get; set; } = 10;
		public DateTime Data { get; set; } = DateTime.Now;
		public Status Status { get; set; } = Status.Open;
		public LongStatus LongStatus { get; set; } = LongStatus.Open;
	}

	class Program
	{
		static void Main(string[] args)
		{
			List<Many> list = CreateList();

			using (var connection = new SqlConnection("Server=localhost;Database=Testes;Trusted_Connection=True;"))
			{
				connection.Execute("AddOneAndMany", new
				{
				    Name = "One's name",
				    Many = TVP.Map(list)
				}, commandType: CommandType.StoredProcedure);
			}
		}

		private static List<Many> CreateList()
		{
			var list = new List<Many>();

		    	for (int i = 0; i < 3; i++)
		    	{
				list.Add(new Many());
		    	}

		    	return list;
		}
	}
}
```

# Customizating SQL types
## Changing default SqlDbType

```c#
using EasyTVP;

public class Foo
{
	[SqlDataRecordType(System.Data.SqlDbType.VarChar)] //default
	public string Name1 { get; set; } = "A name";

	[SqlDataRecordType(System.Data.SqlDbType.NText)]
	public string Name2 { get; set; }

	[SqlDataRecordType(System.Data.SqlDbType.NVarChar)]
	public string Name3 { get; set; }

	[SqlDataRecordType(System.Data.SqlDbType.Text)]
	public string Name4 { get; set; }

	[SqlDataRecordType(System.Data.SqlDbType.Decimal)] //default
	public decimal Decimal1 { get; set; }

	[SqlDataRecordType(System.Data.SqlDbType.Money)]
	public decimal Decimal2 { get; set; }

	[SqlDataRecordType(System.Data.SqlDbType.SmallMoney)]
	public decimal Decimal3 { get; set; }
}
```

## Changing order
```c#
class NotOrderedModel
{
	[SqlDataRecordOrder(2)]
	public int Second { get; set; }

	[SqlDataRecordOrder(1)]
	public string First { get; set; }

	[SqlDataRecordOrder(3)]
	public long Third { get; set; }
}
```

## Changing MaxLength
```c#
public class Foo
{
	[SqlDataRecordMaxLength(2000)]
	public string Name1 { get; set; } = "A name";
}
```

## Changing default(1000) varchar max length

```c#
EasyTVP.Types.StringSqlType.DefaultMaxLength = 1234;
```
