using LibraryDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryLogicLayer
{
   internal class LibraryDataService : ILibraryDataService
    {
        private static ILibraryDataRepository _libraryDataRepository = default;
        public LibraryDataService(ILibraryDataRepository libraryDataRepository)
        {
            _libraryDataRepository = libraryDataRepository;
        }
        public void LogicAddCatalogue(int catalogId, string title, string author, int numberOfPages)
        {
            if (_libraryDataRepository.DoesCatalogExist(catalogId))
            {
                throw new Exception("Catalog already exists");
            }
            _libraryDataRepository.AddCatalog(catalogId, title, author, numberOfPages);
        }
        public void LogicRemoveCatalogue(int id)
        {
            if (!_libraryDataRepository.DoesCatalogExist(id))
            {
                throw new Exception("Catalog does not exist");
            }
            _libraryDataRepository.RemoveCatalogById(id);
        }
        public void LogicAddState(int stateId, int nrOfBooks, int catalogId)
        {
            if (_libraryDataRepository.DoesStateExist(stateId))
            {
                throw new Exception("State already exists");
            }
            if (!_libraryDataRepository.DoesCatalogExist(catalogId))
            {
                throw new Exception("Catalog does not exist");
            }
            _libraryDataRepository.AddState(stateId, nrOfBooks, catalogId);
        }
        public void LogicRemoveState(int id)
        {
            if (!_libraryDataRepository.DoesStateExist(id))
            {
                throw new Exception("State does not exist");
            }
            _libraryDataRepository.RemoveStateByID(id);
        }
        public void LogicAddUser(int userId, string firstName, string lastName)
        {
            if (_libraryDataRepository.DoesUserExist(userId))
            {
                throw new Exception("User already exists");
            }
            _libraryDataRepository.AddUser(userId, firstName, lastName);
        }
        public void LogicRemoveUser(int id)
        {
            if (!_libraryDataRepository.DoesUserExist(id))
            {
                throw new Exception("User does not exist");
            }
            _libraryDataRepository.RemoveUserById(id);
        }
        public void LogicAddUserEvent(int eventId, int employeeId, int stateId, int userId, bool borrowing)
        {
            if (_libraryDataRepository.DoesEventExist(eventId))
            {
                throw new Exception("Event already exists");
            }
            if (!_libraryDataRepository.DoesUserExist(userId))
            {
                throw new Exception("User does not exist");
            }
            if (!_libraryDataRepository.DoesStateExist(stateId))
            {
                throw new Exception("State does not exist");
            }
            _libraryDataRepository.AddUserEvent(eventId, employeeId, stateId, userId, borrowing);
        }
        public void LogicAddDatabaseEvent(int eventId, int employeeId, int stateId, bool addition)
        {
            if (_libraryDataRepository.DoesEventExist(eventId))
            {
                throw new Exception("Event already exists");
            }
            if (!_libraryDataRepository.DoesUserExist(employeeId))
            {
                throw new Exception("Employee does not exist");
            }
            _libraryDataRepository.AddDatabaseEvent(eventId, employeeId, stateId, addition);
        }
        public void LogicRemoveEvent(int id)
        {
            if (!_libraryDataRepository.DoesEventExist(id))
            {
                throw new Exception("Event does not exist");
            }
            _libraryDataRepository.RemoveEventById(id);
        }
    }
}
