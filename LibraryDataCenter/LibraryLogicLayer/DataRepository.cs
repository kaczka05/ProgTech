using LibraryDataLayer;
using System.Linq;
namespace LibraryLogicLayer
{
    internal class DataRepository
    {
        DataContext dataContext = new DataContext();
        Users currentEmployee = new Users();
        void BorrowCatalog(State s, Users u)
        {

            UserEvent userEvent = new UserEvent();
            userEvent.eventId = dataContext.GetCatalogs()[dataContext.GetCatalogs().Count - 1].catalogId + 1;
            userEvent.user = u;
            userEvent.state = s;
            userEvent.employee = currentEmployee;
            userEvent.borrowing = true;
            dataContext.AddEvent(userEvent);
        }

        void ReturnCatalog(State s, Users u)
        {
            UserEvent userEvent = new UserEvent();
            userEvent.eventId = dataContext.GetCatalogs()[dataContext.GetCatalogs().Count - 1].catalogId + 1;
            userEvent.user = u;
            userEvent.state = s;
            userEvent.borrowing = false;
            dataContext.AddEvent(userEvent);
        }

        void DestoryCatalog(State s)
        {
            DatabaseEvent databaseEvent = new DatabaseEvent();
            databaseEvent.eventId = dataContext.GetCatalogs()[dataContext.GetCatalogs().Count - 1].catalogId + 1;
            databaseEvent.state = s;
            databaseEvent.addition = true;
            dataContext.AddEvent(databaseEvent);
        }

        void AddCatalog(State s)
        {
            DatabaseEvent databaseEvent = new DatabaseEvent();
            databaseEvent.eventId = dataContext.GetCatalogs()[dataContext.GetCatalogs().Count - 1].catalogId + 1;
            databaseEvent.state = s;
            databaseEvent.addition = false;
            dataContext.AddEvent(databaseEvent);
        }

        public Users GetUsersFromId(int id)
        {
            var user = dataContext.GetUsers().FirstOrDefault(u => u.userId == id);
            if (user == null)
                throw new Exception("No user with that id in database.");
            return user;
        }

        public Event GetEventFromId(int id)
        {
            var evt = dataContext.GetEvents().FirstOrDefault(e => e.eventId == id);
            if (evt == null)
                throw new Exception("No event with that id in database.");
            return evt;
        }

        public State GetStateFromId(int id)
        {
            var state = dataContext.GetStates().FirstOrDefault(s => s.stateId == id);
            if (state == null)
                throw new Exception("No Book with that id in Library.");
            return state;
        }

        public Catalog GetCatalogFromId(int id)
        {
            var catalog = dataContext.GetCatalogs().FirstOrDefault(c => c.catalogId == id);
            if (catalog == null)
                throw new Exception("No Book with that id in database.");
            return catalog;
        }


    }
}
