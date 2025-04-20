
namespace LibraryDataLayer
{
    internal class LibraryDataContext
    {
        public List<User> Users { get; init; } = new List<User>();
        public List<Book> Catalogs { get; init; } = new List<Book>();
        public List<Event> Events { get; init; } = new List<Event>();
        public List<State> States { get; init; } = new List<State>();
    }

}