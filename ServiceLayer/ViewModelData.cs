using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryPresentationLayer
{
    internal class VMCatalog : PropertyChange
    {
        public int CatalogId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int NrOfPages { get; set; }
        public VMCatalog(int catalogId, string title, string author, int nrOfPages)
        {
            CatalogId = catalogId;
            Title = title;
            Author = author;
            NrOfPages = nrOfPages;
        }
        public VMCatalog()
        {
            CatalogId = 0;
            Title = string.Empty;
            Author = string.Empty;
            NrOfPages = 0;
        }
        public int _catalogId
        {
            get => CatalogId;

            set
            {
                CatalogId = value;
                OnPropertyChanged(nameof(_catalogId));
            }
        }
        public string _title
        {
            get => Title;
            set
            {
                Title = value;
                OnPropertyChanged(nameof(_title));
            }
        }
        public string _author
        {
            get => Author;
            set
            {
                Author = value;
                OnPropertyChanged(nameof(_author));
            }
        }
        public int _nrOfPages
        {
            get => NrOfPages;
            set
            {
                NrOfPages = value;
                OnPropertyChanged(nameof(_nrOfPages));
            }
        }

    }
    internal class VMUser : PropertyChange
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public VMUser(int userId, string firstName, string lastName)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
        }
        public VMUser()
        {
            UserId = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
        }
        public int _userId
        {
            get => UserId;
            set
            {
                UserId = value;
                OnPropertyChanged(nameof(_userId));
            }
        }
        public string _firstName
        {
            get => FirstName;
            set
            {
                FirstName = value;
                OnPropertyChanged(nameof(_firstName));
            }
        }
        public string _lastName
        {
            get => LastName;
            set
            {
                LastName = value;
                OnPropertyChanged(nameof(_lastName));
            }
        }
    }

    internal class VMUserEvent : PropertyChange
    {
        public int EventId { get; set; }
        public VMUser Employee { get; set; }
        public VMState State { get; set; }
        public bool Borrowing { get; set; }
        public VMUser User { get; set; }

        public VMUserEvent(int eventId, VMUser employee, VMState state)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
        }
        public VMUserEvent()
        {
            EventId = 0;
            Employee = new VMUser();
            State = new VMState();
            Borrowing = false;
            User = new VMUser();
        }
        public int _eventId
        {
            get => EventId;
            set
            {
                EventId = value;
                OnPropertyChanged(nameof(_eventId));
            }
        }
        public VMUser _employee
        {
            get => Employee;
            set
            {
                Employee = value;
                OnPropertyChanged(nameof(_employee));
            }
        }
        public VMState _state
        {
            get => State;
            set
            {
                State = value;
                OnPropertyChanged(nameof(_state));
            }
        }
        public bool _borrowing
        {
            get => Borrowing;
            set
            {
                Borrowing = value;
                OnPropertyChanged(nameof(_borrowing));
            }
        }
        public VMUser _user
        {
            get => User;
            set
            {
                User = value;
                OnPropertyChanged(nameof(_user));
            }
        }

    }
    internal class VMDatabaseEvent : PropertyChange
    {

        public int EventId { get; set; }
        public VMUser Employee { get; set; }
        public VMState State { get; set; }
        public bool Addition { get; set; }
        public VMDatabaseEvent(int eventId, VMUser employee, VMState state, bool addition)
        {
            EventId = eventId;
            Employee = employee;
            State = state;
            Addition = addition;
        }
        public VMDatabaseEvent()
        {
            EventId = 0;
            Employee = new VMUser();
            State = new VMState();
            Addition = false;
        }
        public int _eventId
        {
            get => EventId;
            set
            {
                EventId = value;
                OnPropertyChanged(nameof(_eventId));
            }
        }
        public VMUser _employee
        {
            get => Employee;
            set
            {
                Employee = value;
                OnPropertyChanged(nameof(_employee));
            }
        }
        public VMState _state
        {
            get => State;
            set
            {
                State = value;
                OnPropertyChanged(nameof(_state));
            }
        }
        public bool _addition
        {
            get => Addition;
            set
            {
                Addition = value;
                OnPropertyChanged(nameof(_addition));
            }
        }
    }
    internal class VMState : PropertyChange
    {
        public int StateId { get; set; }
        public int NrOfBooks { get; set; }
        public VMCatalog Catalog { get; set; }
        public VMState(int stateId, int nrOfBooks, VMCatalog catalog)
        {
            StateId = stateId;
            NrOfBooks = nrOfBooks;
            Catalog = catalog;
        }
        public VMState()
        {
            StateId = 0;
            NrOfBooks = 0;
            Catalog = new VMCatalog();
        }
        public int _stateId
        {
            get => StateId;
            set
            {
                StateId = value;
                OnPropertyChanged(nameof(_stateId));
            }
        }
        public int _nrOfBooks
        {
            get => NrOfBooks;
            set
            {
                NrOfBooks = value;
                OnPropertyChanged(nameof(_nrOfBooks));
            }
        }
    }


    }
