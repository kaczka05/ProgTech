using LibraryDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDataLayer
{   
    public interface ILibraryDataContext
    {
        public List<IUser> Users { get; init; }
        public List<ICatalog> Catalogs { get; init; }
        public List<IEvent> Events { get; init; }
        public List<IState> States { get; init; }
    }
    public interface ILibraryDataRepository
    {
        
        void AddCatalog(int catalogId, string title, string author, int nrOfPages);

        void RemoveCatalogById(int id);


        void AddUser(int userId, string firstName, string lastName);
        void RemoveUserById(int id);


        void AddDatabaseEvent(int eventId, int employeeId, int stateId, bool addition);
        void AddUserEvent(int eventId, int employeeId, int stateId, int userId, bool borrowing);

        void RemoveEventById(int id);

        void AddState(int stateId, int nrOfBooks, int catalogId);
        void RemoveStateByID(int id);

        bool DoesCatalogExist(int id);
        bool DoesUserExist(int id);
        bool DoesEventExist(int id);
        bool DoesStateExist(int id);
    }
    public interface ICatalog
    {
        public int catalogId { get; init; }
        public string title { get; init; }
        public string author { get; init; }
        public int nrOfPages { get; init; }
    }
    public interface IUser
    {
        int UserId { get; init; }
        string FirstName { get; init; }
        string LastName { get; init; }
    }
    public interface IEvent
    {
        int EventId { get; init; }
        IUser Employee { get; init; }
        IState State { get; init; }
    }

    public interface IDatabaseEvent
    {
        int EventId { get; init; }
        IUser Employee { get; init; }
        IState State { get; init; }
        bool Addition { get; init; }
    }

    public interface IUserEvent
    {
        int EventId { get; init; }
        IUser Employee { get; init; }
        IState State { get; init; }
        IUser User { get; init; }
        bool Borrowing { get; init; }
    }

    public interface IState
    {
        int StateId { get; init; }
        int NrOfBooks { get; init; }
        ICatalog Catalog { get; init; }
    }

    

}
