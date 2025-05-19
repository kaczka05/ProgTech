

namespace LibraryDataLayer
{
    public abstract class Event : IEvent
    {
        public int EventId { get; init; }
        public IUser Employee { get; init; }
        public IState State { get; init; }

        public bool Addition { get; init; }

        public bool Borrowing { get; init; }

        public IUser User { get; init; }
        public Event(int eventId, IUser employee, IState state, bool addition)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
            Addition = addition;
        }

        protected Event(int eventId, IUser employee, IState state)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
        }
    }
}
