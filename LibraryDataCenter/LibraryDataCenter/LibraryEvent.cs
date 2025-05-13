

namespace LibraryDataLayer
{
    public abstract class Event : IEvent
    {
        public int EventId { get; init; }
        public IUser Employee { get; init; }
        public IState State { get; init; }
    }
}
