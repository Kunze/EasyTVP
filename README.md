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
	Name VARCHAR(100),
	Quantity INT,
	OneId int,
	FOREIGN KEY (OneId) REFERENCES One(Id)
)

CREATE TYPE ManyType AS TABLE
( 
	Name VARCHAR(100),
	Quantity INT
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

	INSERT INTO One (Name) OUTPUT INSERTED.Id INTO @Temp VALUES (@Name)

	INSERT INTO Many (Name, Quantity, OneId)
	SELECT Name, Quantity, (SELECT Id FROM @Temp) FROM @Many
END
```

**3 - Run:**
```c#
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace Example
{
    public class Many
    {
        public string Name { get; set; } = "A name";
        public int Quantity { get; set; } = 10;
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection("Server=localhost;Database=Testes;Trusted_Connection=True;"))
            {
                List<Many> list = CreateList();
                var tvps = EasyTVP.TVP.Map(list);

                var parameters = new DynamicParameters();
                parameters.Add("Name", "One's name");
                parameters.Add("Many", SqlMapper.AsTableValuedParameter(tvps, "ManyType"));

                connection.Execute("AddOneAndMany", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        private static List<Many> CreateList()
        {
            var list = new List<Many>();

            for (int i = 0; i < 10; i++)
            {
                list.Add(new Many());
            }

            return list;
        }
    }
}
```

## Customizating SQL types ##

```c#
using EasyTVP;

public class Foo
{
    [SqlMaxLength(123)]
    [SqlDataType(System.Data.SqlDbType.VarChar)] //default
    public string Name1 { get; set; } = "A name";

    [SqlDataType(System.Data.SqlDbType.NText)]
    public string Name2 { get; set; }

    [SqlDataType(System.Data.SqlDbType.NVarChar)]
    public string Name3 { get; set; }

    [SqlDataType(System.Data.SqlDbType.Text)]
    public string Name4 { get; set; }

    [SqlDataType(System.Data.SqlDbType.Decimal)] //default
    public decimal Decimal1 { get; set; }

    [SqlDataType(System.Data.SqlDbType.Money)]
    public decimal Decimal2 { get; set; }

    [SqlDataType(System.Data.SqlDbType.SmallMoney)]
    public decimal Decimal3 { get; set; }
}
```
