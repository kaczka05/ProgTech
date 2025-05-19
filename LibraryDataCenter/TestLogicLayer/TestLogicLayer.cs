using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using LibraryLogicLayer;

namespace TestLogicLayer
{
    [TestClass]
    public class LibraryDataServiceTests
    {
        private StubRepository stub;
        private ILibraryDataService service;

        [TestInitialize]
        public void Init()
        {
            stub = new StubRepository();

            var svcType = typeof(ILibraryDataService).Assembly
                .GetType("LibraryLogicLayer.LibraryDataService", throwOnError: true);

            var ctor = svcType.GetConstructors().FirstOrDefault();
            if (ctor == null || ctor.GetParameters().Length != 1)
                throw new InvalidOperationException("Expected one constructor with one parameter");

            var paramType = ctor.GetParameters()[0].ParameterType;
            var proxy = DispatchProxyGenerator.CreateProxy(paramType, stub);

            service = (ILibraryDataService)Activator.CreateInstance(svcType, proxy);
        }

        [TestMethod]
        public void LogicAddCatalogue_CallsAdd_WhenNotExists()
        {
            stub.CatalogExistsReturn = false;
            service.LogicAddCatalogue(1, "T", "A", 10);
            Assert.AreEqual(1, stub.AddedCatalogs.Count);
            Assert.AreEqual((1, "T", "A", 10), stub.AddedCatalogs[0]);
        }

        [TestMethod]
        public void LogicAddCatalogue_WithStaticAndRandomData_WorksCorrectly()
        {
            stub.CatalogExistsReturn = false;

            // Static data test
            var staticData = StaticTestData.GetTestData();
            service.LogicAddCatalogue(staticData.Id, staticData.Title, staticData.Author, staticData.Pages);
            Assert.AreEqual(staticData, stub.AddedCatalogs.Last());

            // Random data test
            var randomData = RandomTestData.GetTestData();
            service.LogicAddCatalogue(randomData.Id, randomData.Title, randomData.Author, randomData.Pages);
            Assert.AreEqual(randomData, stub.AddedCatalogs.Last());
        }

        public class StubRepository
        {
            public bool CatalogExistsReturn { get; set; }
            public List<(int Id, string Title, string Author, int Pages)> AddedCatalogs = new();

            public bool DoesCatalogExist(int id) => CatalogExistsReturn;
            public void AddCatalog(int catalogId, string title, string author, int nrOfPages)
                => AddedCatalogs.Add((catalogId, title, author, nrOfPages));
        }

        public class DispatchProxyGenerator : DispatchProxy
        {
            private object _target;

            protected override object Invoke(MethodInfo targetMethod, object[] args)
            {
                var method = _target.GetType().GetMethod(targetMethod.Name,
                    BindingFlags.Public | BindingFlags.Instance,
                    null,
                    targetMethod.GetParameters().Select(p => p.ParameterType).ToArray(),
                    null);

                if (method == null)
                    throw new MissingMethodException($"Method {targetMethod.Name} not found in stub.");

                return method.Invoke(_target, args);
            }

            public static object CreateProxy(Type interfaceType, object target)
            {
                var createMethod = typeof(DispatchProxy)
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .Where(m => m.Name == "Create" && m.IsGenericMethodDefinition && m.GetGenericArguments().Length == 2)
                    .First();

                var proxy = (DispatchProxyGenerator)createMethod
                    .MakeGenericMethod(interfaceType, typeof(DispatchProxyGenerator))
                    .Invoke(null, null);

                proxy._target = target;
                return proxy;
            }
        }

        // Simple static test data generator
        public static class StaticTestData
        {
            public static (int Id, string Title, string Author, int Pages) GetTestData()
            {
                return (101, "StaticTitle", "StaticAuthor", 123);
            }
        }

        // Simple random test data generator
        public static class RandomTestData
        {
            private static readonly Random rnd = new();

            public static (int Id, string Title, string Author, int Pages) GetTestData()
            {
                int id = rnd.Next(1000, 9999);
                string title = "Title" + rnd.Next(1, 100);
                string author = "Author" + rnd.Next(1, 100);
                int pages = rnd.Next(50, 500);
                return (id, title, author, pages);
            }
        }
    }
}
