using LibraryLogicLayer;

namespace LibraryDataLayer
{
    internal class UserEvent : Event, IUserEvent
    {
        private IUser user1;
        private IUser user2;

        public IUser User { get; init; }
        public bool Borrowing { get; init; }

        public UserEvent(int eventId, User employee, State state, User user, bool borrowing)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
            User = user;
            Borrowing = borrowing;
        }

        public UserEvent(int eventId, IUser user1, IState state, IUser user2, bool borrowing)
        {
            EventId = eventId;
            this.user1 = user1;
            State = state;
            this.user2 = user2;
            Borrowing = borrowing;
        }
    }
}
