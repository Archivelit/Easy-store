namespace Store.App.GraphQl;

[ObjectType("Query")]
public class Query
{
    public string Ping => "pong";
}