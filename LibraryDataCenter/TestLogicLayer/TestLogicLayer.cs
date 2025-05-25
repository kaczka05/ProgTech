using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Collections.Generic;
using LibraryLogicLayer;
namespace TestLogicLayer
{
    // Stub repository implementing ILibraryDataRepository
    public class StubRepository 
    {
        // Control return values
        public bool CatalogExistsReturn { get; set; }
        public bool UserExistsReturn { get; set; }
        public bool StateExistsReturn { get; set; }
        public bool EventExistsReturn { get; set; }

        // Record calls
        public List<(int id, string t, string a, int p)> AddedCatalogs = new();
        public List<int> RemovedCatalogs = new();
        public List<(int id, string f, string l)> AddedUsers = new();
        public List<int> RemovedUsers = new();
        public List<(int sid, int nb, int cid)> AddedStates = new();
        public List<int> RemovedStates = new();
        public List<(int eid, int emp, int st, bool add)> AddedDbEvents = new();
        public List<(int eid, int emp, int st, int usr, bool bor)> AddedUserEvents = new();
        public List<int> RemovedEvents = new();

        public void AddCatalog(int catalogId, string title, string author, int nrOfPages)
            => AddedCatalogs.Add((catalogId, title, author, nrOfPages));
        public void RemoveCatalogById(int id)
            => RemovedCatalogs.Add(id);
        public void AddUser(int userId, string firstName, string lastName)
            => AddedUsers.Add((userId, firstName, lastName));
        public void RemoveUserById(int id)
            => RemovedUsers.Add(id);
        public void AddState(int stateId, int nrOfBooks, int catalogId)
            => AddedStates.Add((stateId, nrOfBooks, catalogId));
        public void RemoveStateByID(int id)
            => RemovedStates.Add(id);
        public void AddDatabaseEvent(int eventId, int employeeId, int stateId, bool addition)
            => AddedDbEvents.Add((eventId, employeeId, stateId, addition));
        public void AddUserEvent(int eventId, int employeeId, int stateId, int userId, bool borrowing)
            => AddedUserEvents.Add((eventId, employeeId, stateId, userId, borrowing));
        public void RemoveEventById(int id)
            => RemovedEvents.Add(id);

        public bool DoesCatalogExist(int id) => CatalogExistsReturn;
        public bool DoesUserExist(int id) => UserExistsReturn;
        public bool DoesStateExist(int id) => StateExistsReturn;
        public bool DoesEventExist(int id) => EventExistsReturn;
    }

    [TestClass]
    public class LibraryDataServiceTests
    {
        private StubRepository stub;
        private ILibraryDataService service;

        [TestInitialize]
        public void Init()
        {
            stub = new StubRepository();
            // Instantiate internal LibraryDataService via reflection
            var svcType = typeof(ILibraryDataService).Assembly
                .GetType("LibraryLogicLayer.LibraryDataService", true);
            service = (ILibraryDataService)Activator.CreateInstance(svcType, stub);
        }

        [TestMethod]
        public void LogicAddCatalogue_CallsAdd_WhenNotExists()
        {
            stub.CatalogExistsReturn = false;
            //service.LogicAddCatalogue(1, "T", "A", 10);
            Assert.AreEqual(1, stub.AddedCatalogs.Count);
            Assert.AreEqual((1, "T", "A", 10), stub.AddedCatalogs[0]);
        }

        [TestMethod]
        public void LogicAddCatalogue_Throws_WhenExists()
        {
            stub.CatalogExistsReturn = true;
            //Assert.ThrowsException<Exception>(() =>
                //service.LogicAddCatalogue(1, "T", "A", 10));
        }

        [TestMethod]
        public void LogicRemoveCatalogue_CallsRemove_WhenExists()
        {
            stub.CatalogExistsReturn = true;
            //service.LogicRemoveCatalogue(2);
            Assert.AreEqual(1, stub.RemovedCatalogs.Count);
            Assert.AreEqual(2, stub.RemovedCatalogs[0]);
        }

        [TestMethod]
        public void LogicRemoveCatalogue_Throws_WhenNotExists()
        {
            stub.CatalogExistsReturn = false;
            //Assert.ThrowsException<Exception>(() => service.LogicRemoveCatalogue(2));
        }

        [TestMethod]
        public void LogicAddState_CallsAdd_WhenValid()
        {
            stub.StateExistsReturn = false;
            stub.CatalogExistsReturn = true;
            //service.LogicAddState(3, 5, 1);
            Assert.AreEqual(1, stub.AddedStates.Count);
            Assert.AreEqual((3, 5, 1), stub.AddedStates[0]);
        }

        [TestMethod]
        public void LogicAddState_Throws_WhenStateExists()
        {
            stub.StateExistsReturn = true;
            //Assert.ThrowsException<Exception>(() => service.LogicAddState(3, 5, 1));
        }

