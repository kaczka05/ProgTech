


namespace LibraryDataLayer
{
    internal class LibraryDataRepository : ILibraryDataRepository
    {
        private ILibraryDataContext _libraryDataContext;

        public void AddCatalog(int catalogId, string title, string author, int numberOfPages)
        {
            _libraryDataContext.Catalogs.Add(new Book(catalogId, title, author, numberOfPages));
        }

        public void RemoveCatalogById(int id)
        {
            var catalog = _libraryDataContext.Catalogs.FirstOrDefault(c => c.catalogId == id);
            if (catalog != null)
            {
                _libraryDataContext.Catalogs.Remove(catalog);
            }
        }

        public ICatalog GetCatalogById(int id)
        {
            return _libraryDataContext.Catalogs.FirstOrDefault(c => c.catalogId == id);
        }


        public void AddUser(int userId, string firstName, string lastName)
        {
            _libraryDataContext.Users.Add(new User(userId, firstName, lastName));
        }

        public void RemoveUserById(int id)
        {
            var user = _libraryDataContext.Users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                _libraryDataContext.Users.Remove(user);
            }
        }

        public IUser GetUserById(int id)
        {
            return _libraryDataContext.Users.FirstOrDefault(u => u.UserId == id);
        }



        public void AddDatabaseEvent(int eventId, int employeeId, int stateId, bool addition)
        {
            _libraryDataContext.Events.Add(new DatabaseEvent(eventId, GetUserById(employeeId), GetStateById(stateId), addition));
        }

        public void AddUserEvent(int eventId, int employeeId, int stateId, int userId, bool borrowing)
        {
            _libraryDataContext.Events.Add(new UserEvent(eventId, GetUserById(employeeId), GetStateById(stateId), GetUserById(userId), borrowing));
        }

        public void RemoveEventById(int id)
        {
            var eventObj = _libraryDataContext.Events.FirstOrDefault(e => e.EventId == id);
            if (eventObj != null)
            {
                _libraryDataContext.Events.Remove(eventObj);
            }
        }

        public IEvent GetEventById(int id)
        {
            return _libraryDataContext.Events.FirstOrDefault(e => e.EventId == id);
        }

        public void AddState(int stateId, int nrOfBooks, int catalogId)
        {

            _libraryDataContext.States.Add(new State(stateId, nrOfBooks, GetCatalogById(catalogId)));
        }

        public void RemoveStateByID(int id)
        {
            var state = _libraryDataContext.States.FirstOrDefault(s => s.StateId == id);
            if (state != null)
            {
                _libraryDataContext.States.Remove(state);
            }
        }

        public IState GetStateById(int id)
        {
            return _libraryDataContext.States.FirstOrDefault(s => s.StateId == id);
        }
        public bool DoesCatalogExist(int id)
        {
            return _libraryDataContext.Catalogs.Any(c => c.catalogId == id);
        }
        public bool DoesUserExist(int id)
        {
            return _libraryDataContext.Users.Any(u => u.UserId == id);
        }

        public bool DoesEventExist(int id)
        {
            return _libraryDataContext.Events.Any(e => e.EventId == id);
        }

        public bool DoesStateExist(int id)
        {
            return _libraryDataContext.States.Any(s => s.StateId == id);
        }
    }


}