

namespace LibraryDataLayer
{
    internal class Book : ICatalog
    {
        public int catalogId { get; init; }
        public string title { get; init; }
        public string author { get; init; }
        public int nrOfPages { get; init; }

        public Book(int catalogId, string title, string author, int nrOfPages)
        {
            this.catalogId = catalogId;
            this.title = title;
            this.author = author;
            this.nrOfPages = nrOfPages;
        }
    }
}
