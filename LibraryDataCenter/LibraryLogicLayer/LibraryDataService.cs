using LibraryDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryLogicLayer
{
    internal class LibraryDataService
    {
        private static ICatalogRepository _catalogRepository;
        private static IUserRepository _userRepository;
        private static IEventRepository _eventRepository;
        private static IStateRepository _stateRepository;

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

        public void AddCatalog(int catalogId, string title, string author, int numberOfPages)
        {
            _catalogRepository.AddCatalog(catalogId, title, author, numberOfPages);
        }

        public void RemoveCatalogById(int catalogId)
        {
            _catalogRepository.RemoveCatalogById(catalogId);
        }

        public ICatalog GetCatalogById(int catalogId)
        {
            return _catalogRepository.GetCatalogById(catalogId);
        }

        public void AddUser(int userId, string firstName, string lastName)
        {
            _userRepository.AddUser(userId, firstName, lastName);
        }

        public void RemoveUserById(int userId)
        {
            _userRepository.RemoveUserById(userId);
        }

        public IUser GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public void AddDatabaseEvent(int eventId, IUser employee, IState state, bool addition)
        {
            _eventRepository.AddDatabaseEvent(eventId, employee, state, addition);
        }

        public void AddUserEvent(int eventId, IUser employee, IState state, IUser user, bool borrowing)
        {
            _eventRepository.AddUserEvent(eventId, employee, state, user, borrowing);
        }

        public void RemoveEventById(int eventId)
        {
            _eventRepository.RemoveEventById(eventId);
        }

        public IEvent GetEventById(int eventId)
        {
            return _eventRepository.GetEventById(eventId);
        }

        public void AddState(int stateId, int nrOfBooks, ICatalog catalog)
        {
            _stateRepository.AddState(stateId, nrOfBooks, catalog);
        }

        public void RemoveStateById(int stateId)
        {
            _stateRepository.RemoveStateByID(stateId);
        }

        public IState GetStateById(int stateId)
        {
            return _stateRepository.GetStateById(stateId);
        }
    }
}
