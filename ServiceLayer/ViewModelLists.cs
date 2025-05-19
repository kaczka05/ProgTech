using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace LibraryPresentationLayer
{
    internal class VMCatalogList : PropertyChange
    {
        private int CatalogId;
        private string Title;
        private string Author;
        private int NrOfPages;
        private VMCatalog _catalog;
        private IModelCatalog _modelCatalog;
        private IModel model;

        public ICommand AddCatalogCommand { get; }
        public ICommand RemoveCatalogCommand { get; }
        public ICommand UpdateCatalogCommand { get; }
        private ObservableCollection<VMCatalog> _catalogs;
        public VMCatalogList()
        {
            this.model = model;
            _catalogs = new ObservableCollection<VMCatalog>();
            AddCatalogCommand = new RelayCommand(e => { Add(); }, a => true);
            RemoveCatalogCommand = new RelayCommand(e => { Delete(); }, a => true);
            UpdateCatalogCommand = new RelayCommand(e => { GetCatalogs(); }, a => true);
        }
        public VMCatalogList(IModel model)
        {
            this.model = model;
            _catalogs = new ObservableCollection<VMCatalog>();
            AddCatalogCommand = new RelayCommand(e => { Add(); }, a => true);
            RemoveCatalogCommand = new RelayCommand(e => { Delete(); }, a => true);
            UpdateCatalogCommand = new RelayCommand(e => { GetCatalogs(); }, a => true);
        }
        public ObservableCollection<VMCatalog> Catalogs
        {
            get => _catalogs;
            set
            {
                _catalogs = value;
                OnPropertyChanged(nameof(Catalogs));
            }
        }
        public IModelCatalog ModelCatalog
        {
            get => _modelCatalog;
            set
            {
                _modelCatalog = value;
                OnPropertyChanged(nameof(ModelCatalog));
                _catalog = new VMCatalog(CatalogId, Title, Author, NrOfPages);
            }
        }
        public int catalogId
        {
            get => CatalogId;
            set
            {
                CatalogId = value;
                OnPropertyChanged(nameof(catalogId));
            }
        }
        public string title
        {
            get => Title;
            set
            {
                Title = value;
                OnPropertyChanged(nameof(title));
            }
        }
        public string author
        {
            get => Author;
            set
            {
                Author = value;
                OnPropertyChanged(nameof(author));
            }
        }
        public int nrOfPages
        {
            get => NrOfPages;
            set
            {
                NrOfPages = value;
                OnPropertyChanged(nameof(nrOfPages));
            }
        }
        private VMCatalog? CatalogToPresentationLayer(IModelCatalog catalog)
        {
            if (catalog == null)
                return null;
            return new VMCatalog(catalog.CatalogId, catalog.Title, catalog.Author, catalog.NrOfPages);
        }
        public void GetCatalogs()
        {
            var catalogs = model.GetAllCatalogsAsync();
            Catalogs.Clear();
            foreach (var catalog in catalogs)
            {
                Catalogs.Add(CatalogToPresentationLayer(catalog));
            }
            OnPropertyChanged(nameof(Catalogs));
        }
        private async Task Add()
        {
            await model.AddCatalogAsync(CatalogId, Title, Author, NrOfPages);
        }
        private async Task Delete()
        {
            await model.RemoveCatalogAsync(CatalogId);
        }
        public void XD()
        {

        }
    }
    internal class VMUserList : PropertyChange
    {
        private int UserId;
        private string FirstName;
        private string LastName;
        private VMUser _user;
        private IModelUser _modelUser;
        private IModel model;
        public ICommand AddUserCommand { get; }
        public ICommand RemoveUserCommand { get; }
        public ICommand UpdateUserCommand { get; }
        private ObservableCollection<VMUser> _users;
        public VMUserList()
        {
            this.model = model;
            _users = new ObservableCollection<VMUser>();
            AddUserCommand = new RelayCommand(e => { Add(); }, a => true);
            RemoveUserCommand = new RelayCommand(e => { Delete(); }, a => true);
            UpdateUserCommand = new RelayCommand(e => { GetUsers(); }, a => true);
        }
        public VMUserList(IModel model)
        {
            this.model = model;
            _users = new ObservableCollection<VMUser>();
            AddUserCommand = new RelayCommand(e => { Add(); }, a => true);
            RemoveUserCommand = new RelayCommand(e => { Delete(); }, a => true);
            UpdateUserCommand = new RelayCommand(e => { GetUsers(); }, a => true);
        }
        public ObservableCollection<VMUser> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        public IModelUser ModelUser
        {
            get => _modelUser;
            set
            {
                _modelUser = value;
                OnPropertyChanged(nameof(ModelUser));
                _user = new VMUser(UserId, FirstName, LastName);
            }
        }
        public int userId
        {
            get => UserId;
            set
            {
                UserId = value;
                OnPropertyChanged(nameof(userId));
            }
        }
        public string firstName
        {
            get => FirstName;
            set
            {
                FirstName = value;
                OnPropertyChanged(nameof(firstName));
            }
        }
        public string lastName
        {
            get => LastName;
            set
            {
                LastName = value;
                OnPropertyChanged(nameof(lastName));
            }
        }
        private VMUser? UserToPresentationLayer(IModelUser user)
        {
            if (user == null)
                return null;
            return new VMUser(user.UserId, user.FirstName, user.LastName);
        }
        public void GetUsers()
        {
            var users = model.GetAllUsersAsync();
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(UserToPresentationLayer(user));
            }
            OnPropertyChanged(nameof(Users));
        }
        private async Task Add()
        {
            await model.AddUserAsync(UserId, FirstName, LastName);
        }
        private async Task Delete()
        {
            await model.RemoveUserAsync(UserId);
        }
    }
    internal class VMEventList : PropertyChange
    {
        private int EventId;
        private IModelUser _employee;
        private IModelState _state;
        private bool Addition;
        private IModelUser _user;
        private bool Borrowing;
        private IModelEvent _modelEvent;
        private IModel model;
        public ICommand AddDatabaseEventCommand { get; }
        public ICommand AddUserEventCommand { get; }
        public ICommand RemoveEventCommand { get; }
        public ICommand UpdateEventCommand { get; }
        private ObservableCollection<IModelEvent> _events;
        public VMEventList()
        {
            this.model = model;
            _events = new ObservableCollection<IModelEvent>();
            AddDatabaseEventCommand = new RelayCommand(e => { AddDatabaseEvent(); }, a => true);
            AddUserEventCommand = new RelayCommand(e => { AddUserEvent(); }, a => true);
            RemoveEventCommand = new RelayCommand(e => { Delete(); }, a => true);
            UpdateEventCommand = new RelayCommand(e => { GetEvents(); }, a => true);
        }
        public VMEventList(IModel model)
        {
            this.model = model;
            _events = new ObservableCollection<IModelEvent>();
            AddDatabaseEventCommand = new RelayCommand(e => { AddDatabaseEvent(); }, a => true);
            AddUserEventCommand = new RelayCommand(e => { AddUserEvent(); }, a => true);
            RemoveEventCommand = new RelayCommand(e => { Delete(); }, a => true);
            UpdateEventCommand = new RelayCommand(e => { GetEvents(); }, a => true);
        }
        public ObservableCollection<IModelEvent> Events
        {
            get => _events;
            set
            {
                _events = value;
                OnPropertyChanged(nameof(Events));
            }
        }
        public IModelEvent ModelEvent
        {
            get => _modelEvent;
            set
            {
                _modelEvent = value;
                OnPropertyChanged(nameof(ModelEvent));
            }
        }
        public int eventId
        {
            get => EventId;
            set
            {
                EventId = value;
                OnPropertyChanged(nameof(eventId));
            }
        }
        public IModelUser employee
        {
            get => _employee;
            set
            {
                _employee = value;
                OnPropertyChanged(nameof(employee));
            }
        }
        public IModelState state
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged(nameof(state));
            }
        }
        public bool addition
        {
            get => Addition;
            set
            {
                Addition = value;
                OnPropertyChanged(nameof(addition));
            }
        }
        public IModelUser user
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(user));
            }
        }
        public bool borrowing
        {
            get => Borrowing;
            set
            {
                Borrowing = value;
                OnPropertyChanged(nameof(borrowing));
            }
        }
        private IModelEvent? EventToPresentationLayer(IModelEvent eventObj)
        {
            if (eventObj == null)
                return null;
            return new VMEvent(eventObj.EventId, eventObj.Employee, eventObj.State);
        }
        public void GetEvents()
        {
            var events = model.GetAllEventsAsync();
            Events.Clear();
            foreach (var eventObj in events)
            {
                Events.Add(EventToPresentationLayer(eventObj));
            }
            OnPropertyChanged(nameof(Events));
        }
        private async Task AddDatabaseEvent()
        {
            await model.AddDatabaseEventAsync(EventId, employee, state, Addition);
        }
        private async Task AddUserEvent()
        {
            await model.AddUserEventAsync(EventId, employee, state, user,borrowing);
        }
        private async Task Delete()
        {
            await model.RemoveEventAsync(EventId);
        }

    }
}