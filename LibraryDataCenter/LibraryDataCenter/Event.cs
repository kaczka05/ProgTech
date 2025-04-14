namespace LibraryDataLayer
{
    public class Event
    {
        public int eventId { get; set; }

        public User employee { get; set; }
        public State state { get; set; }
    }
}
