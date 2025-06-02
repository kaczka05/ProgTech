using LibraryLogicLayer;
using LibraryPresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryPresentationlLayer
{
    internal class ModelState : IModelState
    {
        private IModelCatalog catalog;

        public ModelState(int stateId, int nrOfBooks, int catalog)
        {
            StateId = stateId;
            NrOfBooks = nrOfBooks;
            Catalog = catalog;
        }

        public int StateId { get; init; }
        public int NrOfBooks { get; init; }
        public int Catalog { get; init; }
        
    }
    internal class ModelUser : IModelUser
    {
        public int UserId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public ModelUser(int userId, string firstName, string lastName)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
    internal class ModelEvent : IModelEvent
    {
        public int EventId { get; init; }
        public int Employee { get; init; }
        public int State { get; init; }
        public int User { get; init; }
        public bool Borrowing { get; init; }
        public bool Addition { get; init; }

    }
    internal class ModelUserEvent : ModelEvent, IModelUserEvent
    {
        public ModelUserEvent(int eventId, int employee, int state, int user, bool borrowing)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
            User = user;
            Borrowing = borrowing;
        }
    }
    internal class ModelDatabaseEvent : ModelEvent, IModelDatabaseEvent
    {
        public ModelDatabaseEvent(int eventId, int employee, int state, bool addition)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
            Addition = addition;
        }

    }
    
    internal class ModelCatalog : IModelCatalog
    {
        public int CatalogId { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public int NrOfPages { get; init; }
        public ModelCatalog(int catalogId, string title, string author, int nrOfPages)
        {
            CatalogId = catalogId;
            Title = title;
            Author = author;
            NrOfPages = nrOfPages;
        }
    }
}
