


using Microsoft.EntityFrameworkCore;

namespace LibraryDataLayer
{
    internal class LibraryDataRepository : ILibraryDataRepository
    {
        private LibraryDBDataContext _libraryDataContext;
        private LibraryDataContext _libraryDataContextOff;
        private bool connected;
        

        public LibraryDataRepository(string? customConnectionString = null)
        {
            if (customConnectionString != null)
            {
                connected = true;
                _libraryDataContext = new LibraryDBDataContext(customConnectionString);
            }
            else
            {
                connected = false;
                _libraryDataContextOff = new LibraryDataContext();
            }
        }

        private ICatalog? EntryToObj(Books book)
        {
            if (book != null)
            {
                return new Book(book.catalogId, book.title, book.author, (int)book.nrOfPages);
            }
            else
            {
                return null;
            }
        }



        public void AddCatalog(int catalogId, string title, string author, int numberOfPages)
        {
            
            if(connected)
            {
                Books book = new Books
                {
                    catalogId = catalogId,
                    title = title,
                    author = author,
                    nrOfPages = numberOfPages
                }
            ;
                _libraryDataContext.Books.InsertOnSubmit(book);
                _libraryDataContext.SubmitChanges();
            }
            else
            {
                Book book = new Book(catalogId,title,author,numberOfPages);            
                _libraryDataContextOff.AddCatalogAsync(book);
            }

        }

        public void RemoveCatalogById(int id)
        {
            if (connected)
            {
                Books cat = _libraryDataContext.Books.Single(Books => Books.catalogId == id);
                _libraryDataContext.Books.DeleteOnSubmit(cat);
                _libraryDataContext.SubmitChanges();
            }
            else
            {
                Book cat = (Book)_libraryDataContextOff.Catalogs.Single(Books => Books.CatalogId == id);
                _libraryDataContextOff.RemoveCatalogAsync(cat);
            }

        }

        public ICatalog? GetCatalogById(int id)
        {
            if(connected)
            {
                Books c = new Books();
                var cat = (from Books
                           in _libraryDataContext.Books
                           where Books.catalogId == id
                           select Books).FirstOrDefault();
                if (cat == null)
                {
                    return null;
                }
                else
                {
                    c.catalogId = cat.catalogId;
                    c.title = cat.title;
                    c.author = cat.author;
                    c.nrOfPages = cat.nrOfPages;
                    return EntryToObj(c);
                }
            }
            else
            {
                var cat = _libraryDataContextOff.Catalogs
                    .FirstOrDefault(c => c.CatalogId == id);
                if (cat == null)
                {
                    return null;
                }
                if (cat is Book b)
                    return b;
                else
                    return new Book(cat.CatalogId, cat.Title, cat.Author, cat.NrOfPages);
            }

        }
        public IEnumerable<ICatalog> GetAllCatalogs()
        {
            if (connected)
            {
                var cat = _libraryDataContext.Books
                    .AsEnumerable() 
                    .Select(Books => EntryToObj(Books));
                return cat;
            }
            else
            {
                var cat = _libraryDataContextOff.Catalogs
                    .AsEnumerable() 
                    .Select(c =>
                    {
                        if (c is Book b)
                        {
                            return b;
                        }
                        else
                        {
                            return new Book(c.CatalogId, c.Title, c.Author, c.NrOfPages);
                        }
                    });
                return cat;
            }
        }
        private IUser? EntryToObj(Users user)
        {
            if (user != null)
            {
                return new User(user.UserId, user.FirstName, user.LastName);
            }
            else
            {
                return null;
            }
        }


