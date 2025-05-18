using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
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

            // Find the constructor of LibraryDataService
            var svcType = typeof(ILibraryDataService).Assembly
                .GetType("LibraryLogicLayer.LibraryDataService", throwOnError: true);

            var ctor = svcType.GetConstructors().FirstOrDefault();
            if (ctor == null || ctor.GetParameters().Length != 1)
                throw new InvalidOperationException("Expected one constructor with one parameter");

            // Get the parameter type (interface, but unknown to us)
            var paramType = ctor.GetParameters()[0].ParameterType;

            // Create a proxy object that has the same methods
            var proxy = DispatchProxyGenerator.CreateProxy(paramType, stub);

            // Create the service
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

        public class StubRepository
        {
            public bool CatalogExistsReturn { get; set; }
            public List<(int, string, string, int)> AddedCatalogs = new();

            public bool DoesCatalogExist(int id) => CatalogExistsReturn;
            public void AddCatalog(int catalogId, string title, string author, int nrOfPages)
                => AddedCatalogs.Add((catalogId, title, author, nrOfPages));
        }

        // DispatchProxy that dynamically forwards calls to StubRepository
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
    }
}
