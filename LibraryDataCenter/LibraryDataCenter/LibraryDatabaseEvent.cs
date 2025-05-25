

namespace LibraryDataLayer
{
    internal class DatabaseEvent : Event, IDatabaseEvent
    {
        private IUser user;
        private object addition;

        public bool Addition { get; init; }

        

        public DatabaseEvent(int eventId, IUser employee, IState state, bool addition)
        : base(eventId, employee, state)
        {
            Addition = addition;
        }


    }
}
