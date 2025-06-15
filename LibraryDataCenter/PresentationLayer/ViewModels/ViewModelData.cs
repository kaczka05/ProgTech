using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryPresentationLayer
{
    public class VMCatalog : PropertyChange
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
    public class VMUser : PropertyChange
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

    public class VMUserEvent : PropertyChange
    {

        public int EventId { get; set; }
        public int EmployeeId { get; set; }
        public int StateId { get; set; }
        public bool Borrowing { get; set; }
        public int UserId { get; set; }

        public VMUserEvent(int eventId, int employee, int state)
        {
            EventId = eventId;
            EmployeeId = employee;
            StateId = state;
        }
        public VMUserEvent()
        {
            EventId = 0;
            EmployeeId = 0;
            StateId = 0;
            Borrowing = false;
            UserId = 0;
        }

        public VMUserEvent(int eventId, int employee, int state, int user, bool borrowing)
        {
            EventId = eventId;
            EmployeeId = employee;
            StateId = state;
            UserId = user;
            Borrowing = borrowing;
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
        public int _employee
        {
            get => EmployeeId;
            set
            {
                EmployeeId = value;
                OnPropertyChanged(nameof(_employee));
            }
        }
        public int _state
        {
            get => StateId;
            set
            {
                StateId = value;
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
        public int _user
        {
            get => UserId;
            set
            {
                UserId = value;
                OnPropertyChanged(nameof(_user));
            }
        }

    }
    public class VMEvent : PropertyChange
    {

        public int EventId { get; set; }
        public int EmployeeId { get; set; }
        public int StateId { get; set; }
        public bool Addition { get; set; }
        public int UserId { get; set; }
        public bool Borrowing { get; set; }
 

        public VMEvent(int eventId, int employeeId, int stateId)
        {
            EventId = eventId;
            EmployeeId = employeeId;
            StateId = stateId;
        }
        public VMEvent()
        {
            EventId = 0;
            EmployeeId = 0;
            StateId = 0;
        }

        public VMEvent(int eventId, int employee, int state, int user, bool borrowing, bool addition)
        {
            EventId = eventId;
            EmployeeId = employee;
            StateId = state;
            UserId = user;
            Borrowing = borrowing;
            Addition = addition;
        }
    }
    public class VMDatabaseEvent : PropertyChange
    {

        public int EventId { get; set; }
        public int EmployeeId { get; set; }
        public int StateId { get; set; }
        public bool Addition { get; set; }

        public VMDatabaseEvent(int eventId, int employee, int state, bool addition)
        {
            EventId = eventId;
            EmployeeId = employee;
            StateId = state;
            Addition = addition;
        }
        public VMDatabaseEvent()
        {
            EventId = 0;
            EmployeeId = 0;
            StateId = 0;
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
        public int _employee
        {
            get => EmployeeId;
            set
            {
                EmployeeId = value;
                OnPropertyChanged(nameof(_employee));
            }
        }
        public int _state
        {
            get => StateId;
            set
            {
                StateId = value;
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
    public class VMState : PropertyChange
    {
        public int StateId { get; set; }
        public int NrOfBooks { get; set; }
        public int CatalogId { get; set; }
        public VMState(int stateId, int nrOfBooks, int catalogId)
        {
            StateId = stateId;
            NrOfBooks = nrOfBooks;
            CatalogId = catalogId;
        }
        public VMState()
        {
            StateId = 0;
            NrOfBooks = 0;
            CatalogId = 0;
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
        public int _catalog
        {
            get => CatalogId;
            set
            {
                CatalogId = value;
                OnPropertyChanged(nameof(_catalog));
            }
        }
    }


    }
