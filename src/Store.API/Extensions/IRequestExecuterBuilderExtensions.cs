namespace Store.API.Extensions;

public static class IRequestExecuterBuilderExtensions
{
    public static IRequestExecutorBuilder AddGraphQlExtendTypes(this IRequestExecutorBuilder builder)
    {
        Log.Debug("Adding GraphQl extend types");
        var assembly = typeof(Query).Assembly;
        var extenderInterface = typeof(IGraphQlExtender);

        var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && extenderInterface.IsAssignableFrom(t));


        foreach (var type in types)
        {
            builder.AddTypeExtension(type);
            Log.Debug("Adding extend type {TypeName}", type.Name);
        }

        Log.Debug("GraphQl extend types added succesfuly");
        return builder;
    }
}