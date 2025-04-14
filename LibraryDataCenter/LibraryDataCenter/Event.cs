namespace LibraryDataLayer
{
    public class Event
    {
        public int eventId { get; set; }

        public Users employee { get; set; }
        public State state { get; set; }
    }
}
