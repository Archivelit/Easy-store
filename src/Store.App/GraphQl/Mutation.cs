namespace Store.App.GraphQl;

[ObjectType("Mutation")]
public class Mutation
{
    public string Ping => "pong";
}