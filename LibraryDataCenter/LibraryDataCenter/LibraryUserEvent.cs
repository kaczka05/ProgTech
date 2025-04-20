using LibraryLogicLayer;

namespace LibraryDataLayer
{
    internal class UserEvent : Event, IUserEvent
    {
        public int EventId { get; init; }
        public User Employee { get; init; }
        public State State { get; init; }
        public User User { get; init; }
        public bool Borrowing { get; init; }

        public UserEvent(int eventId, User employee, State state, User user, bool borrowing)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
            User = user;
            Borrowing = borrowing;
        }
    }
}
