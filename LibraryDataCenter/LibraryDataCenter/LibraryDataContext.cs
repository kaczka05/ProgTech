
using LibraryLogicLayer;

namespace LibraryDataLayer
{
    internal class LibraryDataContext : ILibraryDataContext
    {
        public List<IUser> Users { get; init; } = new List<IUser>();
        public List<ICatalog> Catalogs { get; init; } = new List<ICatalog>();
        public List<IEvent> Events { get; init; } = new List<IEvent>();
        public List<IState> States { get; init; } = new List<IState>();

        public void AddCatalog(int catalogId, string title, string author, int numberOfPages)
        {
            Catalogs.Add(new Book(catalogId, title, author, numberOfPages));
        }

        public void RemoveCatalogById(int id)
        {
            var catalog = Catalogs.FirstOrDefault(c => c.catalogId == id);
            if (catalog != null)
            {
                Catalogs.Remove(catalog);
            }
        }

        public ICatalog GetCatalogById(int id)
        {
            return Catalogs.FirstOrDefault(c => c.catalogId == id);
        }


        public void AddUser(int userId, string firstName, string lastName)
        {
            Users.Add(new User(userId, firstName, lastName));
        }

        public void RemoveUserById(int id)
        {
            var user = Users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                Users.Remove(user);
            }
        }

        public IUser GetUserById(int id)
        {
            return Users.FirstOrDefault(u => u.UserId == id);
        }



        public void AddDatabaseEvent(int eventId, int employeeId, int stateId, bool addition)
        {
            Events.Add(new DatabaseEvent(eventId, GetUserById(employeeId), GetStateById(stateId), addition);
        }

        public void AddUserEvent(int eventId, int employeeId, int stateId, int userId, bool borrowing)
        {
            Events.Add(new UserEvent(eventId, GetUserById(employeeId), GetStateById(stateId), GetUserById(userId), borrowing);
        }

        public void RemoveEventById(int id)
        {
            var eventObj = Events.FirstOrDefault(e => e.EventId == id);
            if (eventObj != null)
            {
                Events.Remove(eventObj);
            }
        }

        public IEvent GetEventById(int id)
        {
            return Events.FirstOrDefault(e => e.EventId == id);
        }

        public void AddState(int stateId, int nrOfBooks, int catalogId)
        {

            States.Add(new State(stateId, nrOfBooks, GetCatalogById(catalogId)));
        }

        public void RemoveStateByID(int id)
        {
            var state = States.FirstOrDefault(s => s.StateId == id);
            if (state != null)
            {
                States.Remove(state);
            }
        }

        public IState GetStateById(int id)
        {
            return States.FirstOrDefault(s => s.StateId == id);
        }
    }


}