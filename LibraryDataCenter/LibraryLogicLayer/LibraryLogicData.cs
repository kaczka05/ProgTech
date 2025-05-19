using LibraryDataLayer;
using LibraryLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogicLayer
{
    internal class LogicState : ILogicState
    {
        private ICatalog catalog;

        public LogicState(int stateId, int nrOfBooks, ICatalog catalog)
        {
            StateId = stateId;
            NrOfBooks = nrOfBooks;
            this.catalog = catalog;
        }

        public int StateId { get; init; }
        public int NrOfBooks { get; init; }
        public ILogicCatalog Catalog { get; init; }
        
    }
    internal class LogicUser : ILogicUser
    {
        public int UserId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public LogicUser(int userId, string firstName, string lastName)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
    internal class LogicEvent : ILogicEvent
    {
        public int EventId { get; init; }
        public ILogicUser Employee { get; init; }
        public ILogicState State { get; init; }
        
    }
    internal class LogicDatabaseEvent : LogicEvent, ILogicDatabaseEvent
    {
        private IUser employee;
        private IState state;

        public int EventId { get; init; }
        public ILogicUser Employee { get; init; }
        public ILogicState State { get; init; }
        public bool Addition { get; init; }
        public LogicDatabaseEvent(int eventId, ILogicUser employee, ILogicState state, bool addition)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
            Addition = addition;
        }

        public LogicDatabaseEvent(int eventId, IUser employee, IState state, bool addition)
        {
            EventId = eventId;
            this.employee = employee;
            this.state = state;
            Addition = addition;
        }
    }
    internal class LogicUserEvent : LogicEvent, ILogicUserEvent
    {
        private IUser employee;
        private IState state;
        private IUser user;

        public int EventId { get; init; }
        public ILogicUser Employee { get; init; }
        public ILogicState State { get; init; }
        public ILogicUser User { get; init; }
        public bool Borrowing { get; init; }
        public LogicUserEvent(int eventId, ILogicUser employee, ILogicState state, ILogicUser user, bool borrowing)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
            User = user;
            Borrowing = borrowing;
        }

        public LogicUserEvent(int eventId, IUser employee, IState state, IUser user, bool borrowing)
        {
            EventId = eventId;
            this.employee = employee;
            this.state = state;
            this.user = user;
            Borrowing = borrowing;
        }
    }
    internal class LogicCatalog : ILogicCatalog
    {
        public int CatalogId { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public int NrOfPages { get; init; }
        public LogicCatalog(int catalogId, string title, string author, int nrOfPages)
        {
            CatalogId = catalogId;
            Title = title;
            Author = author;
            NrOfPages = nrOfPages;
        }
    }
}
