﻿


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

        // Pomocnicza konwersja Events -> IEvent


        private IEvent? EntryToObj(Events ev)
        {
            if (ev != null)
            {
                IUser employee = GetUserById((int)ev.Employee);
                IState state = GetStateById((int)ev.State);
                IUser user = GetUserById((int)ev.User);
                if (ev.EventType == "User")
                {
                    return new UserEvent(ev.EventId, employee, state, user, (bool)ev.Borrowing);
                }
                else if (ev.EventType == "Database")
                {
                    return new DatabaseEvent(ev.EventId, employee, state, (bool)ev.Addition);
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
            Users user = new Users
            {
                UserId = userId,
                FirstName = firstName,
                LastName = lastName
            };
            _libraryDataContext.Users.InsertOnSubmit(user);
            _libraryDataContext.SubmitChanges();
        }


        public void RemoveUserById(int id)
        {
            Users user = _libraryDataContext.Users.Single(u => u.UserId == id);
            _libraryDataContext.Users.DeleteOnSubmit(user);
            _libraryDataContext.SubmitChanges();
        }

   
        public IUser? GetUserById(int id)
        {
            var user = (from u in _libraryDataContext.Users
                        where u.UserId == id
                        select u).FirstOrDefault();
            return EntryToObj(user);
        }

     
        public IEnumerable<IUser> GetAllUsers()
        {
            var users = from u in _libraryDataContext.Users
                        select EntryToObj(u);
            return users;
        }

  
        public void AddDatabaseEvent(int eventId, int employeeId, int stateId, bool addition)
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

       
        public void AddUserEvent(int eventId, int employeeId, int stateId, int userId, bool borrowing)
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

    
        public void RemoveEventById(int id)
        {
            Events ev = _libraryDataContext.Events.Single(e => e.EventId == id);
            _libraryDataContext.Events.DeleteOnSubmit(ev);
            _libraryDataContext.SubmitChanges();
        }

        public IEvent? GetEventById(int id)
        {
            var ev = (from e in _libraryDataContext.Events
                      where e.EventId == id
                      select e).FirstOrDefault();
            return EntryToObj(ev);
        }

        public IEnumerable<IEvent> GetAllEvents()
        {
            var events = from e in _libraryDataContext.Events
                         select EntryToObj(e);
            return events;
        }
      
        private IState? EntryToObj(States state)
        {
            if (state != null)
            {
                var catalog = GetCatalogById((int)state.Catalog);
                return new State(state.StateId, (int)state.NrOfBooks, catalog);
            }
            else
            {
                return null;
            }
        }

   
        public void AddState(int stateId, int nrOfBooks, int catalogId)
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

     
        public void RemoveStateByID(int id)
        {
            States state = _libraryDataContext.States.Single(s => s.StateId == id);
            _libraryDataContext.States.DeleteOnSubmit(state);
            _libraryDataContext.SubmitChanges();
        }

        public IState? GetStateById(int id)
        {
            var state = (from s in _libraryDataContext.States
                         where s.StateId == id
                         select s).FirstOrDefault();
            return EntryToObj(state);
        }

        public IEnumerable<IState> GetAllStates()
        {
            var states = from s in _libraryDataContext.States
                         select EntryToObj(s);
            return states;
        }





        public bool DoesCatalogExist(int id)
        {
            /*if (_libraryDataContext == null) throw new InvalidOperationException("Data context is not initialized.");
            return _libraryDataContext.Books.Any(c => c.catalogId == id);*/
            return false;
        }
        public bool DoesUserExist(int id)
        {
            if (_libraryDataContext == null) throw new InvalidOperationException("Data context is not initialized.");
            return _libraryDataContext.Users.Any(u => u.UserId == id);
        }
        public bool DoesEventExist(int id)
        {
            if (_libraryDataContext == null) throw new InvalidOperationException("Data context is not initialized.");
            return _libraryDataContext.Events.Any(e => e.EventId == id);
        }
        public bool DoesStateExist(int id)
        {
            if (_libraryDataContext == null) throw new InvalidOperationException("Data context is not initialized.");
            return _libraryDataContext.States.Any(s => s.StateId == id);
        }

    }


}