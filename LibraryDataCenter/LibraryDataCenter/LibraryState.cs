using LibraryLogicLayer;

namespace LibraryDataLayer
{
    internal class State : IState
    {
        public int StateId { get; init; }
        public int NrOfBooks { get; init; }
        public Book Catalog { get; init; }

        public State(int stateId, int nrOfBooks, Book catalog)
        {
            StateId = stateId;
            NrOfBooks = nrOfBooks;
            Catalog = catalog;
        }
    }
}
