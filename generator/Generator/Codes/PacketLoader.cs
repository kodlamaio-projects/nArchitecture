using Core.Persistence.Repositories;
using System.Diagnostics;
using System.Reflection;

namespace Generator.Codes;

internal class PacketLoader
{
    private string[] DefaultCoreEntityLayerPacketNames = { "Core.Security", "Core.Persistence" };
    private string[] DefaultEntityLayerPacketNames = { "Domain" };
    private string[] DefaultDataLayerPacketNames = { "DataAccess", "Persistence" };

    public PacketLoader()
    {

    }
    public IList<Type> GetLoadedPacketDbContexts()
    {
        return GetDbContexts(DefaultDataLayerPacketNames);
    }

    public IList<Type> GetLoadedPacketEntities()
    {
        return GetEntities(DefaultEntityLayerPacketNames);
    }

    public IList<Type> GetLoadedPacketEntitiesForCore()
    {
        return GetEntities(DefaultCoreEntityLayerPacketNames);
    }

    private static IList<Assembly> AssembliesLoadVerify(string[] loadPacketNames)
    {
        List<Assembly> verifiedAssembly = new();
        foreach (string packName in loadPacketNames)
        {
            try
            {

                Assembly assembly = Assembly.Load(packName);

                verifiedAssembly.Add(assembly);
            }

            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        return verifiedAssembly;
    }

    public static IList<Type> GetEntities(string[] loadPacketNames)
    {
        IList<Assembly> assemblies = AssembliesLoadVerify(loadPacketNames);

        List<Type> entities = new();

        foreach (Assembly assembly in assemblies)
        {
            Type[] types = assembly.GetTypes()
                .Where(x => typeof(Entity).IsAssignableFrom(x) && x.IsClass && x != typeof(Entity)).ToArray();

            entities.AddRange(types);
        }
        return entities;
    }

    private static IList<Type> GetDbContexts(string[] loadPacketNames)
    {
        IList<Assembly> assemblies = AssembliesLoadVerify(loadPacketNames);

        List<Type> entities = new();

        foreach (Assembly assembly in assemblies)
        {
            Type[] types = assembly.GetTypes()
                .Where(x => typeof(Microsoft.EntityFrameworkCore.DbContext).IsAssignableFrom(x) && x.IsClass).ToArray();

            entities.AddRange(types);
        }
        return entities;
    }
}
