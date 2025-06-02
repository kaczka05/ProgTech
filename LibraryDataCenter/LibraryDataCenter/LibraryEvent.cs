

namespace LibraryDataLayer
{
    public abstract class Event : IEvent
    {
        public int EventId { get; init; }
        public int Employee { get; init; }
        public int State { get; init; }

        public bool Addition { get; init; }

        public bool Borrowing { get; init; }

        public int User { get; init; }
        public Event(int eventId, int employee, int state, bool addition)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
            Addition = addition;
        }

      


        protected Event(int eventId, int employee, int state)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
        }
    }
}
