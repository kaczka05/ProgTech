using LibraryDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogicLayer
{   
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

    public interface ICatalogRepository
    {
        public List<ICatalog> Catalogs { get; init; }
        void AddCatalog(int catalogId, string title, string author, int nrOfPages);

        void RemoveCatalogById(int id);

        ICatalog GetCatalogById(int id);

    }
    public interface IUserRepository
    {
        public List<IUser> Users { get; init; } 
        void AddUser(int userId, string firstName, string lastName);
        void RemoveUserById(int id);

        IUser GetUserById(int id);


    }
    public interface IEventRepository
    {
        public List<IEvent> Events { get; init; }
        void AddDatabaseEvent(int eventId, IUser employee, IState state, bool addition);
        void AddUserEvent(int eventId, IUser employee, IState state, IUser user, bool borrowing );

        void RemoveEventById(int id);
        IEvent GetEventById(int id);

    }
    public interface IStateRepository
    {
        public List<IState> States { get; init; }
        void AddState(int stateId, int nrOfBooks, ICatalog catalog);
        void RemoveStateByID(int id);

        IState GetStateById(int id);

    }
}
