namespace LibraryDataLayer
{
    public class LibraryUserEvent: LibraryEvent
    {
        public User user { get; set; }
        public bool borrowing { get; set; } //true borrowing false returning
    }
}
