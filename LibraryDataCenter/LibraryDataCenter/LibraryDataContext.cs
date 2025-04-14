
namespace LibraryDataLayer
{
    public class LibraryDataContext
    {
        public List<User> Users { get; } = new List<User>();
        public List<LibraryCatalog> Catalogs { get; } = new List<LibraryCatalog>();
        public List<LibraryEvent> Events { get; } = new List<LibraryEvent>();
        public List<LibraryState> States { get; } = new List<LibraryState>();
    }

}