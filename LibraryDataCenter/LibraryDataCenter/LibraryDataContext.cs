


namespace LibraryDataLayer
{
    internal class LibraryDataContext : ILibraryDataContext
    {
        public List<IUser> Users { get; init; } = new List<IUser>();
        public List<ICatalog> Catalogs { get; init; } = new List<ICatalog>();
        public List<IEvent> Events { get; init; } = new List<IEvent>();
        public List<IState> States { get; init; } = new List<IState>();

    }


}