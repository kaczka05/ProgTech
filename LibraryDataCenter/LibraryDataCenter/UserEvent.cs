namespace LibraryDataLayer
{
    public class UserEvent: Event
    {
        public Users user { get; set; }
        public bool borrowing { get; set; } //true borrowing false returning
    }
}
