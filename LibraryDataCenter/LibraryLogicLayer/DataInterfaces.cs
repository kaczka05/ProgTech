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

        Catalog GetCatalogById(int id);

    }
    public interface IUserRepository
    {
        void AddUser(int userId, string firstName, string lastName);
        void RemoveUserById(int id);

        User GetUserById(int id);


    }
    public interface IEventRepository
    {
        void AddDatabaseEvent(int eventId, User employee, State state, bool addition);
        void AddUserEvent(int eventId, User employee, State state, User user, bool borrowing );

        void RemoveEventById(int id);
        Event GetEventById(int id);

    }
    public interface IStateRepository
    {
        void AddState(int stateId, int nrOfBooks, Catalog catalog);
        void RemoveStateByID(int id);

        State GetStateById(int id);

    }
}
