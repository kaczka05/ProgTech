


using Microsoft.EntityFrameworkCore;

namespace LibraryDataLayer
{
    internal class LibraryDataContext : DbContext, ILibraryDataContext
    {
        private readonly string _connectionString;
        public LibraryDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }
       
        public DbSet<Book> Books { get; set; }
        public IQueryable<ICatalog> Catalogs => Books;

        public DbSet<User> UsersDbSet { get; set; }
        public IQueryable<IUser> Users => UsersDbSet;

        public DbSet<Event> EventsDbSet { get; set; }
        public IQueryable<IEvent> DatabaseEvents => EventsDbSet;

        public DbSet<State> StatesDbSet { get; set; }
        public IQueryable<IState> States => StatesDbSet;

        public async Task AddCatalogAsync(ICatalog catalog)
        {
            var entity = new Book(catalog.CatalogId, catalog.Title, catalog.Author, catalog.NrOfPages);
            await Books.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task RemoveCatalogAsync(ICatalog catalog)
        {
            var entity = await Books.FindAsync(catalog.CatalogId);
            if (entity != null)
            {
                Books.Remove(entity);
                await SaveChangesAsync();
            }
        }

        public async Task AddUserAsync(IUser user)
        {
            var entity = new User(user.UserId, user.FirstName, user.LastName);
            await UsersDbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task RemoveUserAsync(IUser user)
        {
            var entity = await UsersDbSet.FindAsync(user.UserId);
            if (entity != null)
            {
                UsersDbSet.Remove(entity);
                await SaveChangesAsync();
            }
        }

        public async Task AddDatabaseEventAsync(IDatabaseEvent databaseEvent)
        {
            var entity = new DatabaseEvent(
                databaseEvent.EventId,
                databaseEvent.Employee,
                databaseEvent.State,
                databaseEvent.Addition
            );
            await EventsDbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task AddUserEventAsync(IUserEvent userEvent)
        {
            var entity = new UserEvent(
                userEvent.EventId,
                userEvent.Employee,
                userEvent.State,
                userEvent.User,
                userEvent.Borrowing
            );
            await EventsDbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task RemoveEventAsync(IEvent eventObj)
        {
            var entity = await EventsDbSet.FindAsync(eventObj.EventId);
            if (entity != null)
            {
                EventsDbSet.Remove(entity);
                await SaveChangesAsync();
            }
        }

        public async Task AddStateAsync(IState state)
        {
            var entity = new State(state.StateId, state.NrOfBooks, state.Catalog);
            await StatesDbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task RemoveStateAsync(IState state)
        {
            var entity = await StatesDbSet.FindAsync(state.StateId);
            if (entity != null)
            {
                StatesDbSet.Remove(entity);
                await SaveChangesAsync();
            }
        }


    }


}