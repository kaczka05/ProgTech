

namespace LibraryDataLayer
{
    internal class UserEvent : Event, IUserEvent
    {

        public UserEvent(int eventId, int employee, int state, int user, bool borrowing)
           : base(eventId, employee, state)
        {
            User = user;
            Borrowing = borrowing;
        }

      
    }
}
