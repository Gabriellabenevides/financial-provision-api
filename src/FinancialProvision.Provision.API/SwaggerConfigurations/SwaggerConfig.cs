using System.Reflection;

namespace RH.API.Extensions.SwaggerConfigurations;

internal static class SwaggerConfig
{
    internal static IServiceCollection AddSwaggerConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            // Obter a referência do assembly principal da aplicação
            var executingAssembly = Assembly.GetExecutingAssembly();

            // Definir o caminho do arquivo XML com os comentários da aplicação
            var xmlFile = $"{executingAssembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            // Incluir os comentários XML no Swagger
            options.IncludeXmlComments(xmlPath);

            // Incluir comentários XML dos projetos referenciados que começam com "Beneficios.Api"
            var referencedProjectsXmlDocPaths = executingAssembly.GetReferencedAssemblies()
                .Where(assembly => assembly.Name != null && assembly.Name.StartsWith("RH.Api", StringComparison.InvariantCultureIgnoreCase))
                .Select(assembly => Path.Combine(AppContext.BaseDirectory, $"{assembly.Name}.xml"));

            foreach (var xmlDocPath in referencedProjectsXmlDocPaths)
            {
                if (File.Exists(xmlDocPath))
                    options.IncludeXmlComments(xmlDocPath);
            }
        });

        return services;
    }
}