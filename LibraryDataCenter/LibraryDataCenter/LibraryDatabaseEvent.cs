

namespace LibraryDataLayer
{
    internal class DatabaseEvent : Event, IDatabaseEvent
    {
        private IUser user;

        public bool Addition { get; init; }

        public DatabaseEvent(int eventId, User employee, State state, bool addition)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
            Addition = addition;
        }

        public DatabaseEvent(int eventId, IUser user, IState state, bool addition)
        {
            EventId = eventId;
            this.user = user;
            State = state;
            Addition = addition;
        }
    }
}
