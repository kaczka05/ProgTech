using LibraryDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogicLayer
{
    public interface ICatalogRepository
    {
        void AddCatalog(int catalogId, string title, string author, int nrOfPages);

        void RemoveCatalogById(int id);

        LibraryCatalog GetCatalogById(int id);

    }
    public interface IUserRepository
    {
        void AddUser(int userId, string firstName, string lastName);
        void RemoveUserById(int id);

        User GetUserById(int id);


    }
    public interface IEventRepository
    {
        void AddDatabaseEvent(int eventId, User employee, LibraryState state, bool addition);
        void AddUserEvent(int eventId, User employee, LibraryState state, User user, bool borrowing );

        void RemoveEventById(int id);
        LibraryEvent GetEventById(int id);

    }
    public interface IStateRepository
    {
        void AddState(int stateId, int nrOfBooks, LibraryCatalog catalog);
        void RemoveStateByID(int id);

        LibraryState GetStateById(int id);

    }
}
