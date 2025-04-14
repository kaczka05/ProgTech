using LibraryDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryLogicLayer
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly List<Catalog> _catalogs = new List<Catalog>();

        public void AddCatalog(int catalogId, string title, string author, int numberOfPages)
        {
            var catalog = new Catalog
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

        public Catalog GetCatalogById(int id)
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
        private readonly List<Event> _events = new List<Event>();

        public void AddDatabaseEvent(int eventId, User employee, State state, bool addition)
        {
            var databaseEvent = new DatabaseEvent
            {
                eventId = eventId,
                employee = employee,
                state = state,
                addition = addition
            };
            _events.Add(databaseEvent);
        }

        public void AddUserEvent(int eventId, User employee, State state, User user, bool borrowing)
        {
            var userEvent = new UserEvent
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

        public Event GetEventById(int id)
        {
            return _events.FirstOrDefault(e => e.eventId == id);
        }
    }

    public class StateRepository : IStateRepository
    {
        private readonly List<State> _states = new List<State>();

        public void AddState(int stateId, int nrOfBooks, Catalog catalog)
        {
            var state = new State
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

        public State GetStateById(int id)
        {
            return _states.FirstOrDefault(s => s.stateId == id);
        }
    }
}
