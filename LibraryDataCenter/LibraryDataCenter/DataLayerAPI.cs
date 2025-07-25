﻿using LibraryDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDataLayer
{   
    public interface ILibraryDataContext
    {
        public IQueryable<ICatalog> Catalogs { get; }
        public IQueryable<IUser> Users { get; }
        public IQueryable<IEvent> Events { get; }
        public IQueryable<IState> States { get; }
        Task AddCatalogAsync(ICatalog catalog);
        Task RemoveCatalogAsync(ICatalog catalog);
        Task AddUserAsync(IUser user);
        Task RemoveUserAsync(IUser user);
        Task AddDatabaseEventAsync(IDatabaseEvent databaseEvent);
        Task AddUserEventAsync(IUserEvent userEvent);
        Task RemoveEventAsync(IEvent userEvent);
        Task AddStateAsync(IState state);
        Task RemoveStateAsync(IState state);


    }
    public interface ILibraryDataRepository
    {
        
        void AddCatalog(int catalogId, string title, string author, int nrOfPages);

        void RemoveCatalogById(int id);

        ICatalog? GetCatalogById(int id);

        IEnumerable<ICatalog> GetAllCatalogs();

        void AddUser(int userId, string firstName, string lastName);
        void RemoveUserById(int id);
        IUser? GetUserById(int id);
        IEnumerable<IUser> GetAllUsers();


        void AddDatabaseEvent(int eventId, int employeeId, int stateId, bool addition);
        void AddUserEvent(int eventId, int employeeId, int stateId, int userId, bool borrowing);


        void RemoveEventById(int id);
        IEvent? GetEventById(int id);
        IEnumerable<IEvent> GetAllEvents();


        void AddState(int stateId, int nrOfBooks, int catalogId);
        void RemoveStateByID(int id);
        IState? GetStateById(int id);
        IEnumerable<IState> GetAllStates();

        bool DoesCatalogExist(int id);
        bool DoesUserExist(int id);
        bool DoesEventExist(int id);
        bool DoesStateExist(int id);

        public static ILibraryDataRepository CreateNewRepository()
        {
            return new LibraryDataRepository();
        }

        public static ILibraryDataRepository CreateNewRepository(string connectionString)
        {
            return new LibraryDataRepository(connectionString);
        }

    }
    public interface ICatalog
    {
        public int CatalogId { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public int NrOfPages { get; init; }
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
        int Employee { get; init; }
        int State { get; init; }
        bool Addition { get; init; }
        int User { get; init; }
        bool Borrowing { get; init; }
    }

    public interface IDatabaseEvent
    {
        int EventId { get; init; }
        int Employee { get; init; }
        int State { get; init; }
        bool Addition { get; init; }
    }

    public interface IUserEvent
    {
        int EventId { get; init; }
        int Employee { get; init; }
        int State { get; init; }
        int User { get; init; }
        bool Borrowing { get; init; }
    }

    public interface IState
    {
        int StateId { get; init; }
        int NrOfBooks { get; init; }
        int Catalog { get; init; }
    }

    

}
