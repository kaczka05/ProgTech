

namespace LibraryDataLayer
{
    internal class UserEvent : Event, IUserEvent
    {
        private IUser user1;
        private IUser user2;



        public UserEvent(int eventId, IUser employee, IState state, IUser user, bool borrowing)
           : base(eventId, employee, state)
        {
            User = user;
            Borrowing = borrowing;
        }

    }
}