        private IEvent? EntryToObj(Events ev)
        {
            if (ev != null)
            {
                if (ev.EventType == "User")
                {
                    return new UserEvent(ev.EventId, (int)ev.Employee, (int)ev.State, (int)ev.User, (bool)ev.Borrowing);
                }
                else if (ev.EventType == "Database")
                {
                    return new DatabaseEvent(ev.EventId, (int)ev.Employee, (int)ev.State, (bool)ev.Addition);
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
        }


        public void AddUser(int userId, string firstName, string lastName)
        {
            if (connected)
            {
                Users user = new Users
                {
                    UserId = userId,
                    FirstName = firstName,
                    LastName = lastName
                };
                _libraryDataContext.Users.InsertOnSubmit(user);
                _libraryDataContext.SubmitChanges();
            }
            else
            {
                User user = new User(userId, firstName, lastName);
                _libraryDataContextOff.AddUserAsync(user);
            }
        }


        public void RemoveUserById(int id)
        {
            if(connected)
            {
                Users user = _libraryDataContext.Users.Single(u => u.UserId == id);
                _libraryDataContext.Users.DeleteOnSubmit(user);
                _libraryDataContext.SubmitChanges();
            }
            else
            {
                User user = (User)_libraryDataContextOff.Users.Single(u => u.UserId == id);
                _libraryDataContextOff.RemoveUserAsync(user);
            }
            
        }

   
        public IUser? GetUserById(int id)
        {
            if(connected)
            { 
            var user = (from u in _libraryDataContext.Users
                        where u.UserId == id
                        select u).FirstOrDefault();
            return EntryToObj(user);
            }
            else
            {
                var user = _libraryDataContextOff.Users
                    .FirstOrDefault(u => u.UserId == id);
                if (user == null)
                {
                    return null;
                }
                return new User(user.UserId, user.FirstName, user.LastName);
            }

        }

     
        public IEnumerable<IUser> GetAllUsers()
        {
            if (!connected)
            {
                return _libraryDataContextOff.Users
                    .Select(u => new User(u.UserId, u.FirstName, u.LastName));
            }
            else
            {
                var users = from u in _libraryDataContext.Users
                            select EntryToObj(u);
                return users;
            }
           
        }


        public void AddDatabaseEvent(int eventId, int employeeId, int stateId, bool addition)
        {
            if (connected)
            {
                Events ev = new Events
                {
                    EventId = eventId,
                    Employee = employeeId,
                    State = stateId,
                    Addition = addition,
                    EventType = "Database"
                };
                _libraryDataContext.Events.InsertOnSubmit(ev);
                _libraryDataContext.SubmitChanges();
            }
            else
            {
                DatabaseEvent ev = new DatabaseEvent(eventId, employeeId, (stateId), addition);
                _libraryDataContextOff.AddDatabaseEventAsync(ev);
            }
        }


        public void AddUserEvent(int eventId, int employeeId, int stateId, int userId, bool borrowing)
        {
            if (connected)
            {
                Events ev = new Events
                {
                    EventId = eventId,
                    Employee = employeeId,
                    State = stateId,
                    User = userId,
                    Borrowing = borrowing,
                    EventType = "User"
                };
                _libraryDataContext.Events.InsertOnSubmit(ev);
                _libraryDataContext.SubmitChanges();
            }
            else
            {
                UserEvent ev = new UserEvent(eventId, employeeId, stateId, userId, borrowing);
                _libraryDataContextOff.AddUserEventAsync(ev);
            }
        }
    
        public void RemoveEventById(int id)
        {
            if (connected)
            {
                Events ev = _libraryDataContext.Events.Single(e => e.EventId == id);
                _libraryDataContext.Events.DeleteOnSubmit(ev);
                _libraryDataContext.SubmitChanges();
            }
            else
            {
                IEvent ev = _libraryDataContextOff.Events.Single(e => e.EventId == id);
                _libraryDataContextOff.RemoveEventAsync(ev);
            }

        }

        public IEvent? GetEventById(int id)
        {
            if (!connected)
            {
                var ev = _libraryDataContextOff.Events
                    .FirstOrDefault(e => e.EventId == id);
                if (ev == null)
                {
                    return null;
                }
                return ev;
            }
            else
            {
                var ev = (from e in _libraryDataContext.Events
                          where e.EventId == id
                          select e).FirstOrDefault();
                return EntryToObj(ev);
            }
            
        }

        public IEnumerable<IEvent> GetAllEvents()
        {
            if (!connected)
            {
                IEnumerable<IEvent> cat = _libraryDataContextOff.Events
                .AsEnumerable()
                .Select(c =>
                {
                    if (c is DatabaseEvent dbEvent)
                    {
                        return (IEvent)dbEvent;
                    }
                    else if (c is UserEvent userEvent)
                    {
                        return (IEvent)userEvent;
                    }
                    else
                    {
                        return new DatabaseEvent(c.EventId, (c.Employee), (c.State), c.Addition); //may cause problems for user event
                    }
                });

                return cat;
            }
            else
            {
                var events = _libraryDataContext.Events
                             .AsEnumerable()
                             .Select(e => EntryToObj(e));
                return events;
            }
        }
      
        private IState? EntryToObj(States state)
        {
            if (state != null)
            {
                return new State(state.StateId, state.NrOfBooks, state.Catalog);
            }
            else
            {
                return null;
            }
        }

   
        public void AddState(int stateId, int nrOfBooks, int catalogId)
        {
            if(connected)
            {
                States state = new States
                {
                    StateId = stateId,
                    NrOfBooks = nrOfBooks,
                    Catalog = catalogId
                };
                _libraryDataContext.States.InsertOnSubmit(state);
                _libraryDataContext.SubmitChanges();
            }
            else
            {
                State state = new State(stateId, nrOfBooks, catalogId);
                _libraryDataContextOff.AddStateAsync(state);
            }

        }

     
        public void RemoveStateByID(int id)
        {
            if (!connected)
            {
                State state = (State)_libraryDataContextOff.States.Single(s => s.StateId == id);
                _libraryDataContextOff.RemoveStateAsync(state);
                return;
            }
            else
            {
                States state = _libraryDataContext.States.Single(s => s.StateId == id);
                _libraryDataContext.States.DeleteOnSubmit(state);
                _libraryDataContext.SubmitChanges();
            }
        }

        public IState? GetStateById(int id)
        {
            if (!connected)
            {
                var state = _libraryDataContextOff.States
                    .FirstOrDefault(s => s.StateId == id);
                if (state == null)
                {
                    return null;
                }
                return new State(state.StateId, state.NrOfBooks, (state.Catalog));
            }
            else
            {
                var state = (from s in _libraryDataContext.States
                             where s.StateId == id
                             select s).FirstOrDefault();
                return EntryToObj(state);
            }
        }

        public IEnumerable<IState> GetAllStates()
        {
            if (!connected)
            {
                return _libraryDataContextOff.States
                    .Select(s => new State(s.StateId, s.NrOfBooks, (s.Catalog)));
            }
            else
            {
                var cat = _libraryDataContext.States
                    .AsEnumerable()
                    .Select(States => EntryToObj(States));
                return cat;
            }
           
        }





        public bool DoesCatalogExist(int id)
        {
            if (connected)
            {
                return _libraryDataContext.Books.Any(c => c.catalogId == id);
            }
            else
            {
                return _libraryDataContextOff.Catalogs.Any(c => c.CatalogId == id);
            }
        }
        public bool DoesUserExist(int id)
        {
            if(connected)
            {
                return _libraryDataContext.Users.Any(u => u.UserId == id);
            }
            else
            {
                return _libraryDataContextOff.Users.Any(u => u.UserId == id);
            }
        }
        public bool DoesEventExist(int id)
        {
            if (connected)
            {
                return _libraryDataContext.Events.Any(e => e.EventId == id);
            }
            else
            {
                return _libraryDataContextOff.Events.Any(e => e.EventId == id);
            }
        }
        public bool DoesStateExist(int id)
        {
            if (connected)
            {
                return _libraryDataContext.States.Any(s => s.StateId == id);
            }
            else
            {
                return _libraryDataContextOff.States.Any(s => s.StateId == id);
            }
        }

    }


}