using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryPresentationLayer
{
    public interface IModel
    {

        Task AddCatalogAsync(int id, string author, string title, int nrOfPages);
        Task RemoveCatalogAsync(int id);
        Task AddUserAsync(int id, string firstName, string lastName);
        Task RemoveUserAsync(int id);
        Task AddDatabaseEventAsync(int id, int employeeId, int stateId, bool addition);
        Task AddUserEventAsync(int id, int employeeId, int stateId, int userId, bool borrowing);
        Task RemoveEventAsync(int id);
        Task AddStateAsync(int id, int nrOfBooks, int catalogId);
        Task RemoveStateAsync(int id);

        List<IModelCatalog> GetAllCatalogsAsync();
        List<IModelUser> GetAllUsersAsync();
        List<IModelEvent> GetAllEventsAsync();
        List<IModelState> GetAllStatesAsync();

    }
    public interface IModelCatalog
    {
        public int CatalogId { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public int NrOfPages { get; init; }
    }
    public interface IModelUser
    {
        int UserId { get; init; }
        string FirstName { get; init; }
        string LastName { get; init; }
    }
    public interface IModelEvent
    {
        int EventId { get; init; }
        IModelUser Employee { get; init; }
        IModelState State { get; init; }
        IModelUser User { get; set; }
        bool Borrowing { get; set; }
        bool Addition { get; set; }
    }

    public interface IModelDatabaseEvent
    {
        int EventId { get; init; }
        IModelUser Employee { get; init; }
        IModelState State { get; init; }
        bool Addition { get; init; }
    }

    public interface IModelUserEvent
    {
        int EventId { get; init; }
        IModelUser Employee { get; init; }
        IModelState State { get; init; }
        IModelUser User { get; init; }
        bool Borrowing { get; init; }
    }

    public interface IModelState
    {
        int StateId { get; init; }
        int NrOfBooks { get; init; }
        IModelCatalog Catalog { get; init; }
    }
}
