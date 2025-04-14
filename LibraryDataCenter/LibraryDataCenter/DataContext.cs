
namespace LibraryDataLayer
{
    public class DataContext
    {
        public List<User> Users { get; } = new List<User>();
        public List<Catalog> Catalogs { get; } = new List<Catalog>();
        public List<Event> Events { get; } = new List<Event>();
        public List<State> States { get; } = new List<State>();
    }

}