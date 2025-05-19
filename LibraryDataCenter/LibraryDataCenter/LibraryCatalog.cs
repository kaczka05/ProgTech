

namespace LibraryDataLayer
{
    internal class Book : ICatalog
    {
        public int CatalogId { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public int NrOfPages { get; init; }

        public Book(int catalogId, string title, string author, int nrOfPages)
        {
            this.CatalogId = catalogId;
            this.Title = title;
            this.Author = author;
            this.NrOfPages = nrOfPages;
        }
    }
}
