using LibraryLogicLayer;

namespace LibraryDataLayer
{
    internal class DatabaseEvent : Event, IDatabaseEvent
    {
        public int EventId { get; init; }
        public User Employee { get; init; }
        public State State { get; init; }
        public bool Addition { get; init; }

        public DatabaseEvent(int eventId, User employee, State state, bool addition)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
            Addition = addition;
        }
    }
}
