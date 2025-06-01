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

        public LogicState(int stateId, int nrOfBooks, int catalog)
        {
            StateId = stateId;
            NrOfBooks = nrOfBooks;
            Catalog = catalog;
        }

        public int StateId { get; init; }
        public int NrOfBooks { get; init; }
        public int Catalog { get; init; }
        
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
        public int Employee { get; init; }
        public int State { get; init; }
        public bool Addition { get; init; }
        public int User { get; init; }
        public bool Borrowing { get; init; }

    }
    internal class LogicDatabaseEvent : LogicEvent, ILogicDatabaseEvent
    {

        public int EventId { get; init; }
        public int Employee { get; init; }
        public int State { get; init; }
        public bool Addition { get; init; }
        public LogicDatabaseEvent(int eventId, int employee, int state, bool addition)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
            Addition = addition;
        }
    }
    internal class LogicUserEvent : LogicEvent, ILogicUserEvent
    {
        public int EventId { get; init; }
        public int Employee { get; init; }
        public int State { get; init; }
        public int User { get; init; }
        public bool Borrowing { get; init; }
        public LogicUserEvent(int eventId, int employee, int state, int  user, bool borrowing)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
            User = user;
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
