using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestLogicLayer
{
    public interface ILibraryDataService
    {
        void LogicAddCatalogue(int id, string title, string author, int pages);
        void LogicAddUser(int id, string firstName, string lastName);
        void LogicRemoveUser(int id);
        void LogicRemoveCatalogue(int id);
        void LogicAddUserEvent(int id, int userId, int catalogId, int stateId, bool returned);
    }

    public class LibraryDataService : ILibraryDataService
    {
        private readonly IRepository repo;

        public LibraryDataService(IRepository repository)
        {
            this.repo = repository;
        }

        public void LogicAddCatalogue(int id, string title, string author, int pages)
        {
            if (!repo.DoesCatalogExist(id))
                repo.AddCatalog(id, title, author, pages);
        }

        public void LogicAddUser(int id, string firstName, string lastName)
        {
            if (!repo.DoesUserExist(id))
                repo.AddUser(id, firstName, lastName);
        }

        public void LogicRemoveUser(int id)
        {
            if (repo.DoesUserExist(id))
                repo.RemoveUser(id);
        }

        public void LogicRemoveCatalogue(int id)
        {
            if (repo.DoesCatalogExist(id))
                repo.RemoveCatalog(id);
        }

        public void LogicAddUserEvent(int id, int userId, int catalogId, int stateId, bool returned)
        {
            if (!repo.DoesEventExist(id))
                repo.AddEvent(id, userId, catalogId, stateId, returned);
        }
    }

    public interface IRepository
    {
        bool DoesCatalogExist(int id);
        void AddCatalog(int id, string title, string author, int pages);
        void RemoveCatalog(int id);

        bool DoesUserExist(int id);
        void AddUser(int id, string firstName, string lastName);
        void RemoveUser(int id);

        bool DoesEventExist(int id);
        void AddEvent(int id, int userId, int catalogId, int stateId, bool returned);
    }

    public class StubRepository : IRepository
    {
        public bool CatalogExistsReturn { get; set; }
        public bool UserExistsReturn { get; set; }
        public bool EventExistsReturn { get; set; }

        public List<(int, string, string, int)> AddedCatalogs = new();
        public List<int> RemovedCatalogs = new();

        public List<(int, string, string)> AddedUsers = new();
        public List<int> RemovedUsers = new();

        public List<(int, int, int, int, bool)> AddedEvents = new();

        public bool DoesCatalogExist(int id) => CatalogExistsReturn;
        public void AddCatalog(int id, string title, string author, int pages) =>
            AddedCatalogs.Add((id, title, author, pages));
        public void RemoveCatalog(int id) => RemovedCatalogs.Add(id);

        public bool DoesUserExist(int id) => UserExistsReturn;
        public void AddUser(int id, string firstName, string lastName) =>
            AddedUsers.Add((id, firstName, lastName));
        public void RemoveUser(int id) => RemovedUsers.Add(id);

        public bool DoesEventExist(int id) => EventExistsReturn;
        public void AddEvent(int id, int userId, int catalogId, int stateId, bool returned) =>
            AddedEvents.Add((id, userId, catalogId, stateId, returned));
    }

    public class DispatchProxyGenerator : DispatchProxy
    {
        private object _target;

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            var method = _target.GetType().GetMethod(
                targetMethod.Name,
                BindingFlags.Public | BindingFlags.Instance,
                null,
                targetMethod.GetParameters().Select(p => p.ParameterType).ToArray(),
                null);

            if (method == null)
                throw new MissingMethodException($"Method {targetMethod.Name} not found.");

            return method.Invoke(_target, args);
        }

        public static object CreateProxy(Type interfaceType, object target)
        {
            var method = typeof(DispatchProxy).GetMethods()
                .First(m => m.Name == "Create" && m.IsGenericMethodDefinition);

            var generic = method.MakeGenericMethod(interfaceType, typeof(DispatchProxyGenerator));
            var proxy = (DispatchProxyGenerator)generic.Invoke(null, null);
            proxy._target = target;
            return proxy;
        }
    }

    [TestClass]
    public class IntegrationLogicReferenceTests
    {
        private StubRepository stub;
        private ILibraryDataService service;

        [TestInitialize]
        public void Init()
        {
            stub = new StubRepository();

            var svcType = typeof(ILibraryDataService).Assembly
                .GetType("TestLogicLayer.LibraryDataService", throwOnError: true);

            var ctor = svcType.GetConstructors().FirstOrDefault();
            if (ctor == null || ctor.GetParameters().Length != 1)
                throw new InvalidOperationException("Expected one constructor with one parameter");

            var paramType = ctor.GetParameters()[0].ParameterType;
            var proxy = DispatchProxyGenerator.CreateProxy(paramType, stub);
            service = (ILibraryDataService)Activator.CreateInstance(svcType, proxy);
        }

        [TestMethod]
        public void LogicAddCatalogue_Works_WhenNotExists()
        {
            stub.CatalogExistsReturn = false;
            service.LogicAddCatalogue(1, "Book", "Author", 123);
            Assert.AreEqual(1, stub.AddedCatalogs.Count);
        }

        [TestMethod]
        public void LogicAddUser_Works_WhenNotExists()
        {
            stub.UserExistsReturn = false;
            service.LogicAddUser(2, "John", "Doe");
            Assert.AreEqual(1, stub.AddedUsers.Count);
        }

        [TestMethod]
        public void LogicRemoveUser_Works_WhenExists()
        {
            stub.UserExistsReturn = true;
            service.LogicRemoveUser(2);
            Assert.AreEqual(1, stub.RemovedUsers.Count);
        }

        [TestMethod]
        public void LogicRemoveCatalogue_Works_WhenExists()
        {
            stub.CatalogExistsReturn = true;
            service.LogicRemoveCatalogue(1);
            Assert.AreEqual(1, stub.RemovedCatalogs.Count);
        }

        [TestMethod]
        public void LogicAddUserEvent_Works_WhenNotExists()
        {
            stub.EventExistsReturn = false;
            service.LogicAddUserEvent(1, 2, 3, 4, true);
            Assert.AreEqual(1, stub.AddedEvents.Count);
        }
    }
}
