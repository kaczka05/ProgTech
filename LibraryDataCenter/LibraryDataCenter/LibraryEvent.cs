namespace LibraryDataLayer
{
    public class LibraryEvent
    {
        public int eventId { get; set; }

        public User employee { get; set; }
        public LibraryState state { get; set; }
    }
}
