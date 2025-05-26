using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDataLayer
{
    internal class LibraryDataContext : ILibraryDataContext
    {
        private readonly List<Book> _books = new();
        private readonly List<User> _users = new();
        private readonly List<Event> _events = new();
        private readonly List<State> _states = new();

        public IQueryable<ICatalog> Catalogs => _books.AsQueryable();
        public IQueryable<IUser> Users => _users.AsQueryable();
        public IQueryable<IEvent> Events => _events.AsQueryable();
        public IQueryable<IState> States => _states.AsQueryable();

        public Task AddCatalogAsync(ICatalog catalog)
        {
            _books.Add(new Book(catalog.CatalogId, catalog.Title, catalog.Author, catalog.NrOfPages));
            return Task.CompletedTask;
        }

        public Task RemoveCatalogAsync(ICatalog catalog)
        {
            var book = _books.FirstOrDefault(b => b.CatalogId == catalog.CatalogId);
            if (book != null)
                _books.Remove(book);
            return Task.CompletedTask;
        }

        public Task AddUserAsync(IUser user)
        {
            _users.Add(new User(user.UserId, user.FirstName, user.LastName));
            return Task.CompletedTask;
        }

        public Task RemoveUserAsync(IUser user)
        {
            var u = _users.FirstOrDefault(x => x.UserId == user.UserId);
            if (u != null)
                _users.Remove(u);
            return Task.CompletedTask;
        }

        public Task AddDatabaseEventAsync(IDatabaseEvent databaseEvent)
        {
            _events.Add(new DatabaseEvent(
                databaseEvent.EventId,
                databaseEvent.Employee,
                databaseEvent.State,
                databaseEvent.Addition
            ));
            return Task.CompletedTask;
        }

        public Task AddUserEventAsync(IUserEvent userEvent)
        {
            _events.Add(new UserEvent(
                userEvent.EventId,
                userEvent.Employee,
                userEvent.State,
                userEvent.User,
                userEvent.Borrowing
            ));
            return Task.CompletedTask;
        }

        public Task RemoveEventAsync(IEvent eventObj)
        {
            var ev = _events.FirstOrDefault(e => e.EventId == eventObj.EventId);
            if (ev != null)
                _events.Remove(ev);
            return Task.CompletedTask;
        }

        public Task AddStateAsync(IState state)
        {
            _states.Add(new State(state.StateId, state.NrOfBooks, state.Catalog));
            return Task.CompletedTask;
        }

        public Task RemoveStateAsync(IState state)
        {
            var s = _states.FirstOrDefault(x => x.StateId == state.StateId);
            if (s != null)
                _states.Remove(s);
            return Task.CompletedTask;
        }
    }
}
