using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogicLayer
{
    public interface ILibraryDataService
    {
        public void LogicAddCatalogue(int catalogId, string title, string author, int numberOfPages)
        public void LogicRemoveCatalogue(int id);

        public void LogicAddState(int stateId, int nrOfBooks, int catalogId);
        public void LogicRemoveState(int id);
        public void LogicAddUser(int userId, string firstName, string lastName);
        public void LogicRemoveUser(int id);
        public void LogicAddUserEvent(int eventId, int employeeId, int stateId, int userId, bool borrowing);
        public void LogicAddDatabaseEvent(int eventId, int employeeId, int stateId, bool addition);
        public void LogicRemoveEvent(int id );


    }
}
