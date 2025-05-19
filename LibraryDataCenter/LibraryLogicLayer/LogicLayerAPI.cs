using LibraryDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogicLayer
{
    public interface ILibraryDataService
    {

        Task AddCatalogAsync(int id, string author, string title, int nrOfPages);
        Task RemoveCatalogAsync(int id);
        Task AddUserAsync(int id, string firstName, string lastName);
        Task RemoveUserAsync(int id);
        Task AddDatabaseEventAsync(int id, ILogicUser employeeId, ILogicState stateId, bool addition);
        Task AddUserEventAsync(int id, ILogicUser employeeId, ILogicState stateId, ILogicUser userId, bool borrowing);
        Task RemoveEventAsync(int id);
        Task AddStateAsync(int id, int nrOfBooks, ILogicCatalog catalogId);
        Task RemoveStateAsync(int id);

        List<ILogicCatalog> GetAllCatalogsAsync();
        List<ILogicUser> GetAllUsersAsync();
        List<ILogicEvent> GetAllEventsAsync();
        List<ILogicState> GetAllStatesAsync();

    }
    public interface ILogicCatalog
    {
        public int CatalogId { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public int NrOfPages { get; init; }
    }
    public interface ILogicUser
    {
        int UserId { get; init; }
        string FirstName { get; init; }
        string LastName { get; init; }
    }
    public interface ILogicEvent
    {
        int EventId { get; init; }
        ILogicUser Employee { get; init; }
        ILogicState State { get; init; }
        bool Addition { get; init; }
        ILogicUser User { get; init; }
        bool Borrowing { get; init; }

    }

    public interface ILogicDatabaseEvent
    {
        int EventId { get; init; }
        ILogicUser Employee { get; init; }
        ILogicState State { get; init; }
        bool Addition { get; init; }
    }

    public interface ILogicUserEvent
    {
        int EventId { get; init; }
        ILogicUser Employee { get; init; }
        ILogicState State { get; init; }
        ILogicUser User { get; init; }
        bool Borrowing { get; init; }
    }

    public interface ILogicState
    {
        int StateId { get; init; }
        int NrOfBooks { get; init; }
        ILogicCatalog Catalog { get; init; }
    }
}
