using LibraryDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryLogicLayer
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly List<LibraryCatalog> _catalogs = new List<LibraryCatalog>();

        public void AddCatalog(int catalogId, string title, string author, int numberOfPages)
        {
            var catalog = new LibraryCatalog
            {
                catalogId = catalogId,
                title = title,
                author = author,
                nrOfPages = numberOfPages
            };
            _catalogs.Add(catalog);
        }

        public void RemoveCatalogById(int id)
        {
            var catalog = _catalogs.FirstOrDefault(c => c.catalogId == id);
            if (catalog != null)
            {
                _catalogs.Remove(catalog);
            }
        }

        public LibraryCatalog GetCatalogById(int id)
        {
            return _catalogs.FirstOrDefault(c => c.catalogId == id);
        }
    }

    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public void AddUser(int userId, string firstName, string lastName)
        {
            var user = new User
            {
                userId = userId,
                firstName = firstName,
                lastName = lastName
            };
            _users.Add(user);
        }

        public void RemoveUserById(int id)
        {
            var user = _users.FirstOrDefault(u => u.userId == id);
            if (user != null)
            {
                _users.Remove(user);
            }
        }

        public User GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.userId == id);
        }
    }

    public class EventRepository : IEventRepository
    {
        private readonly List<LibraryEvent> _events = new List<LibraryEvent>();

        public void AddDatabaseEvent(int eventId, User employee, LibraryState state, bool addition)
        {
            var databaseEvent = new LibraryDatabaseEvent
            {
                eventId = eventId,
                employee = employee,
                state = state,
                addition = addition
            };
            _events.Add(databaseEvent);
        }

        public void AddUserEvent(int eventId, User employee, LibraryState state, User user, bool borrowing)
        {
            var userEvent = new LibraryUserEvent
            {
                eventId = eventId,
                employee = employee,
                state = state,
                user = user,
                borrowing = borrowing
            };
            _events.Add(userEvent);
        }

        public void RemoveEventById(int id)
        {
            var eventObj = _events.FirstOrDefault(e => e.eventId == id);
            if (eventObj != null)
            {
                _events.Remove(eventObj);
            }
        }

        public LibraryEvent GetEventById(int id)
        {
            return _events.FirstOrDefault(e => e.eventId == id);
        }
    }

    public class StateRepository : IStateRepository
    {
        private readonly List<LibraryState> _states = new List<LibraryState>();

        public void AddState(int stateId, int nrOfBooks, LibraryCatalog catalog)
        {
            var state = new LibraryState
            {
                stateId = stateId,
                nrOfBooks = nrOfBooks,
                catalog = catalog
            };
            _states.Add(state);
        }

        public void RemoveStateByID(int id)
        {
            var state = _states.FirstOrDefault(s => s.stateId == id);
            if (state != null)
            {
                _states.Remove(state);
            }
        }

        public LibraryState GetStateById(int id)
        {
            return _states.FirstOrDefault(s => s.stateId == id);
        }
    }
}
