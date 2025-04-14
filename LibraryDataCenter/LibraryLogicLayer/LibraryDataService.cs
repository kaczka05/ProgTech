using LibraryDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryLogicLayer
{
    public class LibraryDataService
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IStateRepository _stateRepository;

        public LibraryDataService(
            ICatalogRepository catalogRepository,
            IUserRepository userRepository,
            IEventRepository eventRepository,
            IStateRepository stateRepository)
        {
            _catalogRepository = catalogRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _stateRepository = stateRepository;
        }

        // Catalog methods
        public void AddCatalog(int catalogId, string title, string author, int numberOfPages)
        {
            _catalogRepository.AddCatalog(catalogId, title, author, numberOfPages);
        }

        public void RemoveCatalogById(int catalogId)
        {
            _catalogRepository.RemoveCatalogById(catalogId);
        }

        public LibraryCatalog GetCatalogById(int catalogId)
        {
            return _catalogRepository.GetCatalogById(catalogId);
        }

        // User methods
        public void AddUser(int userId, string firstName, string lastName)
        {
            _userRepository.AddUser(userId, firstName, lastName);
        }

        public void RemoveUserById(int userId)
        {
            _userRepository.RemoveUserById(userId);
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        // Event methods
        public void AddDatabaseEvent(int eventId, User employee, LibraryState state, bool addition)
        {
            _eventRepository.AddDatabaseEvent(eventId, employee, state, addition);
        }

        public void AddUserEvent(int eventId, User employee, LibraryState state, User user, bool borrowing)
        {
            _eventRepository.AddUserEvent(eventId, employee, state, user, borrowing);
        }

        public void RemoveEventById(int eventId)
        {
            _eventRepository.RemoveEventById(eventId);
        }

        public LibraryEvent GetEventById(int eventId)
        {
            return _eventRepository.GetEventById(eventId);
        }

        // State methods
        public void AddState(int stateId, int nrOfBooks, LibraryCatalog catalog)
        {
            _stateRepository.AddState(stateId, nrOfBooks, catalog);
        }

        public void RemoveStateById(int stateId)
        {
            _stateRepository.RemoveStateByID(stateId);
        }

        public LibraryState GetStateById(int stateId)
        {
            return _stateRepository.GetStateById(stateId);
        }
    }
}
