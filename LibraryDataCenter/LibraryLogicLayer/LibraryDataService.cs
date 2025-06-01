using LibraryDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryLogicLayer
{
   internal class LibraryDataService : ILibraryDataService
    {
        private readonly ILibraryDataRepository _libraryDataRepository;
        public LibraryDataService()
        {
            _libraryDataRepository = ILibraryDataRepository.CreateNewRepository();
        }
        public LibraryDataService(ILibraryDataRepository libraryDataRepository)
        {
            _libraryDataRepository = libraryDataRepository;
        }

        private LogicCatalog ConvertToLogicCatalog(ICatalog catalog) =>
            new LogicCatalog(catalog.CatalogId, catalog.Title, catalog.Author, catalog.NrOfPages);
        
        public async Task AddCatalogAsync(int id, string author, string title, int nrOfPages) =>
            await Task.Run(() =>
            {
                if (_libraryDataRepository.DoesCatalogExist(id))
                {
                    throw new Exception("Catalog already exists");
                }
                _libraryDataRepository.AddCatalog(id, title, author, nrOfPages);
            });

        public async Task RemoveCatalogAsync(int id) =>
            await Task.Run(() =>
            {
                if (!_libraryDataRepository.DoesCatalogExist(id))
                {
                    throw new Exception("Catalog does not exist");
                }
                _libraryDataRepository.RemoveCatalogById(id);
            });
        public List<ILogicCatalog> GetAllCatalogsAsync() =>
            _libraryDataRepository.GetAllCatalogs()
                .Select(c => (ILogicCatalog)ConvertToLogicCatalog(c))
                .ToList();


        private LogicState ConvertToLogicState(IState state) =>
            new LogicState(state.StateId, state.NrOfBooks, (state.Catalog.CatalogId));

        public async Task AddStateAsync(int id, int nrOfBooks, int catalogId) =>
            await Task.Run(() =>
            {
                if (_libraryDataRepository.DoesStateExist(id))
                {
                    throw new Exception("State already exists");
                }
                /*if (!_libraryDataRepository.DoesCatalogExist(catalogId))
                {
                    throw new Exception("Catalog does not exist");
                }*/
                _libraryDataRepository.AddState(id, nrOfBooks, catalogId);
            });
        public async Task RemoveStateAsync(int id) =>
            await Task.Run(() =>
            {
                if (!_libraryDataRepository.DoesStateExist(id))
                {
                    throw new Exception("State does not exist");
                }
                _libraryDataRepository.RemoveStateByID(id);
            });
        public List<ILogicState> GetAllStatesAsync() =>
                       _libraryDataRepository.GetAllStates()
               .Select(s => (ILogicState)ConvertToLogicState(s))
               .ToList();




        private LogicUser ConvertToLogicUser(IUser user) =>
            new LogicUser(user.UserId, user.FirstName, user.LastName);

        public async Task AddUserAsync(int id, string firstName, string lastName) =>
            await Task.Run(() =>
            {
                if (_libraryDataRepository.DoesUserExist(id))
                {
                    throw new Exception("User already exists");
                }
                _libraryDataRepository.AddUser(id, firstName, lastName);
            });
        public async Task RemoveUserAsync(int id) =>
            await Task.Run(() =>
            {
                if (!_libraryDataRepository.DoesUserExist(id))
                {
                    throw new Exception("User does not exist");
                }
                _libraryDataRepository.RemoveUserById(id);
            });
        public List<ILogicUser> GetAllUsersAsync() =>
            _libraryDataRepository.GetAllUsers()
                .Select(u => (ILogicUser)ConvertToLogicUser(u))
                .ToList();
        private LogicUserEvent ConvertToLogicDatabaseEvent(IEvent userEvent) =>
            new LogicUserEvent(userEvent.EventId, (userEvent.Employee.UserId), (userEvent.State.StateId), (userEvent.User.UserId), userEvent.Borrowing);
        private LogicDatabaseEvent ConvertToLogicUserEvent(IEvent databaseEvent) =>
            new LogicDatabaseEvent(databaseEvent.EventId, (databaseEvent.Employee.UserId), (databaseEvent.State.StateId), databaseEvent.Addition);

        private LogicEvent ConvertToLogicEvent(IEvent eventObj)
        {
            return eventObj switch
            {
                IUserEvent userEvent => ConvertToLogicUserEvent(eventObj),
                IDatabaseEvent databaseEvent => ConvertToLogicDatabaseEvent(eventObj),
                _ => throw new Exception("Unknown event type")
            };
        }

        public async Task AddUserEventAsync(int id, int employeeId, int stateId, int userId, bool borrowing) =>
            await Task.Run(() =>
            {
                if (_libraryDataRepository.DoesEventExist(id))
                {
                    throw new Exception("Event already exists");
                }
                /*if (!_libraryDataRepository.DoesUserExist(userId))
                {
                    throw new Exception("User does not exist");
                }
                if (!_libraryDataRepository.DoesStateExist(stateId))
                {
                    throw new Exception("State does not exist");
                }*/
                _libraryDataRepository.AddUserEvent(id, employeeId, stateId, userId, borrowing);
            });
        public async Task AddDatabaseEventAsync(int id, int employeeId, int stateId, bool addition) =>
            await Task.Run(() =>
            {
                if (_libraryDataRepository.DoesEventExist(id))
                {
                    throw new Exception("Event already exists");
                }
                /*if (!_libraryDataRepository.DoesUserExist(employeeId))
                {
                    throw new Exception("Employee does not exist");
                }*/
                _libraryDataRepository.AddDatabaseEvent(id, employeeId, stateId, addition);
            });
        public async Task RemoveEventAsync(int id) =>
            await Task.Run(() =>
            {
                if (!_libraryDataRepository.DoesEventExist(id))
                {
                    throw new Exception("Event does not exist");
                }
                _libraryDataRepository.RemoveEventById(id);
            });
        public List<ILogicEvent> GetAllEventsAsync() =>
            _libraryDataRepository.GetAllEvents()
                .Select(e => (ILogicEvent)ConvertToLogicEvent(e))
                .ToList();

        public void LogicAddCatalogue(int v1, string v2, string v3, int v4)
        {
            throw new NotImplementedException();
        }

        public void LogicRemoveCatalogue(int v)
        {
            throw new NotImplementedException();
        }

        public void LogicAddUser(int v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }

        public void LogicRemoveUser(int v)
        {
            throw new NotImplementedException();
        }

        public void LogicAddUserEvent(int v1, int v2, int v3, int v4, bool v5)
        {
            throw new NotImplementedException();
        }

        public Task AddEventAsync(int newEventId, object userId, object stateId)
        {
            throw new NotImplementedException();
        }
    }
}
