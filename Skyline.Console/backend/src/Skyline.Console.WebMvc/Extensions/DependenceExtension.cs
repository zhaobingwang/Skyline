﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Skyline.Console.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace Skyline.Console.WebMvc
{
    public static class DependenceExtension
    {
        public static void AddSkylineService(this IServiceCollection services)
        {
            var defaultAssemblyNames = DependencyContext.Default.GetDefaultAssemblyNames().Where(x => x.FullName.Contains("Skyline.")).ToList();
            var assemblies = defaultAssemblyNames.SelectMany(a => a.GetTypeOfISerice()).ToList();

            assemblies.ForEach(assembliy =>
            {
                services.AddScoped(assembliy);
            });
        }

        private static List<Type> GetTypeOfISerice(this AssemblyName assemblyName)
        {
            return AssemblyLoadContext.Default.LoadFromAssemblyName(assemblyName).ExportedTypes.Where(b => b.GetInterfaces().Contains(typeof(ISkylineAutoDependence))).ToList();
        }
    }
}
