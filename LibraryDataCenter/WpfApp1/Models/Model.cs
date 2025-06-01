using LibraryDataLayer;
using LibraryLogicLayer;
using LibraryPresentationLayer;
using LibraryPresentationlLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryPresentationLayer
{
    internal class Model : IModel
    {

        private ILibraryDataService _libraryDataService = default;

        internal Model(ILibraryDataService? service = null)
        {
            _libraryDataService = service ?? ILibraryDataService.CreateNewService();  //TODO
        }


        private IModelCatalog ConvertToModelCatalog(ILogicCatalog catalog) =>
            new ModelCatalog(catalog.CatalogId, catalog.Title, catalog.Author, catalog.NrOfPages);

        public async Task AddCatalogAsync(int id, string author, string title, int nrOfPages) =>
            await Task.Run(() =>
            {
                _libraryDataService.AddCatalogAsync(id, title, author, nrOfPages);
            });

        public async Task RemoveCatalogAsync(int id) =>
            await Task.Run(() =>
            {
                _libraryDataService.RemoveCatalogAsync(id);
            });

        public List<IModelCatalog> GetAllCatalogsAsync()
        {
            var listData = _libraryDataService.GetAllCatalogsAsync();
            List<IModelCatalog> result = new List<IModelCatalog>();

            foreach (var entry in listData)
            {
                result.Add(ConvertToModelCatalog(entry));
            }
            return result;
        }

        private IModelUser ConvertToModelUser(ILogicUser user) =>
            new ModelUser(user.UserId, user.FirstName, user.LastName);

        public async Task AddUserAsync(int id, string firstName, string lastName) =>
            await Task.Run(() =>
            {
                _libraryDataService.AddUserAsync(id, firstName, lastName);
            });
        public async Task RemoveUserAsync(int id) =>
            await Task.Run(() =>
            {
                _libraryDataService.RemoveUserAsync(id);
            });

        public List<IModelUser> GetAllUsersAsync()
        {
            var listData = _libraryDataService.GetAllUsersAsync();
            List<IModelUser> result = new List<IModelUser>();
            foreach (var entry in listData)
            {
                result.Add(ConvertToModelUser(entry));
            }
            return result;
        }

        private ModelUserEvent ConvertToModelDatabaseEvent(ILogicEvent userEvent) =>
            new ModelUserEvent(userEvent.EventId, userEvent.Employee, userEvent.State, userEvent.User, userEvent.Borrowing);
        private ModelDatabaseEvent ConvertToModelUserEvent(ILogicEvent databaseEvent) =>
            new ModelDatabaseEvent(databaseEvent.EventId, databaseEvent.Employee, databaseEvent.State, databaseEvent.Addition);

        private ModelEvent ConvertToModelEvent(ILogicEvent eventObj)
        {
            return eventObj switch
            {
                IModelUserEvent userEvent => ConvertToModelUserEvent(eventObj),
                IModelDatabaseEvent databaseEvent => ConvertToModelDatabaseEvent(eventObj),
                _ => throw new Exception("Unknown event type")
            };
        }


        private IModelState ConvertToModelState(ILogicState state) =>
            new ModelState(state.StateId, state.NrOfBooks, state.Catalog);
        
  

        public async Task AddDatabaseEventAsync(int id, int employeeId, int stateId, bool addition) =>
            await Task.Run(() =>
            {
                _libraryDataService.AddDatabaseEventAsync(id, employeeId, stateId, addition);
            });
        public async Task AddUserEventAsync(int id, int employeeId, int stateId, int userId, bool borrowing) =>
            await Task.Run(() =>
            {
                _libraryDataService.AddUserEventAsync(id, employeeId, stateId, userId, borrowing);
            });
        public async Task RemoveEventAsync(int id) =>
            await Task.Run(() =>
            {
                _libraryDataService.RemoveEventAsync(id);
            });
        public List<IModelEvent> GetAllEventsAsync()
        {
            var listData = _libraryDataService.GetAllEventsAsync();
            List<IModelEvent> result = new List<IModelEvent>();
            foreach (var entry in listData)
            {
                result.Add(ConvertToModelEvent(entry));
            }
            return result;
        }
        public async Task AddStateAsync(int id, int nrOfBooks, int catalogId) =>
            await Task.Run(() =>
            {
                _libraryDataService.AddStateAsync(id, nrOfBooks, catalogId);
            });
        public async Task RemoveStateAsync(int id) =>
            await Task.Run(() =>
            {
                _libraryDataService.RemoveStateAsync(id);
            });
        public List<IModelState> GetAllStatesAsync()
        {
            var listData = _libraryDataService.GetAllStatesAsync();
            List<IModelState> result = new List<IModelState>();
            foreach (var entry in listData)
            {
                result.Add(ConvertToModelState(entry));
            }
            return result;
        }



    }
}
