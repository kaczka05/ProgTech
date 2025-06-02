

namespace LibraryDataLayer
{
    internal class DatabaseEvent : Event, IDatabaseEvent
    {


        

        public DatabaseEvent(int eventId, int employee, int state, bool addition)
        : base(eventId, employee, state)
        {
            Addition = addition;
        }

      
    }
}
