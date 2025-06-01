

namespace LibraryDataLayer
{
    internal class State : IState
    {
        private int? nrOfBooks;
        private int? catalog;

        public int StateId { get; init; }
        public int NrOfBooks { get; init; }
        public ICatalog Catalog { get; init; }

        public State(int stateId, int nrOfBooks, Book catalog)
        {
            StateId = stateId;
            NrOfBooks = nrOfBooks;
            Catalog = catalog;
        }

        public State(int stateId, int nrOfBooks, ICatalog catalog)
        {
            StateId = stateId;
            NrOfBooks = nrOfBooks;
            Catalog = catalog;
        }

        public State(int stateId, int? nrOfBooks, int? catalog)
        {
            StateId = stateId;
            this.nrOfBooks = nrOfBooks;
            this.catalog = catalog;
        }
    }
}
