
namespace LibraryDataLayer
{
    public class DataContext
    {
        private List<Catalog> catalogs = new List<Catalog>();
        private List<Event> events = new List<Event>();
        private List<Users> users = new List<Users>();
        private List<State> states = new List<State>();

        public void AddCatalog(Catalog catalog)
        {
            catalogs.Add(catalog);
        }

        public void RemoveCatalog(Catalog catalog)
        {
            catalogs.Remove(catalog);
        }

        public IReadOnlyList<Catalog> GetCatalogs()
        {
            return catalogs.AsReadOnly();
        }

        // Event methods
        public void AddEvent(Event evt)
        {
            events.Add(evt);
        }

        public void RemoveEvent(Event evt)
        {
            events.Remove(evt);
        }

        public IReadOnlyList<Event> GetEvents()
        {
            return events.AsReadOnly();
        }

        // User methods
        public void AddUser(Users user)
        {
            users.Add(user);
        }

        public void RemoveUser(Users user)
        {
            users.Remove(user);
        }

        public IReadOnlyList<Users> GetUsers()
        {
            return users.AsReadOnly();
        }

        // State methods
        public void AddState(State state)
        {
            states.Add(state);
        }

        public void RemoveState(State state)
        {
            states.Remove(state);
        }

        public IReadOnlyList<State> GetStates()
        {
            return states.AsReadOnly();
        }

    }
}