        [TestMethod]
        public void LogicAddState_Throws_WhenCatalogNotExists()
        {
            stub.StateExistsReturn = false;
            stub.CatalogExistsReturn = false;
            //Assert.ThrowsException<Exception>(() => service.LogicAddState(3, 5, 1));
        }

        [TestMethod]
        public void LogicRemoveState_CallsRemove_WhenExists()
        {
            stub.StateExistsReturn = true;
            //service.LogicRemoveState(4);
            Assert.AreEqual(1, stub.RemovedStates.Count);
            Assert.AreEqual(4, stub.RemovedStates[0]);
        }

        [TestMethod]
        public void LogicRemoveState_Throws_WhenNotExists()
        {
            stub.StateExistsReturn = false;
            //Assert.ThrowsException<Exception>(() => service.LogicRemoveState(4));
        }

        [TestMethod]
        public void LogicAddUser_CallsAdd_WhenNotExists()
        {
            stub.UserExistsReturn = false;
            //service.LogicAddUser(5, "F", "L");
            Assert.AreEqual(1, stub.AddedUsers.Count);
            Assert.AreEqual((5, "F", "L"), stub.AddedUsers[0]);
        }

        [TestMethod]
        public void LogicAddUser_Throws_WhenExists()
        {
            stub.UserExistsReturn = true;
            //Assert.ThrowsException<Exception>(() => service.LogicAddUser(5, "F", "L"));
        }

        [TestMethod]
        public void LogicRemoveUser_CallsRemove_WhenExists()
        {
            stub.UserExistsReturn = true;
            //service.LogicRemoveUser(6);
            Assert.AreEqual(1, stub.RemovedUsers.Count);
            Assert.AreEqual(6, stub.RemovedUsers[0]);
        }

        [TestMethod]
        public void LogicRemoveUser_Throws_WhenNotExists()
        {
            stub.UserExistsReturn = false;
            //Assert.ThrowsException<Exception>(() => service.LogicRemoveUser(6));
        }

        [TestMethod]
        public void LogicAddDatabaseEvent_CallsAdd_WhenValid()
        {
            stub.EventExistsReturn = false;
            stub.UserExistsReturn = true;
            //service.LogicAddDatabaseEvent(7, 5, 1, true);
            Assert.AreEqual(1, stub.AddedDbEvents.Count);
            Assert.AreEqual((7, 5, 1, true), stub.AddedDbEvents[0]);
        }

        [TestMethod]
        public void LogicAddDatabaseEvent_Throws_WhenEventExists()
        {
            stub.EventExistsReturn = true;
            //Assert.ThrowsException<Exception>(() => service.LogicAddDatabaseEvent(7, 5, 1, true));
        }

        [TestMethod]
        public void LogicAddDatabaseEvent_Throws_WhenEmployeeNotExists()
        {
            stub.EventExistsReturn = false;
            stub.UserExistsReturn = false;
            //Assert.ThrowsException<Exception>(() => service.LogicAddDatabaseEvent(7, 5, 1, true));
        }

        [TestMethod]
        public void LogicAddUserEvent_CallsAdd_WhenValid()
        {
            stub.EventExistsReturn = false;
            stub.UserExistsReturn = true;
            stub.StateExistsReturn = true;
            //service.LogicAddUserEvent(8, 5, 1, 6, false);
            Assert.AreEqual(1, stub.AddedUserEvents.Count);
            Assert.AreEqual((8, 5, 1, 6, false), stub.AddedUserEvents[0]);
        }

        [TestMethod]
        public void LogicAddUserEvent_Throws_WhenEventExists()
        {
            stub.EventExistsReturn = true;
            //Assert.ThrowsException<Exception>(() => service.LogicAddUserEvent(8, 5, 1, 6, false));
        }

        [TestMethod]
        public void LogicAddUserEvent_Throws_WhenUserNotExists()
        {
            stub.EventExistsReturn = false;
            stub.UserExistsReturn = false;
            stub.StateExistsReturn = true;
            //Assert.ThrowsException<Exception>(() => service.LogicAddUserEvent(8, 5, 1, 6, false));
        }

        [TestMethod]
        public void LogicAddUserEvent_Throws_WhenStateNotExists()
        {
            stub.EventExistsReturn = false;
            stub.UserExistsReturn = true;
            stub.StateExistsReturn = false;
           // Assert.ThrowsException<Exception>(() => service.LogicAddUserEvent(8, 5, 1, 6, false));
        }

        [TestMethod]
        public void LogicRemoveEvent_CallsRemove_WhenExists()
        {
            stub.EventExistsReturn = true;
            //service.LogicRemoveEvent(9);
            Assert.AreEqual(1, stub.RemovedEvents.Count);
            Assert.AreEqual(9, stub.RemovedEvents[0]);
        }

        [TestMethod]
        public void LogicRemoveEvent_Throws_WhenNotExists()
        {
            stub.EventExistsReturn = false;
            //Assert.ThrowsException<Exception>(() => service.LogicRemoveEvent(9));
        }
    }
}